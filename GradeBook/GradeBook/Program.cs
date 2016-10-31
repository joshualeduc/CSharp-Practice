using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades
{
    class Program
    {
        static void Main(string[] args) //void means the function will not return a value, but you can still use return; to exit the function
        {
            IGradeTracker book = createGradeBook();

            //book.NameChanged += OnNameChanged; not used in 2nd half of video
            GetBookName(book);
            AddGrades(book);
            SaveGrades(book);
            WriteResults(book);
        }

        private static IGradeTracker createGradeBook()
        {
            return new ThrowAwayGradeBook();
        }

        private static void WriteResults(IGradeTracker book)
        {
            GradeStatistics stats = book.ComputeStatistics();
            foreach (float grade in book)
            {
                Console.WriteLine(grade);
            }
            WriteResult("Average", stats.AverageGrade);
            WriteResult("Hieghts", stats.HighestGrade);
            WriteResult("Lowest", stats.LowestGrade);
            WriteResult(stats.Description, stats.LetterGrade); //method overloading example
        }

        private static void SaveGrades(IGradeTracker book)
        {
            using (StreamWriter outputFile = File.CreateText("grades.txt"))
            {

                book.WriteGrades(outputFile);
            }
            //outputFile.Close(); without using keyword, stream needs to be manually closed for data to appear in grades.txt
        }

        private static void AddGrades(IGradeTracker book)
        {
            book.AddGrade(91);
            book.AddGrade(89.5f); //the f is needed because the c# compiler will see that decimal and coerce it to a double automatically
            book.AddGrade(75);
        }

        private static void GetBookName(IGradeTracker book)
        {
            try
            {
                Console.WriteLine("Enter a name");
                book.Name = Console.ReadLine();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void OnNameChanged(object sender, NameChangedEventArgs args)
        {
            Console.WriteLine($"Grade book changing name from {args.ExistingName} to {args.NewName}"); //$ needed to inject variables mid string, similar to es6
        }

        static void WriteResult(string description, string result)
        {
            Console.WriteLine($"{description}: {result}");
        }

        static void WriteResult(string description, float result) //functions are defined by their signature, or the name AND parameters
        {
            Console.WriteLine($"{description}: {result:F2}");
        }
    }
}
