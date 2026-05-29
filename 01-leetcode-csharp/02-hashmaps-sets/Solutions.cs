// ============================================================================
// Category: HashMaps & Sets — Google Interview Prep
// Problems: GroupAnagrams(#49), LongestConsecutiveSequence(#128),
//           TopKFrequentElements(#347), ValidSudoku(#36),
//           ValidAnagram(#242), SubarraySumEqualsK(#560),
//           FirstUniqueCharacter(#387)
// ============================================================================

using System;
using System.Collections.Generic;
using System.Linq;

namespace GoogleInterviewPrep.LeetCode
{
    // 🎤 GOOGLE DEMO — 45-min onsite mainstay; tests hashing fluency.
    //   Q: "Given an array of strings, group ANAGRAMS together. Order arbitrary."
    //   Ex: ["eat","tea","tan","ate","nat","bat"] → [["eat","tea","ate"],["tan","nat"],["bat"]]
    //   Approaches: ① sorted-string key O(N·K log K) ★  ② 26-int char-count tuple key O(N·K) ★★
    //   🚩 Red flag: pairwise anagram comparison O(N²·K) — missed the canonical-key insight.
    //   ✨ Strong hire: offer both keys, then say "count-tuple wins when K is large; sorted wins for unicode/short strings".
    //   Follow-ups: LC 242 (single pair), LC 438 (find anagrams in s), LC 567 (permutation in string).
    // --- LC #49: Group Anagrams (Medium) — Sorted Key ---
    // GOAL: Group strings that are anagrams of each other into sublists.
    //
    // INTUITION: Two strings are anagrams iff their sorted characters are
    //   identical. Use that sorted string as a dictionary key — all anagrams
    //   map to the same bucket automatically.
    //
    // STEPS:
    //   For each string s:
    //     1. Sort its characters → key.
    //     2. Append s to dict[key].
    //   Return dict.Values.
    //
    // WHY IT WORKS: Sorting is a canonical form — any two anagrams produce
    //   the same sorted string, so they land in the same group.
    //
    // Time: O(n · k log k)  n = number of strings, k = max length
    // Space: O(n · k)
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

    // 🎤 GOOGLE DEMO — beloved L4 question; surprises candidates with O(n) under no-sort constraint.
    //   Q: "Length of the longest CONSECUTIVE integers sequence in unsorted nums. O(n) required."
    //   Ex: [100,4,200,1,3,2] → 4  ([1,2,3,4])
    //   Approaches: ① sort + scan O(n log n)  ② HashSet, expand only from sequence HEADS O(n) ★
    //   🚩 Red flag: expanding from EVERY element — turns O(n) back into O(n²) worst case.
    //   ✨ Strong hire: prove amortized O(n) — "each element is visited at most twice (once as head, once as extension)".
    //   Follow-ups: LC 298 (longest consecutive in BINARY TREE), LC 1218 (longest arithmetic subsequence).
    // --- LC #128: Longest Consecutive Sequence (Medium) — HashSet ---
    // GOAL: Find the length of the longest sequence of consecutive integers.
    //       Must run in O(n).
    //
    // INTUITION: Put all numbers in a HashSet for O(1) lookup. Only START
    //   counting a sequence from a number n where (n-1) is NOT in the set —
    //   that prevents re-counting the same sequence from every element.
    //
    // STEPS:
    //   1. Load all nums into a HashSet.
    //   2. For each num where (num-1) ∉ set:
    //        Count forward: current = num, length = 1
    //        While (current+1) ∈ set: current++, length++
    //        Update longest = max(longest, length).
    //
    // WHY IT WORKS: By only starting at sequence heads (no left-neighbor),
    //   each number is visited at most twice → O(n) total.
    //
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

    // 🎤 GOOGLE DEMO — probes data-structure choices; expects ALL THREE approaches walked through.
    //   Q: "Return the k MOST FREQUENT elements. MUST beat O(n log n)."
    //   Ex: nums=[1,1,1,2,2,3], k=2 → [1,2]
    //   Approaches: ① sort by freq O(n log n) ✘  ② size-k min-heap O(n log k) ✓  ③ bucket sort by freq O(n) ★
    //   🚩 Red flag: jumping to sort — the problem EXPLICITLY forbids O(n log n).
    //   ✨ Strong hire: explain why buckets work: "frequency is bounded by n, so n+1 buckets suffice".
    //   Follow-ups: LC 692 (top-k words, tie-break by alphabet), LC 451 (sort chars by frequency), LC 973.
    // --- LC #347: Top K Frequent Elements (Medium) — Bucket Sort ---
    // GOAL: Return the k most frequent elements. Must beat O(n log n).
    //
    // INTUITION: Frequency can be at most n. Create n+1 buckets indexed by
    //   frequency count. Place each number in its bucket. Then read buckets
    //   from high to low, collecting elements until we have k.
    //
    // STEPS:
    //   1. Count frequency of each number → freq map.
    //   2. Create buckets array of size n+1; put each number in buckets[freq].
    //   3. Scan buckets right-to-left, collect until result.Count == k.
    //
    // WHY IT WORKS: Bucket indices ARE the frequencies, so no sorting needed.
    //   Reading from highest bucket down naturally gives most-frequent first.
    //
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

    // 🎤 GOOGLE DEMO — classic warm-up; tests modeling constraints concisely.
    //   Q: "Validate a 9×9 Sudoku: no dup 1-9 in any row, col, or 3×3 box. '.' = empty."
    //   Ex: standard partial board → true/false
    //   Approaches: ① three pass (row/col/box) O(1)  ② single pass + one HashSet with prefixed keys O(1) ★
    //   🚩 Red flag: 9 separate HashSets per region — verbose; one prefixed Set is cleaner.
    //   ✨ Strong hire: encode box ID as (r/3)*3 + (c/3); single-pass O(1) (fixed 81 cells).
    //   Follow-ups: LC 37 (SOLVE Sudoku — backtracking), LC 36 streaming version.
    // --- LC #36: Valid Sudoku (Medium) — HashSet per Region ---
    // GOAL: Determine if a 9×9 partially-filled Sudoku board is valid.
    //       (Valid = no duplicate 1-9 in any row, column, or 3×3 box.)
    //
    // INTUITION: For every filled cell (r, c) with value v, encode three
    //   facts as unique strings and add them to a single HashSet:
    //     "r{row}{v}"  → v appears in row r
    //     "c{col}{v}"  → v appears in col c
    //     "b{r/3}{c/3}{v}" → v appears in that 3×3 box
    //   If any encoding is already in the set → duplicate → invalid.
    //
    // STEPS:
    //   Iterate all 81 cells; skip '.'. For each digit v:
    //     Try adding the 3 encodings to a HashSet.
    //     If any Add returns false → board is invalid.
    //
    // WHY IT WORKS: The string encoding uniquely captures "which digit, in
    //   which region" — a collision means a rule violation.
    //
    // Time: O(1)  (fixed 81 cells) | Space: O(1)
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

    // 🎤 GOOGLE DEMO — phone-screen warm-up; tests character-frequency reasoning.
    //   Q: "Return true if t is an ANAGRAM of s."
    //   Ex: s="anagram", t="nagaram" → true   |   s="rat", t="car" → false
    //   Approaches: ① sort both, compare O(n log n)  ② 26-int freq array O(n)/O(1) ★  ③ Dict<char,int> for unicode
    //   🚩 Red flag: forgetting the length check first — wastes work on obviously-different inputs.
    //   ✨ Strong hire: single pass, s contributes +1 and t contributes −1 at the same index.
    //   Follow-ups: LC 49 (group anagrams), LC 438 (find anagrams in window), LC 383 (ransom note).
    // --- LC #242: Valid Anagram (Easy) — Frequency Count ---
    // GOAL: Return true if t is an anagram of s (same letters, possibly reordered).
    //
    // INTUITION: Two strings are anagrams iff every character appears the same
    //   number of times in each. For lowercase English letters, a 26-slot int
    //   array beats a Dictionary in both speed and memory.
    //
    // Time: O(n) | Space: O(1)  (constant alphabet)
    public class ValidAnagram
    {
        public bool IsAnagram(string s, string t)
        {
            if (s.Length != t.Length) return false;          // fast reject — different sizes can't match
            var count = new int[26];                          // one bucket per lowercase letter
            for (int i = 0; i < s.Length; i++)
            {
                count[s[i] - 'a']++;                          // s contributes +1
                count[t[i] - 'a']--;                          // t contributes -1 (same index in single pass)
            }
            foreach (int c in count)                          // every bucket must net to zero
                if (c != 0) return false;
            return true;
        }
    }

    // 🎤 GOOGLE DEMO — L4/L5 favorite; tests prefix-sum insight + sliding-window TRAP.
    //   Q: "Count the number of contiguous subarrays whose sum equals k. (nums may be negative.)"
    //   Ex: nums=[1,1,1], k=2 → 2   |   nums=[1,2,3], k=3 → 2
    //   Approaches: ① brute O(n²)  ② prefix-sum + HashMap O(n)/O(n) ★
    //   🚩 Red flag: proposing sliding-window — BREAKS on negative numbers. State this out loud.
    //   ✨ Strong hire: pre-seed map with {0:1} so subarrays starting at index 0 are counted naturally.
    //   Follow-ups: LC 974 (sums divisible by k — mod prefix), LC 525 (equal 0/1), LC 523 (good subarray).
    // --- LC #560: Subarray Sum Equals K (Medium) — Prefix Sum + HashMap ---
    // GOAL: Count the number of contiguous subarrays whose sum equals k.
    //
    // INTUITION: Let prefix[i] = sum of nums[0..i]. A subarray (j..i] has sum k
    //   iff prefix[i] - prefix[j] == k, i.e. prefix[j] == prefix[i] - k.
    //   So while scanning, we just count how many earlier prefix sums equal
    //   (currentPrefix - k).
    //
    // Time: O(n) | Space: O(n)
    public class SubarraySumEqualsK
    {
        public int SubarraySum(int[] nums, int k)
        {
            // Maps prefix-sum value → how many times it has occurred so far.
            var prefixCount = new Dictionary<long, int> { [0] = 1 }; // empty prefix sum = 0 occurs once
            long prefix = 0;                                  // running prefix sum
            int count = 0;                                    // total valid subarrays
            foreach (int n in nums)
            {
                prefix += n;                                  // extend prefix by current element
                // If some earlier prefix equals (prefix - k), that subarray sums to k.
                if (prefixCount.TryGetValue(prefix - k, out int c)) count += c;
                // Record current prefix for later iterations.
                prefixCount[prefix] = prefixCount.GetValueOrDefault(prefix, 0) + 1;
            }
            return count;
        }
    }

    // 🎤 GOOGLE DEMO — simple phone-screen opener; verifies two-pass thinking.
    //   Q: "Index of the FIRST non-repeating character, or −1 if none."
    //   Ex: "leetcode" → 0   |   "loveleetcode" → 2   |   "aabb" → −1
    //   Approaches: ① nested scan O(n²)  ② two-pass freq array O(n)/O(1) ★  ③ LinkedHashMap for streaming
    //   🚩 Red flag: using Dict<char,int> for lowercase-only input — 26-int array is faster.
    //   ✨ Strong hire: discuss the streaming follow-up (queue of candidates + freq counter).
    //   Follow-ups: "design queue-based stream solution", LC 451 (sort by freq).
    // --- LC #387: First Unique Character in a String (Easy) — Frequency Array ---
    // GOAL: Return the index of the first non-repeating character, or -1.
    //
    // INTUITION: Two passes — first count every char, then scan left-to-right
    //   and return the first index whose count is 1.
    //
    // Time: O(n) | Space: O(1)
    public class FirstUniqueCharacter
    {
        public int FirstUniqChar(string s)
        {
            var freq = new int[26];                           // counts for 'a'..'z'
            foreach (char c in s) freq[c - 'a']++;            // pass 1: tally frequencies
            for (int i = 0; i < s.Length; i++)                // pass 2: find first with freq == 1
                if (freq[s[i] - 'a'] == 1) return i;
            return -1;                                        // no unique character exists
        }
    }
}
