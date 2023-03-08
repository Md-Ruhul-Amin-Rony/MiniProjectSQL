# MiniProjectSQL
                               ##Time Report
**This is a console application that allows users to manage time reports for different projects and persons. The application uses a PostgreSQL database to store and retrieve data.**

##Features
#Create Person
The user can create a new person by providing a name and an email address. If the person already exists, the application will display an error message.

#Create Project
The user can create a new project by providing a name and a description. If the project already exists, the application will display an error message.

#Register Hours
The user can register the number of hours that a person has worked on a project. To do so, the user needs to provide the name of the person, the name of the project, and the number of hours worked. If the person or the project does not exist, the application will display an error message.

#Edit Hours
The user can edit the number of hours that a person has worked on a project. To do so, the user needs to provide the name of the person, the name of the project, and the new number of hours worked. If the person or the project does not exist, the application will display an error message. If the project is not registered to the person, the application will display an error message.

#Dependencies
This project requires the following dependencies:

*.NET 6.0
*Npgsql 6.0.7
*Dapper 2.0.90
*Configuration
The application reads the database connection string from the App.config file. Make sure to update the connection string with your PostgreSQL database credentials.

json
Copy code
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=timereport;Username=postgres;Password=postgres"
  }
}
#How to Run
Clone this repository.
Open the terminal or command prompt and navigate to the project directory.
Run the command dotnet run to start the application.
