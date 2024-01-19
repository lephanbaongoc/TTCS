using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Multiple_choice_Game
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }


        public Player(string name, int score)
        {
            this.Name = name;
            this.Score = score;
        }

        //In ra người chơi và điểm
        public void Output()
        {
            Console.WriteLine(string.Format("{0,20}{1,5}{2,10}", $"{Name}", "|", $"{Score}"));
        }
    }

    internal class Score
    {
        List<Player> list;
        public static int FinalScore = Exam.Final_Score;
        public void ReadFile()
        {
            list = new List<Player>();
            FileStream f = new FileStream("..\\..\\Players\\Score.csv", FileMode.Open, FileAccess.Read);
            StreamReader rd = new StreamReader(f);
            string line;
            while ((line = rd.ReadLine()) != null)
            {
                string[] item = line.Split(';');
                string name = item[0];
                int score = int.Parse(item[1]);
                Player p = new Player(name, score);
                list.Add(p);
            }
            f.Close();
        }

        //Lưu người chơi và điểm vào file


        //Nhập thông tin của người chơi vào file
        public void Input_Score()
        {
            string Name;//tên người chơi
            int score = FinalScore;//điểm người chơi
            int x;//lựa chọn lưu điểm hay ko

            Console.Write("\nHỆ THỐNG SẼ LƯU THÔNG TIN CỦA BẠN NHƯ SAU: ");
            do
            {
                Console.Write("\nNhập tên người chơi: ");
                Name = Console.ReadLine();
            } while (Name == string.Empty);

            Console.Write($"\t\tĐiểm: {score}");
            while (true)
            {
                try
                {
                    Console.Write("\nNếu bạn đồng ý lưu hãy ấn '1'\nNếu không hãy ấn số '0' để hủy lưu\nChương trình sẽ in ra danh sách người chơi và điểm: ");
                    x = int.Parse(Console.ReadLine());

                    //Lưu người chơi và điểm vào file
                    if (x == 1)
                    {
                        Console.Write("\n\nHỆ THỐNG ĐÃ LƯU THÔNG TIN CỦA BẠN!");
                        string text = Name + ";" + score.ToString() + "\n";
                        File.AppendAllText("..\\..\\Players\\Score.csv", text);
                        break;
                    }
                    else if (x == 0)
                    {
                        Console.Write("\n\nBẠN ĐÃ CHỌN KHÔNG LƯU!");
                        break;
                    }
                    Console.WriteLine("\nHHÃY NHẬP VÀO '1' hoặc '0'!!");
                }
                catch (FormatException)
                {   //Khi giá trị nhập không phải số
                    Console.WriteLine("\nHHÃY NHẬP VÀO '1' hoặc '0'!!");
                }
            }
            Console.Write("\n\nPRESS ANY KEY TO CONTINUE!");
            Console.ReadKey();
            Console.Clear();
        }

        //In thông tin tất cả người chơi đã tham gia
        public void Output_Player()
        {
            Console.WriteLine("\n\nBẢNG ĐIỂM CỦA TẤT CẢ NGƯỜI CHƠI ĐÃ THAM GIA\n\n");
            Console.WriteLine("===================================================");
            Console.WriteLine(string.Format("{0,20}{1,5}{2,10}", "TÊN NGƯỜI CHƠI", "|", "ĐIỂM"));
            Console.WriteLine("===================================================");
            foreach (Player p in list)
            {
                p.Output();
            }
            Console.WriteLine("===================================================");
        }
    }
}
