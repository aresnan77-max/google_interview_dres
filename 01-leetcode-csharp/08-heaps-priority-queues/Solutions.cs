// ============================================================================
// Category: Heaps & Priority Queues — Google Interview Prep
// Problems: KthLargestElement(#215), MergeKSortedLists(#23),
//           FindMedianFromDataStream(#295), MinHeap(custom),
//           KClosestPointsToOrigin(#973), TaskScheduler(#621)
// ============================================================================

using System;
using System.Collections.Generic;

namespace GoogleInterviewPrep.LeetCode
{
    // 🎤 GOOGLE DEMO — L4 onsite favorite; tests algorithm trade-offs.
    //   Q: "Kth LARGEST element in an unsorted array. Can you avoid full sorting?"
    //   Ex: [3,2,1,5,6,4], k=2 → 5   |   [3,2,3,1,2,4,5,5,6], k=4 → 4
    //   Approaches: ① sort O(n log n)/O(1)  ② min-heap size k O(n log k)/O(k) ★ (streaming-friendly)  ③ quickselect avg O(n) / worst O(n²) ★ (fastest)
    //   🚩 Red flag: jumping to sort without discussing trade-offs OR forgetting quickselect's worst case.
    //   ✨ Strong hire: name median-of-medians for worst-case O(n); mention introselect.
    //   Follow-ups: LC 703 (Kth largest in STREAM), LC 347 (top-K frequent), LC 692.
    // --- LC #215: Kth Largest Element (Medium) ---
    //
    // APPROACH A — Min Heap of size k:
    // GOAL: Find the k-th largest element without full sorting.
    // INTUITION: Maintain a min-heap of the k largest elements seen so far.
    //   Its root is the smallest of those k elements = the k-th largest overall.
    //   When the heap grows past k, pop the minimum (it's too small to qualify).
    // STEPS:
    //   For each num: push to heap.
    //   If heap.Count > k: pop (remove current minimum).
    //   Return heap.Peek().
    // Time: O(n log k) | Space: O(k)
    //
    // APPROACH B — QuickSelect:
    // INTUITION: Partition like QuickSort. After partition, the pivot is in
    //   its final sorted position. If that position equals target = n-k, done.
    //   Otherwise recurse on only one side — average O(n).
    // STEPS:
    //   target = n - k  (0-indexed position of k-th largest from left).
    //   QuickSelect(lo, hi):
    //     Partition around pivot = nums[hi].
    //     pivot lands at index `store`.
    //     if store == target: return nums[store]
    //     if store < target: recurse right half
    //     else: recurse left half
    // Time: O(n) average, O(n²) worst | Space: O(log n) avg
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

    // 🎤 GOOGLE DEMO — HARD onsite; this pattern shows up in MapReduce / log merging.
    //   Q: "Merge k sorted linked lists into one sorted list."
    //   Ex: [[1,4,5],[1,3,4],[2,6]] → [1,1,2,3,4,4,5,6]
    //   Approaches: ① collect+sort O(N log N)/O(N)  ② min-heap of (val, list) O(N log k)/O(k) ★  ③ divide-and-conquer pairwise merge O(N log k)/O(1)
    //   🚩 Red flag: proposing sequential pairwise merge — O(N·k), the trap.
    //   ✨ Strong hire: explain why heap and D&C are SAME complexity; pick D&C for low memory streams.
    //   Follow-ups: LC 21 (merge TWO lists), LC 88 (merge sorted arrays), LC 632 (smallest range covering k lists).
    // --- LC #23: Merge K Sorted Lists (Hard) — Min Heap ---
    // GOAL: Merge k sorted linked lists into one sorted linked list.
    //
    // INTUITION: At any point the next node in the merged list is the minimum
    //   among the current heads of all k lists. A min-heap gives us that
    //   minimum in O(log k) time. After extracting a node, push its successor.
    //
    // STEPS:
    //   Push the head of each non-null list into a min-heap (keyed by val).
    //   While heap non-empty:
    //     Extract min node; append to result.
    //     If node.next != null: push node.next.
    //   Return dummy.next.
    //
    // WHY IT WORKS: The heap always holds exactly one "candidate" per list
    //   (its current front), so we always pick the globally smallest next node.
    //   Total work: N nodes extracted, each extraction O(log k) → O(N log k).
    //
    // Time: O(N log k)  N = total nodes | Space: O(k)
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

    // 🎤 GOOGLE DEMO — HARD onsite; the TWO-HEAP design pattern.
    //   Q: "addNum(num) + findMedian() on a stream. Even count → avg of two middles."
    //   Ex: add 1,2 → 1.5; add 3 → 2.0
    //   Approaches: ① sorted insert O(n) add  ② BST O(log n) add  ③ TWO HEAPS: max-heap (low) + min-heap (high), kept balanced O(log n) add / O(1) find ★
    //   🚩 Red flag: forgetting the rebalance step — sizes drift and median becomes wrong.
    //   ✨ Strong hire: state the size invariant explicitly; mention buckets if range bounded (99% in [0,100]).
    //   Follow-ups: bounded-range bucket counting, sliding-window median (LC 480), LC 4 (median of 2 sorted).
    // --- LC #295: Find Median from Data Stream (Hard) — Two Heaps ---
    // GOAL: Support AddNum(int) and FindMedian() on a growing stream of numbers.
    //
    // INTUITION: Maintain two heaps that partition the data at the median:
    //   _maxHeap: the smaller half  (max-heap → its root = largest small number)
    //   _minHeap: the larger half   (min-heap → its root = smallest large number)
    //   Keep sizes balanced: |maxHeap| == |minHeap| or |maxHeap| == |minHeap|+1.
    //
    // ADD STEPS:
    //   1. Push to _maxHeap.
    //   2. Move _maxHeap.top to _minHeap (ensures ordering).
    //   3. If _minHeap is larger, move its top back to _maxHeap.
    //
    // FIND MEDIAN:
    //   Odd total  → _maxHeap.top
    //   Even total → (_maxHeap.top + _minHeap.top) / 2.0
    //
    // WHY IT WORKS: After every AddNum the invariant guarantees the two roots
    //   are the two middle values (or the single middle for odd count).
    //
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
    // GOAL: A generic min-heap backed by a dynamic array, supporting
    //       Insert, ExtractMin, and Peek.
    //
    // STRUCTURE: Array-based binary heap. For node at index i:
    //   parent = (i - 1) / 2
    //   left   = 2i + 1
    //   right  = 2i + 2
    //   Invariant: parent ≤ both children (min-heap property).
    //
    // INSERT: Append at end; SiftUp to restore invariant.
    //   SiftUp: while node < parent, swap and move up.
    //
    // EXTRACT MIN: Swap root with last element; remove last;
    //   SiftDown to restore invariant.
    //   SiftDown: while node > either child, swap with the smaller child.
    //
    // BUILD FROM ARRAY (Heapify): Start from last internal node, SiftDown
    //   each node. O(n) — faster than n individual inserts.
    //
    // Time: O(log n) insert/extract, O(1) peek, O(n) build | Space: O(n)
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

    // 🎤 GOOGLE DEMO — L4 favorite; tests heap-orientation choice.
    //   Q: "K points closest to ORIGIN by Euclidean distance."
    //   Ex: [[1,3],[-2,2]], k=1 → [[-2,2]]
    //   Approaches: ① sort by dist O(n log n)  ② MAX-heap size k O(n log k)/O(k) ★  ③ quickselect avg O(n) ★
    //   🚩 Red flag: computing sqrt — unnecessary; compare squared distances.
    //   ✨ Strong hire: explicitly use MAX-heap (drop the farthest) instead of min-heap of all n.
    //   Follow-ups: LC 658 (k closest in SORTED array — binary search), LC 692, streaming variant w/ insertions.
    // --- LC #973: K Closest Points to Origin (Medium) — Max-Heap of Size K ---
    // GOAL: Return the K points closest to (0,0) from a list of 2D points.
    //
    // INTUITION: Keep a MAX-heap of size K keyed by squared distance.
    //   For each point, if heap size < K push; else if its distance < heap top,
    //   pop top and push new point. At end the heap holds the K closest.
    //   (We compare squared distances to avoid the sqrt call.)
    //
    // Time: O(n log k) | Space: O(k)
    public class KClosestPointsToOrigin
    {
        public int[][] KClosest(int[][] points, int k)
        {
            // C# PriorityQueue is a MIN-heap by priority. To make a max-heap on
            // distance, store NEGATIVE distance as priority — largest distance
            // becomes the smallest (most-negative) priority → sits at the top.
            var pq = new PriorityQueue<int[], int>();
            foreach (var p in points)
            {
                int dist = p[0] * p[0] + p[1] * p[1];              // squared distance to origin
                pq.Enqueue(p, -dist);                              // negate → farthest sits on top
                if (pq.Count > k) pq.Dequeue();                    // drop farthest when over capacity
            }

            var res = new int[k][];                                // pull k closest into result array
            for (int i = 0; i < k; i++) res[i] = pq.Dequeue();
            return res;
        }
    }

    // 🎤 GOOGLE DEMO — L4 onsite; tests recognizing CLOSED-FORM math beats simulation.
    //   Q: "Tasks A-Z, same task must be ≥ n apart. Min total time (idles allowed)."
    //   Ex: ['A','A','A','B','B','B'], n=2 → 8 (A B _ A B _ A B)   |   n=0 → 6
    //   Approaches: ① PQ simulation O(N log 26)  ② greedy formula max(N, (maxFreq−1)·(n+1) + tieCount) O(N) ★
    //   🚩 Red flag: skipping the formula and only simulating — you miss the insight signal.
    //   ✨ Strong hire: derive formula by drawing the schedule grid w/ maxFreq as anchor.
    //   Follow-ups: reconstruct the ACTUAL schedule, LC 1834 (single CPU), LC 358 (rearrange string k apart).
    // --- LC #621: Task Scheduler (Medium) — Greedy / Frequency Math ---
    // GOAL: Given task labels and a cooldown n (same task must be ≥ n apart),
    //       return the minimum number of time units to finish all tasks.
    //
    // INTUITION: The task with the highest frequency F dominates. Imagine
    //   placing F-1 "frames" of size (n+1), each starting with a max-freq
    //   task; then append the remaining max-freq tasks at the end.
    //   Formula: max(tasks.Length, (F-1)*(n+1) + tieCount)
    //   where tieCount = how many tasks share the top frequency.
    //
    // Time: O(n) | Space: O(1)  (26 letters)
    public class TaskScheduler
    {
        public int LeastInterval(char[] tasks, int n)
        {
            var freq = new int[26];                                // counts per uppercase letter
            foreach (char t in tasks) freq[t - 'A']++;

            int maxFreq = 0;
            foreach (int f in freq) if (f > maxFreq) maxFreq = f;  // highest count among all tasks

            int tieCount = 0;                                      // how many tasks share that max
            foreach (int f in freq) if (f == maxFreq) tieCount++;

            // Either the structured schedule dominates, or idle time isn't needed.
            int slots = (maxFreq - 1) * (n + 1) + tieCount;
            return Math.Max(tasks.Length, slots);
        }
    }
}
