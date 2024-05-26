using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Buoi0708
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
            string title = "                          ĐƯỜNG ĐI NGẮN NHẤT                        ";   // Tiêu đề menu
            // Danh sách các mục chọn
            string[] ms = { "1. Bài 1: Tìm đường đi ngắn nhất từ một đỉnh đến các đỉnh còn lại - Dijkstra",
                "2. Bài 2: Tìm đường đi từ đỉnh x đến y",
                "3. Bài 3: Tìm đường đi ngắn nhất qua đỉnh trung gian",
                "4. Bài 4: Đường đi ngắn nhất giữa các cặp đỉnh - Thuật toán Floyd",
                "5. Bài 5: Bài toán đi ra biên",
                "6. Bài 6: Chọn thành phố để họp",
                "7. Bài 7 : Công trình.",
                "8. Bài 8: Chọn xã",
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
                            // Tìm đường ngắn nhất từ một đỉnh đến các đỉnh còn lại
                            string filePath = "../../../TextFile/Graph.txt";
                            WeightMatrix g = new WeightMatrix();
                            g.FileToWeightMatrix(filePath); g.Output();
                            Console.WriteLine();
                            Console.Write(" Nhập đỉnh xuất phát : ");
                            int s = int.Parse(Console.ReadLine());
                            Console.WriteLine();
                            g.Dijkstra(s);
                            g.PrintDijkstra(s);
                            break;
                        }
                    case 2:
                        {
                            // Bài 1 : Tìm đường ngắn nhất từ x đến y
                            string filePath = "../../../TextFile/MatrixBai1.txt";
                            WeightMatrix g = new WeightMatrix();
                            g.FileToWeightMatrix(filePath); g.Output();
                            Console.WriteLine();
                            Console.Write(" Nhập đỉnh xuất phát x = ");
                            int x = int.Parse(Console.ReadLine());
                            Console.Write(" Nhập đỉnh đích y = ");
                            int y = int.Parse(Console.ReadLine());
                            g.MinRouteXY(x, y);
                            break;
                        }
                    case 3:
                        {
                            // Bài 2: Đường đi ngắn nhất qua đỉnh trung gian
                            string filePath = "../../../TextFile/MatrixBai1.txt";
                            WeightMatrix g = new WeightMatrix();
                            g.FileToWeightMatrix(filePath); g.Output();
                            Console.WriteLine();
                            Console.Write(" Nhập đỉnh xuất phát x = ");
                            int x = int.Parse(Console.ReadLine());
                            Console.Write(" Nhập đỉnh trung gian y = ");
                            int y = int.Parse(Console.ReadLine());
                            Console.Write(" Nhập đỉnh đích z = ");
                            int z = int.Parse(Console.ReadLine());
                            g.MinRouteXYZ(x, y, z);
                            break;
                        }
                    case 4:
                        {
                            // Bài 3: Đường đi ngắn nhất giữa các cặp đỉnh - thuật toán Floyd
                            string filePath = "../../../TextFile/MatrixFloyd2.txt";
                            WeightMatrix g = new WeightMatrix();
                            g.FileToWeightMatrix(filePath); g.Output();
                            Console.WriteLine();
                            g.Floyd();
                            Console.WriteLine("  Ma trận d, p ");
                            g.Outdp();
                            Console.WriteLine();
                            Console.WriteLine(" Chi tiết đường đi ngắn nhất giữa các cặp đỉnh");
                            for (int i = 0; i < g.N - 1; i++)
                                for (int j = 0; j < g.N; j++)
                                    if (i != j) g.Floyd_RouteXY(i, j);
                            Console.WriteLine();
                            break;
                        }
                    case 5:
                        {
                            // Bài 5 : Bài toán đi ra biên.
                            // Có các hàm trình bày trong Program
                            int[,] mt;
                            Console.WriteLine();
                            // Đọc file --> ma trận a[,]
                            string fileName = "../../../TextFile/Matran.txt";
                            mt = FileToMatrix(fileName);
                            // Xuất ma trận mt
                            Console.WriteLine("  Ma trận a :"); PrintMatrix(mt);

                            // Đọc ma trận mt[,] --> đồ thị ma trận kề g
                            WeightMatrix g = new WeightMatrix();
                            g = MatrixToWeightMatrix(mt);
                            Console.WriteLine();
                            //g.Output();

                            // Nhập tọa độ ô xuất phát s(x,y)
                            Console.WriteLine("  Nhập tọa độ ô xuất phát s(x,y) :");
                            Console.Write("  x = ");
                            int x = int.Parse(Console.ReadLine());
                            Console.Write("  y = ");
                            int y = int.Parse(Console.ReadLine());

                            if (x == 0 || y == 0 || x == mt.GetLength(0) - 1 || y == mt.GetLength(1) - 1)
                                Console.WriteLine("  ({0},{1}) là đỉnh nằm trên biên.", x, y);
                            else
                            {
                                // Chuyển đổi tọa độ (x,y) --> đỉnh s
                                int s = x * mt.GetLength(1) + y;
                                // Gọi Dijkstra từ đỉnh s
                                g.Dijkstra(s);
                                // Tìm đường đi ra biên
                                ToBorderline(g, s);
                            }
                            break;
                        }
                    case 6:
                        {
                            // Bài 6 : Chọn thành phố để họp
                            string filePath = "../../../TextFile/SelectCity.txt";
                            WeightMatrix g = new WeightMatrix();
                            g.FileToWeightMatrix(filePath); g.Output();
                            Console.WriteLine();
                            int selectedCity = SelectCity(g);
                            Console.WriteLine("  Thành phố được chọn : {0}", selectedCity);
                            break;
                        }
                    case 7:
                        {
                            // Bài 7 : Công trình
                            // Đọc file Congtrinh.txt ra đồ thị ma trận kề g, xuất g
                            string filePath = "../../../TextFile/CongTrinh.txt";
                            WeightMatrix g = new WeightMatrix();
                            g.FileToWeightMatrix(filePath); g.Output();
                            Console.WriteLine();
                            // thay đổi giá trị g.A[i,j] = -g.A[i,j] nếu g.A[i,j] khác vô cùng
                            for (int i = 0; i < g.N; i++)
                                for (int j = 0; j < g.N; j++)
                                    if (g.A[i, j] < int.MaxValue)
                                        g.A[i, j] = -g.A[i, j];
                            // Cho g.A[7, 9] = g.A[8, 9] = 0;
                            g.A[7, 9] = 15; g.A[8, 9] = 19;
                            g.A[0, 1] = g.A[0, 3] = 0;
                            // Chạy thuật toán Floyd
                            g.Floyd();
                            // Xuất kết quả : g.D[0,9]
                            Console.WriteLine();
                            Console.WriteLine("  Thời gian sớm nhất hoàn thành công trình : {0} ngày", -g.D[0, 9]);
                            break;
                        }
                    case 8:
                        {
                            // Bài 8 : Chọn Xãp
                            string filePath = "../../../TextFile/Xa.txt";
                            WeightMatrix g = new WeightMatrix();
                            g.FileToWeightMatrix(filePath); g.Output();
                            Console.WriteLine();
                            
                            int xa;
                            xa = chonXa(g);
                            Console.WriteLine("   Xã được xây trường là : {0}", (char)(xa + 65));
                            int minCost = FindMinCost(g, xa);
                            Console.WriteLine("   Chi phí thấp nhất để đưa rước học sinh của tất cả các xã: {0}", minCost);
                            duongDi(g, xa);
                            break;
                        }

                }
                Console.ReadKey();
            } while (chon != 0);
        }
        // Đọc file Matran.txt -> ma trận a[,]
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
        // Xuất ma trận a -> màn hình
        static void PrintMatrix(int[,] a)
        {
            Console.WriteLine("      0   1   2   3   4   5");
            Console.WriteLine("    -------------------------");
            for (int i = 0; i < a.GetLength(0); i++)
            {
                Console.Write(" " + i + "|");
                for (int j = 0; j < a.GetLength(1); j++)
                    Console.Write("{0, 4}", a[i, j]);
                Console.WriteLine("  |");
                Console.WriteLine("  |");
            }
            Console.WriteLine("    -------------------------");
        }
        // Đọc ma trận a --> WeightList g. Hàm trả về một đồ thị danh sách kề
        static WeightMatrix MatrixToWeightMatrix(int[,] mt)
        {
            // Khai báo đồ thị ma trận kề
            WeightMatrix g = new WeightMatrix();
            // Xác định số dòng (row), số cột (col)của ma trận mt
            int row = mt.GetLength(0);
            int col = mt.GetLength(1);
            // Xác định số đỉnh (g.N) của đồ thị và khở tạo g.A
            g.N = row * col;
            g.A = new int[g.N, g.N];
            // Khởi tạo giá trị ban đầu cho g.A với ô (i,i) = 0, còn lại = int.MaxValue
            for (int i = 0; i < g.N; i++)
                for (int j = 0; j < g.N; j++)
                    if (i == j) g.A[i, j] = 0;
                    else g.A[i, j] = int.MaxValue;
            // Duyệt từng ô của ma trận mt : i:0 -> <row, j:0 -> < col
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                {
                    // với mỗi ô (x,y) xác định đỉnh k tương ứng
                    int k = i * col + j;
                    // Mỗi một ô có tối đa 4 ô kề -> đỉnk k có tối đa 4 đỉnh kề -> tìm các đỉnh kề cho k
                    // Có 4 trường hợp như sau : i > 0, j > 0, j < col-1, i < row-1
                    if (i > 0)
                        g.A[k, k - col] = mt[i - 1, j];

                    if (j > 0)
                        g.A[k, k - 1] = mt[i, j - 1];

                    if (j < col - 1)
                        g.A[k, k + 1] = mt[i, j + 1];

                    if (i < row - 1)
                        g.A[k, k + col] = mt[i + 1, j];
                }
            // trả về g
            return g;
        }
        // Xuất đường đi ra biên từ ô s(x,y) ngắn nhất. Ô nằm trên biên có bậc < 4 (có dưới 4 đỉnh kề
        static void ToBorderline(WeightMatrix g, int s)
        {
            // Sử dụng Stack<int> st -> khởi tạo st
            Stack<int> st = new Stack<int>();
            // Biến bien : lưu trữ đỉnh nằm trên biên cần tìm, ban đầu = 0
            int bien = 0;
            // min : lưu giá trị độ dài đường đi ra biên ngắn nhất, ban đầu = int.MaxValue
            int min = int.MaxValue;
            // Xét tất cả các đường từ (x,y) ra biên để chọn đường ngắn nhất.
            // (Đỉnh ở biên có Deg < 4 -> viết thêm hàm DegV(i) tính bậc đỉnh i)
            // g.Dist : chứa các độ dài đến các đỉnh, g.Pre : chứa nội dung -> đường đi
            for (int i = 0; i < g.N; i++)
                if (DegV(i, g) < 4)     // Chỉ xét các đỉnh i có dưới 4 đỉnh kề -> bậc < 4
                    if (g.Dist[i] < min)
                    {
                        min = g.Dist[i];
                        bien = i;
                    }
            Console.WriteLine();
            // Đến đây biến bien lưu trữ đỉnh nằm trên biên gần nhất
            // Xuất đường đi ra biên : từ dinhS --> đỉnh bien và độ dài là bài toán xuất đường đi
            while (bien != s)
            {
                st.Push(bien);
                bien = g.Pre[bien];
            }
            Console.Write("Đường ra biên gần nhất, xuất phát từ đỉnh s({0}) : ", s);
            Console.ForegroundColor = ConsoleColor.Cyan;
            while (st.Count > 0) Console.Write(" -> " + st.Pop());
            Console.WriteLine("  Có độ dài = " + min);
            Console.ForegroundColor = ConsoleColor.White;
        }
        // Hàm Tính bậc của đỉnh v,lưu ý loại trừ các giá trị đặc biệt trong g.A
        static int DegV(int v, WeightMatrix g)
        {
            int deg = 0;
            for (int i = 0; i < g.N; i++)
                if (g.A[v, i] < int.MaxValue && g.A[v, i] != 0) deg++;
            return deg;
        }
        // Bài 6 : chọn thành phố
        static int SelectCity(WeightMatrix g)
        {
            // Chạy thuật toán Floyd -> ma trận d
            g.Floyd();
            // Biến cty : lưu thành phố được chọn, khởi tạo = -1
            int cty = -1;
            // Biến min tham gia tìm, khởi tạo = int.MaxValue;
            int min = int.MaxValue;
            // Duyệt (i : 0 -> < g.N)
            for (int i = 0; i < g.N; i++)
            {
                //Biến max : đường dài nhất khi chọn họp thành phố i
                int max = int.MinValue;
                // Duyệt (j : 0 -> g.N)
                for (int j = 0; j < g.N; j++)
                {
                    // Nếu g.D[i, j] > max thì max = g.D[i, j]
                    if (g.D[i, j] > max) max = g.D[i, j];

                }
                // Nếu max < min
                if (max < min)
                {
                    // cty = i và min = max;
                    cty = i;
                    min = max;
                }

            }
            // Trả về cty
            return cty;
        }
        static int chonXa(WeightMatrix g)
        {
            g.Floyd();
            int xa = -1;
            int min = int.MaxValue;
            for (int i = 0; i < g.N; i++)
            {
                //Biến max : đường dài nhất khi chọn xã i
                int max = int.MinValue;
                // Duyệt (j : 0 -> g.N)
                for (int j = 0; j < g.N; j++)
                {
                    // Nếu g.D[i, j] > max thì max = g.D[i, j]
                    if (g.D[i, j] > max) 
                        max = g.D[i, j];

                }
                // Nếu max < min
                if (max < min)
                {
                    // xa = i và min = max;
                    xa = i;
                    min = max;
                }
            }
            return xa;
        }
        static int FindMinCost(WeightMatrix g, int schoolXa)
        {
            int minCost = 0;

            // Duyệt qua tất cả các xã
            for (int i = 0; i < g.N; i++)
            {
                // Nếu không phải là xã có trường học
                if (i != schoolXa)
                {
                    // Tìm xã gần xã được xây trường học nhất 
                    int minDistanceToSchool = g.D[i, schoolXa];

                    // Cộng vào tổng chi phí
                    minCost += minDistanceToSchool;
                }
            }
            return minCost;
        }
        static void duongDi(WeightMatrix g, int schoolXa)
        {
            int DuongDi = 0;

            for (int i = 0; i < g.N; i++)
            {
                if (i != schoolXa)
                {
                    int minDistanceToSchool = g.D[i, schoolXa];
                    Console.Write(" --> {0}", minDistanceToSchool);
                }    
            }    
        }
    }
}
