# Pattern Recognition Cheatsheet

**Use this 90 seconds into the interview.** After the interviewer states the problem, scan the keyword column → jump to the pattern → propose the named approach out loud.

---

## 1. Keyword → Pattern Lookup

| Keyword / clue in the prompt | Pattern | Default first approach |
|---|---|---|
| "sorted array" + find pair / target | Two Pointers | left/right pointers |
| "subarray" / "substring" + size or sum constraint | Sliding Window | expand-shrink window |
| "kth largest / smallest / closest" | Heap (or Quickselect) | size-k heap of opposite polarity |
| "top K frequent" | Heap + HashMap | freq map → size-k min-heap |
| "max / min in window of size k" | Monotonic Deque | LC 239 template |
| "next greater / smaller element" | Monotonic Stack | LC 496 / 739 |
| "previous smaller" + rectangle area | Monotonic Stack | LC 84 / 85 |
| "linked list cycle / middle / palindrome" | Fast & Slow Pointers | Floyd's tortoise & hare |
| "reverse linked list" / "reorder list" | Pointer Manipulation | iterative prev/curr/next |
| "merge k sorted X" | Min-Heap or D&C | LC 23 |
| "median of stream" | Two Heaps | max-low + min-high |
| "balanced parens" / "valid expression" | Stack | LC 20, 150 |
| "binary tree level / zigzag" | BFS w/ queue | level loop |
| "binary tree path / sum / depth" | DFS recursion | post-order pattern |
| "tree LCA / serialize / diameter" | DFS w/ post-order returning info | LC 236 / 297 / 543 |
| "validate BST" / "kth in BST" | In-order traversal | LC 98 / 230 |
| "count islands" / "regions" / "flood fill" | DFS or BFS on grid | LC 200 |
| "shortest path in unweighted graph/grid" | BFS | LC 994, 127 |
| "shortest path with weights ≥ 0" | Dijkstra | min-heap of (dist, node) |
| "shortest path with negative weights" | Bellman-Ford | n-1 relax passes |
| "course schedule" / "build order" | Topological Sort | Kahn (BFS) or DFS color |
| "connected components" / "redundant edge" | Union-Find | path compression + rank |
| "word ladder" / "minimum transformations" | BFS on implicit graph | LC 127 |
| "clone graph" / "deep copy" | DFS + HashMap (old→new) | LC 133 |
| "fewest coins / steps" | DP (unbounded knapsack) or BFS | LC 322 |
| "max sum subarray" | Kadane | running sum, reset if < 0 |
| "max profit" + 1 / k transactions | DP w/ states | LC 121 / 188 |
| "longest increasing X" / "LIS" | DP O(n²) or Patience sort O(n log n) | LC 300 |
| "longest common X" / "edit distance" | 2D string DP | LC 1143 / 72 |
| "partition into equal X" | 0/1 Knapsack on sum/2 | LC 416 |
| "ways to decode / climb / unique paths" | 1D / 2D DP counting | LC 91 / 70 / 62 |
| "house robber" / "non-adjacent" | include/skip DP | LC 198 |
| "largest square / rectangle of 1's" | DP on bottom-right | LC 221 |
| "find all subsets / permutations / combinations" | Backtracking | include-exclude / used[] |
| "all valid X with constraint" | Backtracking + pruning | LC 51 / 39 / 22 |
| "word search on grid" + dictionary | Trie + DFS backtracking | LC 212 |
| "prefix search" / "autocomplete" | Trie | LC 208 |
| "dot wildcard search" | Trie + DFS branching | LC 211 |
| "O(1) insert / delete / random" | List + Dict (swap-with-last) | LC 380 |
| "LRU / LFU cache" | HashMap + DLL | LC 146 / 460 |
| "design rate limiter / counter / log system" | Sliding window + Queue/Map | system-design adjacent |
| "find anagrams / group anagrams" | HashMap of sorted-key or 26-tuple | LC 49 / 438 |
| "two-sum-like" | HashMap of complement | LC 1 |
| "longest consecutive sequence" | HashSet + start-of-run | LC 128 |

---

## 2. Constraint-Size → Allowed Complexity

| n (input size) | Allowed time | Strategies that fit |
|---|---|---|
| n ≤ 10 | O(n!) / O(2ⁿ · n) | Brute backtracking, permutations |
| n ≤ 20 | O(2ⁿ) | Bitmask DP, subset enumeration |
| n ≤ 100 | O(n³) / O(n⁴) | Floyd-Warshall, 3D DP |
| n ≤ 1,000 | O(n²) | Standard DP, two nested loops |
| n ≤ 10⁵ | O(n log n) | Sort, heap, binary search, segment tree |
| n ≤ 10⁶ | O(n) / O(n log log n) | Hash, two pointers, sliding window, sieve |
| n ≤ 10⁹ | O(log n) / O(√n) | Binary search the answer, math |

**Rule of thumb at Google interviews:** if interviewer says "n up to 10⁵" and you propose O(n²), they will push back. Pre-state the target complexity before coding.

---

## 3. Data Structure Selection by Operation Mix

| You need... | Best structure | Why |
|---|---|---|
| O(1) lookup by key | `Dictionary<K,V>` | hash |
| O(1) lookup + insertion-order iteration | `LinkedHashMap` (or Dict + List) | LRU cache foundation |
| O(1) lookup + ordered iteration | `SortedDictionary` | red-black tree, O(log n) ops actually |
| O(1) random element | `List<T>` + `Dictionary` (swap-trick) | LC 380 |
| Min/max retrieval O(1), insert O(log n) | `PriorityQueue<T,P>` | heap |
| Two-ended O(1) push/pop | `LinkedList<T>` or `Deque` | monotonic deque |
| Prefix / wildcard string queries | `Trie` | branching on chars |
| Range sum / range update | `Prefix Sum` / `Segment Tree` / `BIT` | depends on mutability |
| Disjoint sets / connectivity | `Union-Find` | α(n) ≈ O(1) |

---

## 4. Approach Decision: Sort vs Heap vs Quickselect

| Situation | Best | Why |
|---|---|---|
| Need full sorted output | **Sort** O(n log n) | simplest |
| Need just top/bottom k, k ≪ n | **Heap of size k** O(n log k) | streaming-friendly |
| Need just the k-th element, one-shot | **Quickselect** O(n) avg | fastest in practice |
| Stream of unknown length | **Heap** | sort impossible |
| Memory tight, big n | **Heap of size k** | O(k) memory |

---

## 5. DP vs Greedy vs Backtracking — Quick Disambiguation

```
Optimal substructure + overlapping subproblems  → DP
Optimal substructure + greedy-choice property   → Greedy  (PROVE it!)
Enumerate all solutions / count exact set       → Backtracking
Count / find optimal under constraints + small n → Backtracking + memo (= DP)
```

**Greedy red flag:** if you can't construct a 1-paragraph proof of why the greedy choice is safe, default to DP. Examples where greedy FAILS but looks right:
- Coin Change with `[1,3,4]`, amount 6 (greedy 3, DP 2)
- Jump Game II — always picking the farthest reachable cell is *correct* (BFS-layer argument), but novices try "always take the longest jump now" which is wrong
- 0/1 Knapsack — value/weight ratio greedy fails

---

## 6. Common Templates (memorize these — they answer 70% of LC mediums)

```csharp
// Sliding Window
int l = 0; for (int r = 0; r < n; r++) {
    /* expand: include s[r] */
    while (/* invariant violated */) { /* shrink: exclude s[l++] */ }
    /* record answer at this window */
}

// BFS on grid
var q = new Queue<(int r,int c)>(); q.Enqueue(start); visited.Add(start);
int steps = 0;
while (q.Count > 0) {
    int sz = q.Count;
    for (int i = 0; i < sz; i++) {
        var (r,c) = q.Dequeue();
        if (target) return steps;
        foreach (var (dr,dc) in dirs) { /* enqueue neighbor if valid + unseen */ }
    }
    steps++;
}

// Backtracking
void Backtrack(state, path) {
    if (isGoal(state)) { res.Add(new(path)); return; }
    foreach (var choice in choices(state)) {
        path.Add(choice); /* apply */
        Backtrack(next(state, choice), path);
        path.RemoveAt(path.Count - 1); /* undo */
    }
}

// Binary search the answer (when output is monotonic in candidate)
int lo = minPossible, hi = maxPossible;
while (lo < hi) {
    int mid = lo + (hi - lo) / 2;
    if (canAchieve(mid)) hi = mid; else lo = mid + 1;
}
return lo;

// Union-Find
int Find(int x) => parent[x] == x ? x : parent[x] = Find(parent[x]);
void Union(int a, int b) {
    int ra = Find(a), rb = Find(b);
    if (ra == rb) return;
    if (rank[ra] < rank[rb]) (ra, rb) = (rb, ra);
    parent[rb] = ra; if (rank[ra] == rank[rb]) rank[ra]++;
}

// Monotonic stack — next greater
var st = new Stack<int>();  // indices, decreasing values
for (int i = 0; i < n; i++) {
    while (st.Count > 0 && nums[st.Peek()] < nums[i]) {
        int j = st.Pop(); res[j] = nums[i];
    }
    st.Push(i);
}
```

---

## 7. What Interviewers at Google Score On

| Signal | Hire | No Hire |
|---|---|---|
| Clarifies inputs / constraints BEFORE coding | ✅ | jumps straight to keyboard |
| States complexity target out loud | ✅ | silent |
| Names the pattern ("this is a sliding window") | ✅ | un-named ad-hoc code |
| Mentions trade-offs between 2+ approaches | ✅ | proposes only one |
| Walks through small example before coding | ✅ | hand-waves correctness |
| Self-catches bugs while writing | ✅ | waits for interviewer to point them out |
| Discusses edge cases (empty, 1 elem, overflow, dup) | ✅ | only handles happy path |
| Mentions follow-ups unprompted (LC X is the harder variant) | ✅ | finishes silently |

**Strong-hire move:** at the end, say "If you wanted to extend this to a stream / distributed setting / thread-safe version, I'd change X to Y." This 30 seconds converts hire → strong hire.
