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
            PreOrdenS = "";
        }

        public override TreeNode<T> ReturnRoot()
        {
            return root;
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
            else if (root.value.CompareTo(new_node.value)==1)
            {
                root.right = InsertSub(root.right, new_node);
            }
            else
            {
                root.left = InsertSub(root.left, new_node);
            }
            return root;
        }

        public override void PreOrder(TreeNode<T> root)
        {
            if (root != null)
            {
                bool HelpAux = root.value.Equals(root.value);
                PreOrder(root.left);
                PreOrder(root.right);
            }
        }

        public override void InOrder(TreeNode<T> root)
        {
            if (root != null)
            {
                InOrder(root.left);
                bool HelpAux = root.value.Equals(root.value);
                InOrder(root.right);
            }
        }

        public override void PostOrder(TreeNode<T> root)
        {
            if(root != null)
            {
                PostOrder(root.left);
                PostOrder(root.right);
                bool HelpAux = root.value.Equals(root.value);
            }
        }
    }
}
