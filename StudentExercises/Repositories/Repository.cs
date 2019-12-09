using StudentExercises.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace StudentExercises.Repositories
{
    internal class Repository
    {
        public SqlConnection Connection
        {
            get
            {
                // This is "address" of the database
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=StudentExercises;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }

        public List<Exercise> GetAllExercises()
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, name, language FROM exercises";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> exercises = new List<Exercise>();

                    while (reader.Read())
                    {
                        //id
                        int idColumnPosition = reader.GetOrdinal("id");
                        int exerciseId = reader.GetInt32(idColumnPosition);
                        //name
                        int nameColumnPosition = reader.GetOrdinal("name");
                        string exerciseName = reader.GetString(nameColumnPosition);
                        //language
                        int languageColumnPosition = reader.GetOrdinal("language");
                        string exerciseLanguage = reader.GetString(languageColumnPosition);

                        Exercise newExercise = new Exercise(exerciseId, exerciseName, exerciseLanguage);

                        exercises.Add(newExercise);
                    }

                    reader.Close();

                    return exercises;
                }
            }
        }

        public List<Exercise> GetAllExercisesWithJava()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, name, language FROM exercises WHERE exercises.language = 'Javascript'";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> exercises = new List<Exercise>();

                    while (reader.Read())
                    {
                        //id
                        int idColumnPosition = reader.GetOrdinal("id");
                        int exerciseId = reader.GetInt32(idColumnPosition);
                        //name
                        int nameColumnPosition = reader.GetOrdinal("name");
                        string exerciseName = reader.GetString(nameColumnPosition);
                        //language
                        int languageColumnPosition = reader.GetOrdinal("language");
                        string exerciseLanguage = reader.GetString(languageColumnPosition);

                        Exercise newExercise = new Exercise(exerciseId, exerciseName, exerciseLanguage);

                        exercises.Add(newExercise);
                    }

                    reader.Close();

                    return exercises;
                }
            }
        }

        public Exercise AddExercise (Exercise exercise)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO exercises OUTPUT INSERTED.Id VALUES (@name, @language)";
                    cmd.Parameters.Add(new SqlParameter("@name", exercise.Name));
                    cmd.Parameters.Add(new SqlParameter("@language", exercise.Language));
                    int newId = (int)cmd.ExecuteScalar();

                    exercise.Id = newId;

                    return exercise;
                }
            }
        }

        public List<Exercise> GetAllInstructorsWithCohorts()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT instructors.id, instructors.first_name, instructors.last_name, instructors.slack_handle, instructors.specialty, cohorts.id, cohorts.name " +
                                      "FROM instructors " +
                                      "LEFT JOIN cohorts ON instructors.cohort_id = cohorts.id";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Instructor> exercises = new List<Instructor>();

                    while (reader.Read())
                    {
                        //id
                        int idColumnPosition = reader.GetOrdinal("instructors.id");
                        int instructorId = reader.GetInt32(idColumnPosition);
                        //first_name
                        int firstNameColumnPosition = reader.GetOrdinal("instructors.first_name");
                        string instructorFirstName = reader.GetString(firstNameColumnPosition);
                        //last_name
                        int lastNameColumnPosition = reader.GetOrdinal("instructors.last_name");
                        string instructorLastName = reader.GetString(lastNameColumnPosition);
                        //Specialty
                        int specialtyColumnPosition = reader.GetOrdinal("instructors.specialty");
                        string instructorSpecialty = reader.GetString(specialtyColumnPosition);
                        //Cohort_ID
                        int cohortIDColumnPosition = reader.GetOrdinal("cohorts.id");
                        int cohortID = reader.GetInt32(cohortIDColumnPosition);
                        //Cohort_Name
                        int cohortNameColumnPosition = reader.GetOrdinal("cohorts.name");
                        string cohortName = reader.GetString(cohortNameColumnPosition);

                        Cohort newCohort = new Cohort(cohortID, cohortName);
                        //Continue from here
                        Instructor newInstructor = new Exercise(exerciseId, exerciseName, exerciseLanguage);

                        exercises.Add(newExercise);
                    }

                    reader.Close();

                    return exercises;
                }
            }
        }
    }
}