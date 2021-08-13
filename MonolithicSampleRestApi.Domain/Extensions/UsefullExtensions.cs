using System;
using System.Collections.Generic;
using System.Text;

namespace MonolithicSampleRestApi.Domain.Extensions
{
    public static class UsefullExtensions
    {


        public static double ToDouble(this int s)
        {
            try
            {
                return Convert.ToDouble(s);
            }
            catch
            {
                return 0;
            }

        }

        public static double? ToDoubleOrNull(this int s)
        {
            try
            {
                return Convert.ToDouble(s);
            }
            catch
            {
                return (double?)null;
            }

        }

        public static double ToDouble(this decimal s)
        {
            try
            {
                return Convert.ToDouble(s);
            }
            catch
            {
                return 0;
            }

        }

        public static double? ToDoubleOrNull(this decimal s)
        {
            try
            {
                return Convert.ToDouble(s);
            }
            catch
            {
                return (double?)null;
            }

        }

        public static decimal ToDecimal(this double s)
        {
            try
            {
                return Convert.ToDecimal(s);
            }
            catch
            {
                return 0;
            }
        }

        public static decimal? ToDecimalOrNull(this double s)
        {
            try
            {
                return Convert.ToDecimal(s);
            }
            catch
            {
                return (decimal?)null;
            }

        }
    }
}
