namespace leetcode.problems.ValidParentheses;
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
    protected static readonly IReadOnlyDictionary<char, char> brackets = new Dictionary<char, char>(3) {
            { '(', ')' },
            { '[', ']' },
            { '{', '}' }
        };

    public static bool IsValid(string input)
    {
        var stack = new Stack<char>();

        foreach (char nextInput in input)
        {
            if (brackets.ContainsKey(nextInput))
                stack.Push(nextInput);
            else
            {
                if (!stack.TryPop(out char last))
                    return false;

                if (nextInput != brackets[last])
                    return false;
            }
        }

        return stack.Count == 0;
    }


    [Theory]
    [InlineData("()")]
    [InlineData("()[]{}")]
    public void TestUUT__WithValidInput__ShouldBeTrue(string testInput)
    {
        IsValid(testInput)
            .Should().BeTrue();
    }

    [Theory]
    [InlineData(@"(]")]
    [InlineData("(")]
    [InlineData("}")]
    [InlineData("]")]
    public void TestUUT__WithInvalidInput__ShouldBeFalse(string testInput)
    {
        IsValid(testInput)
            .Should().BeFalse();
    }
}