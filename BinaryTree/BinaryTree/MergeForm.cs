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
        public MergeForm(ListBox listbox) : this()
        {
            foreach (var item in listbox.Items)
                this.listBox1.Items.Add(item);
        }
        private void Merge(object sender, EventArgs e)
        {
            try {
                Pyramid first = (this.Owner.Owner.OwnedForms[listBox1.SelectedIndex] as TreeViewer).pyramid;
                List<int> firstEllements = first.Ellements.GetRange(0, first.Size );
                Pyramid second = new Pyramid();
                second.Ellements= (this.Owner as TreeViewer).pyramid.Ellements.GetRange(0, (this.Owner as TreeViewer).pyramid.Size);

                foreach (var i in firstEllements)
                {
                    second.Add(i);                    
                }
                TreeViewer treeViewer = new TreeViewer(second.Ellements);
                treeViewer.Owner = this.Owner.Owner;
                treeViewer.Show();
                this.Close();
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Select the item");
            }
          
        }
    }
}
