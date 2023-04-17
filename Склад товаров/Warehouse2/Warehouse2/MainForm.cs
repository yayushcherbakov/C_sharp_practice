using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Warehouse2.Entities;
using Warehouse2.Sorters;

namespace Warehouse2
{
    public partial class MainForm : Form
    {
        private DataGridpUdater dataGridpUdater;
        private Classifier rootClassifier;
        private ClassifierTreeNode classifierTreeNode;
        private string warehousePath = string.Empty;

        public MainForm()
        {
            InitializeComponent();

            InitTreeView();
            this.dataGridpUdater = new DataGridpUdater(this.dataGridView1);

            saveFileDialog1.Filter = "Warehouse files(*.warehouse)|*.warehouse";
            openFileDialog1.Filter = "Warehouse files(*.warehouse)|*.warehouse";
            this.fileToolStripMenuItem.DropDownOpening += FileToolStripDropDownItemDropDownOpening;
            this.generateReportToolStripMenuItem.Enabled = false;
            this.sortToolStripMenuItem.Enabled = false;
        }

        private void FileToolStripDropDownItemDropDownOpening(Object sender, EventArgs e)
        {
            this.saveToolStripMenuItem.Enabled = !string.IsNullOrWhiteSpace(this.warehousePath);
            this.saveAsToolStripMenuItem.Enabled = !string.IsNullOrWhiteSpace(this.warehousePath);
        }

        private void InitTreeView()
        {
            this.treeView1.BeforeSelect += TreeView1BeforeSelect;
        }

        void TreeView1BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            var classifer = ((ClassifierTreeNode)e.Node).CurrentClassifier;
            this.dataGridpUdater.UpdateDataGridViewSource(classifer);
        }

        private void NewToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
                this.warehousePath = saveFileDialog1.FileName;
                var rootName = Path.GetFileNameWithoutExtension(this.warehousePath);

                this.rootClassifier = new Classifier()
                {
                    Name = rootName
                };

                string output = JsonConvert.SerializeObject(this.rootClassifier);
                File.WriteAllText(this.warehousePath, output, Encoding.UTF8);

                this.classifierTreeNode = new ClassifierTreeNode(this.rootClassifier, this.dataGridpUdater);

                this.treeView1.Nodes.Add(this.classifierTreeNode);
                HideHelloPictureBoxAndLabels();
                this.sortToolStripMenuItem.Enabled = true;
                this.generateReportToolStripMenuItem.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Can not create warehouse. Please change directory and try again");
            }
        }

        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
                this.warehousePath = openFileDialog1.FileName;

                var content = File.ReadAllText(this.warehousePath, Encoding.UTF8);

                this.rootClassifier = JsonConvert.DeserializeObject<Classifier>(content);
                this.classifierTreeNode = new ClassifierTreeNode(this.rootClassifier, this.dataGridpUdater);
                this.treeView1.Nodes.Clear();
                this.treeView1.Nodes.Add(this.classifierTreeNode);
                this.dataGridpUdater.UpdateDataGridViewSource(this.rootClassifier);
                HideHelloPictureBoxAndLabels();
                this.generateReportToolStripMenuItem.Enabled = true;
                this.sortToolStripMenuItem.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Can not open warehouse. File is corrupred or no access permissions");
            }
        }

        private void HideHelloPictureBoxAndLabels()
        {
            this.pictureBox1.Hide();
            this.label1.Hide();
            this.label2.Hide();
        }

        private void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                string output = JsonConvert.SerializeObject(rootClassifier);
                File.WriteAllText(this.warehousePath, output, Encoding.UTF8);
            }
            catch
            {
                MessageBox.Show("Can not save warehouse. Please change directory and try again");
            }
        }

        private void SaveAsToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
                this.warehousePath = saveFileDialog1.FileName;
                string output = JsonConvert.SerializeObject(rootClassifier);
                File.WriteAllText(this.warehousePath, output, Encoding.UTF8);
            }
            catch
            {
                MessageBox.Show("Can not save warehouse. Please change directory and try again");
            }
        }

        private void GenerateReportToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                using (var form = new GetReportRestrictionForm())
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        var reportPath = form.ReportPath;
                        var minCount = form.MinCount;

                        File.WriteAllText(reportPath, this.classifierTreeNode.GetReport(minCount));
                        MessageBox.Show($"Report successfully created({reportPath})");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can not save report. Please change directory and try again");
            }
            
        }

        private void AscToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.treeView1.TreeViewNodeSorter = new ClassifierAscSorter();
            this.treeView1.Sort();
        }

        private void DescToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.treeView1.TreeViewNodeSorter = new ClassifierDescSorter();
            this.treeView1.Sort();
        }

        private void HelpToolStripMenuItemClick(object sender, EventArgs e)
        {
            var form = new HelpForm();
            form.Show();
        }
    }
}
