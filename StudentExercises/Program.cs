using System;
using StudentExercises.Models;
using StudentExercises.Repositories;
using System.Collections.Generic;

namespace StudentExercises
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repository = new Repository();

            Console.WriteLine("Get all exercises");
            Console.WriteLine("+++++++++++++++++++++++");

            List<Exercise> exercises = repository.GetAllExercises();

            foreach(Exercise exer in exercises)
            {
                Console.WriteLine($"{exer.Id} {exer.Name} {exer.Language}");
            }

            Console.WriteLine("Get all exercises with Javascript");
            Console.WriteLine("+++++++++++++++++++++++");

            List<Exercise> exercisesJava = repository.GetAllExercisesWithJava();

            foreach (Exercise exer in exercisesJava)
            {
                Console.WriteLine($"{exer.Id} {exer.Name} {exer.Language}");
            }

            //Console.WriteLine("Get all exercises with Javascript");
            //Console.WriteLine("+++++++++++++++++++++++");

            //Exercise newExercise = repository.AddExercise(new Exercise(6, "Do a thing", "Javascript"));
            //Console.WriteLine($"{newExercise.Id} {newExercise.Name} {newExercise.Language}");

            Console.WriteLine("Get all Instructors with Cohorts");
            Console.WriteLine("+++++++++++++++++++++++");

            List<Instructor> instructorsWithCohorts = repository.GetAllInstructorsWithCohorts();
            foreach (Instructor inst in instructorsWithCohorts)
            {
                Console.WriteLine($"{inst.Id}: {inst.FirstName} {inst.LastName}. {inst.SlackHandle} {inst.Specialty} Cohort: {inst.CohortId.Name}");
            }

            //Console.WriteLine("Add instructors with existing cohort.");
            //Console.WriteLine("+++++++++++++++++++++++");

            //Instructor newInstructor = repository.AddInstructorWithCohort(new Instructor(3, "Jake", "Long", "JLong", "SQL"), 1);
            //Console.WriteLine($"{newInstructor.Id}: {newInstructor.FirstName} {newInstructor.LastName}. {newInstructor.SlackHandle} {newInstructor.Specialty} assigned to existing Cohort.");

            //Console.WriteLine("Add a new Student Exercise to assign a new assignment to Cohort");
            //Console.WriteLine("+++++++++++++++++++++++");

            //repository.AssignExerciseToStudent(5, 1);
            //Console.WriteLine("New assignment added.");

            //Console.WriteLine("Get students, their cohort, and exercises.");
            //Console.WriteLine("+++++++++++++++++++++++");

            List<Student> studentsWithExercises = repository.GetAllStudentsCohortsAndExercises();
            foreach (Student stud in studentsWithExercises)
            {
                Console.WriteLine("__________________________________________________");
                Console.WriteLine($"Student:{stud.FirstName}: {stud.FirstName} {stud.LastName}. Slack: {stud.SlackHandle}");
                Console.WriteLine($"Cohort:{stud.CohortId.Name}");
                Console.WriteLine($"Assignments: {stud.Exercises.Count}");
                foreach (Exercise exer in stud.Exercises)
                {
                    Console.WriteLine($"Exercise: {exer.Name} Language: {exer.Language}");
                }

            }

        }
    }
}

