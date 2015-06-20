using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;

namespace DokanNFC
{
    /// <summary>
    /// This is a base user control , Containing all code for handling TABs 
    /// & the other messages. Inherit your propertysheet(s) from this control .
    /// Note : Loading of pages happens in  SheetLoader.cs
    /// http://www.codeproject.com/Articles/8065/Property-Sheet-Shell-Extension-in-C
    /// </summary>
    public partial class SheetControl : UserControl
    {
        private PROPSHEETPAGE psp;
        private DialogProc dlgProc;					// The property page's window procedure  
        private PropSheetCallback callback;			// The property page's callback procedure
        private IntPtr dlgTemplate = IntPtr.Zero;	// The dialog template for this property page

        private bool shift;							// Is the shift key pressed? For TAB/SHIFTTAB
        private bool modified;						// Has any control value on page been modifie

        private string title = "dwProperty";		// The title of the property page
        public string SheetTitle
        {
            get { return title; }
            set { title = value; }
        }

        protected IntPtr hWin32Dlg = IntPtr.Zero;
        protected System.Windows.Forms.NotifyIcon SheetIcon;

        public SheetControl()
        {
            InitializeComponent();

            dlgProc = new DialogProc(PropPageDialogProc);
            callback = new PropSheetCallback(PropSheetPageProc);
        }

        public PROPSHEETPAGE GetPSP(short cX, short cY)
        {
            psp = new PROPSHEETPAGE();

            psp.dwSize = Marshal.SizeOf(typeof(PROPSHEETPAGE));
            psp.hInstance = IntPtr.Zero;
            psp.dwFlags = PSP.DEFAULT | PSP.USECALLBACK | PSP.DLGINDIRECT;

            // We're using just a plain resource file as a "placeholder" for our 
            // .NET Framework classes placed controls
            // Warning: this is NOT supported feature in Windows Forms,
            // The only supported unmanaged container for managed controls is IE			
            psp.pResource = GetDialogTemplate(cX, cY);

            // Application defined data
            psp.lParam = IntPtr.Zero;

            // Set the property page's title and icon
            psp.pszTitle = title;

            if (SheetIcon.Icon != null) psp.hIcon = SheetIcon.Icon.Handle;
            else psp.hIcon = IntPtr.Zero;

            // Our delegate will be called when window message arrives
            psp.pfnDlgProc = dlgProc;
            psp.pfnCallback = callback;

            // Set if our property page uses an icon or title
            if (psp.hIcon != IntPtr.Zero) psp.dwFlags |= PSP.USEHICON;
            if (psp.pszTitle != null) psp.dwFlags |= PSP.USETITLE;

            return psp;
        }

        //-------------------------------------------------
        // We need to tell the Win32 side of things to create an empty
        // dialog. The best way to do this is to give it a blank dialog template.
        //

        /// <summary>
        /// Get a pointer to template object
        /// </summary>
        /// <param name="cX">Page width</param>
        /// <param name="cY">Page Heitgh</param>
        /// <returns></returns>
        private IntPtr GetDialogTemplate(short cX, short cY)
        {
            // If we're already created a template, don't bother doing it again
            if (dlgTemplate != IntPtr.Zero) return dlgTemplate;

            DLGTEMPLATE dlg = new DLGTEMPLATE();

            dlg.cx = cX;
            dlg.cy = cY;

            // Put in the standard font 
            dlg.style = DS.SETFONT;
            dlg.fontPointSize = 8;
            byte[] bFontFace = StringToByteArray("MS Shell Dlg");

            int nSize = Marshal.SizeOf(typeof(DLGTEMPLATE));
            dlgTemplate = Marshal.AllocHGlobal(nSize + bFontFace.Length);
            Marshal.StructureToPtr(dlg, dlgTemplate, false);

            // Now copy the string into the IntPtr
            Marshal.Copy(bFontFace, 0, (IntPtr)((Int64)dlgTemplate + nSize), bFontFace.Length);

            return dlgTemplate;
        }

        /// <summary>
        /// Handle all messages coming sent to the property page
        /// </summary>
        /// <param name="hwndDlg"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public bool PropPageDialogProc(IntPtr hwndDlg, uint msg, IntPtr wParam, IntPtr lParam)
        {
            switch ((WM)msg)
            {
                case WM.INITDIALOG:
                    // Set up our parent managed control first
                    // Warning: this is NOT supported feature in Windows Forms,
                    // The only supported unmanaged container for managed controls is IE
                    this.hWin32Dlg = hwndDlg;
                    ShellAPIWrapper.SetParent(this.Handle, this.hWin32Dlg);
                    break;

                case WM.PAINT:
                    // MS sample used to have a Refresh() here. (Then you can return true?).
                    // If you don't return false ,you keep on getting paint messages!
                    return false;

                case WM.NOTIFY:
                    // Most notifications are sent in the form of a WM_NOTIFY message.
                    // To set the return value that some of the notifications require, the 
                    // dialog box procedure must call SetWindowLong function and return TRUE. 
                    // Do not kill,add,remove pages/dialogs during most notification handling

                    int pResult;
                    if (OnNotify(wParam, lParam, out pResult))
                        ShellAPIWrapper.SetWindowLong(hWin32Dlg, (int)DWL.MSGRESULT, pResult);
                    // if we dont send, should we still send return true?

                    break;

                // case WM.DESTROY: ? If an application processes this message, it should return zero!

                default:
                    // We'll let the default windows (dialog procedure) message 
                    // handler handle the rest of the messages  
                    return false;

                // WM.COMMAND,WM.GETDLGCODE,WM.KILLFOCUS,WM.SETFOCUS,WM.CHAR never arrive
            }
            return true;
        }

        /// <summary>
        /// Page handler function
        /// </summary>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="pResult"></param>
        /// <returns></returns>
        protected virtual bool OnNotify(IntPtr wParam, IntPtr lParam, out int pResult)
        {
            NMHDR nmhdr = (NMHDR)Marshal.PtrToStructure(lParam, typeof(NMHDR));
            pResult = 0;

            // don't handle messages not from the page/sheet itself?
            if (nmhdr.hwndFrom != hWin32Dlg && nmhdr.hwndFrom != ShellAPIWrapper.GetParent(hWin32Dlg))
            {
                return false;
            }

            switch ((PSN)nmhdr.code)
            {
                case PSN.SETACTIVE:
                    // Notifies a page that it is about to be activated.
                    // Returns zero to accept the activation, or -1 to activate the next or  
                    // the previous page (depending on whether the user clicked Next or Back). 
                    pResult = 0;
                    break;

                case PSN.KILLACTIVE:
                    // Notifies a page that it is about to lose activation either because another page 
                    // is being activated or the user has clicked the OK button. 
                    // We always return true for this because we validate on control already
                    pResult = 0;
                    break;

                case PSN.APPLY:
                    // Sometimes the Apply button causes a page to make a change to a property sheet, 
                    // and the change cannot be undone. When this happens, the page must send the 
                    // PSM_CANCELTOCLOSE message to the property sheet. --> doesn't work

                    //	After making such a change, a page can send either the PSM_RESTARTWINDOWS or 
                    //	PSM_REBOOTSYSTEM message to the property sheet. to restart the system!!

                    if (modified)
                    {
                        OnApply();
                        modified = false;
                    }

                    pResult = (int)PSNRET.NOERROR;
                    break;

                case PSN.RESET:
                    // Notifies a page that the property sheet is about to be destroyed. 
                    // I think this means cancel, we don't care ... MFC: OnReset();
                    break;

                case PSN.QUERYCANCEL:
                    break;

                /////////////////////////////////////////////////////////////////////////
                // Wizard messages

                case PSN.WIZNEXT:
                    //WINBUG: Win32 will send a PSN_WIZBACK even if the button is disabled.
                    break;

                case PSN.WIZBACK:
                    //WINBUG: Win32 will send a PSN_WIZBACK even if the button is disabled.
                    break;

                case PSN.WIZFINISH:
                    break;

                //////////////////////////////////////////////////////////////////////////////////

                case PSN.HELP:
                    // -- For help, we can use tool tips instead. cool.
                    // SendMessage(WM_COMMAND, ID_HELP);
                    break;

                case PSN.QUERYINITIALFOCUS:
                    pResult = 0;
                    // An application must not call the SetFocus function while 
                    // handling this notification. Return the handle of the control 
                    // that should receive focus.
                    break;

                case PSN.TRANSLATEACCELERATOR:
                    // Accelerator keys messages.
                    // We handle this message to handle TAB and SHIFT TAB buttons.
                    // Hot keys must be handled here too. Weired: these already work in hyenet!

                    // Note: tried overriding ProcessTabKey(),ProcessDialogKey(),WndProc(),_KeyPress(),_KeyDown().
                    // Also try passing message to usercontrol() with out much success (for the TAB problem). 
                    // Also tried changing our base type to Form instead of UserControl, but didn't work.

                    PSHNOTIFY pshn = (PSHNOTIFY)Marshal.PtrToStructure(lParam, typeof(PSHNOTIFY));
                    MSG iMsg = (MSG)Marshal.PtrToStructure(pshn.lParam, typeof(MSG));
                    pResult = (int)PSNRET.NOERROR;

                    // shift key
                    if ((int)iMsg.wParam == 16)
                        if (iMsg.message == (uint)WM.KEYDOWN) shift = true;
                        else if (iMsg.message == (uint)WM.KEYUP) shift = false;

                    // tab key
                    if ((int)iMsg.wParam == 9 && iMsg.message == (uint)WM.KEYDOWN)
                    {
                        ProcessTabKey(!shift);

                        // 1- visual selection (khattaye doresh) of TAB doesn't work on DW server console.
                        // 2- If control at end / start send WM.NEXTDLGCTL to property sheet to WRAP.

                        pResult = (int)PSNRET.MESSAGEHANDLED;
                        // no more processing needed (remove windows tab, with it's wrong order)
                    }

                    break;

                default:
                    return false;   //Not handled
            }
            return true;			//Handled
        }

        protected virtual void OnApply()
        {
        }

        /// <summary>
        /// Handle Page Initialization and destruction 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="uMsg"></param>
        /// <param name="ppsp"></param>
        /// <returns></returns>
        public uint PropSheetPageProc(IntPtr hWnd, uint uMsg, IntPtr ppsp)
        {
            switch ((PSPCB)uMsg)
            {
                case PSPCB.ADDREF:
                    break;

                case PSPCB.RELEASE:
                    arrKeepAlive.Remove(this);
                    break;

                case PSPCB.CREATE:
                    return 1;
            }
            return 0;
        }

        /// <summary>
        /// Converts a string to array of bytes(unicode)
        /// </summary>
        /// <param name="input">String to convert</param>
        /// <returns>Array of bytes</returns>
        public static byte[] StringToByteArray(string input)
        {
            int iStrLength = input.Length;
            byte[] output = new byte[(iStrLength + 1) * 2];
            char[] cinput = input.ToCharArray();

            int j = 0;
            for (int i = 0; i < iStrLength; i++)
            {
                output[j++] = (byte)cinput[i];
                output[j++] = 0;
            }

            output[j++] = 0;
            output[j] = 0;

            return output;
        }


        /// <summary>
        /// ArrayList containing objects we want to stop 
        /// GC from collecting until RELEASE message arrives.
        /// </summary>
        static ArrayList arrKeepAlive = new ArrayList();

        /// <summary>
        /// Add object in the list of handlers that are currently active.
        /// and will be removed when PSPCB.RELEASE arrives.
        /// </summary>
        public void KeepAlive()
        {
            arrKeepAlive.Add(this);
        }
    }
}
