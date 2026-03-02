using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        // do some problems
        static void Main(string[] args)
        {
            int k = 0;
            int n = 0;
            int i, j; // when last we marked
            int x=0, y=0;
            char[][] arr;
            bool flag = false;

            Console.WriteLine("Welcome to the game. The computer is player A, you are player B");
            Console.Write("Enter N: ");
            n = int.Parse(Console.ReadLine());
            arr = NewArray2D(n);
            i = j = 0;

            while ((n * n / 2 > k))
            {
                Console.WriteLine();

                // NextStep;

                if (arr[i][j] == '-')
                    arr[i][j] = 'A';
                else
                {
                    // check edge points
                    if ((i + j < arr.Length) && (arr[i + j][0] == '-'))
                    {
                        arr[i + j][0] = 'A';
                        x = i;
                        y = j;
                        flag = true;
                        i = i + j;
                        j = 0;
                    }
                    else if ((i + j < arr.Length) && (arr[0][i + j] == '-'))
                    {
                        arr[0][i + j] = 'A';
                        x = i;
                        y = j;
                        flag = true;
                        j = i + j;
                        i = 0;
                    }
                    else if ((i + j >= arr.Length) && (arr[n - 1][i + j - (n - 1)] == '-'))
                    {
                        arr[n - 1][i + j - (n - 1)] = 'A';
                        x = i;
                        y = j;
                        flag = true;
                        j = i + j - (n - 1);
                        i = n - 1;
                    }
                    else if ((i + j >= arr.Length) && (arr[i + j - (n - 1)][n - 1] == '-'))
                    {
                        arr[i + j - (n - 1)][n - 1] = 'A';
                        x = i;
                        y = j;
                        flag = true;
                        i = i + j - (n - 1);
                        j = n - 1;
                    }
                    // check for place
                    else
                    {
                        if (flag == true)
                        {
                            i = x;
                            j = y;
                            flag = false;
                        }
                        while (arr[i][j] != '-')
                        {
                            if (((j == 0) || (j == n - 1)) && (i % 2 == 0))
                            {
                                i++;
                            }
                            else if (((i == 0) || (i == n - 1)) && (j % 2 == 1))
                            {
                                j++;
                            }
                            else if ((i + j) % 2 == 0)
                            {
                                i++;
                                j--;
                            }
                            else if ((i + j) % 2 == 1)
                            {
                                i--;
                                j++;
                            }
                        }
                        arr[i][j] = 'A';
                    }
                }
                //
                Print(arr);

                Console.WriteLine();
                //Console.WriteLine("Enter x y: ");
                //x = int.Parse(Console.ReadLine());
                //y = int.Parse(Console.ReadLine());
                //arr[x - 1][y - 1] = 'B';
                RandomB(arr, i, j);
                System.Threading.Thread.Sleep(700); 
                Print(arr);
                Console.WriteLine();
                System.Threading.Thread.Sleep(700); 
                k++;
            }

            RandomB(arr, i, j); // one last B
            Console.WriteLine();
            Print(arr);
            Console.WriteLine();
            Console.WriteLine("A won!");

        }


        static void RandomB(char[][] array, int p, int q)
        {
            int n = array.Length;
            if (p + 1 == n)
            {
                if ((q+1<n) && (array[p][q + 1] == '-'))
                {
                    array[p][q + 1] = 'B';
                }
                else
                {
                    //Console.WriteLine("BADDDD!!!");
                }
            }
            else if (q + 1 == n)
            {
                if ((p + 1 < n) && (array[p+1][q] == '-'))
                {
                    array[p + 1][q] = 'B';
                }
                else
                {
                    //Console.WriteLine("BADDDD!!!");
                }
            }
            else if ((array[p][q + 1] != '-') && (array[p+1][q] == '-'))
            {
                array[p + 1][q] = 'B';
            }
            else if ((array[p][q + 1] == '-') && (array[p + 1][q] != '-'))
            {
                array[p][q + 1] = 'B';
            }
            else if ((array[p][q + 1] == '-') && (array[p + 1][q] == '-'))
            {
                Random random = new Random();
                if (random.Next(0, 2)==1)
                {
                    array[p + 1][q] = 'B';
                }
                else
                {
                    array[p][q + 1] = 'B';
                }
            }
        }

        static void Print(char[][] array)
        {
            for (int x = 0; x < array.Length; x++)
            {
                for (int y = 0; y < array.Length; y++)
                {
                    Console.Write(array[x][y]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        static char[][] NewArray2D(int n)
        {
            char[][] arr = new char[n][];
            for (int x = 0; x < arr.Length; x++)
            {
                arr[x] = new char[n];
            }
            for (int x = 0; x < arr.Length; x++)
            {
                for (int y = 0; y < arr.Length; y++)
                {
                    arr[x][y] = '-';
                }
            }
            return arr;
        }
    }
}
