using DbConnectionHelper;
using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsUP
{
    public partial class FormProfile : Form
    {
        private int currentUserId;

        public FormProfile(int userID)
        {
            InitializeComponent();
            this.currentUserId = userID;
            LoadUserProfile();
        }

        private void LoadUserProfile()
        {
            try
            {
                DataTable userProfile = ProfileManager.GetUserProfile(currentUserId);

                if (userProfile.Rows.Count > 0)
                {
                    DataRow row = userProfile.Rows[0];
                    string fio = row["fio"].ToString();
                    string phone = row["phone"].ToString();
                    string role = row["type"].ToString();

                    labelFio.Text = "ФИО: " + fio;
                    labelPhone.Text = "Номер телефона: " + phone;
                    labelRole.Text = "Роль: " + role;
                }
                else
                {
                    MessageBox.Show("Пользователь не найден.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
