using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
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
            string temp;

            if (listAns[3].Contains("!") == true)//đáp án cuối cung là "Tất cả các ý trên" => chỉ random 3 đáp án phía trên (0,1,2)
            {
                List<int> listNums = Shuffer(0, 3);//list dùng để đảo vị trí (4 lần đảo vị trí)
                foreach (int a in listNums)
                {
                    temp = listAns[a];
                    listAns[a] = listAns[listNums.IndexOf(a)];
                    listAns[listNums.IndexOf(a)] = temp;
                }
                //foreach (int a in listNums)
                //    Console.Write($"{a}\t");
            }
            else//random cả 4 đáp án
            {
                List<int> listNums = Shuffer(0, 4);
                foreach (int a in listNums)
                {
                    temp = listAns[a];
                    listAns[a] = listAns[listNums.IndexOf(a)];
                    listAns[listNums.IndexOf(a)] = temp;
                }
                //foreach (int a in listNums)
                //    Console.Write($"{a}\t");
            }
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
        public void Answer(int id)
        {
            int n;
            do
            {
                Console.Write("\nNgười chơi hãy chọn đáp án (1-4): ");
                n = int.Parse(Console.ReadLine());

                switch (n)
                {
                    case 1:
                        {
                            if (True_Ans(Answer1) == true)
                            {
                                Console.Write("\t!!!!!CORRECT!!!!!");
                                Score++;
                                break;
                            }
                            Console.Write($"\t!!!!!WRONG!!!!!");
                            break;
                        }
                    case 2:
                        {
                            if (True_Ans(Answer2) == true)
                            {
                                Console.Write("\t!!!!!CORRECT!!!!!");
                                Score++;
                                break;
                            }
                            Console.Write($"\t!!!!!WRONG!!!!!");
                            break;
                        }
                    case 3:
                        {
                            if (True_Ans(Answer3) == true)
                            {
                                Console.Write("\t!!!!!CORRECT!!!!!");
                                Score++;
                                break;
                            }
                            Console.Write($"\t!!!!!WRONG!!!!!");
                            break;
                        }
                    case 4:
                        {
                            if (True_Ans(Answer4) == true)
                            {
                                Console.Write("\t!!!!!CORRECT!!!!!");
                                Score++;
                                break;
                            }
                            Console.Write($"\t!!!!!WRONG!!!!!");
                            break;
                        }
                }
            } while (n > 4 || n < 1);
        }
    }


    internal class Exam
    {
        List<Quest> ls;
        public static int Final_Score;
        public static string Name = Test.Name_Choosen;
        public string fileName = Name;
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
                        Console.Write($"\n\n\t{o}. ");
                        o++;
                        q.Output_Quest(q.Id_Quest);
                        //Người chơi nhập câu trả lời
                        q.Answer(q.Id_Quest);
                        s = s + q.Score;
                    }
                }
                possible.RemoveAt(index);//xóa số đó khỏi list
            }
            Final_Score = s;
            Console.Write($"\nĐiểm của bạn là: {s}/{ls.Count}");
        }
    }
}
