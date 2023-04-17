using Orders.Entities;
using Orders.Enums;
using Orders.Forms.Account;
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
    public partial class SellerMainForm : Form
    {
        private User user { get; set; }
        public SellerMainForm(User user)
        {
            InitializeComponent();
            this.user = user;
            this.Text = $"{user.Firstname} {user.Middlename} {user.Lastname}(Seller)";
            RefreshProductView();
            RefreshOrderView();
            RefreshClientView();
        }

        private void SellerMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            WelcomeForm.GetInstance().Show();
        }

        private void AddProductButtonClick(object sender, EventArgs e)
        {
            using (var form = new AddProductForm())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    RefreshProductView();
                }
            }
        }

        private void RefreshProductView()
        {
            var productManager = new ProductManager();
            var products = new List<Product>();
            try
            {
                products = productManager.AllProducts;
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Can't get products");
                return;
            }

            this.productListView.Items.Clear();
            this.productListView.Groups.Clear();
            this.productListView.Refresh();

            var activeGroup = new ListViewGroup("Active products");
            this.productListView.Groups.Add(activeGroup);
            foreach (var product in products.Where(x=>!x.IsDeleted))
            {
                var item = new ListViewItem(new[] { product.Name, product.Article, product.Price.ToString() }, activeGroup);
                item.Tag = product;
                this.productListView.Items.Add(item);
            }

            var deletedGroup = new ListViewGroup("Deleted products");
            this.productListView.Groups.Add(deletedGroup);
            foreach (var product in products.Where(x => x.IsDeleted))
            {
                var item = new ListViewItem(new[] { product.Name, product.Article, product.Price.ToString() }, deletedGroup);
                item.Tag = product;
                this.productListView.Items.Add(item);
            }
        }

        private void ProductListViewMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.productListView.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    var product = (Product)this.productListView.FocusedItem.Tag;
                    var contextMenuStrip = new ContextMenuStrip();
                    var menu = new List<ToolStripMenuItem>();

                    if (product.IsDeleted)
                    {
                        var restoreProductToCart = new ToolStripMenuItem("Restore product");
                        restoreProductToCart.Tag = product;
                        restoreProductToCart.Click += OnClickRestoreProductMenuStripItem;
                        menu.Add(restoreProductToCart);
                    }
                    else
                    {
                        var editProductToCart = new ToolStripMenuItem("Edit product");
                        editProductToCart.Tag = product;
                        editProductToCart.Click += OnClickEditProductMenuStripItem;
                        menu.Add(editProductToCart);

                        var deleteProductToCart = new ToolStripMenuItem("Delete product");
                        deleteProductToCart.Tag = product;
                        deleteProductToCart.Click += OnClickDeleteProductMenuStripItem;
                        menu.Add(deleteProductToCart);
                    }

                    contextMenuStrip.Items.AddRange(menu.ToArray());
                    contextMenuStrip.Show(Cursor.Position);
                }
            }
        }

        private void OnClickDeleteProductMenuStripItem(object sender, EventArgs e)
        {
            var toolStrip = (ToolStripMenuItem)sender;
            var productManager = new ProductManager();
            try
            {
                productManager.DeleteProduct((Product)toolStrip.Tag);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Can't delete product");
                return;
            }

            RefreshProductView();
        }

        private void OnClickRestoreProductMenuStripItem(object sender, EventArgs e)
        {
            var toolStrip = (ToolStripMenuItem)sender;
            var productManager = new ProductManager();
            try
            {
                productManager.RestoreProduct((Product)toolStrip.Tag);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Can't restore product");
                return;
            }

            RefreshProductView();
        }

        private void OnClickEditProductMenuStripItem(object sender, EventArgs e)
        {
            var toolStrip = (ToolStripMenuItem)sender;
            using (var form = new EditProductForm((Product)toolStrip.Tag))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    RefreshProductView();
                }
            }
        }

        private void RefreshProductsButtonClick(object sender, EventArgs e)
        {
            RefreshProductView();
        }

        private void RefreshOrderView()
        {
            this.orderListView.Items.Clear();
            this.orderListView.Groups.Clear();
            this.orderListView.Refresh();

            var orderManager = new OrderManager();
            var orders = new List<Order>();
            try
            {
                orders = orderManager.Orders;
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

        private void RefreshClientView()
        {
            this.clientListView.Items.Clear();
            this.clientListView.Refresh();

            var userManager = new UserManager();
            var users = new List<User>();
            try
            {
                users = userManager.GetAllBuyers();
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Can't get clients");
                return;
            }

            foreach (var user in users)
            {
                var item = new ListViewItem(
                    new[] {
                        user.Firstname,
                        user.Middlename,
                        user.Lastname,
                        user.Email,
                        user.PhoneNumber
                    });
                item.Tag = user;
                this.clientListView.Items.Add(item);
            }
        }

        private void RefreshClientViewButtonClick(object sender, EventArgs e)
        {
            RefreshClientView();
        }

        private void ClientListViewMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.clientListView.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    var user = (User)this.clientListView.FocusedItem.Tag;
                    var contextMenuStrip = new ContextMenuStrip();

                    var viewDetails = new ToolStripMenuItem("View details");
                    viewDetails.Tag = user;
                    viewDetails.Click += OnClickViewClientDetailsMenuStripItem;

                    contextMenuStrip.Items.AddRange(
                        new[] { viewDetails });
                    contextMenuStrip.Show(Cursor.Position);
                }
            }
        }

        private void OnClickViewClientDetailsMenuStripItem(object sender, EventArgs e)
        {
            var toolStrip = (ToolStripMenuItem)sender;
            using (var form = new ClientDetailsForm((User)toolStrip.Tag))
            {
                var result = form.ShowDialog();
            }
        }
    }
}
