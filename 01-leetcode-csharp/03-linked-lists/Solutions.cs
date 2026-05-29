// ============================================================================
// Category: Linked Lists — Google Interview Prep
// Problems: ReverseLinkedList(#206), MergeTwoSortedLists(#21),
//           LinkedListCycle(#141), RemoveNthFromEnd(#19),
//           AddTwoNumbers(#2), PalindromeLinkedList(#234), ReorderList(#143)
// ============================================================================

using System;
using System.Collections.Generic;

namespace GoogleInterviewPrep.LeetCode
{
    // 🎤 GOOGLE DEMO — phone-screen rite-of-passage; checks pointer chops.
    //   Q: "Reverse a singly linked list. Return the new head."
    //   Ex: 1→2→3→4→5→null → 5→4→3→2→1→null
    //   Approaches: ① iterative prev/curr/next O(n)/O(1) ★  ② recursive O(n)/O(n) stack
    //   🚩 Red flag: losing the next pointer before rewiring (NPE on second iteration).
    //   ✨ Strong hire: write both versions; mention that recursion uses O(n) call stack.
    //   Follow-ups: LC 92 (reverse m..n), LC 25 (reverse in K-groups — Google onsite hard).
    // --- LC #206: Reverse Linked List (Easy) — Iterative Pointer Reversal ---
    // GOAL: Reverse a singly linked list in-place and return the new head.
    //
    // INTUITION: Walk forward while re-pointing each node's .next to the
    //   previous node. Carry three pointers: prev (the new chain so far),
    //   current (the node being flipped), next (saved before overwriting).
    //
    // STEPS:
    //   prev = null, current = head.
    //   While current != null:
    //     1. next    = current.next        ← save
    //     2. current.next = prev           ← reverse the link
    //     3. prev    = current             ← advance prev
    //     4. current = next               ← advance current
    //   Return prev (last node visited = new head).
    //
    // WHY IT WORKS: Each node is visited once; its next pointer is flipped
    //   before we move on, so the chain grows backwards without losing track.
    //
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

    // 🎤 GOOGLE DEMO — warm-up; teaches the DUMMY-HEAD pattern.
    //   Q: "Merge two SORTED singly-linked lists by splicing existing nodes (no new allocs)."
    //   Ex: 1→2→4, 1→3→4 → 1→1→2→3→4→4
    //   Approaches: ① iterative dummy-head O(n+m)/O(1) ★  ② recursive O(n+m)/O(n+m)
    //   🚩 Red flag: no dummy node — special-casing the first attach is bug-prone.
    //   ✨ Strong hire: after the loop, attach the remaining list with ONE assignment (not a copy loop).
    //   Follow-ups: LC 23 (merge K — heap or divide-and-conquer), LC 88 (merge sorted arrays in place).
    // --- LC #21: Merge Two Sorted Lists (Easy) — Dummy Head ---
    // GOAL: Merge two sorted linked lists into one sorted linked list.
    //
    // INTUITION: Use a dummy node to avoid special-casing the head. Maintain
    //   a cursor; at each step attach the smaller of the two current nodes,
    //   then advance that list. When one list runs out, attach the rest of
    //   the other.
    //
    // STEPS:
    //   dummy = new node, cur = dummy.
    //   While both lists non-null:
    //     if list1.val ≤ list2.val: cur.next = list1; list1 = list1.next
    //     else:                      cur.next = list2; list2 = list2.next
    //     cur = cur.next
    //   cur.next = whichever list remains.
    //   Return dummy.next.
    //
    // WHY IT WORKS: Because both lists are already sorted, a single greedy
    //   comparison per step is sufficient — the smaller head is always correct.
    //
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

    // 🎤 GOOGLE DEMO — beloved phone-screen; tests creativity under O(1)-space constraint.
    //   Q: "Does the linked list contain a CYCLE? O(1) extra memory."
    //   Ex: 3→2→0→-4, with -4.next → 2  → true
    //   Approaches: ① HashSet of visited nodes O(n)/O(n)  ② Floyd's slow/fast O(n)/O(1) ★
    //   🚩 Red flag: mutating list to mark visited — violates "don't modify input".
    //   ✨ Strong hire: prove Floyd terminates — fast gains one step per tick inside the cycle.
    //   Follow-ups: LC 142 (find cycle START — math trick: reset slow to head), LC 287 (dup as cycle).
    // --- LC #141: Linked List Cycle (Easy) — Floyd's Tortoise and Hare ---
    // GOAL: Detect whether a linked list contains a cycle.
    //
    // INTUITION: Use two pointers moving at different speeds. If there is a
    //   cycle they must eventually occupy the same node. If there is no cycle
    //   the fast pointer reaches null first.
    //
    // STEPS:
    //   slow = head, fast = head.
    //   While fast != null and fast.next != null:
    //     slow = slow.next         ← moves 1 step
    //     fast = fast.next.next    ← moves 2 steps
    //     if slow == fast → cycle found, return true.
    //   Return false.
    //
    // WHY IT WORKS: Inside a cycle the gap between slow and fast decreases
    //   by 1 each iteration, so they MUST meet within (cycle length) steps.
    //
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

    // 🎤 GOOGLE DEMO — L3/L4 favorite; tests "single pass?" insight.
    //   Q: "Remove the Nth node from the END of the linked list. Return the head."
    //   Ex: head=[1,2,3,4,5], n=2 → [1,2,3,5]   |   [1], n=1 → []
    //   Approaches: ① two-pass (count then remove) O(n)/O(1)  ② two-pointer gap, ONE pass O(n)/O(1) ★
    //   🚩 Red flag: skipping the dummy head — head-removal becomes a special case.
    //   ✨ Strong hire: advance fast by n+1 first so slow lands on the PREV of the target.
    //   Follow-ups: LC 876 (middle node), LC 1721 (swap kth from start with kth from end).
    // --- LC #19: Remove Nth Node From End (Medium) — Two-Pointer Gap ---
    // GOAL: Remove the n-th node from the end of the list in one pass.
    //
    // INTUITION: Place two pointers exactly n+1 nodes apart. When the fast
    //   pointer reaches the end (null), the slow pointer is right before the
    //   node to delete. A dummy head avoids edge cases when removing the head.
    //
    // STEPS:
    //   1. dummy.next = head; fast = slow = dummy.
    //   2. Advance fast n+1 times.
    //   3. Move both until fast == null.
    //   4. slow.next = slow.next.next  (skip the target node).
    //   5. Return dummy.next.
    //
    // WHY IT WORKS: The n+1 gap means slow always stops at the predecessor
    //   of the node to remove, so the splice is one assignment.
    //
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

    // 🎤 GOOGLE DEMO — onsite mainstay; tests carry handling + different-length traversal.
    //   Q: "Two non-negative ints as REVERSE-order linked lists, one digit per node. Sum as a list."
    //   Ex: [2,4,3] + [5,6,4] → [7,0,8]   (342 + 465 = 807)
    //   Approaches: ① elementary addition w/ carry, single pass while EITHER list or carry alive O(max(n,m))/O(1) ★
    //   🚩 Red flag: stopping the loop when ONE list ends — the other (and carry!) may still have digits.
    //   ✨ Strong hire: keep the loop condition `l1 != null || l2 != null || carry != 0` — one elegant line.
    //   Follow-ups: LC 445 (forward-order → two stacks or reverse), LC 369 (plus one on linked list).
    // --- LC #2: Add Two Numbers (Medium) — Elementary Addition with Carry ---
    // GOAL: Two linked lists store non-negative integers with digits in REVERSE
    //       order (ones digit first). Return their sum as a linked list.
    //
    // INTUITION: Mimic grade-school addition column by column. Track a `carry`
    //   that propagates to the next digit. Stop when both lists exhausted AND
    //   carry is zero. Use a dummy head to simplify list construction.
    //
    // Time: O(max(m,n)) | Space: O(max(m,n))
    public class AddTwoNumbers
    {
        public ListNode? Add(ListNode? l1, ListNode? l2)
        {
            var dummy = new ListNode(0);                       // sentinel head simplifies appending
            var tail = dummy;                                   // tail always points to last node built
            int carry = 0;                                      // carry from previous digit add

            // Loop until BOTH inputs done AND no leftover carry to write.
            while (l1 != null || l2 != null || carry != 0)
            {
                int v1 = l1?.val ?? 0;                          // missing digit = 0
                int v2 = l2?.val ?? 0;
                int sum = v1 + v2 + carry;                      // column sum
                carry = sum / 10;                               // carry-out is tens digit
                tail.next = new ListNode(sum % 10);             // write ones digit
                tail = tail.next;                               // advance tail
                l1 = l1?.next;                                  // advance whichever list still has nodes
                l2 = l2?.next;
            }
            return dummy.next;                                  // skip sentinel
        }
    }

    // 🎤 GOOGLE DEMO — composes THREE linked-list primitives; high signal value.
    //   Q: "Is the singly-linked list a PALINDROME? O(n) time, O(1) space."
    //   Ex: 1→2→2→1 → true   |   1→2 → false
    //   Approaches: ① dump values to array, two-pointer O(n)/O(n)  ② find mid + reverse 2nd half + compare O(n)/O(1) ★
    //   🚩 Red flag: comparing INCLUDING the middle node on odd length — off-by-one.
    //   ✨ Strong hire: restore the list after comparison (interviewers love the cleanup gesture).
    //   Follow-ups: LC 125 (string palindrome), LC 9 (integer palindrome), LC 5 (longest palindromic substring).
    // --- LC #234: Palindrome Linked List (Easy) — Reverse Second Half ---
    // GOAL: Determine if a singly linked list reads the same forwards/backwards.
    //
    // INTUITION: Use fast/slow pointers to find the middle, reverse the second
    //   half in place, then compare the two halves node by node. O(1) extra space.
    //
    // Time: O(n) | Space: O(1)
    public class PalindromeLinkedList
    {
        public bool IsPalindrome(ListNode? head)
        {
            if (head == null || head.next == null) return true; // 0 or 1 node is trivially a palindrome

            // Step 1: find middle. slow ends at midpoint (start of 2nd half for even length).
            ListNode? slow = head, fast = head;
            while (fast?.next != null) { slow = slow!.next; fast = fast.next.next; }

            // Step 2: reverse from slow onwards (the second half).
            ListNode? prev = null, cur = slow;
            while (cur != null) { var nxt = cur.next; cur.next = prev; prev = cur; cur = nxt; }

            // Step 3: walk first half (from head) and reversed second half (from prev).
            ListNode? a = head, b = prev;
            while (b != null)
            {
                if (a!.val != b.val) return false;              // mismatch ⇒ not palindrome
                a = a.next; b = b.next;
            }
            return true;
        }
    }

    // 🎤 GOOGLE DEMO — L4 onsite; another "compose primitives" question.
    //   Q: "Reorder L0→L1→…→Ln to L0→Ln→L1→Ln-1→L2→… in place (rewire nodes only)."
    //   Ex: 1→2→3→4→5 → 1→5→2→4→3
    //   Approaches: ① stack of second half O(n)/O(n)  ② mid + reverse 2nd + interleave O(n)/O(1) ★
    //   🚩 Red flag: not severing the link at the midpoint — causes cycle after interleave.
    //   ✨ Strong hire: solve as 3 named helpers (FindMid, Reverse, Merge) — demonstrates decomposition.
    //   Follow-ups: LC 86 (partition list), LC 328 (odd-even rearrange), LC 24 (swap pairs).
    // --- LC #143: Reorder List (Medium) — Split + Reverse + Merge ---
    // GOAL: Given L0 → L1 → … → Ln, reorder to L0 → Ln → L1 → Ln−1 → L2 → …
    //
    // INTUITION: Three classic sub-problems:
    //   1. Find middle (fast/slow).
    //   2. Reverse the second half.
    //   3. Interleave first half with reversed second half.
    //
    // Time: O(n) | Space: O(1)
    public class ReorderList
    {
        public void Reorder(ListNode? head)
        {
            if (head?.next == null) return;                     // <=1 node — nothing to reorder

            // --- 1. Find middle: slow lands on first node of "second half" boundary.
            ListNode? slow = head, fast = head;
            while (fast?.next?.next != null) { slow = slow!.next; fast = fast.next.next; }

            // --- 2. Reverse the second half starting AFTER slow.
            ListNode? prev = null, cur = slow!.next;
            slow.next = null;                                   // sever the list into two halves
            while (cur != null) { var nxt = cur.next; cur.next = prev; prev = cur; cur = nxt; }

            // --- 3. Merge first half (head) and reversed second half (prev).
            ListNode? first = head, second = prev;
            while (second != null)
            {
                var t1 = first!.next;                           // remember next of first half
                var t2 = second.next;                           // remember next of second half
                first.next = second;                            // stitch first → second
                second.next = t1;                               // stitch second → (old first.next)
                first = t1;                                     // advance both pointers
                second = t2;
            }
        }
    }
}
