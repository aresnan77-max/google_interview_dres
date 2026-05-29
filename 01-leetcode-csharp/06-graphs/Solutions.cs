// ============================================================================
// Category: Graphs — Google Interview Prep
// Problems: NumberOfIslands(#200), CloneGraph(#133), CourseSchedule(#207),
//           PacificAtlanticWaterFlow(#417), WordSearch(#79),
//           NumberOfConnectedComponents(#323), RottingOranges(#994),
//           RedundantConnection(#684)
// ============================================================================

using System;
using System.Collections.Generic;

namespace GoogleInterviewPrep.LeetCode
{
    // 🎤 GOOGLE DEMO — canonical "can you DFS a grid?" — very common at Google.
    //   Q: "Count islands in a binary grid. Adjacency = 4-directional only."
    //   Ex: 5x5 grid with two land clusters + a single cell → 3
    //   Approaches: ① DFS marking visited O(mn)/O(mn) stack ★  ② BFS O(mn)/O(min(m,n)) queue  ③ Union-Find O(mn·α)
    //   🚩 Red flag: re-visiting cells (no mark) — explodes complexity or infinite loops.
    //   ✨ Strong hire: ask "can I mutate?" up front; mutating to '0' avoids the O(mn) visited set.
    //   Follow-ups: LC 305 (ONLINE — Union-Find required), LC 695 (max area), LC 130 (surrounded regions), LC 463 (perimeter).
    // --- LC #200: Number of Islands (Medium) — DFS Grid ---
    // GOAL: Count distinct islands (groups of connected '1' cells) in a
    //       2-D grid of '1' (land) and '0' (water).
    //
    // INTUITION: Every time we find an unvisited '1', it's a new island.
    //   "Sink" it (flood-fill to '0') so we never count the same island twice.
    //
    // STEPS:
    //   For each cell (i, j):
    //     if grid[i][j] == '1':
    //       count++
    //       DFS/Sink: mark current cell '0', recurse on 4 neighbors.
    //   Return count.
    //
    // WHY IT WORKS: Flood-fill destroys an island the moment we count it, so
    //   subsequent scans skip all its cells. Each cell is visited at most twice.
    //
    // Time: O(m×n) | Space: O(m×n) worst-case stack depth
    public class NumberOfIslands
    {
        public int NumIslands(char[][] grid)
        {
            int count = 0, m = grid.Length, n = grid[0].Length;
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    if (grid[i][j] == '1') { count++; Sink(grid, i, j, m, n); }
            return count;
        }
        private void Sink(char[][] grid, int r, int c, int m, int n)
        {
            if (r < 0 || r >= m || c < 0 || c >= n || grid[r][c] != '1') return;
            grid[r][c] = '0';
            Sink(grid, r + 1, c, m, n); Sink(grid, r - 1, c, m, n);
            Sink(grid, r, c + 1, m, n); Sink(grid, r, c - 1, m, n);
        }
    }

    // 🎤 GOOGLE DEMO — L4 favorite; tests graph traversal + identity-tracking.
    //   Q: "Deep-copy a connected undirected graph (node = val + neighbors[])."
    //   Ex: 4-node ring → brand new 4-node ring, same structure.
    //   Approaches: ① DFS w/ HashMap<orig,clone> O(V+E)/O(V) ★  ② BFS w/ HashMap O(V+E)/O(V) ★
    //   🚩 Red flag: no visited map — infinite recursion on the very first cycle.
    //   ✨ Strong hire: clone-on-first-visit, then recurse to attach neighbors — handles self-loops naturally.
    //   Follow-ups: LC 138 (random pointer linked-list), LC 1485 (clone N-ary tree w/ pointer), LC 1490 (clone N-ary).
    // --- LC #133: Clone Graph (Medium) — DFS + HashMap ---
    // GOAL: Return a deep copy of a connected undirected graph.
    //       Each node has a val and a list of neighbors.
    //
    // INTUITION: Use a hash map (original → clone) as a visited set to avoid
    //   infinite loops on cycles. Before recursing into neighbors, create and
    //   register the clone immediately.
    //
    // STEPS:
    //   clone(node):
    //     if node == null: return null
    //     if node in visited: return visited[node]  ← already cloned
    //     create newNode(node.val); visited[node] = newNode
    //     for each neighbor n: newNode.neighbors.Add(clone(n))
    //     return newNode
    //
    // WHY IT WORKS: Storing the clone BEFORE recursing on neighbors breaks
    //   cycles — the second visit to any node returns the existing clone
    //   instead of creating a new one or looping forever.
    //
    // Time: O(V + E) | Space: O(V)
    public class GraphNode
    {
        public int val;
        public IList<GraphNode> neighbors;
        public GraphNode(int val = 0, IList<GraphNode>? neighbors = null)
        {
            this.val = val;
            this.neighbors = neighbors ?? new List<GraphNode>();
        }
    }

    public class CloneGraph
    {
        private Dictionary<GraphNode, GraphNode> _visited = new();

        public GraphNode? Clone(GraphNode? node)
        {
            if (node == null) return null;
            if (_visited.TryGetValue(node, out var cloned)) return cloned;

            var clone = new GraphNode(node.val);
            _visited[node] = clone;
            foreach (var neighbor in node.neighbors)
                clone.neighbors.Add(Clone(neighbor)!);
            return clone;
        }
    }

    // 🎤 GOOGLE DEMO — onsite STAPLE; tests topological-sort fluency + cycle detection.
    //   Q: "Given course prerequisites, can you finish all courses? (Detect cycle in directed graph.)"
    //   Ex: prereqs=[[1,0]] → true   |   [[1,0],[0,1]] → false (cycle)
    //   Approaches: ① BFS Kahn's (in-degree queue) O(V+E)/O(V) ★  ② DFS 3-color (white/gray/black) O(V+E)/O(V) ★
    //   🚩 Red flag: BFS without tracking in-degree — can't detect cycle, only checks reachability.
    //   ✨ Strong hire: name the technique ("this is Kahn's") and explain why processed-count == V ⇔ acyclic.
    //   Follow-ups: LC 210 (return the ORDER), LC 269 (alien dictionary), LC 802 (eventual safe states), LC 444.
    // --- LC #207: Course Schedule (Medium) — Topological Sort (BFS Kahn's) ---
    // GOAL: Determine if it is possible to finish all courses given
    //       prerequisite pairs [a, b] meaning "take b before a".
    //       Equivalent to: does the directed graph have a cycle?
    //
    // INTUITION: Kahn's algorithm: repeatedly remove nodes with in-degree 0
    //   (no remaining prerequisites). If all nodes are removable, there is no
    //   cycle. If some nodes can never reach in-degree 0, a cycle exists.
    //
    // STEPS:
    //   Build adjacency list and in-degree array.
    //   Enqueue all nodes with inDegree == 0.
    //   While queue non-empty:
    //     Dequeue course; processed++.
    //     For each neighbor: decrement inDegree; if 0 → enqueue.
    //   Return processed == numCourses.
    //
    // WHY IT WORKS: Only cycle-free nodes eventually reach in-degree 0.
    //   If any node remains with in-degree > 0, it belongs to a cycle.
    //
    // Time: O(V + E) | Space: O(V + E)
    public class CourseSchedule
    {
        public bool CanFinish(int numCourses, int[][] prerequisites)
        {
            var adj = new List<int>[numCourses];
            var inDegree = new int[numCourses];
            for (int i = 0; i < numCourses; i++) adj[i] = new List<int>();
            foreach (var p in prerequisites) { adj[p[1]].Add(p[0]); inDegree[p[0]]++; }

            var queue = new Queue<int>();
            for (int i = 0; i < numCourses; i++) if (inDegree[i] == 0) queue.Enqueue(i);

            int processed = 0;
            while (queue.Count > 0)
            {
                int course = queue.Dequeue();
                processed++;
                foreach (int next in adj[course])
                    if (--inDegree[next] == 0) queue.Enqueue(next);
            }
            return processed == numCourses;
        }
    }

    // 🎤 GOOGLE DEMO — L4/L5 onsite; tests REVERSE-BFS insight.
    //   Q: "Cells that can flow to BOTH oceans (Pacific=top/left, Atlantic=bottom/right). Flow = downhill or equal."
    //   Ex: 5x5 heightmap → list of (r,c) coords reaching both.
    //   Approaches: ① from EACH cell DFS to both oceans O((mn)²) ✘  ② multi-source BFS FROM oceans, climb UPHILL O(mn) ★
    //   🚩 Red flag: BFS forward from every cell — will TLE; always look for source-reversal opportunities.
    //   ✨ Strong hire: explicitly name "problem reversal"; intersect two visited sets = answer.
    //   Follow-ups: LC 994 (rotting oranges, multi-source), LC 542 (01-matrix), LC 1162 (max distance from land).
    // --- LC #417: Pacific Atlantic Water Flow (Medium) — Multi-Source BFS ---
    // GOAL: Find all cells from which water can flow to BOTH the Pacific ocean
    //       (top/left edges) and the Atlantic ocean (bottom/right edges).
    //       Water flows to equal or lower neighbors.
    //
    // INTUITION: Instead of simulating downhill flow from every cell (expensive),
    //   reverse the flow: start from ocean edges and BFS/DFS UPHILL.
    //   Mark cells reachable from Pacific edges and from Atlantic edges, then
    //   return cells marked by both.
    //
    // STEPS:
    //   BFS from all Pacific-border cells (uphill) → pacific[r][c] = true.
    //   BFS from all Atlantic-border cells (uphill) → atlantic[r][c] = true.
    //   Collect {r,c} where both are true.
    //
    // WHY IT WORKS: If a cell is reachable going uphill from the Pacific, then
    //   water from that cell CAN reach the Pacific going downhill — and vice versa.
    //
    // Time: O(m×n) | Space: O(m×n)
    public class PacificAtlanticWaterFlow
    {
        public IList<IList<int>> PacificAtlantic(int[][] heights)
        {
            int m = heights.Length, n = heights[0].Length;
            var pacific = new bool[m, n];
            var atlantic = new bool[m, n];
            for (int i = 0; i < m; i++) { DFS(heights, pacific, i, 0, m, n); DFS(heights, atlantic, i, n - 1, m, n); }
            for (int j = 0; j < n; j++) { DFS(heights, pacific, 0, j, m, n); DFS(heights, atlantic, m - 1, j, m, n); }

            var result = new List<IList<int>>();
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    if (pacific[i, j] && atlantic[i, j]) result.Add(new List<int> { i, j });
            return result;
        }
        private void DFS(int[][] h, bool[,] visited, int r, int c, int m, int n)
        {
            if (r < 0 || r >= m || c < 0 || c >= n || visited[r, c]) return;
            visited[r, c] = true;
            int[][] dirs = { new[]{1,0}, new[]{-1,0}, new[]{0,1}, new[]{0,-1} };
            foreach (var d in dirs)
            {
                int nr = r + d[0], nc = c + d[1];
                if (nr >= 0 && nr < m && nc >= 0 && nc < n && h[nr][nc] >= h[r][c])
                    DFS(h, visited, nr, nc, m, n);
            }
        }
    }

    // 🎤 GOOGLE DEMO — onsite favorite; tests DFS + backtracking discipline.
    //   Q: "Does WORD exist in the m×n char grid via 4-adjacent path? No cell reused."
    //   Ex: board=[['A','B','C','E'],['S','F','C','S'],['A','D','E','E']], word="ABCCED" → true   |   "ABCB" → false
    //   Approaches: ① DFS from every cell w/ visited marker O(m·n·4^L) ★
    //   🚩 Red flag: forgetting to UNMARK the cell on backtrack — silently blocks other paths.
    //   ✨ Strong hire: mutate cell to '#' for O(1)-space marking, restore on return; mention prefix-prune.
    //   Follow-ups: LC 212 (search MANY words → Trie + DFS), LC 130 (surrounded regions), LC 980 (unique paths III).
    // --- LC #79: Word Search (Medium) — DFS Backtracking on Grid ---
    // GOAL: Return true if the word exists in the grid following adjacent
    //       (up/down/left/right) cells, using each cell at most once per path.
    //
    // INTUITION: Try starting the word at every cell. From a starting position,
    //   explore all 4 directions recursively character-by-character. Mark the
    //   current cell visited (temporarily replace with '#') to prevent reuse,
    //   then restore it on backtrack.
    //
    // STEPS:
    //   For every (i, j): if DFS(i, j, 0) succeeds → return true.
    //   DFS(r, c, idx):
    //     if idx == word.Length: found!
    //     if out of bounds or board[r][c] != word[idx]: return false
    //     board[r][c] = '#'  (mark visited)
    //     explore 4 neighbors with idx+1
    //     board[r][c] = original char  (restore)
    //
    // WHY IT WORKS: The in-place marking avoids an extra visited array; the
    //   restore on backtrack ensures sibling paths see the unmodified board.
    //
    // Time: O(m×n×4ᴸ) L = word length | Space: O(L)
    public class WordSearch
    {
        public bool Exist(char[][] board, string word)
        {
            int m = board.Length, n = board[0].Length;
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    if (Search(board, word, i, j, 0, m, n)) return true;
            return false;
        }
        private bool Search(char[][] board, string word, int r, int c, int idx, int m, int n)
        {
            if (idx == word.Length) return true;
            if (r < 0 || r >= m || c < 0 || c >= n || board[r][c] != word[idx]) return false;
            char tmp = board[r][c];
            board[r][c] = '#';
            bool found = Search(board, word, r+1, c, idx+1, m, n) ||
                         Search(board, word, r-1, c, idx+1, m, n) ||
                         Search(board, word, r, c+1, idx+1, m, n) ||
                         Search(board, word, r, c-1, idx+1, m, n);
            board[r][c] = tmp;
            return found;
        }
    }

    // 🎤 GOOGLE DEMO — L4 onsite; tests Union-Find vs DFS trade-off articulation.
    //   Q: "Count CONNECTED COMPONENTS in an undirected graph (n nodes, edge list)."
    //   Ex: n=5, edges=[[0,1],[1,2],[3,4]] → 2
    //   Approaches: ① DFS/BFS visited O(V+E)/O(V)  ② Union-Find (path compression + union by rank) O((V+E)·α) ★
    //   🚩 Red flag: defaulting to DFS for STREAMING/ONLINE edges — Union-Find is the right tool.
    //   ✨ Strong hire: "start with n components; each successful union (different roots) decrements count".
    //   Follow-ups: LC 684 (redundant connection), LC 547 (friend circles), LC 1319 (connect components), LC 1971 (path).
    // --- LC #323: Number of Connected Components (Medium) — Union-Find ---
    // GOAL: Count the number of connected components in an undirected graph
    //       with n nodes and given edges.
    //
    // INTUITION: Union-Find (Disjoint Set Union) tracks which nodes share a
    //   component. Start with n components (each node alone). For each edge,
    //   union the two endpoints; if they were in different components, the
    //   count decreases by 1.
    //
    // KEY OPERATIONS:
    //   Find(x): walk parent pointers to the root; apply path compression.
    //   Union(x, y): link the root of x's component to y's (by rank).
    //                Return true only if they were in DIFFERENT components.
    //
    // STEPS:
    //   Initialize parent[i]=i, rank[i]=0, components=n.
    //   For each edge [a, b]: if Union(a,b) → components--.
    //   Return components.
    //
    // WHY IT WORKS: Path compression + union by rank keep Find nearly O(1)
    //   amortized (α(n) inverse Ackermann), making the whole algorithm O(n+e).
    //
    // Time: O((n + e)·α(n)) ≈ O(n + e) | Space: O(n)
    public class NumberOfConnectedComponents
    {
        private int[] _parent = null!;
        private int[] _rank = null!;
        public int CountComponents(int n, int[][] edges)
        {
            _parent = new int[n]; _rank = new int[n];
            for (int i = 0; i < n; i++) _parent[i] = i;
            int components = n;
            foreach (var e in edges)
                if (Union(e[0], e[1])) components--;
            return components;
        }
        private int Find(int x) { if (_parent[x] != x) _parent[x] = Find(_parent[x]); return _parent[x]; }
        private bool Union(int x, int y)
        {
            int px = Find(x), py = Find(y);
            if (px == py) return false;
            if (_rank[px] < _rank[py]) _parent[px] = py;
            else if (_rank[px] > _rank[py]) _parent[py] = px;
            else { _parent[py] = px; _rank[px]++; }
            return true;
        }
    }

    // 🎤 GOOGLE DEMO — L4 onsite; tests multi-source BFS + layer-counting.
    //   Q: "Minutes until no fresh oranges remain (0=empty, 1=fresh, 2=rotten; spreads 4-adjacent per minute). -1 if impossible."
    //   Ex: [[2,1,1],[1,1,0],[0,1,1]] → 4   |   [[2,1,1],[0,1,1],[1,0,1]] → -1
    //   Approaches: ① BFS from EACH rotten one at a time → wrong (gives sequential, not parallel)  ② multi-source BFS, enqueue ALL rotten at t=0, count layers O(mn) ★
    //   🚩 Red flag: running BFS once per rotten orange — produces wrong minute counts.
    //   ✨ Strong hire: count remaining fresh AFTER BFS; if >0 return -1.
    //   Follow-ups: LC 542 (01-matrix — distance to nearest 0), LC 1162 (max distance from land), LC 286 (walls/gates).
    // --- LC #994: Rotting Oranges (Medium) — Multi-Source BFS ---
    // GOAL: In an m×n grid (0 empty, 1 fresh, 2 rotten), each minute every
    //       rotten orange rots its 4-neighbour fresh oranges. Return minutes
    //       until no fresh remain, or -1 if impossible.
    //
    // INTUITION: BFS from ALL initially rotten cells SIMULTANEOUSLY — each
    //   BFS layer represents one minute. Track fresh count; if any remain
    //   after BFS ends, return -1.
    //
    // Time: O(m·n) | Space: O(m·n)
    public class RottingOranges
    {
        public int OrangesRotting(int[][] grid)
        {
            int rows = grid.Length, cols = grid[0].Length;
            var queue = new Queue<(int r, int c)>();              // BFS frontier (all current rotten)
            int fresh = 0;

            // Seed queue with every initially rotten cell; tally fresh ones.
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                {
                    if (grid[r][c] == 2) queue.Enqueue((r, c));
                    else if (grid[r][c] == 1) fresh++;
                }

            if (fresh == 0) return 0;                              // nothing to rot — already done

            int minutes = 0;
            int[][] dirs = { new[]{1,0}, new[]{-1,0}, new[]{0,1}, new[]{0,-1} };

            // Process one BFS layer per iteration of the outer loop.
            while (queue.Count > 0 && fresh > 0)
            {
                int layerSize = queue.Count;                       // freeze size so we only process THIS layer
                for (int i = 0; i < layerSize; i++)
                {
                    var (r, c) = queue.Dequeue();
                    foreach (var d in dirs)
                    {
                        int nr = r + d[0], nc = c + d[1];
                        // Skip out-of-bounds or non-fresh neighbours.
                        if (nr < 0 || nr >= rows || nc < 0 || nc >= cols) continue;
                        if (grid[nr][nc] != 1) continue;
                        grid[nr][nc] = 2;                           // rot it
                        fresh--;                                   // one less fresh orange
                        queue.Enqueue((nr, nc));                    // will rot its neighbours next minute
                    }
                }
                minutes++;                                         // finished one minute of spread
            }
            return fresh == 0 ? minutes : -1;                      // unreachable fresh ⇒ -1
        }
    }

    // 🎤 GOOGLE DEMO — Union-Find pattern question; common at Google L4.
    //   Q: "Tree + 1 extra edge → find that edge. If multiple valid, return last in input."
    //   Ex: [[1,2],[1,3],[2,3]] → [2,3]   |   [[1,2],[2,3],[3,4],[1,4],[1,5]] → [1,4]
    //   Approaches: ① build graph then DFS to find cycle O(V²)  ② Union-Find: first edge whose endpoints already share root O(V·α) ★
    //   🚩 Red flag: DFS hunt without realizing UF naturally returns the LAST cycle-closer.
    //   ✨ Strong hire: explain WHY UF gives the "last in input" answer for free.
    //   Follow-ups: LC 685 (DIRECTED variant — much harder; combine UF w/ in-degree check), LC 1319, LC 261 (graph valid tree).
    // --- LC #684: Redundant Connection (Medium) — Union-Find ---
    // GOAL: Given an undirected graph that started as a tree and had ONE extra
    //       edge added, return that extra edge (the one creating a cycle).
    //
    // INTUITION: Union-Find: for each edge (u,v), if u and v are already in
    //   the same component, this edge closes a cycle — it's the redundant one.
    //   Otherwise, union them and continue.
    //
    // Time: O(n α(n)) ≈ O(n) | Space: O(n)
    public class RedundantConnection
    {
        public int[] FindRedundantConnection(int[][] edges)
        {
            int n = edges.Length;
            var parent = new int[n + 1];                          // 1-indexed nodes
            for (int i = 0; i <= n; i++) parent[i] = i;           // each node is its own root initially

            foreach (var e in edges)
            {
                int ru = Find(parent, e[0]);
                int rv = Find(parent, e[1]);
                if (ru == rv) return e;                            // already connected → e is redundant
                parent[ru] = rv;                                   // union (no rank optim for brevity)
            }
            return Array.Empty<int>();                             // unreachable per problem guarantee
        }

        // Path-compressed Find: makes future lookups near-constant.
        private static int Find(int[] p, int x)
        {
            if (p[x] != x) p[x] = Find(p, p[x]);                  // recursively flatten chain
            return p[x];
        }
    }
}
