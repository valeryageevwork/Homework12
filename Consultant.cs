using System;
using System.Collections.Generic;

namespace Homework11
{
    public class Consultant : IChangingRecords
    {
        protected string name;
        protected string surname;
        protected string phone_number;
        protected string passport_number;
        protected string passport_series;
        protected DataOfRecord data_of_record;

        public string Name
        {
            get => name;
        }
        public string Surname
        {
            get => surname;
        }
        public string PhoneNumber
        {
            get => phone_number;
            set => phone_number = value;
        }
        public string PassportNumber
        {
            get => passport_number;
            set
            {
                if (value != null)
                    passport_number = "****";
                else
                    passport_number = null;
            }
        }
        public string PassportSeries
        {
            get => passport_series;
            set
            {
                if (value != null)
                    passport_series = "******";
                else
                    passport_series = null;
            }
        }
        public DataOfRecord DataRecord
        {
            get
            {
                return data_of_record;
            }
        }

        public Consultant(string name, string surname, string phone_number,
                          string passport_series, string passport_number,
                          DataOfRecord data_of_record)
        {
            this.name = name;
            this.surname = surname;
            PhoneNumber = phone_number;
            PassportNumber = passport_number;
            PassportSeries = passport_series;
            this.data_of_record = data_of_record;
        }

        private class SortBySurname : IComparer<Consultant>
        {
            public int Compare(Consultant x, Consultant y)
            {
                if (x.Surname == y.Surname)
                    return 0;
                else if (String.Compare(x.Surname, y.Surname) > 0)
                    return 0;

                return -1;
            }
        }

        public static IComparer<Consultant> SortedBy()
        {
            return new SortBySurname();
        }
    }
}
