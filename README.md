# Google Developer Relations Engineer — Interview Preparation Project

> A comprehensive **14-day study plan** for Google's Developer Relations Engineer (DRE) role.  
> Combines **LeetCode algorithm practice in C#**, **Unity game development mastery**, and **DevRel interview readiness**.

---

## 🎯 What is a Google DRE?

A Developer Relations Engineer sits at the intersection of **software engineering**, **community engagement**, and **product advocacy**. You must demonstrate:

| Pillar | What Google Evaluates |
|--------|----------------------|
| **Coding & Algorithms** | Data structures, algorithms, clean & readable C# code |
| **System Design** | Scalable architectures, API design, trade-off reasoning |
| **Unity / Game Dev** | Engine mastery, C# scripting, performance optimization |
| **DevRel & Communication** | Technical writing, public speaking, developer empathy |
| **Behavioral / Googliness** | Collaboration, leadership, handling ambiguity |

---

## 🚀 Quick Start

```bash
# Build & run all 48 LeetCode solution tests
dotnet run

# Run specific category
dotnet run -- arrays          # Arrays & Strings (8 problems)
dotnet run -- hashmaps        # HashMaps & Sets (4 problems)
dotnet run -- stacks          # Stacks & Queues (4 problems)
dotnet run -- trees           # Trees (6 problems)
dotnet run -- graphs          # Graphs (5 problems)
dotnet run -- dp              # Dynamic Programming (7 problems)
dotnet run -- heaps           # Heaps & Priority Queues (4 problems)
dotnet run -- backtracking    # Backtracking (4 problems)
dotnet run -- design          # Design Problems (2 problems)
```

> **Requirements:** .NET 8.0 SDK or later. Download from https://dotnet.microsoft.com/download

---

## 📁 Project Structure

```
google-dev-study-plan/
│
├── README.md                              # This file
├── STUDY_PLAN.md                          # Detailed 14-day study calendar
├── GoogleInterviewPrep.csproj             # C# console project
├── Program.cs                             # Test runner for all solutions
│
├── 01-leetcode-csharp/                    # ── Algorithm & DS Practice ──
│   ├── README.md                          # Problem index & progress tracker
│   ├── SharedDataStructures.cs            # ListNode, TreeNode definitions
│   ├── 01-arrays-strings/Solutions.cs     # TwoSum, 3Sum, TrappingRainWater...
│   ├── 02-hashmaps-sets/Solutions.cs      # GroupAnagrams, TopKFrequent...
│   ├── 03-linked-lists/Solutions.cs       # ReverseList, MergeTwoSorted...
│   ├── 04-stacks-queues/Solutions.cs      # ValidParentheses, MinStack...
│   ├── 05-trees/Solutions.cs              # MaxDepth, ValidBST, Serialize...
│   ├── 06-graphs/Solutions.cs             # Islands, CourseSchedule, UnionFind...
│   ├── 07-dynamic-programming/Solutions.cs # CoinChange, WordBreak, LIS...
│   ├── 08-heaps-priority-queues/Solutions.cs # KthLargest, Median, MinHeap...
│   ├── 09-backtracking/Solutions.cs       # Subsets, Permutations, NQueens...
│   └── 10-design/Solutions.cs             # LRU Cache, Trie
│
├── 02-unity-gamedev/                      # ── Unity Game Development ──
│   ├── README.md                          # Unity study roadmap
│   ├── 01-CoreLifecycle.cs                # MonoBehaviour lifecycle deep-dive
│   ├── 02-PhysicsAndCollision.cs          # Rigidbody, Colliders, Raycasting
│   ├── 03-DesignPatterns.cs               # Singleton, Observer, Pool, State...
│   ├── 04-ScriptableObjects.cs            # Data-driven architecture
│   ├── 05-Performance.cs                  # Profiling, GC, Batching, LOD
│   └── 06-UIAndNetworking.cs              # UI systems, Netcode, Rendering
│
├── 03-devrel-prep/                        # ── DevRel Interview Preparation ──
│   ├── README.md                          # Interview process overview
│   ├── 01-SystemDesign.md                 # System design guide + 3 exercises
│   ├── 02-BehavioralPrep.md               # STAR format + 20 questions
│   ├── 03-TechnicalWriting.md             # Writing guides + sample codelab
│   ├── 04-APIDesign.md                    # REST/gRPC design principles
│   └── 05-Portfolio.md                    # Building your public profile
│
└── resources/                             # ── Supplementary Resources ──
    ├── cheatsheets/
    │   ├── AlgorithmComplexity.md          # Big-O reference card
    │   ├── CSharpForInterviews.md          # C# collections & idioms
    │   ├── SystemDesign.md                 # Architecture one-pager
    │   └── UnityQuickRef.md               # Unity API reference
    └── mock-interviews/
        ├── CodingMock.md                  # 45-min coding mock script
        ├── SystemDesignMock.md            # 45-min design mock script
        └── BehavioralMock.md              # 30-min behavioral mock script
```

---

## 📊 48 LeetCode Problems Covered

| # | Category | Count | Key Patterns |
|---|----------|-------|-------------|
| 01 | Arrays & Strings | 8 | Two Pointers, Kadane's, Prefix/Suffix, Monotonic Deque |
| 02 | HashMaps & Sets | 4 | Frequency Map, Bucket Sort, Encoding Keys |
| 03 | Linked Lists | 4 | Fast/Slow Pointers, Dummy Head, Gap Technique |
| 04 | Stacks & Queues | 4 | Monotonic Stack, Auxiliary Stack |
| 05 | Trees | 6 | DFS (Pre/In/Post), BFS, BST Bounds |
| 06 | Graphs | 5 | BFS/DFS Grid, Topological Sort, Union-Find |
| 07 | Dynamic Programming | 7 | 1D/2D DP, Knapsack, Binary Search Optimization |
| 08 | Heaps & Priority Queues | 4 | Min/Max Heap, Two Heaps, QuickSelect |
| 09 | Backtracking | 4 | Include/Exclude, Constraint Pruning |
| 10 | Design | 2 | HashMap+DLL, Prefix Tree |
| | **Total** | **48** | |

---

## 🗓️ 14-Day Schedule Overview

| Week | Focus | Daily Hours |
|------|-------|-------------|
| **Week 1** (Days 1–7) | Foundation — algorithms, data structures, Unity basics | 8h/day |
| **Week 2** (Days 8–14) | Advanced — hard problems, system design, mock interviews | 8h/day |

> See [STUDY_PLAN.md](STUDY_PLAN.md) for the detailed day-by-day calendar.

---

## 📝 Evaluation Criteria (Google Standards)

### Coding Interviews
- ✅ Correct & optimal solution
- ✅ Clean, readable, production-quality code
- ✅ Clear articulation of approach & trade-offs
- ✅ Proper handling of edge cases
- ✅ Accurate time/space complexity analysis

### System Design
- ✅ Structured requirement gathering (functional + non-functional)
- ✅ High-level architecture with clear component responsibilities
- ✅ Scalability, availability, and consistency trade-offs
- ✅ Developer-friendly API design

### Behavioral / Googliness
- ✅ STAR-format stories demonstrating real impact
- ✅ Developer empathy and community mindset
- ✅ Cross-functional collaboration examples
- ✅ Growth mindset and handling of failure

---

## 📄 License

MIT License
