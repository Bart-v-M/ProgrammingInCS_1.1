using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProgrammingInCS_1._1
{
    class Program
    {
        /*
        // Parallel.Invoke in use
        static void Main(string[] args)
        {
            Parallel.Invoke(()=>Task1(), ()=>Task2());  // Performs multiple tasks at the same time
            Console.WriteLine("Finished processing. Press a key to end.");
            Console.ReadKey();
        }

        static void Task1()
        {
            Console.WriteLine("Task 1 starting");
            Thread.Sleep(2000);
            Console.WriteLine("Task 1 ending");
        }

        static void Task2()
        {
            Console.WriteLine("Task 2 starting");
            Thread.Sleep(1000);
            Console.WriteLine("Task 2 ending");
        }
        */

        /*
        // Parallel.ForEach in use
        static void Main(string[] args)
        {
            var items = Enumerable.Range(0, 500);
            Parallel.ForEach(items, item =>        // Performs multiple tasks parallel using .Foreach method
            {
                WorkOnItem(item);
            });

            Console.WriteLine("Finished processing. Press a key to end.");
            Console.ReadKey();
        }

        static void WorkOnItem(object item)
        {
            Console.WriteLine("Started working on: " + item);
            Thread.Sleep(2000);
            Console.WriteLine("Finished working on: " + item);
        }
        */

        /*
        // Parallel.For in use
        static void Main(string[] args)
        {
            var items = Enumerable.Range(0, 500).ToArray();
            Parallel.For(0, items.Length, i =>                       // Performs multiple tasks parallel using .For method
            {
                WorkOnItem(items[i]);
            });

            Console.WriteLine("Finished processing. Press a key to end.");
            Console.ReadKey();
        }

        static void WorkOnItem(object item)
        {
            Console.WriteLine("Started working on: " + item);
            Thread.Sleep(2000);
            Console.WriteLine("Finished working on: " + item);
        }
        */

        /*
        // Managing a parallel For loop
        static void Main(string[] args)
        {
            var items = Enumerable.Range(0, 500).ToArray();

            ParallelLoopResult result = 
                Parallel.For(0, items.Count(), (int i, ParallelLoopState loopState) =>         
            {
                if (i == 200)
                    loopState.Break();  // or .Stop
                
                WorkOnItem(items[i]);
            });

            Console.WriteLine("Completed: " + result.IsCompleted);
            Console.WriteLine("Items: " + result.LowestBreakIteration);

            Console.WriteLine("Finished processing. Press a key to end.");
            Console.ReadKey();
        }

        static void WorkOnItem(object item)
        {
            Console.WriteLine("Started working on: " + item);
            Thread.Sleep(2000);
            Console.WriteLine("Finished working on: " + item);
        }
        */

        ////A parallel LINQ query
        //static void Main(string[] args)
        //{
        //    Stopwatch stopwatch = new Stopwatch();
        //    stopwatch.Start();

        //    Person[] people = new Person[]
        //    {
        //        new Person { Name = "Alan", City = "Hull" },
        //        new Person { Name = "Beryl", City = "Seattle" },
        //        new Person { Name = "Charles", City = "London" },
        //        new Person { Name = "David", City = "Seattle" },
        //        new Person { Name = "Eddy", City = "Paris" },
        //        new Person { Name = "Fred", City = "Berlin" },
        //        new Person { Name = "Gordon", City = "Hull" },
        //        new Person { Name = "Henry", City = "Seattle" },
        //        new Person { Name = "Isaac", City = "Seattle" },
        //        new Person { Name = "James", City = "London" }
        //    };

        //    var result = from person in people.AsParallel()   // Addition of .AsParallel(), which examines if the query would be speeded up in parallel or not
        //                 where person.City == "Seattle"
        //                 select person;

        //    foreach (var person in result)
        //        Console.WriteLine(person.Name);

        //    stopwatch.Stop();
        //    Console.WriteLine($"Finished processing in {stopwatch.ElapsedMilliseconds} milliseconds. Press a key to end.");
        //    Console.ReadKey();
        //}

        //class Person
        //{
        //    public string Name { get; set; }
        //    public string City { get; set; }
        //}



        // Informing parallelization
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            Person[] people = new Person[]
            {
                new Person { Name = "Alan", City = "Hull" },
                new Person { Name = "Beryl", City = "Seattle" },
                new Person { Name = "Charles", City = "London" },
                new Person { Name = "David", City = "Seattle" },
                new Person { Name = "Eddy", City = "Paris" },
                new Person { Name = "Fred", City = "Berlin" },
                new Person { Name = "Gordon", City = "Hull" },
                new Person { Name = "Henry", City = "Seattle" },
                new Person { Name = "Isaac", City = "Seattle" },
                new Person { Name = "James", City = "London" }
            };

            stopwatch.Start();

            var result = from person in people.AsParallel().
                         WithDegreeOfParallelism(4).                                // Request that query is executed with a max of 4 processors
                         WithExecutionMode(ParallelExecutionMode.ForceParallelism)  // Here parallel execution is forced
                         where person.City == "Seattle"
                         select person;

            foreach (var person in result)
                Console.WriteLine(person.Name);

            stopwatch.Stop();
            Console.WriteLine($"Finished processing in {stopwatch.ElapsedMilliseconds} milliseconds. Press a key to end.");
            Console.ReadKey();
        }

        class Person
        {
            public string Name { get; set; }
            public string City { get; set; }
        }



        //// Using AsOrdered to preserve data ordering
        //static void Main(string[] args)
        //{
        //    Stopwatch stopwatch = new Stopwatch();
        //    stopwatch.Start();

        //    Person[] people = new Person[]
        //    {
        //        new Person { Name = "Alan", City = "Hull" },
        //        new Person { Name = "Beryl", City = "Seattle" },
        //        new Person { Name = "Charles", City = "London" },
        //        new Person { Name = "David", City = "Seattle" },
        //        new Person { Name = "Eddy", City = "Paris" },
        //        new Person { Name = "Fred", City = "Berlin" },
        //        new Person { Name = "Gordon", City = "Hull" },
        //        new Person { Name = "Henry", City = "Seattle" },
        //        new Person { Name = "Isaac", City = "Seattle" },
        //        new Person { Name = "James", City = "London" }
        //    };

        //    var result = from person in people.AsParallel().AsOrdered()    
        //                 where person.City == "Seattle"
        //                 select person;

        //    foreach (var person in result)
        //        Console.WriteLine(person.Name);

        //    stopwatch.Stop();
        //    Console.WriteLine($"Finished processing in {stopwatch.ElapsedMilliseconds} milliseconds. Press a key to end.");
        //    Console.ReadKey();
        //}

        //class Person
        //{
        //    public string Name { get; set; }
        //    public string City { get; set; }
        //}


        /*
        // Identifying elements of a parallel query as sequential
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Person[] people = new Person[]
            {
                new Person { Name = "Alan", City = "Hull" },
                new Person { Name = "Beryl", City = "Seattle" },
                new Person { Name = "Charles", City = "London" },
                new Person { Name = "David", City = "Seattle" },
                new Person { Name = "Eddy", City = "Paris" },
                new Person { Name = "Fred", City = "Berlin" },
                new Person { Name = "Gordon", City = "Hull" },
                new Person { Name = "Henry", City = "Seattle" },
                new Person { Name = "Isaac", City = "Seattle" },
                new Person { Name = "James", City = "London" }
            };

            var result = (from person in people.AsParallel()
                          where person.City == "Seattle"
                          orderby person.Name
                          select new
                          {
                              Name = person.Name
                          }).AsSequential().Take(4);

            foreach (var person in result)
                Console.WriteLine(person.Name);

            stopwatch.Stop();
            Console.WriteLine($"Finished processing in {stopwatch.ElapsedMilliseconds} milliseconds. Press a key to end.");
            Console.ReadKey();
        }

        class Person
        {
            public string Name { get; set; }
            public string City { get; set; }
        }
        */

        /*
        // Using the ForAll method
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Person[] people = new Person[]
            {
                new Person { Name = "Alan", City = "Hull" },
                new Person { Name = "Beryl", City = "Seattle" },
                new Person { Name = "Charles", City = "London" },
                new Person { Name = "David", City = "Seattle" },
                new Person { Name = "Eddy", City = "Paris" },
                new Person { Name = "Fred", City = "Berlin" },
                new Person { Name = "Gordon", City = "Hull" },
                new Person { Name = "Henry", City = "Seattle" },
                new Person { Name = "Isaac", City = "Seattle" },
                new Person { Name = "James", City = "London" }
            };

            var result = from person in people.AsParallel()
                         where person.City == "Seattle"
                         select person;
            result.ForAll(person => Console.WriteLine(person.Name));  // The parallel nature of the execution of ForAll means that the order 
                                                                      // of the printed output will not reflect the ordering of the input data.
                                                                      // Without the AsParallel method the ForAll method cannot be used.

            stopwatch.Stop();
            Console.WriteLine($"Finished processing in {stopwatch.ElapsedMilliseconds} milliseconds. Press a key to end.");
            Console.ReadKey();
        }

        class Person
        {
            public string Name { get; set; }
            public string City { get; set; }
        }
        */

        /*
        // Exceptions in PLINQ queries
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Person[] people = new Person[]
            {
                new Person { Name = "Alan", City = "Hull" },
                new Person { Name = "Beryl", City = "Seattle" },
                new Person { Name = "Charles", City = "London" },
                new Person { Name = "David", City = "Seattle" },
                new Person { Name = "Eddy", City = "Paris" },
                new Person { Name = "Fred", City = "Berlin" },
                new Person { Name = "Gordon", City = "Hull" },
                new Person { Name = "Henry", City = "Seattle" },
                new Person { Name = "Isaac", City = "Seattle" },
                new Person { Name = "James", City = "London" }
            };

            try
            {
                var result = from person in people.AsParallel()
                             where Person.CheckCity(person.City)
                             select person;
                result.ForAll(person => Console.WriteLine(person.Name));

            }
            catch (AggregateException e)
            {
                Console.WriteLine(e.InnerExceptions.Count + " exceptions.");
            }

            stopwatch.Stop();
            Console.WriteLine($"Finished processing in {stopwatch.ElapsedMilliseconds} milliseconds. Press a key to end.");
            Console.ReadKey();
        }

        class Person
        {
            public string Name { get; set; }
            public string City { get; set; }

            public static bool CheckCity(string cityName)
            {
                if (cityName == "")
                    throw new ArgumentException(cityName);
                return (cityName == "Seattle" || cityName == "London");
            }
        }
        */

        // CREATE TASKS

        /*
        // Create a task
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Task newTask = new Task(() => DoWork());
            newTask.Start();
            newTask.Wait();

            stopwatch.Stop();
            Console.WriteLine($"Finished processing in {stopwatch.ElapsedMilliseconds} milliseconds. Press a key to end.");
            Console.ReadKey();
        }

        public static void DoWork()
        {
            Console.WriteLine("Work starting");
            Thread.Sleep(2000);
            Console.WriteLine("Work finished");
        }
        */

        /*
        // Run a task
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Task newTask = Task.Run(() => DoWork()); // Your app can use tasks this way if you just want to start
            newTask.Wait();                          // the tasks and have them run to completion

            stopwatch.Stop();
            Console.WriteLine($"Finished processing in {stopwatch.ElapsedMilliseconds} milliseconds. Press a key to end.");
            Console.ReadKey();
        }

        public static void DoWork()
        {
            Console.WriteLine("Work starting");
            Thread.Sleep(2000);
            Console.WriteLine("Work finished");
        }
        */

        /*
        // Task returning a value
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Task<int> task = Task.Run(() =>
            {
                return CalculateResult();
            });

            Console.WriteLine(task.Result);

            stopwatch.Stop();
            Console.WriteLine($"Finished processing in {stopwatch.ElapsedMilliseconds} milliseconds. Press a key to end.");
            Console.ReadKey();
        }
        // COMMENT:
        // The Task.Run method uses the TaskFactory.StartNew method to create and start the task, using the default task scheduler
        // that uses the .NET Framework thread pool. The Task class exposes a Factory property that refers to the default task scheduler.

        public static int CalculateResult()
        {
            Console.WriteLine("Work starting");
            Thread.Sleep(2000);
            Console.WriteLine("Work finished");
            return 99;
        }
        */

        /*
        // Task WaitAll
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Task[] Tasks = new Task[10];

            for (int i = 0; i < 10; i++)
            {
                int taskNum = i;
                Tasks[i] = Task.Run(() => DoWork(taskNum));
            }

            Task.WaitAll(Tasks);
         
            stopwatch.Stop();
            Console.WriteLine($"Finished processing in {stopwatch.ElapsedMilliseconds} milliseconds. Press a key to end.");
            Console.ReadKey();
        }

        public static void DoWork(int i)
        {
            Console.WriteLine("Task {0} starting", i);
            Thread.Sleep(2000);
            Console.WriteLine("Task {0} finished", i);
        }
        */

        /*
        // Continuation tasks
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Thread.Sleep(1000);
            Task task = Task.Run(() => HelloTask());

            Thread.Sleep(1000);
            task.ContinueWith((prevTask) => WorldTask());

            Thread.Sleep(1000);
            stopwatch.Stop();
            Console.WriteLine($"Finished processing in {stopwatch.ElapsedMilliseconds} milliseconds. Press a key to end.");
            Console.ReadKey();
        }

        public static void HelloTask()
        {
            //Thread.Sleep(1000);
            Console.WriteLine("Hello");
        }

        public static void WorldTask()
        {
            //Thread.Sleep(1000);
            Console.WriteLine("World");
        }
        */

        /*
        // Continuation options
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Task task = Task.Run(() => HelloTask());

            task.ContinueWith((prevTask) => WorldTask(), TaskContinuationOptions.OnlyOnRanToCompletion);

            task.ContinueWith((prevTask) => ExceptionTask(), TaskContinuationOptions.OnlyOnFaulted);

            stopwatch.Stop();
            Console.WriteLine($"Finished processing in {stopwatch.ElapsedMilliseconds} milliseconds. Press a key to end.");
            Console.ReadKey();
        }

        private static void ExceptionTask()
        {
            throw new NotImplementedException();
        }

        public static void HelloTask()
        {
            Console.WriteLine("Hello");
        }

        public static void WorldTask()
        {
            Console.WriteLine("World");
        }
        */


        // CHILD TASKS

        /*
        // Attached child tasks
        public static void DoChild(object state)
        {
            Console.WriteLine("Child {0} starting (time: {1} milliseconds)", state, stopwatch.ElapsedMilliseconds);
            Thread.Sleep(2000);
            Console.WriteLine("Child {0} finished (time: {1} milliseconds)", state, stopwatch.ElapsedMilliseconds);
        }
        
        static void Main(string[] args)
        {         
            stopwatch.Start();

            var parent = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Parent starts");
                for (int i = 0; i < 10; i++)
                {
                    int taskNo = i;
                    Task.Factory.StartNew(
                        (x) => DoChild(x),  // lambda expression 
                               taskNo,      // state object
                               TaskCreationOptions.AttachedToParent);
                }
               
            });

            parent.Wait();   // will wait for all the attached children to complete

            stopwatch.Stop();
            Console.WriteLine($"Finished processing in {stopwatch.ElapsedMilliseconds} milliseconds. Press a key to end.");
            Console.ReadKey();
        }

        public static Stopwatch stopwatch = new Stopwatch();
        */


        // THREADS AND THREADPOOL

        /*
        // Creating threads
        static void Main(string[] args)
        {
            Thread thread = new Thread(ThreadHello);
            thread.Start();
        }

        public static void ThreadHello()
        {
            Console.WriteLine("Hello from the thread");
            Thread.Sleep(2000);
        }
        */

        /*
        // Creating threads, using ThreadStart
        static void Main(string[] args)
        {
            ThreadStart ts = new ThreadStart(ThreadHello);
            Thread thread = new Thread(ts);
            thread.Start();
        }

        public static void ThreadHello()
        {
            Console.WriteLine("Hello from the thread");
            Thread.Sleep(2000);
        }
        */

        //// Threads and lambda expressions
        //static void Main(string[] args)
        //{
        //    Thread thread = new Thread(() =>
        //    {
        //        Console.WriteLine("Hello from the thread");
        //        Thread.Sleep(1000);
        //    });
        //    thread.Start();
        //    Console.WriteLine("Press a key to end.");
        //    Console.ReadKey();
        //}

        /*
        // ParameterizedThreadStart, plus added solution for ending the program after thread end
        static void Main(string[] args)
        {
            ParameterizedThreadStart ps = new ParameterizedThreadStart(WorkOnData);
            Thread thread = new Thread(ps);
            thread.Start(66);
            Thread.Sleep(4000);
            HasEnded(thread);
        }

        public static void WorkOnData(object data)
        {
            Console.WriteLine("Working on: {0}", data);
            Thread.Sleep(1000);          
        }

        public static bool HasEnded(Thread thread)
        {
            if (!thread.IsAlive)
            {
                Console.WriteLine("Press a key to end.");
                Console.ReadKey();
                return true;
            }      
            return false;
        }
        */

        //// ParameterizedThreadStart, plus added solution for ending the program after thread end
        //static void Main(string[] args)
        //{
        //    ParameterizedThreadStart ps = new ParameterizedThreadStart(WorkOnData);
        //    Thread thread = new Thread(ps);
        //    thread.Start(66);

        //    while (thread.IsAlive)
        //    {
        //        // Wait until thread ends
        //    }

        //    Console.WriteLine("Press a key to end.");
        //    Console.ReadKey();
        //}

        //public static void WorkOnData(object data)
        //{
        //    Console.WriteLine("Working on: {0}", data);
        //    Thread.Sleep(1000);
        //}

        /*
        // New thread, using lambda
        static void Main(string[] args)
        {
            Thread thread = new Thread((data) =>
            {
                WorkOnData(data);
            });
            thread.Start(99);
        }

        public static void WorkOnData(object data)
        {
            Console.WriteLine("Working on: {0}", data);
            Thread.Sleep(1000);
        }
        */

        /*
        // Aborting a thread (COMMENT: this method gives an error)
        static void Main(string[] args)
        {
            Thread tickThread = new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine("Tick");
                    Thread.Sleep(1000);
                }
            });

            tickThread.Start();

            Console.WriteLine("Press a key to stop the clock");
            Console.ReadKey();
            tickThread.Abort();
            Console.WriteLine("Press a key to exit");
            Console.ReadKey();
        }
        */

        /*
        // Aborting a thread
        static bool tickRunning; // flag variable

        static void Main(string[] args)
        {
            tickRunning = true;
            
            Thread tickThread = new Thread(() =>
            {
                while (tickRunning)
                {
                    Console.WriteLine("Tick");
                    Thread.Sleep(1000);
                }
            });

            Console.WriteLine("Press a key to stop the clock");
            tickThread.Start();
            Console.ReadKey();
            tickRunning = false;
            Console.WriteLine("Press a key to exit");
            Console.ReadKey();
        }
        */

        /*
        // Using join
        static void Main(string[] args)
        {
            Thread threadToWaitFor = new Thread(() =>
            {
                Console.WriteLine("Thread starting");
                Thread.Sleep(2000);
                Console.WriteLine("Thread done");
            });

            threadToWaitFor.Start();
            //Console.WriteLine("Joining thread");
            threadToWaitFor.Join(); // Simple method to wait until the thread is finished
            Console.WriteLine("Press a key to exit");
            Console.ReadKey();
        }
        */

        // THREAD DATA STORA AND ThreadLocal

        /*
        // ThreadLocal
        public static ThreadLocal<Random> RandomGenerator =
            new ThreadLocal<Random>(() =>
            {
                //return new Random(2);
                return new Random();
            });

        static void Main(string[] args)
        {
            Thread t1 = new Thread(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("t1: {0}", RandomGenerator.Value.Next(100));
                    Thread.Sleep(500);
                }
            });

            Thread t2 = new Thread(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("t2: {0}", RandomGenerator.Value.Next(100));
                    Thread.Sleep(500);
                }
            });

            t1.Start();
            t2.Start();
            Console.ReadKey();
        }
        */


        //// Thread context
        //static void DisplayThread(Thread t)
        //{
        //    Console.WriteLine("Name: {0}", t.Name);
        //    Console.WriteLine("Culture: {0}", t.CurrentCulture);
        //    Console.WriteLine("Priority: {0}", t.Priority);
        //    Console.WriteLine("Context: {0}", t.ExecutionContext);
        //    Console.WriteLine("IsBackground?: {0}", t.IsBackground);
        //    Console.WriteLine("IsPool?: {0}", t.IsThreadPoolThread);
        //}

        //static void Main(string[] args)
        //{
        //    Thread.CurrentThread.Name = "Main method";
        //    DisplayThread(Thread.CurrentThread);
        //}

        /*
        // Thread pool
        static void DoWork(object state)
        {
            Console.WriteLine("Doing work: {0}", state);
            Thread.Sleep(2000);
            Console.WriteLine("Work finished: {0}", state);
        }

        static void Main(string[] args)
        {
            for (int i = 1; i <= 50; i++)
            {
                int stateNumber = i;
                ThreadPool.QueueUserWorkItem(state => DoWork(stateNumber));
            }
            Console.ReadKey();
        }
        // COMMENT:
        // A program that creates a large number of individual threads can easily overwhelm a device.
        // However, this does not happen if a ThreadPool is used. The extra threads are placed in the queue.
        */


        /*
        // Using BlockingCollection
        static void Main(string[] args)
        {
            // Blocking collection that can hold 5 items
            BlockingCollection<int> data = new BlockingCollection<int>(5);

            Task.Run(() =>
            {
                // attempt to add 10 items to the collection - blocks after 5th 
                for (int i = 0; i < 10; i++)
                {
                    data.Add(i);
                    Console.WriteLine("Data {0} added successfully.", i);
                }
                // indicate we have no more to add
                data.CompleteAdding();
            });

            Console.ReadKey();
            Console.WriteLine("Reading collection");

            Task.Run(() =>
            {
                while (!data.IsCompleted)
                {
                    try
                    {
                        int v = data.Take();
                        Console.WriteLine("Data {0} taken successfully.", v);
                    }
                    catch (InvalidOperationException) { }
                }
            });

            Console.ReadKey();
        }
        */

        /*
        // Using Block ConcurrentStack (works on the basis of LIFO, "last in first out")
        static void Main(string[] args)
        {
            // Blocking collection that can hold 5 items
            BlockingCollection<int> data = new BlockingCollection<int>(new ConcurrentStack<int>(), 5);

            Task.Run(() =>
            {
                // attempt to add 10 items to the collection - blocks after 5th 
                for (int i = 0; i < 10; i++)
                {
                    data.Add(i);
                    Console.WriteLine("Data {0} added successfully.", i);
                }
                // indicate we have no more to add
                data.CompleteAdding();
            });

            Console.ReadKey();
            Console.WriteLine("Reading collection");

            Task.Run(() =>
            {
                while (!data.IsCompleted)
                {
                    try
                    {
                        int v = data.Take();
                        Console.WriteLine("Data {0} taken successfully.", v);
                    }
                    catch (InvalidOperationException) { }
                }
            });

            Console.ReadKey();
        }
        */


        //// Concurrent queue
        //static void Main(string[] args)
        //{
        //    ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
        //    queue.Enqueue("Bart");
        //    queue.Enqueue("van Meerkerk");
        //    string str;
        //    if (queue.TryPeek(out str))
        //        Console.WriteLine("Peek: {0}", str);
        //    if (queue.TryDequeue(out str))
        //        Console.WriteLine("Dequeue: {0}", str);
        //}

        //// Concurrent stack
        //static void Main(string[] args)
        //{
        //    ConcurrentStack<string> stack = new ConcurrentStack<string>();
        //    stack.Push("Bart");
        //    stack.Push("van Meerkerk");
        //    string str;
        //    if (stack.TryPeek(out str))
        //        Console.WriteLine("Peek: {0}", str);
        //    if (stack.TryPop(out str))
        //        Console.WriteLine("Pop: {0}", str);
        //    Console.ReadKey();
        //}

        //// Concurrent bag
        //static void Main(string[] args)
        //{
        //    ConcurrentBag<string> bag = new ConcurrentBag<string>();
        //    bag.Add("Bart");
        //    bag.Add("van Meerkerk");
        //    bag.Add("Hull");
        //    string str;
        //    if (bag.TryPeek(out str))
        //        Console.WriteLine("Peek: {0}", str);
        //    if (bag.TryTake(out str))
        //        Console.WriteLine("Take: {0}", str);
        //    Console.ReadKey();
        //}

        /*
        // Concurrent Dictionary
        static void Main(string[] args)
        {
            ConcurrentDictionary<string, int> ages = new ConcurrentDictionary<string, int>();
            if (ages.TryAdd("Rob", 21))
                Console.WriteLine("Rob added successfully.");
            Console.WriteLine("Rob's age: {0}", ages["Rob"]);
            // Set Rob's age to 22 if it is 21
            if (ages.TryUpdate("Rob", 22, 21))
                Console.WriteLine("Age updated successfully.");
            Console.WriteLine("Rob's new age: {0}", ages["Rob"]);
            // Increment Rob's age atomically using factory method
            Console.WriteLine("Rob's age updated to {0}", ages.AddOrUpdate("Rob", 1, (name, age) => age = age + 1));
            Console.WriteLine("Rob's new age: {0}", ages["Rob"]);
        }
        */
    }
}
