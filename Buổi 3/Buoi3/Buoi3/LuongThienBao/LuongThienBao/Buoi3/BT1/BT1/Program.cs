using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT1
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
            string title = "TÌM KIẾM TRÊN ĐỒ THỊ BẰNG THUẬT TOÁN BFS (Breadth First Search)"; string[] ms = { "1. Bài 1 : Liệt kê các đỉnh liên thông với đỉnh x bằng thuật toán BFS",
                "2. Bài 2 : Tìm đường đi từ đỉnh x -> y",
                "3. Bài 3 : Xét tính liên thông. Số TPLT, xuất các TPLT",
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
                        {   // Bài 1 : duyệt đồ thị từ đỉnh x theo BFS
                            // Tạo đường dẫn file filePath = "../../TextFile/AdjList1.txt";
                            string fileInput = "../../TextFile/AdjList2Input.txt";
                            // Khởi tạo đồ thị g : AdjList g = new AdjList();
                            AdjList g = new AdjList();
                            // Đọc file ra đồ thị g; Xuất đồ thị lên màn hình
                            g.FileToAdjList(fileInput); g.Output();
                            Console.Write("  Nhập đỉnh xuất phát x : ");
                            int x = int.Parse(Console.ReadLine());
                            Console.Write("  Các đỉnh liên thông với {0} : ", x);
                            // Gọi phương thức BFS(x);
                            g.BFS(x);
                            break;
                        }
                    case 2:
                        {   // Bài 2 : Tìm đường đi từ đỉnh x -> y
                            // Tạo đường dẫn file filePath = "../../../TextFile/AdjList2.txt";
                            string fileInput = "../../TextFile/AdjList2.txt";
                            // Khởi tạo đồ thị g : AdjList g = new AdjList();
                            AdjList g = new AdjList();
                            // Đọc file ra đồ thị g; Xuất đồ thị lên màn hình
                            g.FileToAdjList(fileInput); g.Output();
                            Console.Write("  Nhập đỉnh xuất phát x : ");
                            int x = int.Parse(Console.ReadLine());
                            Console.Write("        Nhập đỉnh đến y : ");
                            int y = int.Parse(Console.ReadLine());
                            // Gọi phương thức BFS_XtoY(x, y);
                            g.BFS_X_to_Y(x, y);
                            break;
                        }
                    case 3:
                        {   // Bài 3 : Xét tính liên thông. Số TPLT, xuất các TPLT
                            // Tạo đường dẫn file filePath = "../../../TextFile/AdjList2.txt";
                            // Khởi tạo đồ thị g : AdjList g = new AdjList();
                            // Đọc file ra đồ thị g; Xuất đồ thị lên màn hình
                            g.Connected();
                            if (g.Inconnect == 1)
                                Console.WriteLine("  Đồ thị liên thông");
                            else
                            {
                                Console.WriteLine("  Đồ thị có {0} thành phần liên thông", g.Inconnect);
                                g.OutConnected();    // Xuất các TPLT
                            }
                            break;
                        }
                }
                Console.ReadKey();
            } while (chon != 0);
        }
    }
}
