using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SLW14
{
    public struct Level
    {
        private double _nodeNumber;
        private double _totalSum;
        public double NodeNumber { get => _nodeNumber; set => _nodeNumber = value; }
        public double TotalSum { get => _totalSum; set => _totalSum = value; }
    }

    public class BinaryTreeNode<TNode> : IComparable<TNode> where TNode : IComparable
    {

        public BinaryTreeNode<TNode> _left { get; set; }
        public BinaryTreeNode<TNode> _right { get; set; }
        private int _level;
        private TNode _value;
        public TNode Value { get => _value; set => _value = value; }
        public int Level { get => _level; set => _level = value; }

        public BinaryTreeNode(TNode value)
        {
            _value = value;
            _left = _right = null;
            _level = 0;
        }
        public int CompareTo(TNode other)
        {
            return _value.CompareTo(other);
        }
        public bool IsPrime(int value)
        {
            int divCount = 1;
            if (value == 0)
                return false;
            for (int i = 2; i <= value; i++)
            {
                if (value % i == 0) divCount++;
                if (divCount > 2) return false;
            }
            return true;
        }
    }

    public class BinaryTree<T> where T : IComparable
    {
        private BinaryTreeNode<T> _head;
        private int _count;
        public int GetCount { get => _count; }
        public BinaryTreeNode<T> GetHead { get => _head; }
        public int _levelCount = 0;
        public Level[] _levelArray;

        public void Add(T value)
        {
            if (_head == null)
            {
                _head = new BinaryTreeNode<T>(value);
            }
            else
            {
                AddTo(_head, value, 1);
            }
            _count++;
        }
        private void AddTo(BinaryTreeNode<T> node, T value, int lvl)
        {
            if (value.CompareTo(node.Value) < 0)
            {
                if (node._left == null)
                {
                    node._left = new BinaryTreeNode<T>(value);
                    node._left.Level = lvl;
                }
                else
                {
                    AddTo(node._left, value, lvl + 1);
                }
            }
            else
            {
                if (node._right == null)
                {
                    node._right = new BinaryTreeNode<T>(value);
                    node._right.Level = lvl;
                }
                else
                {                   
                    AddTo(node._right, value, lvl + 1);
                }
            }
            if (_levelCount < lvl) _levelCount = lvl;
        }
        public void DoFirstProcess(BinaryTreeNode<int> node)
        {
            if (node._right != null || node._left != null)
            {
                if (node._left != null)
                {
                    if (node.IsPrime(node._left.Value))
                    {
                        node.Value = 0;
                    }
                }
                if (node._right != null)
                {
                    if (node.IsPrime(node._right.Value))
                    {
                        node.Value = 0;
                    }
                }
                if (node._left != null)
                    DoFirstProcess(node._left);
                if (node._right != null)
                    DoFirstProcess(node._right);
            }
        }
        private int CalculateSum(BinaryTreeNode<int> node, int level)
        {
            if (node != null)
            {
                if (node.Level == level)
                    return node.Value + CalculateSum(node._left, level) + CalculateSum(node._right, level);
                return CalculateSum(node._left, level) + CalculateSum(node._right, level);
            }
            return 0;
        }
        private int CountNodes(BinaryTreeNode<int> node, int level)
        {
            if (node != null)
            {
                if (node.Level == level)
                    return 1 + CountNodes(node._left, level) + CountNodes(node._right, level);
                return CountNodes(node._left, level) + CountNodes(node._right, level);
            }
            return 0;
        }
        public void DoSecondProcess(BinaryTreeNode<int> node)
        {
            try
            {
                _levelArray = new Level[_levelCount + 1];
                for (int i = 0; i <= _levelCount; i++)
                {
                    _levelArray[i].NodeNumber = CountNodes(node, i);
                    _levelArray[i].TotalSum = CalculateSum(node, i);
                }
            }
            catch(NullReferenceException ex)
            {
                MessageBox.Show($"NullReferenceException: {ex} \n You should declare the \"_levelArray\" object!");
            }
        }
        public bool IsBinarySearchTree(BinaryTreeNode<int> node, ref bool isTrue)
        {
            if (node._right != null || node._left != null)
            {
                if (node._right != null)
                {
                    if (node.Value.CompareTo(node._right.Value) > 0)
                    {
                        isTrue = false;
                    }
                    IsBinarySearchTree(node._right, ref isTrue);
                }
                if (node._left != null)
                {
                    if (node.Value.CompareTo(node._left.Value) <= 0)
                    {
                        isTrue = false;
                    }
                    IsBinarySearchTree(node._left, ref isTrue);
                }
            }
            return isTrue;
        }
        public void SaveIntoFile(System.IO.StreamWriter sw, BinaryTreeNode<int> node)
        {
            try
            {
                if (node != null)
                {
                    sw.WriteLine(node.Value);
                    SaveIntoFile(sw, node._left);
                    SaveIntoFile(sw, node._right);
                }
            }
            catch
            {
                
            }
        }
        public void Draw(Graphics graphics, BinaryTreeNode<int> node, int x, int y, int xx, int yy)
        {
            if (node != null)
            {
                Pen pen = new Pen(Color.Black, 1);
                SolidBrush sBrush = new SolidBrush(Color.Black);
                Font font = new Font("Arial", 11, FontStyle.Regular);
                graphics.DrawEllipse(pen, x - 16, y - 16, 32, 32);
                graphics.DrawString(node.Value.ToString(), font, sBrush, x - 8, y - 8, StringFormat.GenericDefault);
                xx = xx / 2;
                if (xx < 32) xx = 32;
                if (node._right != null)
                {
                    graphics.DrawLine(pen, x, y, x + xx, y + yy);
                    Draw(graphics, node._right, x + xx, y + yy, xx, yy);
                }
                if (node._left != null)
                {
                    graphics.DrawLine(pen, x, y, x - xx, y + yy);
                    Draw(graphics, node._left, x - xx, y + yy, xx, yy);
                }
            }
        }
    }
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());       
        }
    }
}
