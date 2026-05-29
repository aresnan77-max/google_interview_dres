// ============================================================================
// Google DRE Interview Prep — LeetCode Console Test Runner
// ============================================================================
// Run:  dotnet run                — All tests
//       dotnet run -- arrays      — Arrays & Strings only
//       dotnet run -- trees       — Trees only
//       dotnet run -- dp          — Dynamic Programming only
// ============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using GoogleInterviewPrep.LeetCode;

namespace GoogleInterviewPrep
{
    class Program
    {
        static int _passed = 0, _failed = 0, _total = 0;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            PrintBanner();
            string f = args.Length > 0 ? args[0].ToLower() : "all";

            if (f is "all" or "arrays")       TestArrays();
            if (f is "all" or "hashmaps")      TestHashMaps();
            if (f is "all" or "linked")        TestLinkedLists();
            if (f is "all" or "stacks")        TestStacks();
            if (f is "all" or "trees")         TestTrees();
            if (f is "all" or "graphs")        TestGraphs();
            if (f is "all" or "dp")            TestDP();
            if (f is "all" or "heaps")         TestHeaps();
            if (f is "all" or "backtracking")  TestBacktracking();
            if (f is "all" or "design")        TestDesign();

            PrintSummary();
        }

        static void TestArrays()
        {
            Header("01 — Arrays & Strings");
            Assert("TwoSum([2,7,11,15],9)", new TwoSum().Solve(new[]{2,7,11,15}, 9), new[]{0,1});
            Assert("MaxProfit([7,1,5,3,6,4])", new BestTimeToBuyAndSellStock().MaxProfit(new[]{7,1,5,3,6,4}), 5);
            Assert("MaxSubArray([-2,1,-3,4,-1,2,1,-5,4])", new MaxSubarray().MaxSubArray(new[]{-2,1,-3,4,-1,2,1,-5,4}), 6);
            Assert("ProductExceptSelf([1,2,3,4])", new ProductExceptSelf().Solve(new[]{1,2,3,4}), new[]{24,12,8,6});
            var merged = new MergeIntervals().Merge(new[]{new[]{1,3},new[]{2,6},new[]{8,10},new[]{15,18}});
            Assert("MergeIntervals count", merged.Length, 3);
            Assert("ThreeSum count", new ThreeSum().Solve(new[]{-1,0,1,2,-1,-4}).Count, 2);
            Assert("Trap([0,1,0,2,1,0,1,3,2,1,2,1])", new TrappingRainWater().Trap(new[]{0,1,0,2,1,0,1,3,2,1,2,1}), 6);
            Assert("MaxSlidingWindow", new SlidingWindowMaximum().MaxSlidingWindow(new[]{1,3,-1,-3,5,3,6,7},3), new[]{3,3,5,5,6,7});
        }

        static void TestHashMaps()
        {
            Header("02 — HashMaps & Sets");
            Assert("GroupAnagrams count", new GroupAnagrams().Solve(new[]{"eat","tea","tan","ate","nat","bat"}).Count, 3);
            Assert("LongestConsecutive", new LongestConsecutiveSequence().LongestConsecutive(new[]{100,4,200,1,3,2}), 4);
            var topK = new TopKFrequentElements().TopKFrequent(new[]{1,1,1,2,2,3}, 2);
            Assert("TopKFrequent contains 1 & 2", topK.Contains(1) && topK.Contains(2), true);
            char[][] board = {
                new[]{'5','3','.','.','7','.','.','.','.'},new[]{'6','.','.','1','9','5','.','.','.'},
                new[]{'.','9','8','.','.','.','.','6','.'},new[]{'8','.','.','.','6','.','.','.','3'},
                new[]{'4','.','.','8','.','3','.','.','1'},new[]{'7','.','.','.','2','.','.','.','6'},
                new[]{'.','6','.','.','.','.','2','8','.'},new[]{'.','.','.','4','1','9','.','.','5'},
                new[]{'.','.','.','.','8','.','.','7','9'}};
            Assert("ValidSudoku", new ValidSudoku().IsValidSudoku(board), true);
        }

        static void TestLinkedLists()
        {
            Header("03 — Linked Lists");
            Assert("ReverseList([1,2,3,4,5])", ListNode.ToArray(new ReverseLinkedList().ReverseList(ListNode.FromArray(new[]{1,2,3,4,5}))), new[]{5,4,3,2,1});
            Assert("MergeTwoLists", ListNode.ToArray(new MergeTwoSortedLists().MergeTwoLists(ListNode.FromArray(new[]{1,2,4}), ListNode.FromArray(new[]{1,3,4}))), new[]{1,1,2,3,4,4});
            var cycleNode = new ListNode(3); var n2 = new ListNode(2); var n3 = new ListNode(0); var n4 = new ListNode(-4);
            cycleNode.next = n2; n2.next = n3; n3.next = n4; n4.next = n2;
            Assert("HasCycle(cycle)", new LinkedListCycle().HasCycle(cycleNode), true);
            Assert("HasCycle(no cycle)", new LinkedListCycle().HasCycle(ListNode.FromArray(new[]{1,2,3})), false);
            Assert("RemoveNthFromEnd([1,2,3,4,5],2)", ListNode.ToArray(new RemoveNthFromEnd().RemoveNthFromEndOfList(ListNode.FromArray(new[]{1,2,3,4,5}), 2)), new[]{1,2,3,5});
        }

        static void TestStacks()
        {
            Header("04 — Stacks & Queues");
            Assert("IsValid('()')", new ValidParentheses().IsValid("()"), true);
            Assert("IsValid('(]')", new ValidParentheses().IsValid("(]"), false);
            var ms = new MinStack(); ms.Push(-2); ms.Push(0); ms.Push(-3);
            Assert("MinStack.GetMin()", ms.GetMin(), -3);
            ms.Pop(); Assert("MinStack.Top()", ms.Top(), 0);
            Assert("DailyTemps", new DailyTemperatures().Solve(new[]{73,74,75,71,69,72,76,73}), new[]{1,1,4,2,1,1,0,0});
            Assert("LargestRect([2,1,5,6,2,3])", new LargestRectangleInHistogram().LargestRectangleArea(new[]{2,1,5,6,2,3}), 10);
        }

        static void TestTrees()
        {
            Header("05 — Trees");
            var t = TreeNode.FromArray(new int?[]{3,9,20,null,null,15,7});
            Assert("MaxDepth", new MaxDepthBinaryTree().MaxDepth(t), 3);
            Assert("LevelOrder levels", new BinaryTreeLevelOrder().LevelOrder(t).Count, 3);
            Assert("ValidBST([2,1,3])", new ValidateBST().IsValidBST(TreeNode.FromArray(new int?[]{2,1,3})), true);
            Assert("ValidBST([5,1,4,n,n,3,6])", new ValidateBST().IsValidBST(TreeNode.FromArray(new int?[]{5,1,4,null,null,3,6})), false);
            var lcaTree = TreeNode.FromArray(new int?[]{3,5,1,6,2,0,8,null,null,7,4});
            Assert("LCA(5,1)", new LowestCommonAncestor().FindLCA(lcaTree, lcaTree!.left!, lcaTree!.right!)!.val, 3);
            var sd = new SerializeDeserializeBT();
            var orig = TreeNode.FromArray(new int?[]{1,2,3,null,null,4,5});
            Assert("Serialize roundtrip", sd.Serialize(sd.Deserialize(sd.Serialize(orig))), sd.Serialize(orig));
            Assert("MaxPathSum", new BinaryTreeMaxPathSum().MaxPathSum(TreeNode.FromArray(new int?[]{-10,9,20,null,null,15,7})), 42);
        }

        static void TestGraphs()
        {
            Header("06 — Graphs");
            Assert("NumIslands", new NumberOfIslands().NumIslands(new[]{
                new[]{'1','1','0','0','0'},new[]{'1','1','0','0','0'},
                new[]{'0','0','1','0','0'},new[]{'0','0','0','1','1'}}), 3);
            Assert("CanFinish(2,[[1,0]])", new CourseSchedule().CanFinish(2, new[]{new[]{1,0}}), true);
            Assert("CanFinish cycle", new CourseSchedule().CanFinish(2, new[]{new[]{1,0},new[]{0,1}}), false);
            Assert("PacificAtlantic count", new PacificAtlanticWaterFlow().PacificAtlantic(new[]{
                new[]{1,2,2,3,5},new[]{3,2,3,4,4},new[]{2,4,5,3,1},new[]{6,7,1,4,5},new[]{5,1,1,2,4}}).Count, 7);
            Assert("WordSearch ABCCED", new WordSearch().Exist(new[]{
                new[]{'A','B','C','E'},new[]{'S','F','C','S'},new[]{'A','D','E','E'}}, "ABCCED"), true);
            Assert("ConnectedComponents", new NumberOfConnectedComponents().CountComponents(5, new[]{new[]{0,1},new[]{1,2},new[]{3,4}}), 2);
            var gn1 = new GraphNode(1); var gn2 = new GraphNode(2);
            gn1.neighbors = new List<GraphNode>{gn2}; gn2.neighbors = new List<GraphNode>{gn1};
            var cloned = new CloneGraph().Clone(gn1)!;
            Assert("CloneGraph val", cloned.val, 1);
            Assert("CloneGraph is deep copy", !ReferenceEquals(gn1, cloned), true);
        }

        static void TestDP()
        {
            Header("07 — Dynamic Programming");
            Assert("ClimbStairs(5)", new ClimbingStairs().ClimbStairs(5), 8);
            Assert("Rob([2,7,9,3,1])", new HouseRobber().Rob(new[]{2,7,9,3,1}), 12);
            Assert("CoinChange([1,5,11],15)", new CoinChange().Solve(new[]{1,5,11}, 15), 3);
            Assert("CoinChange([2],3)", new CoinChange().Solve(new[]{2}, 3), -1);
            Assert("UniquePaths(3,7)", new UniquePaths().Solve(3, 7), 28);
            Assert("WordBreak", new WordBreak().Solve("leetcode", new List<string>{"leet","code"}), true);
            Assert("LIS([10,9,2,5,3,7,101,18])", new LongestIncreasingSubsequence().LengthOfLIS(new[]{10,9,2,5,3,7,101,18}), 4);
            Assert("Decode('226')", new DecodeWays().NumDecodings("226"), 3);
        }

        static void TestHeaps()
        {
            Header("08 — Heaps & Priority Queues");
            Assert("KthLargest_Heap", new KthLargestElement().FindKthLargest_Heap(new[]{3,2,1,5,6,4}, 2), 5);
            Assert("KthLargest_QS", new KthLargestElement().FindKthLargest_QuickSelect(new[]{3,2,3,1,2,4,5,5,6}, 4), 4);
            var mksl = new MergeKSortedLists().MergeKLists(new[]{
                ListNode.FromArray(new[]{1,4,5}), ListNode.FromArray(new[]{1,3,4}), ListNode.FromArray(new[]{2,6})});
            Assert("MergeKSorted", ListNode.ToArray(mksl), new[]{1,1,2,3,4,4,5,6});
            var mf = new MedianFinder(); mf.AddNum(1); mf.AddNum(2);
            Assert("Median [1,2]", mf.FindMedian(), 1.5); mf.AddNum(3);
            Assert("Median [1,2,3]", mf.FindMedian(), 2.0);
            var heap = new MinHeap(); heap.Insert(5); heap.Insert(3); heap.Insert(8); heap.Insert(1);
            Assert("MinHeap.Peek", heap.Peek(), 1);
            Assert("MinHeap.Extract", heap.ExtractMin(), 1);
        }

        static void TestBacktracking()
        {
            Header("09 — Backtracking");
            Assert("Subsets([1,2,3])", new Subsets().Solve(new[]{1,2,3}).Count, 8);
            Assert("Permutations([1,2,3])", new Permutations().Permute(new[]{1,2,3}).Count, 6);
            Assert("CombinationSum([2,3,6,7],7)", new CombinationSum().Solve(new[]{2,3,6,7}, 7).Count, 2);
            Assert("NQueens(4)", new NQueens().SolveNQueens(4).Count, 2);
            Assert("NQueens(8)", new NQueens().SolveNQueens(8).Count, 92);
        }

        static void TestDesign()
        {
            Header("10 — Design Problems");
            var lru = new LRUCache(2);
            lru.Put(1,1); lru.Put(2,2);
            Assert("LRU.Get(1)", lru.Get(1), 1);
            lru.Put(3,3); Assert("LRU.Get(2) evicted", lru.Get(2), -1);
            lru.Put(4,4); Assert("LRU.Get(1) evicted", lru.Get(1), -1);
            Assert("LRU.Get(3)", lru.Get(3), 3);

            var trie = new Trie();
            trie.Insert("apple");
            Assert("Trie.Search apple", trie.Search("apple"), true);
            Assert("Trie.Search app", trie.Search("app"), false);
            Assert("Trie.StartsWith app", trie.StartsWith("app"), true);
            trie.Insert("app");
            Assert("Trie.Search app after insert", trie.Search("app"), true);
        }

        // ---- Minimal test framework ----
        static void Assert<T>(string name, T actual, T expected)
        {
            _total++;
            if (EqualityComparer<T>.Default.Equals(actual, expected)) { _passed++; Pass(name); }
            else { _failed++; Fail(name, $"{expected}", $"{actual}"); }
        }
        static void Assert(string name, int[] actual, int[] expected)
        {
            _total++;
            if (actual.SequenceEqual(expected)) { _passed++; Pass(name); }
            else { _failed++; Fail(name, $"[{string.Join(",",expected)}]", $"[{string.Join(",",actual)}]"); }
        }
        static void Pass(string n) { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"  ✓ {n}"); Console.ResetColor(); }
        static void Fail(string n, string e, string a) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"  ✗ {n}\n    Expected: {e}\n    Actual:   {a}"); Console.ResetColor(); }
        static void Header(string s) { Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"\n━━━ {s} ━━━"); Console.ResetColor(); }
        static void PrintBanner()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║  Google DRE — LeetCode C# Test Runner (50 problems)  ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
            Console.ResetColor();
        }
        static void PrintSummary()
        {
            Console.ForegroundColor = _failed == 0 ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"\n  Results: {_passed}/{_total} passed, {_failed} failed");
            if (_failed == 0) Console.WriteLine("  🎉 All tests passed!");
            Console.ResetColor();
        }
    }
}
