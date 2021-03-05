using System;
using System.Collections.Generic;
using System.Text;

namespace ListLibrary
{
    public class Tree<T> where T : IComparable
    {
        protected TreeNode<T> root;

        public Tree()
        {
            root = null;
        }

        public virtual void Insert(T value) { }
        public virtual TreeNode<T> InsertSub(TreeNode<T> root, TreeNode<T> new_node) { return root; }

        public virtual TreeNode<T> ReturnRoot() { return root; }

        public virtual void PreOrder(TreeNode<T> root) { }

        public virtual void InOrder(TreeNode<T> root) { }

        public virtual void PostOrder(TreeNode<T> root) { }
        public virtual TreeNode<T> Search(T searched, TreeNode<T> root) { return root; }

        public TreeNode<T> GetRoot()
        {
            return root;
        }
    }
}
