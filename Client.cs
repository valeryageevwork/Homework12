namespace Homework11
{
    public class Client
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string PassportNumber { get; set; }
        public string PassportSeries { get; set; }

        public Client (string name, string surname, string phone_number, string passport_number, string passport_series)
        {
            Name = name;
            Surname = surname;
            PhoneNumber = phone_number;
            PassportNumber = passport_number;
            PassportSeries = passport_series;
        }
    }
}
