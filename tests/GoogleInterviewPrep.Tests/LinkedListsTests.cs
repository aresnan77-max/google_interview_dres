// ============================================================================
// Tests: Linked Lists
// Each [Fact] can be run independently in Rider's test runner
// ============================================================================

using GoogleInterviewPrep.LeetCode;
using Xunit;

namespace GoogleInterviewPrep.Tests;

public class LinkedListsTests
{
    // --- LC #206: Reverse Linked List ---
    [Fact]
    public void ReverseList_BasicCase()
    {
        var head = ListNode.FromArray(new[] { 1, 2, 3, 4, 5 });
        var result = new ReverseLinkedList().ReverseList(head);
        Assert.Equal(new[] { 5, 4, 3, 2, 1 }, ListNode.ToArray(result));
    }

    [Fact]
    public void ReverseList_TwoElements()
    {
        var head = ListNode.FromArray(new[] { 1, 2 });
        var result = new ReverseLinkedList().ReverseList(head);
        Assert.Equal(new[] { 2, 1 }, ListNode.ToArray(result));
    }

    [Fact]
    public void ReverseList_Null()
    {
        Assert.Null(new ReverseLinkedList().ReverseList(null));
    }

    [Fact]
    public void ReverseList_SingleElement()
    {
        var head = ListNode.FromArray(new[] { 1 });
        var result = new ReverseLinkedList().ReverseList(head);
        Assert.Equal(new[] { 1 }, ListNode.ToArray(result));
    }

    // --- LC #21: Merge Two Sorted Lists ---
    [Fact]
    public void MergeTwoLists_BasicCase()
    {
        var l1 = ListNode.FromArray(new[] { 1, 2, 4 });
        var l2 = ListNode.FromArray(new[] { 1, 3, 4 });
        var result = new MergeTwoSortedLists().MergeTwoLists(l1, l2);
        Assert.Equal(new[] { 1, 1, 2, 3, 4, 4 }, ListNode.ToArray(result));
    }

    [Fact]
    public void MergeTwoLists_OneEmpty()
    {
        var l1 = ListNode.FromArray(new[] { 1, 2, 3 });
        var result = new MergeTwoSortedLists().MergeTwoLists(l1, null);
        Assert.Equal(new[] { 1, 2, 3 }, ListNode.ToArray(result));
    }

    [Fact]
    public void MergeTwoLists_BothEmpty()
    {
        var result = new MergeTwoSortedLists().MergeTwoLists(null, null);
        Assert.Null(result);
    }

    // --- LC #141: Linked List Cycle ---
    [Fact]
    public void HasCycle_WithCycle()
    {
        var n1 = new ListNode(3);
        var n2 = new ListNode(2);
        var n3 = new ListNode(0);
        var n4 = new ListNode(-4);
        n1.next = n2; n2.next = n3; n3.next = n4; n4.next = n2; // cycle to n2
        Assert.True(new LinkedListCycle().HasCycle(n1));
    }

    [Fact]
    public void HasCycle_NoCycle()
    {
        var head = ListNode.FromArray(new[] { 1, 2, 3, 4 });
        Assert.False(new LinkedListCycle().HasCycle(head));
    }

    [Fact]
    public void HasCycle_SingleNode_NoCycle()
    {
        Assert.False(new LinkedListCycle().HasCycle(new ListNode(1)));
    }

    // --- LC #19: Remove Nth Node From End ---
    [Fact]
    public void RemoveNthFromEnd_Middle()
    {
        var head = ListNode.FromArray(new[] { 1, 2, 3, 4, 5 });
        var result = new RemoveNthFromEnd().RemoveNthFromEndOfList(head, 2);
        Assert.Equal(new[] { 1, 2, 3, 5 }, ListNode.ToArray(result));
    }

    [Fact]
    public void RemoveNthFromEnd_Head()
    {
        var head = ListNode.FromArray(new[] { 1, 2, 3, 4, 5 });
        var result = new RemoveNthFromEnd().RemoveNthFromEndOfList(head, 5);
        Assert.Equal(new[] { 2, 3, 4, 5 }, ListNode.ToArray(result));
    }

    [Fact]
    public void RemoveNthFromEnd_SingleElement()
    {
        var head = ListNode.FromArray(new[] { 1 });
        var result = new RemoveNthFromEnd().RemoveNthFromEndOfList(head, 1);
        Assert.Null(result);
    }

    // --- LC #2: Add Two Numbers ---
    [Fact]
    public void AddTwoNumbers_Basic()
    {
        // 342 + 465 = 807  → stored reversed as [2,4,3] + [5,6,4] = [7,0,8]
        var l1 = ListNode.FromArray(new[] { 2, 4, 3 });
        var l2 = ListNode.FromArray(new[] { 5, 6, 4 });
        var sum = new AddTwoNumbers().Add(l1, l2);
        Assert.Equal(new[] { 7, 0, 8 }, sum!.ToArray());
    }

    [Fact]
    public void AddTwoNumbers_WithCarryOverflow()
    {
        // 99 + 1 = 100 → [9,9] + [1] = [0,0,1]
        var l1 = ListNode.FromArray(new[] { 9, 9 });
        var l2 = ListNode.FromArray(new[] { 1 });
        Assert.Equal(new[] { 0, 0, 1 }, new AddTwoNumbers().Add(l1, l2)!.ToArray());
    }

    // --- LC #234: Palindrome Linked List ---
    [Theory]
    [InlineData(new[] { 1, 2, 2, 1 }, true)]
    [InlineData(new[] { 1, 2 }, false)]
    [InlineData(new[] { 1 }, true)]
    [InlineData(new[] { 1, 2, 3, 2, 1 }, true)]
    public void PalindromeLinkedList_Cases(int[] vals, bool expected)
    {
        var head = ListNode.FromArray(vals);
        Assert.Equal(expected, new PalindromeLinkedList().IsPalindrome(head));
    }

    // --- LC #143: Reorder List ---
    [Fact]
    public void ReorderList_FiveNodes()
    {
        // [1,2,3,4,5] → [1,5,2,4,3]
        var head = ListNode.FromArray(new[] { 1, 2, 3, 4, 5 });
        new ReorderList().Reorder(head);
        Assert.Equal(new[] { 1, 5, 2, 4, 3 }, head!.ToArray());
    }

    [Fact]
    public void ReorderList_FourNodes()
    {
        // [1,2,3,4] → [1,4,2,3]
        var head = ListNode.FromArray(new[] { 1, 2, 3, 4 });
        new ReorderList().Reorder(head);
        Assert.Equal(new[] { 1, 4, 2, 3 }, head!.ToArray());
    }
}
