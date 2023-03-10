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

        //public static void ListPersons()
        //{
        //    Console.Clear();
        //    Console.WriteLine("Selected option 1 - List persons");
        //    List<PersonModel> persons = LoadPersons();
        //    foreach (var person in persons)
        //    {
        //        Console.WriteLine($"Welcome {person.person_name}");
        //    }

        //}
        //public static void ListPersons(List<PersonModel> persons)
        //{
        //    Console.Clear();
        //    Console.WriteLine("Selected option 1 - List persons");
        //    foreach (var person in persons)
        //    {
        //        Console.WriteLine($"Welcome {person.person_name}");
        //    }
        //}

        //public static void ListProjects()
        //{
        //    Console.Clear();
        //    Console.WriteLine("Selected option 2 - List projects");
        //    List<ProjectModel> projects = LoadProjects();
        //    foreach (var project in projects)
        //    {
        //        Console.WriteLine($"Project name is: {project.project_name}.");
        //    }

        //}

       


        //public static void CreatePersons()
        //{
        //    Console.Clear();
        //    Console.WriteLine("Selected option 3- create persons:");
        //    using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        //    {
        //        try
        //        {
        //            cnn.Open();
        //            Console.WriteLine("Enter your  Name:");
        //            string person_name = Console.ReadLine().ToLower();





        //            // with crosscheck name
        //            string check = "SELECT COUNT(*) FROM mra_person WHERE person_name = @person_name";
        //            int count = cnn.ExecuteScalar<int>(check, new { person_name });
        //            if (count > 0)
        //            {
        //                Console.ForegroundColor = ConsoleColor.DarkRed;
        //                Console.WriteLine("The name is already in use.");
        //                Console.ResetColor();
        //                Console.WriteLine();
        //                return;
        //            }
        //            string sql = "INSERT INTO mra_person (person_name) " +
        //              "VALUES (@person_name)";


        //            cnn.Execute(sql, new { person_name});
        //            Console.ForegroundColor = ConsoleColor.Cyan;
        //            Console.WriteLine("New person has created successfully!");
        //            Console.WriteLine();
        //            Console.ResetColor();
        //            Console.WriteLine("Press enter to go to main");
        //            Console.ReadKey();
        //            Console.Clear();



        //        }
        //        catch (FormatException e)
        //        {
        //            Console.WriteLine("Invalid input. Please enter a valid input");
        //        }
        //    }
        //}


        public static void CreatePerson(PersonModel person)
        {
            

            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Open();
                    string check = "SELECT COUNT(*) FROM mra_person WHERE person_name = @person_name";
                    int count = cnn.ExecuteScalar<int>(check, new { person_name = person.person_name });
                    if (count > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("The name is already in use.Please try with another name");
                        Console.ResetColor();
                        Console.WriteLine();
                        return;
                    }

                    string sql = "INSERT INTO mra_person (person_name) " +
                                 "VALUES (@person_name)";
                    cnn.Execute(sql, person);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("New person has created successfully!");
                    Console.WriteLine();
                    Console.ResetColor();
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Invalid input. Please enter a valid input");
                }
            }
        }



        public static void CreateProject(ProjectModel project)
        {
            //Console.Clear();
            //Console.WriteLine("Selected option 4- create projects:");
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Open();
                    //Console.WriteLine("Enter project Name:");
                    //string project_name = Console.ReadLine().ToLower();
                   



                    // with crosscheck name
                    string check = "SELECT COUNT(*) FROM mra_project WHERE project_name = @project_name";
                    int count = cnn.ExecuteScalar<int>(check, new { project_name=project.project_name });
                    if (count > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("The course name is already in use.");
                        Console.ResetColor();
                        Console.WriteLine();
                        return;
                    }
                    string sql = "INSERT INTO mra_project (project_name) " +
                                 "VALUES (@project_name)";
                    cnn.Execute(sql, project );
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("New project has created successfully!");
                    Console.WriteLine();
                    Console.ResetColor();
                    Console.WriteLine("Press enter to go to main");
                    Console.ReadKey();
                    Console.Clear();



                }
                catch (FormatException e)
                {
                    Console.WriteLine("Invalid input. Please enter a valid input");
                }
            }
        }

        public static void RegisterHour(string project_name, string person_name, int hour)
        {
            
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Open();
                    
                        // Get project id
                        string sqlProject = "SELECT id FROM mra_project WHERE project_name = @project_name";
                        int project_id = cnn.ExecuteScalar<int>(sqlProject, new { project_name });

                        if (project_id == 0)
                        {
                            Console.WriteLine("Invalid input. Project name not found.");
                            return;
                        }
                    
                    // Get person id
                    string sqlPerson = "SELECT id FROM mra_person WHERE person_name = @person_name";
                    int person_id = cnn.ExecuteScalar<int>(sqlPerson, new { person_name });

                    if (person_id == 0)
                    {
                        Console.WriteLine("Invalid input. Person name not found.");
                        return;
                    }

                    // Insert hour into project_person table
                    string sqlHour = "INSERT INTO mra_project_person (project_id, person_id, hours) " +
                                     "VALUES (@project_id, @person_id, @hour)";
                    cnn.Execute(sqlHour, new { project_id, person_id, hour });
                    Console.WriteLine();
                    Console.ForegroundColor= ConsoleColor.DarkYellow;
                    Console.WriteLine($"Hour {hour} registered for {person_name} on project {project_name}.");
                    Console.ResetColor();

                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred: {e.Message}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press enter to go to main");
            Console.ReadKey();
            Console.Clear();
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











        public static void HoursByPerson()
        {
            Console.Clear();
            Console.WriteLine("Selected option 5 - List hours by person");
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Open();
                    Console.WriteLine("Enter person name:");
                    string personName = Console.ReadLine().ToLower();

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
                        Console.WriteLine($"{item.project_name}: {item.hours} hours");
                        totalHours += item.hours;
                    }
                    Console.WriteLine($"Total hours worked by {personName} is {totalHours}");
                    Console.WriteLine("Press enter to go to main");
                    Console.ReadKey();
                    Console.Clear();
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Invalid input. Please enter a valid input");
                }
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
                    Console.WriteLine("Press enter to go to main");
                    Console.ReadKey();
                    Console.Clear();
                
            }
        }







        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
