using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Warehouse2.Entities;

namespace Warehouse2
{
    public class ClassifierTreeNode : TreeNode
    {
        public Classifier CurrentClassifier;
        public Classifier ParentClassifier;
        private DataGridpUdater dataGridpUdater;
        public ClassifierTreeNode(Classifier classifier, DataGridpUdater dataGridpUdater) : base(classifier.Name)
        {
            CurrentClassifier = classifier;
            this.dataGridpUdater = dataGridpUdater;
            CreateDefaultContextMenu(this);
            CreateChildTreeNodes();
        }

        private void CreateChildTreeNodes()
        {
            foreach (var childClassifier in CurrentClassifier.InnerClassifiers)
            {
                AddChildClassifier(childClassifier);
            }
        }

        private void AddChildClassifier(Classifier childClassifier)
        {
            var node = new ClassifierTreeNode(childClassifier, this.dataGridpUdater);
            CreateDefaultContextMenu(node);

            var removeClassifier = new ToolStripMenuItem("Remove classifier");
            removeClassifier.Click += node.OnClickDeleteMenuStripItem;
            node.ContextMenuStrip.Items.Add(removeClassifier);

            node.ParentClassifier = this.CurrentClassifier;

            node.ContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(node.CmsOpening);

            this.Nodes.Add(node);
        }

        private void CreateDefaultContextMenu(ClassifierTreeNode node)
        {
            node.ContextMenuStrip = new ContextMenuStrip();

            var addProduct = new ToolStripMenuItem("Add product");
            addProduct.Click += node.OnClickAddProductMenuStripItem;

            var generateProducts = new ToolStripMenuItem("Generate products");
            generateProducts.Click += node.OnClickGenerateProductMenuStripItem;

            var addClassifier = new ToolStripMenuItem("Add classifier");
            addClassifier.Click += node.OnClickAddClassifierMenuStripItem;

            var editClassifier = new ToolStripMenuItem("Edit classifier");
            editClassifier.Click += node.OnClickEditClassifierMenuStripItem;

            var generateClassifiers = new ToolStripMenuItem("Generate classifiers");
            generateClassifiers.Click += node.OnClickGenerateClassifiersMenuStripItem;

            node.ContextMenuStrip.Items.AddRange(
                new[] { addProduct, generateProducts, addClassifier, editClassifier, generateClassifiers });
        }

        void CmsOpening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var menuItems = ((ContextMenuStrip)sender).Items;
            foreach (ToolStripMenuItem item in menuItems)
            {
                if (item.Text == "Remove classifier")
                {
                    item.Enabled = CurrentClassifier.InnerClassifiers.Count == 0 && CurrentClassifier.Products.Count == 0;
                }
            }
        }

        void OnClickDeleteMenuStripItem(object sender, EventArgs e)
        {
            ParentClassifier.InnerClassifiers.Remove(this.CurrentClassifier);
            this.Remove();
        }

        void OnClickAddClassifierMenuStripItem(object sender, EventArgs e)
        {
            var existingClassifiers = this.CurrentClassifier.InnerClassifiers.Select(x => x.Name).ToList();
            using (var form = new AddNewClassifierForm(existingClassifiers))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var newClassifier = new Classifier()
                    {
                        Name = form.ClassifierName,
                        SortCode = form.SortCode
                    };
                    this.CurrentClassifier.InnerClassifiers.Add(newClassifier);
                    AddChildClassifier(newClassifier);
                }
            }
        }

        void OnClickGenerateClassifiersMenuStripItem(object sender, EventArgs e)
        {
            var existingClassifiers = this.CurrentClassifier.InnerClassifiers.Select(x => x.Name).ToList();
            using (var form = new GenerateClassifiersForm(existingClassifiers))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    foreach (var newClassifier in form.Classifiers)
                    {
                        this.CurrentClassifier.InnerClassifiers.Add(newClassifier);
                        AddChildClassifier(newClassifier);
                    }
                }
            }
        }

        void OnClickEditClassifierMenuStripItem(object sender, EventArgs e)
        {
            var existingClassifiers = this.ParentClassifier?.InnerClassifiers
                ?.Select(x => x.Name)?.ToList() ?? new List<string>();
            using (var form = new EditClassifierForm(this.CurrentClassifier.Name, this.CurrentClassifier.SortCode, existingClassifiers))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.CurrentClassifier.Name = form.ClassifierName;
                    this.CurrentClassifier.SortCode = form.SortCode;
                    this.Text = form.ClassifierName;
                }
            }
        }

        void OnClickAddProductMenuStripItem(object sender, EventArgs e)
        {
            using (var form = new AddNewProductForm())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var newProduct = form.NewProduct;
                    this.CurrentClassifier.Products.Add(newProduct);
                    this.dataGridpUdater.UpdateDataGridViewSource(this.CurrentClassifier);
                }
            }
        }

        void OnClickGenerateProductMenuStripItem(object sender, EventArgs e)
        {
            using (var form = new GeterateProductsForm())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    foreach(var newProduct in form.Products)
                    {
                        this.CurrentClassifier.Products.Add(newProduct);
                    }
                }
                this.dataGridpUdater.UpdateDataGridViewSource(this.CurrentClassifier);
            }
        }

        public string GetReport(int minCount)
        {
            var csv = new StringBuilder();

            var newLine = string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"", "Path", "Article", "Product", "Count");
            csv.AppendLine(newLine);
            AppendProductsToReportByMinCount(this, minCount, csv);
            return csv.ToString();
        }

        private void AppendProductsToReportByMinCount(ClassifierTreeNode node, int minCount, StringBuilder csv)
        {
            foreach (var product in node.CurrentClassifier.Products)
            {
                if (product.Count <= minCount)
                {
                    var newLine = string.Format("\"{0}\",\"{1}\",\"{2}\",{3}", node.FullPath, product.Article, product.Name, product.Count);
                    csv.AppendLine(newLine);
                }
            }
            foreach (ClassifierTreeNode childNode in node.Nodes)
            {
                AppendProductsToReportByMinCount(childNode, minCount, csv);
            }
        }
    }
}
