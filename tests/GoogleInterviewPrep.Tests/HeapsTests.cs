// ============================================================================
// Tests: Heaps & Priority Queues
// Each [Fact] can be run independently in Rider's test runner
// ============================================================================

using System.Collections.Generic;
using GoogleInterviewPrep.LeetCode;
using Xunit;

namespace GoogleInterviewPrep.Tests;

public class HeapsTests
{
    // --- LC #215: Kth Largest Element ---
    [Fact]
    public void KthLargest_Heap_BasicCase()
    {
        Assert.Equal(5, new KthLargestElement().FindKthLargest_Heap(new[] { 3, 2, 1, 5, 6, 4 }, 2));
    }

    [Fact]
    public void KthLargest_Heap_WithDuplicates()
    {
        Assert.Equal(4, new KthLargestElement().FindKthLargest_Heap(new[] { 3, 2, 3, 1, 2, 4, 5, 5, 6 }, 4));
    }

    [Fact]
    public void KthLargest_QuickSelect_BasicCase()
    {
        Assert.Equal(5, new KthLargestElement().FindKthLargest_QuickSelect(new[] { 3, 2, 1, 5, 6, 4 }, 2));
    }

    [Fact]
    public void KthLargest_QuickSelect_WithDuplicates()
    {
        Assert.Equal(4, new KthLargestElement().FindKthLargest_QuickSelect(new[] { 3, 2, 3, 1, 2, 4, 5, 5, 6 }, 4));
    }

    // --- LC #23: Merge K Sorted Lists ---
    [Fact]
    public void MergeKSorted_BasicCase()
    {
        var lists = new[] {
            ListNode.FromArray(new[] { 1, 4, 5 }),
            ListNode.FromArray(new[] { 1, 3, 4 }),
            ListNode.FromArray(new[] { 2, 6 })
        };
        var result = new MergeKSortedLists().MergeKLists(lists);
        Assert.Equal(new[] { 1, 1, 2, 3, 4, 4, 5, 6 }, ListNode.ToArray(result));
    }

    [Fact]
    public void MergeKSorted_EmptyLists()
    {
        var result = new MergeKSortedLists().MergeKLists(new ListNode?[] { null, null });
        Assert.Null(result);
    }

    [Fact]
    public void MergeKSorted_SingleList()
    {
        var lists = new[] { ListNode.FromArray(new[] { 1, 2, 3 }) };
        var result = new MergeKSortedLists().MergeKLists(lists);
        Assert.Equal(new[] { 1, 2, 3 }, ListNode.ToArray(result));
    }

    // --- LC #295: Find Median from Data Stream ---
    [Fact]
    public void MedianFinder_OddCount()
    {
        var mf = new MedianFinder();
        mf.AddNum(1);
        mf.AddNum(2);
        mf.AddNum(3);
        Assert.Equal(2.0, mf.FindMedian());
    }

    [Fact]
    public void MedianFinder_EvenCount()
    {
        var mf = new MedianFinder();
        mf.AddNum(1);
        mf.AddNum(2);
        Assert.Equal(1.5, mf.FindMedian());
    }

    [Fact]
    public void MedianFinder_UnorderedInput()
    {
        var mf = new MedianFinder();
        mf.AddNum(6);
        mf.AddNum(10);
        mf.AddNum(2);
        mf.AddNum(6);
        mf.AddNum(5);
        Assert.Equal(6.0, mf.FindMedian()); // sorted: 2,5,6,6,10 -> median = 6
    }

    // --- Custom: MinHeap ---
    [Fact]
    public void MinHeap_InsertAndPeek()
    {
        var heap = new MinHeap();
        heap.Insert(5);
        heap.Insert(3);
        heap.Insert(8);
        heap.Insert(1);
        Assert.Equal(1, heap.Peek());
    }

    [Fact]
    public void MinHeap_ExtractMin()
    {
        var heap = new MinHeap();
        heap.Insert(5);
        heap.Insert(3);
        heap.Insert(8);
        heap.Insert(1);
        Assert.Equal(1, heap.ExtractMin());
        Assert.Equal(3, heap.ExtractMin());
        Assert.Equal(5, heap.ExtractMin());
        Assert.Equal(8, heap.ExtractMin());
    }

    [Fact]
    public void MinHeap_Count()
    {
        var heap = new MinHeap();
        Assert.Equal(0, heap.Count);
        heap.Insert(1);
        heap.Insert(2);
        Assert.Equal(2, heap.Count);
        heap.ExtractMin();
        Assert.Equal(1, heap.Count);
    }

    [Fact]
    public void MinHeap_BuildFromArray()
    {
        var heap = new MinHeap(new[] { 9, 5, 2, 7, 1 });
        Assert.Equal(1, heap.ExtractMin());
        Assert.Equal(2, heap.ExtractMin());
    }

    // --- LC #973: K Closest Points to Origin ---
    [Fact]
    public void KClosestPoints_Basic()
    {
        var points = new[]
        {
            new[] { 1, 3 },
            new[] { -2, 2 },
            new[] { 5, 8 },
            new[] { 0, 1 }
        };
        var result = new KClosestPointsToOrigin().KClosest(points, 2);
        // The two closest are [0,1] (dist 1) and [-2,2] (dist 8) — order may vary.
        Assert.Equal(2, result.Length);
        var set = new HashSet<string>();
        foreach (var p in result) set.Add($"{p[0]},{p[1]}");
        Assert.Contains("0,1", set);
        Assert.Contains("-2,2", set);
    }

    // --- LC #621: Task Scheduler ---
    [Theory]
    [InlineData(new[] { 'A', 'A', 'A', 'B', 'B', 'B' }, 2, 8)]
    [InlineData(new[] { 'A', 'A', 'A', 'B', 'B', 'B' }, 0, 6)]
    [InlineData(new[] { 'A', 'A', 'A', 'A', 'A', 'A', 'B', 'C', 'D', 'E', 'F', 'G' }, 2, 16)]
    public void TaskScheduler_Cases(char[] tasks, int n, int expected)
    {
        Assert.Equal(expected, new TaskScheduler().LeastInterval(tasks, n));
    }
}
