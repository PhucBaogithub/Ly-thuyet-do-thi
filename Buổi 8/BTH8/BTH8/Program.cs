using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BTH8
{
    class Program
    {
        static void Main(string[] args)
        {
            // Xuất text theo Unicode (có dấu tiếng Việt)
            Console.OutputEncoding = Encoding.Unicode;
            // Nhập text theo Unicode (có dấu tiếng Việt)
            Console.InputEncoding = Encoding.Unicode;

            /* Tạo menu */
            Menu menu = new Menu();
            string title = "BTH8: Ứng dụng giải thuật Dijkstra, Floyd"; string[] ms = { "1. Bài 1 : Bài toán đi ra biên",
                "2. Bài 2 : Chọn thành phố để tổ chức họp",
                "3. Bài 3 : Công trình",
                "4. Bài 4 : Bài toán cực tiểu tổng",
                "0. Thoát" };
            int chon;

            do
            {
                Console.Clear();
                menu.ShowMenu(title, ms);
                Console.Write("     Chọn : ");
                chon = int.Parse(Console.ReadLine());
                switch (chon)
                {
                    case 1:
                        {
                            // Bài 1 : Bài toán đi ra biên.
                            int[,] mt;
                            Console.WriteLine();
                            string filePath = @"..\..\TextFile\Matrix1.txt";
                            mt = FileToMatrix(filePath);
                            Console.WriteLine(" Ma trận a :");
                            PrintMatrix(mt);
                            WeightMatrix newMatrix = new WeightMatrix();
                            newMatrix = MatrixToWeightMatrix(mt);
                            Console.WriteLine();
                            //newMatrix.Output();
                            Console.WriteLine(" Nhập vị trí s(x,y) :");
                            Console.Write(" x = ");
                            int x = int.Parse(Console.ReadLine());
                            Console.Write(" y = ");
                            int y = int.Parse(Console.ReadLine());
                            if (x == 0 || y == 0 || x == mt.GetLength(0) - 1 || y == mt.GetLength(1) - 1)
                            {
                                Console.WriteLine(" ({0},{1}) là đỉnh nằm trên biên.", x, y);
                            }
                            else
                            {
                                int s = x * mt.GetLength(1) + y;
                                newMatrix.Dijkstra(s);
                                ToBorderline(newMatrix, s);
                            }
                            Console.ReadKey();
                            break;
                        }
                    case 2:
                        {
                            //Câu 2: 
                            WeightMatrix newMatrix = new WeightMatrix();
                            newMatrix.fileToWeightMatrix(@"../../TextFile/City.txt");
                            newMatrix.Output();
                            Console.WriteLine();
                            int city = SelectCity(newMatrix);
                            Console.WriteLine("Thành phố đã được chọn: {0}", city);
                            Console.ReadKey();
                            break;
                        }
                    case 3:
                        {
                            // Bài 7 : Công trình
                            // Đọc file Congtrinh.txt ra đồ thị ma trận kề g, xuất g
                            string filePath = "../../TextFile/Congtrinh.txt";
                            WeightMatrix g = new WeightMatrix();
                            g.fileToWeightMatrix(filePath); g.Output();
                            Console.WriteLine();
                            // thay đổi giá trị g.A[i,j] = -g.A[i,j] nếu g.A[i,j] khác vô cùng
                            for (int i = 0; i < g.N; i++)
                                for (int j = 0; j < g.N; j++)
                                    if (g.Array[i, j] < int.MaxValue)
                                        g.Array[i, j] = -g.Array[i, j];
                            // Cho g.A[7, 9] = g.A[8, 9] = 0;
                            g.Array[7, 9] = 15; g.Array[8, 9] = 19;
                            g.Array[0, 1] = g.Array[0, 3] = 0;
                            // Chạy thuật toán Floyd
                            g.Floyd();
                            // Xuất kết quả : -g.D[0,9] 
                            Console.WriteLine();
                            Console.WriteLine("  Thời gian sớm nhất hoàn thành công trình : {0}", -g.FloyDist[0, 9]);
                            break;
                        }
                    case 4:
                        {
                            WeightMatrix newMatrix = new WeightMatrix();
                            string filePath = "../../TextFile/BTCTT.txt";
                            newMatrix.fileToWeightMatrix(filePath);
                            newMatrix.Output();
                            Console.WriteLine();
                            int city = SelectCity(newMatrix);
                            Console.WriteLine("Xã đã được chọn: {0}", city);
                            Console.WriteLine();    
                            for (int i = 0; i < newMatrix.N; i++)
                                for (int j = 0; j < newMatrix.N; j++)
                                    if (newMatrix.Array[i, j] < int.MaxValue)
                                        newMatrix.Array[i, j] = -newMatrix.Array[i, j];
                            newMatrix.Array[0, 1] = 0;
                            newMatrix.Floyd();
                            Console.WriteLine();
                            Console.WriteLine("Chi phí thấp nhất để đưa rước học sinh của tất cả các xã : {0}", -newMatrix.FloyDist[0, 6] - 179308);
                            break;
                        }
                }
                Console.ReadKey();
            } while (chon != 0);
        }

        static int[,] FileToMatrix(string fileName)
        {
            int[,] mt;
            // Doc file Matran.txt -> Ma tran a
            StreamReader sr = new StreamReader(fileName);
            string[] s = sr.ReadLine().Split();
            int row = int.Parse(s[0]);
            int col = int.Parse(s[1]);

            mt = new int[row, col];
            for (int i = 0; i < row; i++)
            {
                s = sr.ReadLine().Split();
                for (int j = 0; j < col; j++)
                    mt[i, j] = int.Parse(s[j]);
            }
            sr.Close();
            return mt;
        }
        static void PrintMatrix(int[,] a)
        {
            Console.WriteLine(" 0 1 2 3 4 5");
            Console.WriteLine(" -------------------------");
            for (int i = 0; i < a.GetLength(0); i++)
            {
                Console.Write(" " + i + "|");
                for (int j = 0; j < a.GetLength(1); j++)
                    Console.Write("{0, 4}", a[i, j]);
                Console.WriteLine(" |");
                Console.WriteLine(" |");
            }
            Console.WriteLine(" -------------------------");
        }
        static WeightMatrix MatrixToWeightMatrix(int[,] mt)
        {
            WeightMatrix g = new WeightMatrix();
            int row = mt.GetLength(0);
            int col = mt.GetLength(1);
            g.N = row * col;
            g.Array = new int[g.N, g.N];
            for (int i = 0; i < g.N; i++)
                for (int j = 0; j < g.N; j++)
                    if (i == j) g.Array[i, j] = 0;
                    else g.Array[i, j] = int.MaxValue;
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                {
                    int k = i * col + j;
                    if (i > 0)
                        g.Array[k, k - col] = mt[i - 1, j];

                    if (j > 0)
                        g.Array[k, k - 1] = mt[i, j - 1];

                    if (j < col - 1)
                        g.Array[k, k + 1] = mt[i, j + 1];

                    if (i < row - 1)
                        g.Array[k, k + col] = mt[i + 1, j];
                }
            // trả về g
            return g;
        }
        static void ToBorderline(WeightMatrix g, int s)
        {
            Stack<int> st = new Stack<int>();
            int bien = 0;
            int min = int.MaxValue;
            for (int i = 0; i < g.N; i++)
                if (DegV(i, g) < 4)
                    if (g.Dist[i] < min)
                    {
                        min = g.Dist[i];
                        bien = i;
                    }
            Console.WriteLine();
            while (bien != s)
            {
                st.Push(bien);
                bien = g.Pre[bien];
            }
            Console.Write("Đường ra biên gần nhất, xuất phát từ đỉnh s({0}) : {1}", s, s);
            Console.ForegroundColor = ConsoleColor.Cyan;
            while (st.Count > 0) Console.Write(" -> " + st.Pop());
            Console.WriteLine(" Có độ dài = " + min);
            Console.ForegroundColor = ConsoleColor.White;
        }
        static int DegV(int v, WeightMatrix g)
        {
            int deg = 0;
            for (int i = 0; i < g.N; i++)
                if (g.Array[v, i] < int.MaxValue && g.Array[v, i] != 0) deg++;
            return deg;
        } // dùng trong hàm ToBorderLine
        static int SelectCity(WeightMatrix g)
        {
            // Chạy thuật toán Floyd -> ma trận d
            g.Floyd();
            // Biến cty : lưu thành phố được chọn, khởi tạo = -1
            int cty = -1;

            int min = int.MaxValue;
            for (int i = 0; i < g.N; i++)
            {
                int max = -int.MaxValue;
                for (int j = 0; j < g.N; j++)
                    if (g.FloyDist[i, j] > max) max = g.FloyDist[i, j];
                if (max < min)
                {
                    cty = i;
                    min = max;
                }
            }
            return cty;
        }
    }
}