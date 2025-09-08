using System;
using System.Windows.Forms;

namespace CitizenReport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Wired by Designer: this.Load += Form1_Load;
        private void Form1_Load(object sender, EventArgs e)
        {
            // (Already disabled in Designer, but harmless to keep)
            button2.Enabled = false; // Local Events
            button3.Enabled = false; // Service Status

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // Wired by Designer: button1.Click += button1_Click;
        private void button1_Click(object sender, EventArgs e)
        {
            // Report Issues
            using (var f = new ReportIssueForm())
            {
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog(this);
            }
        }

        // Wired by Designer: button2.Click += button2_Click;
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Local Events & Announcements: coming soon.", "Not implemented");
        }

        // Wired by Designer: button3.Click += button3_Click;
        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Service Request Status: coming soon.", "Not implemented");
        }
    }
}
