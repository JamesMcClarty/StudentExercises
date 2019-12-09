using System;
using System.Collections.Generic;
using System.Text;

namespace StudentExercises.Models
{
    class Instructor
    {
        public Instructor(int id, string firstName, string lastname, string slack, string specialty)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastname;
            SlackHandle = slack;
            Specialty = specialty;
        }

        public Instructor(int id, string firstName, string lastname, string slack, string specialty, Cohort cohort)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastname;
            SlackHandle = slack;
            Specialty = specialty;
            CohortId = cohort;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SlackHandle { get; set; }
        public string Specialty { get; set; }
        public Cohort CohortId { get; set; }
    }
}
