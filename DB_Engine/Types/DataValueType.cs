using DB_Engine.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DB_Engine.Types
{
    public class DataValueType
    {
        public static readonly Guid IntegerDataValueTypeId  = new Guid("{4f291941-5fc0-4c9b-ba3a-809bf1029084}");

        public static readonly Guid RealDataValueTypeId  = new Guid("{f13b9117-9e0f-40a9-983f-abbcd4dc1281}");

        public static readonly Guid CharDataValueTypeId  = new Guid("{04f1b105-ee3b-4de3-b730-fadccc724735}");

        public static readonly Guid StringDataValueTypeId  = new Guid("{4dda6f8f-fc99-42ec-bdfe-a76b671df5e4}");

        public static readonly Guid ComplexIntegerDataValueTypeId  = new Guid("{620de49c-df9b-4eac-9bed-507652d88a61}");

        public static readonly Guid ComplexRealDataValueTypeId  = new Guid("{64b02720-d6bc-454c-8994-1cd25f733ac0}");

        public static readonly Guid UniqueidentifierDataValueTypeId = new Guid("{93faf9ca-1bbf-4d7c-bda3-7b3244f18cda}");

        public static Type GetType(Guid dataValueTypeId)
        {
            return dataValueTypeId switch
            {
                var t when t == IntegerDataValueTypeId => typeof(int),
                var t when t == RealDataValueTypeId => typeof(double),
                var t when t == CharDataValueTypeId => typeof(char),
                var t when t == StringDataValueTypeId => typeof(string),
                var t when t == ComplexIntegerDataValueTypeId => typeof(ComplexInteger),
                var t when t == ComplexRealDataValueTypeId => typeof(ComplexReal),
                var t when t == UniqueidentifierDataValueTypeId => typeof(Guid),
                _ => throw new DataValueTypeException($"Incorrect DataValueTypeId specified. IncorrectDataValueTypeId = \"{dataValueTypeId}\""),
            };
        }

        public static Guid GetDataValueType(Type type)
        {
            return type switch
            {
                var t when t == typeof(int) => IntegerDataValueTypeId,
                var t when t == typeof(double) => RealDataValueTypeId,
                var t when t == typeof(char) => CharDataValueTypeId,
                var t when t == typeof(string) => StringDataValueTypeId,
                var t when t == typeof(ComplexInteger) => ComplexIntegerDataValueTypeId,
                var t when t == typeof(ComplexReal) => ComplexRealDataValueTypeId,
                var t when t == typeof(Guid) => UniqueidentifierDataValueTypeId,
                _ => throw new DataValueTypeException($"Incorrect Type specified. TypeName = \"{type.Name}\""),
            };
        }


        public static bool IsValidValue(Guid dataValueTypeId, object value)
        {
            var currentType = GetType(dataValueTypeId);

            try
            {
                Convert.ChangeType(value, currentType);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static object GetTypedValue(Guid dataValueType, string value)
        {
            try
            {
                return ParseStrigns[dataValueType](value);
            }
            catch (Exception ex)
            {
                throw new InvalidCastException("Unsupported type", ex);
            }
        }

        private static Dictionary<Guid, Func<string, object>> ParseStrigns =>
            new Dictionary<Guid, Func<string, object>>
            {
                [UniqueidentifierDataValueTypeId] = new Func<string, object>(x => Guid.Parse(x)),
                [CharDataValueTypeId] = new Func<string, object>(x => x.First()),
                [IntegerDataValueTypeId] = new Func<string, object>(x => int.Parse(x)),
                [RealDataValueTypeId] = new Func<string, object>(x => double.Parse(x)),
                [StringDataValueTypeId] = new Func<string, object>(x => x),
                [ComplexIntegerDataValueTypeId] = new Func<string, object>(x =>
                {
                    try
                    {
                        return JsonSerializer.Deserialize<ComplexInteger>(x);
                    }
                    catch
                    {
                        var regexForRealPart = new Regex(@"^(?=[iI.\d+-])([+-]?(?:\d+(?:\.\d*)?|\.\d+)(?:[eE][+-]?\d+)?(?![iI.\d]))?");
                        var regexForImagePart = new Regex(@"([+-]?(?:(?:\d+(?:\.\d*)?|\.\d+)(?:[eE][+-]?\d+)?)?[iI])?$");
                        var matchesReal = regexForRealPart.Matches(x);
                        var matchesImage = regexForImagePart.Matches(x);
                        var result = new ComplexInteger
                        {
                            RealValue = (matchesReal.Count == 1 && matchesReal[0].Value != "") ? int.Parse(matchesReal[0].Value) : 0,
                            ImageValue = (matchesImage.Count >= 1 && matchesImage[0].Value != "") ? int.Parse(matchesImage[0].Value.Remove(matchesImage[0].Value.Length - 1)) : 0
                        };
                        return result;
                    }
                }),
                [ComplexRealDataValueTypeId] = new Func<string, object>(x => 
                {
                    try
                    {
                        return JsonSerializer.Deserialize<ComplexReal>(x);
                    }
                    catch
                    {
                        var regexForRealPart = new Regex(@"^(?=[iI.\d+-])([+-]?(?:\d+(?:\.\d*)?|\.\d+)(?:[eE][+-]?\d+)?(?![iI.\d]))?");
                        var regexForImagePart = new Regex(@"([+-]?(?:(?:\d+(?:\.\d*)?|\.\d+)(?:[eE][+-]?\d+)?)?[iI])?$");
                        var matchesReal = regexForRealPart.Matches(x);
                        var matchesImage = regexForImagePart.Matches(x);
                        var result = new ComplexReal
                        {
                            RealValue = (matchesReal.Count == 1 && matchesReal[0].Value != "") ? double.Parse(matchesReal[0].Value) : 0,
                            ImageValue = (matchesImage.Count >= 1 && matchesImage[0].Value != "") ? double.Parse(matchesImage[0].Value.Remove(matchesImage[0].Value.Length - 1)) : 0
                        };
                        return result;
                    }
                })
            };
    }

}
