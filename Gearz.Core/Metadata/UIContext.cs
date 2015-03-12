using System;
using System.Diagnostics.CodeAnalysis;

namespace Gearz.Core.Metadata
{
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Classes have the same name.")]
    public class UIContext<T, TParentUIContext> : UIContext,
        IUIContext<T, TParentUIContext>
        where TParentUIContext : IUIContext
    {
        /// <summary>
        /// Gets the value of the current property.
        /// </summary>
        public T Value
        {
            get { throw new NotImplementedException("This property is only meant to be used inside expression trees."); }
        }

        /// <summary>
        /// Gets the parent context, if one exits.
        /// </summary>
        public TParentUIContext Parent
        {
            get { throw new NotImplementedException("This property is only meant to be used inside expression trees."); }
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Classes have the same name.")]
    public class UIContext : IUIContext
    {
        /// <summary>
        /// Gets the name of the current property in the parent object.
        /// </summary>
        public string Property
        {
            get { throw new NotImplementedException("This property is only meant to be used inside expression trees."); }
        }
    }
}