using HobbyManiaManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HobbyManiaManager.Forms
{
    public partial class ImbdForm : Form
    {
        public string movie_id;
        public string movie_name;

        public ImbdForm(string movie_id, string movie_name)
        {
            InitializeComponent();
            this.movie_id = movie_id;
            this.movie_name = movie_name;


        }

        private void ImbdForm_Load(object sender, EventArgs e)
        {
            this.Text = movie_name;
            InitBrowser();
        }
        private async Task initizated()
        {
            await webView21.EnsureCoreWebView2Async(null);
        }
        public async void InitBrowser()
        {
            string url = ("https://www.imdb.com/es-es/title/" + movie_id);

            await initizated();
            webView21.CoreWebView2.Navigate(url);
        }
    }
}
