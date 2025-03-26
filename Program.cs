using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

class Program
{
  static void Main()
  {
    var port = 5000;
    int port = 5000;
    var server = new Server(port);
    // var counter = 0;
    string[] usernames = [];
    string[] passwords = [];
    string[] ids = [];  
    Console.WriteLine("The server is running");
    Console.WriteLine($"Main Page: http://localhost:{port}/website/pages/signup.html");

    var database = new Database();
    var counter = 0;
    string[] usernames = [];
    string[] passwords = [];
    string[] ids = [];   
     Console.WriteLine("The server is running");
    Console.WriteLine($"Main Page: http://localhost:{port}/website/pages/login.html");

    while (true)
    {
      (var request, var response) = server.WaitForRequest();

      Console.WriteLine("got a request:" + request.Path);

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
          if (request.Path == "message")
          {
            string text = request.GetBody<string>();
            Console.WriteLine(" recieved " + text + " from the client ");
          }
          else if (request.Path == "setCounter"){
            int difference = request.GetBody<int>();
            counter += difference;
            response.Send(counter);
          }
        
          else if(request.Path=="signup"){
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
          else if (request.Path == "getUsername"){
            string userId = request.GetBody<string>();

            int i=0;
            while(ids[i]!=userId)
            {
              i++;
            }
           
           string username = usernames[i];
           
           response.Send(username);
          }
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