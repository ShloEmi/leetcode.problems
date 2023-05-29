using leetcode.problems.Solution.Helpers;
using static leetcode.problems.NumberRecentCalls.NumberRecentCalls;

namespace leetcode.problems.NumberRecentCalls;
/*
https://leetcode.com/problems/number-of-recent-calls/?envType=study-plan-v2&id=leetcode-75

933. Number of Recent Calls
Easy

You have a RecentCounter class which counts the number of recent requests within a certain time frame.

Implement the RecentCounter class:

RecentCounter() Initializes the counter with zero recent requests.
int ping(int t) Adds a new request at time t, where t represents some time in milliseconds, 
 and returns the number of requests that has happened in the past 3000 milliseconds (including the new request). 
Specifically, return the number of requests that have happened in the inclusive range [t - 3000, t].

It is guaranteed that every call to ping uses a strictly larger value of t than the previous call.

 

Example 1:
Input
["RecentCounter", "ping", "ping", "ping", "ping"]
[[], [1], [100], [3001], [3002]]
Output
[null, 1, 2, 3, 3]

Explanation
RecentCounter recentCounter = new RecentCounter();
recentCounter.ping(1);     // requests = [1], range is [-2999,1], return 1
recentCounter.ping(100);   // requests = [1, 100], range is [-2900,100], return 2
recentCounter.ping(3001);  // requests = [1, 100, 3001], range is [1,3001], return 3
recentCounter.ping(3002);  // requests = [1, 100, 3001, 3002], range is [2,3002], return 3
 

Constraints:
1 <= t <= 109
Each test case will call ping with strictly increasing values of t.
At most 104 calls will be made to ping.
*/


public class NumberRecentCalls
{
    // 5m planning
    // t1 = 22m + 5 min improvments
    // Runtime1 279 ms Beats 78.36% Memory 69.1 MB Beats 21.64%
    // Runtime2 271 ms Beats 93.28% Memory 69.4 MB Beats 7.46%


    public class RecentCounter
    {
        static readonly int timeWindow = 3000; //[mSec]
        readonly int maxCalls = 104;

        readonly Queue<int> recentRequests;


        public RecentCounter()
        {
            recentRequests = new(maxCalls);
        }

        public RecentCounter(int maxCalls)
        {
            this.maxCalls = maxCalls;
            recentRequests = new(maxCalls);
        }


        public int Ping(int t)
        {
            recentRequests.Enqueue(t);
            if (recentRequests.Count == 1)
                return 1;

            while (recentRequests.Peek() < t - timeWindow)
                recentRequests.Dequeue();

            return recentRequests.Count;
        }
    }

    /**
     * Your RecentCounter object will be instantiated and called as such:
     * RecentCounter obj = new RecentCounter();
     * int param_1 = obj.Ping(t);
     */
}


public class NumberRecentCallsUnitTests
{
    private readonly ITestOutputHelper output;
    public NumberRecentCallsUnitTests(ITestOutputHelper output) 
        => this.output = output;


    [Theory]
    [InlineData(new int[] { 1, 100, 3001, 3002 }, new int[] { 1, 2, 3, 3 })]
    public void TestUUT1(int[] tArr, int[] expected)
    {
        RecentCounter uut = new();

        for (int i = 0; i < tArr.Length; i++)
            uut.Ping(tArr[i]).Should().Be(expected[i]);
    }
}


[ShortRunJob, MemoryDiagnoser]
public class NumberRecentCalls1Benchmark
{
    [Params(1, 2, 3, 1_000, 100_000, 10_000_000)]
    public int length;


    [Benchmark(Baseline = true)]
    public bool Benchmark1()
    {
        int[] nums = HelperExt.Random.Bytes(length).Select(c => (int)c).ToArray();
        Array.Sort(nums);

        RecentCounter uut = new(nums.Length);
        for (int i = 0; i < nums.Length; i++)
            uut.Ping(nums[i]);

        return true;
    }
}
