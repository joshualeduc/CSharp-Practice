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

        public bool running;

        protected List<string> tasks;

        private string task;

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

        public IEnumerable<string> InitialList
        {
            set
            {
                foreach (string item in value)
                {
                    tasks.Add(item);
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
            tasks.Add(taskName);
            Console.WriteLine($"Added {taskName}");
        }

        private void done(int input)
        {
            var index = input - 1;
            if(index > tasks.Count || index < 0) {
                Console.WriteLine($"Please type the number of an existing task.");
                return;
            }

            string taskText = tasks[index];
            tasks.RemoveAt(index);
            Console.WriteLine($"Deleted: {taskText}");
        }

        private void ls(string[] searchTerms)
        {
            List<string> listToRead = new List<string>(tasks);
            if (searchTerms != null)
            {
                foreach (string term in searchTerms)
                {
                    listToRead = filterList(listToRead, term);
                }

            }
            readList(listToRead);
            Console.WriteLine($"What would you like to do next?");
        }

        private void exit()
        {
            running = false;
        }

        private void readList(List<string> myList)
        {
            for (int i = 0; i < myList.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {myList[i]}");
            }
        }

        private List<string> filterList(List<string> listToFilter, string searchTerm)
        {
            List<string> filteredList = new List<string>();
            foreach (string item in listToFilter)
            {
                if (item.Contains(searchTerm))
                {
                    filteredList.Add(item);
                }
            }
            return filteredList;
        }
    }
}
