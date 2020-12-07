using DB_Engine.Implementations.Servises;
using DB_Engine.Interfaces;
using DB_Engine.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DB_EngineTest.ServisesTests
{
    [TestFixture]
    class EntityServiceTest
    {

        private IEntityService _entityService;
        private Mock<IStorage> _storage;
        private Entity _entity;

        [SetUp]
        public void Setup()
        {
            _storage = new Mock<IStorage>();
            _entity = new Entity
            {
                Name = TestData.Tables.TableData1.Name,
                Schema = new EntitySchema
                {
                    Columns = TestData.Tables.TableData1.EntityColumns
                }
            };
            _entityService = new EntityService(_entity, _storage.Object);
        }

        [Test]
        public void AddColumnTest()
        {
            //Arrange

            var column = TestData.Tables.TableData1.EntityColumns.First();

            EntityColumn actualColumn = new EntityColumn();
            string  actualTableName = string.Empty;

            _storage.Setup(x => x.AddColumn(It.IsAny<Entity>()))
                .Callback<Entity>((entity) =>
                {
                    actualTableName = entity.Name;
                    actualColumn = entity.Schema.Columns.Last();
                });

            //Act
            _entityService.AddColumn(column);

            //Assert
            Assert.AreEqual(column, actualColumn);

            Assert.AreEqual(_entity.Name, actualTableName);
        }

        [Test]
        public void AddColumnTest2()
        {
            //Arrange

            var expectedColumn = TestData.Tables.TableData1.EntityColumns.First();

            EntityColumn actualColumn = new EntityColumn();
            string actualTableName = string.Empty;

            _storage.Setup(x => x.AddColumn(It.IsAny<Entity>()))
                .Callback<Entity>((entity) =>
                {
                    actualTableName = entity.Name;
                    actualColumn = entity.Schema.Columns.Last();
                });

            //Act
            _entityService.AddColumn(TestData.Tables.TableData1.EntityColumns.First().Name,
                TestData.Tables.TableData1.EntityColumns.First().DataValueType,
                TestData.Tables.TableData1.EntityColumns.First().Validators);

            //Assert
            Assert.AreEqual(expectedColumn, actualColumn);

            Assert.AreEqual(_entity.Name, actualTableName);
        }


        [Test]
        public void DropColumnTest()
        {
            //Arrange
            var fieldName = TestData.Tables.TableData1.EntityColumns.First().Name;

            var actualEntity = new Entity();
            _storage.Setup(x => x.DropColumn(It.IsAny<Entity>(), It.IsAny<int>()))
                .Callback<Entity, int>((entity, index) =>
                {
                    actualEntity = entity;
                });

            //Act
            _entityService.DropColumn(fieldName);

            //Assert
            Assert.IsFalse(actualEntity.Schema.Columns.Contains(TestData.Tables.TableData1.EntityColumns.First()));
            Assert.AreEqual(_entity.Name, actualEntity.Name);
        }

        [Test]
        public void DeleteRows()
        {
            //Arrange
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guidList = new List<Guid> { guid1, guid2 };

            var actualIds = new List<Guid>();
            string actualTableName = string.Empty;
            _storage.Setup(x => x.DeleteRange(It.IsAny<Entity>(), It.IsAny<List<Guid>>()))
                .Callback<Entity, List<Guid>>((entity, ids) =>
                {
                    
                    actualTableName = entity.Name;
                    actualIds = ids;
                });

            //Act
            _entityService.DeleteRange(guidList);

            //Assert
            Assert.AreEqual(_entity.Name, actualTableName);

            Assert.AreEqual(guidList.Count, actualIds.Count);
            Assert.Multiple(() =>
            {
                for (int i = 0; i < actualIds.Count; i++)
                {
                    Assert.AreEqual(guidList[i], actualIds[i]);
                }
            });
        }

        [Test]
        public void InsertDataTest()
        {
            //Arrange
            var row = TestData.Tables.TableData1.Data.First();
            List<object> actualRow = new List<object>();
            string actualDbName = string.Empty, actualTableName = string.Empty;

            _storage.Setup(x => x.Insert(It.IsAny<Entity>(), It.IsAny<List<List<object>>>()))
                .Callback<Entity, List<List<object>>>((entity, rows) =>
                {
                    actualTableName = entity.Name;
                    actualRow = rows.First();
                });

            //Act
            _entityService.Insert(row);

            //Assert
            Assert.AreEqual(_entity.Name, actualTableName);

            Assert.AreEqual(row.Count, actualRow.Count);

            Assert.Multiple(() =>
            {
                for (int i = 0; i < actualRow.Count; i++)
                {
                    Assert.AreEqual(row[i], actualRow[i]);
                }
            });
        }

        [Test]
        public void SelectTest()
        {
            //Arrange
            var excpectedRows = TestData.Tables.TableData1.Data;

            _storage.Setup(x => x.Select(It.IsAny<Entity>(), It.IsAny<bool>())).Returns(excpectedRows);

            //Act
            var actualRows = _entityService.Select();

            //Assert
            Assert.AreEqual(excpectedRows.Count, actualRows.Count);
            Assert.Multiple(() =>
            {
                for (int i = 0; i < actualRows.Count; i++)
                {
                    Assert.AreEqual(excpectedRows[i].Count, actualRows[i].Count);
                    for (int j = 0; j < excpectedRows[i].Count; j++)
                    {
                        Assert.AreEqual(excpectedRows[i][j], actualRows[i][j]);
                    }
                }
            });
        }

        //[Test]
        //public void UnionTest()
        //{
        //    //Arrange
        //    var excpectedRows = Tables.TableData1.Data;

        //    _storage.Setup(x => x.Union(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Models.Table[]>())).Returns(excpectedRows);

        //    //Act
        //    var actualRows = _entityService.Union();

        //    //Assert
        //    Assert.AreEqual(excpectedRows.Count, actualRows.Count);
        //    Assert.Multiple(() =>
        //    {
        //        for (int i = 0; i < actualRows.Count; i++)
        //        {
        //            Assert.AreEqual(excpectedRows[i].Count, actualRows[i].Count);
        //            for (int j = 0; j < excpectedRows[i].Count; j++)
        //            {
        //                Assert.AreEqual(excpectedRows[i][j], actualRows[i][j]);
        //            }
        //        }
        //    });
        //}

    }
}
