using FluentAssertions;

namespace leetcode.problems.ContainsDuplicate;
/*
217. Contains Duplicate
Given an integer array nums, return true if any value appears at least twice in the array, and return false if every element is distinct.

 

Example 1:

Input: nums = [1,2,3,1]
Output: true
Example 2:

Input: nums = [1,2,3,4]
Output: false
Example 3:

Input: nums = [1,1,1,3,3,4,3,2,4,2]
Output: true
 

Constraints:

1 <= nums.length <= 105
-109 <= nums[i] <= 109
 */


public class ContainsDuplicateUnitTests
{
    public static bool ContainsDuplicate(int[] nums)
    {
        HashSet<int> set = new();
        foreach (var num in nums)
        {
            if (set.Contains(num))
                return true;

            set.Add(num);
        }

        return false;
    }


    [Theory]
    [InlineData(new int[] { 1, 1 })]
    [InlineData(new int[] { 1, 2, 3, 1 })]
    [InlineData(new int[] { 1, 1, 1, 3, 3, 4, 3, 2, 4, 2 })]
    public void TestUUT__WithDuplicate__ShouldBeTrue(int[] testInput)
    {
        ContainsDuplicate(testInput)
            .Should().BeTrue();
    }

    [Theory]
    [InlineData(new int[] { 1, 2 })]
    [InlineData(new int[] { 1, 2, 3, 4 })]
    [InlineData(new int[] { 1, 3, 4, 11, 33, 44 })]
    public void TestUUT__WithoutDuplicate__ShouldBeFalse(int[] testInput)
    {
        ContainsDuplicate(testInput)
            .Should().BeFalse();
    }
}