using System;
using JetBrains.Annotations;

namespace Gearz.Core.Metadata
{
    public class VirtualProperty<T>
    {
        /// <summary>
        /// Gets the name of the property represented by this <see cref="VirtualProperty{T}"/>.
        /// </summary>
        [UsedImplicitly]
        public string PropertyName { get; private set; }

        public VirtualProperty(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        /// <summary>
        /// Gets the value of the property represented by this <see cref="VirtualProperty{T}"/>.
        /// Can only be used inside an expression tree.
        /// </summary>
        public T Value
        {
            get { throw new NotImplementedException("This property is only meant to be used inside expression trees."); }
        }
    }
}