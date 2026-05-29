// ============================================================================
// Tests: Dynamic Programming
// Each [Fact] can be run independently in Rider's test runner
// ============================================================================

using GoogleInterviewPrep.LeetCode;
using Xunit;

namespace GoogleInterviewPrep.Tests;

public class DynamicProgrammingTests
{
    // --- LC #70: Climbing Stairs ---
    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 3)]
    [InlineData(5, 8)]
    [InlineData(10, 89)]
    public void ClimbStairs_Cases(int n, int expected)
    {
        Assert.Equal(expected, new ClimbingStairs().ClimbStairs(n));
    }

    // --- LC #198: House Robber ---
    [Fact]
    public void HouseRobber_BasicCase()
    {
        Assert.Equal(12, new HouseRobber().Rob(new[] { 2, 7, 9, 3, 1 }));
    }

    [Fact]
    public void HouseRobber_Adjacent()
    {
        Assert.Equal(4, new HouseRobber().Rob(new[] { 1, 2, 3, 1 }));
    }

    [Fact]
    public void HouseRobber_SingleHouse()
    {
        Assert.Equal(5, new HouseRobber().Rob(new[] { 5 }));
    }

    [Fact]
    public void HouseRobber_TwoHouses()
    {
        Assert.Equal(3, new HouseRobber().Rob(new[] { 2, 3 }));
    }

    // --- LC #322: Coin Change ---
    [Fact]
    public void CoinChange_BasicCase()
    {
        Assert.Equal(3, new CoinChange().Solve(new[] { 1, 5, 11 }, 15));
    }

    [Fact]
    public void CoinChange_Impossible()
    {
        Assert.Equal(-1, new CoinChange().Solve(new[] { 2 }, 3));
    }

    [Fact]
    public void CoinChange_ZeroAmount()
    {
        Assert.Equal(0, new CoinChange().Solve(new[] { 1, 2, 5 }, 0));
    }

    [Fact]
    public void CoinChange_ExactCoin()
    {
        Assert.Equal(1, new CoinChange().Solve(new[] { 1, 2, 5 }, 5));
    }

    [Fact]
    public void CoinChange_Standard()
    {
        Assert.Equal(3, new CoinChange().Solve(new[] { 1, 2, 5 }, 11));
    }

    // --- LC #62: Unique Paths ---
    [Theory]
    [InlineData(3, 7, 28)]
    [InlineData(3, 2, 3)]
    [InlineData(1, 1, 1)]
    [InlineData(7, 3, 28)]
    public void UniquePaths_Cases(int m, int n, int expected)
    {
        Assert.Equal(expected, new UniquePaths().Solve(m, n));
    }

    // --- LC #139: Word Break ---
    [Fact]
    public void WordBreak_BasicCase()
    {
        Assert.True(new WordBreak().Solve("leetcode", new List<string> { "leet", "code" }));
    }

    [Fact]
    public void WordBreak_Reuse()
    {
        Assert.True(new WordBreak().Solve("applepenapple", new List<string> { "apple", "pen" }));
    }

    [Fact]
    public void WordBreak_Impossible()
    {
        Assert.False(new WordBreak().Solve("catsandog", new List<string> { "cats", "dog", "sand", "and", "cat" }));
    }

    // --- LC #300: Longest Increasing Subsequence ---
    [Fact]
    public void LIS_BasicCase()
    {
        Assert.Equal(4, new LongestIncreasingSubsequence().LengthOfLIS(new[] { 10, 9, 2, 5, 3, 7, 101, 18 }));
    }

    [Fact]
    public void LIS_AllIncreasing()
    {
        Assert.Equal(4, new LongestIncreasingSubsequence().LengthOfLIS(new[] { 1, 2, 3, 4 }));
    }

    [Fact]
    public void LIS_AllDecreasing()
    {
        Assert.Equal(1, new LongestIncreasingSubsequence().LengthOfLIS(new[] { 4, 3, 2, 1 }));
    }

    [Fact]
    public void LIS_Duplicates()
    {
        Assert.Equal(1, new LongestIncreasingSubsequence().LengthOfLIS(new[] { 7, 7, 7, 7 }));
    }

    // --- LC #91: Decode Ways ---
    [Theory]
    [InlineData("12", 2)]
    [InlineData("226", 3)]
    [InlineData("06", 0)]
    [InlineData("11106", 2)]
    [InlineData("10", 1)]
    public void DecodeWays_Cases(string s, int expected)
    {
        Assert.Equal(expected, new DecodeWays().NumDecodings(s));
    }

    // --- LC #1143: Longest Common Subsequence ---
    [Theory]
    [InlineData("abcde", "ace", 3)]
    [InlineData("abc", "abc", 3)]
    [InlineData("abc", "def", 0)]
    public void LongestCommonSubsequence_Cases(string a, string b, int expected)
    {
        Assert.Equal(expected, new LongestCommonSubsequence().LCSLength(a, b));
    }

    // --- LC #72: Edit Distance ---
    [Theory]
    [InlineData("horse", "ros", 3)]
    [InlineData("intention", "execution", 5)]
    [InlineData("", "abc", 3)]
    [InlineData("abc", "", 3)]
    public void EditDistance_Cases(string a, string b, int expected)
    {
        Assert.Equal(expected, new EditDistance().MinDistance(a, b));
    }

    // --- LC #221: Maximal Square ---
    [Fact]
    public void MaximalSquare_Basic()
    {
        var matrix = new[]
        {
            new[] { '1','0','1','0','0' },
            new[] { '1','0','1','1','1' },
            new[] { '1','1','1','1','1' },
            new[] { '1','0','0','1','0' }
        };
        Assert.Equal(4, new MaximalSquare().MaximalSquareArea(matrix));
    }

    [Fact]
    public void MaximalSquare_AllZeros()
    {
        var matrix = new[] { new[] { '0', '0' }, new[] { '0', '0' } };
        Assert.Equal(0, new MaximalSquare().MaximalSquareArea(matrix));
    }

    // --- LC #416: Partition Equal Subset Sum ---
    [Theory]
    [InlineData(new[] { 1, 5, 11, 5 }, true)]
    [InlineData(new[] { 1, 2, 3, 5 }, false)]
    [InlineData(new[] { 1, 1 }, true)]
    public void PartitionEqualSubsetSum_Cases(int[] nums, bool expected)
    {
        Assert.Equal(expected, new PartitionEqualSubsetSum().CanPartition(nums));
    }
}
