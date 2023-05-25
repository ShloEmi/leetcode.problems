using BenchmarkDotNet.Attributes;
using FluentAssertions;
using Xunit.Abstractions;

namespace leetcode.problems.MoveZeroes;
/*
283. Move Zeroes
Easy

Given an integer array nums, move all 0's to the end of it while maintaining the relative order of the non-zero elements.

Note that you must do this in-place without making a copy of the array.

 

Example 1:
Input: nums = [0,1,0,3,12]
Output: [1,3,12,0,0]

Example 2:
Input: nums = [0]
Output: [0]
 

Constraints:
1 <= nums.length <= 104
-231 <= nums[i] <= 231 - 1
 

Follow up: Could you minimize the total number of operations done?
*/


public class MoveZeroes
{
    // t = 38m
    // Runtime 162 ms Beats 70.39%  Memory 54 MB Beats 51.2%
    public static void MoveZeroes1(int[] nums)
    {
        if (nums == null)
            return;


        if (nums.Length == 1)
            return;

        int i = 0, lastZero = nums.Length;
        do // A BIT FASTER RESULTS THAN: for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != 0)
            {
                if (lastZero >= i)
                    continue;

                nums[lastZero] = nums[i];   // swap
                nums[i] = 0;

                do
                {
                    ++lastZero;
                } while (nums[lastZero] != 0);
            }
            else
            {
                if (lastZero == nums.Length)
                    lastZero = i;
            }

        } while (++i < nums.Length);

        return;
    }
}


public class MoveZeroesUnitTests
{
    private readonly ITestOutputHelper output;


    public MoveZeroesUnitTests(ITestOutputHelper output)
    {
        this.output = output;
    }


    [Theory]
    [InlineData(new int[] { 0, 1, 0, 3, 12 }, new int[] { 1, 3, 12, 0, 0 })]
    [InlineData(new int[] { 0 }, new int[] { 0 })]

    [InlineData(new int[] { 1 }, new int[] { 1 })]
    [InlineData(new int[] { 1, 0 }, new int[] { 1, 0 })]
    [InlineData(new int[] { 0, 1, 0 }, new int[] { 1, 0, 0 })]
    [InlineData(new int[] { 0, 0, 1, 0 }, new int[] { 1, 0, 0, 0 })]
    [InlineData(new int[] { 1, 0, 0, 1 }, new int[] { 1, 1, 0, 0 })]
    public void TestUUT1(int[] nums, int[] expected)
    {
        MoveZeroes.MoveZeroes1(nums);
        nums.Should().Equal(expected);
    }
}


[ShortRunJob, MemoryDiagnoser]
public class MoveZeroesBenchmark
{ 
    [Params(0, 1, 2, 3, 5, 10, 100, 1_000, 10_000, 100_000, 1_000_000)]
    public int length;

    private static Random random = new();


    [Benchmark]
    public int[] Benchmark1()
    {
        int[] nums = Enumerable.Range(0, length).Select(i => random.Next()).ToArray();
        MoveZeroes.MoveZeroes1(nums);
        return nums;
    }
}
