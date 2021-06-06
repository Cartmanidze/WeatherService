using System;

namespace WeatherService.Helpers
{
    internal static class WindHelper
    {
        internal static string GetDirectionByDegrees(double degrees)
        {
            var directions = new[]
                {"north", "northeast", "east", "southeast", "south", "southwest", "west", "northwest"};
            degrees = degrees * 8 / 360;
            degrees = Math.Round(degrees, MidpointRounding.ToZero);
            degrees = (degrees + 8) % 8;
            return directions[(int)degrees];
        }
    }
}
