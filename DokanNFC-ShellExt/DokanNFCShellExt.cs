using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DokanNFC
{
    [Guid("D7FF7986-8FCF-408B-B54D-D8D9BA4EACCD"), ComVisible(true)]
    public class DokanNFCShellExt : IShellExtInit, IShellPropSheetExt
    {
        private IDataObject dobj = null;

        public DokanNFCShellExt()
        {

        }

        /// <summary>
        /// Get data about object we want properties for
        /// </summary>
        /// <param name="pidlFolder"></param>
        /// <param name="lpdobj"></param>
        /// <param name="hKeyProgID"></param>
        /// <returns></returns>
        int IShellExtInit.Initialize(IntPtr pidlFolder, IDataObject lpdobj, uint hKeyProgID)
        {
            dobj = lpdobj;
            return 0;
        }

        /// <summary>
        /// Add property page for this object
        /// </summary>
        /// <param name="lpfnAddPage"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        int IShellPropSheetExt.AddPages(LPFNSVADDPROPSHEETPAGE pfnAddPage, IntPtr lParam)
        {
            try
            {
                // ADD PAGES HERE
                SheetControl samplePage;
                PROPSHEETPAGE psp;
                IntPtr hPage;

                // create new inherited property page(s) and pass dobj to it
                samplePage = new DokanNFCSheet();
                psp = samplePage.GetPSP(250, 230);

                hPage = ShellAPIWrapper.CreatePropertySheetPage(ref psp);
                bool result = pfnAddPage(hPage, lParam);
                if (!result)
                {
                    ShellAPIWrapper.DestroyPropertySheetPage(hPage);
                }

                // We'll add reference manualy only if there is no exceptions
                samplePage.KeepAlive();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            return 0;
        }

        /// <summary>
        /// Not Used here!
        /// </summary>
        /// <param name="uPageID"></param>
        /// <param name="lpfnReplacePage"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        int IShellPropSheetExt.ReplacePage(uint uPageID, IntPtr lpfnReplacePage, IntPtr lParam)
        {
            return 0;
        }
    }
}
