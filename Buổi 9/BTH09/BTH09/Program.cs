using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace BTH09
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            // Nhập text theo Unicode (có dấu tiếng Việt)
            Console.InputEncoding = Encoding.Unicode;

            /* Tạo menu */
            Menu menu = new Menu();
            // Tiêu đề menu
            string title = "TÌM KIẾM TRÊN ĐỒ THỊ BẰNG THUẬT TOÁN";
            // Danh sách các mục chọn
            string[] ms = {
                "1. Bài 2 : Tìm cây khung nhỏ nhất bằng thuật toán Kruskal",
                "2. Bài 3 : Tìm cây khung nhỏ nhất bằng thuật toán Prim",
                "0. Thoát" };
            int chon;
            do
            {
                Console.Clear();
                // Xuất menu
                menu.ShowMenu(title, ms);
                Console.Write("     Chọn : ");
                chon = int.Parse(Console.ReadLine());
                switch (chon)
                {

                    case 1:
                        {
                            // Bài 1: Kruskal
                            string filePath = "../../Bai2.txt"; // Đường dẫn tới file chứa đồ thị
                            WeightEdgeList graph = new WeightEdgeList();
                            graph.FileToWeightEdgeList(filePath);
                            graph.Kruskal(); // Gọi phương thức Kruskal
                            break;
                        }
                    case 2:
                        {
                            // Bài 2: Prim
                            string filePath = "../../Bai2.txt"; // Đường dẫn tới file chứa đồ thị
                            WeightEdgeList graph = new WeightEdgeList();
                            graph.FileToWeightEdgeList(filePath);
                            // Chọn đỉnh xuất phát cho thuật toán Prim
                            Console.Write("Nhập đỉnh xuất phát: ");
                            int startVertex = int.Parse(Console.ReadLine());
                            graph.Prim(startVertex); // Gọi phương thức Prim
                            break;
                        }
                }
                Console.ReadKey();
            } while (chon != 0);
        }
    }
}
