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

namespace TicketSaleCore.Models.DAL
{
    
    public static class DbInit
    {
        /// <summary>
        /// Database init
        /// </summary>
        /// <param name="context">set you IUnitOfWork context</param>
        /// <returns></returns>
        public static async Task AddTestData(IUnitOfWork context)
        {

            #region EventsType

            var eventCinema = new EventsType
            {
                NameEventsType = "Cinema"
            };
            var eventTheater = new EventsType
            {
                NameEventsType = "Theater"
            };
            var eventSport = new EventsType
            {
                NameEventsType = "Sport"
            };

            context.EventsTypes.Add(eventCinema);
            context.EventsTypes.Add(eventTheater);
            context.EventsTypes.Add(eventSport);

            #endregion

            #region City Table Init

            var cityMinsk = new City { Name = "Minsk" };
            var cityGomel = new City { Name = "Gomel" };
            var cityGrodno = new City { Name = "Grodno" };
            var cityVitebsk = new City { Name = "Vitebsk" };
            var cityBrest = new City { Name = "Brest" };
            var cityMogilev = new City { Name = "Mogilev" };

            context.Citys.Add(cityMinsk);
            context.Citys.Add(cityGomel);
            context.Citys.Add(cityGrodno);
            context.Citys.Add(cityVitebsk);
            context.Citys.Add(cityBrest);
            context.Citys.Add(cityMogilev);

            #endregion

            #region Order Status Table Init

            var statusWaiting = new OrderStatus { StatusName = "Waiting for conformation" };
            var statusConfirmed = new OrderStatus { StatusName = "Confirmed" };
            var statusRejected = new OrderStatus { StatusName = "Rejected" };

            context.OrderStatuses.Add(statusWaiting);
            context.OrderStatuses.Add(statusConfirmed);
            context.OrderStatuses.Add(statusRejected);

            #endregion

            var eventRandomDays = new Random(DateTime.Now.Millisecond);

            #region Event Table Init

            #region CinemaEvent

            //minsk

            var cinemaEventMinsk1 = new Event
            {
                Name = "cinemaEventMinskMoscow",
                EventsType = eventCinema,
                Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                Description = "this is test description for first cinema event in minsk moscow cinema",
                Venue = new Venue
                {
                    Name = "Moscow Cinema",
                    Address = "Moscow Cinema address",
                    City = cityMinsk
                }
            };
            var cinemaEventMinsk2 = new Event
            {
                Name = "cinemaEventMinskAurora",
                EventsType = eventCinema,
                Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                Description = "this is test description for second cinema event in minsk Aurora cinema",
                Venue = new Venue
                {
                    Name = "Aurora Cinema",
                    Address = "Aurora Cinema address",
                    City = cityMinsk
                }
            };
            //Gomel
            var cinemaEventGomel1 = new Event
            {
                Name = "cinemaEventGomel1",
                EventsType = eventCinema,
                Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                Description = "this is test description for cinema event in Gomel #1",
                Venue = new Venue
                {
                    Name = "1 random Cinema in gomel",
                    Address = "1 random Cinema in gomel address",
                    City = cityGomel
                }
            };
            var cinemaEventGomel2 = new Event
            {
                Name = "cinemaEventGomel2",
                EventsType = eventCinema,
                Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                Description = "this is test description for second cinema event in Gomel#2",
                Venue = new Venue
                {
                    Name = "2 random Cinema in gomel",
                    Address = "2 random Cinema in gomel address",
                    City = cityGomel
                }
            };
            var cinemaEventGomel3 = new Event
            {
                Name = "cinemaEventGomel3",
                EventsType = eventCinema,
                Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                Description = "this is test description for second cinema event in Gomel#3",
                Venue = new Venue
                {
                    Name = "3 random Cinema in gomel",
                    Address = "3 random Cinema in gomel address",
                    City = cityGomel
                }
            };
            //grodno
            var cinemaEventGrodno1 = new Event
            {
                Name = "cinemaEventGrodno1",
                EventsType = eventCinema,
                Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                Description = "this is test description for first cinema event in Grodno #1",
                Venue = new Venue
                {
                    Name = "Random Cinema in grodno #1",
                    Address = "Random Cinema in Grodno address #1",
                    City = cityGrodno
                }
            };
            var cinemaEventGrodno2 = new Event
            {
                Name = "cinemaEventGrodno2",
                EventsType = eventCinema,
                Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                Description = "this is test description for first cinema event in Grodno #2",
                Venue = new Venue
                {
                    Name = "Random Cinema in grodno #2",
                    Address = "Random Cinema in Grodno address #2",
                    City = cityGrodno
                }
            };
            //vitebsk
            var cinemaEventVitebsk1 = new Event
            {
                Name = "cinemaEventVitebsk1",
                EventsType = eventCinema,
                Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                Description = "this is test description for first cinema event in Vitebsk (single)",
                Venue = new Venue
                {
                    Name = "Random Cinema in Vitebsk #1",
                    Address = "Random Cinema in Vitebsk address #1",
                    City = cityVitebsk
                }
            };
            //brest
            var cinemaEventBrest1 = new Event
            {
                Name = "cinemaEventBrest1",
                EventsType = eventCinema,
                Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                Description = "this is test description for first cinema event in Brest cinema",
                Venue = new Venue
                {
                    Name = "Random Cinema in Brest #1",
                    Address = "Random Cinema in Brest address #1",
                    City = cityBrest
                }
            };
            //mogilev
            var cinemaEventMogilev1 = new Event
            {
                Name = "cinemaEventMogilev1",
                EventsType = eventCinema,
                Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                Description = "this is test description for first cinema event in Mogilev cinema",
                Venue = new Venue
                {
                    Name = "Random Cinema in Mogilev #1",
                    Address = "Random Cinema in Mogilev address #1",
                    City = cityMogilev
                }
            };
            context.Events.Add(cinemaEventMinsk1);
            context.Events.Add(cinemaEventMinsk2);
            context.Events.Add(cinemaEventGomel1);
            context.Events.Add(cinemaEventGrodno1);
            context.Events.Add(cinemaEventVitebsk1);
            context.Events.Add(cinemaEventBrest1);
            context.Events.Add(cinemaEventMogilev1);
            context.Events.Add(cinemaEventGomel2);
            context.Events.Add(cinemaEventGomel3);
            context.Events.Add(cinemaEventGrodno2);

            #endregion

            #region TheaterEvent

            //minsk
            var theaterEventMinsk1 = new Event
            {
                Name = "TheaterEventMinsk1",
                EventsType = eventTheater,
                Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                Description = "this is test description for first eventTheater in minsk",
                Venue = new Venue
                {
                    Name = "Random Minsk Theater #1",
                    Address = "random Theater in Minsk address #1",
                    City = cityMinsk
                }
            };
            var theaterEventMinsk2 = new Event
            {
                Name = "TheaterEventMinsk2",
                EventsType = eventTheater,
                Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                Description = "this is test description for second eventTheater in minsk",
                Venue = new Venue
                {
                    Name = "Random Minsk Theater #2",
                    Address = "random Theater in Minsk address #2",
                    City = cityMinsk
                }
            };
            //Gomel
            var theaterEventGomel1 = new Event
            {
                Name = "TheaterEventGomel1",
                EventsType = eventTheater,
                Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                Description = "this is test description for TheaterEventGomel1 in Gomel #1",
                Venue = new Venue
                {
                    Name = "1 random Theater in gomel",
                    Address = "1 random Theater in gomel address",
                    City = cityGomel
                }
            };
            var theaterEventGomel2 = new Event
            {
                Name = "TheaterEventGomel2",
                EventsType = eventTheater,
                Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                Description = "this is test description for second TheaterEventGomel2 in Gomel#2",
                Venue = new Venue
                {
                    Name = "2 random Theater in gomel",
                    Address = "2 random Theater in gomel address",
                    City = cityGomel
                }
            };
            //grodno
            var theaterEventGrodno1 = new Event
            {
                Name = "TheaterEventGrodno1",
                EventsType = eventTheater,
                Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                Description = "this is test description for first TheaterEventGrodno1 in Grodno #1",
                Venue = new Venue
                {
                    Name = "Random TheaterEventGrodno1 in grodno #1",
                    Address = "Random TheaterEventGrodno1 in Grodno address #1",
                    City = cityGrodno
                }
            };
            //vitebsk
            var theaterEventVitebsk1 = new Event
            {
                Name = "TheaterEventVitebsk1",
                EventsType = eventTheater,
                Date = DateTime.Now.AddDays(eventRandomDays.NextDouble() + eventRandomDays.Next(15)),
                Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                Description = "this is test description for first TheaterEventVitebsk1 in Vitebsk (single)",
                Venue = new Venue
                {
                    Name = "Random TheaterEventVitebsk1 in Vitebsk #1",
                    Address = "Random TheaterEventVitebsk1 in Vitebsk address #1",
                    City = cityVitebsk
                }
            };


            context.Events.Add(theaterEventMinsk1);
            context.Events.Add(theaterEventMinsk2);
            context.Events.Add(theaterEventGomel1);
            context.Events.Add(theaterEventGomel2);
            context.Events.Add(theaterEventGrodno1);
            context.Events.Add(theaterEventVitebsk1);

            #endregion

            #endregion

            //AppUser user1 = new AppUser
            //{
            //    Email = "User1",
            //    UserName = "User1",
            //    EmailConfirmed = true,
            //    FirstName = "Firstname1",
            //    LastName = "LastName1",
            //    Localization = "ru-RU",
            //    Address = "adress1",
            //    PhoneNumber = "5-53-53-56"
            //};
            //AppUser user2 = new AppUser
            //{
            //    Email = "User2",
            //    UserName = "User2",
            //    EmailConfirmed = true,
            //    FirstName = "Firstname2",
            //    LastName = "LastName2",
            //    Localization = "ru-RU",
            //    Address = "adress2",
            //    PhoneNumber = "5-53-53-56"
            //};
            //AppUser user3 = new AppUser
            //{
            //    Email = "User3",
            //    UserName = "User3",
            //    EmailConfirmed = true,
            //    FirstName = "Firstname3",
            //    LastName = "LastName3",
            //    Localization = "ru-RU",
            //    Address = "adress3",
            //    PhoneNumber = "5-53-53-56"
            //};

            //context.AppUsers.Add(user1);
            //context.AppUsers.Add(user2);
            //context.AppUsers.Add(user3);

            AppUser user1 = context.AppUsers.ToList().First(p => p.Email.Equals("User1"));
            AppUser user2 = context.AppUsers.ToList().First(p => p.Email.Equals("User2"));
            AppUser user3 = context.AppUsers.ToList().First(p => p.Email.Equals("User3"));

            #region Tikets Table Init

            var ticketPriceRandom = eventRandomDays;

            #region ticket for cinemaEventMinsk1

            var ticket1CinemaEventMinsk1 = new Ticket
            {
                Event = cinemaEventMinsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket1CinemaEventMinsk1 seller User 1",
                Order = null

            };
            var ticket2CinemaEventMinsk1 = new Ticket
            {
                Event = cinemaEventMinsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket2CinemaEventMinsk1 seller User 2",
                Order = null
            };
            var ticket3CinemaEventMinsk1 = new Ticket
            {
                Event = cinemaEventMinsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket3CinemaEventMinsk1 seller User 3",
                Order = null
            };
            var ticket4CinemaEventMinsk1 = new Ticket
            {
                Event = cinemaEventMinsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket4CinemaEventMinsk1 seller User 1",
                Order = null
            };
            var ticket5CinemaEventMinsk1 = new Ticket
            {
                Event = cinemaEventMinsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket5CinemaEventMinsk1 seller User 2",
                Order = null
            };
            var ticket6CinemaEventMinsk1 = new Ticket
            {
                Event = cinemaEventMinsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket6CinemaEventMinsk1 seller User 3",
                Order = null
            };
            context.Tickets.Add(ticket1CinemaEventMinsk1);
            context.Tickets.Add(ticket2CinemaEventMinsk1);
            context.Tickets.Add(ticket3CinemaEventMinsk1);
            context.Tickets.Add(ticket4CinemaEventMinsk1);
            context.Tickets.Add(ticket5CinemaEventMinsk1);
            context.Tickets.Add(ticket6CinemaEventMinsk1);

            #endregion

            #region ticket for cinemaEventMinsk2

            var ticket1CinemaEventMinsk2 = new Ticket
            {
                Event = cinemaEventMinsk2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket1CinemaEventMinsk2 seller User 1"
            };
            var ticket2CinemaEventMinsk2 = new Ticket
            {
                Event = cinemaEventMinsk2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket2CinemaEventMinsk2 seller User 2"
            };
            var ticket3CinemaEventMinsk2 = new Ticket
            {
                Event = cinemaEventMinsk2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket3CinemaEventMinsk2 seller User 3"
            };
            var ticket4CinemaEventMinsk2 = new Ticket
            {
                Event = cinemaEventMinsk2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket4CinemaEventMinsk2 seller User 1"
            };
            var ticket5CinemaEventMinsk2 = new Ticket
            {
                Event = cinemaEventMinsk2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket5CinemaEventMinsk2 seller User 2"
            };
            var ticket6CinemaEventMinsk2 = new Ticket
            {
                Event = cinemaEventMinsk2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket6CinemaEventMinsk2 seller User 3"
            };

            context.Tickets.Add(ticket1CinemaEventMinsk2);
            context.Tickets.Add(ticket2CinemaEventMinsk2);
            context.Tickets.Add(ticket3CinemaEventMinsk2);
            context.Tickets.Add(ticket4CinemaEventMinsk2);
            context.Tickets.Add(ticket5CinemaEventMinsk2);
            context.Tickets.Add(ticket6CinemaEventMinsk2);

            #endregion

            #region ticket for cinemaEventGomel1

            var ticket1CinemaEventGomel1 = new Ticket
            {
                Event = cinemaEventGomel1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket1cinemaEventGomel1 seller User 1"
            };
            var ticket2CinemaEventGomel1 = new Ticket
            {
                Event = cinemaEventGomel1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket2cinemaEventGomel1 seller User 2"
            };
            var ticket3CinemaEventGomel1 = new Ticket
            {
                Event = cinemaEventGomel1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket3cinemaEventGomel1 seller User 3"
            };
            var ticket4CinemaEventGomel1 = new Ticket
            {
                Event = cinemaEventGomel1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket4cinemaEventGomel1 seller User 1"
            };
            var ticket5CinemaEventGomel1 = new Ticket
            {
                Event = cinemaEventGomel1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket5CinemaEventGomel1 seller User 2"
            };
            var ticket6CinemaEventGomel1 = new Ticket
            {
                Event = cinemaEventGomel1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket6CinemaEventGomel1 seller User 3"
            };

            context.Tickets.Add(ticket1CinemaEventGomel1);
            context.Tickets.Add(ticket2CinemaEventGomel1);
            context.Tickets.Add(ticket3CinemaEventGomel1);
            context.Tickets.Add(ticket4CinemaEventGomel1);
            context.Tickets.Add(ticket5CinemaEventGomel1);
            context.Tickets.Add(ticket6CinemaEventGomel1);

            #endregion

            #region ticket for cinemaEventGomel2

            var ticket1CinemaEventGomel2 = new Ticket
            {
                Event = cinemaEventGomel2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket1CinemaEventGomel2 seller User 1"
            };
            var ticket2CinemaEventGomel2 = new Ticket
            {
                Event = cinemaEventGomel2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket2CinemaEventGomel2 seller User 2"
            };
            var ticket3CinemaEventGomel2 = new Ticket
            {
                Event = cinemaEventGomel2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket3CinemaEventGomel2 seller User 3"
            };
            var ticket4CinemaEventGomel2 = new Ticket
            {
                Event = cinemaEventGomel2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket4CinemaEventGomel2 seller User 1"
            };
            var ticket5CinemaEventGomel2 = new Ticket
            {
                Event = cinemaEventGomel2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket5CinemaEventGomel2 seller User 2"
            };
            var ticket6CinemaEventGomel2 = new Ticket
            {
                Event = cinemaEventGomel2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket6CinemaEventGomel2 seller User 3"
            };

            context.Tickets.Add(ticket1CinemaEventGomel2);
            context.Tickets.Add(ticket2CinemaEventGomel2);
            context.Tickets.Add(ticket3CinemaEventGomel2);
            context.Tickets.Add(ticket4CinemaEventGomel2);
            context.Tickets.Add(ticket5CinemaEventGomel2);
            context.Tickets.Add(ticket6CinemaEventGomel2);

            #endregion

            #region ticket for cinemaEventGomel3

            var ticket1CinemaEventGomel3 = new Ticket
            {
                Event = cinemaEventGomel3,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket1CinemaEventGomel3 seller User 1"
            };
            var ticket2CinemaEventGomel3 = new Ticket
            {
                Event = cinemaEventGomel3,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket2CinemaEventGomel3 seller User 2"
            };
            var ticket3CinemaEventGomel3 = new Ticket
            {
                Event = cinemaEventGomel3,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket3CinemaEventGomel3 seller User 3"
            };
            var ticket4CinemaEventGomel3 = new Ticket
            {
                Event = cinemaEventGomel3,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket4CinemaEventGomel3 seller User 1"
            };
            var ticket5CinemaEventGomel3 = new Ticket
            {
                Event = cinemaEventGomel3,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket5CinemaEventGomel3 seller User 2"
            };
            var ticket6CinemaEventGomel3 = new Ticket
            {
                Event = cinemaEventGomel3,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket6CinemaEventGomel3 seller User 3"
            };
            context.Tickets.Add(ticket1CinemaEventGomel3);
            context.Tickets.Add(ticket2CinemaEventGomel3);
            context.Tickets.Add(ticket3CinemaEventGomel3);
            context.Tickets.Add(ticket4CinemaEventGomel3);
            context.Tickets.Add(ticket5CinemaEventGomel3);
            context.Tickets.Add(ticket6CinemaEventGomel3);

            #endregion

            #region ticket for cinemaEventGrodno1

            var ticket1CinemaEventGrodno1 = new Ticket
            {
                Event = cinemaEventGrodno1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket1CinemaEventGrodno1 seller User 1"
            };
            var ticket2CinemaEventGrodno1 = new Ticket
            {
                Event = cinemaEventGrodno1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket2CinemaEventGrodno1 seller User 2"
            };
            var ticket3CinemaEventGrodno1 = new Ticket
            {
                Event = cinemaEventGrodno1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket3CinemaEventGrodno1 seller User 3"
            };
            var ticket4CinemaEventGrodno1 = new Ticket
            {
                Event = cinemaEventGrodno1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket4CinemaEventGrodno1 seller User 1"
            };
            var ticket5CinemaEventGrodno1 = new Ticket
            {
                Event = cinemaEventGrodno1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket5CinemaEventGrodno1 seller User 2"
            };
            var ticket6CinemaEventGrodno1 = new Ticket
            {
                Event = cinemaEventGrodno1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket6CinemaEventGrodno1 seller User 3"
            };

            context.Tickets.Add(ticket1CinemaEventGrodno1);
            context.Tickets.Add(ticket2CinemaEventGrodno1);
            context.Tickets.Add(ticket3CinemaEventGrodno1);
            context.Tickets.Add(ticket4CinemaEventGrodno1);
            context.Tickets.Add(ticket5CinemaEventGrodno1);
            context.Tickets.Add(ticket6CinemaEventGrodno1);

            #endregion

            #region ticket for cinemaEventGrodno2

            var ticket1CinemaEventGrodno2 = new Ticket
            {
                Event = cinemaEventGrodno2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket1CinemaEventGrodno2 seller User 1"
            };
            var ticket2CinemaEventGrodno2 = new Ticket
            {
                Event = cinemaEventGrodno2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket2CinemaEventGrodno2 seller User 2"
            };
            var ticket3CinemaEventGrodno2 = new Ticket
            {
                Event = cinemaEventGrodno2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket3CinemaEventGrodno2 seller User 3"
            };
            var ticket4CinemaEventGrodno2 = new Ticket
            {
                Event = cinemaEventGrodno2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket4CinemaEventGrodno2 seller User 1"
            };
            var ticket5CinemaEventGrodno2 = new Ticket
            {
                Event = cinemaEventGrodno2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket5CinemaEventGrodno2 seller User 2"
            };
            var ticket6CinemaEventGrodno2 = new Ticket
            {
                Event = cinemaEventGrodno2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket6CinemaEventGrodno2 seller User 3"
            };

            context.Tickets.Add(ticket1CinemaEventGrodno2);
            context.Tickets.Add(ticket2CinemaEventGrodno2);
            context.Tickets.Add(ticket3CinemaEventGrodno2);
            context.Tickets.Add(ticket4CinemaEventGrodno2);
            context.Tickets.Add(ticket5CinemaEventGrodno2);
            context.Tickets.Add(ticket6CinemaEventGrodno2);

            #endregion

            #region ticket for cinemaEventVitebsk1

            var ticket1CinemaEventVitebsk1 = new Ticket
            {
                Event = cinemaEventVitebsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket1CinemaEventVitebsk1 seller User 1"
            };
            var ticket2CinemaEventVitebsk1 = new Ticket
            {
                Event = cinemaEventVitebsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket2CinemaEventVitebsk1 seller User 2"
            };
            var ticket3CinemaEventVitebsk1 = new Ticket
            {
                Event = cinemaEventVitebsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket3CinemaEventVitebsk1 seller User 3"
            };
            var ticket4CinemaEventVitebsk1 = new Ticket
            {
                Event = cinemaEventVitebsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket4CinemaEventVitebsk1 seller User 1"
            };
            var ticket5CinemaEventVitebsk1 = new Ticket
            {
                Event = cinemaEventVitebsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket5CinemaEventVitebsk1 seller User 2"
            };
            var ticket6CinemaEventVitebsk1 = new Ticket
            {
                Event = cinemaEventVitebsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket6CinemaEventVitebsk1 seller User 3"
            };

            context.Tickets.Add(ticket1CinemaEventVitebsk1);
            context.Tickets.Add(ticket2CinemaEventVitebsk1);
            context.Tickets.Add(ticket3CinemaEventVitebsk1);
            context.Tickets.Add(ticket4CinemaEventVitebsk1);
            context.Tickets.Add(ticket5CinemaEventVitebsk1);
            context.Tickets.Add(ticket6CinemaEventVitebsk1);

            #endregion

            #region ticket for cinemaEventBrest1

            var ticket1CinemaEventBrest1 = new Ticket
            {
                Event = cinemaEventBrest1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket1CinemaEventBrest1 seller User 1"
            };
            var ticket2CinemaEventBrest1 = new Ticket
            {
                Event = cinemaEventBrest1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket2CinemaEventBrest1 seller User 2"
            };
            var ticket3CinemaEventBrest1 = new Ticket
            {
                Event = cinemaEventBrest1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket3CinemaEventBrest1 seller User 3"
            };
            var ticket4CinemaEventBrest1 = new Ticket
            {
                Event = cinemaEventBrest1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket4CinemaEventBrest1 seller User 1"
            };
            var ticket5CinemaEventBrest1 = new Ticket
            {
                Event = cinemaEventBrest1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket5CinemaEventBrest1 seller User 2"
            };
            var ticket6CinemaEventBrest1 = new Ticket
            {
                Event = cinemaEventBrest1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket6CinemaEventBrest1 seller User 3"
            };

            context.Tickets.Add(ticket1CinemaEventBrest1);
            context.Tickets.Add(ticket2CinemaEventBrest1);
            context.Tickets.Add(ticket3CinemaEventBrest1);
            context.Tickets.Add(ticket4CinemaEventBrest1);
            context.Tickets.Add(ticket5CinemaEventBrest1);
            context.Tickets.Add(ticket6CinemaEventBrest1);

            #endregion

            #region ticket for cinemaEventMogilev1

            var ticket1CinemaEventMogilev1 = new Ticket
            {
                Event = cinemaEventMogilev1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket1CinemaEventMogilev1 seller User 1"
            };
            var ticket2CinemaEventMogilev1 = new Ticket
            {
                Event = cinemaEventMogilev1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket2CinemaEventMogilev1 seller User 2"
            };
            var ticket3CinemaEventMogilev1 = new Ticket
            {
                Event = cinemaEventMogilev1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket3CinemaEventMogilev1 seller User 3"
            };
            var ticket4CinemaEventMogilev1 = new Ticket
            {
                Event = cinemaEventMogilev1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket4CinemaEventMogilev1 seller User 1"
            };
            var ticket5CinemaEventMogilev1 = new Ticket
            {
                Event = cinemaEventMogilev1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket5CinemaEventMogilev1 seller User 2"
            };
            var ticket6CinemaEventMogilev1 = new Ticket
            {
                Event = cinemaEventMogilev1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket6CinemaEventMogilev1 seller User 3"
            };

            context.Tickets.Add(ticket1CinemaEventMogilev1);
            context.Tickets.Add(ticket2CinemaEventMogilev1);
            context.Tickets.Add(ticket3CinemaEventMogilev1);
            context.Tickets.Add(ticket4CinemaEventMogilev1);
            context.Tickets.Add(ticket5CinemaEventMogilev1);
            context.Tickets.Add(ticket6CinemaEventMogilev1);

            #endregion

            #region ticket for theaterEventMinsk1

            var ticket1TheaterEventMinsk1 = new Ticket
            {
                Event = theaterEventMinsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket1TheaterEventMinsk1 seller User 1"
            };
            var ticket2TheaterEventMinsk1 = new Ticket
            {
                Event = theaterEventMinsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket2TheaterEventMinsk1 seller User 2"
            };
            var ticket3TheaterEventMinsk1 = new Ticket
            {
                Event = theaterEventMinsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket3TheaterEventMinsk1 seller User 3"
            };
            var ticket4TheaterEventMinsk1 = new Ticket
            {
                Event = theaterEventMinsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket4TheaterEventMinsk1 seller User 1"
            };
            var ticket5TheaterEventMinsk1 = new Ticket
            {
                Event = theaterEventMinsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket5TheaterEventMinsk1 seller User 2"
            };
            var ticket6TheaterEventMinsk1 = new Ticket
            {
                Event = theaterEventMinsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket6TheaterEventMinsk1 seller User 3"
            };

            context.Tickets.Add(ticket1TheaterEventMinsk1);
            context.Tickets.Add(ticket2TheaterEventMinsk1);
            context.Tickets.Add(ticket3TheaterEventMinsk1);
            context.Tickets.Add(ticket4TheaterEventMinsk1);
            context.Tickets.Add(ticket5TheaterEventMinsk1);
            context.Tickets.Add(ticket6TheaterEventMinsk1);

            #endregion

            #region ticket for theaterEventMinsk2

            var ticket1TheaterEventMinsk2 = new Ticket
            {
                Event = theaterEventMinsk2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket1TheaterEventMinsk2 seller User 1"
            };
            var ticket2TheaterEventMinsk2 = new Ticket
            {
                Event = theaterEventMinsk2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket2TheaterEventMinsk2 seller User 2"
            };
            var ticket3TheaterEventMinsk2 = new Ticket
            {
                Event = theaterEventMinsk2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket3TheaterEventMinsk2 seller User 3"
            };
            var ticket4TheaterEventMinsk2 = new Ticket
            {
                Event = theaterEventMinsk2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket4TheaterEventMinsk2 seller User 1"
            };
            var ticket5TheaterEventMinsk2 = new Ticket
            {
                Event = theaterEventMinsk2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket5TheaterEventMinsk2 seller User 2"
            };
            var ticket6TheaterEventMinsk2 = new Ticket
            {
                Event = theaterEventMinsk2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket6TheaterEventMinsk2 seller User 3"
            };

            context.Tickets.Add(ticket1TheaterEventMinsk2);
            context.Tickets.Add(ticket2TheaterEventMinsk2);
            context.Tickets.Add(ticket3TheaterEventMinsk2);
            context.Tickets.Add(ticket4TheaterEventMinsk2);
            context.Tickets.Add(ticket5TheaterEventMinsk2);
            context.Tickets.Add(ticket6TheaterEventMinsk2);

            #endregion

            #region ticket for theaterEventGomel1

            var ticket1TheaterEventGomel1 = new Ticket
            {
                Event = theaterEventGomel1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket1TheaterEventGomel1 seller User 1"
            };
            var ticket2TheaterEventGomel1 = new Ticket
            {
                Event = theaterEventGomel1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket2TheaterEventGomel1 seller User 2"
            };
            var ticket3TheaterEventGomel1 = new Ticket
            {
                Event = theaterEventGomel1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket3TheaterEventGomel1 seller User 3"
            };
            var ticket4TheaterEventGomel1 = new Ticket
            {
                Event = theaterEventGomel1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket4TheaterEventGomel1 seller User 1"
            };
            var ticket5TheaterEventGomel1 = new Ticket
            {
                Event = theaterEventGomel1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket5TheaterEventGomel1 seller User 2"
            };
            var ticket6TheaterEventGomel1 = new Ticket
            {
                Event = theaterEventGomel1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket6TheaterEventGomel1 seller User 3"
            };

            context.Tickets.Add(ticket1TheaterEventGomel1);
            context.Tickets.Add(ticket2TheaterEventGomel1);
            context.Tickets.Add(ticket3TheaterEventGomel1);
            context.Tickets.Add(ticket4TheaterEventGomel1);
            context.Tickets.Add(ticket5TheaterEventGomel1);
            context.Tickets.Add(ticket6TheaterEventGomel1);

            #endregion

            #region ticket for theaterEventGomel2

            var ticket1TheaterEventGomel2 = new Ticket
            {
                Event = theaterEventGomel2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket1TheaterEventGomel2 seller User 1"
            };
            var ticket2TheaterEventGomel2 = new Ticket
            {
                Event = theaterEventGomel2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket2TheaterEventGomel2 seller User 2"
            };
            var ticket3TheaterEventGomel2 = new Ticket
            {
                Event = theaterEventGomel2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket3TheaterEventGomel2 seller User 3"
            };
            var ticket4TheaterEventGomel2 = new Ticket
            {
                Event = theaterEventGomel2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket4TheaterEventGomel2 seller User 1"
            };
            var ticket5TheaterEventGomel2 = new Ticket
            {
                Event = theaterEventGomel2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket5TheaterEventGomel2 seller User 2"
            };
            var ticket6TheaterEventGomel2 = new Ticket
            {
                Event = theaterEventGomel2,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket6TheaterEventGomel2 seller User 3"
            };

            context.Tickets.Add(ticket1TheaterEventGomel2);
            context.Tickets.Add(ticket2TheaterEventGomel2);
            context.Tickets.Add(ticket3TheaterEventGomel2);
            context.Tickets.Add(ticket4TheaterEventGomel2);
            context.Tickets.Add(ticket5TheaterEventGomel2);
            context.Tickets.Add(ticket6TheaterEventGomel2);

            #endregion

            #region ticket for theaterEventGrodno1

            var ticket1TheaterEventGrodno1 = new Ticket
            {
                Event = theaterEventGrodno1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket1TheaterEventGrodno1 seller User 1"
            };
            var ticket2TheaterEventGrodno1 = new Ticket
            {
                Event = theaterEventGrodno1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket2TheaterEventGrodno1 seller User 2"
            };
            var ticket3TheaterEventGrodno1 = new Ticket
            {
                Event = theaterEventGrodno1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket3TheaterEventGrodno1 seller User 3"
            };
            var ticket4TheaterEventGrodno1 = new Ticket
            {
                Event = theaterEventGrodno1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket4TheaterEventGrodno1 seller User 1"
            };
            var ticket5TheaterEventGrodno1 = new Ticket
            {
                Event = theaterEventGrodno1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket5TheaterEventGrodno1 seller User 2"
            };
            var ticket6TheaterEventGrodno1 = new Ticket
            {
                Event = theaterEventGrodno1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket6TheaterEventGrodno1 seller User 3"
            };

            context.Tickets.Add(ticket1TheaterEventGrodno1);
            context.Tickets.Add(ticket2TheaterEventGrodno1);
            context.Tickets.Add(ticket3TheaterEventGrodno1);
            context.Tickets.Add(ticket4TheaterEventGrodno1);
            context.Tickets.Add(ticket5TheaterEventGrodno1);
            context.Tickets.Add(ticket6TheaterEventGrodno1);

            #endregion

            #region ticket for theaterEventVitebsk1

            var ticket1TheaterEventVitebsk1 = new Ticket
            {
                Event = theaterEventVitebsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket1TheaterEventVitebsk1 seller User 1"
            };
            var ticket2TheaterEventVitebsk1 = new Ticket
            {
                Event = theaterEventVitebsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket2TheaterEventVitebsk1 seller User 2"
            };
            var ticket3TheaterEventVitebsk1 = new Ticket
            {
                Event = theaterEventVitebsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket3TheaterEventVitebsk1 seller User 3"
            };
            var ticket4TheaterEventVitebsk1 = new Ticket
            {
                Event = theaterEventVitebsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user1,
                SellerNotes = "ticket4TheaterEventVitebsk1 seller User 1"
            };
            var ticket5TheaterEventVitebsk1 = new Ticket
            {
                Event = theaterEventVitebsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user2,
                SellerNotes = "ticket5TheaterEventVitebsk1 seller User 2"
            };
            var ticket6TheaterEventVitebsk1 = new Ticket
            {
                Event = theaterEventVitebsk1,
                Price = Convert.ToDecimal(ticketPriceRandom.Next(13) + ticketPriceRandom.NextDouble()),
                Seller = user3,
                SellerNotes = "ticket6TheaterEventVitebsk1 seller User 3"
            };

            context.Tickets.Add(ticket1TheaterEventVitebsk1);
            context.Tickets.Add(ticket2TheaterEventVitebsk1);
            context.Tickets.Add(ticket3TheaterEventVitebsk1);
            context.Tickets.Add(ticket4TheaterEventVitebsk1);
            context.Tickets.Add(ticket5TheaterEventVitebsk1);
            context.Tickets.Add(ticket6TheaterEventVitebsk1);

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
            if(await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if(await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }

        

            var create = new Claim(Operations.ClaimTypeForDbWork, Operations.Create.Name);
            var read = new Claim(Operations.ClaimTypeForDbWork, Operations.Read.Name);
            var update = new Claim(Operations.ClaimTypeForDbWork, Operations.Update.Name);
            var delete = new Claim(Operations.ClaimTypeForDbWork, Operations.Delete.Name);

         

            if(await userManager.FindByNameAsync(adminEmail) == null)
            {
                AppUser admin = new AppUser { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if(result.Succeeded)
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

                foreach(var item in users)
                {
                    IdentityResult result1 = await userManager.CreateAsync(item, item.Email);
                    if(result1.Succeeded)
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
