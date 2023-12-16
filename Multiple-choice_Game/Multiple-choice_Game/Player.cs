using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            Console.WriteLine($"\n\t\t{Name}\t\t\t\t\t{Score}");
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
                int score = Convert.ToInt32(item[1]);
                Player p = new Player(name, score);
                list.Add(p);
            }
            f.Close();
        }

        //Lưu người chơi và điểm vào file


        //Nhập thông tin của người chơi vào file
        public void Input_Score()
        {
            Console.Write("\nHỆ THỐNG SẼ LƯU THÔNG TIN CỦA BẠN NHƯ SAU: ");
            Console.Write("\nNhập tên người chơi: ");
            string Name = Console.ReadLine();
            int score = FinalScore;
            Console.Write($"\t\tĐiểm: {score}");

            Console.Write("\nNếu bạn đồng ý lưu hãy ấn 0\nNếu không chương trình sẽ in ra danh sách người chơi và điểm: ");
            int x = int.Parse(Console.ReadLine());
            //FileStream f = new FileStream("..\\..\\Players\\Score.csv", FileMode.Open, FileAccess.ReadWrite);
            //Lưu người chơi và điểm vào file
            if (x == 0)
            {
                string text = Name + ";" + score.ToString() + "\n";

                //StreamWriter sw = new StreamWriter(f);
                //sw.WriteLine(text);
                File.AppendAllText("..\\..\\Players\\Score.csv", text);
            }
            //f.Close();
        }

        //In thông tin tất cả người chơi đã tham gia
        public void Output_Player()
        {
            Console.WriteLine("BẢNG ĐIỂM CỦA TẤT CẢ NGƯỜI CHƠI ĐÃ THAM GIA");
            foreach(Player p in list )
            {
                p.Output();
            }
        }
    }
}
