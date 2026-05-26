// ============================================================================
// Category: HashMaps & Sets — Google Interview Prep
// Problems: GroupAnagrams(#49), LongestConsecutiveSequence(#128),
//           TopKFrequentElements(#347), ValidSudoku(#36)
// ============================================================================

using System;
using System.Collections.Generic;
using System.Linq;

namespace GoogleInterviewPrep.LeetCode
{
    // --- LC #49: Group Anagrams (Medium) — Sorted Key ---
    // Time: O(n * k log k) | Space: O(n * k)
    public class GroupAnagrams
    {
        public IList<IList<string>> Solve(string[] strs)
        {
            var map = new Dictionary<string, IList<string>>();
            foreach (var s in strs)
            {
                char[] chars = s.ToCharArray();
                Array.Sort(chars);
                string key = new string(chars);
                if (!map.ContainsKey(key)) map[key] = new List<string>();
                map[key].Add(s);
            }
            return map.Values.ToList<IList<string>>();
        }
    }

    // --- LC #128: Longest Consecutive Sequence (Medium) — HashSet ---
    // Time: O(n) | Space: O(n)
    public class LongestConsecutiveSequence
    {
        public int LongestConsecutive(int[] nums)
        {
            var set = new HashSet<int>(nums);
            int longest = 0;
            foreach (int num in set)
            {
                if (!set.Contains(num - 1)) // Only start counting from sequence head
                {
                    int current = num, length = 1;
                    while (set.Contains(current + 1)) { current++; length++; }
                    longest = Math.Max(longest, length);
                }
            }
            return longest;
        }
    }

    // --- LC #347: Top K Frequent Elements (Medium) — Bucket Sort ---
    // Time: O(n) | Space: O(n)
    public class TopKFrequentElements
    {
        public int[] TopKFrequent(int[] nums, int k)
        {
            var freq = new Dictionary<int, int>();
            foreach (int n in nums) freq[n] = freq.GetValueOrDefault(n) + 1;

            var buckets = new List<int>[nums.Length + 1];
            foreach (var (num, count) in freq)
            {
                buckets[count] ??= new List<int>();
                buckets[count].Add(num);
            }

            var result = new List<int>();
            for (int i = buckets.Length - 1; i >= 0 && result.Count < k; i--)
                if (buckets[i] != null) result.AddRange(buckets[i]);
            return result.Take(k).ToArray();
        }
    }

    // --- LC #36: Valid Sudoku (Medium) — HashSet per Region ---
    // Time: O(81) = O(1) | Space: O(81) = O(1)
    public class ValidSudoku
    {
        public bool IsValidSudoku(char[][] board)
        {
            var seen = new HashSet<string>();
            for (int r = 0; r < 9; r++)
                for (int c = 0; c < 9; c++)
                {
                    if (board[r][c] == '.') continue;
                    char v = board[r][c];
                    if (!seen.Add($"r{r}{v}") || !seen.Add($"c{c}{v}") || !seen.Add($"b{r/3}{c/3}{v}"))
                        return false;
                }
            return true;
        }
    }
}
