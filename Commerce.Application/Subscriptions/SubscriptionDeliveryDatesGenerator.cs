using Commerce.Domain.Entities;
using Commerce.Domain.Enums;
using Dates.Recurring;
using Dates.Recurring.Builders;
using Dates.Recurring.Type;
using System;
using System.Collections.Generic;

namespace Commerce.Application.Subscriptions
{
    public class SubscriptionDeliveryDatesGenerator
    {
        /// <remarks>
        /// Make sure to pass dates without time.
        /// </remarks>
        public IEnumerable<(DateTime Start, DateTime? End)> GetDeliveryDatesFromSubscription(Subscription subscription, DateTime? startDate = null, DateTime? endDate = null)
        {
            DateTime? after = subscription.StartDate;

            (DateTime Start, DateTime? End)? current = null;

            do
            {
                current = GetDeliveryDate(subscription, after.GetValueOrDefault());
                after = current?.Start;

                yield return current.GetValueOrDefault();
            }
            while (current != null);
        }

        /// <remarks>
        /// Make sure to pass date without time.
        /// </remarks>
        public (DateTime Start, DateTime? End)? GetDeliveryDate(Subscription subscription, DateTime? after)
        {
            IRecurring? recurring = null;

            var subscriptionPlan = subscription.SubscriptionPlan;

            //var startDate = subscription.StartDate.Date;
            //var endDate = subscription.EndDate?.Date;

            if (subscription.StartDate >= subscription.EndDate)
            {
                throw new Exception("EndDate cannot occur before StartDate.");
            }

            StartingBuilder? startingBuilder = Recurs.Starting(subscription.StartDate);

            Console.WriteLine(subscriptionPlan.GetDescription());

            if (subscriptionPlan.Recurrence == Recurrence.Daily)
            {
                var dailyBuilder = startingBuilder
                    .Every(subscriptionPlan.EveryDays.GetValueOrDefault())
                    .Days();

                if (subscription.EndDate != null)
                {
                    dailyBuilder = dailyBuilder.Ending(subscription.EndDate.GetValueOrDefault());
                }

                recurring = dailyBuilder.Build();
            }
            else if (subscriptionPlan.Recurrence == Recurrence.Weekly)
            {
                var weeklyBuilder = startingBuilder
                     .Every(subscriptionPlan.EveryWeeks.GetValueOrDefault())
                     .Weeks()
                     .OnDays(MapWeekDaysToDays(subscriptionPlan.OnWeekDays.GetValueOrDefault()));

                if (subscription.EndDate != null)
                {
                    weeklyBuilder = weeklyBuilder.Ending(subscription.EndDate.GetValueOrDefault());
                }

                recurring = weeklyBuilder.Build();
            }
            else if (subscriptionPlan.Recurrence == Recurrence.Monthly)
            {
                var monthsBuilder = startingBuilder
                    .Every(subscriptionPlan.EveryMonths.GetValueOrDefault())
                    .Months();

                if (subscriptionPlan.OnDay != null)
                {
                    if (subscriptionPlan.OnDayOfWeek == null)
                    {
                        monthsBuilder = monthsBuilder.OnDay(subscriptionPlan.OnDay.GetValueOrDefault());
                    }
                    else
                    {
                        monthsBuilder = monthsBuilder.OnOrdinalWeek((Ordinal)subscriptionPlan.OnDay.GetValueOrDefault());
                        monthsBuilder = monthsBuilder.OnDay(subscriptionPlan.OnDayOfWeek.GetValueOrDefault());
                    }
                }
                else
                {
                    throw new Exception($"SubscriptionPlan {subscriptionPlan.Id} lacks OnDay.");
                }

                if (subscription.EndDate != null)
                {
                    monthsBuilder = monthsBuilder.Ending(subscription.EndDate.GetValueOrDefault());
                }

                recurring = monthsBuilder.Build();
            }
            else if (subscriptionPlan.Recurrence == Recurrence.Yearly)
            {
                var yearsBuilder = startingBuilder
                 .Every(subscriptionPlan.EveryYears.GetValueOrDefault())
                 .Years();

                if (subscriptionPlan.OnDay != null)
                {
                    if (subscriptionPlan.InMonth != null)
                    {
                        if (subscriptionPlan.OnDayOfWeek == null)
                        {
                            yearsBuilder = yearsBuilder.OnMonths((Dates.Recurring.Month)subscriptionPlan.InMonth.GetValueOrDefault());
                            yearsBuilder = yearsBuilder.OnDay(subscriptionPlan.OnDay.GetValueOrDefault());
                        }
                        else
                        {
                            yearsBuilder = yearsBuilder.OnMonths((Dates.Recurring.Month)subscriptionPlan.InMonth.GetValueOrDefault());
                            yearsBuilder = yearsBuilder.OnOrdinalWeek((Ordinal)subscriptionPlan.OnDay.GetValueOrDefault());
                            yearsBuilder = yearsBuilder.OnDay(subscriptionPlan.OnDayOfWeek.GetValueOrDefault());
                        }
                    }
                    else
                    {
                        throw new Exception($"SubscriptionPlan {subscriptionPlan.Id} lacks InMonth.");
                    }
                }
                else
                {
                    throw new Exception($"SubscriptionPlan {subscriptionPlan.Id} lacks OnDay.");
                }

                if (subscription.EndDate != null)
                {
                    yearsBuilder = yearsBuilder.Ending(subscription.EndDate.GetValueOrDefault());
                }

                recurring = yearsBuilder.Build();
            }

            if (recurring == null)
                throw new Exception();

            after = recurring.Next(after.GetValueOrDefault());

            if (after != null)
            {
                var startDateTime = after.GetValueOrDefault().Add(subscription.SubscriptionPlan.StartTime);

                DateTime? endDateTime = null;

                if (subscription.SubscriptionPlan.Duration != null)
                {
                    // Duration might be Null (Unknown)

                    endDateTime = startDateTime.Add(subscription.SubscriptionPlan.Duration.GetValueOrDefault());
                }

                return (
                    startDateTime,
                    endDateTime);
            }

            return null;
        }

        private static Day MapWeekDaysToDays(WeekDays weekDays)
        {
            Day day = 0;

            if (weekDays.HasFlag(WeekDays.Monday))
            {
                day |= Day.MONDAY;
            }
            if (weekDays.HasFlag(WeekDays.Tuesday))
            {
                day |= Day.TUESDAY;
            }
            if (weekDays.HasFlag(WeekDays.Wednesday))
            {
                day |= Day.WEDNESDAY;
            }
            if (weekDays.HasFlag(WeekDays.Thursday))
            {
                day |= Day.THURSDAY;
            }
            if (weekDays.HasFlag(WeekDays.Friday))
            {
                day |= Day.FRIDAY;
            }
            if (weekDays.HasFlag(WeekDays.Saturday))
            {
                day |= Day.SATURDAY;
            }

            return day;
        }
    }
}
