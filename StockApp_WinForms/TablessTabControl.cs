using System;
using System.Windows.Forms;

namespace StockApp_WinForms
{
    class TablessTabControl : TabControl
    {
        protected override void WndProc(ref Message m)
        {
            // Masquer les onglets en piégeant le message TCM_ADJUSTRECT
            if (m.Msg == 0x1328 && !DesignMode)
                m.Result = (IntPtr)1;
            else
                base.WndProc(ref m);
        }
    }
}
