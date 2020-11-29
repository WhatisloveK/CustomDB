using DB_Engine.Factories;
using DB_Engine.Implementations.Servises;
using DB_Engine.Interfaces;
using DB_Engine.Models;
using DB_Engine.Types;
using DB_Engine.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;

namespace DB_EngineTest
{
    [TestFixture]
    public class EntityServiceTest
    {

        //private IStorage _storage;
        //private IDataBaseService dataBaseService;
        

        //[SetUp]
        //public void Initialize()
        //{
        //    string name = "EntityServiceTest";
        //    long fileSize = 1000000;
        //    string path = @"D:\Programming\4term\IT\Proj";
        //    dataBaseService = new DataBaseService(name, path, fileSize);
            
        //    dataBaseService.AddTable("Table1");
        //    dataBaseService.AddTable("Table2");
        //    dataBaseService.AddTable("Table3");
        //    var table3 = dataBaseService.GetEntityService("Table3");
        //    var table1 = dataBaseService.GetEntityService("Table1");
        //    var table2 = dataBaseService.GetEntityService("Table2");
        //    table2.AddColumn("Name2", DataValueType.StringDataValueTypeId);
        //    table2.AddColumn("Age2", DataValueType.IntegerDataValueTypeId);
        //    table2.AddColumn("Income2", DataValueType.IntegerDataValueTypeId);
        //    var data2 = new List<List<object>>()
        //    {
        //         new List<object>{"name1", 10, 3 },
        //         new List<object>{"name3", -12, 2},
        //         new List<object>{"name2", 124, -10}
        //    };
        //    table2.InsertRange(data2);

        //    table1.AddColumn("Name", DataValueType.StringDataValueTypeId);
        //    table1.AddColumn("Age", DataValueType.IntegerDataValueTypeId,
        //        new List<IValidator> { new Validator<int>(ComparsonType.Greater, 0) });
        //    table1.AddColumn("Income", DataValueType.IntegerDataValueTypeId);
        //    var data = new List<List<object>>()
        //    {
        //         new List<object>{"name1", 10, 1 },
        //         new List<object>{"name3", -12, 2},
        //         new List<object>{"name2", 124, -10}
        //    };
        //    table1.InsertRange(data);

        //    table3.AddColumn("Name", DataValueType.StringDataValueTypeId, new List<IValidator> { new Validator<string>(ComparsonType.Contains, "Test") });
        //    table3.AddColumn("ComplexInt", DataValueType.ComplexIntegerDataValueTypeId);
        //    table3.AddColumn("ComplexReal", DataValueType.ComplexRealDataValueTypeId);
        //    var data3 = new List<List<object>>()
        //    {
        //         new List<object>{"Test1", new ComplexInteger { RealValue=1,ImageValue=-1}, new ComplexReal { RealValue=1.5, ImageValue=2.04} },
        //         new List<object>{"Test2", new ComplexInteger { RealValue=2,ImageValue=-5}, new ComplexReal { RealValue=4.5, ImageValue=2.04}},
        //         new List<object>{"Test3", new ComplexInteger { RealValue = 1, ImageValue = -1 }, new ComplexReal { RealValue = 1.7, ImageValue = 2.04 } }
        //    };
        //    table3.InsertRange(data3);
        //    _storage = StorageFactory.GetStorage(dataBaseService.DataBase);
        //}


        //[Test]
        //public void InnerJoinTest()
        //{
        //    //Arrange
        //    Entity entity = _storage.DataBase.Entities.Find(x => x.Name == "Table1");
        //    Entity entity2 = _storage.DataBase.Entities.Find(x => x.Name == "Table2");
        //    EntityService entityService = new EntityService(entity, _storage);
        //    List<List<object>> expectedList = new List<List<object>>
        //    {
        //        new List<object>(){"name1", 10, 1, "name1", 10, 3},
        //        new List<object>(){ "name2", 124, -10, "name2", 124, -10 }
        //    };
        //    var json = JsonSerializer.Serialize(expectedList);
        //    var expected = JsonSerializer.Deserialize<List<List<object>>>(json);


        //    //Act
        //    var actual = entityService.InnerJoin(entity2, new Tuple<string, string>("Name", "Name2"), false);

        //    //Assert
        //    Assert.Multiple(() =>
        //    {
        //        for(int i = 0; i < actual.Count; i++)
        //        {
        //            for(int j=0; j < actual[i].Count; j++)
        //            {
        //                Assert.AreEqual(expected[i][j].ToString(), actual[i][j].ToString());
        //            }
        //        }
        //    });
        //}

        //[Test]
        //public void InnerJoinTest_SelfJoin()
        //{
        //    //Arrange
        //    Entity entity = _storage.DataBase.Entities.Find(x => x.Name == "Table3");
        //    EntityService entityService = new EntityService(entity, _storage);
        //    List<List<object>> expectedList = new List<List<object>>
        //    {
        //         new List<object>{"Test1", new ComplexInteger { RealValue=1,ImageValue=-1}, new ComplexReal { RealValue=1.5, ImageValue=2.04},"Test1", new ComplexInteger { RealValue=1,ImageValue=-1}, new ComplexReal { RealValue=1.5, ImageValue=2.04}  },
        //         new List<object>{"Test1", new ComplexInteger { RealValue=1,ImageValue=-1}, new ComplexReal { RealValue=1.5, ImageValue=2.04},"Test3", new ComplexInteger { RealValue = 1, ImageValue = -1 }, new ComplexReal { RealValue = 1.7, ImageValue = 2.04 }},
        //         new List<object>{"Test2", new ComplexInteger { RealValue=2,ImageValue=-5}, new ComplexReal { RealValue=4.5, ImageValue=2.04}, "Test2", new ComplexInteger { RealValue=2,ImageValue=-5}, new ComplexReal { RealValue=4.5, ImageValue=2.04} },
        //         new List<object>{ "Test3", new ComplexInteger { RealValue = 1, ImageValue = -1 }, new ComplexReal { RealValue = 1.7, ImageValue = 2.04 },"Test1", new ComplexInteger { RealValue=1,ImageValue=-1}, new ComplexReal { RealValue=1.5, ImageValue=2.04}},
        //        new List<object>{"Test3", new ComplexInteger { RealValue = 1, ImageValue = -1 }, new ComplexReal { RealValue = 1.7, ImageValue = 2.04 }, "Test3", new ComplexInteger { RealValue = 1, ImageValue = -1 }, new ComplexReal { RealValue = 1.7, ImageValue = 2.04 } }
        //    };
        //    var json = JsonSerializer.Serialize(expectedList);
        //    var expected = JsonSerializer.Deserialize<List<List<object>>>(json);


        //    //Act
        //    var actual = entityService.InnerJoin(entity, new Tuple<string, string>("ComplexInt", "ComplexInt"), false);

        //    //Assert
        //    Assert.Multiple(() =>
        //    {
        //        for (int i = 0; i < actual.Count; i++)
        //        {
        //            for (int j = 0; j < actual[i].Count; j++)
        //            {
        //                Assert.AreEqual(expected[i][j].ToString(), actual[i][j].ToString());
        //            }
        //        }
        //    });
        //}

        //[Test]
        //public void SelectTest()
        //{
        //    //Arrange
        //    Entity entity = _storage.DataBase.Entities.Find(x => x.Name == "Table2");
        //    EntityService entityService = new EntityService(entity, _storage);
        //    List<List<object>> expectedList = new List<List<object>>
        //    {
        //         new List<object>{"name1", 10, 3 },
        //         new List<object>{"name3", -12, 2},
        //         new List<object>{"name2", 124, -10}
        //    };
        //    var json = JsonSerializer.Serialize(expectedList);
        //    var expected = JsonSerializer.Deserialize<List<List<object>>>(json);


        //    //Act
        //    var actual = entityService.Select(false);

        //    //Assert
        //    Assert.Multiple(() =>
        //    {
        //        for (int i = 0; i < actual.Count; i++)
        //        {
        //            for (int j = 0; j < actual[i].Count; j++)
        //            {
        //                Assert.AreEqual(expected[i][j].ToString(), actual[i][j].ToString());
        //            }
        //        }
        //    });
        //}

        //[Test]
        //public void SelectWithConditionsTest()
        //{
        //    //Arrange
        //    Entity entity = _storage.DataBase.Entities.Find(x => x.Name == "Table2");
        //    EntityService entityService = new EntityService(entity, _storage);
        //    List<List<object>> expectedList = new List<List<object>>
        //    {
        //         //new List<object>{"name1", 10, 3 },
        //         //new List<object>{"name3", -12, 2},
        //         new List<object>{"name2", 124, -10}
        //    };
        //    var json = JsonSerializer.Serialize(expectedList);
        //    var expected = JsonSerializer.Deserialize<List<List<object>>>(json);


        //    //Act
        //    var actual = entityService.Select(new Dictionary<string, List<IValidator>> { ["Name2"] = new List<IValidator> { new Validator<string>(ComparsonType.EndsWith, "e2")}}, false);

            
        //    //Assert
        //    Assert.Multiple(() =>
        //    {
        //        for (int i = 0; i < actual.Count; i++)
        //        {
        //            for (int j = 0; j < actual[i].Count; j++)
        //            {
        //                Assert.AreEqual(expected[i][j].ToString(), actual[i][j].ToString());
        //            }
        //        }
        //    });
        //}

        //[Test]
        //public void CrosJoinTest()
        //{
        //    //Arrange
        //    Entity entity = _storage.DataBase.Entities.Find(x => x.Name == "Table1");
        //    Entity entity2 = _storage.DataBase.Entities.Find(x => x.Name == "Table2");
        //    EntityService entityService = new EntityService(entity, _storage);
        //    List<List<object>> expectedList = new List<List<object>>
        //    {
        //        new List<object>(){"name1", 10, 1, "name1", 10, 3},
        //        new List<object>(){ "name1", 10, 1, "name3", -12, 2 },
        //        new List<object>(){ "name1", 10, 1, "name2", 124, -10 },
        //        new List<object>(){ "name2", 124, -10, "name1", 10, 3 },
        //        new List<object>(){ "name2", 124, -10, "name3", -12, 2 },
        //        new List<object>(){ "name2", 124, -10, "name2", 124, -10 },
        //    };
        //    var json = JsonSerializer.Serialize(expectedList);
        //    var expected = JsonSerializer.Deserialize<List<List<object>>>(json);


        //    //Act
        //    var actual = entityService.CrossJoin(entity2, false);

        //    //Assert
        //    Assert.Multiple(() =>
        //    {
        //        for (int i = 0; i < actual.Count; i++)
        //        {
        //            for (int j = 0; j < actual[i].Count; j++)
        //            {
        //                Assert.AreEqual(expected[i][j].ToString(), actual[i][j].ToString());
        //            }
        //        }
        //    });
        //}

        //[Test]
        //public void UpdateTest()
        //{

        //    //Arrange
        //    Entity entity = _storage.DataBase.Entities.Find(x => x.Name == "Table2");
        //    EntityService entityService = new EntityService(entity, _storage);
        //    var fisrtFromSelect = entityService.Select().First();
            
            
        //    var expectedList = new List<List<object>>() {
        //        new List<object> { new Guid(fisrtFromSelect[0].ToString()), "UpdatedName", 228, 228 }
        //    };
        //    fisrtFromSelect = expectedList[0];
        //    var json = JsonSerializer.Serialize(expectedList);
        //    var expected = JsonSerializer.Deserialize<List<List<object>>>(json);


        //    //Act
        //    entityService.Update(new Dictionary<string, List<IValidator>> { ["Id"] = new List<IValidator> { new Validator<Guid>(ComparsonType.Equal, (Guid)fisrtFromSelect[0]) } }, fisrtFromSelect);
        //    var actual = entityService.Select(new Dictionary<string, List<IValidator>> { ["Id"] = new List<IValidator> { new Validator<Guid>(ComparsonType.Equal, (Guid)fisrtFromSelect[0]) } });


        //    //Assert
        //    Assert.Multiple(() =>
        //    {
        //        for (int i = 0; i < actual.Count; i++)
        //        {
        //            for (int j = 0; j < actual[i].Count; j++)
        //            {
        //                Assert.AreEqual(expected[i][j].ToString(), actual[i][j].ToString());
        //            }
        //        }
        //    });
        //}

        //[Test]
        //public void BulkUpdateTest()
        //{

        //    //Arrange
        //    Entity entity = _storage.DataBase.Entities.Find(x => x.Name == "Table2");
        //    EntityService entityService = new EntityService(entity, _storage);
        //    var fisrtFromSelect = entityService.Select();
        //    var updatedRows = new List<List<object>>() {
        //        new List<object> { new Guid(fisrtFromSelect[0][0].ToString()), "UpdatedName", 228, 228 },
        //        new List<object> { new Guid(fisrtFromSelect[1][0].ToString()), "SecondUpdatedName", 1488, 1488 },
        //        new List<object> { new Guid(fisrtFromSelect[2][0].ToString()), "ThirdUpdateNAme",4444,4444 }
        //    };
        //    var expectedList = updatedRows;
        //    var json = JsonSerializer.Serialize(expectedList);
        //    var expected = JsonSerializer.Deserialize<List<List<object>>>(json);


        //    //Act
        //    entityService.Update(updatedRows);
        //    var actual = entityService.Select();


        //    //Assert
        //    Assert.Multiple(() =>
        //    {
        //        for (int i = 0; i < actual.Count; i++)
        //        {
        //            for (int j = 0; j < actual[i].Count; j++)
        //            {
        //                Assert.AreEqual(expected[i][j].ToString(), actual[i][j].ToString());
        //            }
        //        }
        //    });
        //}
    }
}