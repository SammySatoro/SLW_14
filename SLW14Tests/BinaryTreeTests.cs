using Microsoft.VisualStudio.TestTools.UnitTesting;
using SLW14;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SLW14.Tests
{
    [TestClass()]
    public class BinaryTreeTests
    {
        private void FillTheList(BinaryTreeNode<int> node, List<int> list)
        {
            if (node != null)
            {
                list.Add(node.Value);
                FillTheList(node._left, list);
                FillTheList(node._right, list);
            }
        }
        [TestMethod()]
        public void DoFirstProcessTest()
        {
            BinaryTree<int> _tree2 = new BinaryTree<int>();
            _tree2.Add(15);
            _tree2.Add(4);
            _tree2.Add(7);
            _tree2.Add(0);
            _tree2.Add(20);
            _tree2.Add(18);
            _tree2.Add(19);
            _tree2.Add(2);
            _tree2.Add(17);
            _tree2.Add(6);
            _tree2.Add(5);
            string line;
            string path = @"C:\Users\Евгений\OneDrive\Рабочий стол\tree.res";
            StreamReader sr = new StreamReader(path, System.Text.Encoding.Default);
            List<int> list1 = new List<int>();
            List<int> list2 = new List<int>();
            _tree2.DoFirstProcess(_tree2.GetHead);
            while ((line = sr.ReadLine()) != null)
            {
                list1.Add(Convert.ToInt32(line));
            }
            FillTheList(_tree2.GetHead, list2);
            CollectionAssert.AreEquivalent(list1, list2);
            Console.WriteLine("Test is passed!");
        }
    }
}