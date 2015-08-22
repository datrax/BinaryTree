using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BinaryTree
{
    public partial class Form1 : Form
    {
        List<int> Ellements = new List<int>();
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TreeViewer treeViewer = new TreeViewer(Ellements);
            treeViewer.ShowDialog();
        }
        public bool TextIsValid(string text)
        {

            Ellements.Clear();
            int k;
            int pos = 0;
            do
            {
                for (int i = pos; i < text.Length; i++)
                {
                    if (text[i] == ' ' || i == text.Length - 1)
                    {
                        if (int.TryParse(text.Substring(pos, i - pos + 1), out k))
                        {
                            Ellements.Add(k);
                        }
                        else
                        {
                            return false;
                        }
                        pos = i;
                    }
                }
                pos++;

            } while (pos < text.Length);
            if (Ellements.Count > 0) return true;
            if (int.TryParse(text.Substring(0, text.Length), out k))
            {
                Ellements.Add(k);
                return true;
            }
            return false;
        }

        private void GetText(object sender, EventArgs e)
        {
            if (!TextIsValid(((TextBox)sender).Text))
                button1.Enabled = false;
            else
                button1.Enabled = true;
                
        }

        private void ImportFromFile(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) // відкриваємо файл
            {
                using (StreamReader str = new StreamReader(openFileDialog1.FileName))
                {
                    string s = str.ReadToEnd().Replace(';',' ').Replace('\n', ' ').Replace(',', ' ');
                    if (!TextIsValid(s))
                    {
                        MessageBox.Show("File has wrong syntax! Acceptable seperators: \"space\" , \"enter\" , \";\" , \",\"!");
                        return;
                    }
                    textBox1.Text = s;
                }
            }
        }
    }
}
