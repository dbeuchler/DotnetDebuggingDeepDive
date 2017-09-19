using System;
using System.Collections.Generic;
using System.Threading;

namespace DebuggingDeepDive
{
    public class Program
    {
        private const int StringElements = 1000;
        private const int ByteElements = 104857600; // ~ 100 MB
        private const int ClassElements = 1000;

        private static void Main(string[] args)
        {
            var program = new Program();
            program.Start();
        }

        private void Start()
        {
            var evaluations = new IEvaluation[]
            {
                new StringItemEvaluation(),
                new ObjectItemEvaluation(),
                new ByteArrayEvaluation(),
                new AttachedEventEvaluation(),
                new ThreadEvaluation()
            };

            foreach (var evaluation in evaluations)
            {
                evaluation.DoIt();
                Console.ReadKey();
            }
        }

        private class ThreadEvaluation : IEvaluation
        {
            private const int ThreadCount = 25;

            public void DoIt()
            {
                Console.Write("Dispatching some threads ...");

                for (int i = 0; i < 25; i++)
                {
                    ThreadPool.QueueUserWorkItem(LongMethod);
                }
                
                Console.WriteLine(" {0:N} done!", ThreadCount);
            }

            private static void LongMethod(object state)
            {
                Thread.Sleep(10000);
            }
        }

        private class StringItemEvaluation : IEvaluation
        {
            private List<string> _stringList;

            public void DoIt()
            {
                Console.Write("Creating a list of string items ...");

                _stringList = new List<string>(StringElements);

                for (var i = 0; i < StringElements; i++)
                {
                    _stringList.Add($"Element: {i:D4}");
                }

                Console.WriteLine(" {0:N} done!", _stringList.Count);
            }
        }

        private class ObjectItemEvaluation : IEvaluation
        {
            private List<SomeClass> _classList;

            public void DoIt()
            {
                Console.Write("Creating a list of object items ...");

                _classList = new List<SomeClass>(ClassElements);

                for (var i = 0; i < ClassElements; i++)
                {
                    _classList.Add(new SomeClass { OneString = $"Element: {i:D4}" });
                }

                Console.WriteLine(" {0:N} done!", _classList.Count);
            }
        }

        private class ByteArrayEvaluation : IEvaluation
        {
            private static readonly Random Random = new Random();
            private byte[] _byteArray;

            public void DoIt()
            {
                Console.Write("Creating an array of random bytes ...");

                _byteArray = new byte[ByteElements];
                Random.NextBytes(_byteArray);

                Console.WriteLine(" {0:N} done!", _byteArray.Length);
            }
        }

        private class AttachedEventEvaluation : IEvaluation
        {
            public void DoIt()
            {
                Console.Write("Creating a list of objects with attached events  ...");

                for (var i = 0; i < ClassElements; i++)
                {
                    var obj = new SomeEventClass() { OneString = $"Element: {i:D4}" };
                    SomeEvent += obj.OnSomeEvent;
                }

                Console.WriteLine(" {0:N} done!", ClassElements);
            }

            public event EventHandler SomeEvent;
        }
    }

    public interface IEvaluation
    {
        void DoIt();
    }

    public class SomeClass
    {
        public string OneString { get; set; }
    }

    public class SomeEventClass : SomeClass
    {
        public void OnSomeEvent(object sender, EventArgs eventArgs)
        {
            Console.WriteLine("Do something");
        }
    }
}