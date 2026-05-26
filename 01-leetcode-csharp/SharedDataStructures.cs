// ============================================================================
// Shared Data Structures for LeetCode Problems
// ============================================================================

namespace GoogleInterviewPrep.LeetCode
{
    public class ListNode
    {
        public int val;
        public ListNode? next;
        public ListNode(int val = 0, ListNode? next = null) { this.val = val; this.next = next; }

        public static ListNode? FromArray(int[] values)
        {
            if (values.Length == 0) return null;
            var head = new ListNode(values[0]);
            var cur = head;
            for (int i = 1; i < values.Length; i++) { cur.next = new ListNode(values[i]); cur = cur.next; }
            return head;
        }

        public static int[] ToArray(ListNode? head)
        {
            var r = new List<int>();
            while (head != null) { r.Add(head.val); head = head.next; }
            return r.ToArray();
        }
    }

    public class TreeNode
    {
        public int val;
        public TreeNode? left;
        public TreeNode? right;
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        { this.val = val; this.left = left; this.right = right; }

        public static TreeNode? FromArray(int?[] values)
        {
            if (values.Length == 0 || values[0] == null) return null;
            var root = new TreeNode(values[0]!.Value);
            var queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            int i = 1;
            while (queue.Count > 0 && i < values.Length)
            {
                var cur = queue.Dequeue();
                if (i < values.Length && values[i] != null) { cur.left = new TreeNode(values[i]!.Value); queue.Enqueue(cur.left); }
                i++;
                if (i < values.Length && values[i] != null) { cur.right = new TreeNode(values[i]!.Value); queue.Enqueue(cur.right); }
                i++;
            }
            return root;
        }
    }
}
