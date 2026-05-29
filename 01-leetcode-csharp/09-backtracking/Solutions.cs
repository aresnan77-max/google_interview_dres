// ============================================================================
// Category: Backtracking — Google Interview Prep
// Problems: Subsets(#78), Permutations(#46), CombinationSum(#39), NQueens(#51),
//           LetterCombinationsOfPhone(#17), PalindromePartitioning(#131),
//           WordSearchII(#212)
// ============================================================================

using System;
using System.Collections.Generic;
using System.Linq;

namespace GoogleInterviewPrep.LeetCode
{
    // 🎤 GOOGLE DEMO — canonical "do you know backtracking?" question.
    //   Q: "Return ALL SUBSETS (power set) of nums (unique elements)."
    //   Ex: [1,2,3] → [[],[1],[2],[1,2],[3],[1,3],[2,3],[1,2,3]]
    //   Approaches: ① backtracking include/exclude O(n·2^n) ★  ② iterative "double the list"  ③ bitmask enumerate 2^n masks
    //   🚩 Red flag: appending `cur` without copying — all entries end up the same list reference.
    //   ✨ Strong hire: show bitmask as the elegant alternative and mention it for n≤30.
    //   Follow-ups: LC 90 (Subsets II with DUPS), LC 77 (combinations of fixed k), LC 1286.
    // --- LC #78: Subsets (Medium) — Include/Exclude Decision Tree ---
    // GOAL: Return all possible subsets (the power set) of a list of integers.
    //
    // INTUITION: For each element, make a binary choice: include it or skip it.
    //   This forms a decision tree of depth n with 2ⁿ leaves = 2ⁿ subsets.
    //   Backtracking DFS explores every branch and records each leaf.
    //
    // STEPS:
    //   backtrack(start, current):
    //     Add a COPY of current to result  ← every node is a valid subset
    //     For i = start..n-1:
    //       current.Add(nums[i])
    //       backtrack(i+1, current)    ← explore including nums[i]
    //       current.RemoveLast()       ← undo (backtrack)
    //
    // WHY IT WORKS: By only going forward (start=i+1 each time) we never
    //   produce duplicates. Recording at every node (not just leaves)
    //   captures all subset sizes 0..n.
    //
    // Time: O(n · 2ⁿ) | Space: O(n)
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

    // 🎤 GOOGLE DEMO — L3/L4 onsite; classic permutation backtracking.
    //   Q: "All permutations of distinct integers."
    //   Ex: [1,2,3] → 6 permutations
    //   Approaches: ① backtracking w/ used[] O(n!·n)/O(n) ★  ② in-place swap (Heap's algorithm)  ③ lex "next-permutation" iteration
    //   🚩 Red flag: forgetting to reset used[i]=false (or pop cur) on backtrack — results become garbage.
    //   ✨ Strong hire: mention swap version uses O(1) extra besides recursion; talk about output size n!.
    //   Follow-ups: LC 47 (Permutations II w/ DUPS — sort+skip), LC 31 (next permutation), LC 60 (kth permutation).
    // --- LC #46: Permutations (Medium) — Used Array ---
    // GOAL: Return all permutations of distinct integers.
    //
    // INTUITION: Place elements one by one. At each position, any unused
    //   element can go next. A boolean `used` array tracks which elements
    //   are already in the current path.
    //
    // STEPS:
    //   backtrack(current, used):
    //     if current.Count == n: record current copy; return.
    //     For i = 0..n-1:
    //       if used[i]: skip
    //       used[i] = true; current.Add(nums[i])
    //       backtrack(current, used)
    //       current.RemoveLast(); used[i] = false  ← restore
    //
    // WHY IT WORKS: Iterating all unused elements at each depth ensures all
    //   n! orderings are generated; the `used` guard prevents repetition.
    //
    // Time: O(n! · n) | Space: O(n)
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

    // 🎤 GOOGLE DEMO — onsite favorite; tests duplicate-pruning insight.
    //   Q: "All unique combos of (distinct) candidates summing to target; each number reusable."
    //   Ex: [2,3,6,7], target=7 → [[2,2,3],[7]]
    //   Approaches: ① backtracking w/ `start` index + sorted-prune O(n^(T/M))/O(T/M) ★  ② DP table of "ways"
    //   🚩 Red flag: recursing on i+1 instead of i — disallows reuse and misses [2,2,3].
    //   ✨ Strong hire: sort first, then `if cands[i] > rem: break` — prune mentioned aloud.
    //   Follow-ups: LC 40 (each used AT MOST once, sort+skip dups), LC 216 (fixed k from 1..9), LC 377 (count perms).
    // --- LC #39: Combination Sum (Medium) — Pruning + Unlimited Reuse ---
    // GOAL: Find all unique combinations of candidates that sum to target.
    //       Each candidate can be reused unlimited times.
    //
    // INTUITION: DFS with a `remaining` sum. For each candidate c starting
    //   from `start` (avoids duplicate orderings), subtract c and recurse.
    //   Sort candidates first so we can stop early when c > remaining.
    //
    // STEPS:
    //   backtrack(start, remaining, current):
    //     if remaining == 0: record copy; return.
    //     For i = start..n-1:
    //       if candidates[i] > remaining: break  ← pruning (sorted!)
    //       current.Add(candidates[i])
    //       backtrack(i, remaining - candidates[i], current)  ← reuse i
    //       current.RemoveLast()
    //
    // WHY IT WORKS: Passing i (not i+1) allows reuse of the same element.
    //   The sort + early-break prunes branches where no valid combination exists.
    //
    // Time: O(n^(T/M)) T=target, M=min candidate | Space: O(T/M)
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

    // 🎤 GOOGLE DEMO — HARD onsite; tests constraint encoding + backtracking.
    //   Q: "Place n queens on n×n board, none attacking. Return all valid boards."
    //   Ex: n=4 → 2 distinct solutions
    //   Approaches: ① brute O(n^n) try every cell  ② row-by-row backtracking w/ 3 HashSets (cols, row-col, row+col) O(n!) ★  ③ bitmask O(n!) ★
    //   🚩 Red flag: re-scanning board for conflicts each placement — O(n!·n) instead of O(n!).
    //   ✨ Strong hire: encode diagonals as row±col integers; mention bitmask version for tiny memory.
    //   Follow-ups: LC 52 (just COUNT solutions), LC 37 (Sudoku Solver — same template), LC 980.
    // --- LC #51: N-Queens (Hard) — Constraint Backtracking ---
    // GOAL: Place n queens on an n×n board so no two attack each other.
    //       Return all valid board configurations.
    //
    // INTUITION: Place one queen per row (rows are already separated). For
    //   each row, try every column; skip if the column or either diagonal is
    //   already occupied. Track three sets: used columns, \ diagonals (row-col),
    //   / diagonals (row+col).
    //
    // STEPS:
    //   solve(row):
    //     if row == n: record board snapshot; return.
    //     For col = 0..n-1:
    //       if col in cols OR (row-col) in diag1 OR (row+col) in diag2: skip
    //       Place queen; add to all three sets.
    //       solve(row+1)
    //       Remove queen; remove from all three sets.
    //
    // WHY IT WORKS: Each queen placed tightens three independent constraints.
    //   HashSet lookups are O(1), so the only work is the n! branching factor
    //   reduced by pruning — much less than n! in practice.
    //
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

    // 🎤 GOOGLE DEMO — classic phone-screen; tests recursion + Cartesian product.
    //   Q: "Digits 2–9 → ALL letter combos on phone keypad."
    //   Ex: "23" → ["ad","ae","af","bd","be","bf","cd","ce","cf"]   |   "" → []
    //   Approaches: ① recursive backtracking O(3^N·4^M) ★  ② iterative BFS / queue  ③ LINQ Aggregate one-liner
    //   🚩 Red flag: forgetting the empty-input edge case — returns [""] instead of [].
    //   ✨ Strong hire: use StringBuilder (mutable, length--) instead of string concat in each recursive call.
    //   Follow-ups: LC 22 (generate parentheses), LC 401 (binary watch), LC 89 (Gray code).
    // --- LC #17: Letter Combinations of a Phone Number (Medium) — Backtracking ---
    // GOAL: Map digits 2-9 to phone-keypad letters and return ALL possible
    //       letter combinations the digit string could represent.
    //
    // INTUITION: Classic decision tree. For each digit, branch into each of
    //   the 3-4 letters it maps to. When path length == digits length, save it.
    //
    // Time: O(3^n · 4^m) where n = #digits mapping to 3 letters, m = #digits to 4
    // Space: O(n) recursion depth
    public class LetterCombinationsOfPhone
    {
        // Map digit char → letters on the keypad.
        private static readonly string[] Map = {
            "", "", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz"
        };

        public IList<string> LetterCombinations(string digits)
        {
            var res = new List<string>();
            if (string.IsNullOrEmpty(digits)) return res;          // empty input → empty output
            Build(digits, 0, new System.Text.StringBuilder(), res);
            return res;
        }

        private void Build(string digits, int i, System.Text.StringBuilder cur, List<string> res)
        {
            if (i == digits.Length)                                // consumed all digits → record combo
            {
                res.Add(cur.ToString());
                return;
            }
            string letters = Map[digits[i] - '0'];                 // letters for current digit
            foreach (char c in letters)
            {
                cur.Append(c);                                     // choose letter
                Build(digits, i + 1, cur, res);                    // recurse to next digit
                cur.Length--;                                      // un-choose (backtrack)
            }
        }
    }

    // 🎤 GOOGLE DEMO — L4 onsite; tests backtracking + on-the-fly validation.
    //   Q: "Partition s so every substring is a palindrome. Return all partitions."
    //   Ex: "aab" → [["a","a","b"],["aa","b"]]
    //   Approaches: ① backtracking + two-pointer palindrome check O(n·2^n) ★  ② precompute isPal[i][j] O(n²) then O(2^n) ★  ③ Manacher for palindrome table
    //   🚩 Red flag: rechecking same palindrome substring many times — precompute the table.
    //   ✨ Strong hire: state the O(n²) palindrome-table precomputation and that result is anyway O(2^n).
    //   Follow-ups: LC 132 (min CUTS — DP), LC 5 (longest palindromic substring), LC 647 (count substrings).
    // --- LC #131: Palindrome Partitioning (Medium) — Backtracking with Palindrome Check ---
    // GOAL: Partition string s such that every substring is a palindrome.
    //       Return all possible such partitions.
    //
    // INTUITION: Try every prefix s[start..end]; if it's a palindrome, recurse
    //   on the rest s[end+1..]. Build up the current partition list, then
    //   record a copy when start reaches the end of s.
    //
    // Time: O(n · 2^n) | Space: O(n) recursion + result
    public class PalindromePartitioning
    {
        public IList<IList<string>> Partition(string s)
        {
            var res = new List<IList<string>>();
            Backtrack(s, 0, new List<string>(), res);
            return res;
        }

        private void Backtrack(string s, int start, List<string> cur, List<IList<string>> res)
        {
            if (start == s.Length)                                 // consumed all chars → valid partition
            {
                res.Add(new List<string>(cur));                    // copy because cur is mutated
                return;
            }
            for (int end = start; end < s.Length; end++)
            {
                if (!IsPalindrome(s, start, end)) continue;        // prune: skip non-palindrome prefix
                cur.Add(s.Substring(start, end - start + 1));      // choose this slice
                Backtrack(s, end + 1, cur, res);                   // recurse on remainder
                cur.RemoveAt(cur.Count - 1);                       // backtrack
            }
        }

        private static bool IsPalindrome(string s, int l, int r)
        {
            while (l < r)                                          // standard two-pointer palindrome check
            {
                if (s[l++] != s[r--]) return false;
            }
            return true;
        }
    }

    // 🎤 GOOGLE DEMO — HARD onsite; Trie + DFS composition + pruning.
    //   Q: "Given a grid of letters and a word list, find all words formable by adjacent (⊥/→) cells, no cell reused per word."
    //   Ex: 4x4 board + ["oath","pea","eat","rain"] → ["oath","eat"]
    //   Approaches: ① run LC 79 per word O(W·MN·4^L)  ② build TRIE of all words, DFS board once O(MN·4^L) ★
    //   🚩 Red flag: per-word DFS — quadratic blowup when |words| is large.
    //   ✨ Strong hire: prune by nulling out the Trie's terminal flag after a word is found (dedupe + smaller subtree).
    //   Follow-ups: LC 79 (single word), LC 208 (Trie), LC 720 (longest word in dict via Trie).
    // --- LC #212: Word Search II (Hard) — Trie + Backtracking ---
    // GOAL: Given an m×n board of letters and a word list, return all words
    //       that can be formed by sequentially adjacent letters (no reuse).
    //
    // INTUITION: Build a Trie of all words. DFS from each cell, walking the
    //   Trie in lock-step with the board path. The Trie lets us prune dead
    //   branches early and check many words simultaneously in one DFS.
    //
    // Time: O(M·N·4·3^(L-1)) board cells × DFS branches | Space: O(Σ word lengths)
    public class WordSearchII
    {
        private class Node
        {
            public readonly Node?[] Ch = new Node?[26];
            public string? Word;                                   // non-null only at end of a word
        }

        public IList<string> FindWords(char[][] board, string[] words)
        {
            var root = BuildTrie(words);
            var res = new List<string>();
            int rows = board.Length, cols = board[0].Length;

            // Try starting a DFS from every cell.
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    DFS(board, r, c, root, res);

            return res;
        }

        private void DFS(char[][] b, int r, int c, Node node, List<string> res)
        {
            char ch = b[r][c];
            if (ch == '#') return;                                 // already visited in this path
            var next = node.Ch[ch - 'a'];
            if (next == null) return;                              // no word in trie continues this prefix

            if (next.Word != null)                                 // reached end of some word → record it
            {
                res.Add(next.Word);
                next.Word = null;                                  // avoid duplicates in result
            }

            b[r][c] = '#';                                         // mark visited
            if (r > 0)              DFS(b, r - 1, c, next, res);
            if (r < b.Length - 1)   DFS(b, r + 1, c, next, res);
            if (c > 0)              DFS(b, r, c - 1, next, res);
            if (c < b[0].Length-1)  DFS(b, r, c + 1, next, res);
            b[r][c] = ch;                                          // unmark (backtrack)
        }

        private static Node BuildTrie(string[] words)
        {
            var root = new Node();
            foreach (var w in words)
            {
                var n = root;
                foreach (char c in w)
                {
                    int i = c - 'a';
                    n.Ch[i] ??= new Node();                        // create branch if absent
                    n = n.Ch[i]!;
                }
                n.Word = w;                                        // tag terminal node with the word
            }
            return root;
        }
    }
}
