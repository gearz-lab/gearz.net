using System.Dynamic;

namespace Gearz.Models
{
    public class ApplicationViewModel
    {
        public ApplicationViewModel()
        {
            this.Data = new ExpandoObject();
        }

        /// <summary>
        /// Gets or sets an object that represents the current application state.
        /// </summary>
        public object App { get; set; }

        /// <summary>
        /// Gets or sets the object that contains the application meta-data.
        /// </summary>
        public object Meta { get; set; }

        public dynamic Data { get; private set; }
    }
}