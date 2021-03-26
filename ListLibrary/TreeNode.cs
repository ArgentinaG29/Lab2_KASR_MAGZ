using System;
using System.Collections.Generic;
using System.Text;

namespace ListLibrary
{
    public class TreeNode<T>: Node<T> 
    {
        public TreeNode<T> right;
        public TreeNode<T> left;
        public int height;
        

        public TreeNode()
        {
            value = default(T);
            right = null;
            left = null;
            height = 1;
        }
    }
}
