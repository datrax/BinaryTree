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
    public partial class TreeViewer : Form
    {
        public Treap treap;
        public TreeViewer()
        {
            InitializeComponent();
        }
        public TreeViewer(int[] xItems, int[] yItems) : this()
        {
            treap = Treap.Treapify(xItems, yItems);
        }
        private void TreeViewer_Load(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void DrawTree(object sender, PaintEventArgs e)
        {
            if (treap == null) return;
            DrawTear(treap, 0, 0, pictureBox1.Width / 2.0F, e);

        }

        void DrawTear(Treap treap, int tear, float x, float delta, PaintEventArgs e)
        {
            int tearWidth = 60;
            int horizontalCoef = 20;
            int vertCoef = 20;
            Font font = new Font("Times New Roman", 15);
            Brush brush = new SolidBrush(Color.Black);
            Pen pen = new Pen(Color.FloralWhite, 1);
            float y = tear * tearWidth;
            x += delta;
            e.Graphics.DrawString(treap.x + "; " + treap.y, font, brush, x, y);
            if (delta != 0)
            {
                e.Graphics.DrawLine(pen, x + horizontalCoef, y, x - delta + horizontalCoef, y - tearWidth + vertCoef);
            }

            delta = Math.Abs(delta) / 2F;


            if (treap.Left != null)
                DrawTear(treap.Left, tear + 1, x, -delta, e);
            if (treap.Right != null)
                DrawTear(treap.Right, tear + 1, x, +delta, e);

        }

        private void Redraw(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }


        private void AddItem(object sender, EventArgs e)
        {
            int x, y;
            if (!Int32.TryParse(textBox2.Text, out x))
            {
                MessageBox.Show("Wrong number");
                return;
            }
            if (!Int32.TryParse(textBox3.Text, out y))
            {
                MessageBox.Show("Wrong number");
                return;
            }
            if (treap == null)
                treap = new Treap();
            treap = treap.Add(x, y);
            pictureBox1.Invalidate();
        }

        private void DeleteItem(object sender, EventArgs e)
        {
            int x;
            if (treap != null)
            {
                if (!Int32.TryParse(textBox1.Text, out x))
                {
                    MessageBox.Show("Wrong number");
                    return;
                }
                try
                {
                    treap = treap.Remove(x);
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("There is no element witn index " + x);
                }
            }
            pictureBox1.Invalidate();
        }



        private void SplitTree(object sender, EventArgs e)
        {
            int n;
            if (!Int32.TryParse(textBox4.Text, out n))
            {
                MessageBox.Show("Wrong number");
                return;
            }
            if (n == -1)
            {
                MessageBox.Show("There is no such element");
                return;
            }
            Treap leftTree;
            Treap rightTree;
            TreeViewer treeViewer = new TreeViewer();

            treap.Split(n, out leftTree, out rightTree);

            treeViewer.treap = leftTree;
            treeViewer.Owner = this.Owner;
            treeViewer.Show();
            treeViewer.Text += " " + this.Owner.OwnedForms.Length;
            TreeViewer treeViewer1 = new TreeViewer();
            treeViewer1.treap = rightTree;
            treeViewer1.Owner = this.Owner;
            treeViewer1.Show();
            treeViewer1.Text += " " + this.Owner.OwnedForms.Length;
        }

        private void MergeTree(object sender, EventArgs e)
        {
            MergeForm mf = new MergeForm();
            mf.Owner = this;
            mf.ShowDialog();
        }
    }
}
