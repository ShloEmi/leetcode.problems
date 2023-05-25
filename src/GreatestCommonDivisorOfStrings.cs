using System.Text;

namespace leetcode.problems.GreatestCommonDivisorOfStrings;
/*
https://leetcode.com/problems/merge-strings-alternately/?envType=study-plan-v2&id=leetcode-75
(> 1h)

1071. Greatest Common Divisor of Strings
Easy
3.4K
546
Companies
For two strings s and t, we say "t divides s" if and only if s = t + ... + t (i.e., t is concatenated with itself one or more times).

Given two strings sb1 and sb2, return the largest string x such that x divides both sb1 and sb2.

 

Example 1:

Input: sb1 = "ABCABC", sb2 = "ABC"
Output: "ABC"
Example 2:

Input: sb1 = "ABABAB", sb2 = "ABAB"
Output: "AB"
Example 3:

Input: sb1 = "LEET", sb2 = "CODE"
Output: ""
 

Constraints:

1 <= sb1.length, sb2.length <= 1000
sb1 and sb2 consist of English uppercase letters.
*/


public class GreatestCommonDivisorOfStringsUnitTests
{
    public static string GcdOfStrings(string str1, string str2)
    {
        /// <remarks> Runtime 77 ms, Beats 90.14 ; %Memory 37.6MB, Beats 97.18 %</remarks>
        StringBuilder GcdOfStrings(StringBuilder sb1, StringBuilder sb2)
        {
            StringBuilder sbt;

            do
            {
                if (sb2.Length > sb1.Length)    // swap, sb2 is smallest
                {
                    sbt = sb1;
                    sb1 = sb2;
                    sb2 = sbt;
                }

                if (sb2.Length == 0)
                    return sb1;


                if (StartWith(sb1, sb2))
                    sb1.Remove(0, sb2.Length);
                else
                    return sb1.Clear();
            } while (true);
        }

        static bool StartWith(StringBuilder sb1, StringBuilder sb2)
        {
            if (sb2.Length > sb1.Length)
                return false;

            for (int i = 0; i < sb2.Length; i++)
                if (sb2[i] != sb1[i]) 
                    return false;

            return true;;
        }


        if (str1 == null || str2 == null)
            return string.Empty;

        return GcdOfStrings(new StringBuilder(str1), new StringBuilder(str2)).ToString();
    }

    /// <remarks> version 2 - using the stack</remarks>
    public static string GcdOfStrings2(string str1, string str2)
    {
        if (str2.Length > str1.Length)
            return GcdOfStrings(str2, str1);

        if (str2.Equals(str1))
            return str1;

        if (str1.StartsWith(str2))
            return GcdOfStrings(str1[str2.Length..], str2);

        return string.Empty;
    }


    [Theory]
    [InlineData(null, null, "")]
    [InlineData(null, "pqr", "")]
    [InlineData("abc", null, "")]

    [InlineData("ABCABC", "ABC", "ABC")]
    [InlineData("ABABAB", "ABAB", "AB")]
    [InlineData("LEET", "CODE", "")]

    [InlineData("TAUXXTAUXXTAUXXTAUXXTAUXX", "TAUXXTAUXXTAUXXTAUXXTAUXXTAUXXTAUXXTAUXXTAUXX", "TAUXX")]
    public static void TestUUT(string str1, string str2, string expected)
    {
        GcdOfStrings(str1, str2).Should().Be(expected);
    }
}
