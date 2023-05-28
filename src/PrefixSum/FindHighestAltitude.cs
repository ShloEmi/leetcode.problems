using leetcode.problems.Solution.Helpers;

namespace leetcode.problems.FindHighestAltitude;
/*
1732. Find the Highest Altitude
Easy

There is a biker going on a road trip. The road trip consists of n + 1 points at different altitudes. 
The biker starts his trip on point 0 with altitude equal 0.

You are given an integer array gain of length n where gain[i] is the net gain in altitude between points i​​​​​​ and i + 1 for all (0 <= i < n). 
Return the highest altitude of a point.


Example 1:
Input: gain = [-5,1,5,0,-7]
Output: 1
Explanation: The altitudes are [0,-5,-4,1,1,-6]. The highest is 1.

Example 2:
Input: gain = [-4,-3,-2,-1,4,3,2]
Output: 0
Explanation: The altitudes are [0,-4,-7,-9,-10,-6,-3,-1]. The highest is 0.

 
Constraints:
n == gain.length
1 <= n <= 100
-100 <= gain[i] <= 100
*/


public class FindHighestAltitude
{
    // t1 = 10m
    // Runtime1 83 ms Beats 72.77% Memory 38.3 MB Beats 47.52%
    public static int LargestAltitude1(int[] gain)
    {
        if (gain == null)
            return 0;


        int currentAlt=0, maxAlt = currentAlt;
        for (int i = 0; i < gain.Length; i++)
        {
            currentAlt += gain[i];

            if (maxAlt < currentAlt)
                maxAlt = currentAlt;
        }

        return maxAlt;
    }
}


public class FindHighestAltitudeUnitTests
{
    private readonly ITestOutputHelper output;
    public FindHighestAltitudeUnitTests(ITestOutputHelper output) 
        => this.output = output;


    [Theory]
    [InlineData(new int[] { -5, 1, 5, 0, -7 }, 1)]
    [InlineData(new int[] { -4, -3, -2, -1, 4, 3, 2 }, 0)]

    [InlineData(new int[] { -5 }, 0)]
    [InlineData(new int[] { 5 }, 5)]
    public void TestUUT1(int[] gain, int expected)
    {
        FindHighestAltitude.LargestAltitude1(gain).Should().Be(expected);
    }
}


[ShortRunJob, MemoryDiagnoser]
public class FindHighestAltitudeBenchmark
{
    [Params(1, 2, 3, 5, 10, 100, 1_000, 10_000, 100_000, 1_000_000)]
    public int length;


    [Benchmark(Baseline = true)]
    public int Benchmark1()
    {
        int[] nums = HelperExt.Random.Bytes(length).Select(c => (int)c).ToArray();

        return FindHighestAltitude.LargestAltitude1(nums);
    }
}
