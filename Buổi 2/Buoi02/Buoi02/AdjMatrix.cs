﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buoi02
{
    class AdjMatrix
    {   
        public int n;   // số đỉmh
        public int[,] a;    // Ma trận kề
        // propeties
        public int N { get => n; set => n = value; }
        public int[,] A { get => a; set => a = value; }
        // constructor không đối số
        public AdjMatrix() { }
        // constructor có đối số
        public AdjMatrix(int k)
        {
            n = k;
            a = new int[n, n];
        }
        // Đọc file AdjMatrix --> ma trận a
        public void FileToAdjMatrix(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            n = int.Parse(sr.ReadLine());
            a = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                string[] s = sr.ReadLine().Split();
                for (int j = 0; j < n; j++)
                    a[i, j] = int.Parse(s[j]);
            }
            sr.Close();
        }
        // Xuất ma trận a
        public void Output()
        {
            Console.WriteLine("Đồ thị ma trận kề - số đỉnh : "+ n);
            Console.WriteLine();
            Console.Write(" Đỉnh |");
            for (int i = 0; i < n; i++)  Console.Write("    {0}", i);
            Console.WriteLine(); Console.WriteLine("  " + new string('-',6*n));
            for (int i = 0; i < n; i++)
            {
                Console.Write("    {0} |", i);
                for (int j = 0; j < n; j++)
                    Console.Write("  {0, 3}", a[i, j]);
                Console.WriteLine();
            }
        }

        // Xác định đỉnh i là đỉnh bồn chứa hay không ?
        public bool IsStorage(int i)
        {
            // Duyệt các cột j của ma trận a, j = 0..n-1
            for(int j = 0; j < n; j++)
            {
                // Nếu tồn tại a[i, j] = 1 thì i không phải đỉnh bồn chứa ( return false)
                if (a[i, j] == 1)
                {
                    return false;
                }
            }
            return true;
            // Kết thúc vòng lặp : return true (i là đỉnh bồn chứa)
        }
    }
}
