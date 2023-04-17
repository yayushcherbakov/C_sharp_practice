using Orders.Entities;
using Orders.Enums;
using Orders.Forms.Orders;
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

namespace Orders.Forms.Seller
{
    public partial class ClientDetailsForm : Form
    {
        private User user;
        private List<Order> orders = new List<Order>();
        public ClientDetailsForm(User user)
        {
            this.user = user;
            InitializeComponent();

            var orderManager = new OrderManager();
            orders = new List<Order>();
            try
            {
                orders = orderManager.GetOrdersByBuyer(user.Id);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Can't get user information");
            }
            this.labelUser.Text = $"{user.Firstname} {user.Middlename} {user.Lastname}";
            this.labelEmail.Text = user.Email;
            this.labelPhone.Text = user.PhoneNumber;
            this.labelAddress.Text = user.Address;
            this.labelPaidAmount.Text = 0.0m.ToString();

            RefreshOrderView();
        }

        private void RefreshOrderView()
        {
            this.orderListView.Items.Clear();
            this.orderListView.Groups.Clear();
            this.orderListView.Refresh();

            var orderManager = new OrderManager();
            orders = new List<Order>();
            try
            {
                orders = orderManager.GetOrdersByBuyer(user.Id);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Can't get orders");
                return;
            }

            var activeGroup = new ListViewGroup("Active orders");
            this.orderListView.Groups.Add(activeGroup);
            foreach (var order in orders.Where(x => !x.Status.HasFlag(OrderStatus.Completed)))
            {
                var item = new ListViewItem(
                    new[] {
                        order.OrderNumber.ToString(),
                        order.CreationDate.ToString(),
                        order.Status.ToString(),
                        order.Items.Select(x=>x.Price * x.Count).Sum().ToString(),
                        string.Join(", ", order.Items.Select(x=>x.Name))
                    }, activeGroup);
                item.Tag = order;
                this.orderListView.Items.Add(item);
            }

            var completedGroup = new ListViewGroup("Completed orders");
            this.orderListView.Groups.Add(completedGroup);
            foreach (var order in orders.Where(x => x.Status.HasFlag(OrderStatus.Completed)))
            {
                var item = new ListViewItem(
                    new[] {
                        order.OrderNumber.ToString(),
                        order.CreationDate.ToString(),
                        order.Status.ToString(),
                        order.Items.Select(x=>x.Price * x.Count).Sum().ToString(),
                        string.Join(", ", order.Items.Select(x=>x.Name))
                    }, completedGroup);
                item.Tag = order;
                this.orderListView.Items.Add(item);
            }

            this.labelPaidAmount.Text = orders.Where(x => x.Status.HasFlag(OrderStatus.Paid))
                .Select(o => o.Items.Select(i => i.Price * i.Count).Sum()).Sum().ToString();
        }

        private void OrderListViewMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.orderListView.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    var order = (Order)this.orderListView.FocusedItem.Tag;
                    var contextMenuStrip = new ContextMenuStrip();

                    var viewDetails = new ToolStripMenuItem("View details");
                    viewDetails.Tag = order;
                    viewDetails.Click += OnClickViewOrderDetailsMenuStripItem;

                    var process = new ToolStripMenuItem("Process order");
                    process.Tag = order;
                    process.Click += OnClickProcessOrderMenuStripItem;
                    process.Enabled = !order.Status.HasFlag(OrderStatus.Processed);

                    var ship = new ToolStripMenuItem("Ship order");
                    ship.Tag = order;
                    ship.Click += OnClickShipOrderMenuStripItem;
                    ship.Enabled = order.Status.HasFlag(OrderStatus.Processed) && !order.Status.HasFlag(OrderStatus.Shipped);

                    var complete = new ToolStripMenuItem("Complete order");
                    complete.Tag = order;
                    complete.Click += OnClickCompleteOrderMenuStripItem;
                    complete.Enabled = order.Status.HasFlag(OrderStatus.Shipped)
                        && order.Status.HasFlag(OrderStatus.Paid)
                        && !order.Status.HasFlag(OrderStatus.Completed);

                    contextMenuStrip.Items.AddRange(
                        new[] { viewDetails, process, ship, complete });
                    contextMenuStrip.Show(Cursor.Position);
                }
            }
        }

        private void OnClickViewOrderDetailsMenuStripItem(object sender, EventArgs e)
        {
            var toolStrip = (ToolStripMenuItem)sender;
            using (var form = new OrderDetailsForm((Order)toolStrip.Tag))
            {
                var result = form.ShowDialog();
            }
        }

        private void OnClickProcessOrderMenuStripItem(object sender, EventArgs e)
        {
            var orderManager = new OrderManager();
            var toolStrip = (ToolStripMenuItem)sender;

            try
            {
                orderManager.ChangeStatusOrder((Order)toolStrip.Tag, OrderStatus.Processed);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Can't process by order");
                return;
            }

            MessageBox.Show("Order are processed");
            RefreshOrderView();
        }

        private void OnClickShipOrderMenuStripItem(object sender, EventArgs e)
        {
            var orderManager = new OrderManager();
            var toolStrip = (ToolStripMenuItem)sender;

            try
            {
                orderManager.ChangeStatusOrder((Order)toolStrip.Tag, OrderStatus.Shipped);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Can't ship by order");
                return;
            }

            MessageBox.Show("Order are shiped");
            RefreshOrderView();
        }

        private void OnClickCompleteOrderMenuStripItem(object sender, EventArgs e)
        {
            var orderManager = new OrderManager();
            var toolStrip = (ToolStripMenuItem)sender;

            try
            {
                orderManager.ChangeStatusOrder((Order)toolStrip.Tag, OrderStatus.Completed);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Can't complete by order");
                return;
            }

            MessageBox.Show("Order are completed");
            RefreshOrderView();
        }

        private void RefreshOrderViewButtonClick(object sender, EventArgs e)
        {
            RefreshOrderView();
        }
    }
}
