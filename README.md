# Console App for Time Reporting using PostgresDb in C#:

This is a console application that allows users to manage time reports for different projects and persons. The application uses a PostgreSQL database to store and retrieve data.


## Features

### Create Person:
The user can create a new person by providing a name and an email address. If the person already exists, the application will display an error message.

### Create Project:
The user can create a new project by providing a name and a description. If the project already exists, the application will display an error message.
### Register Hours:
The user can register the number of hours that a person has worked on a project. To do so, the user needs to provide the name of the person, the name of the project, and the number of hours worked. If the person or the project does not exist, the application will display an error message.
### Edit Hours:
The user can edit the number of hours that a person has worked on a project. To do so, the user needs to provide the name of the person, the name of the project, and the new number of hours worked. If the person or the project does not exist, the application will display an error message. If the project is not registered to the person, the application will display an error message.
### View Hours:
The user can view in which projects he has worked and how many hours also. 




## ⚙ Classes / objects
|Object     |Description    |Comment|
|-----|--------|-------|
|App.config |Handle database connectionstring   |
|Menu |Display menu     |
|DataLogic |All the methods for taking input from user     |
|PostgresDataAccess |Methods for accessing data from database    |
|ProjectPersonModel |Class to assign project to different person     |



## 🔑 Key features
|Feature     |Status    |
|-----|:--------:|
|Creating perosns and projects |✅     |
|Assign project to person and hours | ✅    |
|Edit PersonName,ProjectName & hour for person to assigned projects|✅     |
|View total working hour for specific person |✅     |

## Lessons Learned

How to work using different classes. How to join different table in database through SQL query. How to work without taking primary key input from user.
The challenge that i have faced with is just by taking 'name' input from the user to find out which project the user is assigned to!


## 🖥 Tech stack
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white) 
![Postgres](https://img.shields.io/badge/PostgreSQL-316192.svg?style=for-the-badge&logo=postgresql&logoColor=white)



## Usage/Examples

```c sharp
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
```


## Dependencies




This project requires the following dependencies:

`.NET 6.0`
`Npgsql 6.0.7`
`Dapper 2.0.90`
`Configuration`
The application reads the database connection string from the App.config file. Make sure to update the connection string with your PostgreSQL database credentials.







## How to run:


Clone this repository.
Open the terminal or command prompt and navigate to the project directory.
Run the command dotnet run to start the application.

