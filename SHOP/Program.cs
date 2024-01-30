// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


using Microsoft.Data.SqlClient;

string ConnectionString = "Server=DESKTOP-C3SM1NS\\SQLEXPRESS; Database=Shop; Trusted_Connection=true;TrustServerCertificate=True;";
SqlConnection SqlConnection = new SqlConnection(ConnectionString);



Menu();
int request = int.Parse(Console.ReadLine());

while(request != 0)
{
    switch (request)
    {
        case 1:
            Create();
            break;
            case 2:
            GetAll();
            break;
        case 3:
            Delete();
            break;
        case 4:
            Update();
            break;
        case 5:
            GetById();
            break;
        default: 
            break;
    }
    Menu();
    request = int.Parse(Console.ReadLine());
}


void Menu()
{
    Console.ForegroundColor = ConsoleColor.Cyan;

    Console.WriteLine("Add a request");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("1.Create");
    Console.WriteLine("2.GetAll");
    Console.WriteLine("3.Delete");
    Console.WriteLine("4.Update");
    Console.WriteLine("5.GetById");
    Console.WriteLine("0.Close");
    Console.ResetColor();

}


void GetAll()
{
    SqlConnection.Open();
    SqlCommand command = new SqlCommand("Select * from Category Order by Id", SqlConnection);

    SqlDataReader result = command.ExecuteReader();

    while(result.Read())
    {
        Console.WriteLine($"Id:{result["Id"]},Name:{result["Name"]}");
    }

    SqlConnection.Close();
}

void Create()
{
    Console.WriteLine("Add Name");
    string Name = Console.ReadLine();

    SqlConnection.Open();
    SqlCommand sqlCommand = new SqlCommand($"Insert Into Category Values ('{Name}')",SqlConnection);
    int result = sqlCommand.ExecuteNonQuery();

    if(result != 0)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Succsesfully");
    }

    SqlConnection.Close();
}

void Delete()
{
    Console.WriteLine("Enter Id to delete:");
    string Id = Console.ReadLine();

    SqlConnection.Open();

    SqlCommand sqlCommand = new SqlCommand("DELETE FROM Category WHERE Id=@Id", SqlConnection);

    sqlCommand.Parameters.AddWithValue("@Id", Id);

    int result = sqlCommand.ExecuteNonQuery();

    if (result != 0)
    {
        Console.WriteLine("Successfully deleted.");
    }
    else
    {
        Console.WriteLine("No records deleted. Id might not exist.");
    }

    SqlConnection.Close();
}

void Update()
{
    Console.WriteLine("Enter Id to Update:");
    string Id = Console.ReadLine();

    Console.WriteLine("Enter new Name:");
    string Name = Console.ReadLine();

    SqlConnection.Open();

    SqlCommand sqlCommand = new SqlCommand("UPDATE Category SET [Name] = @Name WHERE Id = @Id", SqlConnection);

    sqlCommand.Parameters.AddWithValue("@Id", Id);
    sqlCommand.Parameters.AddWithValue("@Name", Name);

    int result = sqlCommand.ExecuteNonQuery();

    if (result != 0)
    {
        Console.WriteLine("Successfully updated.");
    }
    else
    {
        Console.WriteLine("No records updated. Id might not exist.");
    }

    SqlConnection.Close();
}

void GetById()
{
    Console.WriteLine("Enter to Id");
    string Id =  Console.ReadLine();

    SqlConnection.Open();
    SqlCommand command = new SqlCommand($"Select * from Category Where Id={Id}", SqlConnection);

    SqlDataReader result = command.ExecuteReader();


    while (result.Read())
    {
        Console.WriteLine($"Id:{result["Id"]},Name:{result["Name"]}");
    }

    SqlConnection.Close();
}