using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT6TH
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Xuất text theo Unicode (có dấu tiếng Việt)
            Console.OutputEncoding = Encoding.Unicode;
            // Nhập text theo Unicode (có dấu tiếng Việt)
            Console.InputEncoding = Encoding.Unicode;

            /* Tạo menu */
            Menu menu = new Menu();
            string title = "ỨNG DỤNG THUẬT TOÁN DFS (tiếp)"; string[] ms = { "1. Bài 1 : Đồ thị phân đôi",
                "2. Bài 2 : Kiểm tra đồ thị có chu trình",
                "3. Bài 3 : Sắp xếp topo",
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
                            //// Câu 1: Kiểm tra đồ thị phân đôi
                            AdjList newList = new AdjList();
                            newList.FileToAdjList("../../TextFile/AdjList1.txt");
                            newList.Xuat();
                            newList.Bipartite();
                            Console.ReadKey();
                            break;
                        }
                    case 2:
                        {
                            //Câu 2: Kiểm tra đồ thị có chu trình hay không
                            AdjList newList = new AdjList();
                            newList.FileToAdjList(@"../../TextFile/AdjList2.txt");
                            newList.Xuat();
                            newList.DFSTestChuTrinh();
                            Console.ReadKey();
                            break;
                        }
                    case 3:
                        {
                            AdjList newList = new AdjList();
                            newList.FileToAdjList(@"../../TextFile/AdjList3.txt");
                            newList.Xuat();
                            Console.WriteLine("Topo sort: ");
                            newList.TopoSort();
                            Console.ReadKey();
                            break;
                        }
                }
                Console.ReadKey();
            } while (chon != 0);
        }
    }
}
