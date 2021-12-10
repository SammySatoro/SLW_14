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

namespace SLW14
{
    public partial class Form1 : Form
    {
        static BinaryTree<int> _tree;
        public string _fileName { get; set; }
        public static FileInfo _file;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _tree = new BinaryTree<int>();
            _tree.Add(15);
            _tree.Add(4);
            _tree.Add(7);
            _tree.Add(0);
            _tree.Add(20);
            _tree.Add(18);
            _tree.Add(19);
            _tree.Add(2);
            _tree.Add(17);
            _tree.Add(6);
            _tree.Add(5);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Graphics graphics = Graphics.FromHwnd(pictureBox1.Handle);
            SolidBrush solidBrush = new SolidBrush(Color.White);
            graphics.FillRectangle(solidBrush, 0, 0, pictureBox1.Width, pictureBox1.Height);
            int x = pictureBox1.Width / 2;
            int xx = pictureBox1.Width / 2;
            int y = 30;
            int yy = pictureBox1.Height / (_tree.GetCount + 1);
            _tree.Draw(graphics, _tree.GetHead, x, y, xx, yy);            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Graphics graphics = Graphics.FromHwnd(pictureBox1.Handle);
            SolidBrush solidBrush = new SolidBrush(Color.White);
            bool flag = true;
            _tree.DoFirstProcess(_tree.GetHead);
            if (_tree.IsBinarySearchTree(_tree.GetHead, ref flag))
                textBox1.ForeColor = Color.Lime;
            else
                textBox1.ForeColor = Color.IndianRed;
            graphics.FillRectangle(solidBrush, 0, 0, pictureBox1.Width, pictureBox1.Height);
            int x = pictureBox1.Width / 2;
            int xx = pictureBox1.Width / 2;
            int y = 30;
            int yy = pictureBox1.Height / (_tree.GetCount + 1);
            _tree.Draw(graphics, _tree.GetHead, x, y, xx, yy);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            _tree.DoSecondProcess(_tree.GetHead);
            for (int i = 0; i <= _tree._levelCount; i++)
            {
                try
                {
                    if (Convert.ToInt32(textBox3.Text) <= _tree._levelCount && Convert.ToInt32(textBox3.Text) >= 0)
                        textBox2.Text = $"{_tree._levelArray[Convert.ToInt32(textBox3.Text)].TotalSum / _tree._levelArray[Convert.ToInt32(textBox3.Text)].NodeNumber}";
                    else
                        textBox2.Text = "The level is not exist!";
                }
                catch
                {
                    textBox2.Text = "0";
                    textBox3.Text = "";
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            _fileName = saveFileDialog1.FileName;
            FileInfo _file = new FileInfo(_fileName);
            _file.Create().Close();
            StreamWriter sw = _file.AppendText();
            _tree.SaveIntoFile(sw, _tree.GetHead);
            sw.Close();
            MessageBox.Show("The tree is saved!");
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }     
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
