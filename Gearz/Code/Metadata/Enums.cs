namespace Gearz.Code.Metadata
{
    /// <summary>
    /// A property type, as the Type type cannot be represented in JavaScript
    /// </summary>
    public enum PropertyType
    {
        String = 0,
        Int = 1,
        Float = 2,
        DateTime = 3,
        Boolean = 4,
        Entity = 20
    }
}