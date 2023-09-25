using Tricentis.TCAddOns;

namespace ToscaTCCountReport
{
    // TCAddOn: This abstract class is used to identify the dll as an AddOn and this needs to be inherited and implemented in the AddOn assemmbly.
    public class RibbonAddOn : TCAddOn
    {
        // This property is used to identify the AddOn
        public override string UniqueName => "AddOn";

        // This is the localized AddOn name and it will be used in the context menu and the main menu.
        public override string DisplayedName => "Custom Reports";
    }
}