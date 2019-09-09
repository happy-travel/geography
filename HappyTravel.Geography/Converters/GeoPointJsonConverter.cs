using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HappyTravel.Geography.Converters
{
    public class GeoPointJsonConverter : JsonConverter 
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var target = (GeoPoint) value;

            writer.WriteStartObject();

            writer.WritePropertyName("longitude");
            writer.WriteValue(target.Longitude);

            writer.WritePropertyName("latitude");
            writer.WriteValue(target.Latitude);

            writer.WriteEndObject();
        }


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var jObject = JObject.Load(reader);

            double latitude = default;
            double longitude = default;
            foreach (var child in jObject.Children())
            {
                var name = child.First.Path;

                if (LatitudeSynonyms.Contains(name))
                {
                    latitude = child.First.Value<double>();
                    continue;
                }

                if (LongitudeSynonyms.Contains(name))
                    longitude = child.First.Value<double>();
            }

            return new GeoPoint(longitude, latitude);
        }


        private static readonly HashSet<string> LatitudeSynonyms = new HashSet<string>{"Latitude", "latitude", "Lat", "lat"};
        private static readonly HashSet<string> LongitudeSynonyms = new HashSet<string>{"Longitude", "longitude", "Lon", "lon", "Lng", "lng"};


        public override bool CanConvert(Type objectType) => objectType == typeof(GeoPoint);
    }
}
