namespace FlightManagementSystem
{
    internal class Flight
    {
        public string FlightNumber { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime DepartureTime { get; set; }
        public int AvailableSeat { get; set; }

        public Flight()
        {
            GenerateFlightNumber();
        }

        public void GenerateFlightNumber()
        {
            string number = string.Empty;
            Random rand = new Random();
            for (int i = 0; i < 2; i++)
            {
                number += (char)rand.Next(65, 91);
            }
            number += "-";
            number += rand.Next(100, 1000).ToString();

            FlightNumber = number;
        }
        public override string ToString()
        {
            return $"FlightNumber {FlightNumber} DepartureCity {DepartureCity} " +
                $"ArrivalCity {ArrivalCity} DepartureTime {DepartureTime.ToShortDateString()} " +
                $"AvailableSeat {AvailableSeat}";
        }
    }
}
