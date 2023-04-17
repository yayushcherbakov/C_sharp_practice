using Orders.Enums;
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
    public partial class RegisterForm : Form
    {
        public RegisterModel Result { get; set; } = new RegisterModel();

        /// <summary>
        /// Конструктор формы.
        /// </summary>
        public RegisterForm()
        {
            InitializeComponent();
            Result.Role = UserRole.Buyer;
            this.radioBuyer.Checked = true;
            this.radioSeller.Checked = false;
        }


        private void RadioByerCheckedChanged(object sender, EventArgs e)
        {
            Result.Role = this.radioBuyer.Checked ? UserRole.Buyer : UserRole.Saller;
            if (this.radioBuyer.Checked) this.radioSeller.Checked = false;

        }


        private void RadioSellerCheckedChanged(object sender, EventArgs e)
        {
            Result.Role = this.radioSeller.Checked ? UserRole.Saller : UserRole.Saller;
            if (this.radioSeller.Checked) this.radioBuyer.Checked = false;
        }

        /// <summary>
        /// Нажатие на кнопку входа.
        /// </summary>
        private void SignUpClick(object sender, EventArgs e)
        {
            if (!ValidateFields())
                return;
            try
            {
                Result.Email = this.textBoxEmail.Text;
                Result.Firstname = this.textBoxFirstname.Text;
                Result.Middlename = this.textBoxMiddlename.Text;
                Result.Lastname = this.textBoxLastname.Text;
                Result.PhoneNumber = this.textBoxPhoneNumber.Text;
                Result.Address = this.textBoxAddress.Text;
                Result.Password = this.textBoxPassword.Text;
                var userManager = new UserManager();
                userManager.RegisterUser(Result);
                this.DialogResult = DialogResult.OK;
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Can't register user");
            }

        }

        /// <summary>
        /// Проверка данных на валидность.
        /// </summary>
        /// <returns>Результат проверки.</returns>
        private bool ValidateFields()
        {
            if (!ValidateField((string)this.textBoxEmail.Tag, this.textBoxEmail.Text))
                return false;
            if (!ValidateField((string)this.textBoxFirstname.Tag, this.textBoxFirstname.Text))
                return false;
            if (!ValidateField((string)this.textBoxMiddlename.Tag, this.textBoxMiddlename.Text))
                return false;
            if (!ValidateField((string)this.textBoxPhoneNumber.Tag, this.textBoxPhoneNumber.Text))
                return false;
            if (!ValidateField((string)this.textBoxAddress.Tag, this.textBoxAddress.Text))
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
        /// Проверка данных на валидность.
        /// </summary>
        /// <param name="fildName">Название поля.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Результат проверки.</returns>
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
