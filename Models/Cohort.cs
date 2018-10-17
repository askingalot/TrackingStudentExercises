using System.Collections.Generic;

namespace TrackinggStudentExercises.Models
{
    public class Cohort
    {
        public string Name { get; set; }
        public List<Student> Students { get; set; }
        public List<Instructor> Instructors { get; set; }

        public Cohort()
        {
            Students = new List<Student>();
            Instructors = new List<Instructor>();
        }
    }
}