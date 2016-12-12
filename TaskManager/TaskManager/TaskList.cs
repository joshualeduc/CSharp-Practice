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
        }

        protected Dictionary<int, string> tasks;

        public string task { get; set; }

        public void add(string taskName)
        {
            int newKey = tasks.Count + 1;
            tasks.Add(newKey, taskName);
        }

        public void done(int taskKey)
        {
            tasks.Remove(taskKey);
        }

        public void ls()
        {
            foreach (KeyValuePair<int, string>task in tasks)
            {
                Console.WriteLine($"{task.Key}: {task.Value}");
            }
        }

        public void commandParser(string userInput)
        {

        }
    }
}
