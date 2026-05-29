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
# Build & run the 50 core LeetCode smoke tests (console runner)
dotnet run

# Run specific category smoke tests
dotnet run -- arrays          # Arrays & Strings (13 problems)
dotnet run -- hashmaps        # HashMaps & Sets (7 problems)
dotnet run -- linked          # Linked Lists (7 problems)
dotnet run -- stacks          # Stacks & Queues (7 problems)
dotnet run -- trees           # Trees (10 problems)
dotnet run -- graphs          # Graphs (8 problems)
dotnet run -- dp              # Dynamic Programming (11 problems)
dotnet run -- heaps           # Heaps & Priority Queues (6 problems)
dotnet run -- backtracking    # Backtracking (7 problems)
dotnet run -- design          # Design Problems (4 problems)
```

> 80 LeetCode problems are implemented across the 10 categories.
> The xUnit test project under `tests/` exercises every problem (≈150 cases)
> and is the recommended way to run/verify individual solutions in Rider.

### 🧪 在 Rider 中运行单元测试

本项目包含 xUnit 测试项目，支持在 Rider 中独立运行每一个测试用例：

1. 用 Rider 打开 `GoogleInterviewPrep.sln`
2. 在 Solution Explorer 中展开 `GoogleInterviewPrep.Tests`
3. 每个测试文件对应一个算法分类，每个 `[Fact]` / `[Theory]` 方法都可以单独运行
4. 右键点击测试方法 → Run 或使用左侧的绿色运行按钮

```
tests/GoogleInterviewPrep.Tests/
├── ArraysStringsTests.cs        # 数组与字符串
├── HashMapsTests.cs             # 哈希表与集合
├── LinkedListsTests.cs          # 链表
├── StacksQueuesTests.cs         # 栈与队列
├── TreesTests.cs                # 二叉树
├── GraphsTests.cs               # 图
├── DynamicProgrammingTests.cs   # 动态规划
├── HeapsTests.cs                # 堆与优先队列
├── BacktrackingTests.cs         # 回溯
└── DesignTests.cs               # 设计题
```

> **Requirements:** .NET 8.0 SDK or later. Download from https://dotnet.microsoft.com/download

---

## 📁 Project Structure

```
google-dev-study-plan/
│
├── README.md                              # This file
├── STUDY_PLAN.md                          # Detailed 14-day study calendar
├── GoogleInterviewPrep.sln                # Solution file (open in Rider)
├── GoogleInterviewPrep.csproj             # C# console project
├── Program.cs                             # Console test runner for all solutions
├── global.json                            # SDK version pinning
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
├── tests/GoogleInterviewPrep.Tests/       # ── xUnit Test Project (Rider) ──
│   ├── GoogleInterviewPrep.Tests.csproj   # xUnit test project
│   ├── ArraysStringsTests.cs              # 数组与字符串测试
│   ├── HashMapsTests.cs                   # 哈希表测试
│   ├── LinkedListsTests.cs               # 链表测试
│   ├── StacksQueuesTests.cs              # 栈与队列测试
│   ├── TreesTests.cs                     # 树测试
│   ├── GraphsTests.cs                    # 图测试
│   ├── DynamicProgrammingTests.cs        # 动态规划测试
│   ├── HeapsTests.cs                     # 堆测试
│   ├── BacktrackingTests.cs              # 回溯测试
│   └── DesignTests.cs                    # 设计题测试
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

## 📊 50 LeetCode Problems Covered

| # | Category | Count | Key Patterns |
|---|----------|-------|-------------|
| 01 | Arrays & Strings | 8 | Two Pointers, Kadane's, Prefix/Suffix, Monotonic Deque |
| 02 | HashMaps & Sets | 4 | Frequency Map, Bucket Sort, Encoding Keys |
| 03 | Linked Lists | 4 | Fast/Slow Pointers, Dummy Head, Gap Technique |
| 04 | Stacks & Queues | 4 | Monotonic Stack, Auxiliary Stack |
| 05 | Trees | 6 | DFS (Pre/In/Post), BFS, BST Bounds |
| 06 | Graphs | 6 | BFS/DFS Grid, Topological Sort, Union-Find |
| 07 | Dynamic Programming | 7 | 1D/2D DP, Knapsack, Binary Search Optimization |
| 08 | Heaps & Priority Queues | 4 | Min/Max Heap, Two Heaps, QuickSelect |
| 09 | Backtracking | 4 | Include/Exclude, Constraint Pruning |
| 10 | Design | 2 | HashMap+DLL, Prefix Tree |
| | **Total** | **50** | |

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
