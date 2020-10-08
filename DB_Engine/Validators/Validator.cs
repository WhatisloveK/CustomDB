using DB_Engine.Exceptions;
using DB_Engine.Extentions;
using DB_Engine.Interfaces;
using System;
using System.Collections.Generic;

namespace DB_Engine.Validators
{
    public class Validator<T> : BaseValidator<T> where T : IComparable<T>
    {
        public override string Type => typeof(Validator<T>).AssemblyQualifiedName;

        public Validator(ComparsonType comparsonType, T value) : base(comparsonType, value)
        {
            string errorMessage = $"ComparsonType = \"{_comparsonType}\" doesn't valid for Validator<{typeof(T).Name}>.\n" +
                    $"Available comparson types :[{ComparsonType.Greater}, {ComparsonType.GreaterOrEqual}, " +
                    $"{ComparsonType.Less}, {ComparsonType.LessOrEqual}, {ComparsonType.Equal}, {ComparsonType.NotEqual}]";

            if (typeof(T) == typeof(string))
            {
                errorMessage = $"ComparsonType = \"{_comparsonType}\" doesn't valid for Validator<string>.\n" +
                    $"Available comparson types :[0,1,2,3,4,5,6,7,8,9,10,11,12,13]";

                _comparsonFunc.AddRange(new Dictionary<ComparsonType, Func<T, bool>>
                {
                    [ComparsonType.Contains] = new Func<T, bool>(x => Convert.ToString(x).Contains(Convert.ToString(_comparsonValue))),
                    [ComparsonType.NotContains] = new Func<T, bool>(x => !Convert.ToString(x).Contains(Convert.ToString(_comparsonValue))),
                    [ComparsonType.IsNull] = new Func<T, bool>(x => String.IsNullOrEmpty(Convert.ToString(x))),
                    [ComparsonType.IsNotNull] = new Func<T, bool>(x => String.IsNullOrEmpty(Convert.ToString(x))),
                    [ComparsonType.StartsWith] = new Func<T, bool>(x => Convert.ToString(x).StartsWith(Convert.ToString(_comparsonValue))),
                    [ComparsonType.NotStarstWith] = new Func<T, bool>(x => !Convert.ToString(x).StartsWith(Convert.ToString(_comparsonValue))),
                    [ComparsonType.EndsWith] = new Func<T, bool>(x => Convert.ToString(x).EndsWith(Convert.ToString(_comparsonValue))),
                    [ComparsonType.NotEndsWith] = new Func<T, bool>(x => !Convert.ToString(x).EndsWith(Convert.ToString(_comparsonValue)))
                });
            }
            if (!_comparsonFunc.TryGetValue(_comparsonType, out _))
            {
                throw new ComparsonTypeException(errorMessage);
            }
        }
    }
}
