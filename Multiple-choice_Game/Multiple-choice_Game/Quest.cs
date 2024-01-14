using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Multiple_choice_Game
{
    public class Quest
    {
        public int Id_Quest { get; set; }
        public string Question { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }

        public int Score = 0;

        public Quest(int id, string quest, string ans1, string ans2, string ans3, string ans4)
        {
            this.Id_Quest = id;
            this.Question = quest;
            this.Answer1 = ans1;
            this.Answer2 = ans2;
            this.Answer3 = ans3;
            this.Answer4 = ans4;
        }

        //In câu hỏi
        public void Output_Quest(int id)
        {
            Console.Write($"{Question}\n");
            Output_Answer(Answer1, Answer2, Answer3, Answer4);//Random và in ra các đáp án
        }
        //random đáp án
        public void Output_Answer(string ans1, string ans2, string ans3, string ans4)
        {
            List<string> listAns = new List<string> { ans1, ans2, ans3, ans4 };
            Random rng = new Random();

            //A, B, A&B true, A&B false => Chỉ Random A, B (0,1)
            if (listAns[2].Contains("!") == true && listAns[3].Contains("!") == true)
            {
                List<int> listNums = Shuffer(0, 2);//list dùng để đảo vị trí (nếu số đầu tiên bốc đc là 2 => đáp án 2 sẽ ở vị trí đầu tiên)
                foreach (int a in listNums)
                {
                    if (a == 0) listAns[listNums.IndexOf(a)] = Answer1;
                    if (a == 1) listAns[listNums.IndexOf(a)] = Answer2;
                }
            }

            //đáp án cuối cùng là "Tất cả các ý trên" => chỉ random 3 đáp án phía trên (0,1,2)
            else if (listAns[3].Contains("!") == true)
            {
                List<int> listNums = Shuffer(0, 3);//list dùng để đảo vị trí (nếu số đầu tiên bốc đc là 2 => đáp án 2 sẽ ở vị trí đầu tiên)
                foreach (int a in listNums)
                {
                    if (a == 0) listAns[listNums.IndexOf(a)] = Answer1;
                    if (a == 1) listAns[listNums.IndexOf(a)] = Answer2;
                    if (a == 2) listAns[listNums.IndexOf(a)] = Answer3;
                }
            }
            else//random cả 4 đáp án
            {
                List<int> listNums = Shuffer(0, 4);
                foreach (int a in listNums)
                {
                    if (a == 0) listAns[listNums.IndexOf(a)] = Answer1;
                    if (a == 1) listAns[listNums.IndexOf(a)] = Answer2;
                    if (a == 2) listAns[listNums.IndexOf(a)] = Answer3;
                    if (a == 3) listAns[listNums.IndexOf(a)] = Answer4;
                }
            }
            //Console.WriteLine("\n");
            Answer1 = listAns[0];
            Answer2 = listAns[1];
            Answer3 = listAns[2];
            Answer4 = listAns[3];
            Console.Write($"\t\t1. {Censor_Ans(Answer1)}\n\t\t2. {Censor_Ans(Answer2)}\n\t\t3. {Censor_Ans(Answer3)}\n\t\t4. {Censor_Ans(Answer4)}\n");
        }

        //Random
        public List<int> Shuffer(int x, int n)//random ra 1 list gồm n phần tử ko trùng lặp -> Random từng phần từ trong list rồi xóa
        {
            List<int> possible = Enumerable.Range(x, n).ToList();
            List<int> listNumbers = new List<int>();
            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                int index = rand.Next(0, possible.Count);
                listNumbers.Add(possible[index]);
                possible.RemoveAt(index);
            }
            possible.Clear();
            possible = null;
            return listNumbers.ToList();
            //return 0;//số bất kì rồi xóa
        }

        //Ấn ký tự đánh dầu đáp án
        public string Censor_Ans(string x)
        {
            x = x.Replace("~", "\0");
            x = x.Replace("!", "\0");
            x = x.Replace("^", "\0");
            return x;
        }

        //Tìm đáp án chính xác từ trong các đáp án trong file
        public bool True_Ans(string x)
        {
            if (x.Contains("^") == true)//Kiểm tra xem trong chuỗi x có chuỗi "^" ko
            {
                return true;
            }
            else return false;
        }

        //Kiểm tra đáp án
        public void Answer(int n)
        {
            switch (n)
            {
                case 1:
                    {
                        if (True_Ans(Answer1) == true)
                        {
                            Console.Write("\t!!!!!CORRECT!!!!!\t");
                            Score++;
                            break;
                        }
                        Console.Write($"\t!!!!!WRONG!!!!!\t");
                        break;
                    }
                case 2:
                    {
                        if (True_Ans(Answer2) == true)
                        {
                            Console.Write("\t!!!!!CORRECT!!!!!\t");
                            Score++;
                            break;
                        }
                        Console.Write($"\t!!!!!WRONG!!!!!\t");
                        break;
                    }
                case 3:
                    {
                        if (True_Ans(Answer3) == true)
                        {
                            Console.Write("\t!!!!!CORRECT!!!!!\t");
                            Score++;
                            break;
                        }
                        Console.Write($"\t!!!!!WRONG!!!!!\t");
                        break;
                    }
                case 4:
                    {
                        if (True_Ans(Answer4) == true)
                        {
                            Console.Write("\t!!!!!CORRECT!!!!!\t");
                            Score++;
                            break;
                        }
                        Console.Write($"\t!!!!!WRONG!!!!!\t");
                        break;
                    }
            }
        }
    }

    //Xem lại
    public class Review
    {
        public int Id { get; set; }
        public string Quest { get; set; }
        public string Ans1 { get; set; }
        public string Ans2 { get; set; }
        public string Ans3 { get; set; }
        public string Ans4 { get; set; }
        public int Selected_Ans { get; set; }

        public Review(int id, string quest, string ans1, string ans2, string ans3, string ans4, int selected_Ans)
        {
            this.Id = id;
            this.Quest = quest;
            this.Ans1 = ans1;
            this.Ans2 = ans2;
            this.Ans3 = ans3;
            this.Ans4 = ans4;
            this.Selected_Ans = selected_Ans;
        }

        //Tìm đáp án chính xác
        public bool True_Ans(string x)
        {
            if (x.Contains("^") == true)//Kiểm tra xem trong chuỗi x có chuỗi "^" ko
            {
                return true;
            }
            else return false;
        }
        //Che kí tự đầu của đáp án
        public string Censor_Ans(string x)
        {
            x = x.Replace("~", "\0");
            x = x.Replace("!", "\0");
            x = x.Replace("^", "\0");
            return x;
        }
        public void Output()
        {
            List<string> listAns = new List<string> { Ans1, Ans2, Ans3, Ans4 };
            Console.Write($"{Quest}\n");
            if (True_Ans(listAns[Selected_Ans - 1]) == true)//nếu đáp án bạn chọn là đáp án đúng
            {
                for (int i = 0; i < 4; i++)
                {
                    if (Selected_Ans - 1 == i) Console.WriteLine($"BẠN ĐÚNG!\t\t{i+1}. {Censor_Ans(listAns[i])}");
                    else Console.WriteLine($"\t\t\t{i + 1}. {Censor_Ans(listAns[i])}");
                }
            }
            else//là đáp án sai
            {
                for (int i = 0; i < 4; i++)
                {
                    if (Selected_Ans - 1 == i) Console.WriteLine($"BẠN SAI!\t\t{i + 1}. {Censor_Ans(listAns[i])}");
                    else if (True_Ans(listAns[i]) == true) Console.WriteLine($"ĐÁP ÁN ĐÚNG LÀ\t\t{i + 1}. {Censor_Ans(listAns[i])}");
                    else Console.WriteLine($"\t\t\t{i + 1}. {Censor_Ans(listAns[i])}");
                }
            }

        }
    }

    internal class Exam
    {
        List<Quest> ls;
        public static int Final_Score;
        public static string Name = Test.Name_Choosen;
        public string fileName = Name;
        List<Review> selected_ans;
        public void ReadFile()
        {
            ls = new List<Quest>();
            FileStream f = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader rd = new StreamReader(f);
            string line;
            while ((line = rd.ReadLine()) != null)
            {
                string[] item = line.Split(';');
                int id_quest = Convert.ToInt32(item[0]);
                string quest = item[1];
                string ans_1 = item[2];
                string ans_2 = item[3];
                string ans_3 = item[4];
                string ans_4 = item[5];
                Quest q = new Quest(id_quest, quest, ans_1, ans_2, ans_3, ans_4);
                ls.Add(q);
            }
        }

        public void Output_Quest()
        {
            int num = ls.Count();
            int s = 0;//tính điểm
            int o = 1;//số thứ tự câu hỏi khi in ra màn hình

            selected_ans = new List<Review>();
            int a;//đáp án đã chọn

            //Hàm dung để Random
            List<int> possible = Enumerable.Range(1, num).ToList();//taoj ra 1 list gồm stt các câu hỏi
            Random rand = new Random();
            for (int i = 0; i < num; i++)
            {
                int index = rand.Next(0, possible.Count);//random ra 1 số bất kỳ trong list
                foreach (Quest q in ls)
                {
                    if (possible[index] == q.Id_Quest)//tìm và in ra câu hỏi
                    {

                        //In ra câu hỏi
                        Console.Write($"\tCâu {o}/{ls.Count}. ");
                        o++;
                        q.Output_Quest(q.Id_Quest);

                        //Người chơi nhập câu trả lời
                        while (true)
                        {//hàm kiểm tra xem đầu vào có phải số không
                            try
                            {
                                do
                                {
                                    Console.Write("\nNgười chơi hãy chọn đáp án (1-4): ");
                                    a = int.Parse(Console.ReadLine());
                                } while (a < 1 || a > 4);
                                q.Answer(a);

                                s = s + q.Score;
                                //Thêm vào danh sách để Review
                                Review r = new Review(q.Id_Quest, q.Question, q.Answer1, q.Answer2, q.Answer3, q.Answer4, a);
                                selected_ans.Add(r);

                                Console.WriteLine($"\nĐiểm của bạn là: {s}/{ls.Count}");
                                Console.Write("\n\nPRESS ANY KEY TO CONTINUE!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            catch (FormatException)
                            {   //Khi giá trị nhập không phải số
                                Console.WriteLine("HÃY NHẬP VÀO MỘT SỐ!!");
                            }
                        }
                    }
                }
                possible.RemoveAt(index);//xóa số đó khỏi list
            }
            Final_Score = s;
            Console.Write($"\nĐiểm tổng của bạn là: {s}/{ls.Count}");
        }
        //Review
        public void Review_Test()
        {
            int o = 1;//số thứ tự

            Console.WriteLine("\nXEM LẠI BÀI KIỂM TRA: \n");
            foreach (Review r in selected_ans)
            {
                Console.Write($"\n\tCâu {o}/{ls.Count}. ");
                r.Output();
                o++;

                if (o % 4 == 0)
                {
                    Console.Write("\n\nPRESS ANY KEY TO CONTINUE!");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            Console.Write("\n\nPRESS ANY KEY TO CONTINUE!");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
