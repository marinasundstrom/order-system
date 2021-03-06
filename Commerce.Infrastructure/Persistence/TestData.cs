using Commerce.Domain.Entities;
using Commerce.Domain.Enums;
using Commerce.Application.Subscriptions;
using Commerce.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Commerce.Infrastructure.Persistence
{
    public static class TestData
    {
        private static SubscriptionPlan[]? subscriptionPlans = null;
        private static Product? product1 = null;
        private static Product? product2 = null;
        private static Product? product3 = null;
        private static Product? product4 = null;
        private static Domain.Entities.Object? object1 = null;
        private static Domain.Entities.Object? object2 = null;

        /// <summary>
        /// Fredagskaka
        /// </summary>
        /// <returns></returns>
        public static Product Product1 => product1 ??= new Product()
        {
            Name = "Fredagskaka",
            Description = "Fredagens kaka",
            ProductType = ProductType.Good,
            Category = ProductCategory.Patries,
            Price = new CurrencyAmount()
            {
                Currency = Currency.SEK,
                Value = 15.00m
            }
        };

        /// <summary>
        /// Cheesecake
        /// </summary>
        /// <returns></returns>
        public static Product Product2 => product2 ??= new Product()
        {
            Name = "Cheesecake",
            ProductType = ProductType.Good,
            Category = ProductCategory.Patries,
            Price = new CurrencyAmount()
            {
                Currency = Currency.SEK,
                Value = 25m
            }
        };

        /// <summary>
        /// Bryggkaffe
        /// </summary>
        /// <returns></returns>
        public static Product Product3 => product3 ??= new Product()
        {
            Name = "Bryggkaffe",
            ProductType = ProductType.Good,
            Category = ProductCategory.Beverages,
            Price = new CurrencyAmount()
            {
                Currency = Currency.SEK,
                Value = 19.00m
            }
        };

        /// <summary>
        /// Capuccino
        /// </summary>
        /// <returns></returns>
        public static Product GetProduct4()
        {
            return product4 ??= new Product()
            {
                Name = "Capuccino",
                ProductType = ProductType.Good,
                Category = ProductCategory.Beverages,
                Price = new CurrencyAmount()
                {
                    Currency = Currency.SEK,
                    Value = 20.00m
                }
            };
        }

        public static IEnumerable<SubscriptionPlan> GetSubscriptionPlans()
        {
            return subscriptionPlans ?? (subscriptionPlans = new[] {

                // Every second day
                new SubscriptionPlan
                    {
                        Name = "Daily subscription",
                        Description = "",
                        Recurrence = Recurrence.Daily,
                        EveryDays = 2,
                        StartTime = TimeSpan.Parse("08:15"),
                        Duration = TimeSpan.Parse("00:30")
                    },

                // Every 1 weeks on Friday
                new SubscriptionPlan
                    {
                        Name = "Friday every 1 week subscription",
                        Description = "",
                        Recurrence = Recurrence.Weekly,
                        EveryWeeks = 1,
                        OnWeekDays = WeekDays.Friday,
                        StartTime = TimeSpan.Parse("23:00"),
                        Duration = TimeSpan.Parse("1:30")
                    },

                // Every 1 week on Tuesday and Thursday
                SubscriptionPlanFactory
                    .CreateWeeklyPlan(1, WeekDays.Tuesday | WeekDays.Thursday, TimeSpan.Parse("16:00"), null)
                    .WithName("Bi-weekly subscription")
                    .WithEndTime(TimeSpan.Parse("17:00")),

                //new SubscriptionPlan
                //    {
                //        Name = "Bi-weekly subscription",
                //        Description = "",
                //        Recurrence = Recurrence.Weekly,
                //        EveryWeeks = 1,
                //        OnWeekDays = WeekDays.Tuesday | WeekDays.Thursday,
                //        StartTime = TimeSpan.Parse("08:00"),
                //        Duration = TimeSpan.Parse("00:45")
                //    },
                //subscription.SubscriptionPlan.SetEndTime(TimeSpan.Parse("09:00"));

                //Every 2 months on the 15th
                SubscriptionPlanFactory
                    .CreateMonthlyPlan(1, 1, DayOfWeek.Tuesday, TimeSpan.Parse("10:30"), TimeSpan.Parse("00:30"))
                    .WithName("Monthly subscription 1"),

                //new SubscriptionPlan
                //    {
                //        Name = "Monthly subscription 1",
                //        Description = "",
                //        Recurrence = Recurrence.Monthly,
                //        EveryMonths = 2,
                //        OnDay = 15,
                //        StartTime = TimeSpan.Parse("23:30"),
                //        Duration = TimeSpan.Parse("1:00")
                //    },

                // Every 1 month on the 2nd Tuesday 
                SubscriptionPlanFactory
                    .CreateMonthlyPlan(1, 1, DayOfWeek.Tuesday, TimeSpan.Parse("07:30"), null) // , TimeSpan.Parse("00:45"))
                    .WithName("Monthly subscription 2"),

                //new SubscriptionPlan
                //    {
                //        Name = "Monthly subscription 2",
                //        Description = "",
                //        Recurrence = Recurrence.Monthly,
                //        EveryMonths = 1,
                //        OnDay = 2,
                //        OnDayOfWeek = DayOfWeek.Tuesday,
                //        StartTime = TimeSpan.Parse("08:00"),
                //        Duration = TimeSpan.Parse("00:45")
                //    },

                // Every 1 year on the 15th of April
                 SubscriptionPlanFactory
                    .CreateYearlyPlan(1, Month.April, 15, TimeSpan.Parse("14:30"), TimeSpan.Parse("00:30"))
                    .WithName("Yearly subscription 1"),

                 //new SubscriptionPlan
                 //   {
                 //       Name = "Yearly subscription 1",
                 //       Description = "",
                 //       Recurrence = Recurrence.Yearly,
                 //       EveryYears = 1,
                 //       OnDay = 15,
                 //       InMonth = Month.April,
                 //       StartTime = TimeSpan.Parse("08:00"),
                 //       Duration = TimeSpan.Parse("01:30")
                 //   },

                // Every 1 year on the 2rd Thursday of April
                SubscriptionPlanFactory
                    .CreateYearlyPlan(1, Month.April, 3, DayOfWeek.Thursday, TimeSpan.Parse("09:00"), TimeSpan.Parse("00:20"))
                    .WithName("Yearly subscription 2"),

                //new SubscriptionPlan
                //    {
                //        Name = "Yearly subscription 2",
                //        Description = "",
                //        Recurrence = Recurrence.Yearly,
                //        EveryYears = 1,
                //        OnDay = 3,
                //        OnDayOfWeek = DayOfWeek.Thursday,
                //        InMonth = Month.April,
                //        StartTime = TimeSpan.Parse("08:00"),
                //        Duration = TimeSpan.Parse("01:30")
                //    }
            });
        }

        public static Subscription CreateSubscription(int subcriptionPlanId, DateTime startDate, DateTime endDate)
        {
            return new Subscription()
            {
                SubscriptionPlan = GetSubscriptionPlans().ElementAt(subcriptionPlanId),
                StartDate = startDate,
                EndDate = endDate,
                Status = SubscriptionStatus.Active,
                StatusDate = DateTime.Now
            };
        }


        public static Domain.Entities.Object Object1 => object1 ??= new Domain.Entities.Object()
        {
            Name = "Object 1"
        };


        public static Domain.Entities.Object Object2 => object2 ??= new Domain.Entities.Object()
        {
            Name = "Object 2"
        };

        public static Order CreateOrderWith2Items()
        {
            var order2 = new Order()
            {
                OrderDate = DateTime.Now,
                Status = OrderStatuses.Approved,
                StatusDate = DateTime.Now,
                DeliveryDetails = new DeliveryDetails()
                {
                    Name = new Name()
                    {
                        FirstName = "Lars",
                        LastName = "Bergkvist"
                    },
                    Address = new Address()
                    {
                        Thoroughfare = "Bokdala 45",
                        Locality = "Frörup"
                    }
                }

            };

            order2.Items.Add(new OrderItem()
            {
                Product = TestData.Product1,
                Quantity = 1,
                UnitPrice = TestData.Product1.Price.Clone(),
                SubTotal = TestData.Product1.Price * 1,
                Note = "En anmärkning"
            });

            order2.Items.Add(new OrderItem()
            {
                Product = TestData.Product1,
                Quantity = 3,
                UnitPrice = TestData.Product1.Price.Clone(),
                SubTotal = TestData.Product1.Price * 3,
            });
            return order2;
        }

        public static Order CreateOrderWith2ItemsAndOneWithInlineAddress()
        {
            var order2 = new Order()
            {
                OrderDate = DateTime.Now,
                Status = OrderStatuses.Approved,
                StatusDate = DateTime.Now,
                DeliveryDetails = new DeliveryDetails()
                {
                    Name = new Name()
                    {
                        FirstName = "Gunnel",
                        LastName = "Halldén"
                    },
                    Address = new Address()
                    {
                        Thoroughfare = "Ektorp 19",
                        Locality = "Slyringe"
                    }
                },
                Note = "Test"
            };

            order2.Items.Add(new OrderItem()
            {
                Product = TestData.Product1,
                Quantity = 1,
                UnitPrice = TestData.Product1.Price.Clone(),
                SubTotal = TestData.Product1.Price * 1,
                HasDeliveryDetails = true,
                DeliveryDetails = new DeliveryDetails()
                {
                    Name = new Name()
                    {
                        FirstName = "Omar",
                        LastName = "Sharif"
                    },
                    Address = new Address()
                    {
                        Thoroughfare = "Haga 2",
                        Locality = "Barkaby"
                    }
                }
                ,
                Note = "Södra ingången"
            });

            order2.Items.Add(new OrderItem()
            {
                Product = TestData.Product1,
                Quantity = 3,
                UnitPrice = TestData.Product1.Price.Clone(),
                SubTotal = TestData.Product1.Price * 3,
            });
            return order2;
        }

        public static Order CreateOrderWith2SubscriptionItems()
        {
            var order2 = new Order()
            {
                OrderDate = DateTime.Now,
                Status = OrderStatuses.Approved,
                StatusDate = DateTime.Now
            };

            order2.Items.Add(new OrderItem()
            {
                Product = TestData.Product1,
                Quantity = 1,
                UnitPrice = TestData.Product1.Price.Clone(),
                Subscription = TestData.CreateSubscription(0, DateTime.Now.Date, DateTime.Now.Date.AddMonths(2)),
                SubTotal = TestData.Product1.Price * 1,
                HasDeliveryDetails = true,
                DeliveryDetails = new DeliveryDetails()
                {
                    Name = new Name()
                    {
                        FirstName = "Otto",
                        LastName = "Lundström"
                    },
                    Address = new Address()
                    {
                        Thoroughfare = "Stenbäcken 3",
                        Locality = "Sandskogen"
                    }
                },
                Note = "Södra ingången"
            });

            order2.Items.Add(new OrderItem()
            {
                Product = TestData.Product1,
                Quantity = 3,
                UnitPrice = TestData.Product1.Price.Clone(),
                Subscription = TestData.CreateSubscription(1, DateTime.Now.Date, DateTime.Now.Date.AddMonths(12)),
                SubTotal = TestData.Product1.Price * 3,
                HasDeliveryDetails = true,
                DeliveryDetails = new DeliveryDetails()
                {
                    Name = new Name()
                    {
                        FirstName = "Sven-Olof",
                        LastName = "Andersson"
                    },
                    Address = new Address()
                    {
                        Thoroughfare = "Byvägen 40",
                        Locality = "Eskilstorp"
                    }
                }

            });
            return order2;
        }

        public static Order CreateSubscriptionOrderWith1Item()
        {
            var order = new Order()
            {
                OrderDate = DateTime.Now,
                Status = OrderStatuses.Approved,
                StatusDate = DateTime.Now,
                Object = TestData.Object2,
                Subscription = TestData.CreateSubscription(2, DateTime.Now.Date, DateTime.Now.Date.AddMonths(12)),
                DeliveryDetails = new DeliveryDetails()
                {
                    Name = new Name()
                    {
                        FirstName = "Inga",
                        LastName = "Granström"
                    },
                    Address = new Address()
                    {
                        Thoroughfare = "Elinelundsvägen 3",
                        Locality = "Malmö"
                    }
                }

            };

            order.Items.Add(new OrderItem()
            {
                Product = TestData.Product1,
                Quantity = 0.75,
                Unit = Commerce.Domain.Enums.OrderUnit.Hour,
                UnitPrice = TestData.Product1.Price!.Clone(),
                SubTotal = TestData.Product1.Price * 0.75m
            });
            return order;
        }

        public static Order CreateSubscriptionOrderWith2Items()
        {
            var order = new Order()
            {
                OrderDate = DateTime.Now,
                Status = OrderStatuses.Approved,
                StatusDate = DateTime.Now,
                Object = TestData.Object1,
                Subscription = TestData.CreateSubscription(3, DateTime.Now.Date, DateTime.Now.Date.AddMonths(12)),
                DeliveryDetails = new DeliveryDetails()
                {
                    Name = new Name()
                    {
                        FirstName = "Carolina",
                        LastName = "Kjellander"
                    },
                    Address = new Address()
                    {
                        Thoroughfare = "Destillerigatan 4",
                        Locality = "Söderveda"
                    }
                }

            };

            order.Items.Add(new OrderItem()
            {
                Product = TestData.Product2,
                Quantity = 0.75,
                Unit = Commerce.Domain.Enums.OrderUnit.Hour,
                UnitPrice = TestData.Product2.Price.Clone(),
                SubTotal = TestData.Product2.Price * 0.75m
            });

            order.Items.Add(new OrderItem()
            {
                Product = TestData.Product3,
                Quantity = 0.75,
                Unit = Commerce.Domain.Enums.OrderUnit.Hour,
                UnitPrice = TestData.Product3.Price.Clone(),
                SubTotal = TestData.Product3.Price * 0.75m
            });

            return order;
        }

        public static Order CreateOrderWithInlineDeliveryAddressesAndInlineSubscription()
        {
            var order4 = new Order()
            {
                OrderDate = DateTime.Now,
                Status = OrderStatuses.Approved,
                StatusDate = DateTime.Now,
                DeliveryDetails = new DeliveryDetails()
                {
                    Name = new Name()
                    {
                        FirstName = "Mia",
                        LastName = "Gröndahl"
                    },
                    Address = new Address()
                    {
                        Thoroughfare = "Sesamvägen 2",
                        Locality = "Skogaholm"
                    }
                }
            };

            order4.Items.Add(new OrderItem()
            {
                Product = TestData.Product2,
                Quantity = 2,
                UnitPrice = TestData.Product2.Price!.Clone(),
                Subscription = TestData.CreateSubscription(4, DateTime.Now.Date, DateTime.Now.Date.AddMonths(5)),
                SubTotal = TestData.Product2.Price * 2,
                Note = "Test"
            });

            order4.Items.Add(new OrderItem()
            {
                Product = TestData.Product1,
                Quantity = 10,
                UnitPrice = TestData.Product1.Price!.Clone(),
                SubTotal = TestData.Product1.Price * 10,
            });

            order4.Items.Add(new OrderItem()
            {
                Product = TestData.Product3,
                Quantity = 3,
                UnitPrice = TestData.Product3.Price.Clone(),
                SubTotal = TestData.Product3.Price * 3,
            });

            order4.Items.Add(new OrderItem()
            {
                Product = TestData.Product1,
                Quantity = 1,
                UnitPrice = TestData.Product1.Price.Clone(),
                SubTotal = TestData.Product1.Price * 1,
                HasDeliveryDetails = true,
                DeliveryDetails = new DeliveryDetails()
                {
                    Name = new Name()
                    {
                        FirstName = "Paul",
                        LastName = "Ström"
                    },
                    Address = new Address()
                    {
                        Thoroughfare = "Falkabovägen",
                        Locality = "Göttorp"
                    }
                },
                Note = "foo"
            });
            return order4;
        }
    }
}
