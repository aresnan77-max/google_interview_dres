// ============================================================================
// Category: Backtracking — Google Interview Prep
// Problems: Subsets(#78), Permutations(#46), CombinationSum(#39), NQueens(#51)
// ============================================================================

using System;
using System.Collections.Generic;
using System.Linq;

namespace GoogleInterviewPrep.LeetCode
{
    // --- LC #78: Subsets (Medium) — Include/Exclude Decision Tree ---
    // Time: O(n * 2^n) | Space: O(n)
    public class Subsets
    {
        public IList<IList<int>> Solve(int[] nums)
        {
            var result = new List<IList<int>>();
            Backtrack(nums, 0, new List<int>(), result);
            return result;
        }
        private void Backtrack(int[] nums, int start, List<int> cur, List<IList<int>> res)
        {
            res.Add(new List<int>(cur));
            for (int i = start; i < nums.Length; i++) { cur.Add(nums[i]); Backtrack(nums, i+1, cur, res); cur.RemoveAt(cur.Count-1); }
        }
    }

    // --- LC #46: Permutations (Medium) — Used Array ---
    // Time: O(n! * n) | Space: O(n)
    public class Permutations
    {
        public IList<IList<int>> Permute(int[] nums)
        {
            var result = new List<IList<int>>();
            Backtrack(nums, new List<int>(), new bool[nums.Length], result);
            return result;
        }
        private void Backtrack(int[] nums, List<int> cur, bool[] used, List<IList<int>> res)
        {
            if (cur.Count == nums.Length) { res.Add(new List<int>(cur)); return; }
            for (int i = 0; i < nums.Length; i++)
            {
                if (used[i]) continue;
                used[i] = true; cur.Add(nums[i]);
                Backtrack(nums, cur, used, res);
                cur.RemoveAt(cur.Count-1); used[i] = false;
            }
        }
    }

    // --- LC #39: Combination Sum (Medium) — Pruning + Unlimited Reuse ---
    // Time: O(n^(T/M)) | Space: O(T/M)
    public class CombinationSum
    {
        public IList<IList<int>> Solve(int[] candidates, int target)
        {
            Array.Sort(candidates);
            var result = new List<IList<int>>();
            Backtrack(candidates, target, 0, new List<int>(), result);
            return result;
        }
        private void Backtrack(int[] cands, int rem, int start, List<int> cur, List<IList<int>> res)
        {
            if (rem == 0) { res.Add(new List<int>(cur)); return; }
            for (int i = start; i < cands.Length; i++)
            {
                if (cands[i] > rem) break;
                cur.Add(cands[i]); Backtrack(cands, rem - cands[i], i, cur, res); cur.RemoveAt(cur.Count-1);
            }
        }
    }

    // --- LC #51: N-Queens (Hard) — Constraint Backtracking ---
    // Time: O(n!) | Space: O(n)
    public class NQueens
    {
        public IList<IList<string>> SolveNQueens(int n)
        {
            var result = new List<IList<string>>();
            var board = new char[n][];
            for (int i = 0; i < n; i++) { board[i] = new char[n]; Array.Fill(board[i], '.'); }
            Solve(0, n, board, new HashSet<int>(), new HashSet<int>(), new HashSet<int>(), result);
            return result;
        }
        private void Solve(int row, int n, char[][] board, HashSet<int> cols, HashSet<int> d1, HashSet<int> d2, List<IList<string>> res)
        {
            if (row == n) { res.Add(board.Select(r => new string(r)).ToList()); return; }
            for (int c = 0; c < n; c++)
            {
                if (cols.Contains(c) || d1.Contains(row-c) || d2.Contains(row+c)) continue;
                board[row][c]='Q'; cols.Add(c); d1.Add(row-c); d2.Add(row+c);
                Solve(row+1, n, board, cols, d1, d2, res);
                board[row][c]='.'; cols.Remove(c); d1.Remove(row-c); d2.Remove(row+c);
            }
        }
    }
}
