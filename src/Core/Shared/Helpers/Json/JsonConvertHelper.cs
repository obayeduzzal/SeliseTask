using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TTM.Core.Shared.Helpers.Json;

public static class JsonConvertHelper
{
    #region public methods

    /// <summary>
    /// Serializes object to json structure.
    /// </summary>
    /// <param name="obj">The object that needs to be serialized.</param>
    /// <param name="isCamelCase">Determines if the serialized object should follow camel case convention.</param>
    /// <returns></returns>
    public static string Serialize(this object obj, bool isCamelCase = false)
       => System.Text.Json.JsonSerializer.Serialize(obj, options: SetJsonOptions(isCamelCase));

    /// <summary>
    /// Converts a json string to a object.
    /// </summary>
    /// <typeparam name="T">String that needs to be de-serialized.</typeparam>
    /// <param name="obj"></param>
    /// <param name="isCamelCase">Determines if the serialized object should follow camel case convention.</param>
    /// <returns></returns>
    public static T Deserialize<T>(this string obj, bool isCamelCase = false)
        => System.Text.Json.JsonSerializer.Deserialize<T>(obj, options: SetJsonOptions(isCamelCase))!;
    #endregion

    #region Private Methods
    private static JsonSerializerOptions SetJsonOptions(bool isCamelCase)
    {
        if (isCamelCase)
        {
            return new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new JsonStringEnumConverter() }
            };
        }
        else
        {
            return new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                Converters = { new JsonStringEnumConverter() }
            };
        }
    }
    #endregion
}
