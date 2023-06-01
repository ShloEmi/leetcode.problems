using leetcode.problems.Solution.Helpers;

namespace leetcode.problems.GuessNumberHigherLower;

/*
https://leetcode.com/problems/guess-number-higher-or-lower/

374. Guess Number Higher or Lower
Easy

We are playing the Guess Game. The game is as follows:

I pick a number from 1 to n. You have to guess which number I picked.

Every time you guess wrong, I will tell you whether the number I picked is higher or lower than your guess.

You call a pre-defined API int guess(int guess), which returns three possible results:

-1: Your guess is higher than the number I picked (i.e. guess > pick).
1: Your guess is lower than the number I picked (i.e. guess < pick).
0: your guess is equal to the number I picked (i.e. guess == pick).
Return the number that I picked.

 

Example 1:
Input: n = 10, pick = 6
Output: 6

Example 2:
Input: n = 1, pick = 1
Output: 1

Example 3:
Input: n = 2, pick = 1
Output: 1
 

Constraints:
1 <= n <= 2^31 - 1
1 <= pick <= n
*/


public class GuessNumberHigherLower
{
    internal readonly int pick = 0;


    public GuessNumberHigherLower(int p) => 
        pick = p;


    int guess(int length)
    {
        if (length > pick)
            return -1;
        else if (length < pick)
            return 1;
        return 0;
    }

    // t1 planing = 1m
    // t1 = FAILED!! due to boundaries :/
    // Runtime1 24 ms Beats 74.35% Memory 26.9 MB Beats 7.77%
    public int GuessNumber(int n)
    {
        long low=1, high=n;
        int mid=(int)(low+(high-low)/2), gResult;
        
        do 
        {
            gResult = guess(mid);
            if (gResult < 0)
                high=mid - 1;
            else if (gResult > 0)
                low = mid + 1;

            mid = (int)(low + (high - low) / 2);
        } while ( gResult != 0);

        return mid;
    }
}


public class GuessNumberHigherLowerUnitTests
{
    private readonly ITestOutputHelper output;
    public GuessNumberHigherLowerUnitTests(ITestOutputHelper output)
        => this.output = output;


    public static IEnumerable<object[]> TestCases =>
        new List<object[]>
        {
            new object[]{10, 6},
            new object[]{1, 1},
            new object[]{2, 1},

            new object[]{2, 2},
            new object[]{ 2147483647, 2147483647},
        };


    [Theory]
    [MemberData(nameof(TestCases))]
    public void TestUUT1(int n, int expected)
    {
        var tc = new GuessNumberHigherLower(expected);
        tc.GuessNumber(n).Should().Be(expected);
    }
}


[ShortRunJob, MemoryDiagnoser]
public class GuessNumberHigherLower1Benchmark
{
    [Params(1, 2, 3, 1_000, 100_000, 10_000_000)]
    public int num;


    [Benchmark(Baseline = true)]
    public int Benchmark1()
    {
        int n = HelperExt.Random.Int(1, num);
        return new GuessNumberHigherLower(n).GuessNumber(num);
    }
}
