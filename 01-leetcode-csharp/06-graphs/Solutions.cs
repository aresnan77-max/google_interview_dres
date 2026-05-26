// ============================================================================
// Category: Graphs — Google Interview Prep
// Problems: NumberOfIslands(#200), CloneGraph(#133), CourseSchedule(#207),
//           PacificAtlanticWaterFlow(#417), WordSearch(#79),
//           NumberOfConnectedComponents(#323)
// ============================================================================

using System;
using System.Collections.Generic;

namespace GoogleInterviewPrep.LeetCode
{
    // --- LC #200: Number of Islands (Medium) — DFS Grid ---
    // Time: O(m*n) | Space: O(m*n) worst case
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

    // --- LC #207: Course Schedule (Medium) — Topological Sort (BFS Kahn's) ---
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

    // --- LC #417: Pacific Atlantic Water Flow (Medium) — Multi-Source BFS ---
    // Time: O(m*n) | Space: O(m*n)
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

    // --- LC #79: Word Search (Medium) — DFS Backtracking on Grid ---
    // Time: O(m*n*4^L) | Space: O(L)
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

    // --- LC #323: Number of Connected Components (Medium) — Union-Find ---
    // Time: O(n + e * α(n)) ≈ O(n + e) | Space: O(n)
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
}
