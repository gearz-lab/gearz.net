using Gearz.Core.Metadata;

namespace Gearz.Tests
{
    public class SomeMetadata : FluentMetadataProvider
    {
        public override void SetupMetadata(MetadataContext context)
        {
            // defining a group type
            var grpPhones = context.GroupType("GrpPhones")
                .Display("Phones")
                .Editor("CollapsiblePanel");

            // COMPLEX EDIT PAGE
            VirtualProperty<bool> vpHasPhone3;
            context.EntityView<ComplexObjectSample>()
                .Editor("MetaTabbedEditor")
                .Property(
                    obj => obj.ChildObject,
                    p => p
                        // OPTIONAL: override the default editor for the type of the `ChildObject`
                        .Editor("ComplexChildEditor"))

                // creating a virtual property... does not exist in the view-model class, but exists in the view-domain
                .Property<bool>(
                    "HasPhone2",
                    pcx => pcx
                        .Display("Has secondary phone"))

                // ALTERNATIVE: creating a virtual property... does not exist in the view-model class, but exists in the view-domain
                .Property<bool>(
                    "HasPhone3",
                    out vpHasPhone3,
                    p => p
                        .Display("Has third phone"))

                .Group(
                    grpPhones,
                    "Phones",
                    gcx => gcx
                        // OPTIONAL: override the default editor of the group type
                        .Editor("CustomPhonesCollapsiblePanel")

                        // OPTIONAL: override the default display name of the group type
                        .Display("Phones ABCD")
                        .SomeOtherAttribute("Xpto")
                        .Hint("collapsed", false)

                        // adding child elements
                        .Property(
                            proc => proc.Phone1,
                            spcx => spcx
                                // defining the display name of the property
                                .Display("Main Phone"))

                        .Property(
                            proc => proc.Phone2,
                            spcx => spcx
                                .InvisibleWhen("!HasPhone2")) // text based reference to virtual prop

                        .Property(
                            proc => proc.Phone3,
                            spcx => spcx
                                .InvisibleWhen(p => !vpHasPhone3.Value)) // strongly typed reference to virtual prop

                        .Property(proc => proc.Office.Phone)

                        // referring the group-type by name... should it be possible?
                        .Group(
                            "GrpPhones",
                            "SecondaryPhones",
                            sgcx => sgcx
                                .Display("Other phones")
                                .Hint("collapsed", true)
                                .Property(proc => proc.Office.SecondaryPhone)
                        )
                );
        }
    }

    public class ComplexObjectSample
    {
        public object ChildObject { get; set; }
        public object Phone1 { get; set; }
        public object Phone2 { get; set; }
        public object Phone3 { get; set; }
        public Office Office { get; set; }
    }

    public class Office
    {
        public object Phone { get; set; }
        public object SecondaryPhone { get; set; }
    }
}