using Orders.Enums;
using Orders.Forms.Buyer;
using Orders.Forms.Seller;
using Orders.Managers;
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
    public partial class WelcomeForm : Form
    {
        private static WelcomeForm instance;

        /// <summary>
        /// Конструктор формы приветствия.
        /// </summary>
        private WelcomeForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Получение формы.
        /// </summary>
        /// <returns>Форма приветствия.</returns>
        public static WelcomeForm GetInstance()
        {
            if (instance is null)
                instance = new WelcomeForm();
            return instance;
        }

        /// <summary>
        /// Попытка залогиниться.
        /// </summary>
        private void LoginClick(object sender, EventArgs e)
        {
            using (var form = new LoginForm())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Form mainform = null;
                    var user = form.Result;
                    if (user.Role == UserRole.Buyer)
                    {
                        mainform = new BuyerMainForm(user);
                    }
                    else
                    {
                        mainform = new SellerMainForm(user);
                    }
                    this.Hide();
                    mainform.ShowDialog();
                }
            }
        }


        /// <summary>
        /// Попытка зарегистрироваться.
        /// </summary>
        private void RegisterClick(object sender, EventArgs e)
        {
            using (var form = new RegisterForm())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    MessageBox.Show("Success!");
                }
            }
        }
    }
}
