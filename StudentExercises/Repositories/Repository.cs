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
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=StudentExercises;Integrated Security=True; MultipleActiveResultSets=True";
                return new SqlConnection(_connectionString);
            }
        }

        public List<Exercise> GetAllExercises()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
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

        public Exercise AddExercise(Exercise exercise)
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

        public List<Instructor> GetAllInstructorsWithCohorts()
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

                    List<Instructor> instructors = new List<Instructor>();

                    while (reader.Read())
                    {
                        //id
                        int idColumnPosition = reader.GetOrdinal("id");
                        int instructorId = reader.GetInt32(idColumnPosition);
                        //first_name
                        int firstNameColumnPosition = reader.GetOrdinal("first_name");
                        string instructorFirstName = reader.GetString(firstNameColumnPosition);
                        //last_name
                        int lastNameColumnPosition = reader.GetOrdinal("last_name");
                        string instructorLastName = reader.GetString(lastNameColumnPosition);
                        //Specialty
                        int slackColumnPosition = reader.GetOrdinal("slack_handle");
                        string instructorSlack = reader.GetString(slackColumnPosition);
                        //Specialty
                        int specialtyColumnPosition = reader.GetOrdinal("specialty");
                        string instructorSpecialty = reader.GetString(specialtyColumnPosition);
                        //Cohort_ID
                        int cohortIDColumnPosition = reader.GetOrdinal("id");
                        int cohortID = reader.GetInt32(cohortIDColumnPosition);
                        //Cohort_Name
                        int cohortNameColumnPosition = reader.GetOrdinal("name");
                        string cohortName = reader.GetString(cohortNameColumnPosition);

                        Cohort newCohort = new Cohort(cohortID, cohortName);

                        Instructor newInstructor = new Instructor(instructorId, instructorFirstName, instructorLastName, instructorSlack, instructorSpecialty, newCohort);

                        instructors.Add(newInstructor);
                    }

                    reader.Close();

                    return instructors;
                }
            }
        }

        public Instructor AddInstructorWithCohort(Instructor instructor, int cohort_id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO instructors OUTPUT INSERTED.Id VALUES (@first_name, @last_name, @slack_handle, @specialty, @cohort_id)";
                    cmd.Parameters.Add(new SqlParameter("@first_name", instructor.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@last_name", instructor.LastName));
                    cmd.Parameters.Add(new SqlParameter("@slack_handle", instructor.SlackHandle));
                    cmd.Parameters.Add(new SqlParameter("@specialty", instructor.Specialty));
                    cmd.Parameters.Add(new SqlParameter("@cohort_Id", cohort_id));
                    int newId = (int)cmd.ExecuteScalar();

                    instructor.Id = newId;

                    return instructor;
                }
            }
        }

        public StudentExercise AssignExerciseToStudent(int student, int exercise)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO studentexercises OUTPUT INSERTED.Id VALUES (@student_id, @exercise_id)";
                    cmd.Parameters.Add(new SqlParameter("@student_id", student));
                    cmd.Parameters.Add(new SqlParameter("@exercise_id", exercise));
                    int newId = (int)cmd.ExecuteScalar();

                    StudentExercise newAssignment = new StudentExercise(newId);

                    return newAssignment;
                }
            }
        }

        public List<Student> GetAllStudentsCohortsAndExercises()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT students.id, students.first_name, students.last_name, students.slack_handle, cohorts.id, cohorts.[name]" +
                    "FROM students " +
                    "LEFT JOIN studentexercises ON students.id = studentexercises.student_id " +
                    "RIGHT JOIN cohorts ON students.cohort_id = cohorts.id ";
                    SqlDataReader reader = cmd.ExecuteReader();
                
                    while (reader.Read())
                    {
                        //id
                        int studentIdColumnPosition = reader.GetOrdinal("id");
                        int studentId = reader.GetInt32(studentIdColumnPosition);
                        //first-name
                        int firstNameColumnPosition = reader.GetOrdinal("first_name");
                        string studentFirstName = reader.GetString(firstNameColumnPosition);
                        //last-name
                        int lastNameColumnPosition = reader.GetOrdinal("last_name");
                        string studentLastName = reader.GetString(lastNameColumnPosition);
                        //slack-handle
                        int slackColumnPosition = reader.GetOrdinal("slack_handle");
                        string studentSlack = reader.GetString(slackColumnPosition);
                        //cohorts.id
                        int cohortsIdColumnPosition = reader.GetOrdinal("id");
                        int cohortsId = reader.GetInt32(cohortsIdColumnPosition);
                        //cohorts.name
                        int cohortsNameColumnPosition = reader.GetOrdinal("name");
                        string cohortsName = reader.GetString(cohortsNameColumnPosition);


                        Cohort newCohort = new Cohort(cohortsId, cohortsName);    
                        Student newStudent = new Student(studentId, studentFirstName, studentLastName, studentSlack, newCohort);

                        using (SqlCommand exercisescmd = conn.CreateCommand())
                        {
                            List<Exercise> exercises = new List<Exercise>();
                            exercisescmd.CommandText = "SELECT exercises.id, exercises.name, exercises.language " +
                                "FROM exercises " +
                                "INNER JOIN studentexercises ON exercises.id = studentexercises.exercise_id " +
                                "WHERE studentexercises.student_id = @student_id AND studentexercises.exercise_id IS NOT NULL";
                            exercisescmd.Parameters.Add(new SqlParameter("@student_id", studentId));
                            SqlDataReader exercisereader = exercisescmd.ExecuteReader();
                            while (exercisereader.Read())
                            {
                                //id
                                int exerciseIdColumnPosition = exercisereader.GetOrdinal("id");
                                int exerciseId = exercisereader.GetInt32(exerciseIdColumnPosition);
                                //name
                                int exerciseNameColumnPosition = exercisereader.GetOrdinal("name");
                                string exerciseName = exercisereader.GetString(exerciseNameColumnPosition);
                                //language
                                int exerciseLanguageColumnPosition = exercisereader.GetOrdinal("language");
                                string exerciseLanguage = exercisereader.GetString(exerciseLanguageColumnPosition);

                                Exercise newExercise = new Exercise(exerciseId, exerciseName, exerciseLanguage);
                                exercises.Add(newExercise);
                            }
                            exercisereader.Close();
                            newStudent.Exercises = exercises;
                        }
                        students.Add(newStudent);
                    }
                    reader.Close();
                    return students;
                }
            }
        }
    }
}