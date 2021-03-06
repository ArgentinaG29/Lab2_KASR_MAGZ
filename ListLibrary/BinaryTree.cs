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
            if (root != null)
            {
                PostOrder(root.left);
                PostOrder(root.right);
                bool HelpAux = root.value.Equals(root.value);
            }
        }

        public override TreeNode<T> Search(T searched, TreeNode<T> root)
        {
            int comparison = 0;
            if (root != null)
            {
                comparison = searched.CompareTo(root.value);
            }
            
            if (root == null || comparison == 0)
            {
                return root;
            }
            else
            {
                if (comparison == 1)
                {
                    return Search(searched, root.right);
                }
                else
                {
                    return Search(searched, root.left);
                }
            }
        }

        public override TreeNode<T> Delete(T value, TreeNode<T> root)
        {
            int comparison = value.CompareTo(root.value);
            if (root == null)
            {
                return root;
            }

            if (comparison == -1)
            {
                root.left = Delete(value, root.left);
            }
            else
            {
                if (comparison == 1)
                {
                    root.right = Delete(value, root.right);
                }
                else
                {
                    if (root.left == null)
                    {
                        return root.right;
                    }
                    else if (root.right == null)
                    {
                        return root.left;
                    }

                    root.value = GetMinor(root.right).value;
                    root.right = Delete(root.value, root.right);
                }
            }
            return root;
        }

        public override TreeNode<T> GetMinor(TreeNode<T> root)
        {
            TreeNode<T> min = root;
            while (root.left != null)
            {
                min = root.left;
                root = root.left;
            }
            return min;
        }
    }
}
