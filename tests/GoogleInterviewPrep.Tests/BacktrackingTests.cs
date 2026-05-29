// ============================================================================
// Tests: Backtracking
// Each [Fact] can be run independently in Rider's test runner
// ============================================================================

using GoogleInterviewPrep.LeetCode;
using Xunit;

namespace GoogleInterviewPrep.Tests;

public class BacktrackingTests
{
    // --- LC #78: Subsets ---
    [Fact]
    public void Subsets_ThreeElements()
    {
        var result = new Subsets().Solve(new[] { 1, 2, 3 });
        Assert.Equal(8, result.Count); // 2^3 = 8
    }

    [Fact]
    public void Subsets_SingleElement()
    {
        var result = new Subsets().Solve(new[] { 0 });
        Assert.Equal(2, result.Count); // [], [0]
    }

    [Fact]
    public void Subsets_ContainsEmptySet()
    {
        var result = new Subsets().Solve(new[] { 1, 2 });
        Assert.Contains(result, s => s.Count == 0);
    }

    [Fact]
    public void Subsets_ContainsFullSet()
    {
        var result = new Subsets().Solve(new[] { 1, 2, 3 });
        Assert.Contains(result, s => s.Count == 3 && s[0] == 1 && s[1] == 2 && s[2] == 3);
    }

    // --- LC #46: Permutations ---
    [Fact]
    public void Permutations_ThreeElements()
    {
        var result = new Permutations().Permute(new[] { 1, 2, 3 });
        Assert.Equal(6, result.Count); // 3! = 6
    }

    [Fact]
    public void Permutations_TwoElements()
    {
        var result = new Permutations().Permute(new[] { 0, 1 });
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void Permutations_SingleElement()
    {
        var result = new Permutations().Permute(new[] { 1 });
        Assert.Single(result);
        Assert.Equal(new[] { 1 }, result[0]);
    }

    // --- LC #39: Combination Sum ---
    [Fact]
    public void CombinationSum_BasicCase()
    {
        var result = new CombinationSum().Solve(new[] { 2, 3, 6, 7 }, 7);
        Assert.Equal(2, result.Count); // [2,2,3] and [7]
    }

    [Fact]
    public void CombinationSum_WithReuse()
    {
        var result = new CombinationSum().Solve(new[] { 2, 3, 5 }, 8);
        Assert.Equal(3, result.Count); // [2,2,2,2], [2,3,3], [3,5]
    }

    [Fact]
    public void CombinationSum_NoSolution()
    {
        var result = new CombinationSum().Solve(new[] { 2 }, 1);
        Assert.Empty(result);
    }

    // --- LC #51: N-Queens ---
    [Fact]
    public void NQueens_4x4()
    {
        var result = new NQueens().SolveNQueens(4);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void NQueens_1x1()
    {
        var result = new NQueens().SolveNQueens(1);
        Assert.Single(result);
        Assert.Equal("Q", result[0][0]);
    }

    [Fact]
    public void NQueens_8x8()
    {
        var result = new NQueens().SolveNQueens(8);
        Assert.Equal(92, result.Count);
    }

    [Fact]
    public void NQueens_ValidSolution()
    {
        var result = new NQueens().SolveNQueens(4);
        // Each solution should have 4 rows with exactly one Q per row
        foreach (var solution in result)
        {
            Assert.Equal(4, solution.Count);
            foreach (var row in solution)
            {
                Assert.Equal(4, row.Length);
                Assert.Equal(1, row.Count(c => c == 'Q'));
            }
        }
    }

    // --- LC #17: Letter Combinations of a Phone Number ---
    [Fact]
    public void LetterCombinations_23()
    {
        var result = new LetterCombinationsOfPhone().LetterCombinations("23");
        Assert.Equal(9, result.Count);                              // 3 letters * 3 letters
        Assert.Contains("ad", result);
        Assert.Contains("cf", result);
    }

    [Fact]
    public void LetterCombinations_Empty()
    {
        Assert.Empty(new LetterCombinationsOfPhone().LetterCombinations(""));
    }

    // --- LC #131: Palindrome Partitioning ---
    [Fact]
    public void PalindromePartitioning_Aab()
    {
        var result = new PalindromePartitioning().Partition("aab");
        Assert.Equal(2, result.Count);                              // [[a,a,b], [aa,b]]
        Assert.Contains(result, p => p.Count == 3 && p[0] == "a" && p[1] == "a" && p[2] == "b");
        Assert.Contains(result, p => p.Count == 2 && p[0] == "aa" && p[1] == "b");
    }

    // --- LC #212: Word Search II ---
    [Fact]
    public void WordSearchII_Basic()
    {
        var board = new[]
        {
            new[] { 'o','a','a','n' },
            new[] { 'e','t','a','e' },
            new[] { 'i','h','k','r' },
            new[] { 'i','f','l','v' }
        };
        var words = new[] { "oath", "pea", "eat", "rain" };
        var found = new WordSearchII().FindWords(board, words);
        Assert.Contains("oath", found);
        Assert.Contains("eat", found);
        Assert.DoesNotContain("pea", found);
        Assert.DoesNotContain("rain", found);
    }
}
