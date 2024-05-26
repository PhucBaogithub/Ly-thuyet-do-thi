using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Buoi4
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
            string title = "Ứng dụng thuật toán Breadth First Search – BFS"; string[] ms = {
                "1. Bài 1 : Kiểm tra xem đỉnh x có phải là đỉnh khớp của đồ thị G không.",
                "2. Bài 2 : Kiểm tra xem cạnh (x, y) có phải là cầu của đồ thị G không.",
                "3. Bài 3 : Đồ thị phân đôi",
             
                "0. Thoát" };
            int chon;
            do
            {
                // Xuất menu
                menu.ShowMenu(title, ms);
                Console.Write("     Chọn : ");
                chon = int.Parse(Console.ReadLine());
                switch (chon)
                {
                    case 1:
                        {
                            // Tạo đồ thị ban đầu g
                            string filePath = "../../../TextFile/CutBridge.txt";
                            AdjList g = new AdjList();
                            g.FileToAdjList(filePath); g.Output();
                            // Gọi phương thức liên thông : g.Connected();
                            g.Connected();
                            // Gọi  int gInconnect1 = số TPLT của g
                            int gInconnect1 = g.Inconnect;
                            // Nhập đỉnh x
                            Console.Write("Nhập đỉnh x: ");
                            int x = int.Parse(Console.ReadLine());
                            // Gọi phương thức bỏ các cạnh kề x : g.RemoveEdgeX(x);
                            g.RemoveEdgeX(x);
                            // Gọi phương thức liên thông của g (sau khi đã bỏ các cạnh kề của x)
                            g.Connected();
                            // Gọi int gInconnect2 = số TPLT của g(sau khi đã bỏ các cạnh kề của x)
                            int gInconnect2 = g.Inconnect;
                            //// Nếu gInconnect2 > gInconnect1 + 1  : đỉnh x là đỉnh khớp, 
                            //   ngược lại : x không phải là đỉnh khớp
                            if (gInconnect2 > gInconnect1 + 1)
                            {
                                Console.WriteLine($"Đỉnh {x} là đỉnh khớp.");
                            }
                            else
                            {
                                Console.WriteLine($"Đỉnh {x} không phải là đỉnh khớp.");
                            }
                            break;
                        }
                    case 2:
                        {
                            // Tạo đồ thị ban đầu g
                            string filePath = "../../../TextFile/CutBridge.txt";
                            AdjList g = new AdjList();
                            g.FileToAdjList(filePath); g.Output();
                            // Gọi phương thức liên thông : g.Connected();
                            g.Connected();
                            // Gọi  int gInconnect1 = số TPLT của g
                            int gInconnect1 = g.Inconnect;
                            // Nhập đỉnh x
                            Console.Write("Nhập cạnh x: ");
                            int x = int.Parse(Console.ReadLine());
                            Console.Write("Nhập cạnh y: ");
                            int y = int.Parse(Console.ReadLine());
                            // Gọi phương thức bỏ các cạnh kề x : g.RemoveEdgeX(x);
                            g.RemoveEdgeXY(x, y);
                            // Gọi phương thức liên thông của g (sau khi đã bỏ các cạnh kề của x)
                            g.Connected();
                            // Gọi int gInconnect2 = số TPLT của g(sau khi đã bỏ các cạnh kề của x)
                            int gInconnect2 = g.Inconnect;
                            //// Nếu gInconnect2 > gInconnect1 + 1  : đỉnh x là đỉnh khớp, 
                            //   ngược lại : x không phải là đỉnh khớp
                            if (gInconnect2 > gInconnect1 + 1)
                            {
                                Console.WriteLine($"Cạnh ({x},{y}) là cạnh cầu.");
                            }
                            else
                            {
                                Console.WriteLine($"Cạnh ({x},{y}) không phải là cạnh cầu.");
                            }
                            break;
                        }
                    case 3:
                        {
                            // Kiểm tra đồ thị 2 phía
                            string filePath = "../../../TextFile/Bipartite.txt";
                            AdjList g = new AdjList();
                            g.FileToAdjList(filePath); g.Output();
                            if (g.IsBipartite(0))
                                Console.WriteLine("    Đồ thị 2 phía");
                            else
                                Console.WriteLine("    Không phải đồ thị 2 phía");
                            break;

                            break;
                        }
                    case 4:
                        {
                            // Tạo đồ thị ban đầu g
                            string filePath = "../../../TextFile/Grid.txt";
                            AdjList g = new AdjList();
                            g.GridToAdjList(filePath); g.Output();
                            // Gọi phương thức liên thông : g.Connected();
                            g.Connected();
                            break;
                        }
                }
                Console.ReadKey();
            } while (chon != 0);
        }
    }
}

