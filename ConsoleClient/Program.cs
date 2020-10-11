using DB_Engine.Implementations.Servises;
using DB_Engine.Interfaces;
using DB_Engine.Types;
using DB_Engine.Validators;
using System;
using System.Collections.Generic;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "Test";
            long fileSize = 1000000;
            string path = @"D:\Programming\4term\IT\Proj";
            //IDataBaseService dataBaseService = new DataBaseService(name, path, fileSize);
            IDataBaseService dataBaseService = new DataBaseService(path + "\\Test.vldb");
            //dataBaseService.AddTable("FirstTable");

            var table = dataBaseService.GetEntityService("FirstTable");

            //table.AddColumn("Name", DataValueType.StringDataValueTypeId);
            //table.AddColumn("Age", DataValueType.IntegerDataValueTypeId,
            //    new List<IValidator> { new Validator<int>(ComparsonType.Greater, 0) });
            //table.AddColumn("Income", DataValueType.IntegerDataValueTypeId);
            //var data = new List<List<object>>()
            //{
            //     new List<object>{"name1", 12, 1 },
            //     new List<object>{"name3", -12, 2},
            //     new List<object>{"name2", 124, -10},
            //};
            //table.InsertRange(data);


            var selectData = table.Select();
        }
    }
}
