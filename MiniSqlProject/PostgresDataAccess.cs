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

        public static void ListPersons()
        {
            Console.Clear();
            Console.WriteLine("Selected option 1 - List persons");
            List<PersonModel> perons = LoadPersons();
            foreach (var person in perons)
            {
                Console.WriteLine($"Welcome {person.person_name}");
            }

        }
        public static void ListProjects()
        {
            Console.Clear();
            Console.WriteLine("Selected option 1 - List projects");
            List<ProjectModel> projects = LoadProjects();
            foreach (var project in projects)
            {
                Console.WriteLine($"Project name is: {project.project_name}.");
            }

        }


        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
