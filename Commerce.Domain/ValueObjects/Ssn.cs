using Commerce.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using SlxLuhnLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Commerce.Domain.ValueObjects
{
    [Owned]
    public class Ssn : IEquatable<Ssn?>
    {
        private string? _ssn = null;

        public static Ssn Parse(string ssn)
        {
            var withoutHyphen = ssn
                .Replace("-", string.Empty)
                .Replace("+", string.Empty);

            if (withoutHyphen.Length != 10 && withoutHyphen.Length != 12)
            {
                throw new InvalidSsnException("Too long");
            }

            if (!withoutHyphen
                .All(ch => char.IsDigit(ch)))
            {
                throw new InvalidSsnException("Expected digits");
            }

            //string year;
            //string month;
            //string day;


            string originalSsn = withoutHyphen;

            int year = 0;

            string? rest = null;

            if (withoutHyphen.Length == 10)
            {
                year += int.Parse(ssn[0..2]);
                rest = ssn[2..];
            }
            else
            {
                if (withoutHyphen.Length == 12)
                {
                    year = int.Parse(ssn[0..4]);
                    originalSsn = originalSsn[2..];
                    rest = ssn[4..];
                }
                else
                {
                    throw new InvalidSsnException();
                }
            }

            int month = int.Parse(rest[0..2]);
            int day = int.Parse(rest[2..4]);

            bool isOlderThanHundred = false;

            var str = rest[4];
            if (!int.TryParse(str.ToString(), out var _))
            {
                if (year.ToString().Length == 2)
                {
                    if (str == '-')
                    {
                        year += 1900;
                    }
                    else if (str == '+')
                    {
                        //year = ((2000 + year) - DateTime.Now.Year) >= 31;
                    }
                    else
                    {
                        throw new InvalidSsnException("Unexpected character");
                    }
                }

                rest = rest[1..];
            }

            try
            {
                var date = new DateTime(year, month, day);
            }
            catch (Exception)
            {
                throw new InvalidSsnException("Invalid date");
            }

            if (isOlderThanHundred && (DateTime.Now.Year - year < 100))
            {
                throw new InvalidSsnException("Expected person to be older than hundred");
            }

            int county = int.Parse(rest[4..6]);
            int gender = int.Parse(rest[6..7]);
            int checksum = int.Parse(rest[7..8]);

            if (!ClsLuhnLibrary.CheckLuhn_Base10(originalSsn))
            {
                throw new InvalidSsnException("Checksum is not correct");
            }

            return new Ssn
            {
                Year = year,
                Month = month,
                Day = day,
                BirthCounty = county,
                Gender = gender,
                Checksum = checksum,
            };
        }

        public int Year { get; private init; }

        public int Month { get; private init; }

        public int Day { get; private init; }

        public int BirthCounty { get; private init; }

        public int Gender { get; private init; }

        public int Checksum { get; private init; }

        public bool IsWoman => Gender % 2 == 0;

        public bool IsMan => !IsWoman;

        public override string ToString()
        {
            return _ssn?.ToString()!;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Ssn);
        }

        public bool Equals(Ssn? other)
        {
            return other != null &&
                   Year == other.Year &&
                   Month == other.Month &&
                   Day == other.Day &&
                   BirthCounty == other.BirthCounty &&
                   Gender == other.Gender &&
                   Checksum == other.Checksum;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Year, Month, Day, BirthCounty, Gender, Checksum);
        }

        public static bool operator ==(Ssn? left, Ssn? right)
        {
            return EqualityComparer<Ssn>.Default.Equals(left, right);
        }

        public static bool operator !=(Ssn? left, Ssn? right)
        {
            return !(left == right);
        }

        public static explicit operator Ssn(string ssn)
        {
            return Ssn.Parse(ssn);
        }

        public static implicit operator string(Ssn ssn)
        {
            return ssn?.ToString()!;
        }
    }
}
