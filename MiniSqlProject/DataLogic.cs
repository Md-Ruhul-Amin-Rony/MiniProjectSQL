using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSqlProject
{
    public class DataLogic
    {
        public static void ListPersons(List<PersonModel> persons)
        {
            Console.Clear();
            Console.WriteLine("Selected option 1 - List persons");
            foreach (var person in persons)
            {
                Console.WriteLine($"Welcome {person.person_name}");
            }

        }

        public static void ListProjects(List<ProjectModel> projects)
        {
            Console.Clear();
            Console.WriteLine("Selected option 2 - List projects");
            foreach (var project in projects)
            {
                Console.WriteLine($"Project name is: {project.project_name}");
            }

        }


        public static void CreatePersons()
        {
            Console.Clear();
            Console.WriteLine("Selected option 3 - Create new person");
            Console.Write("Enter person name: ");
            string name = Console.ReadLine();
            
            

            PersonModel newPerson = new PersonModel
            {
                person_name = name,
               
            };

            PostgresDataAccess.CreatePerson(newPerson);
        }

        public static void CreateProjects()
        {
            Console.Clear();
            Console.WriteLine("Selected option 4 - Create new project");
            Console.Write("Enter project name: ");
            string name = Console.ReadLine();



            ProjectModel newProject = new ProjectModel
            {
                project_name = name,

            };

            PostgresDataAccess.CreateProject(newProject);
        }



    }
}
