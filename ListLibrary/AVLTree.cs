using System;
using System.Collections.Generic;
using System.Text;

namespace ListLibrary
{
    public class AVLTree<T>: Tree<T> where T : IComparable
    {
        public AVLTree()
        {
            root = null;
        }

        int Height(TreeNode<T> root)
        {
            if(root == null)
            {
                return 0;
            }
            else
            {
                return root.height;
            }
        }

        int Max(int a, int b)
        {
            return (a > b) ? a : b;
        }
        TreeNode<T> RightRotate(TreeNode<T> root)
        {
            TreeNode<T> x = root.left;
            TreeNode<T> T2 = x.right;

            x.right = root;
            root.left = T2;

            root.height = Max(Height(root.left), Height(root.right)) + 1;
            x.height = Max(Height(x.left), Height(x.right)) + 1;

            return x;
        }

        TreeNode<T> leftRotate(TreeNode<T> x)
        {
            TreeNode<T> y = x.right;
            TreeNode<T> T2 = y.left;

            y.left = x;
            x.right = T2;

            x.height = Max(Height(x.left), Height(x.right)) + 1;
            y.height = Max(Height(y.left), Height(y.right)) + 1;

            return y;
        }

        int GetBalance(TreeNode<T> NumberBalance)
        {
            if (NumberBalance == null)
            {
                return 0;
            }
            else
            {
                return Height(NumberBalance.left) - Height(NumberBalance.right);
            }
        }


        public override TreeNode<T> ReturnRoot()
        {
            return root;
        }

        public override void InsertAVL(T value)
        {
            TreeNode<T> new_node = new TreeNode<T>();
            new_node.value = value;

            if (root == null)
            {
                root = new_node;
            }
            else
            {
                root = InsertSubAVL(root, new_node);
            }
        }

        public override TreeNode<T> InsertSubAVL(TreeNode<T> root, TreeNode<T> new_node)
        {
            if (root == null)
            {
                root = new_node;
            }
            else if (new_node.value.CompareTo(root.value) == 1)
            {
                root.right = InsertSubAVL(root.right, new_node);
            }
            else if(new_node.value.CompareTo(root.value) == -1)
            {
                root.left = InsertSubAVL(root.left, new_node);
            }

            //Actualizar altura
            root.height = 1 + Max(Height(root.left), Height(root.right));

            int balance = GetBalance(root);

            if(balance > 1 && new_node.value.CompareTo(root.left.value)==-1)
            {
                root = RightRotate(root);
            }
            else if(balance < -1 && new_node.value.CompareTo(root.right.value) == 1)
            {
                root = leftRotate(root);
            }
            else if(balance > 1 && new_node.value.CompareTo(root.left.value) == 1)
            {
                root.left = leftRotate(root.left);
                root = RightRotate(root);
            }
            else if(balance < -1 && new_node.value.CompareTo(root.right.value) == -1)
            {
                root.right = RightRotate(root.right);
                root = leftRotate(root);
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

    }
}
