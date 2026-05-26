# C# for Interviews — Quick Reference

## Collections & Time Complexities

| Type | Add | Remove | Lookup | Ordered | Notes |
|------|-----|--------|--------|---------|-------|
| `List<T>` | O(1)* | O(n) | O(n) / O(1) idx | Yes | Dynamic array, most used |
| `Dictionary<K,V>` | O(1) | O(1) | O(1) | No | HashMap, interview essential |
| `HashSet<T>` | O(1) | O(1) | O(1) | No | Unique elements, set operations |
| `Stack<T>` | O(1) push | O(1) pop | O(1) peek | LIFO | Parentheses, monotonic stack |
| `Queue<T>` | O(1) enq | O(1) deq | O(1) peek | FIFO | BFS, sliding window |
| `LinkedList<T>` | O(1)** | O(1)** | O(n) | Yes | LRU Cache, deque operations |
| `SortedSet<T>` | O(log n) | O(log n) | O(log n) | Yes | Red-black tree |
| `SortedDictionary<K,V>` | O(log n) | O(log n) | O(log n) | Yes | Sorted by key |
| `PriorityQueue<T,P>` | O(log n) | O(log n) | O(1) peek | Heap | .NET 6+, min-heap by default |

*Amortized. **With node reference.

## Essential Methods

### Dictionary
```csharp
dict.TryGetValue(key, out var val)    // Safe lookup, returns bool
dict.GetValueOrDefault(key, 0)        // Returns default if missing
dict.TryAdd(key, val)                 // Add only if key absent
dict.ContainsKey(key)                 // Check existence
foreach (var (key, val) in dict)      // Iterate with deconstruction
```

### HashSet
```csharp
set.Add(item)                         // Returns false if already exists
set.Contains(item)                    // O(1) membership test
set.UnionWith(other)                  // Set union (in-place)
set.IntersectWith(other)              // Set intersection (in-place)
set.ExceptWith(other)                 // Set difference (in-place)
```

### String
```csharp
s.Substring(start, length)            // Extract substring
s.Split(',')                           // Split into array
s.ToCharArray()                        // Convert to char[]
new string(charArray)                  // char[] → string
string.Join(",", collection)           // Join with separator
s.Contains("sub")                      // Substring search
s.Replace("old", "new")               // Replace all occurrences
char.IsLetter(c) / char.IsDigit(c)    // Character classification
```

### Array
```csharp
Array.Sort(arr)                        // In-place sort
Array.Sort(arr, (a,b) => a.CompareTo(b))  // Custom comparator
Array.Reverse(arr)                     // In-place reverse
Array.Fill(arr, value)                 // Fill with value
Array.BinarySearch(arr, target)        // Returns index or ~insertPos
arr.Length                             // Size (not Count)
```

## LINQ Essentials (Use Sparingly in Interviews — Prefer Explicit Loops)

```csharp
nums.Where(x => x > 0)                // Filter
nums.Select(x => x * 2)               // Map/Transform
nums.OrderBy(x => x)                  // Sort ascending
nums.OrderByDescending(x => x)        // Sort descending
nums.GroupBy(x => x % 2)              // Group by key
nums.Distinct()                        // Remove duplicates
nums.Take(k)                          // First k elements
nums.Skip(k)                          // Skip first k
nums.Sum() / .Max() / .Min()          // Aggregations
nums.Any(x => x > 0)                  // Any match
nums.All(x => x > 0)                  // All match
nums.Zip(other, (a,b) => a+b)         // Pair elements
nums.ToList() / .ToArray()            // Convert
nums.ToDictionary(x => x.Id)          // To dictionary
```

## Modern C# Features Useful in Interviews

```csharp
// Pattern matching (C# 8+)
if (obj is int n && n > 0) { }
var result = shape switch { Circle c => c.Radius, _ => 0 };

// Tuple deconstruction
var (min, max) = (int.MaxValue, int.MinValue);
(a, b) = (b, a);  // Swap without temp

// Null-coalescing
x ?? defaultValue              // If x is null, use default
x ??= new List<int>();         // Assign if null

// Range/Index (C# 8+)
arr[^1]                        // Last element
arr[1..3]                      // Slice (index 1 to 2)
arr[..3]                       // First 3
arr[3..]                       // Skip first 3

// Collection expressions (C# 12+)
int[] arr = [1, 2, 3];
List<int> list = [1, 2, 3];

// Value types vs Reference types
// struct → stack (no GC), class → heap (GC pressure)
// Use struct for small, immutable data (Point, Color, DamageInfo)
```
