using System;
using System.Threading;
namespace Deadlocks
{
public class Deadlocks
    {
        static readonly object firstLock = new object();
        static readonly object secondLock = new object();
        static readonly object thirdLock = new object();
        static readonly object fourthLock = new object();
        
        static void Main()
        {
            new Thread(ThreadJob).Start();
                                                    // Wait until we're fairly sure the other thread has grabbed firstLock
            Thread.Sleep(500);
            Console.WriteLine("Locking secondLock");
            lock (secondLock)
            {
                Console.WriteLine("Locked secondLock");
                Console.WriteLine("Locking firstLock");
                lock (firstLock)
                {
                    Console.WriteLine("Locked firstLock");
                }
                Console.WriteLine("Released firstLock");
            }
            Console.WriteLine("Released secondLock");
            Console.Read();
            
            //------
            
            Thread thread1 = new Thread((ThreadStart)ObliviousFunction);
            Thread thread2 = new Thread((ThreadStart)BlindFunction);

            thread1.Start();
            thread2.Start();

            while (true)
            {
                // Stare at the two threads in deadlock.
            }
            
            //------
        }       
        
        static void ThreadJob()
        {
            Console.WriteLine("\t\t\t\tLocking firstLock");
            lock (firstLock)
            {
                Console.WriteLine("\t\t\t\tLocked firstLock");
                                                    // Wait until we're fairly sure the first thread has grabbed secondLock
                Thread.Sleep(1000);
                Console.WriteLine("\t\t\t\tLocking secondLock");
                lock (secondLock)
                {
                    Console.WriteLine("\t\t\t\tLocked secondLock");
                }
                Console.WriteLine("\t\t\t\tReleased secondLock");
            }
            Console.WriteLine("\t\t\t\tReleased firstLock");
        }
        public static void ObliviousFunction()
        {
            lock (thirdLock)
            {
                Thread.Sleep(1000); // Wait for the blind to lead
                lock (fourthLock)
                {
                }
            }
        }

        public static void BlindFunction()
        {
            lock (thirdLock)
            {
                Thread.Sleep(1000); // Wait for oblivion
                lock (fourthLock)
                {
                }
            }
        }
    }
}