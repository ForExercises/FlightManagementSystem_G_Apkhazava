namespace FlightManagementSystem
{
    internal class Reservation
    {
        public string ReservationNumber { get; set; }
        public string PassengerName { get; set; }
        public Flight Flight { get; set; } = new Flight();

        public Reservation()
        {
            GenerateReservationNumber();
        }
        public void GenerateReservationNumber()
        {
            string number = string.Empty;
            Random rand = new Random();
            for (int i = 0; i < 2; i++)
            {
                number += (char)rand.Next(65, 91);
            }
            number += "-";
            number += rand.Next(100, 1000).ToString();

            ReservationNumber = number;
        }
        public override string ToString()
        {
            return $"ReservationNumber {ReservationNumber} PassengerName {PassengerName}" +
                $"\n\t[Flight] {Flight}";
        }
    }
}
