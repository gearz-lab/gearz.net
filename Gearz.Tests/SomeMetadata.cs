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
            VirtualProperty<bool> hasPhone3;
            context.EntityView<ComplexObjectViewModel>()
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
                    out hasPhone3,
                    p => p
                        .Display("Has third phone"))

                .Group(
                    "Phones",
                    gcx => gcx
                        .Template(grpPhones)

                        .InvisibleWhen(p => !p.HasPhones)

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
                                .InvisibleWhen(p => !hasPhone3.Value)) // strongly typed reference to virtual prop

                        .Property(proc => proc.Office.Phone)

                        .Group(
                            "SecondaryPhones",
                            sgcx => sgcx
                                // referring the group-template by name... should it be possible?
                                .Template("GrpPhones")
                                .Display("Other phones")
                                .Hint("collapsed", true)
                                .Property(proc => proc.Office.SecondaryPhone)

                                .Property<int>(
                                    "some",
                                    pm => pm
                                        .InvisibleWhen((pv, c) => pv.Phone3 != 0 && c.Value.Phone3 != null))
                        )
                );
        }
    }
}