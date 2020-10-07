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

    public abstract class BaseValidator<T> : BaseValidator
    {

        protected  T _comparsonValue;
        protected Dictionary<ComparsonType, Func<T, bool>> _comparsonFunc;

        public BaseValidator(ComparsonType comparsonType, T value): base(comparsonType)
        {
            _comparsonValue = value;
            Value = value;
        }
        public override bool IsValid(object actualValue)
        {
            return _comparsonFunc[_comparsonType]((T)actualValue);
        }
    }

    
}
