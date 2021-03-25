using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Service.ViewModel.ValidationAttributes
{
    public class MinValueAttribute : ValidationAttribute
    {
        private readonly int MinValue;

        public MinValueAttribute(int minValue)
        {
            this.MinValue = minValue;
        }

        public override bool IsValid(object value)
        {
            return (int)value > this.MinValue;
        }
    }
}
