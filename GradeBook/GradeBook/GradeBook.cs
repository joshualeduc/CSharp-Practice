using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades
{
    public class GradeBook
    {
        public GradeBook()
        {
            grades = new List<float>(); //floats are dumb, us ints for whole numbers or doubles for decimal
            _name = "Empty";
        }

        public GradeStatistics ComputeStatistics()
        {
            GradeStatistics stats = new GradeStatistics();

            float sum = 0;
            foreach (float grade in grades)
            {
                stats.HighestGrade = Math.Max(grade, stats.HighestGrade);
                stats.LowestGrade = Math.Min(grade, stats.LowestGrade);
                sum += grade;
            }
            stats.AverageGrade = sum / grades.Count;
            return stats;
        }

        public void WriteGrades(TextWriter destination) //(parameter originally @out, @ is used to write a parament using a c# keyword @out, @class, etc
        {
            for (int i = 0; i < grades.Count; i++) //.Count is C#'s .length for arrays
            {
                destination.WriteLine(grades[i]);
            }
        }

        public void AddGrade(float grade) //functions without a return statement need to be defined with the void keyword
        {
            grades.Add(grade);
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name cannot be null or empty"); //Overriding basic error message with my custom string
                }

                if (_name != value)
                {
                    NameChangedEventArgs args = new NameChangedEventArgs();
                    args.ExistingName = _name;
                    args.NewName = value;


                    NameChanged(this, args);
                }

                _name = value;

            }
        }

        public event NameChangedDelegate NameChanged; //keyword event forces subscriptions to be assigned as += or -=, prevents = null
        private string _name;
        private List<float> grades; //List is a resizable array (like js arrays)
    }
}
