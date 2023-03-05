﻿using System;
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
            Console.WriteLine("Welcome to the database! Please select one of the options below:");
            Console.WriteLine();

            // Display menu options
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("1. List Persons");
            Console.WriteLine("2. List Projects");
            Console.WriteLine("3. Create persons");
            Console.WriteLine("4. Create project");
            Console.WriteLine("5. Register hours");
            Console.WriteLine("6. Edit hours");
            Console.WriteLine("7. Delete course");
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
                    Console.WriteLine("7. Delete course");
                    break;
                case 8:
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
                    selection = (selection == 1) ? 8 : selection - 1;
                    break;
                case ConsoleKey.DownArrow:
                    selection = (selection == 8) ? 1 : selection + 1;
                    break;
                case ConsoleKey.Enter:
                    switch (selection)
                    {
                        case 1:

                            PostgresDataAccess.ListPersons();

                            break;
                        case 2:
                            PostgresDataAccess.ListProjects();
                            break;
                        case 3:
                          //  PostgresDataAccess.CreateStudents();
                            break;
                        case 4:
                            //PostgresDataAccess.CreateCourses();

                            break;
                        case 5:
                            //PostgresDataAccess.ChangePasswordByEmail();

                            break;
                        case 6:
                            //PostgresDataAccess.EditCourse();

                            break;
                        case 7:
                           // PostgresDataAccess.DeleteCourse();

                            break;
                        case 8:
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