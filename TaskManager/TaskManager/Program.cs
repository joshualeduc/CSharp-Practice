using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    class Program
    {
        static string myPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "todo.txt");
        static void Main(string[] args)
        {
            TaskList workingList = new TaskList();
            Greet(workingList);
            do
            {
                ListenForCommands(workingList);
            } while (workingList.running);
            ExitProgram(workingList);
        }

        static void Greet(TaskList taskList)
        {
            Console.WriteLine("Hello, welcome to your task list.");
            taskList.initialList = System.IO.File.ReadLines(myPath);
        }

        static void ListenForCommands(TaskList taskList)
        {
            string userInput = Console.ReadLine();
            taskList.commandParser(userInput);
        }

        static void ExitProgram(TaskList taskList)
        {
            Console.WriteLine("See you next time.");
            System.IO.File.WriteAllText(myPath, taskList.FormattedList);
            System.Threading.Thread.Sleep(1500);
        }
    }
}
