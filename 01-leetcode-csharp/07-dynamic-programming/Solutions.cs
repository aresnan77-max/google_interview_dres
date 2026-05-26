// ============================================================================
// Category: Dynamic Programming — Google Interview Prep
// Problems: ClimbingStairs(#70), CoinChange(#322), WordBreak(#139),
//           UniquePaths(#62), LongestIncreasingSubsequence(#300),
//           HouseRobber(#198), DecodeWays(#91)
// ============================================================================

using System;
using System.Collections.Generic;

namespace GoogleInterviewPrep.LeetCode
{
    // --- LC #70: Climbing Stairs (Easy) — 1D DP / Fibonacci ---
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

    // --- LC #198: House Robber (Medium) — 1D DP ---
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

    // --- LC #322: Coin Change (Medium) — Unbounded Knapsack DP ---
    // Time: O(n * amount) | Space: O(amount)
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

    // --- LC #62: Unique Paths (Medium) — 2D DP ---
    // Time: O(m*n) | Space: O(n)
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

    // --- LC #139: Word Break (Medium) — DP + HashSet ---
    // Time: O(n^2 * m) | Space: O(n)
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

    // --- LC #300: Longest Increasing Subsequence (Medium) — Binary Search ---
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

    // --- LC #91: Decode Ways (Medium) — 1D DP ---
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
}
