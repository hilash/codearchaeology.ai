using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static int N;
        static bool[,] mat;
        static int[] a;
        static void Main(string[] args)
        {
            Console.Write("Enter N: ");
            N = Int32.Parse(Console.ReadLine());
            mat = new bool[N, N];

            a = new int[2];
            int[] b;

            for (int i = N * N / 2; i > 0; i--)
            {
                Console.WriteLine("Each of these good: ");
                FindPlace(true);

                a = Array.ConvertAll(Console.ReadLine().Split(' '), s => Int32.Parse(s)); //delete it for first
                b = Array.ConvertAll(Console.ReadLine().Split(' '), s => Int32.Parse(s));
                if (b[0] >= 0 && b[0] < N && b[1] >= 0 && b[1] < N && !mat[b[0], b[1]] && 
                    (
                    (a[0] == b[0] && (a[1] == b[1] - 1 || a[1] == b[1] + 1))
                    || (a[1] == b[1] && (a[0] == b[0] - 1 || a[0] == b[0] + 1))
                    ))
                {
                    mat[b[0], b[1]] = true;
                    mat[a[0], a[1]] = true;
                }

                else
                {
                    Console.WriteLine("Cheater");
                    i++;
                }
            }
            Console.WriteLine("I Won!");
            Console.Read();
        }
        static bool FindPlace(bool top)
        {
            bool all;
            bool one;
            bool noPlace = true;

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (!mat[i, j])
                    {
                        noPlace = false;
                        mat[i, j] = true;
                        all = true;
                        one = false;
                        if (i < N-1 && !mat[i + 1, j])
                        {
                            one = true;
                            mat[i + 1, j] = true;
                            all &= FindPlace(false);
                            mat[i + 1, j] = false;
                        }
                        if (i > 0 && !mat[i - 1, j])
                        {
                            one = true;
                            mat[i - 1, j] = true;
                            all &= FindPlace(false);
                            mat[i - 1, j] = false;
                        }
                        if (j < N - 1 && !mat[i, j + 1])
                        {
                            one = true;
                            mat[i, j + 1] = true;
                            all &= FindPlace(false);
                            mat[i, j + 1] = false;
                        }
                        if (j > 0 && !mat[i, j-1])
                        {
                            one = true;
                            mat[i, j - 1] = true;
                            all &= FindPlace(false);
                            mat[i, j - 1] = false;
                        }
                        mat[i, j] = false;
                        if (one && all)
                        {
                            if (top)
                            {
                                a[0] = i;
                                a[1] = j;
                                Console.WriteLine("{0}, {1}", i, j);
                            }
                            else return true;  //delete else for first.
                        }
                        else if (!one) return false;
                    }
                }
            }
            if (noPlace) return true;
            if (top) Console.WriteLine("No Place");
            return false;
        }
    }
}
