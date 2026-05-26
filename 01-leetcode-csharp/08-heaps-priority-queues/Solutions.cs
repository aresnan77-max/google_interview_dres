// ============================================================================
// Category: Heaps & Priority Queues — Google Interview Prep
// Problems: KthLargestElement(#215), MergeKSortedLists(#23),
//           FindMedianFromDataStream(#295), MinHeap(custom)
// ============================================================================

using System;
using System.Collections.Generic;

namespace GoogleInterviewPrep.LeetCode
{
    // --- LC #215: Kth Largest Element (Medium) — Min Heap of size k ---
    // Time: O(n log k) | Space: O(k)
    public class KthLargestElement
    {
        public int FindKthLargest_Heap(int[] nums, int k)
        {
            var minHeap = new PriorityQueue<int, int>();
            foreach (int num in nums)
            {
                minHeap.Enqueue(num, num);
                if (minHeap.Count > k) minHeap.Dequeue();
            }
            return minHeap.Peek();
        }

        public int FindKthLargest_QuickSelect(int[] nums, int k)
        {
            int target = nums.Length - k;
            return QuickSelect(nums, 0, nums.Length - 1, target);
        }
        private int QuickSelect(int[] nums, int lo, int hi, int target)
        {
            int pivot = nums[hi], store = lo;
            for (int i = lo; i < hi; i++)
                if (nums[i] < pivot) { (nums[store], nums[i]) = (nums[i], nums[store]); store++; }
            (nums[store], nums[hi]) = (nums[hi], nums[store]);
            if (store == target) return nums[store];
            return store < target ? QuickSelect(nums, store + 1, hi, target) : QuickSelect(nums, lo, store - 1, target);
        }
    }

    // --- LC #23: Merge K Sorted Lists (Hard) — Min Heap ---
    // Time: O(N log k) | Space: O(k)
    public class MergeKSortedLists
    {
        public ListNode? MergeKLists(ListNode?[] lists)
        {
            var minHeap = new PriorityQueue<ListNode, int>();
            foreach (var l in lists) if (l != null) minHeap.Enqueue(l, l.val);
            var dummy = new ListNode(-1);
            var cur = dummy;
            while (minHeap.Count > 0)
            {
                var node = minHeap.Dequeue();
                cur.next = node; cur = cur.next;
                if (node.next != null) minHeap.Enqueue(node.next, node.next.val);
            }
            return dummy.next;
        }
    }

    // --- LC #295: Find Median from Data Stream (Hard) — Two Heaps ---
    // Time: O(log n) add, O(1) find | Space: O(n)
    public class MedianFinder
    {
        private PriorityQueue<int, int> _maxHeap = new(); // smaller half (negated)
        private PriorityQueue<int, int> _minHeap = new(); // larger half
        public void AddNum(int num)
        {
            _maxHeap.Enqueue(num, -num);
            int val = _maxHeap.Dequeue();
            _minHeap.Enqueue(val, val);
            if (_minHeap.Count > _maxHeap.Count)
            {
                int v = _minHeap.Dequeue();
                _maxHeap.Enqueue(v, -v);
            }
        }
        public double FindMedian()
        {
            if (_maxHeap.Count > _minHeap.Count) { _maxHeap.TryPeek(out int v, out _); return v; }
            _maxHeap.TryPeek(out int a, out _); _minHeap.TryPeek(out int b, out _);
            return (a + b) / 2.0;
        }
    }

    // --- Custom: Min Heap Implementation ---
    // Time: O(log n) insert/extract, O(1) peek | Space: O(n)
    public class MinHeap
    {
        private readonly List<int> _h = new();
        public int Count => _h.Count;
        public MinHeap() { }
        public MinHeap(int[] arr) { _h = new List<int>(arr); for (int i = (_h.Count - 2) / 2; i >= 0; i--) SiftDown(i); }
        public void Insert(int val) { _h.Add(val); SiftUp(_h.Count - 1); }
        public int ExtractMin() { int min = _h[0]; _h[0] = _h[^1]; _h.RemoveAt(_h.Count - 1); if (_h.Count > 0) SiftDown(0); return min; }
        public int Peek() => _h[0];
        private void SiftUp(int i) { while (i > 0) { int p = (i-1)/2; if (_h[p] <= _h[i]) break; (_h[p],_h[i])=(_h[i],_h[p]); i=p; } }
        private void SiftDown(int i) { while (true) { int s=i,l=2*i+1,r=2*i+2; if (l<_h.Count&&_h[l]<_h[s]) s=l; if (r<_h.Count&&_h[r]<_h[s]) s=r; if (s==i) break; (_h[s],_h[i])=(_h[i],_h[s]); i=s; } }
    }
}
