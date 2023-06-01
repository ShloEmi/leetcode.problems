using leetcode.problems.Solution.Helpers;

namespace leetcode.problems.SearchBinarySearchTree;

/*
https://leetcode.com/problems/search-in-a-binary-search-tree

700. Search in a Binary Search Tree
Easy

You are given the root of a binary search tree (BST) and an integer val.

Find the node in the BST that the node's value equals val and return the subtree rooted with that node. 
If such a node does not exist, return null.
 

Example 1:
Input: root = [4,2,7,1,3], val = 2
Output: [2,1,3]

Example 2:
Input: root = [4,2,7,1,3], val = 5
Output: []
 

Constraints:
The number of nodes in the tree is in the range [1, 5000].
1 <= Node.val <= 107
root is a binary search tree.
1 <= val <= 107
*/


public class SearchBinarySearchTree
{
    // t1 planing = 1m
    // t1 = 15m
    // Runtime1 119 ms Beats 37.40% Memory 48 MB Beats 88.6%
    public static TreeNode SearchBST(TreeNode root, int val)
    {
        if (root == null)
            return null;

        if (val < root.val)
            return SearchBST(root.left, val);
        if (val > root.val)
            return SearchBST(root.right, val);

        return root;
    }

    // t2 = t1 + 8m
    // Runtime2 119 ms Beats 37.40% Memory 48.6 MB Beats 34.75%
    public static TreeNode SearchBST2(TreeNode root, int val)
    {
        TreeNode c = root;

        while (c != null)
        {
            if (val < c.val)
                c = c.left;
            else if (val > c.val)
                c = c.right;
            else
                return c;
        }

        return null;
    }
}


public class SearchBinarySearchTreeUnitTests
{
    private readonly ITestOutputHelper output;
    public SearchBinarySearchTreeUnitTests(ITestOutputHelper output)
        => this.output = output;


    public static IEnumerable<object[]> TestCases =>
        new List<object[]>
        {
            new object[]{
                TreeNode.Create(new int?[] { 4,2,7,1,3 }), 2, 2 },

            new object[]{
                TreeNode.Create(new int?[] { 4,2,7,1,3 }), 5, null },
        };


    [Theory]
    [MemberData(nameof(TestCases))]
    public void TestUUT1(TreeNode root, int val, int? expected)
    {
        TreeNode? r = SearchBinarySearchTree.SearchBST(root, val);
        if (r == null && expected == null)
            return;

        r.val.Should().Be(val);
    }

    [Theory]
    [MemberData(nameof(TestCases))]
    public void TestUUT2(TreeNode root, int val, int? expected)
    {
        TreeNode? r = SearchBinarySearchTree.SearchBST2(root, val);
        if (r == null && expected == null)
            return;

        r.val.Should().Be(val);
    }
}


[ShortRunJob, MemoryDiagnoser]
public class SearchBinarySearchTree1Benchmark
{
    [Params(1, 2, 3, 1_000, 100_000, 10_000_000)]
    public int length;


    [Benchmark(Baseline = true)]
    public TreeNode Benchmark1()
    {
        int?[] nums = HelperExt.Random.Bytes(length).Select(c => (int?)c).ToArray();
        TreeNode t = TreeNode.Create(nums);

        return SearchBinarySearchTree.SearchBST(t, 42);
    }

    [Benchmark()]
    public TreeNode Benchmark2()
    {
        int?[] nums = HelperExt.Random.Bytes(length).Select(c => (int?)c).ToArray();
        TreeNode t = TreeNode.Create(nums);

        return SearchBinarySearchTree.SearchBST2(t, 42);
    }
}
