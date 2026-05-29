// ============================================================================
// Tests: Trees
// Each [Fact] can be run independently in Rider's test runner
// ============================================================================

using GoogleInterviewPrep.LeetCode;
using Xunit;

namespace GoogleInterviewPrep.Tests;

public class TreesTests
{
    // --- LC #104: Maximum Depth of Binary Tree ---
    [Fact]
    public void MaxDepth_BasicCase()
    {
        var tree = TreeNode.FromArray(new int?[] { 3, 9, 20, null, null, 15, 7 });
        Assert.Equal(3, new MaxDepthBinaryTree().MaxDepth(tree));
    }

    [Fact]
    public void MaxDepth_SingleNode()
    {
        Assert.Equal(1, new MaxDepthBinaryTree().MaxDepth(new TreeNode(1)));
    }

    [Fact]
    public void MaxDepth_Null()
    {
        Assert.Equal(0, new MaxDepthBinaryTree().MaxDepth(null));
    }

    [Fact]
    public void MaxDepth_LeftSkewed()
    {
        var tree = TreeNode.FromArray(new int?[] { 1, 2, null, 3, null });
        Assert.Equal(3, new MaxDepthBinaryTree().MaxDepth(tree));
    }

    // --- LC #102: Binary Tree Level Order Traversal ---
    [Fact]
    public void LevelOrder_BasicCase()
    {
        var tree = TreeNode.FromArray(new int?[] { 3, 9, 20, null, null, 15, 7 });
        var result = new BinaryTreeLevelOrder().LevelOrder(tree);
        Assert.Equal(3, result.Count);
        Assert.Equal(new[] { 3 }, result[0]);
        Assert.Equal(new[] { 9, 20 }, result[1]);
        Assert.Equal(new[] { 15, 7 }, result[2]);
    }

    [Fact]
    public void LevelOrder_Empty()
    {
        var result = new BinaryTreeLevelOrder().LevelOrder(null);
        Assert.Empty(result);
    }

    [Fact]
    public void LevelOrder_SingleNode()
    {
        var result = new BinaryTreeLevelOrder().LevelOrder(new TreeNode(1));
        Assert.Single(result);
        Assert.Equal(new[] { 1 }, result[0]);
    }

    // --- LC #98: Validate BST ---
    [Fact]
    public void ValidateBST_ValidTree()
    {
        var tree = TreeNode.FromArray(new int?[] { 2, 1, 3 });
        Assert.True(new ValidateBST().IsValidBST(tree));
    }

    [Fact]
    public void ValidateBST_InvalidTree()
    {
        var tree = TreeNode.FromArray(new int?[] { 5, 1, 4, null, null, 3, 6 });
        Assert.False(new ValidateBST().IsValidBST(tree));
    }

    [Fact]
    public void ValidateBST_SingleNode()
    {
        Assert.True(new ValidateBST().IsValidBST(new TreeNode(1)));
    }

    [Fact]
    public void ValidateBST_EqualValues_Invalid()
    {
        // [1, 1] - left child equals root, not a valid BST
        var tree = TreeNode.FromArray(new int?[] { 1, 1 });
        Assert.False(new ValidateBST().IsValidBST(tree));
    }

    // --- LC #236: Lowest Common Ancestor ---
    [Fact]
    public void LCA_RootIsAncestor()
    {
        var tree = TreeNode.FromArray(new int?[] { 3, 5, 1, 6, 2, 0, 8, null, null, 7, 4 });
        var lca = new LowestCommonAncestor().FindLCA(tree, tree!.left!, tree!.right!);
        Assert.Equal(3, lca!.val);
    }

    [Fact]
    public void LCA_OneNodeIsAncestor()
    {
        var tree = TreeNode.FromArray(new int?[] { 3, 5, 1, 6, 2, 0, 8, null, null, 7, 4 });
        // LCA of 5 and 4: 5 is ancestor of 4
        var lca = new LowestCommonAncestor().FindLCA(tree, tree!.left!, tree!.left!.right!.right!);
        Assert.Equal(5, lca!.val);
    }

    // --- LC #297: Serialize and Deserialize Binary Tree ---
    [Fact]
    public void SerializeDeserialize_Roundtrip()
    {
        var sd = new SerializeDeserializeBT();
        var orig = TreeNode.FromArray(new int?[] { 1, 2, 3, null, null, 4, 5 });
        var serialized = sd.Serialize(orig);
        var deserialized = sd.Deserialize(serialized);
        Assert.Equal(serialized, sd.Serialize(deserialized));
    }

    [Fact]
    public void SerializeDeserialize_EmptyTree()
    {
        var sd = new SerializeDeserializeBT();
        var serialized = sd.Serialize(null);
        Assert.Null(sd.Deserialize(serialized));
    }

    [Fact]
    public void SerializeDeserialize_SingleNode()
    {
        var sd = new SerializeDeserializeBT();
        var tree = new TreeNode(42);
        var serialized = sd.Serialize(tree);
        var deserialized = sd.Deserialize(serialized);
        Assert.Equal(42, deserialized!.val);
        Assert.Null(deserialized.left);
        Assert.Null(deserialized.right);
    }

    // --- LC #124: Binary Tree Maximum Path Sum ---
    [Fact]
    public void MaxPathSum_BasicCase()
    {
        var tree = TreeNode.FromArray(new int?[] { -10, 9, 20, null, null, 15, 7 });
        Assert.Equal(42, new BinaryTreeMaxPathSum().MaxPathSum(tree));
    }

    [Fact]
    public void MaxPathSum_SimpleTree()
    {
        var tree = TreeNode.FromArray(new int?[] { 1, 2, 3 });
        Assert.Equal(6, new BinaryTreeMaxPathSum().MaxPathSum(tree));
    }

    [Fact]
    public void MaxPathSum_SingleNegativeNode()
    {
        Assert.Equal(-3, new BinaryTreeMaxPathSum().MaxPathSum(new TreeNode(-3)));
    }

    [Fact]
    public void MaxPathSum_AllNegative()
    {
        var tree = TreeNode.FromArray(new int?[] { -1, -2, -3 });
        Assert.Equal(-1, new BinaryTreeMaxPathSum().MaxPathSum(tree));
    }

    // --- LC #226: Invert Binary Tree ---
    [Fact]
    public void InvertBinaryTree_Basic()
    {
        // Original [4,2,7,1,3,6,9] inverts to [4,7,2,9,6,3,1] in level order.
        var tree = TreeNode.FromArray(new int?[] { 4, 2, 7, 1, 3, 6, 9 });
        var inverted = new InvertBinaryTree().Invert(tree);
        Assert.Equal(7, inverted!.left!.val);                       // root.left was 2, now 7
        Assert.Equal(2, inverted.right!.val);                       // root.right was 7, now 2
        Assert.Equal(9, inverted.left.left!.val);
        Assert.Equal(6, inverted.left.right!.val);
        Assert.Equal(3, inverted.right.left!.val);
        Assert.Equal(1, inverted.right.right!.val);
    }

    [Fact]
    public void InvertBinaryTree_Null() => Assert.Null(new InvertBinaryTree().Invert(null));

    // --- LC #543: Diameter of Binary Tree ---
    [Fact]
    public void Diameter_FiveNodes()
    {
        // [1,2,3,4,5] → longest path is 4-2-1-3 OR 5-2-1-3 (3 edges)
        var tree = TreeNode.FromArray(new int?[] { 1, 2, 3, 4, 5 });
        Assert.Equal(3, new DiameterOfBinaryTree().Diameter(tree));
    }

    [Fact]
    public void Diameter_SingleNode()
    {
        var tree = TreeNode.FromArray(new int?[] { 1 });
        Assert.Equal(0, new DiameterOfBinaryTree().Diameter(tree));
    }

    // --- LC #230: Kth Smallest in BST ---
    [Theory]
    [InlineData(new int?[] { 3, 1, 4, null, 2 }, 1, 1)]
    [InlineData(new int?[] { 3, 1, 4, null, 2 }, 2, 2)]
    [InlineData(new int?[] { 5, 3, 6, 2, 4, null, null, 1 }, 3, 3)]
    public void KthSmallestInBST_Cases(int?[] vals, int k, int expected)
    {
        var tree = TreeNode.FromArray(vals);
        Assert.Equal(expected, new KthSmallestInBST().KthSmallest(tree, k));
    }

    // --- LC #100: Same Tree ---
    [Fact]
    public void SameTree_Identical()
    {
        var a = TreeNode.FromArray(new int?[] { 1, 2, 3 });
        var b = TreeNode.FromArray(new int?[] { 1, 2, 3 });
        Assert.True(new SameTree().IsSameTree(a, b));
    }

    [Fact]
    public void SameTree_DifferentValue()
    {
        var a = TreeNode.FromArray(new int?[] { 1, 2, 1 });
        var b = TreeNode.FromArray(new int?[] { 1, 1, 2 });
        Assert.False(new SameTree().IsSameTree(a, b));
    }
}
