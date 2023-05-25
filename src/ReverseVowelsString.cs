using BenchmarkDotNet.Attributes;
using FluentAssertions;
using Xunit.Abstractions;

namespace leetcode.problems.ReverseVowelsString;
/*
345. Reverse Vowels of a String
Easy

Given a string s, reverse only all the vowels in the string and return it.

The vowels are 'a', 'e', 'i', 'o', and 'u', and they can appear in both lower and upper cases, more than once.

 

Example 1:
Input: s = "hello"
Output: "holle"

Example 2:
Input: s = "leetcode"
Output: "leotcede"
 

Constraints:
1 <= s.length <= 3 * 105
s consist of printable ASCII characters.
*/


[ShortRunJob, MemoryDiagnoser]
public class ReverseVowelsStringUnitTests
{
    // Runtime 116 ms Beats 15.70% Memory 38.3 MB Beats 100%
    public string ReverseVowels2(string s)
    {
        if (s == null)
            return string.Empty;


        if (s.Length == 1)
            return s;

        HashSet<char> vowels = new() {
            'a', 'e', 'i', 'o', 'u',
            'A', 'E', 'I', 'O', 'U',
            };

        char[] str = new char[s.Length];

        int l = 0, r = s.Length - 1;
        for (; l <= r;)
        {
            while (l <= r && !vowels.Contains(s[l]))
                str[l] = s[l++];
            while (l <= r && !vowels.Contains(s[r]))
                str[r] = s[r--];

            while (l <= r && vowels.Contains(s[l]) && vowels.Contains(s[r]))
            {
                str[l] = s[r];
                str[r] = s[l];
                l++; r--;
            }
        }

        GC.Collect();
        return new string(str);
    }

    // t = 25m
    // Runtime 82 ms Beats 87.76% Memory 41 MB Beats 77.83%
    public string ReverseVowels1(string s)
    {
        if (s == null)
            return string.Empty;


        if (s.Length == 1)
            return s;

        HashSet<char> vowels = new() { 
            'a', 'e', 'i', 'o', 'u',
            'A', 'E', 'I', 'O', 'U', 
            };

        char[] str = new char[s.Length];

        int l = 0, r = s.Length - 1;
        for (; l <= r; )
        {
            while (l <= r && !vowels.Contains(s[l]))
                str[l] = s[l++];
            while (l <= r && !vowels.Contains(s[r]))
                str[r] = s[r--];

            while(l <= r && vowels.Contains(s[l]) && vowels.Contains(s[r]))
            {
                str[l] = s[r];
                str[r] = s[l];
                l++; r--;
            }
        }

        return new string(str);
    }



    #region tests

    public ReverseVowelsStringUnitTests()
    { 
    }

    public ReverseVowelsStringUnitTests(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Theory]
    [InlineData("hello", "holle")]
    [InlineData("leetcode", "leotcede")]
    [InlineData("a", "a")]
    [InlineData("b", "b")]
    [InlineData("ab", "ab")]
    [InlineData("ba", "ba")]
    public void TestUUT(string s, string expected)
    {
        ReverseVowels1(s).Should().Be(expected);
        ReverseVowels2(s).Should().Be(expected);
    }




    #endregion


    #region Benchmark

    private readonly ITestOutputHelper output;

    [Params(0, 1, 2, 3, 5, 10, 100, 1_000, 10_000, 100_000, 1_000_000)]
    public int length;

    private static Random random = new();


    [Benchmark]
    public string BenchmarkReverseVowels1()
    {
        return ReverseVowels1(RandomString(length));
    }

    // [Benchmark]
    public string BenchmarkReverseVowels2()
    {
        return ReverseVowels2(RandomString(length));
    }

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    #endregion
}
