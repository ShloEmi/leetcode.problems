using leetcode.problems.Solution.Helpers;

namespace leetcode.problems.FindPivotIndex;
/*
 NOTE:
~~~~~~
- Description or examples are unclear or incorrect
- Edge cases are too frustrating to solve
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

724. Find Pivot Index
Easy

Given an array of integers nums, calculate the pivot index of this array.

The pivot index is the index where the sum of all the numbers strictly to the left of the index is equal to the sum of all the numbers strictly 
 to the index's right.

If the index is on the left edge of the array, then the left sum is 0 because there are no elements to the left. 
This also applies to the right edge of the array.

Return the leftmost pivot index. If no such index exists, return -1.

 

Example 1:
Input: nums = [1,7,3,6,5,6]
Output: 3
Explanation:
The pivot index is 3.
Left sum = nums[0] + nums[1] + nums[2] = 1 + 7 + 3 = 11
Right sum = nums[4] + nums[5] = 5 + 6 = 11

Example 2:
Input: nums = [1,2,3]
Output: -1
Explanation:
There is no index that satisfies the conditions in the problem statement.

Example 3:
Input: nums = [2,1,-1]
Output: 0
Explanation:
The pivot index is 0.
Left sum = 0 (no elements to the left of index 0)
Right sum = nums[1] + nums[2] = 1 + -1 = 0
 

Constraints:
1 <= nums.length <= 104
-1000 <= nums[i] <= 1000
*/


public class FindPivotIndex
{
    // 13m planning
    // t2 = >1h :/
    // Runtime2 112 ms Beats 52.57% Memory 46.1 MB Beats 46.78%
    public static int PivotIndex2(int[] nums)
    {
        if (nums == null)
            return -1;


        if (nums.Length == 0)
            return -1;

        if (nums.Length == 1)
            return 0;

        int sum = nums.Sum();

        int suml = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (suml == sum - suml - nums[i])
                return i;

            suml += nums[i];
        }

        return -1;
    }


    // t3 = t2+2m
    // Runtime3 121 ms Beats 27.4% Memory 46.2 MB Beats 32.94%
    // ~2% faster than PivotIndex2
    public static int PivotIndex3(int[] nums)
    {
        if (nums == null)
            return -1;


        if (nums.Length == 0)
            return -1;

        if (nums.Length == 1)
            return 0;

        int sum = nums.Sum();

        int suml = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (suml*2 == sum - nums[i])
                return i;

            suml += nums[i];
        }

        return -1;
    }

    // t4 = t2+1m
    // Runtime4 113 ms Beats 49.49% Memory 46.3 MB Beats 24.7%
    // ~35% faster than PivotIndex2 , ~1% better memory than PivotIndex2
    public static int PivotIndex4(int[] nums)
    {
        if (nums == null)
            return -1;


        int length = nums.Length;
        if (length == 0)
            return -1;

        if (length == 1)
            return 0;

        int sum=0;
        for (int i = 0; i < length; i++)
            sum += nums[i];

        int suml = 0;
        for (int i = 0; i < length; i++)
        {
            if (suml * 2 == sum - nums[i])
                return i;

            suml += nums[i];
        }

        return -1;
    }
}


public class FindPivotIndexUnitTests
{
    private readonly ITestOutputHelper output;
    public FindPivotIndexUnitTests(ITestOutputHelper output) 
        => this.output = output;


    [Theory]
    [InlineData(new int[] { 1, 7, 3, 6, 5, 6 }, 3)]
    [InlineData(new int[] { 1, 2, 3 }, -1)]
    [InlineData(new int[] { 2, 1, -1 }, 0)]

    [InlineData(new int[] { 1000, -2001, 1001 }, -1)]
    [InlineData(new int[] { 1000, -1001, -1000, 1001 }, -1)]
    [InlineData(new int[] { 1,3,4,-10,2}, -1)]
    [InlineData(new int[] { 0 }, 0)]
    [InlineData(new int[] { 1 }, 0)]
    [InlineData(new int[] { -1 }, 0)]
    [InlineData(new int[] { }, -1)]
    [InlineData(new int[] { -1, -1, -1, 1, 1, 1 }, -1)]
    public void TestUUT1(int[] gain, int expected)
    {
        FindPivotIndex.PivotIndex2(gain).Should().Be(expected);
        FindPivotIndex.PivotIndex3(gain).Should().Be(expected);
        FindPivotIndex.PivotIndex4(gain).Should().Be(expected);
    }
}


[ShortRunJob, MemoryDiagnoser]
public class FindPivotIndexBenchmark
{
    [Params(1, 2, 3, 10, 100, 1_000, 10_000, 100_000, 1_000_000, 10_000_000)]
    public int length;


    [Benchmark(Baseline = true)]
    public int Benchmark2()
    {
        int[] nums = HelperExt.Random.Bytes(length).Select(c => (int)c).ToArray();

        return FindPivotIndex.PivotIndex2(nums);
    }

    [Benchmark()]
    public int Benchmark3()
    {
        int[] nums = HelperExt.Random.Bytes(length).Select(c => (int)c).ToArray();

        return FindPivotIndex.PivotIndex3(nums);
    }

    [Benchmark()]
    public int Benchmark4()
    {
        int[] nums = HelperExt.Random.Bytes(length).Select(c => (int)c).ToArray();

        return FindPivotIndex.PivotIndex4(nums);
    }
}
