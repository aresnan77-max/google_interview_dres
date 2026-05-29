// ============================================================================
// Category: Design Problems — Google Interview Prep
// Problems: LRUCache(#146), Trie(#208),
//           WordDictionary(#211), RandomizedSet(#380)
// ============================================================================

using System;
using System.Collections.Generic;

namespace GoogleInterviewPrep.LeetCode
{
    // 🎤 GOOGLE DEMO — THE most-asked design question. If you memorize ONE, make it this.
    //   Q: "Design LRUCache with O(1) get + put; evict LEAST RECENTLY USED on overflow."
    //   Ex: cap=2; put(1,1); put(2,2); get(1)→1; put(3,3) evicts 2; get(2)→−1
    //   Approaches: ① OrderedDict / LinkedHashMap (built-in)  ② HashMap + DOUBLY linked list w/ sentinel head/tail O(1) ★  ③ array + timestamps O(n) remove (suboptimal)
    //   🚩 Red flag: using a singly linked list — O(n) to find prev for removal; defeats O(1) goal.
    //   ✨ Strong hire: voice the dummy head/tail sentinels; offer thread-safety (lock or ConcurrentDictionary) unprompted.
    //   Follow-ups: LC 460 (LFU — much harder), thread-safe LRU, byte-size capacity, distributed LRU (consistent hashing).
    // --- LC #146: LRU Cache (Medium) — HashMap + Doubly Linked List ---
    // GOAL: Design a cache with O(1) Get and Put. On capacity overflow,
    //       evict the Least Recently Used entry.
    //
    // INTUITION: Two data structures combined:
    //   HashMap (key → node): O(1) access by key.
    //   Doubly Linked List (DLL): tracks recency order.
    //     Most recently used → near the dummy head.
    //     Least recently used → near the dummy tail.
    //   Access (Get or Put) = move the node to the front.
    //   Evict = remove the node just before the dummy tail.
    //
    // GET(key):
    //   Not found → return -1.
    //   Found → MoveToFront(node); return value.
    //
    // PUT(key, value):
    //   Exists → update value; MoveToFront.
    //   New → AddFront; map[key] = node.
    //   Over capacity → Remove(tail.prev); map.Remove(lru.key).
    //
    // WHY IT WORKS: Sentinel head/tail nodes eliminate edge cases for
    //   inserting/removing at the ends of the DLL.
    //
    // Time: O(1) all ops | Space: O(capacity)
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

    // 🎤 GOOGLE DEMO — L4 onsite favorite (think: Search autocomplete, spell-correct).
    //   Q: "Implement Trie with insert, search (exact), startsWith (prefix)."
    //   Ex: insert("apple"); search("apple")→true; search("app")→false; startsWith("app")→true
    //   Approaches: ① HashSet of all prefixes O(L²) memory  ② Trie array[26] O(L) per op ★ (lowercase)  ③ Trie Dictionary<char,Node> ★ (unicode-friendly)
    //   🚩 Red flag: forgetting the End flag — search("app") returns true when only "apple" inserted.
    //   ✨ Strong hire: discuss array vs dict trade-off explicitly; offer compressed (radix) tree for memory.
    //   Follow-ups: LC 211 (wildcards), LC 212 (word search II), LC 1268 (autocomplete suggestions).
    // --- LC #208: Implement Trie (Medium) — Array-based Prefix Tree ---
    // GOAL: Design a data structure that supports Insert, Search, and
    //       StartsWith for a set of strings over lowercase letters.
    //
    // STRUCTURE: Each TrieNode holds:
    //   Ch[26]: pointers to child nodes (one per letter a-z), null = absent.
    //   End: marks whether a complete word ends at this node.
    //
    // INSERT(word):
    //   Walk the trie character by character.
    //   At each char c: if Ch[c-'a'] == null, create a new node.
    //   After all chars: set End = true.
    //
    // SEARCH(word):
    //   Walk all characters; if any child is missing → false.
    //   After all chars: return node.End (whole word must exist).
    //
    // STARTSWITH(prefix):
    //   Same walk as Search; return true if we successfully traversed the
    //   whole prefix (End flag doesn't matter).
    //
    // WHY IT WORKS: Shared prefixes share nodes, so memory is proportional to
    //   total unique characters, not total words. Each operation is O(m).
    //
    // Time: O(m) per operation  m = word length | Space: O(n · m)
    public class Trie
    {
        private class TrieNode { public TrieNode?[] Ch = new TrieNode?[26]; public bool End; }
        private readonly TrieNode _root = new();
        public void Insert(string word) { var n = _root; foreach (char c in word) { int i = c-'a'; n.Ch[i] ??= new TrieNode(); n = n.Ch[i]!; } n.End = true; }
        public bool Search(string word) { var n = Find(word); return n != null && n.End; }
        public bool StartsWith(string prefix) => Find(prefix) != null;
        private TrieNode? Find(string s) { var n = _root; foreach (char c in s) { int i = c-'a'; if (n.Ch[i] == null) return null; n = n.Ch[i]!; } return n; }
    }

    // 🎤 GOOGLE DEMO — onsite extension of LC 208; tests recursive DFS on Trie.
    //   Q: "addWord + search; search supports '.' as ANY-letter wildcard."
    //   Ex: add("bad","dad","mad"); search(".ad")→true; search("b..")→true; search("pad")→false
    //   Approaches: ① store all words in list, regex match O(N·L)  ② Trie + DFS, on '.' branch all 26 ★ O(L) no-wild / O(26^L) all-wild
    //   🚩 Red flag: not stating the O(26^L) worst case for all-dot queries — interviewer will ask.
    //   ✨ Strong hire: mention pruning by remaining length, and bucketing words by length as alt approach.
    //   Follow-ups: support '*' (zero-or-more — much harder), LC 212 (Word Search II), LC 642 (autocomplete).
    // --- LC #211: Design Add and Search Words Data Structure (Medium) — Trie + DFS for '.' ---
    // GOAL: Build a data structure supporting AddWord(word) and Search(word)
    //       where the search word may contain '.' as a wildcard for any letter.
    //
    // INTUITION: Plain Trie for inserts. For Search, DFS the Trie: on a normal
    //   character take that branch; on '.' try ALL 26 children.
    //
    // Time: AddWord O(L); Search O(L) without wildcards, O(26^L) worst case with them.
    // Space: O(Σ word lengths)
    public class WordDictionary
    {
        private class Node { public readonly Node?[] Ch = new Node?[26]; public bool End; }
        private readonly Node _root = new();

        public void AddWord(string word)
        {
            var n = _root;
            foreach (char c in word)
            {
                int i = c - 'a';
                n.Ch[i] ??= new Node();                            // create branch if missing
                n = n.Ch[i]!;
            }
            n.End = true;                                          // mark terminal
        }

        public bool Search(string word) => Dfs(word, 0, _root);

        private bool Dfs(string w, int idx, Node node)
        {
            if (idx == w.Length) return node.End;                  // reached end → must be a terminal
            char c = w[idx];
            if (c == '.')
            {
                // Wildcard: any non-null child that leads to a match works.
                foreach (var child in node.Ch)
                    if (child != null && Dfs(w, idx + 1, child)) return true;
                return false;
            }
            var next = node.Ch[c - 'a'];                           // specific letter branch
            return next != null && Dfs(w, idx + 1, next);
        }
    }

    // 🎤 GOOGLE DEMO — L4 onsite; the "swap-with-last" trick IS the whole point.
    //   Q: "Design set w/ insert, remove, getRandom — ALL O(1) average."
    //   Ex: insert(1)→true; remove(2)→false; insert(2)→true; getRandom()→1 or 2 uniformly
    //   Approaches: ① HashSet alone — O(1) ins/rem but O(n) random ❌  ② List alone — O(1) random but O(n) remove ❌  ③ List + Dict<val,index> + swap-with-last on remove ★
    //   🚩 Red flag: removing from middle of List — O(n) shift; defeats the goal.
    //   ✨ Strong hire: voice the swap-with-last invariant; mention Reservoir Sampling for unbounded streams.
    //   Follow-ups: LC 381 (allow DUPLICATES — Dict<val, HashSet<idx>>), LC 528 (weighted random), LC 710.
    // --- LC #380: Insert Delete GetRandom O(1) (Medium) — List + Dictionary ---
    // GOAL: Design a set supporting Insert, Remove, and GetRandom — each O(1).
    //
    // INTUITION: A Dictionary alone gives O(1) insert/remove but not O(1) random.
    //   A List gives O(1) random by index but O(n) remove. Combine them:
    //   • List<int> for indexable storage.
    //   • Dictionary<int,int> mapping value → its index in the list.
    //   For Remove: swap target with the LAST element, then pop the list end.
    public class RandomizedSet
    {
        private readonly List<int> _values = new();                // O(1) random access
        private readonly Dictionary<int, int> _index = new();      // value → index in _values
        private readonly Random _rng = new();

        public bool Insert(int val)
        {
            if (_index.ContainsKey(val)) return false;             // already present
            _index[val] = _values.Count;                           // record new index
            _values.Add(val);                                      // append to list
            return true;
        }

        public bool Remove(int val)
        {
            if (!_index.TryGetValue(val, out int idx)) return false;
            int lastVal = _values[^1];                             // value sitting at the tail
            _values[idx] = lastVal;                                // overwrite target slot with tail value
            _index[lastVal] = idx;                                 // update tail value's index entry
            _values.RemoveAt(_values.Count - 1);                   // O(1) pop from end
            _index.Remove(val);                                    // forget removed value
            return true;
        }

        public int GetRandom()
        {
            return _values[_rng.Next(_values.Count)];              // uniform random from list
        }
    }
}
