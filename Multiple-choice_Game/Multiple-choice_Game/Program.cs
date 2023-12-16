using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Multiple_choice_Game
{
    internal class Program
    {
        //Hàm đọc tên tất cả bộ đề
        static void List_Exam()
        {
            Test t = new Test();
            t.ReadFolder();
            t.Choose_Test();
        }

        //Hàm bắt đầu giải đề
        static void Multi_choice()
        {
            Exam ex = new Exam();
            ex.ReadFile();
            Console.WriteLine("\n\n\n\t\tBẮT ĐẦU LÀM BÀI KIỂM TRA:\n");
            ex.Output_Quest();

            Console.ReadKey();
        }

        //In ra thông tin của tất cả người chơi đã tham gia
        static void Player_Scores()
        {
            Score s = new Score();

            //Yêu cầu nhập tên và lưu điểm của người chơi
            s.Input_Score();

            //In ra thông tin của tất cả người chơi
            s.ReadFile();
            s.Output_Player();

            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            //Nhập tiếng việt
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            //Chọn bộ đề
            List_Exam();

            //Bắt đầu giải đề
            Multi_choice();

            //Lưu điểm người chơi và in ra tất cả
            Player_Scores();

            Console.ReadKey();
        }
    }
}
