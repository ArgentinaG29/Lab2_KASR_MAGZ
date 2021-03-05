using System;
using System.Collections.Generic;
using System.Text;

namespace ListLibrary
{
    public class BinaryTree<T>: Tree<T> where T : IComparable
    {
        public BinaryTree()
        {
            root = null;
        }

        public override void Insert(T value)
        {
            TreeNode<T> new_node = new TreeNode<T>();
            new_node.value = value;

            if (root == null)
            {
                root = new_node;
            }
            else
            {
                InsertSub(root, new_node);
            }
        }

        public override TreeNode<T> InsertSub(TreeNode<T> root, TreeNode<T> new_node)
        {
            if (root == null)
            {
                root = new_node;
            }
            else if (new_node.value.CompareTo(root.value) == 1)
            {
                root.right = InsertSub(root.right, new_node);
            }
            else
            {
                root.left = InsertSub(root.left, new_node);
            }
            return root;
        }
    }
}
