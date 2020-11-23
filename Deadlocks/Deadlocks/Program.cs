using System;
using System.Threading;
namespace Deadlocks
{
public class Deadlocks
    {
        static readonly object firstLock = new object();
        static readonly object secondLock = new object();
        
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
    }
}