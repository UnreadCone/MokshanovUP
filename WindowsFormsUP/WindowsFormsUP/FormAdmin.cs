using DbConnectionHelper;
using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsUP
{
    public partial class FormAdmin : Form
    {
        private DataTable loginHistoryTable;

        public FormAdmin()
        {
            InitializeComponent();
            LoadLoginHistory();
        }

        private void LoadLoginHistory()
        {
            try
            {
                loginHistoryTable = LoginHistoryManager.GetLoginHistory();
                dataGridView1.DataSource = loginHistoryTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            FormLog formLog = new FormLog();
            formLog.Show();
            this.Close();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text.Trim();
            try
            {
                DataView filteredView = LoginHistoryManager.FilterLoginHistory(loginHistoryTable, searchText);
                dataGridView1.DataSource = filteredView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
