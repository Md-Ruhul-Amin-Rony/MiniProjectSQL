using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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

                    string sql = "INSERT INTO mra_person (person_name, age, email) " +
                                 "VALUES (@person_name, @age, @email)";
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
                    cnn.Execute(sql, new { project });
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

        public static void RegisterHour()
        {
            Console.Clear();
            Console.WriteLine("Selected option 4 - Register hours");

            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Open();
                    Console.WriteLine("Enter project name:");
                    string project_name = Console.ReadLine().ToLower();

                    // Cross-check project_name
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

                    Console.WriteLine("Enter person name:");
                    string person_name = Console.ReadLine().ToLower();

                    // Cross-check person_name
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

                    Console.WriteLine("Enter the hour(s) spent:");
                    int hours = int.Parse(Console.ReadLine());

                    // Insert the hour(s) spent into the project_person table
                    string sql = "INSERT INTO mra_project_person (project_id, person_id, hours) " +
                                 "VALUES ((SELECT id FROM mra_project WHERE project_name = @project_name), " +
                                 "(SELECT id FROM mra_person WHERE person_name = @person_name), @hours)";
                    cnn.Execute(sql, new { project_name, person_name, hours });

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Hour(s) has been registered successfully!");
                    Console.ResetColor();
                    Console.WriteLine();
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





        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
