// ============================================================================
// Category: Design Problems — Google Interview Prep
// Problems: LRUCache(#146), Trie(#208)
// ============================================================================

using System;
using System.Collections.Generic;

namespace GoogleInterviewPrep.LeetCode
{
    // --- LC #146: LRU Cache (Medium) — HashMap + Doubly Linked List ---
    // Time: O(1) for Get and Put | Space: O(capacity)
    public class LRUCache
    {
        private class DNode { public int Key, Value; public DNode? Prev, Next; }
        private readonly int _cap;
        private readonly Dictionary<int, DNode> _map;
        private readonly DNode _head = new(), _tail = new();
        public LRUCache(int capacity) { _cap = capacity; _map = new(capacity); _head.Next = _tail; _tail.Prev = _head; }

        public int Get(int key)
        {
            if (!_map.TryGetValue(key, out var node)) return -1;
            MoveToFront(node); return node.Value;
        }
        public void Put(int key, int value)
        {
            if (_map.TryGetValue(key, out var existing)) { existing.Value = value; MoveToFront(existing); return; }
            var node = new DNode { Key = key, Value = value };
            _map[key] = node; AddFront(node);
            if (_map.Count > _cap) { var lru = _tail.Prev!; Remove(lru); _map.Remove(lru.Key); }
        }
        private void AddFront(DNode n) { n.Prev = _head; n.Next = _head.Next; _head.Next!.Prev = n; _head.Next = n; }
        private void Remove(DNode n) { n.Prev!.Next = n.Next; n.Next!.Prev = n.Prev; }
        private void MoveToFront(DNode n) { Remove(n); AddFront(n); }
    }

    // --- LC #208: Implement Trie (Medium) — Array-based Prefix Tree ---
    // Time: O(m) per operation | Space: O(n * m)
    public class Trie
    {
        private class TrieNode { public TrieNode?[] Ch = new TrieNode?[26]; public bool End; }
        private readonly TrieNode _root = new();
        public void Insert(string word) { var n = _root; foreach (char c in word) { int i = c-'a'; n.Ch[i] ??= new TrieNode(); n = n.Ch[i]!; } n.End = true; }
        public bool Search(string word) { var n = Find(word); return n != null && n.End; }
        public bool StartsWith(string prefix) => Find(prefix) != null;
        private TrieNode? Find(string s) { var n = _root; foreach (char c in s) { int i = c-'a'; if (n.Ch[i] == null) return null; n = n.Ch[i]!; } return n; }
    }
}
