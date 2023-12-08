using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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
        public string True_Answer { get; set; }
        public int Score = 0;

        public Quest(int id, string quest, string ans1, string ans2, string ans3, string ans4, string true_ans)
        {
            this.Id_Quest = id;
            this.Question = quest;
            this.Answer1 = ans1;
            this.Answer2 = ans2;
            this.Answer3 = ans3;
            this.Answer4 = ans4;
            this.True_Answer = true_ans;
        }

        //In câu hỏi
        public void Output(int id)
        {
            Console.Write($"{Question}" +
                                $"\n\t\t1. {Answer1}" +
                                $"\n\t\t2. {Answer2}" +
                                $"\n\t\t3. {Answer3}" +
                                $"\n\t\t4. {Answer4}\n");
        }

        //Kiểm tra đáp án
        public void Answer(int id)
        {
            int n;
            do
            {
                Console.Write("Người chơi hãy chọn đáp án (1-4): ");
                n = int.Parse(Console.ReadLine());

                switch (n)
                {
                    case 1:
                        {
                            if (Answer1 == True_Answer)
                            {
                                Console.Write("\t!!!!!CORRECT!!!!!");
                                Score++;
                                break;
                            }
                            Console.Write($"\t!!!!!WRONG!!!!!\t Correct Answer: {True_Answer}");
                            break;
                        }
                    case 2:
                        {
                            if (Answer2 == True_Answer)
                            {
                                Console.Write("\t!!!!!CORRECT!!!!!");
                                Score++;
                                break;
                            }
                            Console.Write($"\t!!!!!WRONG!!!!!\t Correct Answer: {True_Answer}");
                            break;
                        }
                    case 3:
                        {
                            if (Answer3 == True_Answer)
                            {
                                Console.Write("\t!!!!!CORRECT!!!!!");
                                Score++;
                                break;
                            }
                            Console.Write($"\t!!!!!WRONG!!!!!\t Correct Answer: {True_Answer}");
                            break;
                        }
                    case 4:
                        {
                            if (Answer4 == True_Answer)
                            {
                                Console.Write("\t!!!!!CORRECT!!!!!");
                                Score++;
                                break;
                            }
                            Console.Write($"\t!!!!!WRONG!!!!!\t Correct Answer: {True_Answer}");
                            break;
                        }
                }
            } while (n > 4 || n < 1);
        }
    }


    internal class Exam
    {
        List<Quest> ls;
        public void ReadFile()
        {
            ls = new List<Quest>();
            FileStream f = new FileStream("..\\..\\Examination\\text.csv", FileMode.Open, FileAccess.Read);
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
                string true_ans = item[6];
                Quest q = new Quest(id_quest, quest, ans_1, ans_2, ans_3, ans_4, true_ans);
                ls.Add(q);
            }
        }
        //Random
        public List<int> Shuffer(int n)//random ra 1 list gồm n phần tử ko trùng lặp -> Random từng phần từ trong list rồi xóa
        {
            List<int> possible = Enumerable.Range(1, n).ToList();
            List<int> listNumbers = new List<int>();
            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                int index = rand.Next(0, possible.Count);
                listNumbers.Add(possible[index]);
                possible.RemoveAt(index);
            }
            return listNumbers.ToList();
            //return 0;//số bất kì rồi xóa
        }
        public void Output_Quest()
        {
            List<int> listNumbers = Shuffer(15);

            int s = 0;
            int o = 1;
            foreach (int n in listNumbers)
            {
                foreach (Quest q in ls)
                {
                    if (n == q.Id_Quest)
                    {
                        //In ra câu hỏi
                        Console.Write($"\n\t{o}. ");
                        o++;
                        q.Output(q.Id_Quest);
                        //Người chơi nhập câu trả lời
                        q.Answer(q.Id_Quest);
                        s = s + q.Score;
                    }
                }
            }
            Console.Write($"Điểm của bạn là: {s}/15");
        }
        
    }
}
