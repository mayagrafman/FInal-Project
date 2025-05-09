using System.ComponentModel.DataAnnotations;
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

    var database = new Database();
    if (database.IsNewlyCreated())
    {
      database.Cities.Add(new City("Tel Aviv", "/website/images/tel_aviv.jpg"));
      database.Hotels.Add(new Hotel("Amare hall", "/website/images/amare.jpg", 1));


      database.Cities.Add(new City("Petah Tikva", "/website/images/petah_tikva.jpg"));

      database.Cities.Add(new City("Haifa", "/website/images/Haifa.jpg"));


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
           else if (request.Path == "getCities")
          {
            var cities = database.Cities.ToArray();

            response.Send(cities);
          }
             else if (request.Path == "getHotels")
          {
            var cityId = request.GetBody<int>();
            var hotels = database
              .Hotels
              .Where(hotel => hotel.CityId == cityId)
              .ToArray();
            response.Send(hotels);
          }
            else if (request.Path == "getHotel")
          {
            var hotelId = request.GetBody<int>();

            var hotel = database.Hotels.Find(hotelId);

            response.Send(hotel);
          }
                 else if (request.Path == "getDates")
          {
            var hotelId = request.GetBody<int>();
            var hotel = database.Hotels.Find(hotelId);
            var reservations = database
              .Reservations
              .Where(res => res.HotelId == hotelId)
              .Select(res => res.Date)
              .ToArray();
            response.Send(reservations);
          }
          else if (request.Path == "addReservation")
          {
            var (date, userId, hotelId) = request.GetBody<(string, string, int)>();
            var exists = database
              .Reservations
              .Any(res => res.HotelId == hotelId && res.Date == date);
            if (!exists)
            {
              database.Reservations.Add(new Reservation(date, userId, hotelId));
            }
            var success = !exists;
            response.Send(success);
          }
          else if (request.Path == "getReservations")
          {
            var userId = request.GetBody<string>();

            var reservations = database.Reservations.Where(res => res.UserId == userId).ToArray();

            response.Send(reservations);
          }
          else if (request.Path == "unbook")
          {
            var resId = request.GetBody<int>();

            var res = database.Reservations.Find(resId)!;

            database.Remove(res);
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
    public DbSet<Hotel> Hotels { get; set; } = default!;
    public DbSet<Reservation> Reservations { get; set; } = default!;


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
 
 class Hotel(string name, string image, int cityId)
{
  [Key] public int Id { get; set; } = default!;
  public string Name { get; set; } = name;
  public string Image { get; set; } = image;
  public int CityId { get; set; } = cityId;
  [ForeignKey("CityId")] public City City { get; set; } = default!;
}

class Reservation(string date, string userId, int hotelId)
{
  [Key] public int Id { get; set; } = default!;
  public string Date { get; set; } = date;
  public string UserId { get; set; } = userId;
  [ForeignKey("UserId")] public User User { get; set; } = default!;
  public int HotelId { get; set; } = hotelId;
  [ForeignKey("HotelId")] public Hotel Hotel { get; set; } = default!;
}
}
