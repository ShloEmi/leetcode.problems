using FluentAssertions;

namespace leetcode.problems.CanPlaceFlowers;
/*
605. Can Place Flowers
Easy

You have a long flowerbed in which some of the plots are planted, and some are not. However, flowers cannot be planted in adjacent plots.

Given an integer array flowerbed containing 0's and 1's, where 0 means empty and 1 means not empty, and an integer n, 
 return true if n new flowers can be planted in the flowerbed without violating the no-adjacent-flowers rule and false otherwise.

 

Example 1:
Input: flowerbed = [1,0,0,0,1], n = 1
Output: true

Example 2:
Input: flowerbed = [1,0,0,0,1], n = 2
Output: false
 

Constraints:

1 <= flowerbed.length <= 2 * 104
flowerbed[i] is 0 or 1.
There are no two adjacent flowers in flowerbed.
0 <= n <= flowerbed.length
*/


public class CanPlaceFlowersUnitTests
{
    // Runtime 135 ms Beats 5.8% Memory 40.4 MB Beats 96.31%
    public static bool CanPlaceFlowers3(int[] flowerbed, int n)
    {
        if (flowerbed == null)
            return false;


        if (n == 0)
            return true;
        if (n > (flowerbed.Length + 1) / 2)
        {
            flowerbed = null;
            GC.Collect();
            return false;
        }

        bool result = false;
        int freeBed = 1;
        for (int i = 0; i < flowerbed.Length; i++)
        {
            if (flowerbed[i] != 0)
                freeBed = 0;
            else
            {
                freeBed++;
                if (freeBed == 3)
                {
                    --n;
                    if (n == 0)
                    {
                        result = true;
                        break;
                    }

                    freeBed = 1;
                }
            }
        }

        if (!result && freeBed == 2)
        {
            n--;
            if (n == 0)
                result = true;
        }

        
        flowerbed = null;
        GC.Collect();   // sorry..  :)
        return result;
    }

    // Runtime 134 ms Beats 5.8% Memory 40.6 MB Beats 96.31%
    public static bool CanPlaceFlowers2(int[] flowerbed, int n)
    {
        if (flowerbed == null)
            return false;


        if (n == 0)
            return true;
        if (n > (flowerbed.Length + 1) / 2)
        {
            flowerbed = null;
            GC.Collect();
            return false;
        }


        int freeBed = 1;
        for (int i = 0; i < flowerbed.Length; i++)
        {
            if (flowerbed[i] != 0)
                freeBed = 0;
            else
            {
                freeBed++;
                if (freeBed == 3)
                {
                    --n;
                    if (n == 0)
                    {
                        flowerbed = null;
                        GC.Collect();
                        return true;
                    }

                    freeBed = 1;
                }
            }
        }

        if (freeBed == 2)
        {
            n--;
            if (n == 0)
            {
                flowerbed = null;
                GC.Collect();

                return true;
            }
        }

        flowerbed = null;
        GC.Collect();
        return false;
    }

    // Runtime 112 ms Beats 75.98% Memory 45.8 MB Beats 60.51%
    public static bool CanPlaceFlowers(int[] flowerbed, int n)
    {
        if (flowerbed == null)
            return false;


        if (n == 0)
            return true;
        if (n > (flowerbed.Length + 1) / 2)
            return false;


        int freeBed = 1;
        for (int i = 0; i < flowerbed.Length; i++)
        {
            if (flowerbed[i] != 0)
                freeBed = 0;
            else
            {
                freeBed++;
                if (freeBed == 3)
                {
                    n--;
                    if (n == 0)
                        return true;

                    freeBed = 1;
                }
            }
        }

        if (freeBed == 2)
        {
            n--;
            if (n == 0)
                return true;
        }

        return false;
    }



    [Theory]
    [InlineData(new int[] { 0, 0 }, 2, false)]

    [InlineData(new int[] { 0, 0, 0, 0, 0 }, 3, true)]
    [InlineData(new int[] { 0, 0, 0, 0, 1 }, 3, false)]

    [InlineData(new int[] { 0, 0, 0, 0, 0 }, 2, true)]
    [InlineData(new int[] { 0, 0, 0, 0, 1 }, 2, true)]
    [InlineData(new int[] { 0, 0, 0, 1, 0 }, 2, false)]
    [InlineData(new int[] { 0, 0, 1, 0, 0 }, 2, true)]
    [InlineData(new int[] { 0, 1, 0, 0, 0 }, 2, false)]
    [InlineData(new int[] { 1, 0, 0, 0, 0 }, 2, true)]
    [InlineData(new int[] { 1, 0, 0, 0, 1 }, 2, false)]

    [InlineData(new int[] { 0, 0, 0, 0, 0 }, 1, true)]
    [InlineData(new int[] { 0, 0, 0, 0, 1 }, 1, true)]
    [InlineData(new int[] { 0, 0, 0, 1, 0 }, 1, true)]
    [InlineData(new int[] { 0, 0, 1, 0, 1 }, 1, true)]
    [InlineData(new int[] { 0, 0, 1, 1, 0 }, 1, true)]
    [InlineData(new int[] { 0, 0, 1, 1, 1 }, 1, true)]
    [InlineData(new int[] { 0, 1, 0, 0, 0 }, 1, true)]
    [InlineData(new int[] { 0, 1, 0, 0, 1 }, 1, false)]
    [InlineData(new int[] { 0, 1, 0, 1, 0 }, 1, false)]
    [InlineData(new int[] { 0, 1, 0, 1, 1 }, 1, false)]
    [InlineData(new int[] { 0, 1, 1, 0, 0 }, 1, true)]
    [InlineData(new int[] { 0, 1, 1, 0, 1 }, 1, false)]
    [InlineData(new int[] { 0, 1, 1, 1, 0 }, 1, false)]
    [InlineData(new int[] { 0, 1, 1, 1, 1 }, 1, false)]
    [InlineData(new int[] { 1, 0, 0, 0, 0 }, 1, true)]
    [InlineData(new int[] { 1, 0, 0, 0, 1 }, 1, true)]
    [InlineData(new int[] { 1, 0, 0, 1, 0 }, 1, false)]
    [InlineData(new int[] { 1, 0, 1, 0, 1 }, 1, false)]
    [InlineData(new int[] { 1, 0, 1, 1, 0 }, 1, false)]
    [InlineData(new int[] { 1, 0, 1, 1, 1 }, 1, false)]
    [InlineData(new int[] { 1, 1, 0, 0, 0 }, 1, true)]
    [InlineData(new int[] { 1, 1, 0, 0, 1 }, 1, false)]
    [InlineData(new int[] { 1, 1, 0, 1, 0 }, 1, false)]
    [InlineData(new int[] { 1, 1, 0, 1, 1 }, 1, false)]
    [InlineData(new int[] { 1, 1, 1, 0, 0 }, 1, true)]
    [InlineData(new int[] { 1, 1, 1, 0, 1 }, 1, false)]
    [InlineData(new int[] { 1, 1, 1, 1, 0 }, 1, false)]
    [InlineData(new int[] { 1, 1, 1, 1, 1 }, 1, false)]
    public static void TestUUT(int[] flowerbed, int n, bool expected)
    {
        CanPlaceFlowers(flowerbed, n).Should().Be(expected);
    }
}
