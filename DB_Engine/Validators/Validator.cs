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

        public Validator() { }
        public Validator(ComparsonType comparsonType, T value) : base(comparsonType, value)
        {
            Init();
        }

        private string CastToString(T item)
        {
            return (string)Convert.ChangeType(item, typeof(string));
        }

        public override void Init()
        {
            base.Init();
            string errorMessage = $"ComparsonType = \"{_comparsonType}\" doesn't valid for Validator<{typeof(T).Name}>.\n" +
                    $"Available comparson types :[{Interfaces.ComparsonType.Greater}, {Interfaces.ComparsonType.GreaterOrEqual}, " +
                    $"{Interfaces.ComparsonType.Less}, {Interfaces.ComparsonType.LessOrEqual}, {Interfaces.ComparsonType.Equal}, {Interfaces.ComparsonType.NotEqual}]";

            if (typeof(T) == typeof(string))
            {
                errorMessage = $"ComparsonType = \"{_comparsonType}\" doesn't valid for Validator<string>.\n" +
                    $"Available comparson types :[0,1,2,3,4,5,6,7,8,9,10,11,12,13]";

                _comparsonFunc.AddRange(new Dictionary<ComparsonType, Func<T, bool>>
                {
                    [Interfaces.ComparsonType.Contains] = new Func<T, bool>(x => CastToString(x).Contains(CastToString(_comparsonValue))),
                    [Interfaces.ComparsonType.NotContains] = new Func<T, bool>(x => !CastToString(x).Contains(CastToString(_comparsonValue))),
                    [Interfaces.ComparsonType.IsNull] = new Func<T, bool>(x => string.IsNullOrEmpty(CastToString(x))),
                    [Interfaces.ComparsonType.IsNotNull] = new Func<T, bool>(x => !string.IsNullOrEmpty(CastToString(x))),
                    [Interfaces.ComparsonType.StartsWith] = new Func<T, bool>(x => CastToString(x).StartsWith(CastToString(_comparsonValue))),
                    [Interfaces.ComparsonType.NotStarstWith] = new Func<T, bool>(x => !CastToString(x).StartsWith(CastToString(_comparsonValue))),
                    [Interfaces.ComparsonType.EndsWith] = new Func<T, bool>(x => CastToString(x).EndsWith(CastToString(_comparsonValue))),
                    [Interfaces.ComparsonType.NotEndsWith] = new Func<T, bool>(x => !CastToString(x).EndsWith(CastToString(_comparsonValue)))
                });
            }
            if (!_comparsonFunc.TryGetValue(_comparsonType, out _))
            {
                throw new ComparsonTypeException(errorMessage);
            }
        }
    }
}
