using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiple_choice_Game
{
    internal class Program
    {
        static void Multi_choice()
        {
            Exam ex = new Exam();
            ex.ReadFile();
            Console.WriteLine("\t\tBẮT ĐẦU LÀM BÀI KIỂM TRA:\n");
            ex.Output_Quest();

            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            //Nhập tiếng việt
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            //Bắt đầu giải đề
            Multi_choice();


            //Nhập tên

            Console.ReadKey();
        }
    }
}
