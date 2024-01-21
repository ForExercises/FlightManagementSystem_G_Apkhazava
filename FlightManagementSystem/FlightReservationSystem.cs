using FlightManagementSystem.Enums;

namespace FlightManagementSystem
{
    internal class FlightReservationSystem
    {
        #region Databases
        public List<Flight> Flights { get; set; } = new List<Flight>()
        {
            new Flight()
            {
                DepartureCity = "KUT",
                ArrivalCity = "BCN",
                DepartureTime = new DateTime(2024,1,20),
                AvailableSeat = 19,
            },
            new Flight()
            {
                DepartureCity = "KUT",
                ArrivalCity = "BCN",
                DepartureTime = new DateTime(2024,1,22),
                AvailableSeat = 35,
            },
            new Flight()
            {
                DepartureCity = "KUT",
                ArrivalCity = "DOH",
                DepartureTime = new DateTime(2024,1,20),
                AvailableSeat = 25,
            },
        };
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

        public List<User> Users { get; set; } = new List<User>()
        {
            new User(){UserName="Guga",Password="Guga123",UserType=EmployeeType.ADMIN}
        };
        #endregion

        #region User FUnctions
        public void AddUser(User user)
        {
            Users.Add(user);
        }
        public User GetActiveUser()
        {
            foreach (var item in Users)
            {
                if (item.Isactive == true)
                    return item;
            }
            return null;
        }
        public bool SignIn(string username,string password)
        {
            foreach (var item in Users)
            {
                if (item.UserName.Equals(username) && item.Password.Equals(password))
                { item.Isactive = true;  return true; }
            }
            return false;
        }
        public bool SignOut(User usr)
        {
            usr.Isactive = false;
            return true;
        }
        #endregion

        #region MainFunction
        public void MainFUnctions()
        {
            int closecondition = 0;
            do
            {
                #region Momxmareblebis registracia
                int userregister = 0;
                do
                {
                    Console.WriteLine( "do not  have  account ? let's  register one , if  have  just  press   any key ");
                    User us = new User();
                    Console.WriteLine("enter type of user");
                    var type = Console.ReadLine();
                    switch (type)
                    {
                        case "admin":
                            us.UserType = EmployeeType.ADMIN;
                            break;
                        case "user":
                            us.UserType = EmployeeType.USER;
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine("enter user name");
                    string name = Console.ReadLine();
                    us.UserName = name;
                    Console.WriteLine("please enter password");
                    us.Password = Console.ReadLine();
                    Console.WriteLine( "account succesfully register if  want  sign in just type  1");
                    userregister = int.Parse(Console.ReadLine());
                } while (userregister!=1);
                #endregion

                #region sistemashi shesvla 
                bool shedegofsign = false;
                do
                {
                    Console.WriteLine("want use guest account ?  put 1");
                    int a =int.Parse( Console.ReadLine());
                    if (a == 1) break;
                    Console.WriteLine( "plese  enter user name");
                    string username = Console.ReadLine();
                    Console.WriteLine("please enter password");
                    string password = Console.ReadLine();

                   shedegofsign= SignIn(username, password);

                } while (shedegofsign!=true);

                #endregion

                Console.WriteLine( "momxarebeli warmatebit shevida sistemashi ");

                User usr = this.GetActiveUser();
                if(usr==null)
                {
                    Console.WriteLine( "Hello Guest , Welcome  ,  You  can still explore  some  futures  without  registration");
                    usr = new User()
                    {
                        UserName = "Guest",
                        UserType = EmployeeType.GUEST,
                        Isactive = false,
                        Password = "No Need Password"
                    };
                }
                int stopreserve = 0;
                do
                {
                    ShowIntro();

                    stopreserve = int.Parse(Console.ReadLine());

                    switch (stopreserve)
                    {
                        case 1:
                            DoReservation(usr);
                            break;
                        case 2:
                            showReservationForUser();
                            break;
                        case 3:
                            CancelReservationForUserNow(usr);
                            break;
                        case 4:
                            ShowFlights(usr);
                            break;
                        case 5: continue;

                        case 7:
                            ShowReservations(usr);
                            break;

                        default:
                            Console.WriteLine( "Naxvamdis ");
                            break;
                    }
                  
                } while (stopreserve!=-1);

                usr.Isactive = false;//  gamovagdot sistemidan momxarebeli
                Console.WriteLine("want use program ? put 1 , Enter -1  to  quit");
                closecondition = int.Parse(Console.ReadLine());
            } while (closecondition!=-1);

        }
        #endregion

        #region Helper Functions
        private void CancelReservationForUserNow(User usr)
        {
            Console.WriteLine( "Enter Reservation Number");
            string resn = Console.ReadLine();
            var reservat = GEtReservationByNumber(resn);
            if (reservat == null)
            {
                Console.WriteLine( " no reservation exist for this number");
                return;
            }
            CancelReservation(reservat, usr);
        }

        private void showReservationForUser()
        {
            Console.WriteLine("Enter the UserName");
            string Name = Console.ReadLine();
            var res = from re in Reservations
                      where re.PassengerName.Equals(Name)
                      select re;
            foreach (var item in res.ToList())
            {
                Console.WriteLine(item);
            }
        }

        private void DoReservation(User usr)
        {
            Console.WriteLine("Let's  Buy a Ticket");
            Console.WriteLine("Enter FLight Number");
            string fn = Console.ReadLine();
            Console.WriteLine("Enter THe passenger Name");
            string Name = Console.ReadLine();
            this.BookTicket(this.GetFlight(fn, usr), Name, usr);
            Console.WriteLine("Reservations");
            this.ShowReservations(usr);
        }
    

        private  void ShowIntro()
        {
            Console.WriteLine( "Hello , Welcome AGain ,  CHoice  some options ");
            Console.WriteLine( "1. Buy a  ticket ");
            Console.WriteLine( " 2.  show  your  reservations  ,  if  any ");
            Console.WriteLine( "3. cancel  the reservation ");
            Console.WriteLine( "4. show  flights");
            Console.WriteLine( "5 . use again ");
            Console.WriteLine( " -1 . for exit;");
            Console.WriteLine(  "7. show all reservation");
            Console.WriteLine("for refresh , put any key ");

        }
        #endregion

        #region Flight FUnctions
        public Reservation GetReservation(string reservationNumber,User user)
        {
            if (user.UserType == EmployeeType.GUEST)
            {
                Console.WriteLine("You are not allowed first  signed in ");
                return null;
            }
            foreach (var item in Reservations)
            {
                if (item.ReservationNumber == reservationNumber)
                    return item;
            }
            return null;
        }
        private Flight GetFlight(string flightNumber,User usr)
        {
         
            foreach (var item in Flights)
            {
                if (item.FlightNumber == flightNumber)
                    return item;
            }
            return null;
        }
        public Reservation GEtReservationByNumber(string ReservationNumber)
        {
            foreach (var item in Reservations)
            {
                if (item.ReservationNumber == ReservationNumber) return item;
            }
            return null;
        }
        public void CancelReservation(Reservation reservation,User usr)
        {
            if (usr.UserType == EmployeeType.GUEST)
            {
                Console.WriteLine("GUest Not allowed  there");
                return;
            }
            var res = from re in Reservations
                      where re.Equals(reservation)
                      select re;

            var actual= res.FirstOrDefault();
            foreach (var item in Flights)// igive ufro martivad shegvidzlia linkit
            {
                if(item.Equals(actual.Flight))
                {
                    item.AvailableSeat++; // adgili gamotavisuflda
                }
            }
            Reservations.Remove(reservation);
        }
        public void BookTicket(Flight flight,string passengerName,User usr)
        {
            if (usr.UserType != EmployeeType.ADMIN)
            {
                Console.WriteLine("you Must BE a admin ");
                return;
            }
            flight.AvailableSeat--;//  bileti gaiyida da adgili agar unda iyos
            if (flight.AvailableSeat < 0)
            {
                Console.WriteLine("Samwuxarod mocemul frenaze agar aris adgilebi ");
                return;
            }
            Reservations.Add(new Reservation()
            {
                Flight = flight,
                PassengerName = passengerName
            });
        }
        public void SearchFlight(string departureCity, string arrivalCity, DateTime departureTime,User usr)
        {
            // yvelas sheudzlia am  funqciis gamoyeneba 
           
            foreach (var item in Flights)
            {
                if(item.DepartureCity == departureCity&&item.ArrivalCity == arrivalCity&&
                    item.DepartureTime == departureTime)
                {
                    Console.WriteLine(item);
                }
            }
        }
        public void SearchFlight(string departureCity, string arrivalCity, User usr)
        {
            // yvelas sheudzlia am  funqciis gamoyeneba 
            foreach (var item in Flights)
            {
                if (item.DepartureCity == departureCity && item.ArrivalCity == arrivalCity)
                {
                    Console.WriteLine(item);
                }
            }
        }
        public void SearchFlight(User usr)
        {
            // yvelas sheudzlia am  funqciis gamoyeneba 
            string departureCity = Console.ReadLine();
            string arrivalCity = Console.ReadLine();
            foreach (var item in Flights)
            {
                if (item.DepartureCity == departureCity && item.ArrivalCity == arrivalCity)
                {
                    Console.WriteLine(item);
                }
            }
        }
        public void ShowReservations(User usr)
        {
            if (usr.UserType == EmployeeType.GUEST)
            {
                Console.WriteLine("You are not allowed first  signed in ");
                return;
            }
            foreach (var item in Reservations)
            {
                Console.WriteLine(item);
            }
        }
        public void ShowFlights(User usr)
        {
            // yvelas sheudzlia am  funqciis gamoyeneba 
            foreach (var item in Flights)
            {
                Console.WriteLine(item);
            }
        }
        #endregion
    }
}
