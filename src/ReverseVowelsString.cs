using leetcode.problems.Helpers;

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

public class ReverseVowelsString
{
    /*
        doCleanup = false:
        t = 25m
        Runtime 82 ms Beats 87.76% Memory 41 MB Beats 77.83%
     */
    /*
        doCleanup = true:
        Runtime 116 ms Beats 15.70% Memory 38.3 MB Beats 100%
     */
    public static string ReverseVowels1(string s, bool doCleanup = false)
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

        if (doCleanup)
            GC.Collect();

        return new string(str);
    }
}


public class ReverseVowelsStringUnitTests
{
    [Theory]
    [InlineData("hello", "holle")]
    [InlineData("leetcode", "leotcede")]
    [InlineData("a", "a")]
    [InlineData("b", "b")]
    [InlineData("ab", "ab")]
    [InlineData("ba", "ba")]
    public void TestUUT1(string s, string expected)
    {
        ReverseVowelsString.ReverseVowels1(s).Should().Be(expected);
    }
}


[ShortRunJob, MemoryDiagnoser]
public class ReverseVowelsStringBenchmark
{ 
    [Params(0, 1, 2, 3, 5, 10, 100, 1_000, 10_000, 100_000, 1_000_000)]
    public int length;



    [Benchmark]
    public string BenchmarkReverseVowels1()
    {
        return ReverseVowelsString.ReverseVowels1(HelperExt.Random.String(length));
    }

    [Benchmark]
    public string BenchmarkReverseVowels2()
    {
        return ReverseVowelsString.ReverseVowels1(HelperExt.Random.String(length), true);
    }
}
