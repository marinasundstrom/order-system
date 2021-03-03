using System;
using Commerce.Domain.Enums;

namespace Commerce.Domain.Entities
{
    public class BillingPlan
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public Recurrence Recurrence { get; set; }

        public int? EveryDays { get; set; }

        public int? EveryWeeks { get; set; }

        public WeekDays? OnWeekDays { get; set; }

        public int? EveryMonths { get; set; } // Every two months

        public int? EveryYears { get; set; }

        public int? OnDay { get; set; } // 3rd of January. If OnDayOfWeek set to i.e. Tuesday, 3rd Tuesday

        public DayOfWeek? OnDayOfWeek { get; set; }

        public Month? InMonth { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan? Duration { get; set; }
    }
}
