using DB_Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Engine.Validators
{
    public abstract class BaseValidator : IValidator
    {
        protected ComparsonType _comparsonType;

        public virtual object Value { get; set; }

        public virtual int Operation { get; set; }

        public virtual string Type { get; set; }

        public abstract bool IsValid(object actualValue);
        public BaseValidator(ComparsonType comparsonType)
        {
            _comparsonType = comparsonType;
            Operation = (int)comparsonType;
        }
    }

    public abstract class BaseValidator<T> : BaseValidator where T : IComparable<T>
    {

        protected  T _comparsonValue;
        protected Dictionary<ComparsonType, Func<T, bool>> _comparsonFunc;

        public BaseValidator(ComparsonType comparsonType, T value): base(comparsonType)
        {
            _comparsonValue = value;
            Value = value;
            _comparsonFunc = new Dictionary<ComparsonType, Func<T, bool>>
            {
                [ComparsonType.Greater] = new Func<T, bool>(x => x.CompareTo(_comparsonValue) == 1),
                [ComparsonType.GreaterOrEqual] = new Func<T, bool>(x => x.CompareTo(_comparsonValue) == 1 || x.CompareTo(_comparsonValue) == 0),
                [ComparsonType.Less] = new Func<T, bool>(x => x.CompareTo(_comparsonValue) == -1),
                [ComparsonType.LessOrEqual] = new Func<T, bool>(x => x.CompareTo(_comparsonValue) == -1 || x.CompareTo(_comparsonValue) == 0),
                [ComparsonType.Equal] = new Func<T, bool>(x => x.CompareTo(_comparsonValue) == 0),
                [ComparsonType.NotEqual] = new Func<T, bool>(x => x.CompareTo(_comparsonValue) != 0)
            };
        }
        public override bool IsValid(object actualValue)
        {
            return _comparsonFunc[_comparsonType]((T)actualValue);
        }
    }
}
