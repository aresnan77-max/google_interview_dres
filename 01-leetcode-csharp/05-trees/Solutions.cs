// ============================================================================
// Category: Trees — Google Interview Prep
// Problems: MaxDepthBinaryTree(#104), BinaryTreeLevelOrder(#102),
//           ValidateBST(#98), LowestCommonAncestor(#236),
//           SerializeDeserializeBT(#297), BinaryTreeMaxPathSum(#124),
//           InvertBinaryTree(#226), DiameterOfBinaryTree(#543),
//           KthSmallestInBST(#230), SameTree(#100)
// ============================================================================

using System;
using System.Collections.Generic;

namespace GoogleInterviewPrep.LeetCode
{
    // 🎤 GOOGLE DEMO — recursion warm-up; every Google candidate gets a tree question.
    //   Q: "Return the MAXIMUM DEPTH of a binary tree (#nodes on longest root→leaf path)."
    //   Ex: [3,9,20,null,null,15,7] → 3
    //   Approaches: ① recursive DFS O(n)/O(h) ★  ② iterative BFS level count O(n)/O(w)
    //   🚩 Red flag: confusing height (edges) vs depth (nodes) — always clarify.
    //   ✨ Strong hire: mention worst-case skewed tree → O(n) stack → prefer iterative if stack-bounded.
    //   Follow-ups: LC 111 (MIN depth — careful with null children!), LC 559 (N-ary), LC 110 (height-balanced).
    // --- LC #104: Maximum Depth of Binary Tree (Easy) — DFS ---
    // GOAL: Return the number of nodes along the longest root-to-leaf path.
    //
    // INTUITION: The depth of a node = 1 + max(depth of left child,
    //   depth of right child). A null node has depth 0. This recursive
    //   definition is the solution itself.
    //
    // STEPS:
    //   maxDepth(null) = 0
    //   maxDepth(node) = 1 + max(maxDepth(node.left), maxDepth(node.right))
    //
    // WHY IT WORKS: Post-order DFS: children's depths are computed before
    //   combining, so the result naturally bubbles up from leaves to root.
    //
    // Time: O(n) | Space: O(h)  h = tree height
    public class MaxDepthBinaryTree
    {
        public int MaxDepth(TreeNode? root)
        {
            if (root == null) return 0;
            return 1 + Math.Max(MaxDepth(root.left), MaxDepth(root.right));
        }
    }

    // 🎤 GOOGLE DEMO — L4 staple; tests BFS template + level grouping.
    //   Q: "Return level-order traversal grouped LEVEL BY LEVEL, left to right."
    //   Ex: [3,9,20,null,null,15,7] → [[3],[9,20],[15,7]]
    //   Approaches: ① BFS w/ size snapshot per level O(n)/O(w) ★  ② DFS w/ depth param O(n)/O(h)
    //   🚩 Red flag: not snapshotting queue.Count BEFORE the inner loop — mixes levels together.
    //   ✨ Strong hire: same template handles right-view, averages, zigzag — mention this generality.
    //   Follow-ups: LC 199 (right-side view), LC 103 (zigzag), LC 637 (averages), LC 314 (vertical).
    // --- LC #102: Binary Tree Level Order Traversal (Medium) — BFS ---
    // GOAL: Return node values grouped by level (top to bottom).
    //
    // INTUITION: BFS processes nodes level by level. At the start of each
    //   BFS iteration, the queue contains exactly all nodes on the current
    //   level. Record queue.Count before processing to know when the level ends.
    //
    // STEPS:
    //   Enqueue root.
    //   While queue non-empty:
    //     size = queue.Count  (nodes on this level)
    //     For i = 0..size-1: dequeue, record val, enqueue left & right children.
    //     Append the level list to result.
    //
    // WHY IT WORKS: BFS guarantees we visit all nodes at depth d before any
    //   node at depth d+1, so the snapshot-at-start-of-loop gives clean levels.
    //
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

    // 🎤 GOOGLE DEMO — HIGH-FREQUENCY onsite; trips many candidates.
    //   Q: "Is this a valid BST? Left subtree < node < right subtree, recursively."
    //   Ex: [5,1,4,null,null,3,6] → false (3 in right subtree but <5)
    //   Approaches: ① compare to PARENT only (WRONG)  ② recursive (min,max) bounds O(n)/O(h) ★  ③ iterative inorder + strictly-increasing check O(n)/O(h) ★
    //   🚩 Red flag: only checking node vs immediate children — silently passes invalid trees.
    //   ✨ Strong hire: use `long?` (or long.MinValue/MaxValue) for bounds to handle int.MinValue / MaxValue leaf values.
    //   Follow-ups: LC 99 (recover BST w/ swapped nodes), LC 1008 (build BST from preorder), LC 530 (min diff).
    // --- LC #98: Validate Binary Search Tree (Medium) — DFS with Bounds ---
    // GOAL: Verify that every node satisfies the BST invariant: all nodes in
    //       the left subtree are strictly less, right subtree strictly greater.
    //
    // INTUITION: Simply checking left < root < right at each node is NOT
    //   enough (e.g., a right subtree node could be less than the root's
    //   ancestor). Pass down an allowed (min, max) range and verify each
    //   node falls strictly within it.
    //
    // STEPS:
    //   validate(node, min=-∞, max=+∞):
    //     if node == null: return true
    //     if node.val <= min or node.val >= max: return false
    //     return validate(left, min, node.val) && validate(right, node.val, max)
    //
    // WHY IT WORKS: The min/max range tightens as we descend: going left sets
    //   a new upper bound; going right sets a new lower bound. Any violation
    //   of an ancestor constraint is caught.
    //
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

    // 🎤 GOOGLE DEMO — "org chart" onsite; tests post-order return-value composition.
    //   Q: "Find the LOWEST COMMON ANCESTOR of nodes p and q in a binary tree."
    //   Ex: root=[3,5,1,6,2,0,8,null,null,7,4], p=5,q=1 → 3   |   p=5,q=4 → 5
    //   Approaches: ① collect ancestor paths O(n)/O(n)  ② single-pass recursive returns O(n)/O(h) ★  ③ parent-ptr + set
    //   🚩 Red flag: assuming it's a BST — if so use LC 235 (different, simpler algo).
    //   ✨ Strong hire: 4-line recursive solution; explain WHY "both sides non-null at this node" → this IS the LCA.
    //   Follow-ups: LC 235 (BST), LC 1650 (with parent ptrs), LC 1644 (nodes may not exist), LC 1123 (deepest leaves LCA).
    // --- LC #236: Lowest Common Ancestor (Medium) — DFS Recursive ---
    // GOAL: Find the lowest (deepest) node that has both p and q as
    //       descendants (a node is a descendant of itself).
    //
    // INTUITION: If the current node IS p or q, it is the LCA (because the
    //   other must be in its subtree). If p is found in the left subtree and
    //   q in the right (or vice versa), the current node is the LCA.
    //
    // STEPS:
    //   lca(node, p, q):
    //     if node == null or node == p or node == q: return node
    //     left  = lca(node.left,  p, q)
    //     right = lca(node.right, p, q)
    //     if left != null && right != null: return node  ← split here
    //     return left ?? right
    //
    // WHY IT WORKS: A non-null return bubbles up the found node. When both
    //   children return non-null, the current node is the fork point = LCA.
    //
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

    // 🎤 GOOGLE DEMO — HARD onsite; tests system-design thinking alongside coding.
    //   Q: "Serialize and deserialize a binary tree (round-trip exactly)."
    //   Ex: [1,2,3,null,null,4,5] → string → same tree
    //   Approaches: ① BFS w/ "null" sentinels O(n)/O(n) ★  ② preorder DFS w/ '#' sentinels O(n)/O(n) ★  ③ LC 449 BST-only (compact)
    //   🚩 Red flag: no delimiters — "10" and "1","0" become ambiguous.
    //   ✨ Strong hire: discuss STREAMING + chunked encoding for very large trees; mention length-prefix to avoid escaping.
    //   Follow-ups: LC 449 (BST — shorter encoding), LC 428 (N-ary tree), LC 652 (find duplicate subtrees — uses serialization!).
    // --- LC #297: Serialize and Deserialize Binary Tree (Hard) — BFS ---
    // GOAL: Convert a binary tree to a string and reconstruct it exactly.
    //
    // INTUITION: BFS level-order naturally preserves parent→child relationships.
    //   Serialize with "null" placeholders for missing children so the child
    //   positions are unambiguous. Deserialize by replaying the BFS queue.
    //
    // SERIALIZE STEPS:
    //   BFS; append node.val or "null" for each dequeued slot.
    //   Join with commas.
    //
    // DESERIALIZE STEPS:
    //   Split by comma. Root = vals[0]. Queue root.
    //   i = 1. While queue non-empty:
    //     dequeue node.
    //     vals[i] != "null" → node.left  = new node; enqueue it. i++
    //     vals[i] != "null" → node.right = new node; enqueue it. i++
    //
    // WHY IT WORKS: The BFS queue during deserialization mirrors the BFS order
    //   of serialization, so child indices always align with parent slots.
    //
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

    // 🎤 GOOGLE DEMO — HARD onsite; canonical "return-value + side-effect" pattern.
    //   Q: "Max sum of ANY path (nodes connected by edges, used once, need not pass through root)."
    //   Ex: [-10,9,20,null,null,15,7] → 42  (15→20→7)  |  [1,2,3] → 6
    //   Approaches: ① try every (u,v) pair via LCA O(n²)  ② DFS post-order, return one-arm, update global w/ two-arm O(n)/O(h) ★
    //   🚩 Red flag: returning the two-arm value to the parent — a path can't use both arms then branch up.
    //   ✨ Strong hire: clamp negative subtree contributions to 0; state the dual-quantity invariant clearly.
    //   Follow-ups: LC 543 (diameter — same pattern), LC 687 (longest same-value path), LC 1373 (max BST sum).
    // --- LC #124: Binary Tree Maximum Path Sum (Hard) — DFS Post-order ---
    // GOAL: Find the maximum sum of any path in the tree. A path can start
    //       and end at any node; it need not pass through the root.
    //
    // INTUITION: For each node, the best path THROUGH it uses its value plus
    //   optionally the best arm from the left and/or right child. The "arm"
    //   gain from a child is max(0, dfs(child)) — never take a negative arm.
    //   We update a global max at every node, but return only ONE arm (left or
    //   right) upward, since a parent can extend the path in only one direction.
    //
    // STEPS:
    //   dfs(node):
    //     if null: return 0
    //     left  = max(0, dfs(node.left))
    //     right = max(0, dfs(node.right))
    //     globalMax = max(globalMax, left + right + node.val)  ← full path here
    //     return node.val + max(left, right)                   ← one arm up
    //
    // WHY IT WORKS: Every possible path passes through exactly one "top" node
    //   where both arms meet. The update at each node captures that candidate.
    //
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

    // 🎤 GOOGLE DEMO — quick warm-up; the famous "Max Howell / Homebrew" question.
    //   Q: "Invert a binary tree (mirror left/right)."
    //   Ex: [4,2,7,1,3,6,9] → [4,7,2,9,6,3,1]
    //   Approaches: ① recursive swap O(n)/O(h) ★  ② iterative BFS w/ queue O(n)/O(w)
    //   🚩 Red flag: swapping in-place AFTER recursing on right — mixes up which subtree gets inverted.
    //   ✨ Strong hire: write both versions; mention tuple-swap `(l, r) = (r, l)` reads cleanly in C#.
    //   Follow-ups: LC 101 (symmetric tree — invert + compare), LC 951 (flip-equivalent), LC 814 (binary tree pruning).
    // --- LC #226: Invert Binary Tree (Easy) — Recursive Swap ---
    // GOAL: Mirror a binary tree so left/right children are swapped at every node.
    //
    // INTUITION: At each node, swap its children, then recurse on both. Pure
    //   structural recursion: a beginner-perfect demonstration of DFS.
    //
    // Time: O(n) | Space: O(h) recursion (worst h = n for skewed tree)
    public class InvertBinaryTree
    {
        public TreeNode? Invert(TreeNode? root)
        {
            if (root == null) return null;                      // empty subtree → stay empty
            // Swap children FIRST, then recurse — order doesn't matter here.
            (root.left, root.right) = (Invert(root.right), Invert(root.left));
            return root;                                        // return current node so parent can link
        }
    }

    // 🎤 GOOGLE DEMO — L3 onsite; tests "return one thing, update another" pattern.
    //   Q: "Length (#edges) of the DIAMETER — longest path between ANY two nodes."
    //   Ex: [1,2,3,4,5] → 3  (4→2→1→3)
    //   Approaches: ① brute LCA-pair O(n²)  ② DFS returns HEIGHT, global tracks max(left+right) O(n)/O(h) ★
    //   🚩 Red flag: returning diameter from DFS instead of height — breaks composition with parent.
    //   ✨ Strong hire: distinguish "diameter measured in edges vs nodes" — problem-dependent.
    //   Follow-ups: LC 124 (max path SUM), LC 687 (longest same-value), LC 1245 (tree diameter for general graph).
    // --- LC #543: Diameter of Binary Tree (Easy) — DFS Returning Height ---
    // GOAL: Length (in edges) of the longest path between any two nodes.
    //       Path may or may not pass through the root.
    //
    // INTUITION: The longest path THROUGH a node = left height + right height.
    //   Do DFS; at each node compute heights of children, update global max
    //   with the through-node candidate, return the node's own height.
    //
    // Time: O(n) | Space: O(h)
    public class DiameterOfBinaryTree
    {
        private int _best;                                      // longest path (in edges) seen so far

        public int Diameter(TreeNode? root)
        {
            _best = 0;                                          // reset for repeated calls
            Height(root);                                       // populate _best via side effect
            return _best;
        }

        // Returns height in NODES of subtree rooted at node.
        private int Height(TreeNode? node)
        {
            if (node == null) return 0;                         // empty subtree has height 0
            int l = Height(node.left);                          // recurse left
            int r = Height(node.right);                         // recurse right
            _best = Math.Max(_best, l + r);                     // candidate diameter through this node
            return 1 + Math.Max(l, r);                          // height of this subtree
        }
    }

    // 🎤 GOOGLE DEMO — L4 onsite; tests BST property awareness + iterative inorder.
    //   Q: "Return the kth SMALLEST value (1-indexed) in a BST."
    //   Ex: root=[3,1,4,null,2], k=1 → 1   |   [5,3,6,2,4,null,null,1], k=3 → 3
    //   Approaches: ① full inorder + index O(n)/O(n)  ② iterative inorder, early-stop at k O(h+k)/O(h) ★  ③ augmented BST (subtree sizes) O(h)
    //   🚩 Red flag: recursive inorder that can't early-stop — wastes work past k.
    //   ✨ Strong hire: bring up the "FREQUENTLY MODIFIED BST" follow-up: "augment nodes w/ subtree size for O(h)".
    //   Follow-ups: LC 235 (LCA in BST), LC 173 (BST iterator), LC 700 (search BST), LC 270 (closest val).
    // --- LC #230: Kth Smallest Element in a BST (Medium) — Iterative Inorder ---
    // GOAL: Return the k-th smallest value in a BST (1-indexed).
    //
    // INTUITION: Inorder traversal of a BST yields values in sorted order.
    //   Walk inorder with an explicit stack; the k-th popped node is the answer.
    //   Stop early without traversing the whole tree.
    //
    // Time: O(h + k) | Space: O(h)
    public class KthSmallestInBST
    {
        public int KthSmallest(TreeNode? root, int k)
        {
            var stack = new Stack<TreeNode>();                  // holds ancestors waiting to be visited
            var cur = root;
            while (cur != null || stack.Count > 0)
            {
                // Dive as far LEFT as possible, pushing each node on the way.
                while (cur != null) { stack.Push(cur); cur = cur.left; }
                cur = stack.Pop();                              // smallest unvisited node
                if (--k == 0) return cur.val;                   // k-th smallest found
                cur = cur.right;                                // then explore right subtree
            }
            return -1;                                          // unreachable for valid input
        }
    }

    // 🎤 GOOGLE DEMO — phone-screen freebie that builds confidence; pure structural recursion.
    //   Q: "Are two binary trees IDENTICAL (same structure AND values)?"
    //   Ex: [1,2,3], [1,2,3] → true  |  [1,2], [1,null,2] → false
    //   Approaches: ① recursive structural compare O(n)/O(h) ★  ② iterative w/ paired stacks O(n)/O(h)
    //   🚩 Red flag: only checking values without first verifying both nodes null/non-null — NPE.
    //   ✨ Strong hire: short-circuit: `p==null && q==null → true`; `p==null || q==null → false`; then val + recurse.
    //   Follow-ups: LC 101 (symmetric — mirrored same tree), LC 572 (subtree — uses SameTree as helper), LC 951.
    // --- LC #100: Same Tree (Easy) — Structural Recursion ---
    // GOAL: Determine whether two binary trees are structurally identical AND
    //       have the same node values.
    //
    // INTUITION: Two trees are equal iff their roots match AND their left
    //   subtrees match AND their right subtrees match. Pure recursion.
    //
    // Time: O(n) | Space: O(h)
    public class SameTree
    {
        public bool IsSameTree(TreeNode? p, TreeNode? q)
        {
            if (p == null && q == null) return true;            // both empty → trivially equal
            if (p == null || q == null) return false;           // only one empty → different shape
            if (p.val != q.val) return false;                   // values must match at this node
            return IsSameTree(p.left, q.left)                   // left subtrees match
                && IsSameTree(p.right, q.right);                // and right subtrees match
        }
    }
}
