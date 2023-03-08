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

        public static void RegisterHours()
        {
            Console.Clear();
            Console.WriteLine("Selected option 5 - Register hour");

            // Get project name from user
            Console.WriteLine("Enter project name:");
            string project_name = Console.ReadLine().ToLower();
            
            

            // Get person name from user
            Console.WriteLine("Enter person name:");
            string person_name = Console.ReadLine().ToLower();

            // Get hour from user
            Console.WriteLine("Enter hour:");
            int hour;
            if (!int.TryParse(Console.ReadLine(), out hour))
            {
                Console.WriteLine("Invalid input. Please enter an integer value for hour.");
                return;
            }

            PostgresDataAccess.RegisterHour(project_name,person_name,hour);
        }

        public static void EditHours()
        {
            Console.Clear();
            Console.WriteLine("Selected option 4 - Edit hours");

            // Get the project name
            Console.WriteLine("Enter project name:");
            string project_name = Console.ReadLine().ToLower();

            Console.WriteLine("Enter person name:");
            string person_name = Console.ReadLine().ToLower();
            Console.WriteLine("Enter the new hours:");
            int newHours;
            bool success = int.TryParse(Console.ReadLine(), out newHours);
            if (!success)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid input. Please enter a valid integer.");
                Console.ResetColor();
                Console.WriteLine();
                return;
            }

            PostgresDataAccess.EditHour(project_name,person_name,newHours);

        }
    }
}
