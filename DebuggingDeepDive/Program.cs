using System;
using System.Collections.Generic;

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
            var rnd = new Random();
            
            Console.Write("1/3: Creating a list of string items ...");
            var stringList = new List<string>(StringElements);
            for (var i = 0; i < StringElements; i++)
                stringList.Add($"Element: {i:D4}");
            Console.WriteLine(" {0:N} done!", stringList.Count);

            Console.ReadKey();

            Console.Write("2/3: Creating a list of object items ...");
            var objList = new List<SomeClass>(StringElements);
            for (var i = 0; i < ClassElements; i++)
                objList.Add(new SomeClass { OneString = $"Element: {i:D4}" });
            Console.WriteLine(" {0:N} done!", objList.Count);

            Console.ReadKey();

            Console.Write("3/3: Creating an array of random bytes ...");
            var byteArray = new byte[ByteElements];
            rnd.NextBytes(byteArray);
            Console.WriteLine(" {0:N} done!", byteArray.Length);

            Console.ReadKey();
        }
    }

    public class SomeClass
    {
        public string OneString { get; set; }
    }
}
