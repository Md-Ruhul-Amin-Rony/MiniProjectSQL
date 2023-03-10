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

        public static void EditHour(int project_id, int person_id, int hours)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {


                cnn.Open();

                // Insert hour into project_person table
                cnn.Execute("UPDATE mra_project_person SET hours = @hours WHERE project_id = @project_id AND person_id = @person_id",
                new { hours, project_id, person_id });


            }
        }
        public static bool CheckIfPersonAssignedToProject(int person_id, int project_id)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Open();
                // to check if the person is assigned to the project 
                return cnn.ExecuteScalar<int>("SELECT COUNT(*) FROM mra_project_person WHERE person_id = @person_id AND project_id = @project_id",
                    new { person_id, project_id }) > 0;
            }
        }



        public static IEnumerable<dynamic> HoursByPerson(string personName)
        // IEnumerable<dynamic> is a generic interface that provides an abstraction for iteration over a collection of data.
        // so the user can use "foreach" or LINQ to iterate over the returned sequence and access data
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
               
                    // Get hours by person by joining all three tables
                    string sql = "SELECT p.project_name, pp.hours " +
                                 "FROM mra_project p " +
                                 "JOIN mra_project_person pp ON pp.project_id = p.id " +
                                 "JOIN mra_person pe ON pp.person_id = pe.id " +
                                 "WHERE pe.person_name = @personName";
                     return cnn.Query(sql, new { personName });
                

                   
                
            }
        }







        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
