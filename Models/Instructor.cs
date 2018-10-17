namespace TrackinggStudentExercises.Models
{
    public class Instructor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SlackHandle { get; set; }
        public Cohort Cohort { get; set; }

        public void AssignExercise(Exercise exercise, Student student) {
            student.Exercises.Add(exercise);
        }
     }
}