using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace BeautifulRestApi.Models
{
    public class Link
    {
        public const string GetMethod = "GET";
        public const string PostMethod = "POST";
        public const string DeleteMethod = "DELETE";

        public static RouteLink To(string routeName, object routeValues = null)
            => new RouteLink
            {
                RouteName = routeName,
                RouteValues = routeValues,
                Method = GetMethod,
                Relations = null
            };

        public static RouteLink ToCollection(string routeName, object routeValues = null)
            => new RouteLink
            {
                RouteName = routeName,
                RouteValues = routeValues,
                Method = GetMethod,
                Relations = new string[] { Collection<Link>.CollectionRelation }
            };

        public static Link ToForm(
            string routeName,
            object routeValues = null,
            string method = PostMethod,
            params string[] relations)
            => new RouteLink
            {
                RouteName = routeName,
                RouteValues = routeValues,
                Method = method,
                Relations = relations
            };

        /// <summary>
        /// Gets or sets the link target (href).
        /// </summary>
        /// <value>The link target (href).</value>
        [JsonProperty(Order = -4)]
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets the method used when following the link.
        /// </summary>
        /// <value>The method used when following the link.</value>
        [JsonProperty(Order = -3, NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(GetMethod)]
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the optional link relations.
        /// </summary>
        /// <value>The link relations.</value>
        [JsonProperty(Order = -2, PropertyName = "rel", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Relations { get; set; }
    }
}
