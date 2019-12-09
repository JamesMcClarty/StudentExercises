using System;
using System.Collections.Generic;
using System.Text;

namespace StudentExercises.Models
{
    class Exercise
    {
        public Exercise(int id, string name, string lang)
        {
            Id = id;
            Name = name;
            Language = lang;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
    }
}
