using FluentAssertions;
using System.Runtime.CompilerServices;

namespace leetcode.problems.MajorityElement;
/*
169. Majority Element
Easy
14.7K
446
Companies
Given an array nums of size n, return the majority element.

The majority element is the element that appears more than [n / 2] times. You may assume that the majority element always exists in the array.
 

Example 1:
Input: nums = [3,2,3]
Output: 3

Example 2:
Input: nums = [2,2,1,1,1,2,2]
Output: 2
 

Constraints:

n == nums.length
1 <= n <= 5 * 104
-109 <= nums[l] <= 109
 

Follow-up: Could you solve the problem in linear time and in O(1) space?
*/


public class MajorityElementUnitTests
{
    public int MajorityElement(int[] nums)
    {
        if (nums == null) 
            return int.MinValue;

        if (nums.Length == 1)
            return nums[0];


        int result;
        Random rndProvider = new();
        int l=0, r=nums.Length-1;

        while (true)
        {
            swap(ref nums[l], ref nums[rndProvider.Next(0, r - l + 1)]);
            (l, r ) = /*use queck sort*/Partition(nums, l, r);
            // find if left or right length is better, and return new boundaries accordingly.. 
        }


        return result;
    }

    private (int l, int r) Partition(int[] nums, int l, int r)
    {
        while (nums[l] < nums[l]) {
            l++;
        }
        while (nums[r] >= nums[l]) r--;

        return (0,0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void swap(ref int v1, ref int v2)
    {
        int t = v1;
        v1=v2;
        v2=t;
    }


    [Theory]
    [InlineData(new [] {1}, 1)]
    [InlineData(new [] {1, 1}, 1)]
    [InlineData(new [] {1, 1, 1 }, 1)]

    [InlineData(new [] {-1, 1, 1, 1 }, 1)]
    [InlineData(new [] { 5, 1, 1, 1 }, 1)]
    [InlineData(new [] { -1, 1, 1, 1, 2 }, 1)]

    [InlineData(new [] { 3, 2, 3 }, 3)]
    [InlineData(new [] { 2, 2, 1, 1, 1, 2, 2 }, 2)]
    public void Test__MajorityElement__TBD(int[] nums, int expected)
    {
        MajorityElement(nums).Should().Be(expected);
    }
}