﻿using System;
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
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.aboutM;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {

        }
    }
}
