using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MiniSqlProject
{
    public class PostgresDataAccess
    {
        public static void SavePerson(PersonModel person)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into mra_person (person_name) values (@person_name)", person);

            }
        }

        
        public static void EditPersonName(string newPersonName, string personName)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                
                
                    cnn.Open();

                   
                    string sql = "UPDATE mra_person SET person_name = @newPersonName WHERE person_name = @personName";
                    cnn.Execute(sql, new { newPersonName, personName });
                
               
            }
        }

        public static void EditProjectName(string newProjectName, string projectName)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {


                cnn.Open();


                string sql = "UPDATE mra_project SET project_name = @newProjectName WHERE project_name = @projectName";
                cnn.Execute(sql, new { newProjectName, projectName });


            }
        }

       


        public static void SaveProject(ProjectModel project)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT INTO mra_project (project_name) values (@project_name)", project);
            }
        }
        public static List<PersonModel> LoadPersons()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                //cnn.Open();
                var output = cnn.Query<PersonModel>("select * from mra_person", new DynamicParameters());



                return output.ToList();
                //cnn.Close();
            }

        }
        public static List<ProjectModel> LoadProjects()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ProjectModel>("SELECT * FROM mra_project", new DynamicParameters());
                return output.ToList();
            }
        }




        
        public static int CheckIfPersonExists(string person_name)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                string check = "SELECT COUNT(*) FROM mra_person WHERE person_name = @person_name";
                int count = cnn.ExecuteScalar<int>(check, new { person_name });
                return count;
            }
        }

        public static void CreatePerson(PersonModel person)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                string sql = "INSERT INTO mra_person (person_name) " +
                             "VALUES (@person_name)";
                cnn.Execute(sql, person);
            }
        }




        public static int CheckIfProjectExists(string project_name)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                string check = "SELECT COUNT(*) FROM mra_project WHERE project_name = @project_name";
                int count = cnn.ExecuteScalar<int>(check, new { project_name });
                return count;
            }
        }

        public static void CreateProject(ProjectModel project)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                string sql = "INSERT INTO mra_project (project_name) VALUES (@project_name)";
                cnn.Execute(sql, project);
            }
        }

        public static int RegisterProjectExists(string project_name)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                string sql = "SELECT id FROM mra_project WHERE project_name = @project_name";
                return cnn.ExecuteScalar<int>(sql, new { project_name });
            }
        }

        public static int RegisterPersonExists(string person_name)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                string sql = "SELECT id FROM mra_person WHERE person_name = @person_name";
                return cnn.ExecuteScalar<int>(sql, new { person_name });
            }
        }


        //public static void RegisterHour(string project_name, string person_name, int hour)
        //{

        //    using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        //    {
        //        try
        //        {
        //            cnn.Open();

        //                // Get project id
        //                string sqlProject = "SELECT id FROM mra_project WHERE project_name = @project_name";
        //                int project_id = cnn.ExecuteScalar<int>(sqlProject, new { project_name });

        //                if (project_id == 0)
        //                {
        //                    Console.WriteLine("Invalid input. Project name not found.");
        //                    return;
        //                }

        //            // Get person id
        //            string sqlPerson = "SELECT id FROM mra_person WHERE person_name = @person_name";
        //            int person_id = cnn.ExecuteScalar<int>(sqlPerson, new { person_name });

        //            if (person_id == 0)
        //            {
        //                Console.WriteLine("Invalid input. Person name not found.");
        //                return;
        //            }

        //            // Insert hour into project_person table
        //            string sqlHour = "INSERT INTO mra_project_person (project_id, person_id, hours) " +
        //                             "VALUES (@project_id, @person_id, @hour)";
        //            cnn.Execute(sqlHour, new { project_id, person_id, hour });
        //            Console.WriteLine();
        //            Console.ForegroundColor= ConsoleColor.DarkYellow;
        //            Console.WriteLine($"Hour {hour} registered for {person_name} on project {project_name}.");
        //            Console.ResetColor();

        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine($"An error occurred: {e.Message}");
        //        }
        //    }


        //}

        public static void RegisterHour(int projectId, int personId, int hour)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                
                
                    cnn.Open();

                    // Insert hour into project_person table
                    string sqlHour = "INSERT INTO mra_project_person (project_id, person_id, hours) " +
                                     "VALUES (@project_id, @person_id, @hour)";
                    cnn.Execute(sqlHour, new { project_id = projectId, person_id = personId, hour });
                
               
            }
        }



        public static void EditHour(string project_name, string person_name, int newHours)
        {


            // Check if project exists
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                string checkProject = "SELECT COUNT(*) FROM mra_project WHERE project_name = @project_name";
                int projectCount = cnn.ExecuteScalar<int>(checkProject, new { project_name });
                if (projectCount == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("The project name does not exist.");
                    Console.ResetColor();
                    Console.WriteLine();
                    return;
                }
            }

            // Get the person name


            // Check if person exists
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                string checkPerson = "SELECT COUNT(*) FROM mra_person WHERE person_name = @person_name";
                int personCount = cnn.ExecuteScalar<int>(checkPerson, new { person_name });
                if (personCount == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("The person name does not exist.");
                    Console.ResetColor();
                    Console.WriteLine();
                    return;
                }
            }

            // Get the new hours


            // Update the hours worked
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                string sql = "UPDATE mra_project_person SET hours = @hours " +
                             "WHERE project_id = (SELECT id FROM mra_project WHERE project_name = @project_name) " +
                             "AND person_id = (SELECT id FROM mra_person WHERE person_name = @person_name) " +
                             "AND EXISTS (SELECT 1 FROM mra_project_person " +
                             "WHERE project_id = (SELECT id FROM mra_project WHERE project_name = @project_name) " +
                             "AND person_id = (SELECT id FROM mra_person WHERE person_name = @person_name))";
                int rowsUpdated = cnn.Execute(sql, new { hours = newHours, project_name, person_name });

                if (rowsUpdated == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("The person is not assigned to the given project.");
                    Console.ResetColor();
                    Console.WriteLine();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Hours updated successfully!");
                Console.ResetColor();
                Console.WriteLine();
            }

        }











        


        public static void HoursByPerson(string personName)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                
                    cnn.Open();
                    

                    // Check if person exists
                    string checkPerson = "SELECT COUNT(*) FROM mra_person WHERE person_name = @personName";
                    int personCount = cnn.ExecuteScalar<int>(checkPerson, new { personName });
                    if (personCount == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("The person does not exist.");
                        Console.ResetColor();
                        Console.WriteLine();
                        return;
                    }

                    // Get hours by person
                    string sql = "SELECT p.project_name, pp.hours " +
                                 "FROM mra_project p " +
                                 "JOIN mra_project_person pp ON pp.project_id = p.id " +
                                 "JOIN mra_person pe ON pp.person_id = pe.id " +
                                 "WHERE pe.person_name = @personName";
                    var result = cnn.Query(sql, new { personName });

                    // Display results
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Hours worked by {personName} on different projects:");
                    Console.ResetColor();
                    int totalHours = 0;
                    foreach (var item in result)
                    {
                        Console.WriteLine($"Project name: {item.project_name} : {item.hours} hours");
                        totalHours += item.hours;
                    }
                    Console.WriteLine($"Total hours worked by {personName} is {totalHours}");
                    
                
            }
        }







        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
