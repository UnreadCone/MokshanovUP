using DbConnectionHelper;
using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsUP
{
    public partial class FormMaster : Form
    {
        private int currentUserId;

        public FormMaster(int userId)
        {
            InitializeComponent();
            this.currentUserId = userId;
            LoadMasterRequests();
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
                MasterRequestManager.SaveChanges(dataGridView1);
                MessageBox.Show("Изменения успешно сохранены.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadMasterRequests()
        {
            try
            {
                DataTable dataTable = MasterRequestManager.LoadMasterRequests(currentUserId);
                dataGridView1.DataSource = dataTable;

                dataGridView1.Columns["requestID"].HeaderText = "ID заявки";
                dataGridView1.Columns["startDate"].HeaderText = "Дата начала";
                dataGridView1.Columns["problemDescription"].HeaderText = "Описание проблемы";
                dataGridView1.Columns["requestStatus"].HeaderText = "Статус заявки";
                dataGridView1.Columns["repairParts"].HeaderText = "Запчасти";
                dataGridView1.Columns["model"].HeaderText = "Модель";
                dataGridView1.Columns["techTypeID"].HeaderText = "ID типа техники";
                dataGridView1.Columns["techTypeName"].HeaderText = "Тип техники";
                dataGridView1.Columns["message"].HeaderText = "Комментарий";

                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ConfigureDataGridView()
        {
            dataGridView1.Columns["requestStatus"].ReadOnly = false;
            dataGridView1.Columns["repairParts"].ReadOnly = false;
            dataGridView1.Columns["message"].ReadOnly = false;

            dataGridView1.Columns["requestID"].ReadOnly = true;
            dataGridView1.Columns["startDate"].ReadOnly = true;
            dataGridView1.Columns["problemDescription"].ReadOnly = true;
            dataGridView1.Columns["model"].ReadOnly = true;
            dataGridView1.Columns["techTypeID"].ReadOnly = true;
            dataGridView1.Columns["techTypeName"].ReadOnly = true;
        }
    }
}
