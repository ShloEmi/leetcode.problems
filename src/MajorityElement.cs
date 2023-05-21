using FluentAssertions;

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
        if (nums == null || nums.Length == 0) 
            return int.MinValue;


        int candidate = 0, 
            count = 0, 
            majorCount = nums.Length / 2;

        for (int i = 0; i < nums.Length; i++)
        {
            if (candidate == nums[i])
            {
                if (++count > majorCount)
                    return candidate;
            }
            else if (count == 0)
            {
                candidate = nums[i];
                count++;
            } 
            else
                count--;
        }

        return candidate;
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
