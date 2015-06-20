using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DokanNFC
{
    /// <summary>
    /// Summary description for ShellAPIWrapper.
    /// 
    /// </summary>
    public class ShellAPIWrapper
    {
        [DllImport("comctl32.dll", EntryPoint = "CreatePropertySheetPage")]
        private static extern IntPtr CreatePropertySheetPage_(ref PROPSHEETPAGE psp);

        public static IntPtr CreatePropertySheetPage(ref PROPSHEETPAGE psp)
        {
            IntPtr hPage = ShellAPIWrapper.CreatePropertySheetPage_(ref psp);
            if (hPage == IntPtr.Zero) throw new Exception("CreatePropertySheetPage failed");
            return hPage;
        }

        [DllImport("comctl32.dll")]
        public static extern IntPtr DestroyPropertySheetPage(IntPtr hProp);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint RegisterClipboardFormat(
            string lpszFormat);					// name of new format - LPCTSTR 

        [DllImport("ole32", SetLastError = true)]	// SETLAST by us
        public static extern void ReleaseStgMedium(ref STGMEDIUM pmedium);

        [DllImport("user32.dll", SetLastError = true)] // SETLAST by us
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern long SetWindowLong(
            IntPtr hWnd,		// handle to window
            int nIndex,			// offset of value to set
            int dwNewLong		// new value // LONG
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetParent(IntPtr hWnd);
    }
}
