using System.Text;

namespace leetcode.problems.ArrayString;

/*
https://leetcode.com/problems/merge-strings-alternately/?envType=study-plan-v2&id=leetcode-75
(12m)

1768. Merge Strings Alternately
Easy
2.1K
38
Companies
You are given two strings word1 and word2. Merge the strings by adding letters in alternating order, starting with word1. If a string is longer than the other, append the additional letters onto the end of the merged string.

Return the merged string.

 

Example 1:

Input: word1 = "abc", word2 = "pqr"
Output: "apbqcr"
Explanation: The merged string will be merged as so:
word1:  a   b   c
word2:    p   q   r
merged: a p b q c r
Example 2:

Input: word1 = "ab", word2 = "pqrs"
Output: "apbqrs"
Explanation: Notice that as word2 is longer, "rs" is appended to the end.
word1:  a   b 
word2:    p   q   r   s
merged: a p b q   r   s
Example 3:

Input: word1 = "abcd", word2 = "pq"
Output: "apbqcd"
Explanation: Notice that as word1 is longer, "cd" is appended to the end.
word1:  a   b   c   d
word2:    p   q 
merged: a p b q c   d
 

Constraints:

1 <= word1.length, word2.length <= 100
word1 and word2 consist of lowercase English letters.
*/


public class MergeStringsAlternatelyUnitTests
{
    public string? MergeAlternately(string word1, string word2)
    {
        if (word1 == null) return word2;
        if (word2 == null) return word1;
        if (word1 == null && word2 == null) return null;

        StringBuilder sb = new(word1.Length + word2.Length);
        for (int i = 0, j = 0; i < word1.Length || j < word2.Length; i++, j++)
        {
            if (i < word1.Length)
                sb.Append(word1[i]);
            if (j < word2.Length)
                sb.Append(word2[j]);
        }

        return sb.ToString();
    }


    [Theory]
    [InlineData(null, null, null)]
    [InlineData(null, "pqr", "pqr")]
    [InlineData("abc", null, "abc")]

    [InlineData("abc", "pqr", "apbqcr")]
    [InlineData("ab", "pqrs", "apbqrs")]
    [InlineData("abcd", "pq", "apbqcd")]
    public void TestUUT(string word1, string word2, string expected)
    {
        MergeAlternately(word1, word2).Should().Be(expected);
    }
}
