using leetcode.problems.Solution.Helpers;

namespace leetcode.problems.UniqueNumberOccurrences;
/*
https://leetcode.com/problems/unique-number-of-occurrences/description/?envType=study-plan-v2&id=leetcode-75

1207. Unique Number of Occurrences
Easy

Given an array of integers arr, return true if the number of occurrences of each value in the array is unique or false otherwise.
 

Example 1:
Input: arr = [1,2,2,1,1,3]
Output: true
Explanation: The value 1 has 3 occurrences, 2 has 2 and 3 has 1. No two values have the same number of occurrences.

Example 2:
Input: arr = [1,2]
Output: false

Example 3:
Input: arr = [-3,0,1,-3,1,1,1,-3,10,0]
Output: true


Constraints:
1 <= arr.length <= 1000
-1000 <= arr[i] <= 1000
*/


public class UniqueNumberOccurrences
{
    // 3m planning
    // t1 = 20m
    // Runtime1 111 ms Beats 13.33% Memory 41.1 MB Beats 5.88%
    public static bool UniqueOccurrences1(int[] arr)
    {
        if (arr.Length == 1)
            return true;

        Dictionary<int, int> itemsCount = arr
            .GroupBy(i => i)
            .ToDictionary( g => g.Key, g => g.Count());
        HashSet<int> itemsCountUnique = new(itemsCount.Values);

        return itemsCountUnique.Count == itemsCount.Values.Count;
    }

    // t2 = t1 + 22m
    // Runtime2 95 ms Beats 89.2% Memory 40.6 MB Beats 23.14%
    // ~ a bit faster than UniqueOccurrences1 (and less memory usage)
    public static bool UniqueOccurrences2(int[] arr)
    {
        if (arr.Length == 1)
            return true;

        int[] itemsCount = arr
            .GroupBy(i => i)
            .Select(g => g.Count())
            .ToArray();
        Array.Sort(itemsCount);

        for (int i = 1; i < itemsCount.Length; i++)
        {
            if (itemsCount[i-1] == itemsCount[i])
                return false;
        }
        return true;
    }
}


public class UniqueNumberOccurrencesUnitTests
{
    private readonly ITestOutputHelper output;
    public UniqueNumberOccurrencesUnitTests(ITestOutputHelper output) 
        => this.output = output;


    [Theory]
    [InlineData(new int[] { 1, 2, 2, 1, 1, 3 }, true)]
    [InlineData(new int[] { 1, 2 }, false)]
    [InlineData(new int[] { -3, 0, 1, -3, 1, 1, 1, -3, 10, 0 }, true)]
    public void TestUUT1(int[] arr, bool expected)
    {
        UniqueNumberOccurrences.UniqueOccurrences1(arr).Should().Be(expected);
        UniqueNumberOccurrences.UniqueOccurrences2(arr).Should().Be(expected);
    }
}


[ShortRunJob, MemoryDiagnoser]
public class UniqueNumberOccurrences1Benchmark
{
    [Params(1, 2, 3, 1_000, 100_000, 10_000_000)]
    public int length;


    [Benchmark(Baseline = true)]
    public bool Benchmark1()
    {
        int[] nums = HelperExt.Random.Bytes(length).Select(c => (int)c).ToArray();

        return UniqueNumberOccurrences.UniqueOccurrences1(nums);
    }

    [Benchmark()]
    public bool Benchmark2()
    {
        int[] nums = HelperExt.Random.Bytes(length).Select(c => (int)c).ToArray();

        return UniqueNumberOccurrences.UniqueOccurrences2(nums);
    }
}
