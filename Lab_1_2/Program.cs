namespace Lab_1_2
{
    internal class Program
    {
        private record Params(int Value1 = 1, int Value2 = 100)
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


        private static void StartThreads(IEnumerable<Thread> threads)
        {
            foreach (var thread in threads.Reverse())
            {
                thread.Start();
                thread.Join();
            }
        }

        private static List<Thread> CreateThreads(int threadCount)
        {
            var threads = new List<Thread>();
            for (int i = 1; i <= threadCount; i++)
            {
                var parameters = new Params();
                var thread = new Thread(parameters.Print)
                {
                    Name = $"Поток {i}"
                };
                threads.Add(thread);
            }
            return threads;
        }
    }
}
