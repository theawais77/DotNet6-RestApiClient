using System;
using System.Net;
using System.Threading.Tasks;

namespace HttpClientSample
{
    public class WeatherInfo
    {
        public Main main { get; set; }
        public string name { get; set; }
    }

    public class Main
    {
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int humidity { get; set; }
        public double feels_like {  get; set; }
        public double pressure {  get; set; }
    }

    public class Post
    {
        
        public int Id { get; set; }
        public string name { get; set; }
       
    }

    class Program
    {
        static void ShowWeather(WeatherInfo weather)
        {
            Console.WriteLine($"\tCity:{weather.name}\n\tMinimum Temperature: {weather.main.temp_min} C\n\tMaximum Temperature:{weather.main.temp_max} C\n\tHumidity: {weather.main.humidity}%\n\tFeels Like:{weather.main.feels_like} C\n\tAir Pressure:{weather.main.pressure} hPa");
        }

        static void ShowPost(Post post)
        {
            Console.WriteLine($"\tId: {post.Id}\tTitle: {post.name}");
        }

        static async Task Main(string[] args)
        {
            string weatherBaseAddress = "https://api.openweathermap.org/data/2.5/";
            RestApiClient weatherClient = new RestApiClient(weatherBaseAddress);


            bool exit = false;

            do
            {
                try
                {
                    Console.WriteLine("Choose an operation:");
                    Console.WriteLine("1. Get Weather Data");
                    Console.WriteLine("2. Create a Post");
                    Console.WriteLine("3. Update a Post");
                    Console.WriteLine("4. Delete a Post");
                    Console.WriteLine("5. Exit");
                    Console.Write("Enter your choice: ");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            
                            Console.WriteLine("Enter name of city for which you want to check the weather:");
                            string city = Console.ReadLine();

                            string apiKey = "cfee7a418c5cd5baaffe61d4d2526e66";
                            string units = "metric";
                            string weatherPath = $"weather?q={city}&appid={apiKey}&units={units}";

                            WeatherInfo weather = await weatherClient.GetAsync<WeatherInfo>(weatherPath);
                            ShowWeather(weather);
                            break;

                        case 2:
                           
                            Post newPost = new Post { Id = 1, name = "Rwp" };
                            Uri postUri = await weatherClient.PostAsync("posts", newPost);
                            Console.WriteLine($"Post created at {postUri}");
                            break;

                        case 3:
                            
                            Post updatedPost = new Post { Id = 1, name = "isl" };
                            Post resultPost = await weatherClient.PutAsync("posts", updatedPost);
                            ShowPost(resultPost);
                            break;

                        case 4:
                            
                            HttpStatusCode statusCode = await weatherClient.DeleteAsync("posts/1");
                            Console.WriteLine($"Post deleted (HTTP Status = {(int)statusCode})");
                            break;

                        case 5:
                            
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.WriteLine("Press any key to continue..");
                Console.ReadKey();
                
            }
            while (!exit);
        }

    }
}

