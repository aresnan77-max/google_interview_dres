// ============================================================================
// Category: Arrays & Strings — Google Interview Prep
// Problems: TwoSum(#1), BestTimeToBuyAndSellStock(#121), MaxSubarray(#53),
//           ProductExceptSelf(#238), MergeIntervals(#56), ThreeSum(#15),
//           TrappingRainWater(#42), SlidingWindowMaximum(#239),
//           ContainsDuplicate(#217), ValidPalindrome(#125),
//           MaxProductSubarray(#152), LongestSubstringWithoutRepeating(#3),
//           RotateImage(#48)
// ============================================================================

using System;
using System.Collections.Generic;
using System.Linq;

namespace GoogleInterviewPrep.LeetCode
{
    // 🎤 GOOGLE DEMO — classic warm-up; phone-screen opener.
    //   Q: "Given an array and a target, return indices of the two numbers summing to target."
    //   Ex: nums=[2,7,11,15], target=9 → [0,1]
    //   Approaches: ① brute O(n²)  ② hashmap O(n) ★
    //   🚩 Red flag: nested loops without mentioning the hashmap trade-off.
    //   ✨ Strong hire: ask "sorted? duplicates? negative numbers?" BEFORE coding.
    //   Follow-ups: LC 167 (sorted → two-pointer O(1) space), LC 15 (3Sum), LC 454 (4SumII).
    // --- LC #1: Two Sum (Easy) — HashMap Pattern ---
    // GOAL: Find two indices i,j such that nums[i] + nums[j] == target.
    //
    // INTUITION: Instead of checking all pairs O(n²), store each element's
    //   index in a hash map. For every element, the "complement" we need is
    //   (target - nums[i]). If that complement is already in the map, we found
    //   our pair immediately.
    //
    // STEPS:
    //   1. Iterate through nums with index i.
    //   2. complement = target - nums[i]
    //   3. If complement exists in map → return {map[complement], i}
    //   4. Otherwise store map[nums[i]] = i and continue.
    //
    // WHY IT WORKS: We look "backwards" — by the time we reach nums[i],
    //   any earlier element that pairs with it is already in the map.
    //
    // Time: O(n) | Space: O(n)
    public class TwoSum
    {
        public int[] Solve(int[] nums, int target)
        {
            var map = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                int complement = target - nums[i];
                if (map.TryGetValue(complement, out int idx)) return new[] { idx, i };
                map.TryAdd(nums[i], i);
            }

            throw new ArgumentException("No solution");
        }
    }

    // 🎤 GOOGLE DEMO — phone-screen favorite; tests "DP-ish but actually one pass" insight.
    //   Q: "prices[i] = stock price on day i. ONE buy + ONE later sell. Return max profit (or 0)."
    //   Ex: [7,1,5,3,6,4] → 5  (buy@1, sell@6)
    //   Approaches: ① brute O(n²)  ② DP O(n)/O(n)  ③ single-pass min tracking O(n)/O(1) ★
    //   🚩 Red flag: tracking max-after-i with a suffix array — unnecessary O(n) space.
    //   ✨ Strong hire: frame as "running min + best profit so far" in one sentence.
    //   Follow-ups: LC 122 (unlimited → greedy), LC 123 (≤2 trans → DP), LC 309 (cooldown).
    // --- LC #121: Best Time to Buy and Sell Stock (Easy) — Single Pass ---
    // GOAL: Find max profit from one buy + one sell (buy before sell).
    //
    // INTUITION: You want to buy at the lowest price seen so far, and sell at
    //   the highest price seen AFTER that. One pass is enough: track the
    //   running minimum and update max profit on each step.
    //
    // STEPS:
    //   1. Initialize minPrice = ∞, maxProfit = 0.
    //   2. For each price p:
    //        minPrice = min(minPrice, p)
    //        maxProfit = max(maxProfit, p - minPrice)
    //
    // WHY IT WORKS: At every index the best possible profit ending here is
    //   (current price − cheapest price seen before it). We just keep the
    //   best of all those candidates.
    //
    // Time: O(n) | Space: O(1)
    public class BestTimeToBuyAndSellStock
    {
        public int MaxProfit(int[] prices)
        {
            int minPrice = int.MaxValue, maxProfit = 0;
            foreach (int price in prices)
            {
                minPrice = Math.Min(minPrice, price);
                maxProfit = Math.Max(maxProfit, price - minPrice);
            }

            return maxProfit;
        }
    }

    // 🎤 GOOGLE DEMO — L3/L4 onsite; tests recognizing Kadane's pattern.
    //   Q: "Find the contiguous subarray (≥1 element) with the LARGEST SUM. Return that sum."
    //   Ex: [-2,1,-3,4,-1,2,1,-5,4] → 6  ([4,-1,2,1])
    //   Approaches: ① brute O(n²)  ② Kadane DP O(n)/O(1) ★  ③ divide-and-conquer O(n log n)
    //   🚩 Red flag: initializing globalMax = 0 (breaks on all-negative input).
    //   ✨ Strong hire: state the invariant "curMax = best subarray ENDING at i".
    //   Follow-ups: LC 152 (max PRODUCT — track min too), LC 918 (circular), LC 1186 (with one deletion).
    // --- LC #53: Maximum Subarray (Medium) — Kadane's Algorithm ---
    // GOAL: Find the contiguous subarray with the largest sum.
    //
    // INTUITION: At each position, decide: "start fresh here, or extend the
    //   existing subarray?" If the running sum turns negative, it can only
    //   hurt future sums — reset it to the current element.
    //
    // STEPS:
    //   1. currentMax = globalMax = nums[0].
    //   2. For i = 1 .. n-1:
    //        currentMax = max(nums[i], currentMax + nums[i])  ← extend or restart
    //        globalMax  = max(globalMax, currentMax)
    //   3. Return globalMax.
    //
    // WHY IT WORKS: currentMax always holds the maximum subarray sum that
    //   *ends* at position i. Taking the global max over all positions gives
    //   the answer.
    //

    // currentMax 以当前位置结尾的最大子数组和决定是否延续之前的子数组 
    // globalMax 全局最大子数组和记录所有位置中的最大值
    // Time: O(n) | Space: O(1)
    public class MaxSubarray
    {
        public int MaxSubArray(int[] nums)
        {
            int currentMax = nums[0], globalMax = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                currentMax = Math.Max(nums[i], currentMax + nums[i]);
                globalMax = Math.Max(globalMax, currentMax);
            }

            return globalMax;
        }
    }

    // 🎤 GOOGLE DEMO — classic onsite; tests prefix-sum thinking under no-division constraint.
    //   Q: "answer[i] = product of all nums EXCEPT nums[i]. NO division. O(n) time."
    //   Ex: [1,2,3,4] → [24,12,8,6]
    //   Approaches: ① prefix[] + suffix[] arrays O(n)/O(n)  ② reuse output + running suffix O(n)/O(1) ★
    //   🚩 Red flag: proposing division then handling zeros — violates the explicit constraint.
    //   ✨ Strong hire: write the O(1)-extra-space version on the first try; explain why two passes suffice.
    //   Follow-ups: LC 152, LC 42 (same prefix/suffix idea on heights), LC 724 (pivot index).
    // --- LC #238: Product of Array Except Self (Medium) — Prefix/Suffix ---
    // GOAL: Return array output where output[i] = product of all nums except nums[i].
    //       Must run in O(n) without using division.
    //
    // INTUITION: output[i] = (product of everything LEFT of i)
    //                      × (product of everything RIGHT of i).
    //   Build the left-products in a first pass, then multiply in the
    //   right-products using a running variable in a second pass.
    //
    // STEPS:
    //   Pass 1 (left → right): result[i] = product of nums[0..i-1]
    //   Pass 2 (right → left): multiply result[i] by suffix product, then
    //                          update suffix *= nums[i].
    //
    // WHY IT WORKS: Each index contributes its left context and its right
    //   context independently — no division needed.
    //
    // Time: O(n) | Space: O(1) excluding output array
    public class ProductExceptSelf
    {
        public int[] Solve(int[] nums)
        {
            int n = nums.Length;
            int[] result = new int[n];
            result[0] = 1;
            for (int i = 1; i < n; i++) result[i] = result[i - 1] * nums[i - 1];
            int suffix = 1;
            for (int i = n - 1; i >= 0; i--)
            {
                result[i] *= suffix;
                suffix *= nums[i];
            }

            return result;
        }
    }

    // 🎤 GOOGLE DEMO — calendar/scheduling onsite; gateway to meeting-rooms family.
    //   Q: "Merge all overlapping intervals from a list of [start, end] pairs."
    //   Ex: [[1,3],[2,6],[8,10],[15,18]] → [[1,6],[8,10],[15,18]]
    //   Approaches: ① sort by start + linear merge O(n log n) ★  ② sweep-line (events) O(n log n)
    //   🚩 Red flag: forgetting to ask "does [1,4] + [4,5] count as overlapping?" (Yes.)
    //   ✨ Strong hire: mention sweep-line for the streaming follow-up before being asked.
    //   Follow-ups: LC 57 (insert), LC 253 (min meeting rooms), LC 435 (non-overlap erasure), LC 986 (intersection).
    // --- LC #56: Merge Intervals (Medium) — Sort + Merge ---
    // GOAL: Given a list of intervals, merge all overlapping ones.
    //
    // INTUITION: After sorting by start time, any overlapping interval must
    //   be adjacent. Walk through and extend the last interval's end whenever
    //   the next interval starts before the current one ends.
    //
    // STEPS:
    //   1. Sort intervals by start time.
    //   2. Initialize result with intervals[0].
    //   3. For each next interval:
    //        If next.start <= result.last.end → merge: extend end if needed.
    //        Else → no overlap, append next to result.
    //
    // WHY IT WORKS: Sorting guarantees we process intervals in order, so any
    //   overlap can only involve the interval we're currently tracking.
    //
    // Time: O(n log n) | Space: O(n)
    public class MergeIntervals
    {
        public int[][] Merge(int[][] intervals)
        {
            Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
            var merged = new List<int[]> { intervals[0] };
            for (int i = 1; i < intervals.Length; i++)
            {
                var last = merged[^1];
                if (intervals[i][0] <= last[1])
                    last[1] = Math.Max(last[1], intervals[i][1]);
                else
                    merged.Add(intervals[i]);
            }

            return merged.ToArray();
        }
    }

    // 🎤 GOOGLE DEMO — L4 staple; the "two-pointer fluency" test.
    //   Q: "Return ALL unique triplets [a,b,c] with a+b+c == 0. No duplicate triplets."
    //   Ex: [-1,0,1,2,-1,-4] → [[-1,-1,2], [-1,0,1]]
    //   Approaches: ① brute O(n³)  ② hashmap per pair O(n²)/O(n)  ③ sort + two-pointer O(n²)/O(1) ★
    //   🚩 Red flag: dedup via HashSet of triplets — works but signals you missed the sort trick.
    //   ✨ Strong hire: dedup by SKIPPING equal neighbors on i, lo, AND hi independently.
    //   Follow-ups: LC 16 (3Sum Closest), LC 18 (4Sum), LC 259 (3Sum Smaller).
    // --- LC #15: 3Sum (Medium) — Sort + Two Pointers ---
    // GOAL: Find all unique triplets [a, b, c] such that a + b + c == 0.
    //
    // INTUITION: Sort the array so duplicates are adjacent (easy to skip) and
    //   so that two-pointer search is valid. Fix one element a = nums[i],
    //   then use lo/hi pointers to find the pair that sums to -a.
    //
    // STEPS:
    //   1. Sort nums.
    //   2. For i = 0 .. n-3:
    //        Skip if nums[i] == nums[i-1] (avoid duplicate triplets).
    //        lo = i+1, hi = n-1.
    //        While lo < hi:
    //          sum = nums[i] + nums[lo] + nums[hi]
    //          sum == 0 → record, skip duplicates at both ends, move both.
    //          sum < 0  → lo++
    //          sum > 0  → hi--
    //
    // WHY IT WORKS: Sorting + skipping duplicates ensures each unique triplet
    //   is found exactly once. Two pointers cover all pairs in O(n) per i.
    //
    // Time: O(n²) | Space: O(1) excluding output
    public class ThreeSum
    {
        public IList<IList<int>> Solve(int[] nums)
        {
            Array.Sort(nums);
            var result = new List<IList<int>>();
            for (int i = 0; i < nums.Length - 2; i++)
            {
                if (i > 0 && nums[i] == nums[i - 1]) continue;
                int lo = i + 1, hi = nums.Length - 1;
                while (lo < hi)
                {
                    int sum = nums[i] + nums[lo] + nums[hi];
                    if (sum == 0)
                    {
                        result.Add(new List<int> { nums[i], nums[lo], nums[hi] });
                        while (lo < hi && nums[lo] == nums[lo + 1]) lo++;
                        while (lo < hi && nums[hi] == nums[hi - 1]) hi--;
                        lo++;
                        hi--;
                    }
                    else if (sum < 0) lo++;
                    else hi--;
                }
            }

            return result;
        }
    }

    // 🎤 GOOGLE DEMO — HARD onsite; tests insight progression under pressure.
    //   Q: "n bars of width 1, height[i]. How much water is trapped after rain?"
    //   Ex: [0,1,0,2,1,0,1,3,2,1,2,1] → 6
    //   Approaches: ① brute O(n²)  ② leftMax/rightMax arrays O(n)/O(n)  ③ two-pointer O(n)/O(1) ★  ④ monotonic stack O(n)
    //   🚩 Red flag: jumping straight to two-pointer — always voice the brute first to show progression.
    //   ✨ Strong hire: explain WHY smaller side is binding ("the OTHER side guarantees the wall").
    //   Follow-ups: LC 11 (container with most water), LC 84 (largest rectangle), LC 407 (2D → min-heap from boundary).
    // --- LC #42: Trapping Rain Water (Hard) — Two Pointers ---
    // GOAL: Given an elevation map, compute how much water it can trap.
    //
    // INTUITION: The water above position i is min(maxLeft[i], maxRight[i]) - height[i].
    //   Instead of precomputing both arrays, use two inward-moving pointers:
    //   the side with the SMALLER max height is the binding constraint and
    //   can be processed immediately.
    //
    // STEPS:
    //   left=0, right=n-1, leftMax=0, rightMax=0, water=0.
    //   While left < right:
    //     If height[left] < height[right]:
    //       leftMax = max(leftMax, height[left])
    //       water  += leftMax - height[left]   ← left side is the bottleneck
    //       left++
    //     Else:
    //       rightMax = max(rightMax, height[right])
    //       water   += rightMax - height[right]
    //       right--
    //
    // WHY IT WORKS: Whichever pointer points at the smaller height, its
    //   water level is fully determined by the max on its own side (the other
    //   side is guaranteed to be at least as tall).
    //
    // Time: O(n) | Space: O(1)
    public class TrappingRainWater
    {
        public int Trap(int[] height)
        {
            int left = 0, right = height.Length - 1;
            int leftMax = 0, rightMax = 0, water = 0;
            while (left < right)
            {
                if (height[left] < height[right])
                {
                    leftMax = Math.Max(leftMax, height[left]);
                    water += leftMax - height[left];
                    left++;
                }
                else
                {
                    rightMax = Math.Max(rightMax, height[right]);
                    water += rightMax - height[right];
                    right--;
                }
            }

            return water;
        }
    }

    // 🎤 GOOGLE DEMO — L5+ onsite; tests data-structure choice + invariants.
    //   Q: "Sliding window size k moves left→right. Return MAX of each window."
    //   Ex: nums=[1,3,-1,-3,5,3,6,7], k=3 → [3,3,5,5,6,7]
    //   Approaches: ① brute O(n·k)  ② max-heap w/ lazy delete O(n log k)  ③ monotonic deque O(n) ★
    //   🚩 Red flag: storing VALUES (not indices) in the deque — can't tell when to evict.
    //   ✨ Strong hire: state invariant "deque values are strictly decreasing; front is always window max".
    //   Follow-ups: LC 480 (median, two heaps), LC 862 (shortest subarray sum ≥ K), LC 1499.
    // --- LC #239: Sliding Window Maximum (Hard) — Monotonic Deque ---
    // GOAL: For each window of size k, find the maximum element. Return all maxima.
    //
    // INTUITION: Maintain a deque of indices whose values are in decreasing order.
    //   The front of the deque is always the maximum for the current window.
    //   Before adding a new element, remove from the back any indices whose
    //   values are smaller (they can never be the window max again).
    //
    // STEPS:
    //   For i = 0 .. n-1:
    //     1. Remove front if it has slid out of window (index < i - k + 1).
    //     2. Remove from back while deque.back value < nums[i].
    //     3. Append i to deque.
    //     4. If i >= k-1, record nums[deque.front] as window max.
    //
    // WHY IT WORKS: The deque is always sorted descending by value, and its
    //   front holds the index of the largest element still inside the window.
    //   Each index is pushed and popped at most once → O(n) total.
    //
    // Time: O(n) | Space: O(k)
    public class SlidingWindowMaximum
    {
        public int[] MaxSlidingWindow(int[] nums, int k)
        {
            var deque = new LinkedList<int>(); // stores indices
            var result = new int[nums.Length - k + 1];
            for (int i = 0; i < nums.Length; i++)
            {
                while (deque.Count > 0 && deque.First!.Value < i - k + 1) deque.RemoveFirst();
                while (deque.Count > 0 && nums[deque.Last!.Value] < nums[i]) deque.RemoveLast();
                deque.AddLast(i);
                if (i >= k - 1) result[i - k + 1] = nums[deque.First!.Value];
            }

            return result;
        }
    }

    // 🎤 GOOGLE DEMO — phone-screen opener; gauges baseline fluency + clarifying questions.
    //   Q: "Return true if ANY value appears ≥2 times in the array, else false."
    //   Ex: [1,2,3,1] → true  |  [1,2,3,4] → false
    //   Approaches: ① sort + scan O(n log n)/O(1)  ② HashSet O(n)/O(n) ★
    //   🚩 Red flag: not asking "is space a constraint?" — dictates which approach wins.
    //   ✨ Strong hire: use `seen.Add(n)` return value (no double-lookup); mention bitset for [0,n-1] range.
    //   Follow-ups: LC 219 (within distance k), LC 220 (within k AND value diff), LC 287 (1..n single dup).
    // --- LC #217: Contains Duplicate (Easy) — HashSet ---
    // GOAL: Return true if any value appears at least twice in the array.
    //
    // INTUITION: A HashSet remembers what we've already seen in O(1) per lookup.
    //   Walk the array once; the first time Add returns false, we've found a dup.
    //
    // Time: O(n) | Space: O(n)
    public class ContainsDuplicate
    {
        public bool HasDuplicate(int[] nums)
        {
            var seen = new HashSet<int>();           // tracks values seen so far
            foreach (int n in nums)                  // single pass through input
            {
                if (!seen.Add(n)) return true;       // Add returns false ⇒ duplicate found
            }
            return false;                            // no duplicates anywhere
        }
    }

    // 🎤 GOOGLE DEMO — warm-up; tests string-cleanliness + char-API knowledge.
    //   Q: "Is s a palindrome considering ONLY alphanumeric chars, case-insensitive?"
    //   Ex: "A man, a plan, a canal: Panama" → true   |   "race a car" → false
    //   Approaches: ① normalize (filter+lower) then compare reverse O(n)/O(n)  ② two-pointer in-place O(n)/O(1) ★
    //   🚩 Red flag: building a new string with regex — wastes memory and signals "unfamiliar with two-pointer".
    //   ✨ Strong hire: use `char.IsLetterOrDigit` + `char.ToLower` directly; mention unicode caveat.
    //   Follow-ups: LC 680 (delete ≤1 char), LC 5 (longest palindromic substring), LC 647 (count palindromic substrings).
    // --- LC #125: Valid Palindrome (Easy) — Two Pointers ---
    // GOAL: Check if a string is a palindrome considering only alphanumeric
    //       characters and ignoring case.
    //
    // INTUITION: Walk from both ends inward. Skip non-alphanumerics. Whenever
    //   both pointers land on letters/digits, they must match (case-insensitive).
    //
    // Time: O(n) | Space: O(1)
    public class ValidPalindrome
    {
        public bool IsPalindrome(string s)
        {
            int l = 0, r = s.Length - 1;                  // two pointers at each end
            while (l < r)                                 // move inward until they cross
            {
                // Skip any character on the LEFT that is not a letter or digit
                while (l < r && !char.IsLetterOrDigit(s[l])) l++;
                // Skip any character on the RIGHT that is not a letter or digit
                while (l < r && !char.IsLetterOrDigit(s[r])) r--;
                // Compare in lowercase so 'A' == 'a'
                if (char.ToLower(s[l]) != char.ToLower(s[r])) return false;
                l++;                                      // advance both pointers
                r--;
            }
            return true;                                  // every pair matched ⇒ palindrome
        }
    }

    // 🎤 GOOGLE DEMO — sneaky Kadane variant; trips candidates on negative numbers.
    //   Q: "Find the contiguous subarray with the LARGEST PRODUCT. Return that product."
    //   Ex: [2,3,-2,4] → 6  ([2,3])   |   [-2,0,-1] → 0  (zero resets)
    //   Approaches: ① brute O(n²)  ② DP tracking BOTH curMax and curMin O(n)/O(1) ★
    //   🚩 Red flag: copy-pasting Kadane and forgetting that neg×neg = pos (running MIN matters too).
    //   ✨ Strong hire: handle the negative case by saying "swap min/max when n<0" out loud.
    //   Follow-ups: LC 53 (sum), LC 628 (max product of 3), LC 713 (count subarrays w/ product < k).
    // --- LC #152: Maximum Product Subarray (Medium) — DP with Min/Max Tracking ---
    // GOAL: Find the contiguous subarray within an array that has the LARGEST PRODUCT.
    //
    // INTUITION: Unlike sum, a negative number can flip the sign — so the
    //   running MIN matters as much as the running MAX. A very negative min
    //   times a new negative element can become the new max.
    //   At each step track both the running max and running min ending here.
    //
    // Time: O(n) | Space: O(1)
    public class MaxProductSubarray
    {
        public int MaxProduct(int[] nums)
        {
            int maxSoFar = nums[0];                       // overall answer
            int curMax = nums[0], curMin = nums[0];       // best/worst product ending at current index

            for (int i = 1; i < nums.Length; i++)
            {
                int n = nums[i];
                // If n is negative, swapping max/min before extension is equivalent
                // to taking the (former) min * n as new max candidate.
                int tempMax = Math.Max(n, Math.Max(curMax * n, curMin * n));
                curMin       = Math.Min(n, Math.Min(curMax * n, curMin * n));
                curMax       = tempMax;                   // commit after computing both
                maxSoFar     = Math.Max(maxSoFar, curMax);// update global answer
            }
            return maxSoFar;
        }
    }

    // 🎤 GOOGLE DEMO — THE most common Google phone-screen problem.
    //   Q: "Length of the LONGEST SUBSTRING (contiguous) without repeating characters."
    //   Ex: "abcabcbb" → 3 ("abc")  |  "pwwkew" → 3 ("wke", NOT subseq)
    //   Approaches: ① brute O(n³)  ② sliding window + Set O(n)/O(charset)  ③ window + lastSeen map O(n)/O(charset) ★
    //   🚩 Red flag: confusing subSTRING vs subSEQUENCE — always clarify out loud.
    //   ✨ Strong hire: with lastSeen map, JUMP left to prev+1 (skip the Set-shrink loop entirely).
    //   Follow-ups: LC 340 (≤k distinct — same template), LC 76 (min window covering t), LC 992 (exactly k distinct).
    // --- LC #3: Longest Substring Without Repeating Characters (Medium) — Sliding Window ---
    // GOAL: Find the length of the longest substring with all unique characters.
    //
    // INTUITION: Maintain a window [left..right] that always contains unique chars.
    //   Use a Dictionary<char,int> mapping each character to its LAST seen index.
    //   When we encounter a repeat already inside the window, jump `left` past
    //   that previous occurrence.
    //
    // Time: O(n) | Space: O(min(n, charset))
    public class LongestSubstringWithoutRepeating
    {
        public int LengthOfLongestSubstring(string s)
        {
            var lastSeen = new Dictionary<char, int>();   // char → its most recent index
            int best = 0, left = 0;                       // window left edge & best length

            for (int right = 0; right < s.Length; right++)
            {
                char c = s[right];
                // If c was seen INSIDE current window, shrink the window from the left
                if (lastSeen.TryGetValue(c, out int prev) && prev >= left)
                    left = prev + 1;                      // skip past previous occurrence

                lastSeen[c] = right;                      // record/update last index of c
                best = Math.Max(best, right - left + 1);  // window size = right - left + 1
            }
            return best;
        }
    }

    // 🎤 GOOGLE DEMO — onsite favorite; tests matrix-index reasoning under in-place constraint.
    //   Q: "Rotate the n×n matrix 90° CLOCKWISE. IN-PLACE — no new matrix."
    //   Ex: [[1,2,3],[4,5,6],[7,8,9]] → [[7,4,1],[8,5,2],[9,6,3]]
    //   Approaches: ① copy to new matrix O(n²)/O(n²)  ② transpose + reverse rows O(n²)/O(1) ★  ③ rotate 4-cell groups in-place
    //   🚩 Red flag: nested loop using j<n (not j>i) — double-swaps every cell back to original.
    //   ✨ Strong hire: explain WHY transpose+reverse = 90° CW (and reverse cols = 90° CCW).
    //   Follow-ups: LC 54 (spiral traversal), LC 59 (spiral generate), LC 73 (set zeros in-place).
    // --- LC #48: Rotate Image (Medium) — Transpose + Reverse ---
    // GOAL: Rotate an n×n 2-D matrix 90° clockwise IN-PLACE.
    //
    // INTUITION: A 90° clockwise rotation = transpose along the main diagonal,
    //   then reverse each row. Both steps are in-place and easy to reason about.
    //
    //   Original     Transpose     Reverse rows
    //   1 2 3         1 4 7         7 4 1
    //   4 5 6   →    2 5 8    →   8 5 2
    //   7 8 9         3 6 9         9 6 3
    //
    // Time: O(n²) | Space: O(1)
    public class RotateImage
    {
        public void Rotate(int[][] matrix)
        {
            int n = matrix.Length;
            // Step 1: TRANSPOSE — swap matrix[i][j] with matrix[j][i] across diagonal
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)           // j starts at i+1 to avoid double-swap
                    (matrix[i][j], matrix[j][i]) = (matrix[j][i], matrix[i][j]);

            // Step 2: REVERSE each row left-to-right
            for (int i = 0; i < n; i++)
                Array.Reverse(matrix[i]);
        }
    }
}
