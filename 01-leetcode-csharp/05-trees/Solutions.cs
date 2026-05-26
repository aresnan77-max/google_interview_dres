// ============================================================================
// Category: Trees — Google Interview Prep
// Problems: MaxDepthBinaryTree(#104), BinaryTreeLevelOrder(#102),
//           ValidateBST(#98), LowestCommonAncestor(#236),
//           SerializeDeserializeBT(#297), BinaryTreeMaxPathSum(#124)
// ============================================================================

using System;
using System.Collections.Generic;

namespace GoogleInterviewPrep.LeetCode
{
    // --- LC #104: Maximum Depth of Binary Tree (Easy) — DFS ---
    // Time: O(n) | Space: O(h)
    public class MaxDepthBinaryTree
    {
        public int MaxDepth(TreeNode? root)
        {
            if (root == null) return 0;
            return 1 + Math.Max(MaxDepth(root.left), MaxDepth(root.right));
        }
    }

    // --- LC #102: Binary Tree Level Order Traversal (Medium) — BFS ---
    // Time: O(n) | Space: O(n)
    public class BinaryTreeLevelOrder
    {
        public IList<IList<int>> LevelOrder(TreeNode? root)
        {
            var result = new List<IList<int>>();
            if (root == null) return result;
            var queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                int size = queue.Count;
                var level = new List<int>();
                for (int i = 0; i < size; i++)
                {
                    var node = queue.Dequeue();
                    level.Add(node.val);
                    if (node.left != null) queue.Enqueue(node.left);
                    if (node.right != null) queue.Enqueue(node.right);
                }
                result.Add(level);
            }
            return result;
        }
    }

    // --- LC #98: Validate Binary Search Tree (Medium) — DFS with Bounds ---
    // Time: O(n) | Space: O(h)
    public class ValidateBST
    {
        public bool IsValidBST(TreeNode? root) => Validate(root, long.MinValue, long.MaxValue);
        private bool Validate(TreeNode? node, long min, long max)
        {
            if (node == null) return true;
            if (node.val <= min || node.val >= max) return false;
            return Validate(node.left, min, node.val) && Validate(node.right, node.val, max);
        }
    }

    // --- LC #236: Lowest Common Ancestor (Medium) — DFS Recursive ---
    // Time: O(n) | Space: O(h)
    public class LowestCommonAncestor
    {
        public TreeNode? FindLCA(TreeNode? root, TreeNode p, TreeNode q)
        {
            if (root == null || root == p || root == q) return root;
            var left = FindLCA(root.left, p, q);
            var right = FindLCA(root.right, p, q);
            if (left != null && right != null) return root;
            return left ?? right;
        }
    }

    // --- LC #297: Serialize and Deserialize Binary Tree (Hard) — BFS ---
    // Time: O(n) | Space: O(n)
    public class SerializeDeserializeBT
    {
        public string Serialize(TreeNode? root)
        {
            if (root == null) return "";
            var result = new List<string>();
            var queue = new Queue<TreeNode?>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (node == null) { result.Add("null"); continue; }
                result.Add(node.val.ToString());
                queue.Enqueue(node.left);
                queue.Enqueue(node.right);
            }
            return string.Join(",", result);
        }

        public TreeNode? Deserialize(string data)
        {
            if (string.IsNullOrEmpty(data)) return null;
            var vals = data.Split(',');
            var root = new TreeNode(int.Parse(vals[0]));
            var queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            int i = 1;
            while (queue.Count > 0 && i < vals.Length)
            {
                var node = queue.Dequeue();
                if (vals[i] != "null") { node.left = new TreeNode(int.Parse(vals[i])); queue.Enqueue(node.left); }
                i++;
                if (i < vals.Length && vals[i] != "null") { node.right = new TreeNode(int.Parse(vals[i])); queue.Enqueue(node.right); }
                i++;
            }
            return root;
        }
    }

    // --- LC #124: Binary Tree Maximum Path Sum (Hard) — DFS Post-order ---
    // Time: O(n) | Space: O(h)
    public class BinaryTreeMaxPathSum
    {
        private int _maxSum;
        public int MaxPathSum(TreeNode? root)
        {
            _maxSum = int.MinValue;
            DFS(root);
            return _maxSum;
        }
        private int DFS(TreeNode? node)
        {
            if (node == null) return 0;
            int left = Math.Max(0, DFS(node.left));
            int right = Math.Max(0, DFS(node.right));
            _maxSum = Math.Max(_maxSum, left + right + node.val);
            return node.val + Math.Max(left, right);
        }
    }
}
