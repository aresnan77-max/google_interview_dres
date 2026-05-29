// ============================================================================
// Tests: HashMaps & Sets
// Each [Fact] can be run independently in Rider's test runner
// ============================================================================

using GoogleInterviewPrep.LeetCode;
using Xunit;

namespace GoogleInterviewPrep.Tests;

public class HashMapsTests
{
    // --- LC #49: Group Anagrams ---
    [Fact]
    public void GroupAnagrams_BasicCase()
    {
        var result = new GroupAnagrams().Solve(new[] { "eat", "tea", "tan", "ate", "nat", "bat" });
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void GroupAnagrams_SingleString()
    {
        var result = new GroupAnagrams().Solve(new[] { "a" });
        Assert.Single(result);
    }

    [Fact]
    public void GroupAnagrams_EmptyString()
    {
        var result = new GroupAnagrams().Solve(new[] { "" });
        Assert.Single(result);
    }

    // --- LC #128: Longest Consecutive Sequence ---
    [Fact]
    public void LongestConsecutive_BasicCase()
    {
        Assert.Equal(4, new LongestConsecutiveSequence().LongestConsecutive(new[] { 100, 4, 200, 1, 3, 2 }));
    }

    [Fact]
    public void LongestConsecutive_LongerSequence()
    {
        Assert.Equal(9, new LongestConsecutiveSequence().LongestConsecutive(new[] { 0, 3, 7, 2, 5, 8, 4, 6, 0, 1 }));
    }

    [Fact]
    public void LongestConsecutive_Empty()
    {
        Assert.Equal(0, new LongestConsecutiveSequence().LongestConsecutive(Array.Empty<int>()));
    }

    [Fact]
    public void LongestConsecutive_Duplicates()
    {
        Assert.Equal(3, new LongestConsecutiveSequence().LongestConsecutive(new[] { 1, 2, 0, 1 }));
    }

    // --- LC #347: Top K Frequent Elements ---
    [Fact]
    public void TopKFrequent_BasicCase()
    {
        var result = new TopKFrequentElements().TopKFrequent(new[] { 1, 1, 1, 2, 2, 3 }, 2);
        Assert.Contains(1, result);
        Assert.Contains(2, result);
        Assert.Equal(2, result.Length);
    }

    [Fact]
    public void TopKFrequent_SingleElement()
    {
        var result = new TopKFrequentElements().TopKFrequent(new[] { 1 }, 1);
        Assert.Equal(new[] { 1 }, result);
    }

    // --- LC #36: Valid Sudoku ---
    [Fact]
    public void ValidSudoku_ValidBoard()
    {
        char[][] board = {
            new[] { '5','3','.','.','7','.','.','.','.' },
            new[] { '6','.','.','1','9','5','.','.','.' },
            new[] { '.','9','8','.','.','.','.','6','.' },
            new[] { '8','.','.','.','6','.','.','.','3' },
            new[] { '4','.','.','8','.','3','.','.','1' },
            new[] { '7','.','.','.','2','.','.','.','6' },
            new[] { '.','6','.','.','.','.','2','8','.' },
            new[] { '.','.','.','4','1','9','.','.','5' },
            new[] { '.','.','.','.','8','.','.','7','9' }
        };
        Assert.True(new ValidSudoku().IsValidSudoku(board));
    }

    [Fact]
    public void ValidSudoku_InvalidBoard_DuplicateInRow()
    {
        char[][] board = {
            new[] { '8','3','.','.','7','.','.','.','.' },
            new[] { '6','.','.','1','9','5','.','.','.' },
            new[] { '.','9','8','.','.','.','.','6','.' },
            new[] { '8','.','.','.','6','.','.','.','3' },
            new[] { '4','.','.','8','.','3','.','.','1' },
            new[] { '7','.','.','.','2','.','.','.','6' },
            new[] { '.','6','.','.','.','.','2','8','.' },
            new[] { '.','.','.','4','1','9','.','.','5' },
            new[] { '.','.','.','.','8','.','.','7','9' }
        };
        Assert.False(new ValidSudoku().IsValidSudoku(board));
    }

    // --- LC #242: Valid Anagram ---
    [Theory]
    [InlineData("anagram", "nagaram", true)]
    [InlineData("rat", "car", false)]
    [InlineData("ab", "a", false)]
    public void ValidAnagram_Cases(string s, string t, bool expected)
    {
        Assert.Equal(expected, new ValidAnagram().IsAnagram(s, t));
    }

    // --- LC #560: Subarray Sum Equals K ---
    [Theory]
    [InlineData(new[] { 1, 1, 1 }, 2, 2)]
    [InlineData(new[] { 1, 2, 3 }, 3, 2)]
    [InlineData(new[] { 1, -1, 0 }, 0, 3)]
    public void SubarraySumEqualsK_Cases(int[] nums, int k, int expected)
    {
        Assert.Equal(expected, new SubarraySumEqualsK().SubarraySum(nums, k));
    }

    // --- LC #387: First Unique Character ---
    [Theory]
    [InlineData("leetcode", 0)]
    [InlineData("loveleetcode", 2)]
    [InlineData("aabb", -1)]
    public void FirstUniqueChar_Cases(string s, int expected)
    {
        Assert.Equal(expected, new FirstUniqueCharacter().FirstUniqChar(s));
    }
}
