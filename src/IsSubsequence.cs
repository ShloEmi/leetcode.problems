using leetcode.problems.Helpers;

namespace leetcode.problems.IsSubsequence;
/*
392. Is Subsequence
Easy

Given two strings s and t, return true if s is a subsequence of t, or false otherwise.

A subsequence of a string is a new string that is formed from the original string by deleting some (can be none) of the characters 
 without disturbing the relative positions of the remaining characters. (i.e., "ace" is a subsequence of "abcde" while "aec" is not).
 

Example 1:
Input: s = "abc", t = "ahbgdc"
Output: true

Example 2:
Input: s = "axc", t = "ahbgdc"
Output: false

 
Constraints:
0 <= s.length <= 100
0 <= t.length <= 104
s and t consist only of lowercase English letters.
 

Follow up: Suppose there are lots of incoming s, say s1, s2, ..., sk where k >= 109, and you want to check one by one to see if t has its subsequence. In this scenario, how would you change your code?
*/


public class IsSubsequence
{
    // t = ?m
    // Runtime 
    public static bool IsSubsequence1(string s, string t)
    {
        return false;
    }
}


public class IsSubsequenceUnitTests
{
    private readonly ITestOutputHelper output;
    public IsSubsequenceUnitTests(ITestOutputHelper output) 
        => this.output = output;


    [Theory]
    [InlineData("abc", "ahbgdc", true)]
    [InlineData("axc", "ahbgdc", false)]
    public void TestUUT1(string s, string t, bool expected)
    {
        IsSubsequence.IsSubsequence1(s, t).Should().Be(expected);
    }
}


[ShortRunJob, MemoryDiagnoser]
public class IsSubsequenceBenchmark
{
    [Params(0, 1, 2, 3, 5, 10, 100, 1_000, 10_000, 100_000, 1_000_000)]
    public int length1;

    [Params(0, 1, 2, 3, 5, 10, 100, 1_000, 10_000, 100_000, 1_000_000)]
    public int length2;


    [Benchmark]
    public bool Benchmark1()
    {
        return IsSubsequence.IsSubsequence1(HelperExt.Random.String(length1), HelperExt.Random.String(length2));
    }
}
