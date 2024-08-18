using System;
using Motorcycle_Group_Rides_Management_API.Helpers;

namespace Motorcycle_Group_Rides_Management_API.External
{

    public class RouteInfo
    {
        public string DistanceText { get; set; }
        public int DistanceValue { get; set; }
        public string DurationText { get; set; }
        public int DurationValue { get; set; }
    }

    public class GeocodedWaypoint
    {
        public string GeocoderStatus { get; set; }
        public string PlaceId { get; set; }
        public List<string> Types { get; set; }
    }

    public class Northeast
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class Southwest
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class Bounds
    {
        public Northeast Northeast { get; set; }
        public Southwest Southwest { get; set; }
    }

    public class Distance
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }

    public class Duration
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }

    public class EndLocation
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class StartLocation
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class Polyline
    {
        public string Points { get; set; }
    }

    public class Step
    {
        public Distance Distance { get; set; }
        public Duration Duration { get; set; }
        public EndLocation EndLocation { get; set; }
        public string HtmlInstructions { get; set; }
        public Polyline Polyline { get; set; }
        public StartLocation StartLocation { get; set; }
        public string TravelMode { get; set; }
        public string Maneuver { get; set; }
    }

    public class Leg
    {
        public Distance Distance { get; set; }
        public Duration Duration { get; set; }
        public string EndAddress { get; set; }
        public EndLocation EndLocation { get; set; }
        public string StartAddress { get; set; }
        public StartLocation StartLocation { get; set; }
        public List<Step> Steps { get; set; }
        public List<object> TrafficSpeedEntry { get; set; }
        public List<object> ViaWaypoint { get; set; }
    }

    public class Route
    {
        public Bounds Bounds { get; set; }
        public string Copyrights { get; set; }
        public List<Leg> Legs { get; set; }
        public Polyline OverviewPolyline { get; set; }
        public string Summary { get; set; }
        public List<object> Warnings { get; set; }
        public List<object> WaypointOrder { get; set; }
    }

    public class DirectionsResponse
    {
        public List<GeocodedWaypoint> GeocodedWaypoints { get; set; }
        public List<Route> Routes { get; set; }
        public string Status { get; set; }
    }


}

