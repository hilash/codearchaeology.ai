using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Program
    {
        class Pos
        {
            public int[] position;
            public Pos CameFrom;

            public Pos(int[] positions, Pos CameFrom)
            {
                this.position = positions;
                this.CameFrom = CameFrom;
            }
        }
        static void Main(string[] args)
        {
            int[] three = Array.ConvertAll(Console.ReadLine().Split(' '), k => Int32.Parse(k));
            int sum = three[0] + three[1] + three[2];
            three = three.OrderBy(k => k).ToArray();
            bool[,] matrix = new bool[sum,sum];
            Queue<Pos> q = new Queue<Pos>();
            q.Enqueue(new Pos(three, null));

            while (q.Count != 0)
            {
                if (doSomeWork(matrix, q))
                    break;
            }

            Console.ReadLine();
        }
        static bool doSomeWork(bool[,] matrix, Queue<Pos> q)
        {
            Pos current = q.Dequeue();
            int x = current.position[0];
            int y = current.position[1];
            int z = current.position[2];

            if (x == 0)
            {
                WriteTrack(current);
                return true;
            }


            if (matrix[x, y])
                return false;

            matrix[x, y] = true;
            q.Enqueue(new Pos((new int[3] { 2 * x, y - x, z }).OrderBy(k => k).ToArray(), current));
            q.Enqueue(new Pos((new int[3] { 2 * x, y, z - x }).OrderBy(k => k).ToArray(), current));
            q.Enqueue(new Pos((new int[3] { x, 2 * y, z - y }).OrderBy(k => k).ToArray(), current));

            return false;
        }
        static void WriteTrack(Pos s)
        {
            while (s != null)
            {
                Console.WriteLine("{0} {1} {2}", s.position[0], s.position[1], s.position[2]);
                s = s.CameFrom;
            }
        }
    }
}
