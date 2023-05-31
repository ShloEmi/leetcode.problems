using leetcode.problems.Solution.Helpers;
using TreeNode = leetcode.problems.MaximumDepthBinaryTree.MaximumDepthBinaryTree.TreeNode;


namespace leetcode.problems.MaximumDepthBinaryTree;

/*
https://leetcode.com/problems/maximum-depth-of-binary-tree/

104. Maximum Depth of Binary Tree
Easy

Given the root of a binary tree, return its maximum depth.

A binary tree's maximum depth is the number of nodes along the longest path from the root node down to the farthest leaf node.


Example 1:
Input: root = [3,9,20,null,null,15,7]
Output: 3

Example 2:
Input: root = [1,null,2]
Output: 2
 

Constraints:
The number of nodes in the tree is in the range [0, 104].
-100 <= Node.val <= 100
*/


public class MaximumDepthBinaryTree
{
    // Definition for a binary tree node.
    public class TreeNode {
        public int val;
        public TreeNode left;
        public TreeNode right;


        public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
            this.val = val;
            this.left = left;
            this.right = right;
        }


        // Function to insert nodes in level order
        public static TreeNode Create(int?[] arr, int i=0)
        {
            TreeNode root = null;
            // Base case for recursion
            if (i < arr.Length)
            {
                if (arr[i] == null)
                    return null;

                root = new TreeNode(arr[i]!.Value)
                {
                    // insert left child
                    left = Create(arr, 2 * i + 1),

                    // insert right child
                    right = Create(arr, 2 * i + 2)
                };
            }
            return root;
        }
    }

    // t1 planing = 2m
    // t1 = 4m
    // Runtime1 94 ms Beats 47.24% Memory 39.8 MB Beats 54.74%
    public static int MaxDepth1(TreeNode root)
    {
        if (root == null)
            return 0;

        return 1 + Math.Max(MaxDepth1(root.left), MaxDepth1(root.right));
    }

    // t2 = t1 + 22m
    // Runtime2 94 ms Beats 47.24% Memory 40 MB Beats 29.23%
    // seems this one is >20% slower, same memory usage though
    public static int MaxDepth2(TreeNode root)
    {
        if (root == null)
            return 0;


        Stack<(TreeNode, int) > s = new();
        s.Push((root, 0));

        int max = 1;
        while (s.Count >= 1)
        {
            (TreeNode currentNode, int count) = s.Pop();

            if (count > max)
                max = count;

            if (currentNode == null)
                continue;

            s.Push((currentNode.left, count+1));
            s.Push((currentNode.right, count+1));
        }

        return max;
    }
}


public class MaximumDepthBinaryTreeUnitTests
{
    private readonly ITestOutputHelper output;
    public MaximumDepthBinaryTreeUnitTests(ITestOutputHelper output)
        => this.output = output;


    public static IEnumerable<object[]> TestCases =>
        new List<object[]>
        {
            new object[]{ TreeNode.Create(new int?[] {3,9,20,null,null,15,7}), 3 },
            new object[]{ TreeNode.Create(new int?[] { 1, null, 2 }), 2 },
        };


    [Theory]
    [MemberData(nameof(TestCases))]
    public void TestUUT1(TreeNode root, int expected)
    {
        MaximumDepthBinaryTree.MaxDepth1(root).Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(TestCases))]
    public void TestUUT2(TreeNode root, int expected)
    {
        MaximumDepthBinaryTree.MaxDepth2(root).Should().Be(expected);
    }
}


[ShortRunJob, MemoryDiagnoser]
public class MaximumDepthBinaryTree1Benchmark
{
    [Params(1, 2, 3, 1_000, 100_000, 10_000_000)]
    public int length;


    [Benchmark(Baseline = true)]
    public int Benchmark1()
    {
        int?[] nums = HelperExt.Random.Bytes(length).Select(c => (int?)c).ToArray();
        TreeNode t =  TreeNode.Create(nums);

        return MaximumDepthBinaryTree.MaxDepth1(t);
    }

    [Benchmark()]
    public int Benchmark2()
    {
        int?[] nums = HelperExt.Random.Bytes(length).Select(c => (int?)c).ToArray();
        TreeNode t = TreeNode.Create(nums);

        return MaximumDepthBinaryTree.MaxDepth2(t);
    }
}
