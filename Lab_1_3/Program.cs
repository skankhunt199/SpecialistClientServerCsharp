namespace Lab_1_3
{
    internal class Program
    {
        private static double value = 1;
        private static readonly SemaphoreSlim cosSemaphore = new(1);
        private static readonly SemaphoreSlim acosSemaphore = new(0);

        static void Main(string[] args)
        {
            var allThreads = new List<Thread>();

            allThreads.AddRange(CreateThreads(Math.Cos, cosSemaphore, acosSemaphore));
            allThreads.AddRange(CreateThreads(Math.Acos, acosSemaphore, cosSemaphore));

            foreach (var thread in allThreads) thread.Start();
            foreach (var thread in allThreads) thread.Join();
        }

        private static List<Thread> CreateThreads(Func<double, double> math, SemaphoreSlim semaphore1, SemaphoreSlim semaphore2)
        {
            var threads = new List<Thread>();
            for (int i = 1; i <= 10; i++)
            {
                threads.Add(new Thread(() =>
                {
                    try
                    {
                        semaphore1.Wait();
                        value = math(value);
                        Console.WriteLine($"{Thread.CurrentThread.Name} {math.Method.Name}: Результат вычисления: {value}");                        
                    }
                    finally
                    {
                        semaphore2.Release();
                    }
                })
                {
                    Name = $"Поток {i}"
                });
            }
            return threads;
        }
    }
}
