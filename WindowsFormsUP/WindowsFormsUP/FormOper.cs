using DbConnectionHelper;
using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsUP
{
    public partial class FormOper : Form
    {
        private DataTable requestsTable;
        private int currentUserId;

        public FormOper(int userId)
        {
            InitializeComponent();
            this.currentUserId = userId;
            LoadRequests();
        }

        private void LoadRequests()
        {
            try
            {
                requestsTable = OperationManager.LoadRequests();
                dataGridView1.DataSource = requestsTable;
                SetupDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetupDataGridView()
        {
            dataGridView1.Columns.Clear();

            DataGridViewComboBoxColumn statusColumn = new DataGridViewComboBoxColumn
            {
                Name = "requestStatus",
                HeaderText = "Статус заявки",
                DataSource = OperationManager.GetStatusData(),
                DisplayMember = "StatusName",
                ValueMember = "StatusId",
                DataPropertyName = "requestStatus"
            };
            dataGridView1.Columns.Add(statusColumn);

            DataGridViewComboBoxColumn masterColumn = new DataGridViewComboBoxColumn
            {
                Name = "masterID",
                HeaderText = "Мастер",
                DataSource = OperationManager.GetMasterData(),
                DisplayMember = "fio",
                ValueMember = "userID",
                DataPropertyName = "masterID"
            };
            dataGridView1.Columns.Add(masterColumn);
        }

        private void buttonProfile_Click(object sender, EventArgs e)
        {
            FormProfile profileForm = new FormProfile(currentUserId);
            profileForm.ShowDialog();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            FormLog formLog = new FormLog();
            formLog.Show();
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                OperationManager.SaveChanges(requestsTable);
                MessageBox.Show("Изменения успешно сохранены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                LoadRequests();
            }
            else
            {
                try
                {
                    requestsTable = OperationManager.SearchRequests(searchText);
                    dataGridView1.DataSource = requestsTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
