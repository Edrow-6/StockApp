using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockApp_WinForms
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        void MoveIndicator(Control control)
        {
            indicator.Top = control.Top;
            indicator.Height = control.Height;
        }

        void HeaderTitle(Control control)
        {
            pageName.Text = control.Text;
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            MoveIndicator((Control) sender);
            HeaderTitle((Control) sender);
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            MoveIndicator((Control) sender);
            HeaderTitle((Control) sender);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            MoveIndicator((Control) sender);
            HeaderTitle((Control) sender);
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            MoveIndicator((Control) sender);
            HeaderTitle((Control) sender);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            MoveIndicator((Control) sender);
            HeaderTitle((Control) sender);
        }
    }
}
