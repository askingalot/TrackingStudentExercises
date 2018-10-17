using System;
using System.Collections.Generic;
using System.Linq;
using TrackingStudentExercises.Models;
using Dapper;

namespace TrackingStudentExercises {
    class Program {
        static void Main (string[] args) {
            var conn = DatabaseInterface.Connection;

            conn.Execute("delete from exercise where name = 'Ex 5'");
            conn.Execute("insert into exercise (name, language) values ('Ex 5', 'csharp')");

            List<Exercise> exercises = conn.Query<Exercise>("select * from exercise").ToList();
            /*foreach(Exercise exercise in exercises) {
                Console.WriteLine($"{exercise.Name} {exercise.Language}");
            }
            */

            conn.Execute("delete from instructor where slackhandle = 'inst4'");
            conn.Execute(@"insert into instructor (firstname, lastname, slackhandle, cohortid) 
                           values ('InstFirst4', 'InstLast4', 'inst4', 1)");

            List<Instructor> instructors = conn.Query<Instructor, Cohort, Instructor>(
                @"select i.id,
                         i.FirstName,
                         i.LastName,
                         i.SlackHandle,
                         c.id,
                         c.Name
                    from instructor i join cohort c on i.cohortId = c.id
                ",
                (instructor, cohort) => {
                    instructor.Cohort = cohort;
                    return instructor;
                }).ToList();

/* 
            foreach(Instructor instructor in instructors) {
                Console.WriteLine($"{instructor.FirstName} {instructor.LastName} is in {instructor.Cohort.Name}");
            }
*/

            Dictionary<int, Student> uniqueStudents = new Dictionary<int, Student>();

            List<Student> students = conn.Query<Student, Cohort, Exercise, Student>(
                @"select s.id,
                         s.FirstName,
                         s.LastName,
                         s.SlackHandle,
                         c.id,
                         c.Name,
                         e.id,
                         e.Name,
                         e.language
                    from student s join cohort c on s.cohortId = c.id
                         left join studentexercises se on se.studentid = s.id
                         left join exercise e on se.exerciseid = e.id
                ", (student, cohort, exercise) => {
                    if (! uniqueStudents.ContainsKey(student.Id)) {
                        student.Exercises = new List<Exercise>();
                        student.Cohort = cohort;
                        uniqueStudents.Add(student.Id, student);
                    } 
                    uniqueStudents[student.Id].Exercises.Add(exercise);
                    return uniqueStudents[student.Id];
                }
            ).ToList();

            foreach (Student student in uniqueStudents.Values) {
                Console.WriteLine(
                    $"{student.FirstName} in {student.Cohort.Name} is working on " + 
                    $"{string.Join(", ", student.Exercises.Select(e => e.Name))}");
            }
        }


        static void EarlierExercises() {
            List<Exercise> exercises = new List<Exercise> {
                new Exercise () { Name = "one", Language = "C#" },
                new Exercise () { Name = "two", Language = "Python" },
                new Exercise () { Name = "three", Language = "JavaScript" },
                new Exercise () { Name = "four", Language = "C#" },
            };

            List<Cohort> cohorts = new List<Cohort> {
                new Cohort () { Name = "Day 1" },
                new Cohort () { Name = "Day 2" },
                new Cohort () { Name = "Evening 3" },
                new Cohort () { Name = "Evening 4" },
            };

            List<Student> students = new List<Student> {
                new Student () {
                    FirstName = "First1", LastName = "Last1", SlackHandle = "slack1",
                    Cohort = cohorts[0]
                },
                new Student () {
                    FirstName = "First2", LastName = "Last2", SlackHandle = "slack2",
                    Cohort = cohorts[1]
                },
                new Student () {
                    FirstName = "First3", LastName = "Last3", SlackHandle = "slack3",
                    Cohort = cohorts[2]
                },
                new Student () {
                    FirstName = "First4", LastName = "Last4", SlackHandle = "slack4",
                    Cohort = cohorts[3]
                },
            };
            cohorts[0].Students.Add (students[0]);
            cohorts[1].Students.Add (students[1]);
            cohorts[2].Students.Add (students[2]);
            cohorts[3].Students.Add (students[3]);

            Instructor instructor1 = new Instructor () {
                FirstName = "First1", LastName = "Last1", SlackHandle = "slack1",
                Cohort = cohorts[0]
            };
            cohorts[0].Instructors.Add (instructor1);

            Instructor instructor2 = new Instructor () {
                FirstName = "First2", LastName = "Last2", SlackHandle = "slack2",
                Cohort = cohorts[1]
            };
            cohorts[1].Instructors.Add (instructor1);

            Instructor instructor3 = new Instructor () {
                FirstName = "First3", LastName = "Last3", SlackHandle = "slack3",
                Cohort = cohorts[2]
            };
            cohorts[2].Instructors.Add (instructor1);

            Instructor instructor4 = new Instructor () {
                FirstName = "First4", LastName = "Last4", SlackHandle = "slack4",
                Cohort = cohorts[3]
            };
            cohorts[3].Instructors.Add (instructor1);

            List<Instructor> instructors = new List<Instructor> () {
                instructor1,
                instructor2,
                instructor3,
                instructor4
            };

            foreach (Student student in students) {
                foreach (Instructor instructor in instructors) {
                    instructor.AssignExercise (exercises[0], student);
                    instructor.AssignExercise (exercises[1], student);
                }
            }

            // #1
            List<Exercise> jsExercises = exercises.Where(e => e.Language == "JavaScript").ToList();

            // 2 - 4 didn't seem worth it

            // #5 
            List<Student> notWorking = students.Where(s => !s.Exercises.Any()).ToList();

            // #6
            Student hardestWorker = students.OrderByDescending(s => s.Exercises.Count).First();

            var report = cohorts.Select(c => new { CohortName = c.Name, Count = c.Students.Count } ).ToList();
            foreach (var entry in report) {
                Console.WriteLine($"{entry.CohortName}: {entry.Count}");
            }
        }
    }
}