// ============================================================================
// Tests: Arrays & Strings
// Each [Fact] can be run independently in Rider's test runner
// ============================================================================

using GoogleInterviewPrep.LeetCode;
using Xunit;

namespace GoogleInterviewPrep.Tests;

public class ArraysStringsTests
{
    // --- LC #1: Two Sum ---
    [Fact]
    public void TwoSum_BasicCase()
    {
        var result = new TwoSum().Solve(new[] { 2, 7, 11, 15 }, 9);
        Assert.Equal(new[] { 0, 1 }, result);
    }

    [Fact]
    public void TwoSum_MiddleElements()
    {
        var result = new TwoSum().Solve(new[] { 3, 2, 4 }, 6);
        Assert.Equal(new[] { 1, 2 }, result);
    }

    [Fact]
    public void TwoSum_SameElement()
    {
        var result = new TwoSum().Solve(new[] { 3, 3 }, 6);
        Assert.Equal(new[] { 0, 1 }, result);
    }

    // --- LC #121: Best Time to Buy and Sell Stock ---
    [Fact]
    public void MaxProfit_BasicCase()
    {
        Assert.Equal(5, new BestTimeToBuyAndSellStock().MaxProfit(new[] { 7, 1, 5, 3, 6, 4 }));
    }

    [Fact]
    public void MaxProfit_Decreasing_ReturnsZero()
    {
        Assert.Equal(0, new BestTimeToBuyAndSellStock().MaxProfit(new[] { 7, 6, 4, 3, 1 }));
    }

    [Fact]
    public void MaxProfit_SingleDay()
    {
        Assert.Equal(0, new BestTimeToBuyAndSellStock().MaxProfit(new[] { 5 }));
    }

    // --- LC #53: Maximum Subarray (Kadane's) ---
    [Fact]
    public void MaxSubArray_MixedArray()
    {
        Assert.Equal(6, new MaxSubarray().MaxSubArray(new[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 }));
    }

    [Fact]
    public void MaxSubArray_AllNegative()
    {
        Assert.Equal(-1, new MaxSubarray().MaxSubArray(new[] { -3, -2, -1, -5 }));
    }

    [Fact]
    public void MaxSubArray_SingleElement()
    {
        Assert.Equal(1, new MaxSubarray().MaxSubArray(new[] { 1 }));
    }

    // --- LC #238: Product of Array Except Self ---
    [Fact]
    public void ProductExceptSelf_BasicCase()
    {
        Assert.Equal(new[] { 24, 12, 8, 6 }, new ProductExceptSelf().Solve(new[] { 1, 2, 3, 4 }));
    }

    [Fact]
    public void ProductExceptSelf_WithZero()
    {
        Assert.Equal(new[] { 0, 0, 9, 0, 0 }, new ProductExceptSelf().Solve(new[] { -1, 1, 0, -3, 3 }));
    }

    // --- LC #56: Merge Intervals ---
    [Fact]
    public void MergeIntervals_OverlappingIntervals()
    {
        var result = new MergeIntervals().Merge(new[] { new[] { 1, 3 }, new[] { 2, 6 }, new[] { 8, 10 }, new[] { 15, 18 } });
        Assert.Equal(3, result.Length);
        Assert.Equal(new[] { 1, 6 }, result[0]);
        Assert.Equal(new[] { 8, 10 }, result[1]);
        Assert.Equal(new[] { 15, 18 }, result[2]);
    }

    [Fact]
    public void MergeIntervals_ContainedInterval()
    {
        var result = new MergeIntervals().Merge(new[] { new[] { 1, 4 }, new[] { 2, 3 } });
        Assert.Single(result);
        Assert.Equal(new[] { 1, 4 }, result[0]);
    }

    // --- LC #15: 3Sum ---
    [Fact]
    public void ThreeSum_BasicCase()
    {
        var result = new ThreeSum().Solve(new[] { -1, 0, 1, 2, -1, -4 });
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void ThreeSum_AllZeros()
    {
        var result = new ThreeSum().Solve(new[] { 0, 0, 0 });
        Assert.Single(result);
        Assert.Equal(new List<int> { 0, 0, 0 }, result[0]);
    }

    [Fact]
    public void ThreeSum_NoSolution()
    {
        var result = new ThreeSum().Solve(new[] { 1, 2, -2, -1 });
        Assert.Empty(result);
    }

    // --- LC #42: Trapping Rain Water ---
    [Fact]
    public void Trap_BasicCase()
    {
        Assert.Equal(6, new TrappingRainWater().Trap(new[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 }));
    }

    [Fact]
    public void Trap_VariedHeights()
    {
        Assert.Equal(9, new TrappingRainWater().Trap(new[] { 4, 2, 0, 3, 2, 5 }));
    }

    [Fact]
    public void Trap_Flat()
    {
        Assert.Equal(0, new TrappingRainWater().Trap(new[] { 1, 1, 1, 1 }));
    }

    // --- LC #239: Sliding Window Maximum ---
    [Fact]
    public void MaxSlidingWindow_BasicCase()
    {
        Assert.Equal(new[] { 3, 3, 5, 5, 6, 7 },
            new SlidingWindowMaximum().MaxSlidingWindow(new[] { 1, 3, -1, -3, 5, 3, 6, 7 }, 3));
    }

    [Fact]
    public void MaxSlidingWindow_SingleElementWindow()
    {
        Assert.Equal(new[] { 1, -1 },
            new SlidingWindowMaximum().MaxSlidingWindow(new[] { 1, -1 }, 1));
    }

    [Fact]
    public void MaxSlidingWindow_FullArrayWindow()
    {
        Assert.Equal(new[] { 7 },
            new SlidingWindowMaximum().MaxSlidingWindow(new[] { 1, 3, -1, -3, 5, 3, 6, 7 }, 8));
    }

    // --- LC #217: Contains Duplicate ---
    [Theory]
    [InlineData(new[] { 1, 2, 3, 1 }, true)]
    [InlineData(new[] { 1, 2, 3, 4 }, false)]
    [InlineData(new[] { 1, 1, 1, 3, 3, 4, 3, 2, 4, 2 }, true)]
    public void ContainsDuplicate_Cases(int[] nums, bool expected)
    {
        Assert.Equal(expected, new ContainsDuplicate().HasDuplicate(nums));
    }

    // --- LC #125: Valid Palindrome ---
    [Theory]
    [InlineData("A man, a plan, a canal: Panama", true)]
    [InlineData("race a car", false)]
    [InlineData(" ", true)]
    public void ValidPalindrome_Cases(string s, bool expected)
    {
        Assert.Equal(expected, new ValidPalindrome().IsPalindrome(s));
    }

    // --- LC #152: Maximum Product Subarray ---
    [Theory]
    [InlineData(new[] { 2, 3, -2, 4 }, 6)]
    [InlineData(new[] { -2, 0, -1 }, 0)]
    [InlineData(new[] { -2, 3, -4 }, 24)]
    public void MaxProductSubarray_Cases(int[] nums, int expected)
    {
        Assert.Equal(expected, new MaxProductSubarray().MaxProduct(nums));
    }

    // --- LC #3: Longest Substring Without Repeating Characters ---
    [Theory]
    [InlineData("abcabcbb", 3)]
    [InlineData("bbbbb", 1)]
    [InlineData("pwwkew", 3)]
    [InlineData("", 0)]
    public void LongestSubstringWithoutRepeating_Cases(string s, int expected)
    {
        Assert.Equal(expected, new LongestSubstringWithoutRepeating().LengthOfLongestSubstring(s));
    }

    // --- LC #48: Rotate Image ---
    [Fact]
    public void RotateImage_3x3()
    {
        var matrix = new[]
        {
            new[] { 1, 2, 3 },
            new[] { 4, 5, 6 },
            new[] { 7, 8, 9 }
        };
        new RotateImage().Rotate(matrix);
        Assert.Equal(new[] { 7, 4, 1 }, matrix[0]);
        Assert.Equal(new[] { 8, 5, 2 }, matrix[1]);
        Assert.Equal(new[] { 9, 6, 3 }, matrix[2]);
    }
}
