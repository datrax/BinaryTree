using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinaryTree
{
    public partial class MergeForm : Form
    {
        public MergeForm()
        {
            InitializeComponent();        
        }

        private void Merge(object sender, EventArgs e)
        {
            TreeViewer treeViewer=new TreeViewer();
            treeViewer.treap = Treap.Merge(((TreeViewer)this.Owner).treap, ((TreeViewer)this.Owner.Owner.OwnedForms[listBox1.SelectedIndex]).treap);
            treeViewer.Owner = this.Owner.Owner;
            treeViewer.Text += " " + this.Owner.Owner.OwnedForms.Length;
            this.Close();
            treeViewer.Show();
        }

        private void MergeForm_Load(object sender, EventArgs e)
        {
            foreach (var item in this.Owner.Owner.OwnedForms)
                this.listBox1.Items.Add(item.Text);
        }
    }
}
