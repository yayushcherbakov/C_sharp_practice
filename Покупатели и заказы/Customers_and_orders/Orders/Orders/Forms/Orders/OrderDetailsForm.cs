using Orders.Entities;
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

namespace Orders.Forms.Orders
{
    public partial class OrderDetailsForm : Form
    {
        /// <summary>
        /// Конструктор информационной формы.
        /// </summary>
        /// <param name="order"></param>
        public OrderDetailsForm(Order order)
        {
            InitializeComponent();
            var userManager = new UserManager();
            try
            {
                var user = userManager.GetUserById(order.UserId);
                this.labelUser.Text = $"{user.Firstname} {user.Middlename} {user.Lastname}";
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Can't get user information");
            }
            this.labelOrderNumber.Text = order.OrderNumber.ToString();
            this.labelCreationDate.Text = order.CreationDate.ToString();
            this.labelStatus.Text = order.Status.ToString();
            this.labelTotalPrice.Text = order.Items.Select(x => x.Price * x.Count).Sum().ToString();
            foreach (var orderItem in order.Items)
            {
                var item = new ListViewItem(new[] { orderItem.Name, orderItem.Article, orderItem.Price.ToString(), orderItem.Count.ToString() });
                item.Tag = orderItem;
                this.cartListView.Items.Add(item);
            }
        }
    }
}
