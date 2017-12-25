using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Helper.Configuration;
using Helper.Optimization;
using Helper.Text;

namespace Helper.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            DemoBenchmarkTimer();
            Console.WriteLine();
            ConsoleHelper.Pause();
            Console.WriteLine();
            DemoEventHandlerHelperRaise();
            Console.WriteLine();
            ConsoleHelper.Pause();
            Console.WriteLine();
            // DemoReadConfigurationsFromIniFile("C:\\temp\\test.ini");
            // Console.WriteLine();
            // ConsoleHelper.Pause();
            // Console.WriteLine();
            DemoBuildingFixedFieldStringFromGivenRawStrings();
            Console.WriteLine();
            ConsoleHelper.Pause();
            Console.WriteLine();
            DemoBuildingStringFromGivenFixedLengthFieldValues();
            Console.WriteLine();
            ConsoleHelper.Pause();
            Console.WriteLine();
            DemoChangingFieldValueByUsingIndexerProperty();
            Console.WriteLine();
            ConsoleHelper.Pause();
        }

        private static void DemoBenchmarkTimer()
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("Demonstrating how to use BenchmarkTimer class");
            Console.WriteLine("=============================================");

            Console.WriteLine();
            const int benchmarkCycles = 100000000;
            SimpleBenchmarking.Benchmark("List<Int32>", () =>
            {
                List<Int32> l = new List<Int32>();
                for (int i = 0; i < benchmarkCycles; i++)
                {
                    l.Add(i);
                    int x = l[i];
                }
                l = null;
            });
            SimpleBenchmarking.Benchmark("List<object> of Int32", () =>
            {
                List<object> l = new List<object>();
                for (int i = 0; i < benchmarkCycles; i++)
                {
                    l.Add(i);
                    int x = (int)l[i];
                }
                l = null;
            });
            SimpleBenchmarking.Benchmark("List<string>", () =>
            {
                List<string> l = new List<string>();
                for (int i = 0; i < benchmarkCycles; i++)
                {
                    l.Add("X");
                    string s = l[i];
                }
                l = null;
            });
            SimpleBenchmarking.Benchmark("List<object> of string", () =>
            {
                List<object> l = new List<object>();
                for (int i = 0; i < benchmarkCycles; i++)
                {
                    l.Add("X");
                    string s = (string)l[i];
                }
                l = null;
            });
        }

        private static void DemoEventHandlerHelperRaise()
        {
            Console.WriteLine("========================================================");
            Console.WriteLine("Demonstrating how to use EventHandlerHelper.Raise method");
            Console.WriteLine("========================================================");

            Console.WriteLine();
            Car car = new Car("AB1234");
            car.Started += CarStarted;
            car.Start();
            car.Started -= CarStarted;
        }

        private static void CarStarted(object sender, CarStartedEventArgs e)
        {
            Console.WriteLine($"Car {e.LicenseNumber} is started.");
        }

        private static void DemoReadConfigurationsFromIniFile(string path)
        {
            Console.WriteLine("=====================================================");
            Console.WriteLine("Reading all configurations from a section in INI file");
            Console.WriteLine("=====================================================");

            IniFile ini = new IniFile(path);
            Console.WriteLine();
            Console.WriteLine("INI parameters and values");
            Console.WriteLine("-------------------------");
            foreach (var kvp in ini.ReadAllKeysAndValues<string>("General"))
                Console.WriteLine(kvp.Key + ", " + kvp.Value);
        }

        private static void DemoBuildingFixedFieldStringFromGivenRawStrings()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("Building fixed-field-string from given raw strings");
            Console.WriteLine("==================================================");

            Console.WriteLine();
            Console.WriteLine("Input Raw Strings");
            Console.WriteLine("-----------------");
            string[] rows = new string[] {
                "A123456(7)CHAN TAI MAN        10015032017",
                "B319191(9)LAM CHI CHUNG       32 23052017"
            };
            foreach (string row in rows)
                Console.WriteLine(row);

            Console.WriteLine();
            Console.WriteLine("Output Fields");
            Console.WriteLine("-------------");
            Stopwatch sw =new Stopwatch();
            sw.Start();
            FixedLengthFieldCollection fields = new FixedLengthFieldCollection() {
                new FixedLengthStringField("HKID", 10),
                new FixedLengthStringField("Name", 20),
                new FixedLengthInt32Field("Age", 3),
                //new FixedLengthField<DateTime>("DOB", 8) { DateTimeFormatString = "ddMMyyyy" }
                new FixedLengthDateTimeField("DOB", "ddMMyyyy")
            };

            foreach (string row in rows)
            {
                FixedLengthFieldString fixedStr = new FixedLengthFieldString(row, fields);
                foreach (IFixedLengthField field in fixedStr.Fields)
                    Console.WriteLine(field.Name + ": |" + field.Value + "|");
            }
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine($"Elapsed time: {sw.Elapsed}.");
        }

        private static void DemoBuildingStringFromGivenFixedLengthFieldValues()
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("Building string from given fixed-length-fields");
            Console.WriteLine("==============================================");

            Console.WriteLine();
            Console.WriteLine("Input Fields");
            Console.WriteLine("------------");
            Stopwatch sw =new Stopwatch();
            sw.Start();
            FixedLengthFieldCollection fields = new FixedLengthFieldCollection() {
                new FixedLengthStringField("UID", 22) { Value = "2017052200410030001100" },
                new FixedLengthInt64Field("Amount", 13) { PaddingCharPosition = PaddingCharPosition.Left, PaddingChar = '0', Value = 1234 },
                new FixedLengthDateTimeField("ChequeDate", 20) 
                {
                    PaddingCharPosition = PaddingCharPosition.Left, 
                    Value = DateTime.ParseExact("20170301", "yyyyMMdd", CultureInfo.InvariantCulture) 
                }
            };
            foreach (var field in fields)
                Console.WriteLine(field.Name + ": |" + field.Value + "|");

            Console.WriteLine();
            Console.WriteLine("Output String");
            Console.WriteLine("-------------");
            FixedLengthFieldString fixedStr = new FixedLengthFieldString(fields);
            sw.Stop();
            Console.WriteLine(fixedStr);
            Console.WriteLine();
            Console.WriteLine($"Elapsed time: {sw.Elapsed}.");
        }

        private static void DemoChangingFieldValueByUsingIndexerProperty()
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("Changing Field Value By Using Indexer Property");
            Console.WriteLine("==============================================");

            Console.WriteLine();
            Console.WriteLine("Before Updating Field Value");
            Console.WriteLine("---------------------------");
            Stopwatch sw =new Stopwatch();
            FixedLengthFieldCollection fields = new FixedLengthFieldCollection() {
                new FixedLengthStringField("Staff ID", 10) { Value = "AB223345" },
                new FixedLengthStringField("Name", 20) { Value = "CHAN TAI MAN" },
                new FixedLengthInt64Field("Salary", 13) { PaddingCharPosition = PaddingCharPosition.Left, PaddingChar = '0', Value = 1000000 }
            };
            foreach (var field in fields)
                Console.WriteLine(field.Name + ": |" + field.Value + "|");
            Console.WriteLine("Raw String: " + new FixedLengthFieldString(fields));

            sw.Start();
            fields["Salary"] = 1300000;
            sw.Stop();

            Console.WriteLine();
            Console.WriteLine("After Updating Field Value");
            Console.WriteLine("---------------------------");
            foreach (var field in fields)
                Console.WriteLine(field.Name + ": |" + field.Value + "|");
            Console.WriteLine("Raw String: " + new FixedLengthFieldString(fields));
            Console.WriteLine();
            Console.WriteLine($"Elapsed time: {sw.Elapsed}.");
        }
    }
}
