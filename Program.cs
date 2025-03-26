using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

class Program
{
  static void Main()
  {
    int port = 5000;
    var server = new Server(port);
    // var counter = 0;
    string[] usernames = [];
    string[] passwords = [];
    string[] ids = [];  
    Console.WriteLine("The server is running");
    Console.WriteLine($"Main Page: http://localhost:{port}/website/pages/signup.html");

    var database = new Database();

    while (true)
    {
      (var request, var response) = server.WaitForRequest();

      Console.WriteLine($"Recieved a request with the path: {request.Path}");

      if (File.Exists(request.Path))
      {
        var file = new File(request.Path);
        response.Send(file);
      }
      else if (request.ExpectsHtml())
      {
        var file = new File("website/pages/404.html");
        response.SetStatusCode(404);
        response.Send(file);
      }
      else
      {
        try
        {
  
          if(request.Path=="signup"){
            (string username,string password) = request.GetBody<(string,string)>();
           usernames = [..usernames, username];
           passwords = [..passwords, password];
           ids = [..ids, Guid.NewGuid().ToString()];
           Console.WriteLine(username + ", "+ password);

          }
            else if(request.Path == "login"){
           (string username,string password) = request.GetBody<(string,string)>();

           bool foundUser=false;
           string userId = "";

           for(int i = 0; i<usernames.Length; i++){
           
           if (username==usernames[0] && password==passwords[0]){

            foundUser=true;
            userId = ids [i];
           }
           }
          
          response.Send((foundUser,userId));
          }
          
          response.SetStatusCode(405);

          database.SaveChanges();
        }
        catch (Exception exception)
        {
          Log.WriteException(exception);
        }
      }

      response.Close();
    }
  }
}


class Database() : DbBase("database")
{
  /*──────────────────────────────╮
  │ Add your database tables here │
  ╰──────────────────────────────*/
}

class User(string id, string username, string password)
{
  [Key] public string Id { get; set; } = id;
  public string Username { get; set; } = username;
  public string Password { get; set; } = password;
}