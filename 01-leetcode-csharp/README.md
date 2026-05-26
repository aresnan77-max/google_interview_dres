# LeetCode C# Solutions — Problem Index & Progress Tracker

## Recommended Solving Order

Follow the Study Plan daily schedule, or use this priority order:

### Priority 1 — Must Know (Google Essentials)
- [ ] TwoSum (#1) → HashMap pattern
- [ ] MergeIntervals (#56) → Sort + Merge
- [ ] ValidateBST (#98) → DFS with Bounds
- [ ] NumberOfIslands (#200) → DFS Grid
- [ ] CourseSchedule (#207) → Topological Sort
- [ ] CoinChange (#322) → DP Knapsack
- [ ] LRUCache (#146) → HashMap + DLL

### Priority 2 — Highly Frequent
- [ ] 3Sum (#15) → Two Pointers
- [ ] TrappingRainWater (#42) → Two Pointers
- [ ] GroupAnagrams (#49) → Sort Key
- [ ] MaxDepthBinaryTree (#104) → DFS
- [ ] BinaryTreeLevelOrder (#102) → BFS
- [ ] WordBreak (#139) → DP + Set
- [ ] SerializeDeserializeBT (#297) → BFS String
- [ ] ImplementTrie (#208) → Prefix Tree

### Priority 3 — Important Patterns
- [ ] All remaining problems below

---

## Complete Problem Index

| # | Problem | LC# | Diff | Pattern | File |
|---|---------|-----|------|---------|------|
| | **Arrays & Strings** | | | | `01-arrays-strings/` |
| 1 | Two Sum | 1 | Easy | HashMap | Solutions.cs |
| 2 | Best Time to Buy/Sell Stock | 121 | Easy | Single Pass | Solutions.cs |
| 3 | Maximum Subarray | 53 | Med | Kadane's Algorithm | Solutions.cs |
| 4 | Product of Array Except Self | 238 | Med | Prefix/Suffix | Solutions.cs |
| 5 | Merge Intervals | 56 | Med | Sort + Merge | Solutions.cs |
| 6 | 3Sum | 15 | Med | Sort + Two Pointers | Solutions.cs |
| 7 | Trapping Rain Water | 42 | Hard | Two Pointers | Solutions.cs |
| 8 | Sliding Window Maximum | 239 | Hard | Monotonic Deque | Solutions.cs |
| | **HashMaps & Sets** | | | | `02-hashmaps-sets/` |
| 9 | Group Anagrams | 49 | Med | Sorted Key | Solutions.cs |
| 10 | Longest Consecutive Sequence | 128 | Med | HashSet | Solutions.cs |
| 11 | Top K Frequent Elements | 347 | Med | Bucket Sort | Solutions.cs |
| 12 | Valid Sudoku | 36 | Med | HashSet per Region | Solutions.cs |
| | **Linked Lists** | | | | `03-linked-lists/` |
| 13 | Reverse Linked List | 206 | Easy | Iterative Reversal | Solutions.cs |
| 14 | Merge Two Sorted Lists | 21 | Easy | Dummy Head | Solutions.cs |
| 15 | Linked List Cycle | 141 | Easy | Floyd's Algorithm | Solutions.cs |
| 16 | Remove Nth From End | 19 | Med | Two-Pointer Gap | Solutions.cs |
| | **Stacks & Queues** | | | | `04-stacks-queues/` |
| 17 | Valid Parentheses | 20 | Easy | Stack | Solutions.cs |
| 18 | Min Stack | 155 | Med | Auxiliary Stack | Solutions.cs |
| 19 | Daily Temperatures | 739 | Med | Monotonic Stack | Solutions.cs |
| 20 | Largest Rectangle in Histogram | 84 | Hard | Monotonic Stack | Solutions.cs |
| | **Trees** | | | | `05-trees/` |
| 21 | Maximum Depth of Binary Tree | 104 | Easy | DFS Recursive | Solutions.cs |
| 22 | Binary Tree Level Order | 102 | Med | BFS Queue | Solutions.cs |
| 23 | Validate BST | 98 | Med | DFS with Bounds | Solutions.cs |
| 24 | Lowest Common Ancestor | 236 | Med | DFS Recursive | Solutions.cs |
| 25 | Serialize/Deserialize BT | 297 | Hard | BFS + String | Solutions.cs |
| 26 | Binary Tree Max Path Sum | 124 | Hard | DFS Post-order | Solutions.cs |
| | **Graphs** | | | | `06-graphs/` |
| 27 | Number of Islands | 200 | Med | DFS/BFS Grid | Solutions.cs |
| 28 | Course Schedule | 207 | Med | Topological Sort | Solutions.cs |
| 29 | Pacific Atlantic Water Flow | 417 | Med | Multi-source DFS | Solutions.cs |
| 30 | Word Search | 79 | Med | DFS Backtracking | Solutions.cs |
| 31 | Connected Components | 323 | Med | Union-Find | Solutions.cs |
| | **Dynamic Programming** | | | | `07-dynamic-programming/` |
| 32 | Climbing Stairs | 70 | Easy | Fibonacci DP | Solutions.cs |
| 33 | House Robber | 198 | Med | 1D DP | Solutions.cs |
| 34 | Coin Change | 322 | Med | Unbounded Knapsack | Solutions.cs |
| 35 | Unique Paths | 62 | Med | 2D DP | Solutions.cs |
| 36 | Word Break | 139 | Med | DP + HashSet | Solutions.cs |
| 37 | Longest Increasing Subsequence | 300 | Med | Binary Search DP | Solutions.cs |
| 38 | Decode Ways | 91 | Med | 1D DP | Solutions.cs |
| | **Heaps & Priority Queues** | | | | `08-heaps-priority-queues/` |
| 39 | Kth Largest Element | 215 | Med | Min Heap / QuickSelect | Solutions.cs |
| 40 | Merge K Sorted Lists | 23 | Hard | Min Heap | Solutions.cs |
| 41 | Find Median from Data Stream | 295 | Hard | Two Heaps | Solutions.cs |
| 42 | MinHeap (Custom) | — | — | Array Binary Heap | Solutions.cs |
| | **Backtracking** | | | | `09-backtracking/` |
| 43 | Subsets | 78 | Med | Include/Exclude | Solutions.cs |
| 44 | Permutations | 46 | Med | Used Array | Solutions.cs |
| 45 | Combination Sum | 39 | Med | Pruning + Reuse | Solutions.cs |
| 46 | N-Queens | 51 | Hard | Constraint Backtrack | Solutions.cs |
| | **Design** | | | | `10-design/` |
| 47 | LRU Cache | 146 | Med | HashMap + DLL | Solutions.cs |
| 48 | Implement Trie | 208 | Med | Array Prefix Tree | Solutions.cs |

---

## Pattern Summary

| Pattern | Problems | Key Idea |
|---------|----------|----------|
| **Two Pointers** | 3Sum, TrappingRainWater | Converge from both ends |
| **Sliding Window** | SlidingWindowMax | Maintain window with deque |
| **HashMap Lookup** | TwoSum, GroupAnagrams | O(1) lookup for complement/key |
| **Monotonic Stack** | DailyTemps, LargestRect | Maintain increasing/decreasing order |
| **BFS** | LevelOrder, Islands | Queue-based level traversal |
| **DFS** | MaxDepth, ValidBST, Islands | Recursive/stack exploration |
| **Topological Sort** | CourseSchedule | Kahn's algorithm (in-degree) |
| **Union-Find** | ConnectedComponents | Path compression + union by rank |
| **Dynamic Programming** | CoinChange, WordBreak, LIS | State definition + recurrence |
| **Backtracking** | Subsets, Permutations, NQueens | Choose → Explore → Un-choose |
| **Two Heaps** | MedianFinder | Split data into halves |
| **HashMap + DLL** | LRU Cache | O(1) get + O(1) eviction |
