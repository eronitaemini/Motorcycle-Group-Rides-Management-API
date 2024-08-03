using System;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Motorcycle_Group_Rides_Management_API.External
{
    public class RouteInfoService : IRouteInfo
    {

         
            private static readonly HttpClient httpClient;
            private readonly string _apiKey;
            private readonly IConfiguration _configuration;

        
        static RouteInfoService()
        {

            httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://maps.googleapis.com/")
            };


        }


        public RouteInfoService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<RouteInfo> GetRouteInfoAsync(string origin, string destination)
        {
            var apiKey = _configuration.GetValue<string>("GoogleDirectionsApi:ApiKey");

            try
            {
                var url = string.Format("maps/api/directions/json?origin={0}&destination={1}&key={2}", origin, destination, apiKey);

                var response = await httpClient.GetAsync(url);
                var result = new RouteInfo();
                var directionResponse = new DirectionsResponse();
                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    directionResponse = JsonSerializer.Deserialize<DirectionsResponse>(stringResponse, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                   

                    if (directionResponse?.Routes != null && directionResponse.Routes.Count > 0)
                    {
                        var firstLeg = directionResponse.Routes[0].Legs[0];
                        result.DistanceText = firstLeg.Distance.Text;
                        result.DistanceValue = firstLeg.Distance.Value;
                        result.DurationText = firstLeg.Duration.Text;
                        result.DurationValue = firstLeg.Duration.Value;
                    }

                }
                return result;
            }
            catch (HttpRequestException httpException)
            {
                throw new Exception("Http request failed: " + httpException.Message);
            }
            catch(Newtonsoft.Json.JsonException jsonException)
            {
                throw new Exception("JSON deserialization failed: " + jsonException.Message);
            }
            catch (Exception exception)
            {
                throw new Exception("An unexcpected error occurred: " + exception.Message);
            }

        }
    }


 }

