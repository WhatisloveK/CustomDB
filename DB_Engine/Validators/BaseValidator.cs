using DB_Engine.Interfaces;
using DB_Engine.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Engine.Validators
{
    public abstract class BaseValidator : IValidator
    {
        protected ComparsonType _comparsonType;

        public virtual object Value { get; set; }

        public virtual int ComparsonType { get; set; }

        public virtual string Type { get; set; }

        public virtual Guid DataValueTypeId { get; set; }

        public abstract bool IsValid(object actualValue);

        public abstract void Init();
        public BaseValidator() { }
        public BaseValidator(ComparsonType comparsonType)
        {
            _comparsonType = comparsonType;
            ComparsonType = (int)comparsonType;
        }
    }

    public abstract class BaseValidator<T> : BaseValidator where T : IComparable<T>
    {
        protected  T _comparsonValue;
        protected Dictionary<ComparsonType, Func<T, bool>> _comparsonFunc;
        protected string ValueType => typeof(T).AssemblyQualifiedName;

        public override Guid DataValueTypeId => DataValueType.GetDataValueType(typeof(T));
        public BaseValidator() { }
        public BaseValidator(ComparsonType comparsonType, T value): base(comparsonType)
        {
            _comparsonValue = value;
            Value = value;
            Init();
        }
        public override void Init()
        {
            _comparsonType = (ComparsonType)ComparsonType;
            _comparsonValue = (T)Value;
            _comparsonFunc = new Dictionary<ComparsonType, Func<T, bool>>
            {
                [Interfaces.ComparsonType.Greater] = new Func<T, bool>(x => x.CompareTo(_comparsonValue) == 1),
                [Interfaces.ComparsonType.GreaterOrEqual] = new Func<T, bool>(x => x.CompareTo(_comparsonValue) == 1 || x.CompareTo(_comparsonValue) == 0),
                [Interfaces.ComparsonType.Less] = new Func<T, bool>(x => x.CompareTo(_comparsonValue) == -1),
                [Interfaces.ComparsonType.LessOrEqual] = new Func<T, bool>(x => x.CompareTo(_comparsonValue) == -1 || x.CompareTo(_comparsonValue) == 0),
                [Interfaces.ComparsonType.Equal] = new Func<T, bool>(x => x.CompareTo(_comparsonValue) == 0),
                [Interfaces.ComparsonType.NotEqual] = new Func<T, bool>(x => x.CompareTo(_comparsonValue) != 0)
            };
        }

        public override bool IsValid(object actualValue)
        {
            return _comparsonFunc[_comparsonType]((T)actualValue);
        }
    }
}
