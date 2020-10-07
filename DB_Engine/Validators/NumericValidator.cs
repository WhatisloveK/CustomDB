using DB_Engine.Exceptions;
using DB_Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Engine.Validators
{
    public class NumericValidator<T> : BaseValidator<T> where T : IComparable<T>
    {
        public override string Type => typeof(NumericValidator<T>).AssemblyQualifiedName;
        
        public NumericValidator(ComparsonType comparsonType, T value) : base(comparsonType, value)
        {
            _comparsonFunc = new Dictionary<ComparsonType, Func<T, bool>>
            {
                [ComparsonType.Greater] = new Func<T, bool>(x => x.CompareTo(_comparsonValue) == 1),
                [ComparsonType.GreaterOrEqual] = new Func<T, bool>(x => x.CompareTo(_comparsonValue) == 1 || x.CompareTo(_comparsonValue) == 0),
                [ComparsonType.Less] = new Func<T, bool>(x => x.CompareTo(_comparsonValue) == -1),
                [ComparsonType.LessOrEqual] = new Func<T, bool>(x => x.CompareTo(_comparsonValue) == -1 || x.CompareTo(_comparsonValue) == 0),
                [ComparsonType.Equal] = new Func<T, bool>(x => x.CompareTo(_comparsonValue) == 0),
                [ComparsonType.NotEqual] = new Func<T, bool>(x => x.CompareTo(_comparsonValue) != 0)
            };
            if (!_comparsonFunc.TryGetValue(_comparsonType, out _))
            {
                var message = $"ComparsonType = \"{_comparsonType}\" doesn't valid for NumericValidator.\n" +
                    $"Available comparson types :[{ComparsonType.Greater}, {ComparsonType.GreaterOrEqual}, " +
                    $"{ComparsonType.Less}, {ComparsonType.LessOrEqual}, {ComparsonType.Equal}, {ComparsonType.NotEqual}]";
                throw new ComparsonTypeException(message);
            }
        }

    }

    
}
