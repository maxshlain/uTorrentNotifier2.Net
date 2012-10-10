using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace uTorrentNotifier2.Net
{
    public partial class EnterPin : Form
    {
        public EnterPin()
        {
            InitializeComponent();
        }

        private void btn_done_Click(object sender, EventArgs e)
        {
            //Properties.Settings.Default.pin = textBox1.Text;
            //Properties.Settings.Default.Save();
            Close();
        }
    }
}
