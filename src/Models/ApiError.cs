using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace BeautifulRestApi.Models
{
    public sealed class ApiError
    {
        public const string ModelBindingErrorMessage = "Invalid parameters.";

        public ApiError()
        {
        }

        public ApiError(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Creates a new <see cref="ApiError"/> from the result of a model binding attempt.
        /// The first model binding error (if any) is placed in the <see cref="Detail"/> property.
        /// </summary>
        /// <param name="modelState"></param>
        public ApiError(ModelStateDictionary modelState)
        {
            Message = ModelBindingErrorMessage;

            Detail = modelState
                .FirstOrDefault(x => x.Value.Errors.Any())
                .Value?.Errors?.FirstOrDefault()?.ErrorMessage;
        }

        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Detail { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue("")]
        public string StackTrace { get; set; }

    }
}
