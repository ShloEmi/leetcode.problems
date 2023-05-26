using leetcode.problems.Helpers;

namespace leetcode.problems.MaximumAverageSubarrayI;
/*
643. Maximum Average Subarray I
Easy

You are given an integer array nums consisting of n elements, and an integer k.

Find a contiguous subarray whose length is equal to k that has the maximum average value and return this value. 
Any answer with a calculation error less than 10-5 will be accepted.

 

Example 1:
Input: nums = [1,12,-5,-6,50,3], k = 4
Output: 12.75000
Explanation: Maximum average is (12 - 5 - 6 + 50) / 4 = 51 / 4 = 12.75

Example 2:
Input: nums = [5], k = 1
Output: 5.00000
 

Constraints:
n == nums.length
1 <= k <= n <= 105
-104 <= nums[i] <= 104
*/


public class MaximumAverageSubarrayI
{
    // t1 = 13m
    // Runtime1 288 ms Beats 67.91% Memory 59.5 MB Beats 86.51%
    public static double FindMaxAverage1(int[] nums, int k)
    {
        if (nums == null)
            return double.NaN;
        if (nums.Length < k)
            return double.NaN;


        int max = int.MinValue;
        int sum = 0;
        for (int i = 0; i <= nums.Length - k; i++)
        {
            if (i == 0)
                for (int j = 0; j < k; j++)
                    sum += nums[j];
            else
                sum = sum - nums[i - 1] + nums[i + k - 1];

            if (max < sum)
                max = sum;
        }

        return max / (double)k;
    }

    // eyeballs runtime optimized
    // t2 = t1+5m
    // Runtime 281 ms Beats 84.19% Memory 60.1 MB Beats 13.49%
    public static double FindMaxAverage2(int[] nums, int k)
    {
        if (nums == null)
            return double.NaN;
        if (nums.Length < k)
            return double.NaN;


        int length = nums.Length;
        if (length == 1)
            return nums[0];

        int sum = 0;
        for (int j = 0; j < k; j++)
            sum += nums[j];
        int max = sum;

        for (int i = 1; i <= length - k; i++)
        {
            sum = sum - nums[i - 1] + nums[i + k - 1];

            if (max < sum)
                max = sum;
        }

        return max / (double)k;
    }
}


public class MaximumAverageSubarrayIUnitTests
{
    private readonly ITestOutputHelper output;
    public MaximumAverageSubarrayIUnitTests(ITestOutputHelper output) 
        => this.output = output;


    [Theory]
    [InlineData(new int[] { 1, 12, -5, -6, 50, 3 }, 4, 12.75)]
    [InlineData(new int[] { 5 }, 1, 5.0)]
    [InlineData(new int[] { 0 }, 1, 0.0)]
    [InlineData(new int[] { 0, 1 }, 1, 1.0)]
    [InlineData(new int[] { 0, 1 }, 2, 0.5)]
    [InlineData(new int[] { 0, 1, 10 }, 1, 10)]
    [InlineData(new int[] { 0, 1, 10 }, 2, 5.5)]
    [InlineData(new int[] { 0, 1, 10 }, 3, 11/3.0)]
    public void TestUUT1(int[] nums, int k, double expected)
    {
        MaximumAverageSubarrayI.FindMaxAverage1(nums, k).Should().Be(expected);
        MaximumAverageSubarrayI.FindMaxAverage2(nums, k).Should().Be(expected);
    }
}


[ShortRunJob, MemoryDiagnoser]
public class MaximumAverageSubarrayIBenchmark
{
    [Params(1, 2, 3, 5, 10, 100, 1_000, 10_000, 100_000, 1_000_000)]
    public int length;


    [Benchmark(Baseline = true)]
    public double Benchmark1()
    {
        int k = HelperExt.Random.Int(1, length);
        int[] nums = HelperExt.Random.Bytes(length).Select(c => (int)c).ToArray();

        return MaximumAverageSubarrayI.FindMaxAverage1(nums, k);
    }

    [Benchmark]
    public double Benchmark2()
    {
        int k = HelperExt.Random.Int(1, length);
        int[] nums = HelperExt.Random.Bytes(length).Select(c => (int)c).ToArray();

        return MaximumAverageSubarrayI.FindMaxAverage2(nums, k);
    }
}
