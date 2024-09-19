using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DbConnectionHelper;


namespace WindowsFormsUP
{
    public partial class FormLog : Form
    {
        private string captchaText;
        private bool isPasswordVisible = false;
        private bool captchaVisible = false;
        private int failedAttempts = -1;
        private int captchaAttempts = -1;
        private bool isLockedOut = false;
        private Timer lockoutTimer;
        private DateTime lockoutEndTime;
        private bool captchaVerified = false;

        private int currentUserId;

        public FormLog()
        {
            InitializeComponent();
            textBoxPass.UseSystemPasswordChar = true;
            buttonChange.Click += buttonChange_Click;
            buttonEnter.Click += buttonEnter_Click;

            lockoutTimer = new Timer();
            lockoutTimer.Interval = 1000;
            lockoutTimer.Tick += LockoutTimer_Tick;
        }

        private void GenerateCaptcha()
        {
            captchaText = GenerateRandomText(4);

            Bitmap bitmap = new Bitmap(200, 50);
            Graphics g = Graphics.FromImage(bitmap);
            Font font = new Font("Arial", 24, FontStyle.Bold);
            Random rand = new Random();

            g.Clear(Color.White);

            for (int i = 0; i < captchaText.Length; i++)
            {
                g.DrawString(captchaText[i].ToString(), font, Brushes.Black, new PointF(i * 40 + rand.Next(-10, 10), rand.Next(-10, 10)));
            }

            for (int i = 0; i < 5; i++)
            {
                g.DrawLine(new Pen(Color.Gray, 2),
                    new Point(rand.Next(200), rand.Next(50)),
                    new Point(rand.Next(200), rand.Next(50)));
                g.DrawEllipse(new Pen(Color.Gray, 1), rand.Next(200), rand.Next(50), 5, 5);
            }

            pictureBox.Image = bitmap;
        }

        private string GenerateRandomText(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            char[] result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }
            return new string(result);
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (isLockedOut)
            {
                ShowLockoutMessage();
                return;
            }

            if (captchaVisible && !captchaVerified)
            {
                HandleCaptchaVerification();
                return;
            }

            HandleLogin();
        }

        private void HandleCaptchaVerification()
        {
            if (textBoxCap.Text != captchaText)
            {
                captchaAttempts++;
                if (captchaAttempts >= 3)
                {
                    InitiateLockout();
                }
                else
                {
                    labelMistake.Text = "Неверная капча!";
                    labelMistake.ForeColor = Color.Red;
                    GenerateCaptcha();
                    buttonChange.Visible = true;
                }
            }
            else
            {
                captchaVerified = true;
                if (!ValidateUser(textBoxLogin.Text, textBoxPass.Text))
                {
                    HandleLoginFailure();
                }
                else
                {
                    ProceedToUserForm();
                }
            }
        }

        private void HandleLogin()
        {
            if (ValidateUser(textBoxLogin.Text, textBoxPass.Text))
            {
                ProceedToUserForm();
            }
            else
            {
                HandleLoginFailure();
            }
        }

        private void HandleLoginFailure()
        {
            if (!captchaVisible)
            {
                GenerateCaptcha();
                captchaVisible = true;
                pictureBox.Visible = true;
                textBoxCap.Visible = true;
                buttonChange.Visible = true;
                labelMistake.Text = "Неверный логин или пароль!";
                labelMistake.ForeColor = Color.Red;

                SaveLoginHistory(textBoxLogin.Text, false);

                failedAttempts++;
            }
            else
            {
                InitiateLockout();
            }
        }

        private void ProceedToUserForm()
        {
            SaveLoginHistory(textBoxLogin.Text, true);

            if (captchaVisible)
            {
                ResetCaptcha();
            }

            OpenUserForm(textBoxLogin.Text);
        }

        private void InitiateLockout()
        {
            isLockedOut = true;
            lockoutEndTime = DateTime.Now.AddMinutes(3);
            buttonEnter.Enabled = false;
            lockoutTimer.Start();
        }

        private void ResetCaptcha()
        {
            captchaVisible = false;
            captchaAttempts = 0;
            pictureBox.Visible = false;
            textBoxCap.Visible = false;
            buttonChange.Visible = false;
            captchaVerified = false;
        }

        private void ShowLockoutMessage()
        {
            if (isLockedOut)
            {
                MessageBox.Show("Вход заблокирован на 3 минуты из-за слишком большого количества попыток.");
            }
        }

        private void LockoutTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan remainingTime = lockoutEndTime - DateTime.Now;
            if (remainingTime.TotalSeconds <= 0)
            {
                isLockedOut = false;
                buttonEnter.Enabled = true;
                lockoutTimer.Stop();
                labelLockoutTime.Text = "";
            }
            else
            {
                labelLockoutTime.Text = $"{remainingTime.ToString(@"hh\:mm\:ss")}";
            }
        }

        private bool ValidateUser(string login, string password)
        {
            bool isValid = false;
            using (SqlConnection connection = new SqlConnection(Class1.ConnectionString))
            {
                connection.Open();
                string query = "SELECT userID FROM Users WHERE login = @login AND password = @password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);

                    var result = command.ExecuteScalar();

                    if (result != null)
                    {
                        currentUserId = Convert.ToInt32(result);
                        isValid = true;
                    }
                }
            }
            return isValid;
        }

        private void SaveLoginHistory(string login, bool isSuccess)
        {
            using (SqlConnection connection = new SqlConnection(Class1.ConnectionString))
            {
                connection.Open();
                string query = "INSERT INTO [dbo].[История] ([Логин пользователя], [Вход подтверждён], [Дата]) " +
                               "VALUES (@login, @isSuccess, @date)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@isSuccess", isSuccess ? 1 : 0);
                    command.Parameters.AddWithValue("@date", DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void OpenUserForm(string login)
        {
            using (SqlConnection connection = new SqlConnection(Class1.ConnectionString))
            {
                connection.Open();
                string query = "SELECT [type] FROM [dbo].[Users] WHERE [login] = @login";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    string userType = command.ExecuteScalar()?.ToString();

                    if (userType != null)
                    {
                        Form formToOpen = null;
                        switch (userType)
                        {
                            case "Мастер":
                                formToOpen = new FormMaster(currentUserId);
                                break;
                            case "Заказчик":
                                formToOpen = new FormUser(currentUserId);
                                break;
                            case "Администратор":
                                formToOpen = new FormAdmin();
                                break;
                            case "Оператор":
                                formToOpen = new FormOper(currentUserId);
                                break;
                            default:
                                formToOpen = new FormProfile(currentUserId);
                                break;
                        }

                        if (formToOpen != null)
                        {
                            formToOpen.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пользователь с таким логином не найден.");
                    }
                }
            }
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            GenerateCaptcha();
            textBoxCap.Text = "";
        }

        private void buttonEYE_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            textBoxPass.UseSystemPasswordChar = !isPasswordVisible;
            buttonEYE.Text = isPasswordVisible ? "🙉" : "🙈";
        }
    }
}
