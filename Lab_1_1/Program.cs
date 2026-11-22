namespace Lab_1_1
{
    internal class Program
    {
        private record Params(int Value1, int Value2)
        {
            public void Print()
            {
                for (int i = Value1; i <= Value2; i++)
                {
                    Console.WriteLine($"Поток {Thread.CurrentThread.Name}: {i}");
                }
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Введите количество потоков: ");
            var threadCount = int.Parse(Console.ReadLine());
            var threads = CreateThreads(threadCount);
            StartThreads(threads);
        }

        private static void StartThreads(List<Thread> threads)
        {
            foreach (var thread in threads)
            {
                thread.Start();
            }
        }

        private static List<Thread> CreateThreads(int threadCount)
        {
            var threads = new List<Thread>();
            for (int i = 1; i <= threadCount; i++)
            {
                var parameters = GetRangeValues(i);
                var thread = new Thread(parameters.Print)
                {
                    Name = $"Поток {i}"
                };
                threads.Add(thread);
            }
            return threads;
        }

        private static Params GetRangeValues(int threadNumber)
        {
            Console.Write($"Введите диапазон чисел для {threadNumber} потока (через пробел): ");
            var input = Console.ReadLine().Split(' ');
            if(int.TryParse(input[0], out int value1) && int.TryParse(input[1], out int value2))
            {
                return new Params(value1, value2);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите два целых числа.");
                return GetRangeValues(threadNumber);
            }
        }
    }
}
