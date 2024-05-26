using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Buoi01
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
            string title = "NHẬP, XUẤT & CÁC THAO TÁC CƠ BẢN TRÊN ĐỒ THỊ";   // Tiêu đề menu
            // Danh sách các mục chọn
            string[] ms = { "1. Bài 1: Đồ thị vô hướng ma trận kề, tính bậc của của các đỉnh",
                "2. Bài 2: Đồ thị có hướng ma trận kề, bậc vào, tính bậc ra của các đỉnh",
                "3. Bài 3: Đồ thị danh sách kề, tính bậc các đỉnh",
                "4. Bài 4: Đồ thị danh sách cạnh, tính bậc của các đỉnh",
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
                        {   // Bài 1:
                            // Khai báo đối tượng : AdjMatrix g = new AdjMatrix();
                            AdjMatrix g = new AdjMatrix();
                            // Tạo đường dẫn file input
                            string fileInput = "../../TextFile/AdjMatrix.txt";
                            // Gọi các phương thức đọc file và xuất đồ thị
                            g.FileToAdjMatrix(fileInput);
                            g.Output();
                            // Tạo đường dẫn file output
                            string fileOutput = "../../TextFile/BacCacDinh.txt";
                            g.DegVs(fileOutput);
                            // Gọi phương thức tính bậc của các đỉnh và xuất
                            break;
                        }
                    case 2:
                        {   
                            // Bài 2:
                            // Khai báo đối tượng : AdjMatrix g = new AdjMatrix();
                            AdjMatrix g = new AdjMatrix();
                            // Tạo đường dẫn file input
                            string fileInput = "../../TextFile/DirectedMatrix.txt";
                            // Gọi các phương thức đọc file và xuất đồ thị
                            g.FileToAdjMatrix(fileInput);
                            g.Output();
                            // Tạo đường dẫn file output
                            string fileOutput = "../../TextFile/BacVaoRa.txt";
                            g.DegInOut(fileOutput);
                            // Gọi phương thức tính bậc của các đỉnh và xuất
                            break;
                        }
                    case 3:
                        {   // Bài 3 :
                            AdjList g = new AdjList();
                            string fileInput = "../../TextFile/AdjList.txt";
                            g.FileToAdjList(fileInput);
                            g.Output();
                            string fileOutput = "../../TextFile/Danhsachke.txt";
                            g.DegV(fileOutput);
                            break;
                        }
                    case 4:
                        {   // Bài 4 :
                            EdgeList g = new EdgeList();
                            string fileInput = "../../TextFile/EdgeList.txt";
                            g.FileToEdgeList(fileInput);
                            g.Output();
                            string fileOutput = "../../TextFile/Danhsachke.txt";
                            g.DegV(fileOutput);
                            break;
                        }
                }
                Console.WriteLine(" Nhấn một phím bất kỳ");
                Console.ReadKey();
                Console.Clear();
            } while (chon != 0);
        }
    }
}
