using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DokanNFC
{
    /// <summary>
    /// Definitions for COM interfaces.
    /// http://www.codeproject.com/Articles/8065/Property-Sheet-Shell-Extension-in-C
    /// </summary>

    public delegate bool LPFNSVADDPROPSHEETPAGE(IntPtr psp, IntPtr lParam);
    public delegate uint PropSheetCallback(IntPtr hwnd, uint uMsg, IntPtr ppsp);
    public delegate bool DialogProc(IntPtr hwndDlg, uint uMsg, IntPtr wParam, IntPtr lParam);


    #region COM Interfaces

    [ComImport(), InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    GuidAttribute("0000010e-0000-0000-C000-000000000046")]
    public interface IDataObject
    {
        void GetData(ref FORMATETC pFormatetc, ref STGMEDIUM medium);
        void GetDataHere(int formatetc, ref STGMEDIUM pmedium);
        void QueryGetData(int formatetc);
        void GetCanonicalFormatEtc(int formatetcIn, ref int formatetcOut);
        void SetData(int formatetc, int pmedium, int release);
        void EnumFormatEtc(uint direction, ref Object enumFormatetc);
        void DAdvise(int formatetc, uint advf, Object advSink, ref uint connection);
        void DUnadvise(uint connection);
        void EnumDAdvise(ref Object ppenumAdvise);
    }

    /// <summary>
    /// IShellExtInit
    /// </summary>
    [ComImport(), InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    GuidAttribute("000214e8-0000-0000-c000-000000000046")]
    public interface IShellExtInit
    {
        [PreserveSig()]
        int Initialize(IntPtr pidlFolder, IDataObject lpdobj, uint hKeyProgID);
    }

    /// <summary>
    /// IShellPropSheetExt
    /// </summary>
    [ComImport(), InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    GuidAttribute("000214e9-0000-0000-c000-000000000046")]
    public interface IShellPropSheetExt
    {
        [PreserveSig()]
        int AddPages([MarshalAs(UnmanagedType.FunctionPtr)] LPFNSVADDPROPSHEETPAGE pfnAddPage, IntPtr lParam);

        [PreserveSig()]
        int ReplacePage(uint uPageID, IntPtr lpfnReplacePage, IntPtr lParam);
    }


    #endregion


    #region Defs, Enums, Structs

    public enum CLIPFORMAT : uint
    {
        TEXT = 1,
        BITMAP = 2,
        METAFILEPICT = 3,
        SYLK = 4,
        DIF = 5,
        TIFF = 6,
        OEMTEXT = 7,
        DIB = 8,
        PALETTE = 9,
        PENDATA = 10,
        RIFF = 11,
        WAVE = 12,
        UNICODETEXT = 13,
        ENHMETAFILE = 14,
        HDROP = 15,
        LOCALE = 16,
        MAX = 17,

        OWNERDISPLAY = 0x0080,
        DSPTEXT = 0x0081,
        DSPBITMAP = 0x0082,
        DSPMETAFILEPICT = 0x0083,
        DSPENHMETAFILE = 0x008E,

        PRIVATEFIRST = 0x0200,
        PRIVATELAST = 0x02FF,

        GDIOBJFIRST = 0x0300,
        GDIOBJLAST = 0x03FF
    }

    public enum DVASPECT : uint
    {
        CONTENT = 1,
        THUMBNAIL = 2,
        ICON = 4,
        DOCPRINT = 8
    }

    public enum TYMED : uint
    {
        HGLOBAL = 1,
        FILE = 2,
        ISTREAM = 4,
        ISTORAGE = 8,
        GDI = 16,
        MFPICT = 32,
        ENHMF = 64,
        NULL = 0
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FORMATETC
    {
        public CLIPFORMAT cfFormat;
        public IntPtr ptd;
        public DVASPECT dwAspect;
        public int lindex;
        public TYMED tymed;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct STGMEDIUM
    {
        public uint tymed;
        public IntPtr hGlobal;
        public IntPtr pUnkForRelease;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DSA_NEWOBJ_DISPINFO
    {
        public uint dwSize;
        public IntPtr hObjClassIcon;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszWizTitle;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszContDisplayName;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PROPSHEETPAGE
    {
        public int dwSize;
        public PSP dwFlags;
        public IntPtr hInstance;
        public IntPtr pResource;
        public IntPtr hIcon;
        public string pszTitle;
        public DialogProc pfnDlgProc;
        public IntPtr lParam;

        public PropSheetCallback pfnCallback;

        public int pcRefParent;
        public string pszHeaderTitle;
        public string pszHeaderSubTitle;
    }

    public enum PSP : uint
    {
        DEFAULT = 0x00000000,
        DLGINDIRECT = 0x00000001,
        USEHICON = 0x00000002,
        USEICONID = 0x00000004,
        USETITLE = 0x00000008,
        RTLREADING = 0x00000010,

        HASHELP = 0x00000020,
        USEREFPARENT = 0x00000040,
        USECALLBACK = 0x00000080,
        PREMATURE = 0x00000400,

        HIDEHEADER = 0x00000800,
        USEHEADERTITLE = 0x00001000,
        USEHEADERSUBTITLE = 0x00002000
    }

    public enum DS : uint
    {
        SETFONT = 0x40,
        FIXEDSYS = 0x0008
    }

    public enum DSA_NEWOBJ_CTX
    {
        PRECOMMIT = 0x00000001,
        COMMIT = 0x00000002,
        POSTCOMMIT = 0x00000003,
        CLEANUP = 0x00000004
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Auto)]
    public struct DLGTEMPLATE
    {
        internal DS style;
        internal uint extendedStyle;
        internal ushort cdit;
        internal short x;
        internal short y;
        internal short cx;
        internal short cy;
        internal short menuResource;
        internal short windowClass;
        internal short titleArray;
        internal short fontPointSize;
        [MarshalAs(UnmanagedType.LPWStr)]
        internal string fontTypeface;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DSOBJECT
    {
        public uint dwFlags;
        public uint dwProviderFlags;
        public uint offsetName;
        public uint offsetClass;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DSOBJECTNAMES
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.I1, SizeConst = 16)]
        public byte[] clsidNamespace;
        public uint cItems;
        public DSOBJECT aObjects;
    }

    public enum WM : uint
    {
        INITDIALOG = 0x0110,
        COMMAND = 0x0111,
        DESTROY = 0x0002,
        NOTIFY = 0x004E,
        PAINT = 0x000F,
        USER = 0x0400,
        SETFOCUS = 0x0007,
        KILLFOCUS = 0x0008,
        GETDLGCODE = 0x0087,
        NEXTDLGCTL = 0x0028,

        KEYFIRST = 0x0100,
        KEYDOWN = 0x0100,
        KEYUP = 0x0101,
        CHAR = 0x0102,
        DEADCHAR = 0x0103,
        SYSKEYDOWN = 0x0104,
        SYSKEYUP = 0x0105,
        SYSCHAR = 0x0106,
        SYSDEADCHAR = 0x0107,
        UNICHAR = 0x0109,
        KEYLAST = 0x0109
        //UNICODE_NOCHAR                  0xFFFF

    }
    public enum DWL : int
    {
        MSGRESULT = 0
        // DLGPROC
        // USER
    }

    public enum PSN : int
    {
        FIRST = -200,//(0U-200U),
        LAST = -299,//(0U-299U),

        SETACTIVE = (FIRST - 0),
        KILLACTIVE = (FIRST - 1),
        APPLY = (FIRST - 2),
        RESET = (FIRST - 3),
        HELP = (FIRST - 5),
        WIZBACK = (FIRST - 6),
        WIZNEXT = (FIRST - 7),
        WIZFINISH = (FIRST - 8),
        QUERYCANCEL = (FIRST - 9),

        // these were commented by Win32 files itself
        // PSN_VALIDATE    (FIRST-1) 
        // PSN_CANCEL      (FIRST-3) 

        GETOBJECT = (FIRST - 10),
        TRANSLATEACCELERATOR = (FIRST - 12),
        QUERYINITIALFOCUS = (FIRST - 13)
    }

    public enum PSNRET : uint
    {
        NOERROR = 0,
        INVALID = 1,
        INVALID_NOCHANGEPAGE = 2,
        MESSAGEHANDLED = 3
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MSG
    {
        public IntPtr hwnd;
        public uint message;
        public IntPtr wParam;
        public IntPtr lParam;
        public uint time;
        public POINT pt;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PSHNOTIFY
    {
        public NMHDR hdr;
        public IntPtr lParam;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NMHDR
    {
        public IntPtr hwndFrom;
        public IntPtr idFrom;
        public uint code;
    }

    public enum PSPCB : uint
    {
        ADDREF = 0,
        RELEASE = 1,
        CREATE = 2
    }

    #endregion
}
