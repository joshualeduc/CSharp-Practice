using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskList workingList = new TaskList();
            bool running = true;
            Greet(workingList);
            do
            {
                ListenForCommands(workingList);
            } while (running);
            //ExitProgram();
        }

        static void Greet(TaskList taskList)
        {
            Console.WriteLine("Hello, welcome to your task list.");
        }

        static void ListenForCommands(TaskList taskList)
        {
            string userInput = Console.ReadLine();
            taskList.commandParser(userInput);
        }

        //static void ExitProgram()
        //{

        //}
    }
}
