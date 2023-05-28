using leetcode.problems.Solution.Helpers;

namespace leetcode.problems.FindDifferenceTwoArrays;
/*
2215. Find the Difference of Two Arrays
Easy

Given two 0-indexed integer arrays nums1 and nums2, return a list1 answer of size 2 where:

answer[0] is a list1 of all distinct integers in nums1 which are not present in nums2.
answer[1] is a list1 of all distinct integers in nums2 which are not present in nums1.
Note that the integers in the lists may be returned in any order.

 

Example 1:
Input: nums1 = [1,2,3], nums2 = [2,4,6]
Output: [[1,3],[4,6]]
Explanation:
For nums1, nums1[1] = 2 is present at index 0 of nums2, whereas nums1[0] = 1 and nums1[2] = 3 are not present in nums2. Therefore, answer[0] = [1,3].
For nums2, nums2[0] = 2 is present at index 1 of nums1, whereas nums2[1] = 4 and nums2[2] = 6 are not present in nums2. Therefore, answer[1] = [4,6].

Example 2:
Input: nums1 = [1,2,3,3], nums2 = [1,1,2,2]
Output: [[3],[]]
Explanation:
For nums1, nums1[2] and nums1[3] are not present in nums2. Since nums1[2] == nums1[3], their value is only included once and answer[0] = [3].
Every integer in nums2 is present in nums1. Therefore, answer[1] = [].
 

Constraints:1
1 <= nums1.length, nums2.length <= 1000
-1000 <= nums1[i], nums2[i] <= 1000
*/


public class FindDifferenceTwoArrays
{
    // 3m planning
    // t1 = 20m ?
    // Runtime1 178 ms Beats 66.36% Memory 59.2 MB Beats 11.98%
    public static IList<IList<int>> FindDifference1(int[] nums1, int[] nums2)
    {
        List<IList<int>> result = new(2);

        HashSet<int> hs = new(nums2);
        HashSet<int> hsr = new();
        foreach (var num in nums1)
            if (!hs.Contains(num))
                hsr.Add(num);
        result.Add(new List<int>(hsr));

        hs = new(nums1);
        hsr = new();
        foreach (var num in nums2)
            if (!hs.Contains(num))
                hsr.Add(num);
        result.Add(new List<int>(hsr));

        return result;
    }

    // Runtime2 193 ms Beats 24.9% Memory 59.2 MB Beats 11.98%
    // ~0.85/1 faster runtime && 1.02/1 more memory than FindDifference1
    public static IList<IList<int>> FindDifference2(int[] nums1, int[] nums2)
    {
        HashSet<int> hs1 = new(nums1);
        HashSet<int> hs2 = new(nums2);

        List<IList<int>> result = new(2)
        {
            hs1.Except(hs2).ToList(),
            hs2.Except(hs1).ToList()
        };

        return result;
    }
}


public class FindDifferenceTwoArraysUnitTests
{
    private readonly ITestOutputHelper output;
    public FindDifferenceTwoArraysUnitTests(ITestOutputHelper output) 
        => this.output = output;


    [Theory]
    [InlineData(new int[] { 1, 2, 3 }, new int[] { 2, 4, 6 } , @"[[1,3],[4,6]]")]
    // [InlineData(new int[] { 1, 2, 3, 3 }, new int[] { 1, 1, 2, 2 } , @"[[3],[]]")]
    public void TestUUT1(int[] nums1, int[] nums2, string expected)
    {
        string[] r = expected[2..^2].Split(@"],[");
        List<int> e0 = r[0].Split(@",").Select(s => int.Parse(s)).ToList();
        List<int> e1 = r[1].Split(@",").Select(s => int.Parse(s)).ToList();

        IList<IList<int>> list1 = FindDifferenceTwoArrays.FindDifference1(nums1, nums2);
        list1[0].Should().Equal(e0);
        list1[1].Should().Equal(e1);

        IList<IList<int>> list2 = FindDifferenceTwoArrays.FindDifference2(nums1, nums2);
        list2[0].Should().Equal(e0);
        list2[1].Should().Equal(e1);
    }
}


[ShortRunJob, MemoryDiagnoser]
public class FindDifferenceTwoArrays1Benchmark
{
    [Params(/*1, 2, 3, 10, 100,*/ 1_000, 10_000, 100_000/*, 1_000_000, 10_000_000*/)]
    public int length;

    [Params(/*1, 2, 3, 10, 100,*/ 1_000, 10_000, 100_000/*, 1_000_000, 10_000_000*/)]
    public int length2;


    [Benchmark(Baseline = true)]
    public IList<IList<int>> Benchmark1()
    {
        int[] nums = HelperExt.Random.Bytes(length).Select(c => (int)c).ToArray();
        int[] nums2 = HelperExt.Random.Bytes(length).Select(c => (int)c).ToArray();

        return FindDifferenceTwoArrays.FindDifference1(nums, nums2);
    }

    [Benchmark()]
    public IList<IList<int>> Benchmark2()
    {
        int[] nums = HelperExt.Random.Bytes(length).Select(c => (int)c).ToArray();
        int[] nums2 = HelperExt.Random.Bytes(length).Select(c => (int)c).ToArray();

        return FindDifferenceTwoArrays.FindDifference2(nums, nums2);
    }
}
