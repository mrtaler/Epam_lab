using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicketSaleCore.Models.DAL.IRepository;
using TicketSaleCore.Models.Entities;
using System.Security.Claims;
using TicketSaleCore.AuthorizationPolit.ResourceBased;
using TicketSaleCore.Models.BLL.Interfaces;
using TicketSaleCore.Models.BLL.Infrastructure;

namespace TicketSaleCore.Models.DAL
{

    public static class DbInit
    {
        /// <summary>
        /// Database init
        /// </summary>
        /// <param name="context">set you IUnitOfWork context</param>
        /// <returns></returns>
        public static async Task AddTestData(IUnitOfWork context,
            ICityService cityService,
            IEventTypeService eventTypeService,
            IOrderStatusService orderStatusService,
            IEventService eventService,
            IVenuesService venuesService,
            ITicketsService ticketsService
            )
        {


            #region EventsType

            var eventCinema = new EventsType { NameEventsType = "Cinema" };
            if (eventTypeService.IsExists(eventCinema.NameEventsType))
            {
                eventCinema = eventTypeService.Get(eventCinema.NameEventsType);
            }
            else
            {
                try { eventTypeService.Add(eventCinema); }
                catch (BllValidationException) { }
            }

            var eventTheater = new EventsType { NameEventsType = "Theater" };
            if (eventTypeService.IsExists(eventTheater.NameEventsType))
            {
                eventTheater = eventTypeService.Get(eventTheater.NameEventsType);
            }
            else
            {
                try { eventTypeService.Add(eventTheater); }
                catch (BllValidationException) { }
            }

            var eventSport = new EventsType { NameEventsType = "Sport" };
            if (eventTypeService.IsExists(eventSport.NameEventsType))
            {
                eventSport = eventTypeService.Get(eventSport.NameEventsType);
            }
            else
            {
                try { eventTypeService.Add(eventSport); }
                catch (BllValidationException) { }
            }

            #endregion

            #region City Table Init

            var cityMinsk = new City { Name = "Minsk" };
            if (cityService.IsExists(cityMinsk.Name))
            {
                cityMinsk = cityService.Get(cityMinsk.Name);
            }
            else
            {
                try { cityService.Add(cityMinsk); }
                catch (BllValidationException) { }
            }

            var cityGomel = new City { Name = "Gomel" };
            if (cityService.IsExists(cityGomel.Name))
            {
                cityGomel = cityService.Get(cityGomel.Name);
            }
            else
            {
                try { cityService.Add(cityGomel); }
                catch (BllValidationException) { }
            }

            var cityGrodno = new City { Name = "Grodno" };
            if (cityService.IsExists(cityGrodno.Name))
            {
                cityGrodno = cityService.Get(cityGrodno.Name);
            }
            else
            {
                try { cityService.Add(cityGrodno); }
                catch (BllValidationException) { }
            }

            var cityVitebsk = new City { Name = "Vitebsk" };
            if (cityService.IsExists(cityVitebsk.Name))
            {
                cityVitebsk = cityService.Get(cityVitebsk.Name);
            }
            else
            {
                try { cityService.Add(cityVitebsk); }
                catch (BllValidationException) { }
            }

            var cityBrest = new City { Name = "Brest" };
            if (cityService.IsExists(cityBrest.Name))
            {
                cityBrest = cityService.Get(cityBrest.Name);
            }
            else
            {
                try { cityService.Add(cityBrest); }
                catch (BllValidationException) { }
            }

            var cityMogilev = new City { Name = "Mogilev" };
            if (cityService.IsExists(cityMogilev.Name))
            {
                cityMogilev = cityService.Get(cityMogilev.Name);
            }
            else
            {
                try { cityService.Add(cityMogilev); }
                catch (BllValidationException) { }
            }

            #endregion

            #region Order Status Table Init

            var statusWaiting = new OrderStatus { StatusName = "Waiting for conformation" };
            if (orderStatusService.IsExists(statusWaiting.StatusName))
            {
                statusWaiting = orderStatusService.Get(statusWaiting.StatusName);
            }
            else
            {
                try { orderStatusService.Add(statusWaiting); }
                catch (BllValidationException) { }
            }
            var statusConfirmed = new OrderStatus { StatusName = "Confirmed" };
            if (orderStatusService.IsExists(statusConfirmed.StatusName))
            {
                statusConfirmed = orderStatusService.Get(statusConfirmed.StatusName);
            }
            else
            {
                try { orderStatusService.Add(statusConfirmed); }
                catch (BllValidationException) { }
            }
            var statusRejected = new OrderStatus { StatusName = "Rejected" };
            if (orderStatusService.IsExists(statusRejected.StatusName))
            {
                statusRejected = orderStatusService.Get(statusRejected.StatusName);
            }
            else
            {
                try { orderStatusService.Add(statusRejected); }
                catch (BllValidationException) { }
            }

            #endregion

            var eventRandomDays = new Random(DateTime.Now.Millisecond);

            #region Event Table Init

            #region CinemaEvent

            #region Venues


            var venuesCreate = new List<Venue>
            {
              new Venue
              {
                  Name = "Moscow Cinema for test Add id db",
                  Address = "Moscow Cinema address",
                  City = cityMinsk
              },
              new Venue
                {
                Name = "Aurora Cinema test to add with seriv(not exist)",
                Address = "Aurora Cinema address",
                City = cityMinsk
            },
          new Venue
            {
                Name = "1 random Cinema in gomel",
                Address = "1 random Cinema in gomel address",
                City = cityGomel
            },
            new Venue
            {
                Name = "2 random Cinema in gomel",
                Address = "2 random Cinema in gomel address",
                City = cityGomel
            },
           new Venue
            {
                Name = "Random Cinema in grodno #1",
                Address = "Random Cinema in Grodno address #1",
                City = cityGrodno
            },
           new Venue
            {
                Name = "3 random Cinema in gomel",
                Address = "3 random Cinema in gomel address",
                City = cityGomel
            },
           new Venue
            {
                Name = "Random Cinema in grodno #2",
                Address = "Random Cinema in Grodno address #2",
                City = cityGrodno
            },
       new Venue
            {
                Name = "Random Cinema in Vitebsk #1",
                Address = "Random Cinema in Vitebsk address #1",
                City = cityVitebsk
            },
           new Venue
            {
                Name = "Random Cinema in Brest #1",
                Address = "Random Cinema in Brest address #1",
                City = cityBrest
            },
           new Venue
            {
                Name = "Random Cinema in Mogilev #1",
                Address = "Random Cinema in Mogilev address #1",
                City = cityMogilev
            },



           new Venue
            {
                Name = "Random Minsk Theater #1",
                Address = "random Theater in Minsk address #1",
                City = cityMinsk
            },
          new Venue
            {
                Name = "Random Minsk Theater #2",
                Address = "random Theater in Minsk address #2",
                City = cityMinsk
            },
           new Venue
            {
                Name = "1 random Theater in gomel",
                Address = "1 random Theater in gomel address",
                City = cityGomel
            },
           new Venue
            {
                Name = "2 random Theater in gomel",
                Address = "2 random Theater in gomel address",
                City = cityGomel
            },
            new Venue
            {
                Name = "Random TheaterEventGrodno1 in grodno #1",
                Address = "Random TheaterEventGrodno1 in Grodno address #1",
                City = cityGrodno
            },
            new Venue
            {
                Name = "Random TheaterEventVitebsk1 in Vitebsk #1",
                Address = "Random TheaterEventVitebsk1 in Vitebsk address #1",
                City = cityVitebsk
            }
        };

            var venuesUse = new List<Venue>();
            foreach (var item in venuesCreate)
            {
                if (venuesService.IsExists(item.Name))
                {
                    venuesUse.Add(venuesService.Get(item.Name));
                }
                else
                {
                    try
                    {
                        venuesService.Add(item);
                        venuesUse.Add(venuesService.Get(item.Name));
                    }
                    catch (BllValidationException) { }
                }
            }




            #endregion
            //minsk

            var events = new List<Event>
            {
                new Event
                {
                    Name = "cinemaEventMinskMoscow Test to exist add",
                    EventsType = eventCinema,
                    Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                    Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                    Description = "this is test description for first cinema event in minsk moscow cinema",
                    Venue = venuesUse[0]
                },
                new Event
                {
                    Name = "cinemaEventMinskAurora",
                    EventsType = eventCinema,
                    Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                    Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                    Description = "this is test description for second cinema event in minsk Aurora cinema",
                    Venue = venuesUse[1]
                },
                //Gomel
                new Event
                {
                    Name = "cinemaEventGomel1",
                    EventsType = eventCinema,
                    Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                    Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                    Description = "this is test description for cinema event in Gomel #1",
                    Venue = venuesUse[2]
                },
                new Event
                {
                    Name = "cinemaEventGomel2",
                    EventsType = eventCinema,
                    Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                    Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                    Description = "this is test description for second cinema event in Gomel#2",
                    Venue = venuesUse[3]
                },
                new Event
                {
                    Name = "cinemaEventGomel3",
                    EventsType = eventCinema,
                    Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                    Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                    Description = "this is test description for second cinema event in Gomel#3",
                    Venue = venuesUse[4]
                },
                //grodno
                new Event
                {
                    Name = "cinemaEventGrodno1",
                    EventsType = eventCinema,
                    Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                    Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                    Description = "this is test description for first cinema event in Grodno #1",
                    Venue = venuesUse[5]
                },
                new Event
                {
                    Name = "cinemaEventGrodno2",
                    EventsType = eventCinema,
                    Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                    Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                    Description = "this is test description for first cinema event in Grodno #2",
                    Venue = venuesUse[6]
                },
                //vitebsk
                new Event
                {
                    Name = "cinemaEventVitebsk1",
                    EventsType = eventCinema,
                    Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                    Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                    Description = "this is test description for first cinema event in Vitebsk (single)",
                    Venue = venuesUse[7]
                },
                //brest
                new Event
                {
                    Name = "cinemaEventBrest1",
                    EventsType = eventCinema,
                    Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                    Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                    Description = "this is test description for first cinema event in Brest cinema",
                    Venue = venuesUse[8]
                },
                //mogilev
                new Event
                {
                    Name = "cinemaEventMogilev1",
                    EventsType = eventCinema,
                    Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                    Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                    Description = "this is test description for first cinema event in Mogilev cinema",
                    Venue = venuesUse[9]
                },



                new Event
                {
                    Name = "TheaterEventMinsk1",
                    EventsType = eventTheater,
                    Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                    Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                    Description = "this is test description for first eventTheater in minsk",
                    Venue = venuesUse[10]
                },
                new Event
                {
                    Name = "TheaterEventMinsk2",
                    EventsType = eventTheater,
                    Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                    Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                    Description = "this is test description for second eventTheater in minsk",
                    Venue = venuesUse[11]
                },
                //Gomel

                new Event
                {
                    Name = "TheaterEventGomel1",
                    EventsType = eventTheater,
                    Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                    Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                    Description = "this is test description for TheaterEventGomel1 in Gomel #1",
                    Venue = venuesUse[12]
                },

                new Event
                {
                    Name = "TheaterEventGomel2",
                    EventsType = eventTheater,
                    Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                    Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                    Description = "this is test description for second TheaterEventGomel2 in Gomel#2",
                    Venue = venuesUse[13]
                },
                //grodno
                new Event
                {
                    Name = "TheaterEventGrodno1",
                    EventsType = eventTheater,
                    Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                    Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                    Description = "this is test description for first TheaterEventGrodno1 in Grodno #1",
                    Venue = venuesUse[14]
                },
                //vitebsk
                new Event
                {
                    Name = "TheaterEventVitebsk1",
                    EventsType = eventTheater,
                    Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                    Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                    Description = "this is test description for first TheaterEventVitebsk1 in Vitebsk (single)",
                    Venue = venuesUse[15]
                }
            };
            var eventsUse = new List<Event>();
            foreach (var item in events)
            {
                if (eventService.IsExists(item.Name))
                {
                    eventsUse.Add(eventService.Get(item.Name));
                }
                else
                {
                    try
                    {
                        eventService.Add(item);
                        eventsUse.Add(eventService.Get(item.Name));
                    }
                    catch (BllValidationException) { }
                }
            }



            #endregion

            #endregion

            AppUser user1 = context.AppUsers.ToList().First(p => p.Email.Equals("User1"));
            AppUser user2 = context.AppUsers.ToList().First(p => p.Email.Equals("User2"));
            AppUser user3 = context.AppUsers.ToList().First(p => p.Email.Equals("User3"));

            #region Tikets Table Init

            var ticketPriceRandom = eventRandomDays;
            var tickets = new List<Ticket>
            {
                new Ticket
                {
                    Event = eventsUse[0],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket1CinemaEventMinsk1 seller User 1",
                    Order = null
                }, //0
                new Ticket
                {
                    Event = eventsUse[0],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket2CinemaEventMinsk1 seller User 2",
                    Order = null
                }, //1
                new Ticket
                {
                    Event = eventsUse[0],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket3CinemaEventMinsk1 seller User 3",
                    Order = null
                }, //2
                new Ticket
                {
                    Event = eventsUse[0],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket4CinemaEventMinsk1 seller User 1",
                    Order = null
                }, //3
                new Ticket
                {
                    Event = eventsUse[0],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket5CinemaEventMinsk1 seller User 2",
                    Order = null
                }, //4
                new Ticket
                {
                    Event = eventsUse[0],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket6CinemaEventMinsk1 seller User 3",
                    Order = null
                }, //5

                new Ticket
                {
                    Event = eventsUse[1],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket1CinemaEventMinsk2 seller User 1"
                }, //6
                new Ticket
                {
                    Event = eventsUse[1],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket2CinemaEventMinsk2 seller User 2"
                }, //7
                new Ticket
                {
                    Event = eventsUse[1],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket3CinemaEventMinsk2 seller User 3"
                }, //8
                new Ticket
                {
                    Event = eventsUse[1],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket4CinemaEventMinsk2 seller User 1"
                }, //9
                new Ticket
                {
                    Event = eventsUse[1],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket5CinemaEventMinsk2 seller User 2"
                }, //10
                new Ticket
                {
                    Event = eventsUse[1],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket6CinemaEventMinsk2 seller User 3"
                } //11

                ,
                new Ticket
                {
                    Event = eventsUse[2],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket1cinemaEventGomel1 seller User 1"
                }, //12
                new Ticket
                {
                    Event = eventsUse[2],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket2cinemaEventGomel1 seller User 2"
                }, //13
                new Ticket
                {

                    Event = eventsUse[2],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket3cinemaEventGomel1 seller User 3"
                }, //14
                new Ticket
                {
                    Event = eventsUse[2],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket4cinemaEventGomel1 seller User 1"
                }, //15
                new Ticket
                {
                    Event = eventsUse[2],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket5CinemaEventGomel1 seller User 2"
                }, //16
                new Ticket
                {
                    Event = eventsUse[2],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket6CinemaEventGomel1 seller User 3"
                }, //17

                new Ticket
                {
                    Event = eventsUse[3],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket1CinemaEventGomel2 seller User 1"
                }, //18
                new Ticket
                {
                    Event = eventsUse[3],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket2CinemaEventGomel2 seller User 2"
                }, //19
                new Ticket
                {
                    Event = eventsUse[3],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket3CinemaEventGomel2 seller User 3"
                }, //20
                new Ticket
                {
                    Event = eventsUse[3],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket4CinemaEventGomel2 seller User 1"
                }, //21
                new Ticket
                {
                    Event = eventsUse[3],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket5CinemaEventGomel2 seller User 2"
                }, //22
                new Ticket
                {
                    Event = eventsUse[3],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket6CinemaEventGomel2 seller User 3"
                }, //23

                new Ticket
                {
                    Event = eventsUse[4],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket1CinemaEventGomel3 seller User 1"
                }, //24
                new Ticket
                {
                    Event = eventsUse[4],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket2CinemaEventGomel3 seller User 2"
                }, //25
                new Ticket
                {
                    Event = eventsUse[4],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket3CinemaEventGomel3 seller User 3"
                }, //26
                new Ticket
                {
                    Event = eventsUse[4],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket4CinemaEventGomel3 seller User 1"
                }, //27
                new Ticket
                {
                    Event = eventsUse[4],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket5CinemaEventGomel3 seller User 2"
                }, //28
                new Ticket
                {
                    Event = eventsUse[4],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket6CinemaEventGomel3 seller User 3"
                }, //29

                new Ticket
                {
                    Event = eventsUse[5],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket1CinemaEventGrodno1 seller User 1"
                }, //30
                new Ticket
                {
                    Event = eventsUse[5],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket2CinemaEventGrodno1 seller User 2"
                }, //31
                new Ticket
                {
                    Event = eventsUse[5],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket3CinemaEventGrodno1 seller User 3"
                }, //32
                new Ticket
                {
                    Event = eventsUse[5],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket4CinemaEventGrodno1 seller User 1"
                }, //33
                new Ticket
                {
                    Event = eventsUse[5],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket5CinemaEventGrodno1 seller User 2"
                }, //34
                new Ticket
                {
                    Event = eventsUse[5],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket6CinemaEventGrodno1 seller User 3"
                }, //35

                new Ticket
                {
                    Event = eventsUse[6],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket1CinemaEventGrodno2 seller User 1"
                }, //36
                new Ticket
                {
                    Event = eventsUse[6],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket2CinemaEventGrodno2 seller User 2"
                }, //37
                new Ticket
                {
                    Event = eventsUse[6],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket3CinemaEventGrodno2 seller User 3"
                }, //38
                new Ticket
                {
                    Event = eventsUse[6],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket4CinemaEventGrodno2 seller User 1"
                }, //39
                new Ticket
                {
                    Event = eventsUse[6],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket5CinemaEventGrodno2 seller User 2"
                }, //40
                new Ticket
                {
                    Event = eventsUse[6],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket6CinemaEventGrodno2 seller User 3"
                } //41

                ,
                new Ticket
                {
                    Event = eventsUse[7],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket1CinemaEventVitebsk1 seller User 1"
                }, //42
                new Ticket
                {
                    Event = eventsUse[7],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket2CinemaEventVitebsk1 seller User 2"
                }, //43
                new Ticket
                {
                    Event = eventsUse[7],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket3CinemaEventVitebsk1 seller User 3"
                }, //44
                new Ticket
                {
                    Event = eventsUse[7],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket4CinemaEventVitebsk1 seller User 1"
                }, //45
                new Ticket
                {
                    Event = eventsUse[7],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket5CinemaEventVitebsk1 seller User 2"
                }, //46
                new Ticket
                {
                    Event = eventsUse[7],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket6CinemaEventVitebsk1 seller User 3"
                }, //47
                new Ticket
                {
                    Event = eventsUse[8],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket1CinemaEventBrest1 seller User 1"
                }, //48
                new Ticket
                {
                    Event = eventsUse[8],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket2CinemaEventBrest1 seller User 2"
                }, //49
                new Ticket
                {
                    Event = eventsUse[8],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket3CinemaEventBrest1 seller User 3"
                }, //50
                new Ticket
                {
                    Event = eventsUse[8],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket4CinemaEventBrest1 seller User 1"
                }, //51
                new Ticket
                {
                    Event = eventsUse[8],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket5CinemaEventBrest1 seller User 2"
                }, //52
                new Ticket
                {
                    Event = eventsUse[8],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket6CinemaEventBrest1 seller User 3"
                }, //53
                new Ticket
                {
                    Event = eventsUse[9],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket1CinemaEventMogilev1 seller User 1"
                }, //54
                new Ticket
                {
                    Event = eventsUse[9],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket2CinemaEventMogilev1 seller User 2"
                }, //55
                new Ticket
                {
                    Event = eventsUse[9],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket3CinemaEventMogilev1 seller User 3"
                }, //56
                new Ticket
                {
                    Event = eventsUse[9],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket4CinemaEventMogilev1 seller User 1"
                }, //57
                new Ticket
                {
                    Event = eventsUse[9],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket5CinemaEventMogilev1 seller User 2"
                }, //58
                new Ticket
                {
                    Event = eventsUse[9],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket6CinemaEventMogilev1 seller User 3"
                }, //59
                new Ticket
                {
                    Event = eventsUse[10],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket1TheaterEventMinsk1 seller User 1"
                }, //60
                new Ticket
                {
                    Event = eventsUse[10],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket2TheaterEventMinsk1 seller User 2"
                }, //61
                new Ticket
                {
                    Event = eventsUse[10],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket3TheaterEventMinsk1 seller User 3"
                }, //62
                new Ticket
                {
                    Event = eventsUse[10],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket4TheaterEventMinsk1 seller User 1"
                }, //63
                new Ticket
                {
                    Event = eventsUse[10],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket5TheaterEventMinsk1 seller User 2"
                }, //64
                new Ticket
                {
                    Event = eventsUse[10],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket6TheaterEventMinsk1 seller User 3"
                }, //65
                new Ticket
                {
                    Event = eventsUse[11],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket1TheaterEventMinsk2 seller User 1"
                }, //66
                new Ticket
                {
                    Event = eventsUse[11],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket2TheaterEventMinsk2 seller User 2"
                }, //67
                new Ticket
                {
                    Event = eventsUse[11],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket3TheaterEventMinsk2 seller User 3"
                }, //68
                new Ticket
                {
                    Event = eventsUse[11],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket4TheaterEventMinsk2 seller User 1"
                }, //69
                new Ticket
                {
                    Event = eventsUse[11],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket5TheaterEventMinsk2 seller User 2"
                }, //70
                new Ticket
                {
                    Event = eventsUse[11],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket6TheaterEventMinsk2 seller User 3"
                }, //71
                new Ticket
                {
                    Event = eventsUse[12],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket1TheaterEventGomel1 seller User 1"
                }, //72
                new Ticket
                {
                    Event = eventsUse[12],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket2TheaterEventGomel1 seller User 2"
                }, //73
                new Ticket
                {
                    Event = eventsUse[12],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket3TheaterEventGomel1 seller User 3"
                }, //74
                new Ticket
                {
                    Event = eventsUse[12],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket4TheaterEventGomel1 seller User 1"
                }, //75
                new Ticket
                {
                    Event = eventsUse[12],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket5TheaterEventGomel1 seller User 2"
                }, //76
                new Ticket
                {
                    Event = eventsUse[12],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket6TheaterEventGomel1 seller User 3"
                }, //77
                new Ticket
                {
                    Event = eventsUse[13],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket1TheaterEventGomel2 seller User 1"
                }, //78
                new Ticket
                {
                    Event = eventsUse[13],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket2TheaterEventGomel2 seller User 2"
                }, //79
                new Ticket
                {
                    Event = eventsUse[13],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket3TheaterEventGomel2 seller User 3"
                }, //80
                new Ticket
                {
                    Event = eventsUse[13],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket4TheaterEventGomel2 seller User 1"
                }, //81
                new Ticket
                {
                    Event = eventsUse[13],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket5TheaterEventGomel2 seller User 2"
                }, //82
                new Ticket
                {
                    Event = eventsUse[13],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket6TheaterEventGomel2 seller User 3"
                }, //53
                new Ticket
                {
                    Event = eventsUse[14],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket1TheaterEventGrodno1 seller User 1"
                }, //84
                new Ticket
                {
                    Event = eventsUse[14],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket2TheaterEventGrodno1 seller User 2"
                }, //85
                new Ticket
                {
                    Event = eventsUse[14],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket3TheaterEventGrodno1 seller User 3"
                }, //86
                new Ticket
                {
                    Event = eventsUse[14],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket4TheaterEventGrodno1 seller User 1"
                }, //87
                new Ticket
                {
                    Event = eventsUse[14],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket5TheaterEventGrodno1 seller User 2"
                }, //88
                new Ticket
                {
                    Event = eventsUse[14],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket6TheaterEventGrodno1 seller User 3"
                } //89

                ,
                new Ticket
                {
                    Event = eventsUse[15],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket1TheaterEventVitebsk1 seller User 1"
                },//90
                new Ticket
                {
                    Event = eventsUse[15],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket2TheaterEventVitebsk1 seller User 2"
                },//91
                new Ticket
                {
                    Event = eventsUse[15],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket3TheaterEventVitebsk1 seller User 3"
                },//92
                new Ticket
                {
                    Event = eventsUse[15],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user1,
                    SellerNotes = "ticket4TheaterEventVitebsk1 seller User 1"
                },//93
                new Ticket
                {
                    Event = eventsUse[15],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user2,
                    SellerNotes = "ticket5TheaterEventVitebsk1 seller User 2"
                },//94
                new Ticket
                {
                    Event = eventsUse[15],
                    Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                    Seller = user3,
                    SellerNotes = "ticket6TheaterEventVitebsk1 seller User 3"
                }//95
            };


            #region tickets

            var ticketsUse = new List<Ticket>();
            foreach (var item in tickets)
            {
                if (ticketsService.IsExists(item.SellerNotes))
                {
                    ticketsUse.Add(ticketsService.Get(item.SellerNotes));
                }
                else
                {
                    try
                    {
                        ticketsService.Add(item);
                        ticketsUse.Add(ticketsService.Get(item.SellerNotes));
                    }
                    catch (BllValidationException) { }
                }
            }



            var ticket1CinemaEventMinsk1 = ticketsUse[0];
            var ticket2CinemaEventMinsk1 = ticketsUse[1];
            var ticket5CinemaEventMinsk1 = ticketsUse[4];
            var ticket6CinemaEventMinsk1 = ticketsUse[5];
            var ticket1CinemaEventMinsk2 = ticketsUse[6];
            var ticket2CinemaEventMinsk2 = ticketsUse[7];
            var ticket3CinemaEventMinsk2 = ticketsUse[8];
            var ticket4CinemaEventMinsk2 = ticketsUse[9];
            var ticket5CinemaEventMinsk2 = ticketsUse[10];
            var ticket6CinemaEventMinsk2 = ticketsUse[11];
            var ticket1CinemaEventGomel1 = ticketsUse[12];
            var ticket2CinemaEventGomel1 = ticketsUse[13];
            var ticket4CinemaEventGomel1 = ticketsUse[15];
            var ticket6CinemaEventGomel1 = ticketsUse[17];
            var ticket2CinemaEventGomel2 = ticketsUse[19];
            var ticket3CinemaEventGomel2 = ticketsUse[20];
            var ticket1CinemaEventGomel3 = ticketsUse[24];
            var ticket3CinemaEventGomel3 = ticketsUse[26];
            var ticket5CinemaEventGomel3 = ticketsUse[28];
            var ticket6CinemaEventGomel3 = ticketsUse[29];
            var ticket1CinemaEventGrodno1 = ticketsUse[30];
            var ticket2CinemaEventGrodno1 = ticketsUse[31];
            var ticket4CinemaEventGrodno1 = ticketsUse[33];
            var ticket5CinemaEventGrodno1 = ticketsUse[34];
            var ticket6CinemaEventGrodno1 = ticketsUse[35];
            var ticket6CinemaEventGrodno2 = ticketsUse[41];
            var ticket1CinemaEventVitebsk1 = ticketsUse[42];
            var ticket2CinemaEventVitebsk1 = ticketsUse[43];
            var ticket4CinemaEventVitebsk1 = ticketsUse[45];
            var ticket1CinemaEventBrest1 = ticketsUse[48];
            var ticket2CinemaEventBrest1 = ticketsUse[49];
            var ticket1TheaterEventMinsk1 = ticketsUse[60];
            var ticket2TheaterEventMinsk1 = ticketsUse[61];
            var ticket5TheaterEventMinsk1 = ticketsUse[64];
            var ticket6TheaterEventMinsk2 = ticketsUse[71];
            var ticket6TheaterEventGomel1 = ticketsUse[77];
            var ticket2TheaterEventGomel2 = ticketsUse[79];
            var ticket3TheaterEventGomel2 = ticketsUse[80];
            var ticket5TheaterEventGomel2 = ticketsUse[82];
            var ticket6TheaterEventGomel2 = ticketsUse[83];
            var ticket1TheaterEventGrodno1 = ticketsUse[84];
            var ticket2TheaterEventGrodno1 = ticketsUse[85];
            var ticket2TheaterEventVitebsk1 = ticketsUse[91];
            var ticket5TheaterEventVitebsk1 = ticketsUse[94];
            var ticket6TheaterEventVitebsk1 = ticketsUse[95];

            #endregion


            #endregion

            #region Order Table Init For User1

            var order1 = new Order
            {
                Buyer = user1,
                Status = statusWaiting,
                OrderTickets = new List<Ticket>
                {
                    ticket2CinemaEventMinsk1,
                    ticket5CinemaEventMinsk1,
                },
                TrackNo = "User1 Order #1"
            };
            var order2 = new Order
            {
                Buyer = user1,
                Status = statusConfirmed,
                OrderTickets = new List<Ticket>
                {
                    ticket2CinemaEventMinsk2,
                    ticket6CinemaEventMinsk2,
                    ticket5CinemaEventMinsk2
                },
                TrackNo = "User1 Order #2"
            };
            var order3 = new Order
            {
                Buyer = user1,
                Status = statusRejected,
                OrderTickets = new List<Ticket>
                {
                    ticket6CinemaEventGomel1,
                    ticket2CinemaEventGomel1,
                },
                TrackNo = "User1 Order #3"
            };
            var order4 = new Order
            {
                Buyer = user1,
                Status = statusConfirmed,
                OrderTickets = new List<Ticket>
                {
                    ticket2CinemaEventGomel2,
                },
                TrackNo = "User1 Order #4"
            };
            var order5 = new Order
            {
                Buyer = user1,
                Status = statusWaiting,
                OrderTickets = new List<Ticket>
                {
                    ticket5CinemaEventGomel3,
                    ticket6CinemaEventGomel3,
                },
                TrackNo = "User1 Order #5"
            };
            var order6 = new Order
            {
                Buyer = user1,
                Status = statusConfirmed,
                OrderTickets = new List<Ticket>
                {
                    ticket6CinemaEventGrodno1
                },
                TrackNo = "User1 Order #6"
            };
            var order7 = new Order
            {
                Buyer = user1,
                Status = statusRejected,
                OrderTickets = new List<Ticket>
                {
                    ticket6CinemaEventGrodno2,
                    ticket6TheaterEventMinsk2,
                    ticket6TheaterEventGomel1
                },
                TrackNo = "User1 Order #7"
            };
            var order8 = new Order
            {
                Buyer = user1,
                Status = statusConfirmed,
                OrderTickets = new List<Ticket>
                {
                    ticket2TheaterEventGomel2,//8 
                    ticket3TheaterEventGomel2,//8 
                    ticket5TheaterEventGomel2,//8 
                    ticket6TheaterEventGomel2 //8 
                },
                TrackNo = "User1 Order #8"
            };
            var order9 = new Order
            {
                Buyer = user1,
                Status = statusWaiting,
                OrderTickets = new List<Ticket>
                {
                    ticket6TheaterEventVitebsk1, //9
                    ticket5TheaterEventVitebsk1,
                    ticket2TheaterEventVitebsk1
                },
                TrackNo = "User1 Order #9"
            };

            context.Orders.Add(order1);
            context.Orders.Add(order2);
            context.Orders.Add(order3);
            context.Orders.Add(order4);
            context.Orders.Add(order5);
            context.Orders.Add(order6);
            context.Orders.Add(order7);
            context.Orders.Add(order8);
            context.Orders.Add(order9);
            #endregion

            #region Order Table Init For User2

            var order21 = new Order
            {
                Buyer = user2,
                Status = statusWaiting,
                OrderTickets = new List<Ticket>
                {
                    ticket1CinemaEventMinsk1,
                    ticket6CinemaEventMinsk1,
                },
                TrackNo = "User2 Order #1"
            };
            var order22 = new Order
            {
                Buyer = user2,
                Status = statusConfirmed,
                OrderTickets = new List<Ticket>
                {
                    ticket1CinemaEventMinsk2,
                    ticket4CinemaEventMinsk2,
                    ticket3CinemaEventMinsk2
                },
                TrackNo = "User2 Order #2"
            };
            var order23 = new Order
            {
                Buyer = user2,
                Status = statusRejected,
                OrderTickets = new List<Ticket>
                {
                    ticket1CinemaEventGomel1,
                    ticket4CinemaEventGomel1,
                },
                TrackNo = "User2 Order #3"
            };
            var order24 = new Order
            {
                Buyer = user2,
                Status = statusConfirmed,
                OrderTickets = new List<Ticket>
                {
                    ticket3CinemaEventGomel2,
                },
                TrackNo = "User2 Order #4"
            };
            var order25 = new Order
            {
                Buyer = user2,
                Status = statusWaiting,
                OrderTickets = new List<Ticket>
                {
                    ticket1CinemaEventGomel3,
                    ticket3CinemaEventGomel3,
                },
                TrackNo = "User2 Order #5"
            };

            context.Orders.Add(order21);
            context.Orders.Add(order22);
            context.Orders.Add(order23);
            context.Orders.Add(order24);
            context.Orders.Add(order25);
            #endregion

            #region Order Table Init For User2

            var order31 = new Order
            {
                Buyer = user3,
                Status = statusWaiting,
                OrderTickets = new List<Ticket>
                {
                    ticket1CinemaEventGrodno1,
                    ticket2CinemaEventGrodno1,
                    ticket4CinemaEventGrodno1,
                    ticket5CinemaEventGrodno1,
                },
                TrackNo = "User3 Order #1"
            };
            var order32 = new Order
            {
                Buyer = user3,
                Status = statusConfirmed,
                OrderTickets = new List<Ticket>
                {
                    ticket1CinemaEventVitebsk1,
                    ticket2CinemaEventVitebsk1,
                    ticket4CinemaEventVitebsk1
                },
                TrackNo = "User3 Order #2"
            };
            var order33 = new Order
            {
                Buyer = user3,
                Status = statusRejected,
                OrderTickets = new List<Ticket>
                {
                    ticket1CinemaEventBrest1,
                    ticket2CinemaEventBrest1,
                },
                TrackNo = "User3 Order #3"
            };
            var order34 = new Order
            {
                Buyer = user3,
                Status = statusConfirmed,
                OrderTickets = new List<Ticket>
                {
                    ticket1TheaterEventMinsk1,
                    ticket2TheaterEventMinsk1,
                    ticket5TheaterEventMinsk1,
                },
                TrackNo = "User3 Order #4"
            };
            var order35 = new Order
            {
                Buyer = user3,
                Status = statusWaiting,
                OrderTickets = new List<Ticket>
                {
                    ticket1TheaterEventGrodno1,
                    ticket2TheaterEventGrodno1,
                },
                TrackNo = "User3 Order #5"
            };

            context.Orders.Add(order31);
            context.Orders.Add(order32);
            context.Orders.Add(order33);
            context.Orders.Add(order34);
            context.Orders.Add(order35);
            #endregion

            context.SaveChanges();
        }
        /// <summary>
        /// Init User and Roles use User&Role manager
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task UserInit(IServiceProvider serviceProvider)
        {


            UserManager<AppUser> userManager =
                serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();


            string adminEmail = "Admin";
            string password = "Admin";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }



            var create = new Claim(Operations.ClaimTypeForDbWork, Operations.Create.Name);
            var read = new Claim(Operations.ClaimTypeForDbWork, Operations.Read.Name);
            var update = new Claim(Operations.ClaimTypeForDbWork, Operations.Update.Name);
            var delete = new Claim(Operations.ClaimTypeForDbWork, Operations.Delete.Name);



            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                AppUser admin = new AppUser { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");

                    await userManager.AddClaimAsync(admin, create);
                    await userManager.AddClaimAsync(admin, read);
                    await userManager.AddClaimAsync(admin, update);
                    await userManager.AddClaimAsync(admin, delete);
                }
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        Email = "User1",
                        UserName = "User1",
                        EmailConfirmed = true,
                        FirstName = "Firstname1",
                        LastName = "LastName1",
                        Localization = "ru-RU",
                        Address = "adress1",
                        PhoneNumber = "5-53-53-56"
                    },
                    new AppUser
                    {
                        Email = "User2",
                        UserName = "User2",
                        EmailConfirmed = true,
                        FirstName = "Firstname2",
                        LastName = "LastName2",
                        Localization = "ru-RU",
                        Address = "adress2",
                        PhoneNumber = "5-53-53-56"
                    },
                    new AppUser
                    {
                        Email = "User3",
                        UserName = "User3",
                        EmailConfirmed = true,
                        FirstName = "Firstname3",
                        LastName = "LastName3",
                        Localization = "ru-RU",
                        Address = "adress3",
                        PhoneNumber = "5-53-53-56"
                    }
                };

                var userManagerAcces = new Claim("UserManagerAcces", Operations.Read.Name);

                foreach (var item in users)
                {
                    IdentityResult result1 = await userManager.CreateAsync(item, item.Email);
                    if (result1.Succeeded)
                    {
                        await userManager.AddClaimAsync(item, create);
                        await userManager.AddClaimAsync(item, read);
                        await userManager.AddClaimAsync(item, update);
                        if (!item.UserName.Contains("3"))
                        {
                            await userManager.AddClaimAsync(item, userManagerAcces);
                        }

                        await userManager.AddToRoleAsync(item, "user");
                    }
                }
            }
        }
    }
}
