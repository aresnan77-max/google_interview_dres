// ============================================================================
// Category: Arrays & Strings — Google Interview Prep
// Problems: TwoSum(#1), BestTimeToBuyAndSellStock(#121), MaxSubarray(#53),
//           ProductExceptSelf(#238), MergeIntervals(#56), ThreeSum(#15),
//           TrappingRainWater(#42), SlidingWindowMaximum(#239)
// ============================================================================

using System;
using System.Collections.Generic;
using System.Linq;

namespace GoogleInterviewPrep.LeetCode
{
    // --- LC #1: Two Sum (Easy) — HashMap Pattern ---
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

    // --- LC #121: Best Time to Buy and Sell Stock (Easy) — Single Pass ---
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

    // --- LC #53: Maximum Subarray (Medium) — Kadane's Algorithm ---
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

    // --- LC #238: Product of Array Except Self (Medium) — Prefix/Suffix ---
    // Time: O(n) | Space: O(1) excluding output
    public class ProductExceptSelf
    {
        public int[] Solve(int[] nums)
        {
            int n = nums.Length;
            int[] result = new int[n];
            result[0] = 1;
            for (int i = 1; i < n; i++) result[i] = result[i - 1] * nums[i - 1];
            int suffix = 1;
            for (int i = n - 1; i >= 0; i--) { result[i] *= suffix; suffix *= nums[i]; }
            return result;
        }
    }

    // --- LC #56: Merge Intervals (Medium) — Sort + Merge ---
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

    // --- LC #15: 3Sum (Medium) — Sort + Two Pointers ---
    // Time: O(n^2) | Space: O(1) excluding output
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
                        lo++; hi--;
                    }
                    else if (sum < 0) lo++;
                    else hi--;
                }
            }
            return result;
        }
    }

    // --- LC #42: Trapping Rain Water (Hard) — Two Pointers ---
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

    // --- LC #239: Sliding Window Maximum (Hard) — Monotonic Deque ---
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
}
