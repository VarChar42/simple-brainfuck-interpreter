using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainfuckInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "in.bf";
            char[] input = new char[0];
            int cellsize = 30000;
            
            if (args.Length > 0)
            {
                inputFile = args[0];
            }
            if (args.Length > 1)
            {
                input = args[1].ToCharArray();
            }
            if(args.Length > 2)
            {
                cellsize = int.Parse(args[2]);
            }

            if (!File.Exists(inputFile) || inputFile.Length < 1)
            {
                Console.WriteLine("File not found");
            }
            char[] brainfuck = File.ReadAllText(inputFile).ToCharArray();

            int pointer = 0;
            int inputPointer = 0;
            int jumpTo = 0;
            
            Stack<int> loopStack = new Stack<int>();
            byte[] cells = new byte[cellsize];
            for (int x = 0; x < brainfuck.Length; x++)
            {
                if (jumpTo > 0 && !(brainfuck[x] == ']' || brainfuck[x] == '[')) continue;
                switch (brainfuck[x])
                {
                    case '>':
                        pointer++;
                        break;
                    case '<':
                        pointer--;
                        break;
                    case '+':
                        cells[pointer]++;
                        break;
                    case '-':
                        cells[pointer]--;
                        break;
                    case '[':
                        if (cells[pointer] == 0 || jumpTo > 0) jumpTo++;
                        else loopStack.Push(x);
                        break;
                    case ']':
                        if (jumpTo > 0) jumpTo--;
                        else if (cells[pointer] != 0) x = loopStack.Peek();
                        else loopStack.Pop();
                        break;
                    case '.':
                        Console.Write((char)cells[pointer]);

                        break;
                    case ',':
                        if (inputPointer < input.Length)
                            cells[pointer] = (byte)input[inputPointer];
                        else
                            cells[pointer] = 0;
                        inputPointer++;
                        break;
                }
            }
        }
    }
}
