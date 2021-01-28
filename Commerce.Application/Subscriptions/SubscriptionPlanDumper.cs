using Commerce.Domain.Entities;
using System;

namespace Commerce.Application.Subscriptions
{
    static class SubscriptionPlanDumper
    {
        public static void Dump(this SubscriptionPlan subscriptionPlan)
        {
            Console.WriteLine(subscriptionPlan.GetDescription());
        }
    }
}
