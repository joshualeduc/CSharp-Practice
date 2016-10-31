using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades
{
    public class GradeBook : GradeTracker
    {
        public GradeBook()
        {
            grades = new List<float>(); //floats are dumb, us ints for whole numbers or doubles for decimal
            _name = "Empty";
        }

        public override GradeStatistics ComputeStatistics() //(virtual keyword changed to override since we are now inheriting from GradeTracker)virtual keyword needed for polymorphism ie ThrowAwayGradeBook class can use this method, must be paired with override
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

        public override void WriteGrades(TextWriter destination) //(parameter originally @out, @ is used to write a parament using a c# keyword @out, @class, etc
        {
            for (int i = 0; i < grades.Count; i++) //.Count is C#'s .length for arrays
            {
                destination.WriteLine(grades[i]);
            }
        }

        public override void AddGrade(float grade) //functions without a return statement need to be defined with the void keyword
        {
            grades.Add(grade);
        }

        public override IEnumerator GetEnumerator()
        {
            return grades.GetEnumerator();
        }


        protected List<float> grades; //List is a resizable array (like js arrays) --- protected allows access from the class its written in or an inherited class 
    }
}
