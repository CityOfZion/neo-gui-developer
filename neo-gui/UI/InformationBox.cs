﻿using System.Windows.Forms;

namespace Neo.UI
{
    internal partial class InformationBox : Form
    {
        public InformationBox()
        {
            InitializeComponent();
        }

        public static DialogResult Show(string text, string message = null, string title = null)
        {
            using (InformationBox box = new InformationBox())
            {
                box.textBox1.Text = text;
                if (message != null)
                {
                    box.label1.Text = message;
                }
                if (title != null)
                {
                    box.Text = title;
                }
                return box.ShowDialog();
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            textBox1.SelectAll();
            textBox1.Copy();
        }
    }
}
