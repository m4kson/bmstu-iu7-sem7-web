using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.View.Enums;


namespace ProdMonitor.GUI.Helpers
{
    public static class SexTypeConverter
    {
        public static SexTypeView ToView(this SexType domainSexType)
        {
            return domainSexType switch
            {
                SexType.Male => SexTypeView.Male,
                SexType.Female => SexTypeView.Female,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public static SexType ToDomain(this SexTypeView viewSexType)
        {
            return viewSexType switch
            {
                SexTypeView.Male => SexType.Male,
                SexTypeView.Female => SexType.Female,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

}
