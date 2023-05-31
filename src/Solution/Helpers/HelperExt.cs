namespace leetcode.problems.Solution.Helpers;

internal static class HelperExt
{
    public static Faker Faker = new();
    public static Randomizer Random = new();
}


// Definition for a binary tree node.
public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;


    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }


    // Function to insert nodes in level order
    public static TreeNode Create(int?[] arr, int i = 0)
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
