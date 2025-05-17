namespace TTM.Core.Infrastructure.OpenApi;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class SwaggerHeaderAttribute : Attribute
{
    public string HeaderName { get; }
    public string? Description { get; }
    public Guid DefaultValue { get; }
    public bool IsRequired { get; }

    public SwaggerHeaderAttribute(
        string headerName,
        Guid defaultValue,
        string? description = null,
        bool isRequired = false)
    {
        HeaderName = headerName;
        Description = description;
        DefaultValue = defaultValue;
        IsRequired = isRequired;
    }
}