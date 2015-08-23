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
        public Pyramid pyramid;
        public string CodeName;
        public TreeViewer()
        {
            InitializeComponent();
        }
        public TreeViewer(List<int> list) : this()
        {
            pyramid = new Pyramid(list);
        }
        private void TreeViewer_Load(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void DrawTree(object sender, PaintEventArgs e)
        {
            if (pyramid.Size == 0)
            {
                CodeName = "";
                return;
            }
            int tear = 0;
            int counter = 0;
            int elWidth = 30;
            int elHeight = 20;
            bool noElementsLeft = false;
            int tearWidth = 60;
            Pen pen = new Pen(Color.FloralWhite, 1);
            Font font = new Font("Times New Roman", 15);
            Brush brush = new SolidBrush(Color.Black);
            while (!noElementsLeft)
            {
                for (int i = 1; i <= Math.Pow(2, tear) && !noElementsLeft; i++)
                {
                    float x1 = (float)(i * pictureBox1.Width / (Math.Pow(2, tear) + 1));
                    float y1 = tear * tearWidth;                    
                    e.Graphics.DrawString(pyramid.Ellements[counter].ToString(), font, brush, x1, y1);
                    if (tear != 0)
                    {
                        float x2 = (float)((Math.Round((i) / 2.0, MidpointRounding.AwayFromZero)) * pictureBox1.Width / (Math.Pow(2, tear - 1) + 1)) + elWidth / 2.0F;
                        float y2 = (tear - 1) * tearWidth + elHeight;
                        e.Graphics.DrawLine(pen, x1 + elWidth / 2.0F, y1, x2, y2);

                    }
                    if (++counter == pyramid.Size)
                        noElementsLeft = true;
                }
                tear++;
            }
            string name = "";
            foreach (int p in pyramid.Ellements)
            {
                name += p + " ";
            }
            CodeName = name;
        }

        private void Redraw(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void SortPyramid(object sender, EventArgs e)
        {
            pyramid.HeapSort();
            pictureBox1.Invalidate();
        }

        private void AddItem(object sender, EventArgs e)
        {
            int n;
            if (!Int32.TryParse(textBox1.Text, out n))
            {
                MessageBox.Show("Wrong number");
                return;
            }
            pyramid.Add(n);
            pyramid.HeapSort();
            pictureBox1.Invalidate();
        }

        private void DeleteTop(object sender, EventArgs e)
        {
            if (pyramid.Size == 0) return;
            pyramid.GetMax();
            pyramid.Heapify(0);
            pyramid.HeapSort();
            pictureBox1.Invalidate();
        }

        private void ReplceItems(object sender, EventArgs e)
        {
            int n, t;
            if (!Int32.TryParse(textBox2.Text, out n))
            {
                MessageBox.Show("Wrong number");
                return;
            }
            if (!Int32.TryParse(textBox3.Text, out t))
            {
                MessageBox.Show("Wrong number");
                return;
            }
            n = pyramid.Ellements.IndexOf(n);
            if (n == -1)
            {
                MessageBox.Show("There is no such element");
                return;
            }
            pyramid.HeapIncreaseKey(n, t);
            pyramid.HeapSort();
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
            n = pyramid.Ellements.IndexOf(n);
            if (n == -1)
            {
                MessageBox.Show("There is no such element");
                return;
            }
            List<int> firstEllements = new List<int>();
            List<int> secondEllements = new List<int>();
            pyramid.SplitTree(firstEllements, secondEllements, n);
            TreeViewer treeViewer = new TreeViewer(firstEllements);
            treeViewer.Show();
            TreeViewer treeViewer2 = new TreeViewer(secondEllements);
            treeViewer2.Show();
            treeViewer.Owner = this.Owner;
            treeViewer2.Owner = this.Owner;
        }

        private void button6_Click(object sender, EventArgs e)
        {

            ListBox lb = new ListBox();
            foreach (TreeViewer form in this.Owner.OwnedForms)
            {
                lb.Items.Add(form.CodeName);
            }
            MergeForm mf = new MergeForm(lb);
            mf.Owner = this;
            mf.ShowDialog();
        }
    }
}
