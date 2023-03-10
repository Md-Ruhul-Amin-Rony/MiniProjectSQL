using System;
using MiniSqlProject;

class Menu
{
    static void Main()
    {
        // Set initial selection to option 1
        int selection = 1;

        // Loop until user selects "Terminate"
        while (selection != 0)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Time Report App! Please select one of the options below:");
            Console.WriteLine();

            // Display menu options
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("1. List Persons");
            Console.WriteLine("2. List Projects");
            Console.WriteLine("3. Create persons");
            Console.WriteLine("4. Create project");
            Console.WriteLine("5. Register hours");
            Console.WriteLine("6. Edit hours");
            Console.WriteLine("7. Worked hours on projects by person");
            Console.WriteLine("8. Edit Persons");
            Console.WriteLine("9. Edit Projects");
            Console.WriteLine("A. Terminate");
            Console.WriteLine("       ↓");
            Console.WriteLine("       ↓");

            Console.WriteLine();
            Console.WriteLine("Press the " + "\u2193" +" \u2191"+" key to move down and up.");



            // Highlight selected option
            switch (selection)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("1. List persons");
                    //Console.WriteLine("Here is student list");
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("2. List projects");
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("3. Create person");
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("4. Create project");
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("5. Register hours");
                    break;
                case 6:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("6. Edit hours");
                    break;
                case 7:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("7. Worked hours on projects by person");
                    break;
                case 8:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("8. Edit Persons");
                    break;
                case 9:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("9. Edit Projects");
                    break;
                case 10:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("A. Terminate");
                    break;
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        
            // Wait for user input
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            // Update selection based on user input
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    selection = (selection == 1) ? 10 : selection - 1;
                    break;
                case ConsoleKey.DownArrow:
                    selection = (selection == 10) ? 1 : selection + 1;
                    break;
                case ConsoleKey.Enter:
                    switch (selection)
                    {
                        case 1:
                           List<PersonModel> listOfPersons =PostgresDataAccess.LoadPersons();
                            DataLogic.ListPersons(listOfPersons);

                            //PostgresDataAccess.ListPersons();

                            break;
                        case 2:
                            List<ProjectModel> listOfProjects= PostgresDataAccess.LoadProjects();
                            DataLogic.ListProjects(listOfProjects);    
                            //PostgresDataAccess.ListProjects();
                            break;
                        case 3:
                            DataLogic.CreatePersons();

                           // PostgresDataAccess.CreatePersons();
                          //  PostgresDataAccess.CreateStudents();
                            break;
                        case 4:
                            
                            DataLogic.CreateProjects(); 
                            //PostgresDataAccess.CreateCourses();

                            break;
                        case 5:

                            DataLogic.RegisterHours();
                            //PostgresDataAccess.ChangePasswordByEmail();

                            break;
                        case 6:
                            DataLogic.EditHours();
                          //PostgresDataAccess.EditHours();
                            //PostgresDataAccess.EditCourse();

                            break;
                        case 7:
                          //  PostgresDataAccess.HoursByPerson();
                            DataLogic.HoursByPersons();
                           // PostgresDataAccess.DeleteCourse();

                            break;
                            case 8:
                            DataLogic.EditPersonNames();    
                            break;
                        case 9:
                            DataLogic.EditProjectNames();
                            break;
                        case 10:
                            selection = 0;
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Press any key to continue...");
                    Console.ResetColor();
                    Console.ReadKey(true);
                    break;
            }
        }
    }
}