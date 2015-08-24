using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace BinaryTree
{
    public partial class Form1 : Form
    {
        List<int> xItems = new List<int>();
        List<int> yItems = new List<int>();
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
        }

        private void BuildTree(object sender, EventArgs e)
        {
            TreeViewer treeViewer = new TreeViewer(xItems.ToArray(),yItems.ToArray());
            treeViewer.Owner = this;
            treeViewer.Text += " "+OwnedForms.Length;
            treeViewer.Show();
        }
       

        private bool GetText(string s)
        {
            xItems.Clear();
            yItems.Clear();
            foreach (string line in s.Split('\n'))

            {
                int spacePos=line.IndexOf(" ");
                int p, p1;
                if (spacePos>0&&Int32.TryParse(line.Substring(0, spacePos), out p) && Int32.TryParse(line.Substring(spacePos), out p1))
                {
                    xItems.Add(p);
                    yItems.Add(p1);
                }
                else
                    return false;
            }
            return true;
        }

        private void ImportFromFile(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) // відкриваємо файл
            {
                using (StreamReader str = new StreamReader(openFileDialog1.FileName))
                {
                    string s = str.ReadToEnd().Replace(';',' ').Replace(',', ' ');
                    if (!GetText(s))
                    {
                        MessageBox.Show("File has wrong syntax! Acceptable seperators: \"space\" , \"enter\" , \";\" , \",\"!");
                        return;
                    }
                    textBox1.Text = s;
                }
            }
        }

        private void CheckText(object sender, EventArgs e)
        {
            if (GetText(textBox1.Text))
            button1.Enabled = true;
              else
                  button1.Enabled = false; 
        }
    }
}
