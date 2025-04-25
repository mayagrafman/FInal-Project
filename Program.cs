﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

class Program
{
  static void Main()
  {
    var port = 5000;
    var server = new Server(port);

    Console.WriteLine("The server is running");
    Console.WriteLine($"Main Page: http://localhost:{port}/website/pages/index.html");

    // var database = new Database();
    var database = new Database();
    if (database.IsNewlyCreated())
    {
      database.Cities.Add(new City("Tel Aviv", "/website/images/tel_aviv.jpg"));

      database.SaveChanges();
    }

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
          if (request.Path == "verifyUserId")
          {
            var userId = request.GetBody<string>();

            var varified = database.Users.Any(user => user.Id == userId);

            response.Send(varified);
          }
          else if (request.Path == "signUp")
          {
            var (username, password) = request.GetBody<(string, string)>();

            var userExists = database.Users.Any(user =>
              user.Username == username
            );

            if (!userExists)
            {
              var userId = Guid.NewGuid().ToString();
              database.Users.Add(new User(userId, username, password));
              response.Send(userId);
            }
          }
          else if (request.Path == "logIn")
          {
            var (username, password) = request.GetBody<(string, string)>();

            var user = database.Users.First(
              user => user.Username == username && user.Password == password
            );

            var userId = user.Id;

            response.Send(userId);
          }
          else if (request.Path == "getUsername")
          {
            string userId = request.GetBody<string>();

            var user = database.Users.Find(userId);

            var username = user?.Username;

            response.Send(username);
          }

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


  class Database() : DbBase("database")
  {
    /*──────────────────────────────╮
    │ Add your database tables here │
    ╰──────────────────────────────*/
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<City> Cities { get; set; } = default!;

  }
  class User(string id, string username, string password)
  {
    [Key] public string Id { get; set; } = id;
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
  }

  class City(string name, string image)
  {
    [Key] public int Id { get; set; } = default!;
    public string Name { get; set; } = name;
    public string Image { get; set; } = image;
  }

}
