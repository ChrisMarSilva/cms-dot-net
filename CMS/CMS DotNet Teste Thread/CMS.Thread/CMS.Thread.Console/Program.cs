using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Thread.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            System.Console.WriteLine("Inicio");
            System.Console.WriteLine("");

            //-----------------------------------------------------------------------------------------------------------
            //-----------------------------------------------------------------------------------------------------------

            //DateTime StartDateTime = DateTime.Now;
            //System.Console.WriteLine(@"foreach Loop start at : {0}", StartDateTime);
            //List<int> integerList = Enumerable.Range(1, 10).ToList();
            //foreach (int i in integerList)
            //{
            //    long total = DoSomeIndependentTimeconsumingTask();
            //    System.Console.WriteLine("{0} - {1}", i, total);
            //};
            //DateTime EndDateTime = DateTime.Now;
            //System.Console.WriteLine(@"foreach Loop end at : {0}", EndDateTime);
            //TimeSpan span = EndDateTime - StartDateTime;
            //int ms = (int)span.TotalMilliseconds;
            //System.Console.WriteLine(@"Time Taken by foreach Loop in miliseconds {0}", ms);

            //System.Console.WriteLine("");

            //DateTime StartDateTime2 = DateTime.Now;
            //System.Console.WriteLine(@"Parallel foreach method start at : {0}", StartDateTime2);
            //List<int> integerList2 = Enumerable.Range(1, 10).ToList();
            //Parallel.ForEach(integerList2, i =>
            //{
            //    long total = DoSomeIndependentTimeconsumingTask();
            //    System.Console.WriteLine("{0} - {1}", i, total);
            //});

            //-----------------------------------------------------------------------------------------------------------
            //-----------------------------------------------------------------------------------------------------------

            //DateTime EndDateTime2 = DateTime.Now;
            //System.Console.WriteLine(@"Parallel foreach method end at : {0}", EndDateTime2);
            //TimeSpan span2 = EndDateTime2 - StartDateTime2;
            //int ms2 = (int)span2.TotalMilliseconds;
            //System.Console.WriteLine(@"Time Taken by Parallel foreach method in miliseconds {0}", ms2);

            //List<int> integerList = Enumerable.Range(0, 10).ToList();
            //Parallel.ForEach(integerList, i => System.Console.WriteLine(@" 0011 value of i = {0}, thread = {1}", i, System.Threading.Thread.CurrentThread.ManagedThreadId) );
            //var options = new ParallelOptions() { MaxDegreeOfParallelism = 2 };
            //Parallel.ForEach(integerList, options, i => System.Console.WriteLine(@" 0022 value of i = {0}, thread = {1}", i, System.Threading.Thread.CurrentThread.ManagedThreadId));

            //-----------------------------------------------------------------------------------------------------------
            //-----------------------------------------------------------------------------------------------------------

            //string[] colors = {"1. Red","2. Green","3. Blue","4. Yellow","5. White","6. Black","7. Violet","8. Brown","9. Orange","10. Pink"};
            //System.Console.WriteLine("Traditional foreach loop");
            //var sw = Stopwatch.StartNew();
            //foreach (string color in colors)
            //{
            //    System.Console.WriteLine("        {0}, Thread Id= {1}", color, System.Threading.Thread.CurrentThread.ManagedThreadId);
            //    System.Threading.Thread.Sleep(10);
            //}
            //System.Console.WriteLine("foreach loop execution time = {0} seconds", sw.Elapsed.TotalSeconds);

            //System.Console.WriteLine("");

            //System.Console.WriteLine("Using Parallel.ForEach");
            //sw = Stopwatch.StartNew();
            //Parallel.ForEach(colors, color =>
            //{
            //    System.Console.WriteLine("        {0}, Thread Id= {1}", color, System.Threading.Thread.CurrentThread.ManagedThreadId);
            //    System.Threading.Thread.Sleep(10);
            //}
            //);
            //System.Console.WriteLine("Parallel.ForEach() execution time = {0} seconds", sw.Elapsed.TotalSeconds);


            //-----------------------------------------------------------------------------------------------------------
            //-----------------------------------------------------------------------------------------------------------

            // await Task.Delay(i);
            // Thread.Sleep(10);

            //Parallel.ForEach(myCollection, async item => { });

            //Thread th = new Thread(() => { });

            //var tasks = myCollection.Select(async item => { });
            //await Task.WhenAll(tasks);

            // await Task.Run(() => { });

            // Task t = Task.Factory.StartNew( () => { } );
            // t.Wait();

            //Task taskA = Task.Run(() => Thread.Sleep(2000));
            //Console.WriteLine("taskA Status: {0}", taskA.Status);
            //taskA.Wait();
            //Console.WriteLine("taskA Status: {0}", taskA.Status);

            //Task taskA = Task.Run(() => Thread.Sleep(2000));
            //taskA.Wait(1000);       // Wait for 1 second.
            //    bool completed = taskA.IsCompleted;
            //    Console.WriteLine("Task A completed: {0}, Status: {1}",completed, taskA.Status);
            //    if (!completed) Console.WriteLine("Timed out before task A completed.");

            //var tasks = new Task[3];
            //var rnd = new Random();
            //for (int ctr = 0; ctr <= 2; ctr++) tasks[ctr] = Task.Run(() => Thread.Sleep(rnd.Next(500, 3000)));
            //int index = Task.WaitAny(tasks);
            //Console.WriteLine("Task #{0} completed first.\n", tasks[index].Id);
            //Console.WriteLine("Status of all tasks:");
            //foreach (var t in tasks) Console.WriteLine("   Task #{0}: {1}", t.Id, t.Status);

            //Task[] tasks = new Task[10];
            //for (int i = 0; i < 10; i++) tasks[i] = Task.Run(() => Thread.Sleep(2000));
            //Task.WaitAll(tasks);
            //foreach (var t in tasks) Console.WriteLine("   Task #{0}: {1}", t.Id, t.Status);

            //Task[] tasks = new Task[2];
            //String[] files = null;
            //String[] dirs = null;
            //String docsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //tasks[0] = Task.Factory.StartNew(() => files = Directory.GetFiles(docsDirectory));
            //tasks[1] = Task.Factory.StartNew(() => dirs = Directory.GetDirectories(docsDirectory));
            //Task.Factory.ContinueWhenAll(tasks, completedTasks => { Console.WriteLine("{0} contains: ", docsDirectory); Console.WriteLine("   {0} subdirectories", dirs.Length); Console.WriteLine("   {0} files", files.Length); });

            //-----------------------------------------------------------------------------------------------------------
            //-----------------------------------------------------------------------------------------------------------

            System.Console.WriteLine("");
            System.Console.WriteLine("Fim");
            System.Console.ReadKey();
        }

        static long DoSomeIndependentTimeconsumingTask()
        {
            long total = 0;
            for (int i = 1; i < 100000000; i++)
                total += i;
            return total;
        }
    }
}
