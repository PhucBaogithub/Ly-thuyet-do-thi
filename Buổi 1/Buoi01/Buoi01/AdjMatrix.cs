using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Buoi01
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
        // constructor có đối số k là số đỉnh của đồ thị
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
        // Xuất ma trận a lên màn hình
        public void Output()
        {
            Console.WriteLine("Đồ thị ma trận kề - số đỉnh : " + n);
            Console.WriteLine();
            Console.Write(" Đỉnh |");
            for (int i = 0; i < n; i++) Console.Write("    {0}", i);
            Console.WriteLine(); Console.WriteLine("  " + new string('-', 6 * n));
            for (int i = 0; i < n; i++)
            {
                Console.Write("    {0} |", i);
                for (int j = 0; j < n; j++)
                    Console.Write("  {0, 3}", a[i, j]);
                Console.WriteLine();
            }
        }
        // Các phương thức xử lý các thao tác trên đồ thị là bài tập thực hành...

        // Tính bậc của đỉnh i
        public int DegVi(int i)
        {
            int count = 0;
            // Duyệt từng cột j trên dòng i
            for(int j = 0; j < n; j++)
            {
                // Đếm số lượng ô(i, j) = 1
                if (a[i, j] == 1)
                {
                    count++;
                }
            }
            // Trả về kết quả
            return count;
        }
        // Bậc của các đỉnh, tham số là tên file để ghi kết quả
        public void DegVs(string filePath)
        {
            // Sử dụng đối tượng : StreamWriter sw = new StreamWriter(filePath);
            StreamWriter sw = new StreamWriter(filePath);
            sw.WriteLine("Bậc của các đỉnh");
            // Duyệt từng đỉnh của đồ thị
            for (int i = 0; i< n; i++)
            {
                //Tính bậc của đỉnh i : DegVi(i);
                sw.WriteLine($"Đỉnh {i} : " + DegVi(i));
            }
            // Ghi vào file filePath và xuất lên màn hình theo yêu cầu
            // Đóng file
            sw.Close();
        }

        //Bài 2:
        // 1. Tính bậc ra của đỉnh i
        public int DegOut(int i)
        {
            int count = 0;
            // Duyệt từng cột j trên dòng i
            for (int j = 0; j < n; j++)
            {
                // Đếm số lượng ô(i, j) = 1
                if (a[i, j] == 1)
                {
                    count++;
                }
            }
            // Trả về kết quả
            return count;
        }
        // 2. Tính bậc vào của đỉnh j
        public int DegIn(int j)
        {
            int count = 0;
            // Duyệt từng cột j trên dòng i
            for (int i = 0; i < n; i++)
            {
                // Đếm số lượng ô(i, j) = 1
                if (a[i, j] == 1)
                {
                    count++;
                }
            }
            // Trả về kết quả
            return count;
        }
        // 3. Xuất bậc vào bậc ra của các đỉnh theo yêu cầu
        public void DegInOut(string filePath)
        {
            // Sử dụng đối tượng : StreamWriter sw = new StreamWriter(filePath);
            StreamWriter sw = new StreamWriter(filePath);
            sw.WriteLine("Bậc vào - Bậc ra");
            // Duyệt từng đỉnh của đồ thị
            for (int i = 0; i < n; i++)
            {
                //Tính bậc của đỉnh i : DegVi(i);
                sw.WriteLine($"Bậc của đỉnh {i+1} : " + DegIn(i) + " " + "-" + " " + DegOut(i));
            }
            // Ghi vào file filePath và xuất lên màn hình theo yêu cầu
            // Đóng file
            sw.Close();
        }


    }
}
