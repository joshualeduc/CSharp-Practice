using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    public class TaskList
    {
        public TaskList()
        {
            tasks = new List<string>();
            running = true;
        }

        protected List<string> tasks;

        public string task { get; set; }

        public bool running { get; set; }

        public string FormattedList
        {
            get
            {
                string myList = String.Empty;
                foreach (string task in tasks)
                {
                    myList += task + Environment.NewLine;
                }
                return myList;
            }
        }

        public IEnumerable<string> initialList
        {
            set
            {
                foreach (string item in value)
                {
                    tasks.Add(item);
                }
            }
        }

        public void add(string taskName)
        {
            tasks.Add(taskName);
            Console.WriteLine($"Added {taskName}{Environment.NewLine}");
        }

        public void done(int input)
        {
            var index = input - 1;
            if(index > tasks.Count || index < 0) {
                Console.WriteLine($"Please type the number of an existing task.{Environment.NewLine}");
                return;
            }

            string taskText = tasks[index];
            tasks.RemoveAt(index);
            Console.WriteLine($"Deleted: {taskText}{Environment.NewLine}");
        }

        public void ls()
        {
            for(int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {tasks[i]}");
            }
            Console.WriteLine($"");
        }

        public void exit()
        {
            running = false;
        }

        public void commandParser(string userInput)
        {
            int i = userInput.IndexOf(" ");
            string command;
            string input;
            if(i != -1)
            {
                command = userInput.Substring(0, i);
                input = userInput.Substring(i + 1);
            }
            else
            {
                command = userInput;
                input = String.Empty;
            }

            switch (command)
            {
                case "add":
                    add(input);
                    break;
                case "done":
                    done(Int32.Parse(input));
                    break;
                case "ls":
                    ls();
                    break;
                case "exit":
                    exit();
                    break;
                default:
                    Console.WriteLine("Please use the commands 'add', 'done', or 'ls'.");
                    break;
            }
        }
    }
}
