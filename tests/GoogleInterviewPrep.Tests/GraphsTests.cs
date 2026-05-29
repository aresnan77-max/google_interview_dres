// ============================================================================
// Tests: Graphs
// Each [Fact] can be run independently in Rider's test runner
// ============================================================================

using GoogleInterviewPrep.LeetCode;
using Xunit;

namespace GoogleInterviewPrep.Tests;

public class GraphsTests
{
    // --- LC #133: Clone Graph ---
    [Fact]
    public void CloneGraph_BasicCase()
    {
        // Create graph: 1 -- 2
        //               |    |
        //               4 -- 3
        var n1 = new GraphNode(1);
        var n2 = new GraphNode(2);
        var n3 = new GraphNode(3);
        var n4 = new GraphNode(4);
        n1.neighbors = new List<GraphNode> { n2, n4 };
        n2.neighbors = new List<GraphNode> { n1, n3 };
        n3.neighbors = new List<GraphNode> { n2, n4 };
        n4.neighbors = new List<GraphNode> { n1, n3 };

        var cloned = new CloneGraph().Clone(n1)!;
        Assert.Equal(1, cloned.val);
        Assert.Equal(2, cloned.neighbors.Count);
        Assert.NotSame(n1, cloned); // different instance
        Assert.NotSame(n2, cloned.neighbors[0]);
    }

    [Fact]
    public void CloneGraph_Null()
    {
        Assert.Null(new CloneGraph().Clone(null));
    }

    [Fact]
    public void CloneGraph_SingleNode()
    {
        var node = new GraphNode(1);
        var cloned = new CloneGraph().Clone(node)!;
        Assert.Equal(1, cloned.val);
        Assert.Empty(cloned.neighbors);
        Assert.NotSame(node, cloned);
    }

    // --- LC #200: Number of Islands ---
    [Fact]
    public void NumIslands_ThreeIslands()
    {
        char[][] grid = {
            new[] { '1', '1', '0', '0', '0' },
            new[] { '1', '1', '0', '0', '0' },
            new[] { '0', '0', '1', '0', '0' },
            new[] { '0', '0', '0', '1', '1' }
        };
        Assert.Equal(3, new NumberOfIslands().NumIslands(grid));
    }

    [Fact]
    public void NumIslands_SingleIsland()
    {
        char[][] grid = {
            new[] { '1', '1', '1', '1', '0' },
            new[] { '1', '1', '0', '1', '0' },
            new[] { '1', '1', '0', '0', '0' },
            new[] { '0', '0', '0', '0', '0' }
        };
        Assert.Equal(1, new NumberOfIslands().NumIslands(grid));
    }

    [Fact]
    public void NumIslands_NoIslands()
    {
        char[][] grid = {
            new[] { '0', '0', '0' },
            new[] { '0', '0', '0' }
        };
        Assert.Equal(0, new NumberOfIslands().NumIslands(grid));
    }

    // --- LC #207: Course Schedule ---
    [Fact]
    public void CanFinish_NoCycle()
    {
        Assert.True(new CourseSchedule().CanFinish(2, new[] { new[] { 1, 0 } }));
    }

    [Fact]
    public void CanFinish_WithCycle()
    {
        Assert.False(new CourseSchedule().CanFinish(2, new[] { new[] { 1, 0 }, new[] { 0, 1 } }));
    }

    [Fact]
    public void CanFinish_NoPrereqs()
    {
        Assert.True(new CourseSchedule().CanFinish(3, Array.Empty<int[]>()));
    }

    [Fact]
    public void CanFinish_ComplexGraph()
    {
        Assert.True(new CourseSchedule().CanFinish(4, new[] {
            new[] { 1, 0 }, new[] { 2, 0 }, new[] { 3, 1 }, new[] { 3, 2 }
        }));
    }

    // --- LC #417: Pacific Atlantic Water Flow ---
    [Fact]
    public void PacificAtlantic_BasicCase()
    {
        int[][] heights = {
            new[] { 1, 2, 2, 3, 5 },
            new[] { 3, 2, 3, 4, 4 },
            new[] { 2, 4, 5, 3, 1 },
            new[] { 6, 7, 1, 4, 5 },
            new[] { 5, 1, 1, 2, 4 }
        };
        var result = new PacificAtlanticWaterFlow().PacificAtlantic(heights);
        Assert.Equal(7, result.Count);
    }

    [Fact]
    public void PacificAtlantic_SingleCell()
    {
        int[][] heights = { new[] { 1 } };
        var result = new PacificAtlanticWaterFlow().PacificAtlantic(heights);
        Assert.Single(result);
    }

    // --- LC #79: Word Search ---
    [Fact]
    public void WordSearch_Found()
    {
        char[][] board = {
            new[] { 'A', 'B', 'C', 'E' },
            new[] { 'S', 'F', 'C', 'S' },
            new[] { 'A', 'D', 'E', 'E' }
        };
        Assert.True(new WordSearch().Exist(board, "ABCCED"));
    }

    [Fact]
    public void WordSearch_NotFound()
    {
        char[][] board = {
            new[] { 'A', 'B', 'C', 'E' },
            new[] { 'S', 'F', 'C', 'S' },
            new[] { 'A', 'D', 'E', 'E' }
        };
        Assert.False(new WordSearch().Exist(board, "ABCB"));
    }

    [Fact]
    public void WordSearch_SEE()
    {
        char[][] board = {
            new[] { 'A', 'B', 'C', 'E' },
            new[] { 'S', 'F', 'C', 'S' },
            new[] { 'A', 'D', 'E', 'E' }
        };
        Assert.True(new WordSearch().Exist(board, "SEE"));
    }

    // --- LC #323: Number of Connected Components (Union-Find) ---
    [Fact]
    public void ConnectedComponents_TwoComponents()
    {
        Assert.Equal(2, new NumberOfConnectedComponents().CountComponents(5,
            new[] { new[] { 0, 1 }, new[] { 1, 2 }, new[] { 3, 4 } }));
    }

    [Fact]
    public void ConnectedComponents_AllConnected()
    {
        Assert.Equal(1, new NumberOfConnectedComponents().CountComponents(5,
            new[] { new[] { 0, 1 }, new[] { 1, 2 }, new[] { 2, 3 }, new[] { 3, 4 } }));
    }

    [Fact]
    public void ConnectedComponents_NoEdges()
    {
        Assert.Equal(4, new NumberOfConnectedComponents().CountComponents(4, Array.Empty<int[]>()));
    }

    // --- LC #994: Rotting Oranges ---
    [Fact]
    public void RottingOranges_Basic()
    {
        var grid = new[]
        {
            new[] { 2, 1, 1 },
            new[] { 1, 1, 0 },
            new[] { 0, 1, 1 }
        };
        Assert.Equal(4, new RottingOranges().OrangesRotting(grid));
    }

    [Fact]
    public void RottingOranges_Impossible()
    {
        var grid = new[]
        {
            new[] { 2, 1, 1 },
            new[] { 0, 1, 1 },
            new[] { 1, 0, 1 }
        };
        Assert.Equal(-1, new RottingOranges().OrangesRotting(grid));
    }

    [Fact]
    public void RottingOranges_AllEmpty()
    {
        var grid = new[] { new[] { 0, 0 }, new[] { 0, 0 } };
        Assert.Equal(0, new RottingOranges().OrangesRotting(grid));
    }

    // --- LC #684: Redundant Connection ---
    [Fact]
    public void RedundantConnection_Triangle()
    {
        var edges = new[]
        {
            new[] { 1, 2 },
            new[] { 1, 3 },
            new[] { 2, 3 }   // this closes a cycle
        };
        Assert.Equal(new[] { 2, 3 }, new RedundantConnection().FindRedundantConnection(edges));
    }

    [Fact]
    public void RedundantConnection_LongerCycle()
    {
        var edges = new[]
        {
            new[] { 1, 2 }, new[] { 2, 3 }, new[] { 3, 4 },
            new[] { 1, 4 },   // closes cycle with first three
            new[] { 1, 5 }
        };
        Assert.Equal(new[] { 1, 4 }, new RedundantConnection().FindRedundantConnection(edges));
    }
}
