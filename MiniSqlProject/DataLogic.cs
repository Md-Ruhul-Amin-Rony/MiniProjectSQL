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
             PersonModel person= new PersonModel { person_name= name };


            int count = PostgresDataAccess.CheckIfPersonExists(person.person_name);
            if (count > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("The name is already in use.Please try with another name");
                Console.ResetColor();
                Console.WriteLine();
                return;
            }

            PostgresDataAccess.CreatePerson(person);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("New person has created successfully!");
            Console.WriteLine();
            Console.ResetColor();
        }


        public static void EditPersonNames() 
        {
            Console.Clear();
            Console.ForegroundColor=ConsoleColor.Green; 
            Console.WriteLine("Selected option 8 - Edit person name:");
            Console.ResetColor();
            List<PersonModel> persons =PostgresDataAccess.LoadPersons();
            Console.WriteLine("Enter the name of the person you want to edit:");
            string personName = Console.ReadLine().ToLower();

            // Check if person exists
            bool personExists = persons.Any(p => p.person_name.ToLower() == personName);
            if (!personExists)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Person not found.");
                Console.ResetColor();
                Console.WriteLine();
                return;
            }

            Console.WriteLine("Enter the new name for the person:");
            string newPersonName = Console.ReadLine().ToLower();

            PostgresDataAccess.EditPersonName(newPersonName, personName);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"You have successfully edited the name of the person {personName} to new name {newPersonName}");
            Console.ResetColor();
        }


        public static void CreateProjects()
        {
            Console.Clear();
            Console.WriteLine("Selected option 4- create projects:");

            Console.WriteLine("Enter project Name:");
            string project_name = Console.ReadLine().ToLower();

            // with crosscheck name
            int count = PostgresDataAccess.CheckIfProjectExists(project_name);
            if (count > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("The project name is already in use.");
                Console.ResetColor();
                Console.WriteLine();
                return;
            }

            var project = new ProjectModel { project_name = project_name };
            PostgresDataAccess.CreateProject(project);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("New project has created successfully!");
            Console.WriteLine();
            Console.ResetColor();
           
        }

       


        public static void EditProjectNames()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Selected option 8 - Edit person name:");
            Console.ResetColor();
            List<ProjectModel> projects = PostgresDataAccess.LoadProjects();
            Console.WriteLine("Enter the name of the project you want to edit:");
            string projectName = Console.ReadLine().ToLower();

            // Check if person exists
            bool projectExists = projects.Any(p => p.project_name.ToLower() == projectName);
            if (!projectExists)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Project not found.");
                Console.ResetColor();
                Console.WriteLine();
                return;
            }

            Console.WriteLine("Enter the new name for the project:");
            string newProjectName = Console.ReadLine().ToLower();

            PostgresDataAccess.EditProjectName(newProjectName, projectName);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"You have successfully edited the name of the project {projectName} to new name {newProjectName}");
            Console.ResetColor();
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

            // Check if project exists
            int project_id = PostgresDataAccess.RegisterProjectExists(project_name);
            if (project_id == 0)
            {
                Console.WriteLine("Invalid input. Project name not found.");
                return;
            }

            // Get person name from user
            Console.WriteLine("Enter person name:");
            string person_name = Console.ReadLine().ToLower();

            // Check if person exists
            int person_id = PostgresDataAccess.RegisterPersonExists(person_name);
            if (person_id == 0)
            {
                Console.WriteLine("Invalid input. Person name not found.");
                return;
            }

            // Get hour from user
            Console.WriteLine("Enter hour:");
            int hour;
            if (!int.TryParse(Console.ReadLine(), out hour))
            {
                Console.WriteLine("Invalid input. Please enter an integer value for hour.");
                return;
            }

            PostgresDataAccess.RegisterHour(project_id, person_id, hour);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Hour {hour} registered for {person_name} on project {project_name}.");
            Console.ResetColor();
        }


        public static void EditHours()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Selected option 6 - Edit hours");
            Console.ResetColor();

            // Get project name from user
            Console.WriteLine("Enter project name:");
            string project_name = Console.ReadLine().ToLower();

            // Check if project exists
            int project_id = PostgresDataAccess.RegisterProjectExists(project_name);
            if (project_id == 0)
            {
                Console.WriteLine("Invalid input. Project name not found.");
                return;
            }

            // Get person name from user
            Console.WriteLine("Enter person name:");
            string person_name = Console.ReadLine().ToLower();

            // Check if person exists
            int person_id = PostgresDataAccess.RegisterPersonExists(person_name);
            if (person_id == 0)
            {
                Console.WriteLine("Invalid input. Person name not found.");
                return;
            }

            // Check if person is assigned to project
            bool isAssigned = PostgresDataAccess.CheckIfPersonAssignedToProject(person_id, project_id);
            if (!isAssigned)
            {
                Console.WriteLine("Invalid input. Person is not assigned to this project.");
                return;
            }

            // Get new hour from user
            Console.WriteLine("Enter new hour:");
            int hour;
            if (!int.TryParse(Console.ReadLine(), out hour))
            {
                Console.WriteLine("Invalid input. Please enter an integer value for hour.");
                return;
            }

            // Update project_person table with new hour
            PostgresDataAccess.EditHour(project_id, person_id, hour);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Hour for {person_name} on project {project_name} updated to {hour}.");
            Console.ResetColor();
        }




        //public static void EditHours()
        //{
        //    Console.Clear();
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine("Selected option 6 - Edit hours");
        //    Console.ResetColor();

        //    // Get the project name
        //    Console.WriteLine("Enter project name:");
        //    string project_name = Console.ReadLine().ToLower();

        //    Console.WriteLine("Enter person name:");
        //    string person_name = Console.ReadLine().ToLower();
        //    Console.WriteLine("Enter the new hours:");
        //    int newHours;
        //    bool success = int.TryParse(Console.ReadLine(), out newHours);
        //    if (!success)
        //    {
        //        Console.ForegroundColor = ConsoleColor.DarkRed;
        //        Console.WriteLine("Invalid input. Please enter a valid integer.");
        //        Console.ResetColor();
        //        Console.WriteLine();
        //        return;
        //    }

        //    PostgresDataAccess.EditHour(project_name, person_name, newHours);

        //}


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
            catch 
            {
                Console.WriteLine("Invalid input. Please enter a valid input");
            }

            
        }
    }

}
