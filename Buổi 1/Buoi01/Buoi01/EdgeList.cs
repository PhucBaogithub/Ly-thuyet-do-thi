using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Buoi01
{
    class EdgeList
    {
        LinkedList<Tuple<int, int>> g;
        int n;      // số đỉnh
        int m;      // số cạnh
        // Propeties
        public int N { get => n; set => n = value; }
        public int M { get => m; set => m = value; }
        public int[,] a;
        public LinkedList<Tuple<int, int>> G { get => g; set => g = value; }
        // constructor
        public EdgeList()
        {
            g = new LinkedList<Tuple<int, int>>();
        }
        // Đọc file EdgeList.txt --> g
        public void FileToEdgeList(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            string[] s = sr.ReadLine().Split();
            n = int.Parse(s[0]);
            m = int.Parse(s[1]);
            a = new int[m, 2];
            for (int i = 0; i < m; i++)
            {
                s = sr.ReadLine().Split();
                for (int j = 0; j < 2; j++)
                    a[i, j] = int.Parse(s[j]);
            }
            sr.Close();
        }
        // Xuất danh sách cạnh lên màn hình
        public void Output()
        {
            Console.WriteLine("Danh sách cạnh của đồ thị với số đỉnh n = " + n);
            Console.WriteLine("Đồ thị ma trận kề - số đỉnh : " + n);
            Console.WriteLine();
            Console.Write(" Đỉnh |");
            for (int i = 1; i < 2; i++) Console.Write("    {0}", i);
            Console.WriteLine(); Console.WriteLine("  " + new string('-', 6 * n));
            for (int i = 0; i < m ; i++)
            {
                Console.Write("    {0} |", i);
                for (int j = 0; j < 2; j++)
                    Console.Write("  {0, 3}", a[i, j]);
                Console.WriteLine();
            }
        }
        // Tính bậc các đỉnh
        public void DegV(string filePath)
        {
            // Khởi tạo biến sw là biến đối tượng StreamWriter
            StreamWriter sw = new StreamWriter(filePath);
            // Duyệt các đỉnh (các phần tử v[i])
            int[] count = new int[n];
            for(int i = 0; i < m; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    count[a[i, j]]++;
                }
            }
            for (int i = 0; i < count.Length; i++)
            {
                sw.WriteLine($"Bậc của đỉnh {i + 1}: {count[i]}");
            }
            // Đóng file
            sw.Close();
        }
    }
}
