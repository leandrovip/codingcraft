using System.Collections.ObjectModel;

namespace CodingCraftEx04.Api.Areas.HelpPage.ModelDescriptions
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public Collection<EnumValueDescription> Values { get; private set; }

        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }
    }
}