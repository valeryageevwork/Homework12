using System;
using System.Collections.Generic;

namespace Homework11
{
    public class Manager : Consultant, IChangingRecords
    {
        public new string Name
        {
            get => name;
            set => name = value;
        }
        public new string Surname
        {
            get => surname;
            set => surname = value;
        }
        public new string PhoneNumber
        {
            get => phone_number;
            set => phone_number = value;
        }
        public new string PassportNumber
        {
            get => passport_number;
            set => passport_number = value;
        }
        public new string PassportSeries
        {
            get => passport_series;
            set => passport_series = value;
        }

        public Manager(string name, string surname, string phone_number, 
                       string passport_series, string passport_number,
                       DataOfRecord data_of_record) :
                  base(name, surname, phone_number, 
                       passport_series, passport_number, 
                       data_of_record)
        {
            PassportNumber = passport_number;
            PassportSeries = passport_series;
        }

        private class SortBySurname : IComparer<Manager>
        {
            public int Compare(Manager x, Manager y)
            {
                if (x.Surname == y.Surname)
                    return 0;
                else if (String.Compare(x.Surname, y.Surname) > 0)
                    return 0;

                return -1;
            }
        }

        public static IComparer<Manager> SortedBy()
        {
            return new SortBySurname();
        }
    }
}
