using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buoi2_Bai6
{
    internal class AdjMatrix
    {
        private int n;
        private int[,] a;
        public int N { get =>  n; set => n = value; }   
        public int[,] A { get => a; set => a = value; }
        public AdjMatrix()
        {
            
        }
        public AdjMatrix(int n)
        {
            this.n = n;
            a = new int[n, n];
        }
        public void DocFile(string pathInp)
        {
            StreamReader sr = new StreamReader(pathInp);
            n = int.Parse(sr.ReadLine());
            a = new int[n, n];
            for(int i = 0; i < n; i++)
            {
                string[] line = sr.ReadLine().Split(' ');
                for(int j = 0; j < line.Length; j++)
                {
                    a[i, j] = int.Parse(line[j]);
                }
            }
            sr.Close();
        }
    }
}
