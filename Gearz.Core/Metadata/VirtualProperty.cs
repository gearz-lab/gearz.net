using System;

namespace Gearz.Core.Metadata
{
    public class VirtualProperty<T>
    {
        public string PropertyName { get; private set; }

        public VirtualProperty(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        public T Value
        {
            get { throw new NotImplementedException("This property is only meant to be used inside expression trees."); }
        }
    }
}