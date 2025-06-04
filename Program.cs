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

      var hotel1 = new Hotel("Amare hall", "/website/images/amare.jpg", 1, "");
      hotel1.MapUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3386.6736842066593!2d34.78063582453237!3d31.91546237403552!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x1502b791e7e4f8c3%3A0x8f84eb65bc8e78a2!2zQU1BUkUgLSDXkNee15DXqNeU!5e0!3m2!1siw!2sil!4v1747982522052!5m2!1siw!2sil";
      database.Hotels.Add(hotel1);

      var hotel2 = new Hotel("Arca hall", "/website/images/Arca.jpg", 1, "");
      hotel2.MapUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3380.0201132568072!2d34.775296024524685!3d32.09574207395565!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x151d4c0a8d208b2d%3A0x6b68746c79d3c3f0!2z15DXqNen15QgLSBBcmNhIHdlZGRpbmcgY2x1Yg!5e0!3m2!1siw!2sil!4v1748413490387!5m2!1siw!2sil";
      database.Hotels.Add(hotel2);

      var hotel3 = new Hotel("Trask hall", "/website/images/Trask.jpg", 1, "");
      hotel3.MapUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3379.7848143876413!2d34.778304724524425!3d32.10210097395298!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x151d4cc868ca94e7%3A0x85f2f1f0756c12ba!2z15jXqNeQ16HXpyAtIFRyYXNrINeQ15XXnNedINeQ15nXqNeV16LXmdedINeR16rXnCDXkNeR15nXkQ!5e0!3m2!1siw!2sil!4v1748414171051!5m2!1siw!2sil";
      database.Hotels.Add(hotel3);

      database.Cities.Add(new City("Petah Tikva", "/website/images/petah_tikva.jpg"));

      var hotel4 = new Hotel("Amore hall", "/website/images/Amore.jpg", 2, "");
      hotel4.MapUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3379.670837345526!2d34.89511102452425!3d32.10518077395153!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x151d37c1d6b7f85b%3A0xa881c6bafd97527f!2z15DXldec150g15DXmdeo15XXoteZ150g15HXpNeq15cg16rXp9eV15XXlCAtINeQ157Xldeo15QgfCBBbW9yZQ!5e0!3m2!1siw!2sil!4v1748586531769!5m2!1siw!2sil";
      database.Hotels.Add(hotel4);

      var hotel5 = new Hotel("Terminal hall", "/website/images/Terminal.jpg", 2, "");
      hotel5.MapUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3380.353339822254!2d34.85685012452508!3d32.08673477395974!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x151d4bb76abe479f%3A0x8b16afcf4e39b5f8!2z15jXqNee15nXoNecINeQ15nXqNeV16LXmdedIC0g15DXldec150g15DXmdeo15XXoteZ150g15HXnteo15vXlg!5e0!3m2!1siw!2sil!4v1748587283358!5m2!1siw!2sil";
      database.Hotels.Add(hotel5);
      
      
      var hotel6 = new Hotel("Clay hall", "/website/images/Clay.jpg", 2, "");
      hotel6.MapUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3379.6728728676585!2d34.89969292452429!3d32.10512577395153!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x151d379d25a69953%3A0xf759ce0302540ce4!2z16fXnNeZ15kg15DXmdeo15XXoteZ150!5e0!3m2!1siw!2sil!4v1748587578686!5m2!1siw!2sil";
      database.Hotels.Add(hotel6);

      database.Cities.Add(new City("Haifa", "/website/images/Haifa.jpg"));

       var hotel7 = new Hotel("Garden hall", "/website/images/Garden.jpg", 3, "");
      hotel7.MapUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3354.0105523627703!2d34.967789524495004!3d32.79197947365739!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x151dbb957c09c97d%3A0x1b9137f7a2bcf22f!2z15LXnyDXkNeZ16jXldei15nXnSDXkteQ16jXk9efINeX15nXpNeUINee16jXm9eWINen15XXoNeS16jXodeZ150!5e0!3m2!1siw!2sil!4v1749015687763!5m2!1siw!2sil";
      database.Hotels.Add(hotel7);

      var hotel8 = new Hotel("Bloom hall", "/website/images/Bloom.jpg", 3, "");
      hotel8.MapUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3353.038042643407!2d35.05721992449384!3d32.817756973646674!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x151db1435e738eeb%3A0xdde490f8ddb6c042!2z15HXnNeV150g15fXmdek15QgVEhFIEJMT09N!5e0!3m2!1siw!2sil!4v1749015800549!5m2!1siw!2sil";
      database.Hotels.Add(hotel8);
      
      
      var hotel9 = new Hotel("Kala hall", "/website/images/Kala.webp", 3, "");
      hotel9.MapUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3353.003538199879!2d35.05556687449379!3d32.818671223646305!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x151db0b3297ba995%3A0xdaa976b7d52ec343!2z16fXkNec15Qg15DXqNeV16LXmded!5e0!3m2!1siw!2sil!4v1749015888057!5m2!1siw!2sil";
      database.Hotels.Add(hotel9);
      
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
            var userExists = database.Users.Any(user => user.Username == username);
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
            var user = database.Users.First(user => user.Username == username && user.Password == password);
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
            var hotels = database.Hotels.Where(hotel => hotel.CityId == cityId).ToArray();
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
            var reservations = database.Reservations.Where(res => res.HotelId == hotelId).Select(res => res.Date).ToArray();
            response.Send(reservations);
          }
          else if (request.Path == "addReservation")
          {
            var (date, userId, hotelId) = request.GetBody<(string, string, int)>();
            var exists = database.Reservations.Any(res => res.HotelId == hotelId && res.Date == date);
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
          else if (request.Path == "getRating")
          {
            var (userId, hotelId) = request.GetBody<(string, int)>();
            var value = database.Ratings.FirstOrDefault(rating => rating.UserId == userId && rating.HotelId == hotelId)?.Value;
            response.Send(value);
          }
          else if (request.Path == "rate")
          {
            var (rating, userId, hotelId) = request.GetBody<(double, string, int)>();
            var existrating = database.Ratings.FirstOrDefault(rating => rating.UserId == userId && rating.HotelId == hotelId);
            if (existrating != null)
            {
              existrating.Value = rating;
            }
            else
            {
              var newRating = new Rating(rating, userId, hotelId);
              database.Ratings.Add(newRating);
            }
            database.SaveChanges();
            response.Send("Rating added");
          }
          else if (request.Path == "getAverage")
          {
            var hotelId = request.GetBody<int>();
            var average = database.Ratings.Where(rating => rating.HotelId == hotelId).Average(rating => rating.Value);
            response.Send(average);
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
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<City> Cities { get; set; } = default!;
    public DbSet<Hotel> Hotels { get; set; } = default!;
    public DbSet<Reservation> Reservations { get; set; } = default!;
    public DbSet<Rating> Ratings { get; set; } = default!;
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

  class Hotel(string name, string image, int cityId, string mapUrl)
  {
    [Key] public int Id { get; set; } = default!;
    public string Name { get; set; } = name;
    public string Image { get; set; } = image;
    public int CityId { get; set; } = cityId;
    public string MapUrl { get; set; } = mapUrl;
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

  class Rating(double value, string userId, int hotelId)
  {
    [Key] public int Id { get; set; } = default!;
    public double Value { get; set; } = value;
    public string UserId { get; set; } = userId;
    [ForeignKey("UserId")] public User User { get; set; } = default!;
    public int HotelId { get; set; } = hotelId;
    [ForeignKey("HotelId")] public Hotel Hotel { get; set; } = default!;
  }
}