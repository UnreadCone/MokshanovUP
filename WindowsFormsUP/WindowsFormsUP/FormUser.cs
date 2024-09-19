using DbConnectionHelper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsUP
{
    public partial class FormUser : Form
    {
        private int currentUserId;

        public FormUser(int userId)
        {
            InitializeComponent();
            this.currentUserId = userId;
        }

        private void FormUser_Load(object sender, EventArgs e)
        {
            FillTechTypeComboBox();
        }

        private void FillTechTypeComboBox()
        {
            using (SqlConnection connection = new SqlConnection(Class1.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT techTypeName FROM TechTypes";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        comboBoxType.Items.Clear();

                        while (reader.Read())
                        {
                            string techTypeName = reader["techTypeName"].ToString();
                            comboBoxType.Items.Add(techTypeName);
                        }

                        comboBoxType.Refresh();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке типов техники: " + ex.Message);
                }
            }
        }

        private void buttonProfile_Click(object sender, EventArgs e)
        {
            FormProfile profileForm = new FormProfile(currentUserId);
            profileForm.ShowDialog();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            buttonСreate.BackColor = Color.OrangeRed;
            buttonAll.BackColor = Color.Orange;
        }

        private void buttonAll_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            buttonСreate.BackColor = Color.Orange;
            buttonAll.BackColor = Color.OrangeRed;
            LoadClientRequests();
        }

        private void LoadClientRequests()
        {
            try
            {
                DataTable dataTable = RequestManager.GetClientRequests(currentUserId);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string techType = comboBoxType.Text;
            string model = textBoxModel.Text;
            string problemDescription = textBoxProblem.Text;

            if (string.IsNullOrEmpty(techType) || string.IsNullOrEmpty(model) || string.IsNullOrEmpty(problemDescription))
            {
                MessageBox.Show("Пожалуйста, заполните все поля!");
                return;
            }

            DialogResult result = MessageBox.Show("Отправить заявку?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }

            try
            {
                int techTypeId = RequestManager.GetTechTypeId(techType);
                int requestId = RequestManager.GetNextRequestID();

                RequestManager.CreateRequest(requestId, DateTime.Now, problemDescription, 1, null, null, null, currentUserId, model, techTypeId);

                MessageBox.Show("Заявка успешно создана!");
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
    }
}
