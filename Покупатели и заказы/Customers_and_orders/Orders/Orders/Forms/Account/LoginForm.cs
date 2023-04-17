using Orders.Entities;
using Orders.Managers;
using Orders.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Orders.Forms.Account
{
    public partial class LoginForm : Form
    {
        public User Result { get; set; }

        /// <summary>
        /// Конструктор формы.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Нажатие на кнопку для входа.
        /// </summary>
        private void Button1Click(object sender, EventArgs e)
        {
            ValidateFields();
            try
            {
                var loginModel = new LoginModel();
                loginModel.Login = this.textBoxLogin.Text;
                loginModel.Password = this.textBoxPassword.Text;
                var userManager = new UserManager();
                Result = userManager.LoginUser(loginModel);
                this.DialogResult = DialogResult.OK;
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Can't login user");
            }
        }

        /// <summary>
        /// Проверка на валидность данных.
        /// </summary>
        /// <returns>Результат проверки.</returns>
        private bool ValidateFields()
        {
            if (!ValidateField((string)this.textBoxLogin.Tag, this.textBoxLogin.Text))
                return false;
            if (!ValidateField((string)this.textBoxPassword.Tag, this.textBoxPassword.Text))
                return false;
            if (this.textBoxPassword.Text.Length < 6)
            {
                MessageBox.Show($"{this.textBoxPassword.Tag} can not be less 6 symbols");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверка на валидность данных.
        /// </summary>
        /// <param name="fildName">Имя поля.</param>
        /// <param name="value">Значение поля.</param>
        /// <returnsРезультат проверки.></returns>
        private bool ValidateField(string fildName, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                MessageBox.Show($"{fildName} can not be empty or contain only whitespases");
                return false;
            }
            return true;
        }
    }
}
