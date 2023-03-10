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
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine("Selected option 1 - List persons");
            Console.ResetColor();   
            Console.WriteLine();
            foreach (var person in persons)
            {
                Console.WriteLine($"Welcome {person.person_name}");
            }

        }

        public static void ListProjects(List<ProjectModel> projects)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Selected option 2 - List projects");
            Console.ResetColor();
            foreach (var project in projects)
            {
                Console.WriteLine($"Project name is: {project.project_name}");
            }

        }


        public static void CreatePersons()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Selected option 3 - Create new person");
            Console.ResetColor();
            Console.Write("Enter person name: ");
            string name = Console.ReadLine().ToLower();



            PersonModel newPerson = new PersonModel
            {
                person_name = name,

            };

            PostgresDataAccess.CreatePerson(newPerson);
        }

        public static void CreateProjects()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Selected option 4 - Create new project");
            Console.ResetColor();
            Console.Write("Enter project name: ");
            string name = Console.ReadLine().ToLower();



            ProjectModel newProject = new ProjectModel
            {
                project_name = name,

            };

            PostgresDataAccess.CreateProject(newProject);
        }

        public static void RegisterHours()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Selected option 5 - Register hour");
            Console.ResetColor();

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

            PostgresDataAccess.RegisterHour(project_name, person_name, hour);
        }

        public static void EditHours()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Selected option 6 - Edit hours");
            Console.ResetColor();

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

            PostgresDataAccess.EditHour(project_name, person_name, newHours);

        }


        public static void HoursByPersons()
        {
            try
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Selected option 7 - Worked hours by person");
                Console.ResetColor();
                Console.WriteLine("Enter person name:");
                string personName = Console.ReadLine().ToLower();
                PostgresDataAccess.HoursByPerson(personName);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid input. Please enter a valid input");
            }

            
        }
    }

}
