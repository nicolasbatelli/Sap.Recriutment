using System.Collections.Generic;

namespace Sat.Recruitment.Api.Constants
{
    public static class UserTypesPercentages
    {
        public static IDictionary<string, decimal> UserPercentages = new Dictionary<string, decimal>()
        {
            { UserTypes.Normal, new decimal(0.12)},
            { UserTypes.SuperUser, new decimal(0.20)},
            { UserTypes.Normal_better, new decimal(0.8)},
            { UserTypes.Premium, new decimal(2)}
        };
    }
}
