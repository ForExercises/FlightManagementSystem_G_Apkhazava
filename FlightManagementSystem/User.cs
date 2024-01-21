using FlightManagementSystem.Enums;

namespace FlightManagementSystem
{
    public class User
    {
        public User()
        {
            InicializeID();
                
        }
        public string USerID { get; set; }
        public string  UserName { get; set; }

        public  EmployeeType UserType { get; set; }

        public string Password { get; set; }

        public bool Isactive { get; set; }

        private void InicializeID()
        {
            var rand = new Random();
            var chr= (char)rand.Next(23, 64);
            string res = "";
            res += chr;
            for (int i = 0; i < 10; i++)
            {
                res += rand.Next(i, i + 100);
            }
            USerID = res;
        }

        public override string ToString()
        {
            return string.Format("Username {0} UserID {1} UserType {2} Isactive ? {3}", UserName, USerID, UserType, Isactive);
        }
    }
}
