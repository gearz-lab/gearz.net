namespace Gearz.Core.Metadata
{
    /// <summary>
    /// Represents a named group or template.
    /// </summary>
    public interface INamedGroupOrTemplate
    {
        /// <summary>
        /// Gets the group name.
        /// </summary>
        string Name { get; }
    }
}