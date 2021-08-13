using System;
using System.Collections.Generic;
using System.Text;

namespace MonolithicSampleRestApi.Domain.Models.ApiModels
{
    public class RequestCalcInterest
    {
        public RequestCalcInterest()
        {

        }

        public RequestCalcInterest(decimal initialValue, int monthQuantity)
        {
            this.InitialValue = initialValue;
            this.MonthQuantity = monthQuantity;
        }

        public decimal InitialValue { get; set; }
        public int MonthQuantity { get; set; }
    } 

    public class CalcInterest
    {
        public CalcInterest()
        {

        }

        public CalcInterest(decimal value)
        {
            this.FormatedValue = value.ToString("N2");
            this.Value = value;
        }

        public string FormatedValue { get; set; }

        public decimal Value { get; set; }
    }

}
