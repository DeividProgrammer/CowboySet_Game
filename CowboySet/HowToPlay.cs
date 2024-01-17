using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CowboySet
{
    public partial class HowToPlay : Form
    {
        public HowToPlay()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.HowToPlay;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void HowToPlay_Load(object sender, EventArgs e)
        {

        }
    }
}
