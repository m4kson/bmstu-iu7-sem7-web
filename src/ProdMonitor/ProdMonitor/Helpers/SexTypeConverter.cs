using System;

namespace ProdMonitor.Helpers
{
    public static class SexTypeConverter
    {
        public static SexTypeView ToView(this SexType domainSexType)
        {
            return domainSexType switch
            {
                SexType.Male => SexTypeView.Male,
                SexType.Female => SexTypeView.Female,
                SexType.Other => SexTypeView.Other,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public static SexType ToDomain(this SexTypeView viewSexType)
        {
            return viewSexType switch
            {
                SexTypeView.Male => SexType.Male,
                SexTypeView.Female => SexType.Female,
                SexTypeView.Other => SexType.Other,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

}
