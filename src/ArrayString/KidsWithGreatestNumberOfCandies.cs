﻿namespace leetcode.problems.ArrayString;

/*
1431. Kids With the Greatest Number of Candies
Easy

There are n kids with candies. You are given an integer array candies, where each candies[i] represents the number of candies the ith kid has, 
 and an integer extraCandies, denoting the number of extra candies that you have.
Return a boolean array result of length n, where result[i] is true if, after giving the ith kid all the extraCandies, 
 they will have the greatest number of candies among all the kids, or false otherwise.
Note that multiple kids can have the greatest number of candies.

 

Example 1:

Input: candies = [2,3,5,1,3], extraCandies = 3
Output: [true,true,true,false,true] 
Explanation: If you give all extraCandies to:
- Kid 1, they will have 2 + 3 = 5 candies, which is the greatest among the kids.
- Kid 2, they will have 3 + 3 = 6 candies, which is the greatest among the kids.
- Kid 3, they will have 5 + 3 = 8 candies, which is the greatest among the kids.
- Kid 4, they will have 1 + 3 = 4 candies, which is not the greatest among the kids.
- Kid 5, they will have 3 + 3 = 6 candies, which is the greatest among the kids.
Example 2:

Input: candies = [4,2,1,1,2], extraCandies = 1
Output: [true,false,false,false,false] 
Explanation: There is only 1 extra candy.
Kid 1 will always have the greatest number of candies, even if a different kid is given the extra candy.
Example 3:

Input: candies = [12,1,12], extraCandies = 10
Output: [true,false,true]
*/


public class KidsWithGreatestNumberOfCandiesUnitTests
{
    /// <remarks>10m</remarks>
    // Runtime 155 ms Beats 45.34% Memory 44.7 MB Beats 31.16%
    public static IList<bool> KidsWithCandies(int[] candies, int extraCandies)
    {
        int max = candies.Max();
        return candies.Select(c => c + extraCandies >= max).ToList();
    }

    // Runtime 155 ms Beats 45.34% Memory 44.7 MB Beats 31.16%
    public static IList<bool> KidsWithCandies2(int[] candies, int extraCandies)
    {
        if (candies == null)
            return null;

        // find max in O(N)
        int max = candies[0];
        for (int i = 1; i < candies.Length; i++)
            if (candies[i] > max)
                max = candies[i];

        // + O(N)
        bool[] result = new bool[candies.Length];
        for (int i = 0; i < candies.Length; i++)
            result[i] = candies[i] + extraCandies >= max;

        // total O(2*N) -> O(N)
        return result;
    }




    [Theory]
    [InlineData(new int[] { 2, 3, 5, 1, 3 }, 3, new bool[] { true, true, true, false, true })]
    [InlineData(new int[] { 4, 2, 1, 1, 2 }, 1, new bool[] { true, false, false, false, false })]
    [InlineData(new int[] { 12, 1, 12 }, 10, new bool[] { true, false, true })]
    public static void TestUUT(int[] candies, int extraCandies, bool[] expected)
    {
        KidsWithCandies(candies, extraCandies).Should().BeEquivalentTo(expected.ToList());
    }
}