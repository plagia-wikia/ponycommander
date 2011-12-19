using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Threading;

namespace PonyCommander
{
    public partial class PonySplash : Form
    {
        public PonySplash()
        {
            InitializeComponent();          
        }

        private void PonySplash_Load(object sender, EventArgs e)
        {
            SoundPlayer sndplayr = new SoundPlayer(PonyCommander.Properties.Resources.imagination);

            sndplayr.Play();
        }

    }
}
