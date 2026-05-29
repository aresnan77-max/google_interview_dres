// ============================================================================
// Tests: Design Problems
// Each [Fact] can be run independently in Rider's test runner
// ============================================================================

using GoogleInterviewPrep.LeetCode;
using Xunit;

namespace GoogleInterviewPrep.Tests;

public class DesignTests
{
    // --- LC #146: LRU Cache ---
    [Fact]
    public void LRUCache_BasicEviction()
    {
        var lru = new LRUCache(2);
        lru.Put(1, 1);
        lru.Put(2, 2);
        Assert.Equal(1, lru.Get(1));
        lru.Put(3, 3); // evicts key 2
        Assert.Equal(-1, lru.Get(2));
        lru.Put(4, 4); // evicts key 1
        Assert.Equal(-1, lru.Get(1));
        Assert.Equal(3, lru.Get(3));
        Assert.Equal(4, lru.Get(4));
    }

    [Fact]
    public void LRUCache_UpdateExistingKey()
    {
        var lru = new LRUCache(2);
        lru.Put(1, 1);
        lru.Put(2, 2);
        lru.Put(1, 10); // update key 1
        Assert.Equal(10, lru.Get(1));
        lru.Put(3, 3); // should evict key 2 (not key 1 since it was recently updated)
        Assert.Equal(-1, lru.Get(2));
        Assert.Equal(10, lru.Get(1));
    }

    [Fact]
    public void LRUCache_GetMissReturnsNegativeOne()
    {
        var lru = new LRUCache(1);
        Assert.Equal(-1, lru.Get(99));
    }

    [Fact]
    public void LRUCache_SingleCapacity()
    {
        var lru = new LRUCache(1);
        lru.Put(1, 1);
        lru.Put(2, 2); // evicts key 1
        Assert.Equal(-1, lru.Get(1));
        Assert.Equal(2, lru.Get(2));
    }

    // --- LC #208: Implement Trie ---
    [Fact]
    public void Trie_InsertAndSearch()
    {
        var trie = new Trie();
        trie.Insert("apple");
        Assert.True(trie.Search("apple"));
        Assert.False(trie.Search("app"));
        Assert.True(trie.StartsWith("app"));
    }

    [Fact]
    public void Trie_InsertPrefix()
    {
        var trie = new Trie();
        trie.Insert("apple");
        trie.Insert("app");
        Assert.True(trie.Search("app"));
        Assert.True(trie.Search("apple"));
    }

    [Fact]
    public void Trie_SearchNonExistent()
    {
        var trie = new Trie();
        trie.Insert("hello");
        Assert.False(trie.Search("hell"));
        Assert.False(trie.Search("helloo"));
        Assert.False(trie.Search("world"));
    }

    [Fact]
    public void Trie_StartsWithNonExistent()
    {
        var trie = new Trie();
        trie.Insert("hello");
        Assert.True(trie.StartsWith("hel"));
        Assert.False(trie.StartsWith("abc"));
    }

    [Fact]
    public void Trie_MultipleWords()
    {
        var trie = new Trie();
        trie.Insert("car");
        trie.Insert("card");
        trie.Insert("care");
        trie.Insert("careful");
        Assert.True(trie.Search("car"));
        Assert.True(trie.Search("card"));
        Assert.True(trie.Search("care"));
        Assert.True(trie.Search("careful"));
        Assert.False(trie.Search("ca"));
        Assert.True(trie.StartsWith("car"));
        Assert.True(trie.StartsWith("care"));
    }

    // --- LC #211: Word Dictionary (Add and Search Words) ---
    [Fact]
    public void WordDictionary_AddAndSearchWithWildcards()
    {
        var d = new WordDictionary();
        d.AddWord("bad");
        d.AddWord("dad");
        d.AddWord("mad");
        Assert.False(d.Search("pad"));                              // no exact match
        Assert.True(d.Search("bad"));                               // exact
        Assert.True(d.Search(".ad"));                               // wildcard matches b/d/m + 'ad'
        Assert.True(d.Search("b.."));                               // wildcard matches 'b' + any 2
        Assert.False(d.Search("b"));                                // too short
    }

    // --- LC #380: Insert Delete GetRandom O(1) ---
    [Fact]
    public void RandomizedSet_BasicOperations()
    {
        var s = new RandomizedSet();
        Assert.True(s.Insert(1));
        Assert.False(s.Remove(2));                                  // not present
        Assert.True(s.Insert(2));
        var r = s.GetRandom();
        Assert.True(r == 1 || r == 2);                              // one of the inserted values
        Assert.True(s.Remove(1));
        Assert.False(s.Insert(2));                                  // already present
        Assert.Equal(2, s.GetRandom());                             // only 2 remains
    }
}
