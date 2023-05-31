using leetcode.problems.Solution.Helpers;

namespace leetcode.problems.LeafSimilarTrees;

/*
https://leetcode.com/problems/leaf-similar-trees

872. Leaf-Similar Trees
Easy

Consider all the leaves of a binary tree, from left to right order, the values of those leaves form a leaf value sequence.


For example, in the given tree above, the leaf value sequence is (6, 7, 4, 9, 8).

Two binary trees are considered leaf-similar if their leaf value sequence is the same.

Return true if and only if the two given trees with head nodes root1 and root2 are leaf-similar.


Example 1:
Input: root1 = [3,5,1,6,2,9,8,null,null,7,4], root2 = [3,5,1,6,7,4,2,null,null,null,null,null,null,9,8]
Output: true

Example 2:
Input: root1 = [1,2,3], root2 = [1,3,2]
Output: false
 

Constraints:
The number of nodes in each tree will be in the range [1, 200].
Both of the given trees will have values in the range [0, 200].
*/


public class LeafSimilarTrees
{
    // t1 planing = 2m
    // t1 = 11m
    // Runtime1 109 ms Beats 16.3% Memory 40.7 MB Beats 32.82%
    public static bool LeafSimilar1(TreeNode root1, TreeNode root2)
    {
        Queue<int> TreeNodeLeafs(TreeNode t, Queue<int> q)
        {
            if (t == null)
                return q;

            if (t.left != null)
                TreeNodeLeafs(t.left, q);

            if (t.right != null)
                TreeNodeLeafs(t.right, q);

            if (t.left == null && t.right == null)
                q.Enqueue(t.val);

            return q;
        }

        Queue<int> q1 = TreeNodeLeafs(root1, new());
        Queue<int> q2 = TreeNodeLeafs(root2, new());

        while (q1.Count > 0 && q2.Count > 0)
            if (q1.Dequeue() != q2.Dequeue())
                return false;

        return q1.Count == 0 && q2.Count == 0;
    }

    // t2 = t1 + 2m
    // Runtime2 103 ms Beats 51.91% Memory 40.7 MB Beats 32.82%
    public static bool LeafSimilar2(TreeNode root1, TreeNode root2)
    {
        static Queue<int> TreeNodeLeafs(TreeNode t, Queue<int> q)
        {
            if (t == null)
                return q;

            if (t.left != null)
                TreeNodeLeafs(t.left, q);

            if (t.right != null)
                TreeNodeLeafs(t.right, q);

            if (t.left == null && t.right == null)
                q.Enqueue(t.val);

            return q;
        }

        Queue<int> q1 = TreeNodeLeafs(root1, new());
        Queue<int> q2 = TreeNodeLeafs(root2, new());

        while (q1.Count > 0 && q2.Count > 0)
            if (q1.Dequeue() != q2.Dequeue())
                return false;

        return q1.Count == 0 && q2.Count == 0;
    }

    // t3 = t3 + 2m
    // Runtime3 98 ms Beats 81.68% Memory 40.5 MB Beats 70.99%
    public static bool LeafSimilar3(TreeNode root1, TreeNode root2)
    {
        static Queue<int> TreeNodeLeafs(TreeNode t, Queue<int> q)
        {
            if (t == null)
                return q;

            if (t.left == null && t.right == null)
                q.Enqueue(t.val);

            if (t.left != null)
                TreeNodeLeafs(t.left, q);

            if (t.right != null)
                TreeNodeLeafs(t.right, q);

            return q;
        }

        Queue<int> q1 = TreeNodeLeafs(root1, new());
        Queue<int> q2 = TreeNodeLeafs(root2, new());

        while (q1.Count > 0 && q2.Count > 0)
            if (q1.Dequeue() != q2.Dequeue())
                return false;

        return q1.Count == 0 && q2.Count == 0;
    }
}


public class LeafSimilarTreesUnitTests
{
    private readonly ITestOutputHelper output;
    public LeafSimilarTreesUnitTests(ITestOutputHelper output)
        => this.output = output;


    public static IEnumerable<object[]> TestCases =>
        new List<object[]>
        {
            new object[]{
                TreeNode.Create(new int?[] { 3, 5, 1, 6, 2, 9, 8, null, null, 7, 4 }),
                TreeNode.Create(new int?[] { 3,5,1,6,7,4,2,null,null,null,null,null,null,9,8 }),
                true },

            new object[]{
                TreeNode.Create(new int?[] { 1,2,3 }),
                TreeNode.Create(new int?[] { 1,3,2 }),
                false },
        };


    [Theory]
    [MemberData(nameof(TestCases))]
    public void TestUUT1(TreeNode root1, TreeNode root2, bool expected)
    {
        LeafSimilarTrees.LeafSimilar1(root1, root2).Should().Be(expected);
        LeafSimilarTrees.LeafSimilar2(root1, root2).Should().Be(expected);
        LeafSimilarTrees.LeafSimilar3(root1, root2).Should().Be(expected);
    }
}


[ShortRunJob, MemoryDiagnoser]
public class LeafSimilarTrees1Benchmark
{
    [Params(1, 2, 3, 1_000, 100_000, 10_000_000)]
    public int length;


    [Benchmark(Baseline = true)]
    public bool Benchmark1()
    {
        int?[] nums = HelperExt.Random.Bytes(length).Select(c => (int?)c).ToArray();
        TreeNode t = TreeNode.Create(nums);

        int?[] nums2 = HelperExt.Random.Bytes(length).Select(c => (int?)c).ToArray();
        TreeNode t2 = TreeNode.Create(nums2);

        return LeafSimilarTrees.LeafSimilar1(t, t2);
    }

    [Benchmark()]
    public bool Benchmark2()
    {
        int?[] nums = HelperExt.Random.Bytes(length).Select(c => (int?)c).ToArray();
        TreeNode t = TreeNode.Create(nums);

        int?[] nums2 = HelperExt.Random.Bytes(length).Select(c => (int?)c).ToArray();
        TreeNode t2 = TreeNode.Create(nums2);

        return LeafSimilarTrees.LeafSimilar2(t, t2);
    }

    [Benchmark()]
    public bool Benchmark3()
    {
        int?[] nums = HelperExt.Random.Bytes(length).Select(c => (int?)c).ToArray();
        TreeNode t = TreeNode.Create(nums);

        int?[] nums2 = HelperExt.Random.Bytes(length).Select(c => (int?)c).ToArray();
        TreeNode t2 = TreeNode.Create(nums2);

        return LeafSimilarTrees.LeafSimilar3(t, t2);
    }
}
