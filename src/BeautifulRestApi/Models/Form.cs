using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace BeautifulRestApi.Models
{
    public class Form : Collection<FormField>
    {
        public Form(string path, string method, string relation, IEnumerable<FormField> fields)
        {
            Meta = new PlaceholderLink
            {
                Href = path,
                Method = method,
                Relations = new[] {relation}
            };

            Items = fields.ToArray();
        }

        public static Form FromModel<T>(string path, string method, string relation)
            where T : class, new()
        {
            var fields = typeof(T).GetTypeInfo().DeclaredProperties.Select(p =>
            {
                var attributes = p.GetCustomAttributes().ToArray();

                return new FormField
                {
                    Name = attributes.OfType<DisplayAttribute>().FirstOrDefault()?.Name ?? p.Name,
                    Required = attributes.OfType<RequiredAttribute>().Any(),
                    MaxLength = attributes.OfType<MaxLengthAttribute>().FirstOrDefault()?.Length,
                    MinLength = attributes.OfType<MinLengthAttribute>().FirstOrDefault()?.Length,
                    Type = GetType(p.PropertyType)
                };
            });

            return new Form(path, method, relation, fields);
        }

        private static readonly IReadOnlyDictionary<Type, string> TypeLookup = new Dictionary<Type, string>()
        {
            {typeof(string), "string"},
            {typeof(bool), "boolean"},
            {typeof(DateTimeOffset), "datetime"},
            {typeof(TimeSpan), "duration"},
            {typeof(int), "integer"},
        };

        private static string GetType(Type type)
        {
            var nullableUnderlying = Nullable.GetUnderlyingType(type);
            if (nullableUnderlying != null)
            {
                return GetType(nullableUnderlying);
            }

            if (type.IsArray)
            {
                return "array";
            }

            string typeString;
            TypeLookup.TryGetValue(type, out typeString);
            return typeString;
        }
    }
}
