
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace GrpcClient
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
            switch (dataValueTypeId)
            {
                case var t when t == IntegerDataValueTypeId:
                    return typeof(int);
                case var t when t == RealDataValueTypeId:
                    return typeof(double);
                case var t when t == CharDataValueTypeId:
                    return typeof(char);
                case var t when t == StringDataValueTypeId:
                    return typeof(string);
                case var t when t == ComplexIntegerDataValueTypeId:
                    return typeof(ComplexInteger);
                case var t when t == ComplexRealDataValueTypeId:
                    return typeof(ComplexReal);
                case var t when t == UniqueidentifierDataValueTypeId:
                    return typeof(Guid);
                default:
                    throw new DataValueTypeException($"Incorrect DataValueTypeId specified. IncorrectDataValueTypeId = \"{dataValueTypeId}\"");
            }
        }

        public static Guid GetDataValueType(string type)
        {
            switch (type)
            {
                case "int":
                    return IntegerDataValueTypeId;
                case "double":
                    return RealDataValueTypeId;
                case "char":
                    return CharDataValueTypeId;
                case "string":
                    return StringDataValueTypeId;
                case "cmplxInt":
                    return ComplexIntegerDataValueTypeId;
                case "cmplxReal":
                    return ComplexRealDataValueTypeId;
                default:
                    throw new DataValueTypeException($"Incorrect Type specified. TypeName = \"{type}\"");
            }
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
                return _parseStrigns[dataValueType](value);
            }
            catch (Exception ex)
            {
                throw new InvalidCastException("Unsupported type", ex);
            }
        }

        private static Dictionary<Guid, Func<string, object>> _parseStrigns =>
            new Dictionary<Guid, Func<string, object>>
            {
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
