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
        public static void ConsoleOut(List<List<object>> table, bool flag = false)
        {
            foreach (var row in table)
            {
                foreach (var elem in row)
                {
                    Console.Write(elem.ToString() + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            if (flag)
                Console.WriteLine("--------------------------------------------------");

        }
        static void Main(string[] args)
        {
            string name = "Test";
            long fileSize = 1000000;
            string path = @"D:\Programming\4term\IT\Proj";
            //IDataBaseService dataBaseService = new DataBaseService(name, path, fileSize);
            IDataBaseService dataBaseService = new DataBaseService(path + "\\Test.vldb");
            //dataBaseService.AddTable("Table1");
            //dataBaseService.AddTable("Table2");
            var table1 = dataBaseService.GetEntityService("Table1");
            var table2 = dataBaseService.GetEntityService("Table2");
            //table2.AddColumn("Name2", DataValueType.StringDataValueTypeId);
            //table2.AddColumn("Age2", DataValueType.IntegerDataValueTypeId);
            //table2.AddColumn("Income2", DataValueType.IntegerDataValueTypeId);
            //var data2 = new List<List<object>>()
            //{
            //     new List<object>{"name1", 10, 3 },
            //     new List<object>{"name3", -12, 2},
            //     new List<object>{"name2", 124, -10}
            //};
            //table2.InsertRange(data2);
            var resultInnerJoin = table1.InnerJoin(table2.Entity, new Tuple<string, string>("Name", "Name2"),false);
            var resultIntersect = table1.CrossJoin(table2.Entity,false);

            ConsoleOut(table1.Select());
            ConsoleOut(table2.Select());

            ConsoleOut(resultIntersect, true);

            ConsoleOut(resultInnerJoin, true);

            //table1.AddColumn("Name", DataValueType.StringDataValueTypeId);
            //table1.AddColumn("Age", DataValueType.IntegerDataValueTypeId,
            //    new List<IValidator> { new Validator<int>(ComparsonType.Greater, 0) });
            //table1.AddColumn("Income", DataValueType.IntegerDataValueTypeId);
            //var data = new List<List<object>>()
            //{
            //     new List<object>{"name1", 10, 1 },
            //     new List<object>{"name3", -12, 2},
            //     new List<object>{"name2", 124, -10}
            //};
            //table1.InsertRange(data);


            //var selectData = table.Select();
        }
    }
}
