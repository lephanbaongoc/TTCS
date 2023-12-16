using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiple_choice_Game
{
    public class Test_Kit
    {
        public string Name { get; set; }
        public Test_Kit(string name)
        {
            this.Name = name;
        }

        //In ra danh sách các file trong folder
        public void Output()
        {
            Console.WriteLine($"{Censor(Name)}");
        }

        //Ẩn đường dẫn, chỉ hiện tên file
        public string Censor(string x)
        {
            x = x.Replace("..\\..\\Examinations\\", "\0");
            return x;
        }
    }
    internal class Test
    {
        public static string Name_Choosen;
        List<Test_Kit> ls;
        //Đọc tên danh sách các bộ đề trong thư mục Examinations
        public void Choose_Test()
        {
            int o = 1;//Số thứ tự
            int a;//Số thứ tự của đề đã chọn
            string x = "";
            //In ra ds các đề có trong folder
            Console.WriteLine("Danh sách các đề có thể chọn: ");
            foreach (Test_Kit t in ls)
            {
                Console.Write($"\t\t\t{o}.");
                t.Output();
                o++;
            }

            //Chọn đề
            do
            {
                Console.Write("Chọn 1 đề trong danh sách trên theo số thứ tự như trên: ");
                a = Convert.ToInt32(Console.ReadLine());
                for (int i = 0; i < ls.Count; i++)
                {
                    if (a - 1 == i)
                    {
                        x = ls[i].Name;
                        break;
                    }
                }
            } while (a < 1 || a > ls.Count);
            Name_Choosen = x;
            Console.Write($"Bạn đã chọn đề: {Name_Choosen.Replace("..\\..\\Examinations\\", "\0")}");
        }

        

        public void ReadFolder()
        {
            string[] ls_Files = Directory.GetFiles("..\\..\\Examinations");//Đọc danh sách các file có trong thư mục rồi lưu vào mảng
            ls = new List<Test_Kit> ();
            foreach (string s in ls_Files)
            {
                Test_Kit t = new Test_Kit(s);
                ls.Add(t);
            }
        }
    }
}
