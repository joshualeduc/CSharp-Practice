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
            tasks = new Dictionary<int, string>();
            running = true;
        }

        public bool running;

        protected Dictionary<int, string> tasks;

        public string FormattedList
        {
            get
            {
                string myList = String.Empty;
                foreach (var task in tasks)
                {
                    myList += task.Value + Environment.NewLine;
                }
                return myList;
            }
        }

        public IEnumerable<string> InitialList
        {
            set
            {
                int counter = 1;
                foreach (string item in value)
                {
                    tasks.Add(counter, item);
                    counter++;
                }
            }
        }

        public void commandParser(string userInput)
        {
            int i = userInput.IndexOf(" ");
            string command;
            string input;
            string[] splitInput;

            if (i != -1)
            {
                command = userInput.Substring(0, i);
                input = userInput.Substring(i + 1);
                splitInput = input.Split(null);
            }
            else
            {
                command = userInput;
                input = null;
                splitInput = null;
            }

            switch (command)
            {
                case "add":
                    add(input);
                    break;
                case "pri":
                    prioritize(input);
                    break;
                case "done":
                    done(Int32.Parse(input));
                    break;
                case "ls":
                    ls(splitInput);
                    break;
                case "exit":
                    exit();
                    break;
                default:
                    Console.WriteLine("Please use the commands 'add', 'pri', 'done', 'ls', or 'exit'.");
                    break;
            }
        }

        private void add(string taskName)
        {
            for(int i = 1; i < 1000; i++)
            {
                if (!tasks.ContainsKey(i))
                {
                    tasks.Add(i, taskName);
                    break;
                }
            }
            Console.WriteLine($"Added {taskName}");
        }

        private void prioritize(string userInput)
        {
            int i = userInput.IndexOf(" ");
            int taskKey = Int32.Parse(userInput.Substring(0, i));
            char priority = userInput[i + 1];

            if (tasks[taskKey].StartsWith("[A]") || tasks[taskKey].StartsWith("[B]") || tasks[taskKey].StartsWith("[C]"))
            {
                tasks[taskKey] = tasks[taskKey].Substring(4);
            }

            switch(priority)
            {
                case 'a':
                case 'A':
                    tasks[taskKey] = $"[A] {tasks[taskKey]}";
                    Console.WriteLine($"High importance added to: {tasks[taskKey]}");
                    break;
                case 'b':
                case 'B':
                    tasks[taskKey] = $"[B] {tasks[taskKey]}";
                    Console.WriteLine($"Medium importance added to: {tasks[taskKey]}");
                    break;
                case 'c':
                case 'C':
                    tasks[taskKey] = $"[C] {tasks[taskKey]}";
                    Console.WriteLine($"Low importance added to: {tasks[taskKey]}");
                    break;
                default:
                    Console.WriteLine("Please type 'pri # letter' using either a, b, or c.");
                    break;
            }
        }

        private void done(int input)
        {
            if(!tasks.ContainsKey(input)) 
            {
                Console.WriteLine($"Please type the number of an existing task.");
                return;
            }

            string taskText = tasks[input];
            tasks.Remove(input);
            Console.WriteLine($"Deleted: {taskText}");
        }

        private void ls(string[] searchTerms)
        {
            Dictionary<int, string> listToRead = tasks;

            if (searchTerms != null)
            {
                foreach (string term in searchTerms)
                {
                    listToRead = listToRead.Where(task => task.Value.Contains(term)).ToDictionary(i => i.Key, i => i.Value);
                }

            }
            readList(listToRead);
            Console.WriteLine($"What would you like to do next?");
        }

        private void exit()
        {
            running = false;
        }

        private void readList(Dictionary<int, string> myList)
        {
            foreach (var task in myList.OrderBy(key => key.Value))
            {
                string[] taskToWrite = task.Value.Split(' ');
                var defaultColor = ConsoleColor.Gray;

                if (task.Value.Contains("[A]"))
                {
                    defaultColor = ConsoleColor.Yellow;
                }
                if (task.Value.Contains("[B]"))
                {
                    defaultColor = ConsoleColor.Green;
                }
                if (task.Value.Contains("[C]"))
                {
                    defaultColor = ConsoleColor.Cyan;
                }
                Console.ForegroundColor = defaultColor;

                Console.Write(task.Key + ":");
                for(int i = 0; i < taskToWrite.Length; i++)
                {
                    if(taskToWrite[i][0] == '+')
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                    if(taskToWrite[i][0] == '@')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                    Console.Write(" " + taskToWrite[i]);
                    Console.ForegroundColor = defaultColor;
                }
                Console.Write(Environment.NewLine);
                Console.ResetColor();
            }
        }
    }
}
