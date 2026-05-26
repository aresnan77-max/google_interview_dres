# Algorithm Complexity Cheatsheet

## Big-O Quick Reference

| Complexity | Name | Example | Growth |
|-----------|------|---------|--------|
| O(1) | Constant | HashMap lookup, array index | Flat |
| O(log n) | Logarithmic | Binary search, BST lookup | Very slow |
| O(n) | Linear | Array scan, linked list traversal | Steady |
| O(n log n) | Linearithmic | Merge sort, heap sort | Moderate |
| O(n²) | Quadratic | Nested loops, bubble sort | Fast |
| O(2ⁿ) | Exponential | Subsets, recursive Fibonacci | Very fast |
| O(n!) | Factorial | Permutations, TSP brute force | Explosive |

## Data Structure Operations

| Structure | Access | Search | Insert | Delete | Space |
|-----------|--------|--------|--------|--------|-------|
| Array | O(1) | O(n) | O(n) | O(n) | O(n) |
| Linked List | O(n) | O(n) | O(1)* | O(1)* | O(n) |
| Stack/Queue | O(n) | O(n) | O(1) | O(1) | O(n) |
| Hash Table | — | O(1) avg | O(1) avg | O(1) avg | O(n) |
| BST (balanced) | O(log n) | O(log n) | O(log n) | O(log n) | O(n) |
| Heap | — | O(n) | O(log n) | O(log n) | O(n) |
| Trie | — | O(m) | O(m) | O(m) | O(n·m) |

*With reference to the node

## Sorting Algorithms

| Algorithm | Best | Average | Worst | Space | Stable | Notes |
|-----------|------|---------|-------|-------|--------|-------|
| Bubble Sort | O(n) | O(n²) | O(n²) | O(1) | Yes | Educational only |
| Insertion Sort | O(n) | O(n²) | O(n²) | O(1) | Yes | Good for small/nearly sorted |
| Merge Sort | O(n log n) | O(n log n) | O(n log n) | O(n) | Yes | Predictable, good for linked lists |
| Quick Sort | O(n log n) | O(n log n) | O(n²) | O(log n) | No | Fastest in practice |
| Heap Sort | O(n log n) | O(n log n) | O(n log n) | O(1) | No | In-place, not cache-friendly |
| Counting Sort | O(n+k) | O(n+k) | O(n+k) | O(k) | Yes | Integers only, k = range |
| Radix Sort | O(n·d) | O(n·d) | O(n·d) | O(n+k) | Yes | d = digits, k = base |

## Common Pattern Complexities

| Pattern | Time | Space | Examples |
|---------|------|-------|----------|
| Two Pointers | O(n) | O(1) | 3Sum, TrappingRainWater |
| Sliding Window | O(n) | O(k) | SlidingWindowMax |
| Binary Search | O(log n) | O(1) | LIS optimal, search sorted |
| BFS/DFS on graph | O(V+E) | O(V) | Islands, CourseSchedule |
| BFS on tree | O(n) | O(w) | LevelOrder (w=width) |
| DP (1D) | O(n) | O(1)* | ClimbStairs, HouseRobber |
| DP (2D) | O(n·m) | O(n·m) | UniquePaths, EditDistance |
| Backtracking | O(2ⁿ) or O(n!) | O(n) | Subsets, Permutations |
| Topological Sort | O(V+E) | O(V) | CourseSchedule |
| Union-Find | O(α(n))≈O(1) | O(n) | ConnectedComponents |

*With space optimization
