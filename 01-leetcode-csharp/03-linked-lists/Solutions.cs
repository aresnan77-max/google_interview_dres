// ============================================================================
// Category: Linked Lists — Google Interview Prep
// Problems: ReverseLinkedList(#206), MergeTwoSortedLists(#21),
//           LinkedListCycle(#141), RemoveNthFromEnd(#19)
// ============================================================================

using System;
using System.Collections.Generic;

namespace GoogleInterviewPrep.LeetCode
{
    // --- LC #206: Reverse Linked List (Easy) — Iterative Pointer Reversal ---
    // Time: O(n) | Space: O(1)
    public class ReverseLinkedList
    {
        public ListNode? ReverseList(ListNode? head)
        {
            ListNode? prev = null, current = head;
            while (current != null)
            {
                ListNode? next = current.next;
                current.next = prev;
                prev = current;
                current = next;
            }
            return prev;
        }
    }

    // --- LC #21: Merge Two Sorted Lists (Easy) — Dummy Head ---
    // Time: O(n + m) | Space: O(1)
    public class MergeTwoSortedLists
    {
        public ListNode? MergeTwoLists(ListNode? list1, ListNode? list2)
        {
            var dummy = new ListNode(-1);
            var cur = dummy;
            while (list1 != null && list2 != null)
            {
                if (list1.val <= list2.val) { cur.next = list1; list1 = list1.next; }
                else { cur.next = list2; list2 = list2.next; }
                cur = cur.next;
            }
            cur.next = list1 ?? list2;
            return dummy.next;
        }
    }

    // --- LC #141: Linked List Cycle (Easy) — Floyd's Tortoise and Hare ---
    // Time: O(n) | Space: O(1)
    public class LinkedListCycle
    {
        public bool HasCycle(ListNode? head)
        {
            ListNode? slow = head, fast = head;
            while (fast?.next != null)
            {
                slow = slow!.next;
                fast = fast.next.next;
                if (slow == fast) return true;
            }
            return false;
        }
    }

    // --- LC #19: Remove Nth Node From End (Medium) — Two-Pointer Gap ---
    // Time: O(n) | Space: O(1)
    public class RemoveNthFromEnd
    {
        public ListNode? RemoveNthFromEndOfList(ListNode? head, int n)
        {
            var dummy = new ListNode(0, head);
            ListNode? fast = dummy, slow = dummy;
            for (int i = 0; i <= n; i++) fast = fast!.next;
            while (fast != null) { fast = fast.next; slow = slow!.next; }
            slow!.next = slow.next!.next;
            return dummy.next;
        }
    }
}
