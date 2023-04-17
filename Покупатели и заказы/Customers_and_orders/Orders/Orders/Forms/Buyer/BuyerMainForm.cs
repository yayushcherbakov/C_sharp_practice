using Carts.Managers;
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

namespace Orders.Forms.Buyer
{
    public partial class BuyerMainForm : Form
    {
        private User user { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public BuyerMainForm(User user)
        {
            InitializeComponent();
            this.user = user;
            this.Text = $"{user.Firstname} {user.Middlename} {user.Lastname}(Buyer)";
            RefreshProductView();
            RefreshCartView();
            RefreshOrderView();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuyerMainFormClosing(object sender, FormClosingEventArgs e)
        {
            WelcomeForm.GetInstance().Show();
        }

        /// <summary>
        /// 
        /// </summary>
        private void RefreshProductView()
        {
            var productManager = new ProductManager();
            var products = new List<Product>();
            try
            {
                products = productManager.ActiveProducts;
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
            this.productListView.Refresh();
            foreach (var product in products)
            {
                var item = new ListViewItem(new[] { product.Name, product.Article, product.Price.ToString() });
                item.Tag = product;
                this.productListView.Items.Add(item);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductListViewMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.productListView.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    var product = (Product)this.productListView.FocusedItem.Tag;
                    var contextMenuStrip = new ContextMenuStrip();

                    var addProductToCart = new ToolStripMenuItem("Add product to cart");
                    addProductToCart.Tag = product;
                    addProductToCart.Click += OnClickAddProductMenuStripItem;

                    contextMenuStrip.Items.AddRange(
                        new[] { addProductToCart });
                    contextMenuStrip.Show(Cursor.Position);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClickAddProductMenuStripItem(object sender, EventArgs e)
        {
            var toolStrip = (ToolStripMenuItem)sender;
            using (var form = new AddProductToCartForm((Product)toolStrip.Tag, user.Id))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    RefreshCartView();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshProductsButtonClick(object sender, EventArgs e)
        {
            RefreshProductView();
        }

        /// <summary>
        /// 
        /// </summary>
        private void RefreshCartView()
        {
            var cartManager = new CartManager();
            var cart = new Cart();
            try
            {
                cart = cartManager.GetUserCart(user.Id);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Can't get cart");
                return;
            }
            this.cartListView.Items.Clear();
            this.cartListView.Refresh();
            foreach (var cartItem in cart.Items)
            {
                var item = new ListViewItem(new[] { cartItem.Name, cartItem.Article, cartItem.Price.ToString(), cartItem.Count.ToString() });
                item.Tag = cartItem;
                this.cartListView.Items.Add(item);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshCartViewClick(object sender, EventArgs e)
        {
            RefreshCartView();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CartListViewMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.cartListView.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    var cartItem = (OrderItem)this.cartListView.FocusedItem.Tag;
                    var contextMenuStrip = new ContextMenuStrip();

                    var editCartItemToCart = new ToolStripMenuItem("Edit item");
                    editCartItemToCart.Tag = cartItem;
                    editCartItemToCart.Click += OnClickEditCartItemMenuStripItem;

                    var deleteCartItemToCart = new ToolStripMenuItem("Delete item");
                    deleteCartItemToCart.Tag = cartItem;
                    deleteCartItemToCart.Click += OnClickDeleteCartItemMenuStripItem;

                    contextMenuStrip.Items.AddRange(
                        new[] { editCartItemToCart, deleteCartItemToCart });
                    contextMenuStrip.Show(Cursor.Position);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClickEditCartItemMenuStripItem(object sender, EventArgs e)
        {
            var toolStrip = (ToolStripMenuItem)sender;
            using (var form = new AddProductToCartForm((Product)toolStrip.Tag, user.Id))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    RefreshCartView();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClickDeleteCartItemMenuStripItem(object sender, EventArgs e)
        {
            var cartManager = new CartManager();
            var toolStrip = (ToolStripMenuItem)sender;
            var cartItem = (Product)toolStrip.Tag;
            try
            {
                var cart = cartManager.GetUserCart(user.Id);
                cart.Items = cart.Items.Where(x => x.Article != cartItem.Article).ToList();
                cartManager.SaveCart(cart);
                RefreshCartView();
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Can't delete cart item");
                return;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateOrderButtonClick(object sender, EventArgs e)
        {
            var orderManager = new OrderManager();
            var cartManager = new CartManager();

            try
            {
                var cart = cartManager.GetUserCart(user.Id);
                orderManager.CreateOrder(cart);
                cartManager.DeleteCart(cart);
                RefreshCartView();
                RefreshOrderView();
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Can't create order");
                return;
            }

            this.tabControl.SelectedTab = this.tabPage3;
            MessageBox.Show("Order is created");
        }

        /// <summary>
        /// 
        /// </summary>
        private void RefreshOrderView()
        {
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
            this.orderListView.Items.Clear();
            this.orderListView.Refresh();
            foreach (var order in orders)
            {
                var item = new ListViewItem(
                    new[] {
                        order.OrderNumber.ToString(),
                        order.CreationDate.ToString(),
                        order.Status.ToString(),
                        order.Items.Select(x=>x.Price * x.Count).Sum().ToString(),
                        string.Join(", ", order.Items.Select(x=>x.Name))
                    });
                item.Tag = order;
                this.orderListView.Items.Add(item);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                    var pay = new ToolStripMenuItem("Pay");
                    pay.Tag = order;
                    pay.Click += OnClickPayOrderMenuStripItem;
                    pay.Enabled = order.Status.HasFlag(OrderStatus.Processed) && !order.Status.HasFlag(OrderStatus.Paid);
                    contextMenuStrip.Items.AddRange(
                        new[] { viewDetails, pay });
                    contextMenuStrip.Show(Cursor.Position);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClickViewOrderDetailsMenuStripItem(object sender, EventArgs e)
        {
            var toolStrip = (ToolStripMenuItem)sender;
            using (var form = new OrderDetailsForm((Order)toolStrip.Tag))
            {
                var result = form.ShowDialog();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClickPayOrderMenuStripItem(object sender, EventArgs e)
        {
            var orderManager = new OrderManager();
            var toolStrip = (ToolStripMenuItem)sender;

            try
            {
                orderManager.ChangeStatusOrder((Order)toolStrip.Tag, OrderStatus.Paid);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Can't pay by order");
                return;
            }

            MessageBox.Show("Order are paid");
            RefreshOrderView();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshOrderViewButtonClick(object sender, EventArgs e)
        {
            RefreshOrderView();
        }
    }
}
