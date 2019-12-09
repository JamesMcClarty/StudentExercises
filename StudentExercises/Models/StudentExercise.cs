using System;
using System.Collections.Generic;
using System.Text;

namespace StudentExercises.Models
{
    class StudentExercise
    {
        public StudentExercise (int id)
        {
            Id = id;
        }
        public StudentExercise(int id, Student student, Exercise exercise)
        {
            Id = id;
            StudentId = student;
            ExerciseId = exercise;
        }
        public int Id { get; set; }
        public Student StudentId { get; set; }
        public Exercise ExerciseId { get; set; }

    }
}
