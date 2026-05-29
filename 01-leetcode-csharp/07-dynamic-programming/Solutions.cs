// ============================================================================
// Category: Dynamic Programming — Google Interview Prep
// Problems: ClimbingStairs(#70), CoinChange(#322), WordBreak(#139),
//           UniquePaths(#62), LongestIncreasingSubsequence(#300),
//           HouseRobber(#198), DecodeWays(#91),
//           LongestCommonSubsequence(#1143), EditDistance(#72),
//           MaximalSquare(#221), PartitionEqualSubsetSum(#416)
// ============================================================================

using System;
using System.Collections.Generic;

namespace GoogleInterviewPrep.LeetCode
{
    // 🎤 GOOGLE DEMO — DP "hello world"; common phone-screen opener.
    //   Q: "Climb n stairs taking 1 or 2 steps at a time. How many distinct ways?"
    //   Ex: n=2 → 2   |   n=3 → 3   |   n=5 → 8 (Fibonacci)
    //   Approaches: ① recursion O(2^n)  ② memoized O(n)/O(n)  ③ bottom-up DP O(n)/O(n)  ④ two-variable rolling O(n)/O(1) ★
    //   🚩 Red flag: writing naive recursion without recognizing the exponential blowup.
    //   ✨ Strong hire: name the recurrence as Fibonacci out loud; mention matrix-exp for O(log n).
    //   Follow-ups: LC 746 (min cost climbing), LC 1137 (Tribonacci), LC 70 generalized to k steps.
    // --- LC #70: Climbing Stairs (Easy) — 1D DP / Fibonacci ---
    // GOAL: Count the number of distinct ways to climb n stairs, taking 1 or
    //       2 steps at a time.
    //
    // INTUITION: To reach stair n you must have come from stair n-1 (1 step)
    //   or stair n-2 (2 steps). So ways(n) = ways(n-1) + ways(n-2).
    //   This is exactly the Fibonacci recurrence.
    //
    // STEPS:
    //   Base cases: ways(1) = 1, ways(2) = 2.
    //   For i = 3..n: ways(i) = ways(i-1) + ways(i-2).
    //   Use two variables a, b instead of an array to save space.
    //
    // WHY IT WORKS: Each new step is uniquely determined by the two previous
    //   values; no other history is needed → O(1) space.
    //
    // Time: O(n) | Space: O(1)
    public class ClimbingStairs
    {
        public int ClimbStairs(int n)
        {
            if (n <= 2) return n;
            int a = 1, b = 2;
            for (int i = 3; i <= n; i++) { int tmp = a + b; a = b; b = tmp; }
            return b;
        }
    }

    // 🎤 GOOGLE DEMO — L3 onsite; teaches the "include-vs-skip" DP template.
    //   Q: "Max money you can rob; can't rob TWO ADJACENT houses."
    //   Ex: [1,2,3,1] → 4   |   [2,7,9,3,1] → 12
    //   Approaches: ① brute O(2^n)  ② DP dp[i]=max(dp[i-1], dp[i-2]+nums[i]) O(n)/O(n)  ③ two-var rolling O(n)/O(1) ★
    //   🚩 Red flag: greedy "rob every other house" — wrong on [2,1,1,2].
    //   ✨ Strong hire: state the include/skip choice clearly; mention this template generalizes.
    //   Follow-ups: LC 213 (CIRCULAR — split into two linear), LC 337 (TREE — post-order pair), LC 740 (delete and earn).
    // --- LC #198: House Robber (Medium) — 1D DP ---
    // GOAL: Maximize the sum of non-adjacent elements in an array (houses).
    //       You cannot rob two adjacent houses.
    //
    // INTUITION: For each house i, you either skip it (best up to i-1) or rob
    //   it (best up to i-2 + nums[i]). Track two rolling variables instead of
    //   an array.
    //
    // STEPS:
    //   prev2 = 0, prev1 = 0.
    //   For each num:
    //     tmp   = max(prev1, prev2 + num)
    //     prev2 = prev1
    //     prev1 = tmp
    //   Return prev1.
    //
    // WHY IT WORKS: At every step prev1 = max loot achievable through the
    //   current house, prev2 = max loot through the house before that.
    //   Choosing max(prev1, prev2 + num) captures both decisions in O(1).
    //
    // Time: O(n) | Space: O(1)
    public class HouseRobber
    {
        public int Rob(int[] nums)
        {
            int prev2 = 0, prev1 = 0;
            foreach (int num in nums) { int tmp = Math.Max(prev1, prev2 + num); prev2 = prev1; prev1 = tmp; }
            return prev1;
        }
    }

    // 🎤 GOOGLE DEMO — HIGH-FREQUENCY onsite; canonical unbounded knapsack.
    //   Q: "Fewest coins (unlimited supply) to make `amount`. −1 if impossible."
    //   Ex: coins=[1,2,5], amount=11 → 3   |   coins=[2], amount=3 → -1
    //   Approaches: ① brute O(amount^n)  ② memoized DFS O(n·amount)  ③ bottom-up DP O(n·amount)/O(amount) ★  ④ BFS levels = #coins
    //   🚩 Red flag: greedy "take largest coin" — fails on coins=[1,3,4], amount=6 (greedy 3, optimal 2).
    //   ✨ Strong hire: explicitly disprove greedy with a counterexample BEFORE writing DP.
    //   Follow-ups: LC 518 (count COMBINATIONS, not min coins), LC 377 (count perm), LC 983 (min ticket cost).
    // --- LC #322: Coin Change (Medium) — Unbounded Knapsack DP ---
    // GOAL: Find the fewest coins that sum to amount; return -1 if impossible.
    //       Coins can be reused unlimited times.
    //
    // INTUITION: dp[i] = min coins to make amount i.
    //   For each amount i, try every coin c: if c ≤ i, we could take one coin c
    //   and need dp[i-c] more coins. Take the minimum over all valid coins.
    //
    // STEPS:
    //   dp[0] = 0; dp[1..amount] = amount+1 (sentinel for "impossible").
    //   For i = 1..amount:
    //     For each coin c where c ≤ i:
    //       dp[i] = min(dp[i], dp[i-c] + 1)
    //   Return dp[amount] > amount ? -1 : dp[amount].
    //
    // WHY IT WORKS: Bottom-up ensures dp[i-c] is already solved when we need
    //   it. The sentinel value propagates "impossible" automatically.
    //
    // Time: O(n · amount) | Space: O(amount)
    public class CoinChange
    {
        public int Solve(int[] coins, int amount)
        {
            int[] dp = new int[amount + 1];
            Array.Fill(dp, amount + 1);
            dp[0] = 0;
            for (int i = 1; i <= amount; i++)
                foreach (int coin in coins)
                    if (coin <= i) dp[i] = Math.Min(dp[i], dp[i - coin] + 1);
            return dp[amount] > amount ? -1 : dp[amount];
        }
    }

    // 🎤 GOOGLE DEMO — grid-DP staple; tests recurrence derivation.
    //   Q: "Unique paths from top-left to bottom-right of m×n grid, moving only RIGHT or DOWN."
    //   Ex: m=3,n=7 → 28   |   m=3,n=2 → 3
    //   Approaches: ① recursion O(2^(m+n))  ② 2D DP O(mn)/O(mn) ★  ③ 1D rolling O(mn)/O(n) ★  ④ combinatorics C(m+n-2, m-1) O(min(m,n)) ★
    //   🚩 Red flag: implementing 2D DP when interviewer hints at "can you reduce space?".
    //   ✨ Strong hire: mention the combinatorial closed-form to cap the discussion.
    //   Follow-ups: LC 63 (OBSTACLES), LC 64 (min path SUM), LC 980 (unique paths III — backtracking).
    // --- LC #62: Unique Paths (Medium) — 2D DP ---
    // GOAL: Count paths from top-left to bottom-right of an m×n grid, moving
    //       only right or down.
    //
    // INTUITION: paths(r, c) = paths(r-1, c) + paths(r, c-1).
    //   The first row and first column are all 1 (only one way to reach them).
    //   Compress to a 1-D array by processing row by row.
    //
    // STEPS:
    //   dp[0..n-1] = 1  (first row)
    //   For each subsequent row i:
    //     For j = 1..n-1: dp[j] += dp[j-1]
    //   Return dp[n-1].
    //
    // WHY IT WORKS: When processing row i column j, dp[j] holds the count from
    //   the cell above (old value), and dp[j-1] holds the count from the left
    //   (already updated this row). Adding them gives the correct total.
    //
    // Time: O(m×n) | Space: O(n)
    public class UniquePaths
    {
        public int Solve(int m, int n)
        {
            int[] dp = new int[n];
            Array.Fill(dp, 1);
            for (int i = 1; i < m; i++)
                for (int j = 1; j < n; j++)
                    dp[j] += dp[j - 1];
            return dp[n - 1];
        }
    }

    // 🎤 GOOGLE DEMO — L4 onsite favorite; bridges DP and string processing.
    //   Q: "Can s be segmented into a sequence of dictionary words (words may repeat)?"
    //   Ex: "leetcode", ["leet","code"] → true   |   "catsandog", ["cats","dog","sand","and","cat"] → false
    //   Approaches: ① brute DFS O(2^n)  ② memoized DFS O(n²·L)  ③ bottom-up DP O(n²·L)/O(n) ★  ④ Trie + DP
    //   🚩 Red flag: writing plain DFS without memoization — TLE on adversarial inputs like "aaaa...aab".
    //   ✨ Strong hire: scan suffixes from each true dp[j], not full O(n²) inner loop — mention max-word-length cap.
    //   Follow-ups: LC 140 (return ALL segmentations), LC 472 (concatenated words), LC 1278.
    // --- LC #139: Word Break (Medium) — DP + HashSet ---
    // GOAL: Return true if string s can be segmented into words from wordDict.
    //
    // INTUITION: dp[i] = true iff s[0..i-1] can be segmented.
    //   For each position i, try every split point j < i: if dp[j] is true
    //   AND s[j..i-1] is in the dictionary, then dp[i] is true.
    //
    // STEPS:
    //   Load wordDict into a HashSet for O(1) lookup.
    //   dp[0] = true (empty prefix is always valid).
    //   For i = 1..n:
    //     For j = 0..i-1:
    //       if dp[j] && dict.Contains(s[j..i-1]): dp[i] = true; break.
    //   Return dp[n].
    //
    // WHY IT WORKS: dp[j] being true means the prefix up to j is valid;
    //   checking the remaining substring closes the gap to position i.
    //
    // Time: O(n² · m) n = len(s), m = avg word length | Space: O(n)
    public class WordBreak
    {
        public bool Solve(string s, IList<string> wordDict)
        {
            var wordSet = new HashSet<string>(wordDict);
            bool[] dp = new bool[s.Length + 1];
            dp[0] = true;
            for (int i = 1; i <= s.Length; i++)
                for (int j = 0; j < i; j++)
                    if (dp[j] && wordSet.Contains(s.Substring(j, i - j))) { dp[i] = true; break; }
            return dp[s.Length];
        }
    }

    // 🎤 GOOGLE DEMO — L4/L5 favorite; binary-search insight is the differentiator.
    //   Q: "Length of LONGEST STRICTLY INCREASING SUBSEQUENCE (not contiguous)."
    //   Ex: [10,9,2,5,3,7,101,18] → 4   |   [0,1,0,3,2,3] → 4
    //   Approaches: ① brute O(2^n)  ② O(n²) DP dp[i]=best LIS ending at i  ③ O(n log n) tails + binary search ★ (L5 expected)
    //   🚩 Red flag: confusing subsequence with substring — always clarify out loud.
    //   ✨ Strong hire: explain tails array correctness — "tails[i] = smallest tail of any inc subseq of len i+1".
    //   Follow-ups: reconstruct the subsequence, LC 354 (Russian Doll Envelopes), LC 1671 (mountain array), LC 673.
    // --- LC #300: Longest Increasing Subsequence (Medium) — Binary Search ---
    // GOAL: Find the length of the longest strictly increasing subsequence.
    //
    // INTUITION: Maintain a list `tails` where tails[i] is the smallest tail
    //   element of all increasing subsequences of length i+1 seen so far.
    //   For each new number:
    //     - Binary search for its position in tails.
    //     - Replace tails[pos] if found (keeping tails as small as possible),
    //       or extend tails if it's larger than all current tails.
    //
    // STEPS:
    //   tails = [].
    //   For each num:
    //     pos = lower_bound(tails, num)
    //     if pos == tails.Length: tails.Append(num)
    //     else: tails[pos] = num
    //   Return tails.Length.
    //
    // WHY IT WORKS: `tails` is always sorted (invariant), enabling binary
    //   search. Its length equals the LIS length even though it may not
    //   represent an actual valid subsequence.
    //
    // Time: O(n log n) | Space: O(n)
    public class LongestIncreasingSubsequence
    {
        public int LengthOfLIS(int[] nums)
        {
            var tails = new List<int>();
            foreach (int num in nums)
            {
                int pos = tails.BinarySearch(num);
                if (pos < 0) pos = ~pos;
                if (pos == tails.Count) tails.Add(num);
                else tails[pos] = num;
            }
            return tails.Count;
        }
    }

    // 🎤 GOOGLE DEMO — L4 onsite; tests EDGE-CASE discipline more than DP.
    //   Q: "Decode digit string where A=1..Z=26. Return # of ways to decode."
    //   Ex: "12" → 2 (AB / L)   |   "226" → 3 (BZ / VF / BBF)   |   "06" → 0
    //   Approaches: ① memoized DFS O(n)/O(n)  ② bottom-up DP O(n)/O(n)  ③ two-var rolling O(n)/O(1) ★
    //   🚩 Red flag: treating '0' as a normal digit — "06" or "30" silently produce wrong counts.
    //   ✨ Strong hire: enumerate '0' rules out loud — "only valid as 2nd digit of 10 or 20".
    //   Follow-ups: LC 639 (with '*' wildcard — much trickier), LC 1416 (decode w/ k-limit), LC 248.
    // --- LC #91: Decode Ways (Medium) — 1D DP ---
    // GOAL: Count the number of ways to decode a digit string into letters
    //       (A=1, B=2, ..., Z=26).
    //
    // INTUITION: Like Climbing Stairs but with validity constraints.
    //   dp[i] = ways to decode s[0..i-1].
    //   A single digit s[i] contributes dp[i-1] ways (if digit != '0').
    //   A two-digit number s[i-1..i] in [10, 26] contributes dp[i-2] ways.
    //
    // STEPS:
    //   prev2 = 1 (empty string), prev1 = (s[0] != '0') ? 1 : 0.
    //   For i = 1..n-1:
    //     current = 0
    //     if s[i] != '0': current += prev1         ← 1-digit decode
    //     twoDigit = int(s[i-1..i])
    //     if 10 ≤ twoDigit ≤ 26: current += prev2  ← 2-digit decode
    //     prev2 = prev1; prev1 = current
    //
    // WHY IT WORKS: '0' can never stand alone, so we only add prev1 when the
    //   single digit is valid. Two-digit range [10,26] maps exactly to letters.
    //
    // Time: O(n) | Space: O(1)
    public class DecodeWays
    {
        public int NumDecodings(string s)
        {
            if (s[0] == '0') return 0;
            int prev2 = 1, prev1 = 1;
            for (int i = 1; i < s.Length; i++)
            {
                int current = 0;
                if (s[i] != '0') current = prev1;
                int twoDigit = int.Parse(s.Substring(i - 1, 2));
                if (twoDigit >= 10 && twoDigit <= 26) current += prev2;
                prev2 = prev1; prev1 = current;
            }
            return prev1;
        }
    }

    // 🎤 GOOGLE DEMO — onsite classic; the TEMPLATE for all 2D string DP.
    //   Q: "Length of the longest common subsequence (not substring) of text1 and text2."
    //   Ex: "abcde", "ace" → 3 ("ace")   |   "abc", "def" → 0
    //   Approaches: ① brute 2^n  ② memo O(nm)  ③ 2D DP O(nm)/O(nm) ★  ④ 1D rolling O(nm)/O(min(n,m)) ★
    //   🚩 Red flag: solving for longest common SUBSTRING accidentally — different recurrence.
    //   ✨ Strong hire: backtrack the DP table to recover the LCS string itself; mention Hunt-Szymanski for sparse cases.
    //   Follow-ups: LC 583 (delete to equal), LC 1092 (shortest common supersequence), LC 72 (edit distance).
    // --- LC #1143: Longest Common Subsequence (Medium) — 2D DP ---
    // GOAL: Return the length of the longest subsequence common to two strings.
    //       (A subsequence preserves order but is not contiguous.)
    //
    // INTUITION: Let dp[i][j] = LCS length of text1[0..i) and text2[0..j).
    //   • If chars match (text1[i-1] == text2[j-1]) → dp[i][j] = dp[i-1][j-1] + 1.
    //   • Else → dp[i][j] = max(dp[i-1][j], dp[i][j-1]).
    //
    // Time: O(m·n) | Space: O(m·n) (can compress to O(min(m,n)))
    public class LongestCommonSubsequence
    {
        public int LCSLength(string a, string b)
        {
            int m = a.Length, n = b.Length;
            var dp = new int[m + 1, n + 1];                       // row/col 0 = empty prefix → LCS 0
            for (int i = 1; i <= m; i++)
                for (int j = 1; j <= n; j++)
                {
                    if (a[i - 1] == b[j - 1])
                        dp[i, j] = dp[i - 1, j - 1] + 1;          // extend diagonal LCS by 1
                    else
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]); // drop one char from either string
                }
            return dp[m, n];                                       // LCS of full strings
        }
    }

    // 🎤 GOOGLE DEMO — L5 onsite; tests 2D DP state transitions + real-product relevance (spell-check, DNA).
    //   Q: "Min #ops (insert/delete/replace) to convert word1 → word2."
    //   Ex: "horse"→"ros" → 3   |   "intention"→"execution" → 5
    //   Approaches: ① brute O(3^(n+m))  ② memo O(nm)  ③ 2D DP O(nm)/O(nm) ★  ④ 1D rolling O(nm)/O(min(n,m)) ★
    //   🚩 Red flag: forgetting the dp[0][j]=j and dp[i][0]=i base cases — silently wrong by 1.
    //   ✨ Strong hire: name the three transitions (insert/delete/replace) explicitly; mention Hirschberg for O(min) space.
    //   Follow-ups: LC 583 (delete-only), LC 712 (min ASCII delete sum), LC 161 (one-edit distance).
    // --- LC #72: Edit Distance (Medium) — Levenshtein DP ---
    // GOAL: Min edits (insert/delete/replace) to convert word1 into word2.
    //
    // INTUITION: dp[i][j] = edit distance between word1[0..i) and word2[0..j).
    //   • Empty source needs j inserts; empty target needs i deletes.
    //   • If last chars equal → dp[i][j] = dp[i-1][j-1] (no extra op).
    //   • Else → 1 + min(replace=dp[i-1][j-1], delete=dp[i-1][j], insert=dp[i][j-1]).
    //
    // Time: O(m·n) | Space: O(m·n)
    public class EditDistance
    {
        public int MinDistance(string a, string b)
        {
            int m = a.Length, n = b.Length;
            var dp = new int[m + 1, n + 1];
            for (int i = 0; i <= m; i++) dp[i, 0] = i;             // delete all i chars from a
            for (int j = 0; j <= n; j++) dp[0, j] = j;             // insert all j chars into a

            for (int i = 1; i <= m; i++)
                for (int j = 1; j <= n; j++)
                {
                    if (a[i - 1] == b[j - 1])
                        dp[i, j] = dp[i - 1, j - 1];               // no edit needed for matching tail char
                    else
                        dp[i, j] = 1 + Math.Min(dp[i - 1, j - 1],  // replace
                                       Math.Min(dp[i - 1, j],     // delete from a
                                                dp[i, j - 1]));   // insert into a
                }
            return dp[m, n];
        }
    }

    // 🎤 GOOGLE DEMO — L4 onsite; tests DP on a 2D grid with neighbor relations.
    //   Q: "Largest all-1's SQUARE in a binary matrix. Return AREA."
    //   Ex: 4x5 matrix → 4 (a 2×2)
    //   Approaches: ① brute O((mn)²)  ② 2D DP dp[i][j]=side w/ bottom-right at (i,j) O(mn)/O(mn) ★  ③ 1D rolling O(mn)/O(n) ★
    //   🚩 Red flag: trying to track positions instead of side-length — over-complicates the state.
    //   ✨ Strong hire: derive the "min of THREE neighbors + 1" recurrence verbally; explain why min (not max).
    //   Follow-ups: LC 85 (max RECTANGLE in 0/1 — much harder, uses histogram trick), LC 1277 (count squares), LC 304.
    // --- LC #221: Maximal Square (Medium) — DP on Grid Bottom-Right Corner ---
    // GOAL: Largest square of '1's in a binary matrix; return its AREA.
    //
    // INTUITION: dp[i][j] = side length of the largest all-ones square whose
    //   BOTTOM-RIGHT corner is at (i,j). If cell is '1', it's bounded by the
    //   smallest of its three neighbours (top, left, top-left) plus 1.
    //
    // Time: O(m·n) | Space: O(m·n)
    public class MaximalSquare
    {
        public int MaximalSquareArea(char[][] matrix)
        {
            int m = matrix.Length, n = matrix[0].Length;
            var dp = new int[m + 1, n + 1];                       // padding row/col simplifies bounds
            int maxSide = 0;
            for (int i = 1; i <= m; i++)
                for (int j = 1; j <= n; j++)
                    if (matrix[i - 1][j - 1] == '1')
                    {
                        // Square extends only as far as the SMALLEST adjacent square allows.
                        dp[i, j] = 1 + Math.Min(dp[i - 1, j - 1],
                                       Math.Min(dp[i - 1, j], dp[i, j - 1]));
                        maxSide = Math.Max(maxSide, dp[i, j]);
                    }
            return maxSide * maxSide;                              // area = side²
        }
    }

    // 🎤 GOOGLE DEMO — L4 onsite; teaches problem REFRAMING into 0/1 knapsack.
    //   Q: "Can positive nums be partitioned into TWO subsets with EQUAL sum?"
    //   Ex: [1,5,11,5] → true ([1,5,5] and [11])   |   [1,2,3,5] → false (odd total)
    //   Approaches: ① brute 2^n  ② reframe as subset-sum=total/2; 2D dp O(n·sum)/O(n·sum)  ③ 1D dp iterating sum DOWN O(n·sum)/O(sum) ★  ④ bitset O(n·sum/64)
    //   🚩 Red flag: iterating sum UPWARD in the 1D version — silently reuses elements (becomes unbounded knapsack).
    //   ✨ Strong hire: voice the reframing "can a subset sum to total/2?" BEFORE coding.
    //   Follow-ups: LC 494 (target sum w/ +/-), LC 1049 (last stone weight II), LC 698 (k equal subsets).
    // --- LC #416: Partition Equal Subset Sum (Medium) — 0/1 Knapsack ---
    // GOAL: Can the array be partitioned into two subsets with equal sum?
    //
    // INTUITION: Equivalent to: does some subset sum to total/2? Classic 0/1
    //   knapsack on a boolean DP array. dp[s] = true iff some subset sums to s.
    //   Iterate sums DOWNWARD per item to avoid reusing the same item twice.
    //
    // Time: O(n·sum) | Space: O(sum)
    public class PartitionEqualSubsetSum
    {
        public bool CanPartition(int[] nums)
        {
            int total = 0;
            foreach (int x in nums) total += x;
            if (total % 2 != 0) return false;                      // odd total ⇒ cannot split evenly
            int target = total / 2;

            var dp = new bool[target + 1];                         // dp[s] = can we hit exactly s?
            dp[0] = true;                                          // empty subset sums to 0
            foreach (int num in nums)
            {
                // Walk DOWN so each num is considered at most once per outer pass.
                for (int s = target; s >= num; s--)
                    dp[s] = dp[s] || dp[s - num];                  // either skip num, or include it
            }
            return dp[target];
        }
    }
}
