using FluentAssertions;

namespace leetcode.problems.ContainsDuplicate;
/*
20. Valid Parentheses
Given a string s containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid.

An input string is valid if:

Open brackets must be closed by the same type of brackets.
Open brackets must be closed in the correct order.
Every close bracket has a corresponding open bracket of the same type.
 

Example 1:

Input: s = "()"
Output: true
Example 2:

Input: s = "()[]{}"
Output: true
Example 3:

Input: s = "(]"
Output: false
 

Constraints:

1 <= s.length <= 104
s consists of parentheses only '()[]{}'.

 */

public class ValidParenthesesUnitTests
{
    public static bool IsValid(string s)
    {
        return false;
    }



    [Theory]
    [InlineData("()")]
    [InlineData("()[]{}")]
    public void Test__IsValid__WithValidInput__ShouldBeTrue(string testInput)
    {
        IsValid(testInput)
            .Should().BeTrue();
    }


    [Theory]
    [InlineData(@"(]")]
    public void Test__IsValid__WithInvalidInput__ShouldBeFalse(string testInput)
    {
        IsValid(testInput)
            .Should().BeFalse();
    }
}