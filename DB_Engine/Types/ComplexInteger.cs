using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Engine.Types
{
    public class ComplexInteger
    {
        public int RealValue { get; set; }

        public int ImageValue { get; set; }

        public override bool Equals(object obj)
        {
            var otherInterval = (ComplexInteger)obj;

            return RealValue == otherInterval.RealValue
                && ImageValue == otherInterval.ImageValue;
        }

        public override string ToString()
        {
            return RealValue.ToString() + ImageValue.ToString() + "i";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
