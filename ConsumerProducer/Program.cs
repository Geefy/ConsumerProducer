using System;
using System.Threading;

namespace ConsumerProducer
{
    class Program
    {
        static string[] products = new string[5];
        static int productCount = 0;
        static object _lock = new object();
        static void Main(string[] args)
        {

            Thread producer = new Thread(Produce);
            for (int i = 0; i < 10; i++)
            {

            Thread consumer = new Thread(Consume);
            consumer.Start();
            }

            producer.Start();
        }

        static void Produce()
        {
            while (true)
            {


                lock (_lock)
                {

                    while (productCount <= products.Length)
                    {
                        productCount++;
                        Console.WriteLine("Producer produces");
                        Monitor.PulseAll(_lock);
                    }
                    Console.WriteLine("Producer waits");
                    Monitor.Wait(_lock);
                }
            }

        }
        static void Consume()
        {
            while (true)
            {

                lock (_lock)
                {
                    while (productCount == 0)
                    {
                        Console.WriteLine("Consumer waits");
                        Monitor.Wait(_lock);
                    }
                    productCount--;
                    Console.WriteLine("Consumer eats" + productCount);
                    Monitor.PulseAll(_lock);
                }
            }
        }
    }
}
