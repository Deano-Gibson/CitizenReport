using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace CitizenReport
{
    public partial class ReportIssueForm : Form
    {
        private readonly List<string> _attachments = new List<string>();
        private readonly ErrorProvider _errors = new ErrorProvider();

        public ReportIssueForm()
        {
            InitializeComponent();

            // Window behavior & keyboard shortcuts
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.AcceptButton = button2; // Submit
            this.CancelButton = button1; // Back

            // Wire engagement updates
            locationInput.TextChanged += AnyFieldChanged;
            comboBox1.SelectedIndexChanged += AnyFieldChanged;
            richTextBox1.TextChanged += AnyFieldChanged;

            // Set up category correctly
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = -1; // force a deliberate selection

            // Clear placeholder text; use labels instead
            locationInput.Clear();
            richTextBox1.Clear();

            UpdateEngagementUI();
        }

        private void button1_Click(object sender, EventArgs e) => this.Close(); // Back

        private void button4_Click(object sender, EventArgs e) // Add Attachments
        {
            using (var dlg = new OpenFileDialog
            {
                Title = "Attach image or document",
                Filter = "Images/Docs|*.png;*.jpg;*.jpeg;*.pdf;*.docx;*.doc;*.txt|All files|*.*",
                Multiselect = true
            })
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    foreach (var p in dlg.FileNames)
                    {
                        _attachments.Add(p);
                        lstAttachments.Items.Add(Path.GetFileName(p));
                    }
                    UpdateEngagementUI();
                }
            }
        }

        private void AnyFieldChanged(object sender, EventArgs e) => UpdateEngagementUI();

        private void UpdateEngagementUI()
        {
            int filled = 0;
            if (!string.IsNullOrWhiteSpace(locationInput.Text)) filled++;
            if (comboBox1.SelectedIndex >= 0) filled++;
            if (!string.IsNullOrWhiteSpace(richTextBox1.Text)) filled++;

            progress.Value = Math.Min(100, filled * 33); // 0,33,66,99
        }

        private void button2_Click(object sender, EventArgs e) // Submit
        {
            _errors.Clear();
            bool ok = true;

            if (string.IsNullOrWhiteSpace(locationInput.Text))
            { _errors.SetError(locationInput, "Enter a location."); ok = false; }

            if (comboBox1.SelectedIndex < 0)
            { _errors.SetError(comboBox1, "Choose a category."); ok = false; }

            // Optional confirmation checkbox:
            // if (!checkBox1.Checked) { _errors.SetError(checkBox1, "Please confirm details."); ok = false; }

            if (!ok) return;

            var issue = new Issue
            {
                Location = locationInput.Text.Trim(),
                Category = (IssueCategory)Enum.Parse(typeof(IssueCategory), comboBox1.SelectedItem.ToString()),
                Description = richTextBox1.Text.Trim(),
                Attachments = new List<string>(_attachments)
            };
            IssueStore.Issues.Add(issue);

            MessageBox.Show(
                $"Thanks! Your report was captured.\nReference: {issue.Id.ToString().Substring(0, 8)}",
                "Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reset for next report
            locationInput.Clear();
            comboBox1.SelectedIndex = -1;
            richTextBox1.Clear();
            _attachments.Clear();
            lstAttachments.Items.Clear();
            UpdateEngagementUI();
        }
    }

    public enum IssueCategory { Sanitation, Roads, Electricity, Water, Safety, Other }

    public class Issue
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Location { get; set; }
        public IssueCategory Category { get; set; }
        public string Description { get; set; }
        public List<string> Attachments { get; set; } = new List<string>();
        public DateTime CreatedAt { get; } = DateTime.Now;
    }

    public static class IssueStore
    {
        public static readonly List<Issue> Issues = new List<Issue>();
    }
}


