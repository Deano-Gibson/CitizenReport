using System;
using System.Collections.Generic;

namespace CitizenReportWeb.Data
{
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        private class Node
        {
            public T Data;
            public Node? Left;
            public Node? Right;

            public Node(T data) => Data = data;
        }

        private Node? _root;

        public BinarySearchTree(IEnumerable<T> items)
        {
            foreach (var item in items)
                Insert(item);
        }

        public void Insert(T value) => _root = Insert(_root, value);

        private Node Insert(Node? node, T value)
        {
            if (node == null) return new Node(value);
            int cmp = value.CompareTo(node.Data);
            if (cmp < 0) node.Left = Insert(node.Left, value);
            else if (cmp > 0) node.Right = Insert(node.Right, value);
            return node;
        }

        public List<T> InOrderTraversal()
        {
            var list = new List<T>();
            Traverse(_root, list);
            return list;
        }

        private void Traverse(Node? node, List<T> list)
        {
            if (node == null) return;
            Traverse(node.Left, list);
            list.Add(node.Data);
            Traverse(node.Right, list);
        }
    }
}
