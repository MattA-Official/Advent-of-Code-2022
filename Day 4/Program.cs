using System;
using System.Collections.Generic;

/*
--- Day 4: Camp Cleanup ---
Space needs to be cleared before the last supplies can be unloaded from the ships, and so several Elves have been assigned the job of cleaning up sections of the camp. Every section has a unique ID number, and each Elf is assigned a range of section IDs.

However, as some of the Elves compare their section assignments with each other, they've noticed that many of the assignments overlap. To try to quickly find overlaps and reduce duplicated effort, the Elves pair up and make a big list of the section assignments for each pair (your puzzle input).

For example, consider the following list of section assignment pairs:

2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8
For the first few pairs, this list means:

Within the first pair of Elves, the first Elf was assigned sections 2-4 (sections 2, 3, and 4), while the second Elf was assigned sections 6-8 (sections 6, 7, 8).
The Elves in the second pair were each assigned two sections.
The Elves in the third pair were each assigned three sections: one got sections 5, 6, and 7, while the other also got 7, plus 8 and 9.
This example list uses single-digit section IDs to make it easier to draw; your actual list might contain larger numbers. Visually, these pairs of section assignments look like this:

.234.....  2-4
.....678.  6-8

.23......  2-3
...45....  4-5

....567..  5-7
......789  7-9

.2345678.  2-8
..34567..  3-7

.....6...  6-6
...456...  4-6

.23456...  2-6
...45678.  4-8
Some of the pairs have noticed that one of their assignments fully contains the other. For example, 2-8 fully contains 3-7, and 6-6 is fully contained by 4-6. In pairs where one assignment fully contains the other, one Elf in the pair would be exclusively cleaning sections their partner will already be cleaning, so these seem like the most in need of reconsideration. In this example, there are 2 such pairs.

In how many assignment pairs does one range fully contain the other?

--- Part Two ---
It seems like there is still quite a bit of duplicate work planned. Instead, the Elves would like to know the number of pairs that overlap at all.

In the above example, the first two pairs (2-4,6-8 and 2-3,4-5) don't overlap, while the remaining four pairs (5-7,7-9, 2-8,3-7, 6-6,4-6, and 2-6,4-8) do overlap:

5-7,7-9 overlaps in a single section, 7.
2-8,3-7 overlaps all of the sections 3 through 7.
6-6,4-6 overlaps in a single section, 6.
2-6,4-8 overlaps in sections 4, 5, and 6.
So, in this example, the number of overlapping assignment pairs is 4.

In how many assignment pairs do the ranges overlap?
*/

namespace Day_4
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read the input file
            string[] input = System.IO.File.ReadAllLines(@"input.txt");

            // Create a list of all the ranges
            List<int[]> ranges = new List<int[]>();

            // Create a counter for the number of ranges that fully contain another range
            int fullyContainedRanges = 0;

            // Create a counter for the number of ranges that overlap
            int overlappingRanges = 0;

            // Loop through each line of the input
            foreach (string line in input)
            {
                // Split the line into two ranges
                string[] rangeStrings = line.Split(',');

                // Get the first range
                string[] range1String = rangeStrings[0].Split('-');

                // Create an array to hold the first range
                int[] range1 = new int[int.Parse(range1String[1]) - int.Parse(range1String[0]) + 1];

                // Loop through the first range
                for (int i = 0; i < range1.Length; i++)
                {
                    // Add the current number to the range
                    range1[i] = int.Parse(range1String[0]) + i;
                }

                // Get the second range
                string[] range2String = rangeStrings[1].Split('-');

                // Create an array to hold the second range
                int[] range2 = new int[int.Parse(range2String[1]) - int.Parse(range2String[0]) + 1];

                // Loop through the second range
                for (int i = 0; i < range2.Length; i++)
                {
                    // Add the current number to the range
                    range2[i] = int.Parse(range2String[0]) + i;
                }

                // Add the ranges to the list
                ranges.Add(range1);
                ranges.Add(range2);

                // Check if the first range fully contains the second range or vice versa
                if (
                    range1[0] <= range2[0] && range1[range1.Length - 1] >= range2[range2.Length - 1]
                    || range2[0] <= range1[0]
                        && range2[range2.Length - 1] >= range1[range1.Length - 1]
                )
                {
                    // Increment the fully contained ranges counter
                    fullyContainedRanges++;
                }

                // Check if the ranges overlap
                if (
                    range1[0] <= range2[range2.Length - 1] && range1[range1.Length - 1] >= range2[0]
                )
                {
                    // Increment the overlapping ranges counter
                    overlappingRanges++;
                }
            }

            // Write out the number of contained and overlapping ranges
            Console.WriteLine("Part 1: " + fullyContainedRanges);
            Console.WriteLine("Part 2: " + overlappingRanges);

            Console.ReadLine();
        }
    }
}
