using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RESTFULLAPI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void txtURL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(txtURL.Text);

                if (response.IsSuccessStatusCode) 
                    rtxtLog.Text = await response.Content.ReadAsStringAsync();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtURL.Clear();
            rtxtLog.Text = null;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
