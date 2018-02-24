using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace task5
{
    class Program
    {
        static void Main(string[] args)
        {
            var task1 = new YandexTask5_1();
            var task2 = new YandexTask5_2();
            var task5 = new YandexTask5_5();

            foreach (var line in File.ReadLines("shkib.csv").Skip(1))
            {
                var parts = line.Split(new[] { ',' }, StringSplitOptions.None);
                task1.ProcessRow(parts);
                task2.ProcessRow(parts);
                task5.ProcessRow(parts);
            }
            
            Console.WriteLine($"# Поиск 5ти пользователей, сгенерировавших наибольшее количество запросов{Environment.NewLine}" +
                              $"{string.Join(Environment.NewLine, task1.GetResult())}{Environment.NewLine}" +
                              $"# Поиск 5ти пользователей, отправивших наибольшее количество данных{Environment.NewLine}" +
                              $"{string.Join(Environment.NewLine, task2.GetResult())}{Environment.NewLine}" +
                              $"# Рассматривая события сетевого трафика как символы неизвестного языка,{Environment.NewLine}" +
                              $"# найти 5 наиболее устойчивых N-грамм журнала событий{Environment.NewLine}" +
                              $"# (текста на неизвестном языке){Environment.NewLine}" +
                              $"{string.Join(Environment.NewLine, task5.GetResult())}");
            Console.ReadLine();
        }
    }
}
