using System;
using System.Collections.Generic;

/*
--- Day 5: Supply Stacks ---
The expedition can depart as soon as the final supplies have been unloaded from the ships. Supplies are stored in stacks of marked crates, but because the needed supplies are buried under many other crates, the crates need to be rearranged.

The ship has a giant cargo crane capable of moving crates between stacks. To ensure none of the crates get crushed or fall over, the crane operator will rearrange them in a series of carefully-planned steps. After the crates are rearranged, the desired crates will be at the top of each stack.

The Elves don't want to interrupt the crane operator during this delicate procedure, but they forgot to ask her which crate will end up where, and they want to be ready to unload them as soon as possible so they can embark.

They do, however, have a drawing of the starting stacks of crates and the rearrangement procedure (your puzzle input). For example:

    [D]
[N] [C]
[Z] [M] [P]
 1   2   3

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2
In this example, there are three stacks of crates. Stack 1 contains two crates: crate Z is on the bottom, and crate N is on top. Stack 2 contains three crates; from bottom to top, they are crates M, C, and D. Finally, stack 3 contains a single crate, P.

Then, the rearrangement procedure is given. In each step of the procedure, a quantity of crates is moved from one stack to a different stack. In the first step of the above rearrangement procedure, one crate is moved from stack 2 to stack 1, resulting in this configuration:

[D]
[N] [C]
[Z] [M] [P]
 1   2   3
In the second step, three crates are moved from stack 1 to stack 3. Crates are moved one at a time, so the first crate to be moved (D) ends up below the second and third crates:

        [Z]
        [N]
    [C] [D]
    [M] [P]
 1   2   3
Then, both crates are moved from stack 2 to stack 1. Again, because crates are moved one at a time, crate C ends up below crate M:

        [Z]
        [N]
[M]     [D]
[C]     [P]
 1   2   3
Finally, one crate is moved from stack 1 to stack 2:

        [Z]
        [N]
        [D]
[C] [M] [P]
 1   2   3
The Elves just need to know which crate will end up on top of each stack; in this example, the top crates are C in stack 1, M in stack 2, and Z in stack 3, so you should combine these together and give the Elves the message CMZ.

After the rearrangement procedure completes, what crate ends up on top of each stack?
*/

namespace Day_5
{
    // Move struct
    public struct Move
    {
        public int From;
        public int To;
        public int Quantity;
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Get the input from the file
            string[] input = System.IO.File.ReadAllLines(@"input.txt");

            // Create a list of all the stacks
            List<Stack<Char>> stacks = new List<Stack<Char>>();

            // Create 9 stacks
            for (int i = 0; i < 9; i++)
            {
                stacks.Add(new Stack<char>());
            }

            // Create a list of all the moves
            List<Move> moves = new List<Move>();

            // Loop through the input to get the moves
            for (int i = 0; i < input.Length; i++)
            {
                string line = input[i];

                // If the line starts with "move", then it's a move
                if (line.StartsWith("move"))
                {
                    // Split the line into parts
                    string[] parts = line.Split(' ');

                    // Create a new move
                    Move move = new Move();

                    move.Quantity = int.Parse(parts[1]);
                    move.From = int.Parse(parts[3]);
                    move.To = int.Parse(parts[5]);

                    // Add the move to the list
                    moves.Add(move);
                }
            }

            // Loop through the input backwards to get the stacks
            for (int i = input.Length - 1; i >= 0; i--)
            {
                string line = input[i];

                if (line != "" && !line.StartsWith("move"))
                {
                    // Add every third character starting with the second one to the appropriate stack
                    for (int j = 1; j < line.Length; j += 4)
                    {
                        // if the character is a space or a number, then skip it
                        if (line[j] == ' ' || (line[j] >= '0' && line[j] <= '9'))
                        {
                            continue;
                        }

                        stacks[((j - 1) / 4)].Push(line[j]);
                    }
                }
            }

            // Loop through the moves
            for (int i = 0; i < moves.Count; i++)
            {
                // Get the move
                Move move = moves[i];

                // Loop through the crates to move
                for (int j = 0; j < move.Quantity; j++)
                {
                    // Add the crate to the stack
                    stacks[move.To - 1].Push(stacks[move.From - 1].Pop());
                }
            }

            // Create a string to hold the answer
            string answer = "";

            // Loop through the stacks
            for (int i = 0; i < stacks.Count; i++)
            {
                // If the stack is empty, then skip it
                if (stacks[i].Count == 0)
                {
                    continue;
                }

                // Add the top crate to the answer
                answer += stacks[i].Peek();
            }

            // Print the answer
            Console.WriteLine("Part 1: " + answer);

            Console.ReadLine();
        }
    }
}
