using Commerce.Domain.Enums;
using Commerce.Domain.ValueObjects;
using System;

namespace Commerce.Domain.Entities
{
    public class SubscriptionPlan
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public Person? Person { get; set; } // This Subscription belongs to a Person

        public Organization? Organization { get; set; }

        public Product? Product { get; set; } // This Subscription belongs to a Product

        public CurrencyAmount? Price { get; set; } // This Subscription belongs to a Product

        public bool AutoRenew { get; set; }
        public Recurrence Recurrence { get; set; }

        //public bool RescheduleWhenOnWeekend { get; set; } = true;

        public int? EveryDays { get; set; }

        public int? EveryWeeks { get; set; }

        public WeekDays? OnWeekDays { get; set; }

        public int? EveryMonths { get; set; } // Every two months

        public int? EveryYears { get; set; }

        public int? OnDay { get; set; } // 3rd of January. If OnDayOfWeek set to i.e. Tuesday, 3rd Tuesday

        public DayOfWeek? OnDayOfWeek { get; set; }

        public Month? InMonth { get; set; }

        public TimeSpan StartTime { get; set; }

        //public TimeSpan? StartEndTime { get; set; }

        //public TimeSpan? EndStartTime { get; set; }

        public TimeSpan? Duration { get; set; }

        //public TimeSpan? DurationStretch { get; set; }


        public SubscriptionPlan WithName(string name)
        {
            Name = name;

            return this;
        }
        public SubscriptionPlan WithDescription(string description)
        {
            Description = description;

            return this;
        }

        public SubscriptionPlan WithEndTime(TimeSpan value)
        {
            Duration = value - StartTime;

            return this;
        }
    }
}
