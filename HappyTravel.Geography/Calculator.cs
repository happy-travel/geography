using System;

namespace HappyTravel.Geography
{
    public static class Calculator
    {
        public static double CalculateDistance(in GeoPoint geoPoint1, in GeoPoint geoPoint2)
            => CalculateDistance(geoPoint1.Longitude, geoPoint1.Latitude, geoPoint2.Longitude, geoPoint2.Latitude);


        public static double CalculateDistance(double longitude1, double latitude1, double longitude2, double latitude2)
        {
            var latitudeDelta = ToRadians(latitude2 - latitude1);
            var longitudeDelta = ToRadians(longitude2 - longitude1);

            latitude1 = ToRadians(latitude1);
            latitude2 = ToRadians(latitude2);

            // Haversine formula:
            // a = sin²(Δφ/2) + cos φ1 ⋅ cos φ2 ⋅ sin²(Δλ/2)
            // c = 2 ⋅ atan2( √a, √(1−a) )
            // d = R ⋅ c
            var halfChordLengthSquare = Math.Sin(latitudeDelta / 2) * Math.Sin(latitudeDelta / 2) +
                Math.Sin(longitudeDelta / 2) * Math.Sin(longitudeDelta / 2) * Math.Cos(latitude1) * Math.Cos(latitude2);
            var angularDistance = 2 * Math.Atan2(Math.Sqrt(halfChordLengthSquare), Math.Sqrt(1 - halfChordLengthSquare));

            return EarthRadiusInKm * angularDistance * 1000;


            double ToRadians(double target) => target * Math.PI / 180;
        }


        private const int EarthRadiusInKm = 6371;
    }
}