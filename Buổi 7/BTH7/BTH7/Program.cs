using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTH7
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
            string title = "BTH7: Tìm đường đi ngắn nhất - Giải thuật Dijkstra, Floyd"; string[] ms = { "1. Bài 1 : Tìm đường đi ngắn nhất từ đỉnh x đến đỉnh y theo thuật toán Dijkstra",
                "2. Bài 2 : Đường đi ngắn nhất từ đỉnh x đến đỉnh z qua đỉnh trung gian y",
                "3. Bài 3 : Đường đi ngắn nhất giữa các cặp đỉnh – giải thuật Floyd",
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
                            //Câu 1: 
                            WeightMatrix newMatrix = new WeightMatrix();
                            newMatrix.fileToWeightMatrix(@"../../TextFile/MatrixBai1.txt");
                            newMatrix.Output();
                            Console.WriteLine();
                            Console.Write("Nhập đỉnh bắt đầu: ");
                            int x = int.Parse(Console.ReadLine());
                            Console.Write("Nhập đỉnh kết thúc: ");
                            int y = int.Parse(Console.ReadLine());
                            newMatrix.MinRouteXY(x, y);
                            Console.ReadKey();
                            break;
                        }
                    case 2:
                        {
                            //Câu 2: 
                            WeightMatrix newMatrix = new WeightMatrix();
                            newMatrix.fileToWeightMatrix(@"../../TextFile/MatrixBai1.txt");
                            newMatrix.Output();
                            Console.WriteLine();
                            Console.Write("Nhập đỉnh bắt đầu: ");
                            int x = int.Parse(Console.ReadLine());
                            Console.Write("Nhập đỉnh trung gian: ");
                            int y = int.Parse(Console.ReadLine());
                            Console.Write("Nhập đỉnh kết thúc: ");
                            int z = int.Parse(Console.ReadLine());
                            newMatrix.MinRouteXYZ(x, y, z);
                            Console.ReadKey();
                            break;
                        }
                    case 3:
                        {
                            //Câu 3:
                            WeightMatrix newMatrix = new WeightMatrix();
                            newMatrix.fileToWeightMatrix(@"../../TextFile/MatrixBai3.txt");
                            newMatrix.Output();
                            Console.WriteLine();
                            newMatrix.Floyd();
                            Console.WriteLine("DP matrix: ");
                            newMatrix.OutDToP();
                            Console.WriteLine();
                            Console.WriteLine("In chi tiết: ");
                            for (int i = 0; i < newMatrix.N - 1; i++)
                            {
                                for (int j = 0; j < newMatrix.N; j++)
                                {
                                    if (i != j)
                                    {
                                        newMatrix.Floyd_RouteXY(i, j);
                                    }
                                }
                            }
                            break;
                        }
                }
                Console.ReadKey();
            } while (chon != 0);
        }
    }
}