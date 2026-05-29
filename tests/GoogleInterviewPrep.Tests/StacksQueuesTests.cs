// ============================================================================
// Tests: Stacks & Queues
// Each [Fact] can be run independently in Rider's test runner
// ============================================================================

using GoogleInterviewPrep.LeetCode;
using Xunit;

namespace GoogleInterviewPrep.Tests;

public class StacksQueuesTests
{
    // --- LC #20: Valid Parentheses ---
    [Theory]
    [InlineData("()", true)]
    [InlineData("()[]{}", true)]
    [InlineData("(]", false)]
    [InlineData("([)]", false)]
    [InlineData("{[]}", true)]
    [InlineData("", true)]
    [InlineData("(", false)]
    public void ValidParentheses_Cases(string input, bool expected)
    {
        Assert.Equal(expected, new ValidParentheses().IsValid(input));
    }

    // --- LC #155: Min Stack ---
    [Fact]
    public void MinStack_BasicOperations()
    {
        var ms = new MinStack();
        ms.Push(-2);
        ms.Push(0);
        ms.Push(-3);
        Assert.Equal(-3, ms.GetMin());
        ms.Pop();
        Assert.Equal(0, ms.Top());
        Assert.Equal(-2, ms.GetMin());
    }

    [Fact]
    public void MinStack_AllSameValues()
    {
        var ms = new MinStack();
        ms.Push(1);
        ms.Push(1);
        ms.Push(1);
        Assert.Equal(1, ms.GetMin());
        ms.Pop();
        Assert.Equal(1, ms.GetMin());
    }

    [Fact]
    public void MinStack_DecreasingOrder()
    {
        var ms = new MinStack();
        ms.Push(3);
        ms.Push(2);
        ms.Push(1);
        Assert.Equal(1, ms.GetMin());
        ms.Pop();
        Assert.Equal(2, ms.GetMin());
        ms.Pop();
        Assert.Equal(3, ms.GetMin());
    }

    // --- LC #739: Daily Temperatures ---
    [Fact]
    public void DailyTemperatures_BasicCase()
    {
        Assert.Equal(new[] { 1, 1, 4, 2, 1, 1, 0, 0 },
            new DailyTemperatures().Solve(new[] { 73, 74, 75, 71, 69, 72, 76, 73 }));
    }

    [Fact]
    public void DailyTemperatures_AllDecreasing()
    {
        Assert.Equal(new[] { 0, 0, 0 },
            new DailyTemperatures().Solve(new[] { 76, 75, 74 }));
    }

    [Fact]
    public void DailyTemperatures_AllIncreasing()
    {
        Assert.Equal(new[] { 1, 1, 1, 0 },
            new DailyTemperatures().Solve(new[] { 70, 71, 72, 73 }));
    }

    // --- LC #84: Largest Rectangle in Histogram ---
    [Fact]
    public void LargestRectangle_BasicCase()
    {
        Assert.Equal(10, new LargestRectangleInHistogram().LargestRectangleArea(new[] { 2, 1, 5, 6, 2, 3 }));
    }

    [Fact]
    public void LargestRectangle_SingleBar()
    {
        Assert.Equal(2, new LargestRectangleInHistogram().LargestRectangleArea(new[] { 2 }));
    }

    [Fact]
    public void LargestRectangle_EqualBars()
    {
        Assert.Equal(9, new LargestRectangleInHistogram().LargestRectangleArea(new[] { 3, 3, 3 }));
    }

    [Fact]
    public void LargestRectangle_TwoBars()
    {
        Assert.Equal(4, new LargestRectangleInHistogram().LargestRectangleArea(new[] { 2, 4 }));
    }

    // --- LC #150: Evaluate Reverse Polish Notation ---
    [Theory]
    [InlineData(new[] { "2", "1", "+", "3", "*" }, 9)]            // (2+1)*3
    [InlineData(new[] { "4", "13", "5", "/", "+" }, 6)]           // 4 + (13/5)
    [InlineData(new[] { "10", "6", "9", "3", "+", "-11", "*", "/", "*", "17", "+", "5", "+" }, 22)]
    public void EvaluateRPN_Cases(string[] tokens, int expected)
    {
        Assert.Equal(expected, new EvaluateRPN().EvalRPN(tokens));
    }

    // --- LC #22: Generate Parentheses ---
    [Fact]
    public void GenerateParentheses_N3()
    {
        var result = new GenerateParentheses().Generate(3);
        Assert.Equal(5, result.Count);                              // Catalan(3) = 5
        Assert.Contains("((()))", result);
        Assert.Contains("(()())", result);
        Assert.Contains("(())()", result);
        Assert.Contains("()(())", result);
        Assert.Contains("()()()", result);
    }

    [Fact]
    public void GenerateParentheses_N1()
    {
        Assert.Equal(new[] { "()" }, new GenerateParentheses().Generate(1));
    }

    // --- LC #232: Queue using Stacks ---
    [Fact]
    public void MyQueue_FIFOBehavior()
    {
        var q = new MyQueue();
        q.Push(1); q.Push(2); q.Push(3);
        Assert.Equal(1, q.Peek());                                  // oldest stays on top
        Assert.Equal(1, q.Pop());
        Assert.Equal(2, q.Pop());
        q.Push(4);
        Assert.Equal(3, q.Pop());                                   // FIFO holds across mixed ops
        Assert.Equal(4, q.Pop());
        Assert.True(q.Empty());
    }
}
