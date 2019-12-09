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


        }
    }
}

