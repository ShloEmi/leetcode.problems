using leetcode.problems.Solution.Helpers;


namespace leetcode.problems.ReverseLinkedList;

/*
https://leetcode.com/problems/reverse-linked-list/?envType=study-plan-v2&id=leetcode-75

206. Reverse Linked List
Easy

Given the head of a singly linked list, reverse the list, and return the reversed list.

 

Example 1:
Input: head = [1,2,3,4,5]
Output: [5,4,3,2,1]

Example 2:
Input: head = [1,2]
Output: [2,1]

Example 3:
Input: head = []
Output: []
 

Constraints:
The number of nodes in the list is the range [0, 5000].
-5000 <= Node.val <= 5000
 

Follow up: A linked list can be reversed either iteratively or recursively. Could you implement both?
*/


public class ReverseLinkedList
{
    //Definition for singly-linked list.

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    // 2m planning
    // t1 = 10m
    // Runtime1 95 ms Beats 16.72% Memory 39.4 MB Beats 6.55%
    public static ListNode ReverseList1(ListNode head)
    {
        if (head == null)
            return head;


        if (head.next == null)
            return head;

        Stack<int> s = new();

        ListNode current = head;
        while (current != null)
        {
            s.Push(current.val);
            current = current.next;
        }

        current = head;
        while (current != null)
        {
            current.val =  s.Pop();
            current = current.next;
        }

        return head;
    }


    // REMARK: Best solution found (not mine!)
    // O(1*n) solution, with O(1) memory 
    public static ListNode ReverseList2(ListNode head)
    {
        ListNode h2 = null;
        while (head is not null)
        {
            var t = head.next;
            head.next = h2;
            h2 = head;
            head = t;
        }

        return h2;
    }
}


public class ReverseLinkedListUnitTests
{
    private readonly ITestOutputHelper output;
    public ReverseLinkedListUnitTests(ITestOutputHelper output) 
        => this.output = output;


    public static IEnumerable<object[]> TestCases =>
        new List<object[]>
        {
            new object[] {new int[] { 1, 2, 3, 4, 5 }, new int[] { 5, 4, 3, 2, 1 } },
            new object[] {new int[] { 1, 2 }, new int[] { 2, 1 } },
            new object[] {new int[] { }, new int[] { } },

            new object[] {new int[] { 42 }, new int[] { 42 } },
            new object[] {new int[] { 1, 1 }, new int[] { 1, 1 } },
        };


    [Theory]
    [MemberData(nameof(TestCases))]
    public void TestUUT1(int[] arr, int[] expected)
    {
        ReverseLinkedList.ListNode list = CreateListFrom(arr);

        // act
        ReverseLinkedList.ListNode rListCurrent = ReverseLinkedList.ReverseList1(list);

        for (int i = 0; i < expected.Length; i++)
        {
            rListCurrent.val.Should().Be(expected[i]);
            rListCurrent = rListCurrent.next;
        }
    }

    [Theory]
    [MemberData(nameof(TestCases))]
    public void TestUUT2(int[] arr, int[] expected)
    {
        ReverseLinkedList.ListNode list = CreateListFrom(arr);

        // act
        ReverseLinkedList.ListNode rListCurrent = ReverseLinkedList.ReverseList2(list);

        for (int i = 0; i < expected.Length; i++)
        {
            rListCurrent.val.Should().Be(expected[i]);
            rListCurrent = rListCurrent.next;
        }
    }


    internal protected static ReverseLinkedList.ListNode CreateListFrom(int[] arr)
    {
        ReverseLinkedList.ListNode list = null;
        ReverseLinkedList.ListNode head = list;

        if (arr.Length > 0)
        {
            list = new(arr[0]);
            head = list;
        }

        for (int i = 1; i < arr.Length; i++)
        {
            head.next = new(arr[i]);
            head = head.next;
        }

        return list;
    }
}


[ShortRunJob, MemoryDiagnoser]
public class ReverseLinkedList1Benchmark
{
    [Params(1, 2, 3, 1_000, 100_000, 10_000_000)]
    public int length;


    [Benchmark(Baseline = true)]
    public ReverseLinkedList.ListNode Benchmark1()
    {
        int[] nums = HelperExt.Random.Bytes(length).Select(c => (int)c).ToArray();

        ReverseLinkedList.ListNode list = ReverseLinkedListUnitTests.CreateListFrom(nums);

        // act
        ReverseLinkedList.ListNode rListCurrent = ReverseLinkedList.ReverseList1(list);

        return rListCurrent;
    }

    [Benchmark()]
    public ReverseLinkedList.ListNode Benchmark2()
    {
        int[] nums = HelperExt.Random.Bytes(length).Select(c => (int)c).ToArray();

        ReverseLinkedList.ListNode list = ReverseLinkedListUnitTests.CreateListFrom(nums);

        // act
        ReverseLinkedList.ListNode rListCurrent = ReverseLinkedList.ReverseList2(list);

        return rListCurrent;
    }
}
