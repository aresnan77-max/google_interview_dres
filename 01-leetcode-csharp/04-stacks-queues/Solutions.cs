// ============================================================================
// Category: Stacks & Queues — Google Interview Prep
// Problems: ValidParentheses(#20), MinStack(#155),
//           DailyTemperatures(#739), LargestRectangleInHistogram(#84)
// ============================================================================

using System;
using System.Collections.Generic;

namespace GoogleInterviewPrep.LeetCode
{
    // --- LC #20: Valid Parentheses (Easy) — Stack ---
    // Time: O(n) | Space: O(n)
    public class ValidParentheses
    {
        public bool IsValid(string s)
        {
            var stack = new Stack<char>();
            var map = new Dictionary<char, char> { { ')', '(' }, { '}', '{' }, { ']', '[' } };
            foreach (char c in s)
            {
                if (map.ContainsKey(c))
                {
                    if (stack.Count == 0 || stack.Pop() != map[c]) return false;
                }
                else stack.Push(c);
            }
            return stack.Count == 0;
        }
    }

    // --- LC #155: Min Stack (Medium) — Auxiliary Stack ---
    // Time: O(1) all ops | Space: O(n)
    public class MinStack
    {
        private readonly Stack<int> _stack = new();
        private readonly Stack<int> _minStack = new();
        public void Push(int val) { _stack.Push(val); _minStack.Push(_minStack.Count == 0 ? val : Math.Min(val, _minStack.Peek())); }
        public void Pop() { _stack.Pop(); _minStack.Pop(); }
        public int Top() => _stack.Peek();
        public int GetMin() => _minStack.Peek();
    }

    // --- LC #739: Daily Temperatures (Medium) — Monotonic Stack ---
    // Time: O(n) | Space: O(n)
    public class DailyTemperatures
    {
        public int[] Solve(int[] temperatures)
        {
            int n = temperatures.Length;
            int[] result = new int[n];
            var stack = new Stack<int>(); // stores indices
            for (int i = 0; i < n; i++)
            {
                while (stack.Count > 0 && temperatures[stack.Peek()] < temperatures[i])
                {
                    int idx = stack.Pop();
                    result[idx] = i - idx;
                }
                stack.Push(i);
            }
            return result;
        }
    }

    // --- LC #84: Largest Rectangle in Histogram (Hard) — Monotonic Stack ---
    // Time: O(n) | Space: O(n)
    public class LargestRectangleInHistogram
    {
        public int LargestRectangleArea(int[] heights)
        {
            var stack = new Stack<int>();
            int maxArea = 0, n = heights.Length;
            for (int i = 0; i <= n; i++)
            {
                int h = (i == n) ? 0 : heights[i];
                while (stack.Count > 0 && h < heights[stack.Peek()])
                {
                    int height = heights[stack.Pop()];
                    int width = stack.Count == 0 ? i : i - stack.Peek() - 1;
                    maxArea = Math.Max(maxArea, height * width);
                }
                stack.Push(i);
            }
            return maxArea;
        }
    }
}
