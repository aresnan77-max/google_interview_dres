// ============================================================================
// Category: Stacks & Queues — Google Interview Prep
// Problems: ValidParentheses(#20), MinStack(#155),
//           DailyTemperatures(#739), LargestRectangleInHistogram(#84),
//           EvaluateRPN(#150), GenerateParentheses(#22),
//           ImplementQueueUsingStacks(#232)
// ============================================================================

using System;
using System.Collections.Generic;

namespace GoogleInterviewPrep.LeetCode
{
    // 🎤 GOOGLE DEMO — phone-screen classic; verifies the stack instinct.
    //   Q: "Is the string of brackets '()[]{}' VALID (matched types, correct nesting)?"
    //   Ex: "()[]{}" → true  |  "(]" → false  |  "([)]" → false  |  "{[]}" → true
    //   Approaches: ① stack push opens, pop+match on closes O(n)/O(n) ★  ② char-counting (FAILS on "([)]")
    //   🚩 Red flag: tracking open/close COUNTS instead of order — silently passes invalid nestings.
    //   ✨ Strong hire: map close→open in a dict; pop & compare in one line; check stack EMPTY at end.
    //   Follow-ups: LC 22 (generate), LC 32 (longest valid — stack of indices), LC 921 (min add to make valid).
    // --- LC #20: Valid Parentheses (Easy) — Stack ---
    // GOAL: Return true iff every opening bracket is closed by the correct
    //       closing bracket in proper nesting order.
    //
    // INTUITION: Brackets must be closed in LIFO order — the most recently
    //   opened bracket must be closed first. A stack naturally models this.
    //
    // STEPS:
    //   map = { ')':'(', '}':'{', ']':'[' }.
    //   For each char c:
    //     If c is a closing bracket:
    //       stack is empty OR top != map[c] → return false.
    //       Else pop.
    //     If c is an opening bracket: push c.
    //   Return stack.IsEmpty (all opened brackets were closed).
    //
    // WHY IT WORKS: Every closing bracket must match the most recent unclosed
    //   opening bracket — exactly what a stack tracks.
    //
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

    // 🎤 GOOGLE DEMO — L4 onsite favorite; data-structure design under O(1) constraint.
    //   Q: "Design a stack with push/pop/top/getMin all O(1)."
    //   Ex: push(-2), push(0), push(-3); getMin→-3; pop(); top→0; getMin→2
    //   Approaches: ① parallel min-stack pushed every op O(n) space ★  ② push only on new-min (handle dups carefully)  ③ encode diff to current min (O(1) extra)
    //   🚩 Red flag: scanning the stack for min on getMin — violates O(1).
    //   ✨ Strong hire: handle DUPLICATE mins correctly (push to min-stack when val ≤ currentMin).
    //   Follow-ups: LC 716 (max stack), LC 895 (freq stack), LC 1381 (custom stack w/ batch increment).
    // --- LC #155: Min Stack (Medium) — Auxiliary Stack ---
    // GOAL: Design a stack that supports Push, Pop, Top, and GetMin —
    //       all in O(1) time.
    //
    // INTUITION: A single stack can't track the minimum after pops. Mirror
    //   the main stack with a second "min stack" that at each level stores
    //   the minimum of everything pushed so far up to that depth.
    //
    // STEPS:
    //   Push(val):
    //     push val onto _stack.
    //     push min(val, _minStack.Peek()) onto _minStack.
    //   Pop():
    //     pop both stacks in sync.
    //   GetMin(): return _minStack.Peek().
    //
    // WHY IT WORKS: _minStack[i] = minimum of the original stack's bottom i+1
    //   elements. After a pop the min reverts to whatever it was before —
    //   exactly what the shadow stack tracks.
    //
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

    // 🎤 GOOGLE DEMO — the canonical "next greater element" pattern at Google.
    //   Q: "For each day, how many DAYS until a warmer temperature? 0 if none."
    //   Ex: [73,74,75,71,69,72,76,73] → [1,1,4,2,1,1,0,0]
    //   Approaches: ① brute O(n²)  ② monotonic DECREASING stack of INDICES O(n)/O(n) ★  ③ reverse-scan w/ jump table
    //   🚩 Red flag: storing VALUES on the stack — can't compute the day-gap.
    //   ✨ Strong hire: state invariant "stack holds indices with strictly decreasing temps".
    //   Follow-ups: LC 496/503/556 (next greater element family), LC 901 (online stock span).
    // --- LC #739: Daily Temperatures (Medium) — Monotonic Stack ---
    // GOAL: For each day, return how many days until a warmer temperature.
    //       0 if no warmer day exists.
    //
    // INTUITION: Maintain a stack of indices whose temperatures are still
    //   "waiting" for a warmer day. When a new temperature is higher than
    //   the top, that day's wait is resolved: answer = currentIndex - stackTop.
    //
    // STEPS:
    //   stack = empty (stores indices).
    //   For i = 0 .. n-1:
    //     While stack non-empty AND temps[stack.top] < temps[i]:
    //       idx = stack.pop()
    //       result[idx] = i - idx
    //     push i.
    //
    // WHY IT WORKS: The stack stays monotonically non-increasing in temperature.
    //   The first time we can pop an index is exactly the first warmer day for it.
    //
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

    // 🎤 GOOGLE DEMO — HARD onsite; demonstrates monotonic-stack mastery.
    //   Q: "Area of the LARGEST RECTANGLE in a histogram of unit-width bars."
    //   Ex: [2,1,5,6,2,3] → 10  (5×2 from bars [5,6])
    //   Approaches: ① brute O(n²)  ② divide-and-conquer O(n log n)  ③ monotonic INCREASING stack O(n) ★
    //   🚩 Red flag: forgetting to flush remaining stack at end — misses rectangles extending to the right edge.
    //   ✨ Strong hire: use a SENTINEL 0 (or -1 index) to auto-flush; cleaner code, no post-loop fix-up.
    //   Follow-ups: LC 85 (max rect in 0/1 matrix — REDUCES to this row-by-row!), LC 42 (rain water).
    // --- LC #84: Largest Rectangle in Histogram (Hard) — Monotonic Stack ---
    // GOAL: Find the area of the largest rectangle that fits inside the histogram.
    //
    // INTUITION: The largest rectangle using bar i as its shortest bar extends
    //   left until a shorter bar and right until a shorter bar. A monotone
    //   increasing stack lets us find those left/right boundaries lazily:
    //   we compute the area for a bar the moment we find the first bar to its
    //   right that is shorter.
    //
    // STEPS:
    //   Append a sentinel height 0 at the end to flush the stack.
    //   For i = 0 .. n:
    //     While stack non-empty AND heights[stack.top] > heights[i]:
    //       height = heights[stack.pop()]
    //       width  = (stack empty) ? i : i - stack.top - 1
    //       maxArea = max(maxArea, height * width)
    //     push i.
    //
    // WHY IT WORKS: When we pop bar j at index i, bar i is the first shorter
    //   bar to its right, and stack.top (after popping) is the first shorter
    //   bar to its left — so width is exact.
    //
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

    // 🎤 GOOGLE DEMO — classic onsite; calculator problems are common at Google.
    //   Q: "Evaluate RPN expression. Operators: +-*/. Division truncates toward ZERO."
    //   Ex: ["2","1","+","3","*"] → 9   |   ["4","13","5","/","+"] → 6
    //   Approaches: ① single stack of operands O(n)/O(n) ★
    //   🚩 Red flag: using C# `/` directly without thinking about negative cases (it actually truncates toward zero in C#, but mention you checked!).
    //   ✨ Strong hire: order matters — pop b FIRST then a, compute `a op b` (not `b op a`).
    //   Follow-ups: LC 224 (basic calc, parens), LC 227 (with precedence *,/), LC 772 (full infix calculator).
    // --- LC #150: Evaluate Reverse Polish Notation (Medium) — Stack ---
    // GOAL: Evaluate a postfix arithmetic expression made of integers and
    //       operators (+, -, *, /). Integer division truncates toward zero.
    //
    // INTUITION: Postfix is purpose-built for a stack. Push numbers; when you
    //   see an operator, pop the TWO operands (right is popped first), apply,
    //   push the result back.
    //
    // Time: O(n) | Space: O(n)
    public class EvaluateRPN
    {
        public int EvalRPN(string[] tokens)
        {
            var stack = new Stack<int>();                       // holds intermediate operands
            foreach (string tok in tokens)
            {
                // If it's an operator, apply it; otherwise parse as integer and push.
                if (tok == "+" || tok == "-" || tok == "*" || tok == "/")
                {
                    int b = stack.Pop();                        // RIGHT operand was pushed last
                    int a = stack.Pop();                        // LEFT operand below it
                    stack.Push(tok switch                       // compute and push result
                    {
                        "+" => a + b,
                        "-" => a - b,
                        "*" => a * b,
                        _   => a / b                            // C# int division truncates toward 0 ✓
                    });
                }
                else
                {
                    stack.Push(int.Parse(tok));                 // plain number
                }
            }
            return stack.Pop();                                 // final lone value = answer
        }
    }

    // 🎤 GOOGLE DEMO — L4 staple; tests recursive enumeration with pruning constraints.
    //   Q: "Generate ALL well-formed parenthesis strings using n pairs."
    //   Ex: n=3 → ["((()))","(()())","(())()","()(())","()()()"]
    //   Approaches: ① brute generate-all-2^(2n) + validate O(2^2n · n)  ② backtracking w/ open/close counters O(4^n/√n) Catalan ★
    //   🚩 Red flag: generating all 2^(2n) strings then filtering — explodes quickly.
    //   ✨ Strong hire: state invariants explicitly: add '(' if open<n; add ')' if close<open.
    //   Follow-ups: derive count = Catalan C_n, LC 301 (remove invalid parens), LC 678 (validity w/ '*').
    // --- LC #22: Generate Parentheses (Medium) — Backtracking with Counters ---
    // GOAL: Given n pairs of parentheses, generate all combinations of well-
    //       formed parentheses.
    //
    // INTUITION: Build the string char by char. Invariants for validity:
    //   • Can add '(' as long as we've used fewer than n of them.
    //   • Can add ')' only if it doesn't exceed the count of '(' already placed.
    //   When length == 2n, we have a complete valid string.
    //
    // Time: O(C(n)) where C(n) is the nth Catalan number | Space: O(n) recursion depth
    public class GenerateParentheses
    {
        public IList<string> Generate(int n)
        {
            var res = new List<string>();                       // accumulator for all valid strings
            Build(new System.Text.StringBuilder(), 0, 0, n, res);
            return res;
        }

        private void Build(System.Text.StringBuilder sb, int open, int close, int n, List<string> res)
        {
            if (sb.Length == 2 * n) { res.Add(sb.ToString()); return; } // complete → record copy

            if (open < n)                                       // can still add '('
            {
                sb.Append('(');
                Build(sb, open + 1, close, n, res);
                sb.Length--;                                    // undo (backtrack)
            }
            if (close < open)                                   // ')' only if it has a matching '('
            {
                sb.Append(')');
                Build(sb, open, close + 1, n, res);
                sb.Length--;                                    // backtrack
            }
        }
    }

    // 🎤 GOOGLE DEMO — classic data-structure design; tests amortized analysis.
    //   Q: "Implement FIFO Queue (push/peek/pop/empty) using ONLY two stacks."
    //   Ex: push(1); push(2); peek()→1; pop()→1; empty()→false
    //   Approaches: ① push always on `in`, dump to `out` on peek/pop when out is empty ★  ② push expensive (always rotate)
    //   🚩 Red flag: dumping in→out on EVERY pop — turns O(1) amortized into O(n) worst-case unnecessarily.
    //   ✨ Strong hire: prove amortized O(1) via accounting/aggregate analysis (each element moves at most twice).
    //   Follow-ups: LC 225 (stack using TWO queues — push O(n) trick), LC 622 (circular queue), LC 933.
    // --- LC #232: Implement Queue using Stacks (Easy) — Two-Stack Amortized O(1) ---
    // GOAL: Implement FIFO queue ops (push, pop, peek, empty) using only stacks.
    //
    // INTUITION: Use two stacks: `inStack` for pushes, `outStack` for pops.
    //   When outStack is empty and we need to pop/peek, dump all of inStack
    //   into outStack — the order reverses, which is exactly FIFO order.
    //   Each element moves at most twice → amortized O(1) per op.
    public class MyQueue
    {
        private readonly Stack<int> _in = new();                // newest items pile up here
        private readonly Stack<int> _out = new();               // oldest items surface here

        public void Push(int x) => _in.Push(x);                 // enqueue: just push to in

        public int Pop()
        {
            Shift();                                            // ensure oldest is on top of _out
            return _out.Pop();
        }

        public int Peek()
        {
            Shift();                                            // ensure oldest is on top of _out
            return _out.Peek();
        }

        public bool Empty() => _in.Count == 0 && _out.Count == 0;

        // Move everything from _in to _out only when _out is empty.
        private void Shift()
        {
            if (_out.Count > 0) return;                         // still have reversed items — do nothing
            while (_in.Count > 0) _out.Push(_in.Pop());         // reverse order via pop/push
        }
    }
}
