using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Buoi0304
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
            string title = "TÌM KIẾM TRÊN ĐỒ THỊ BẰNG THUẬT TOÁN BFS (Breadth First Search)"; string[] ms = { "1. Bài 1 : Liệt kê các đỉnh liên thông với đỉnh x bằng thuật toán BFS",
                "2. Bài 2 : Tìm đường đi từ đỉnh x -> y",
                "3. Bài 3 : Xét tính liên thông. Số TPLT, xuất các TPLT",
                "4. Bài 4 : Đỉnh khớp",
                "5. Bài 5 : Cạnh cầu",
                "6. Bài 6 : Đồ thị phân đôi",
                "7. Bài 7 : Tìm đường đi",
                "8. Bài 8 : Tìm đường đi tiếp theo",
                "9. Bài 9 : Tìm đường đi bằng DFS",
                "10. Bài 10 : Tìm đường đi từ 2 đỉnh bằng DFS",
                "11. Bài 11 : Xét tính liên thông. Số TPLT, xuất các TPLT bằng DFS",
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
                            string fileInput = "../../TextFile/AdjList1.txt";
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
                            g.BFS_XtoY(x, y);
                            break;
                        }
                    case 3:
                        {   // Bài 3 : Xét tính liên thông. Số TPLT, xuất các TPLT
                            // Tạo đường dẫn file filePath = "../../../TextFile/AdjList2.txt";
                            string fileInput = "../../TextFile/Output1.txt";
                            // Khởi tạo đồ thị g : AdjList g = new AdjList();
                            AdjList g = new AdjList();
                            // Đọc file ra đồ thị g; Xuất đồ thị lên màn hình
                            g.FileToAdjList(fileInput); g.Output();
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
                    case 4:
                        {   // Bài 4 : // Tạo đồ thị ban đầu g
                            // Tạo đường dẫn file filePath = "../../TextFile/AdjList1.txt";
                            string fileInput = "../../TextFile/CutBridge.txt";
                            AdjList g = new AdjList();
                            g.FileToAdjList(fileInput); g.Output();
                            // Gọi phương thức liên thông : g.Connected();
                            g.Connected();
                            // Gọi  int gInconnect1 = số TPLT của g
                            int gInconnect1 = g.Inconnect;
                            // Nhập đỉnh x
                            Console.Write("Hãy nhập đỉnh X: ");
                            int x = int.Parse(Console.ReadLine());
                            // Gọi phương thức bỏ các cạnh kề x : g.RemoveEdgeX(x);
                            g.RemoveEdgeX(x);
                            // Gọi phương thức liên thông của g (sau khi đã bỏ các cạnh kề của x)
                            g.Connected();
                            //Gọi int gInconnect2 = số TPLT của g(sau khi đã bỏ các cạnh kề của x)
                            int gInconnect2 = g.Inconnect;
                            // Nếu gInconnect2 > gInconnect1 + 1
                            if(gInconnect2 > gInconnect1 + 1)
                            {
                                //: đỉnh x là đỉnh khớp,
                                Console.WriteLine($"{x} là đỉnh bị khớp");
                            }
                            else
                            {
                                //ngược lại : x không phải là đỉnh khớp
                                Console.WriteLine($"{x} không là đỉnh bị khớp");
                            }
                            break;
                        }
                    case 5:
                        {   // Bài 5 : // Cạnh cầu
                            // Tạo đường dẫn file filePath = "../../TextFile/AdjList1.txt";
                            string fileInput = "../../TextFile/CutBridge.txt";
                            AdjList g = new AdjList();
                            g.FileToAdjList(fileInput); g.Output();
                            // Gọi phương thức liên thông : g.Connected();
                            g.Connected();
                            // Gọi  int gInconnect1 = số TPLT của g
                            int gInconnect1 = g.Inconnect;
                            // Nhập đỉnh x
                            Console.Write("Hãy nhập đỉnh X: ");
                            int x = int.Parse(Console.ReadLine());
                            Console.Write("Hãy nhập đỉnh Y: ");
                            int y = int.Parse(Console.ReadLine());
                            // Gọi phương thức bỏ các cạnh cầu x : g.RemoveEdgeX(x);
                            g.RemoveEdgeXY(x,y);
                            // Gọi phương thức liên thông của g (sau khi đã bỏ các cạnh cầu của x)
                            g.Connected();
                            //Gọi int gInconnect2 = số TPLT của g(sau khi đã bỏ các cạnh cầu của x)
                            int gInconnect2 = g.Inconnect;
                            //Nếu gInconnect2 > gInconnect1 + 1
                            if (gInconnect2 > gInconnect1)
                            {
                                //: đỉnh x là cạnh cầu,
                                Console.WriteLine($"{x}{y} là cạnh cầu");
                            }
                            else
                            {
                                //ngược lại : x không phải là cạnh cầu
                                Console.WriteLine($"{x}{y} không là cạnh cầu");
                            }
                            break;
                        }                   
                    case 6:
                        {   // Kiểm tra đồ thị 2 phía
                            string filePath = "../../TextFile/Bipartite.txt";
                            AdjList g = new AdjList();
                            g.FileToAdjList(filePath); g.Output();
                            if (g.IsBipartite(0))
                                Console.WriteLine("    Đồ thị 2 phía");
                            else
                                Console.WriteLine("    Không phải đồ thị 2 phía");
                            break;
                        }
                    case 7:
                        {
                            //Bài 7 : Tìm đường đi
                            string fileInput = "../../TextFile/Grid.txt";
                            AdjList g = new AdjList();
                            g.GridToAdjList(fileInput);
                            break;
                        }
                    case 8:
                        {
                            //Bài 8 : Tìm đường đi tiếp theo
                            string fileInput = "../../TextFile/Xuatmatranmecung.txt";
                            AdjList g = new AdjList();
                            g.GridToAdjList(fileInput);
                            break;
                        }
                    case 9:
                        {
                            AdjList newList = new AdjList();
                            newList.FileToAdjList("../../TextFile/AdjList1.txt");
                            Console.Write("Nhập đỉnh xuất phát: ");
                            int x = int.Parse(Console.ReadLine());
                            Console.Write("Các đỉnh liên thông với " + x + ": ");
                            newList.TraverseDFS(x);
                            Console.ReadKey();
                            break;
                        }
                    case 10:
                        {
                            AdjList newList = new AdjList();
                            newList.FileToAdjList("../../TextFile/AdjList1.txt");
                            Console.Write("Nhập điểm bắt đầu: ");
                            int x = int.Parse(Console.ReadLine());
                            Console.Write("Nhập điểm kết thúc: ");
                            int y = int.Parse(Console.ReadLine());
                            newList.DFSfromXtoY(x, y);
                            Console.ReadKey();
                            break;
                        }
                    case 11:
                        {
                            AdjList newList = new AdjList();
                            newList.FileToAdjList("../../TextFile/AdjList2.txt");
                            newList.ConnectedDFS();
                            Console.WriteLine("Đồ thị có {0} miền liên thông", newList.Inconnect);
                            newList.OutConnectedDFS();
                            Console.ReadKey();
                            break;
                        }
                }
                Console.ReadKey();
            } while (chon != 0);
        }
    }   
}