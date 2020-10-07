using DB_Engine.Exceptions;
using DB_Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DB_Engine.Validators
{
    public class StringValidator : BaseValidator                                                 
    {
        private string _comparsonValue;

        protected Dictionary<ComparsonType, Func<string, bool>> _comparsonFunc;
        public override string Type => typeof(StringValidator).AssemblyQualifiedName;
        public StringValidator(ComparsonType comparsonType, string value) : base(comparsonType) 
        {
            _comparsonValue = value;
            Value = value;
            _comparsonFunc = new Dictionary<ComparsonType, Func<string, bool>>
            {
                [ComparsonType.Greater] = new Func<string, bool>(x => x.CompareTo(_comparsonValue) == 1),
                [ComparsonType.GreaterOrEqual] = new Func<string, bool>(x => x.CompareTo(_comparsonValue) == 1 || x.CompareTo(_comparsonValue) == 0),
                [ComparsonType.Less] = new Func<string, bool>(x => x.CompareTo(_comparsonValue) == -1),
                [ComparsonType.LessOrEqual] = new Func<string, bool>(x => x.CompareTo(_comparsonValue) == -1 || x.CompareTo(_comparsonValue) == 0),
                [ComparsonType.Equal] = new Func<string, bool>(x => x.CompareTo(_comparsonValue) == 0),
                [ComparsonType.NotEqual] = new Func<string, bool>(x => x.CompareTo(_comparsonValue) != 0),
                [ComparsonType.Contains] = new Func<string, bool>(x => x.Contains(_comparsonValue)),
                [ComparsonType.NotContains] = new Func<string, bool>(x => !x.Contains(_comparsonValue)),
                [ComparsonType.IsNull] = new Func<string, bool>(x => String.IsNullOrEmpty(x)),
                [ComparsonType.IsNotNull] = new Func<string, bool>(x => String.IsNullOrEmpty(x)),
                [ComparsonType.StartsWith] = new Func<string, bool>(x => x.StartsWith(_comparsonValue)),
                [ComparsonType.NotStarstWith] = new Func<string, bool>(x => !x.StartsWith(_comparsonValue)),
                [ComparsonType.EndsWith] = new Func<string, bool>(x => x.EndsWith(_comparsonValue)),
                [ComparsonType.NotEndsWith] = new Func<string, bool>(x => !x.EndsWith(_comparsonValue))
            };

            if (!_comparsonFunc.TryGetValue(_comparsonType, out _))
            {
                var message = $"ComparsonType = \"{_comparsonType}\" doesn't valid for StringValidator.\n" +
                    $"Available comparson types :[0,1,2,3,4,5,6,7,8,9,10,11,12,13]";
                throw new ComparsonTypeException(message);
            }
        }
        public override bool IsValid(object actualValue)
        {
            return _comparsonFunc[_comparsonType]((string)actualValue);
        }
    }
}
