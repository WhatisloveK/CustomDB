using DB_Engine.Factories;
using DB_Engine.Implementations.DbWriters;
using DB_Engine.Interfaces;
using DB_Engine.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using static DB_EngineTest.TestData;

namespace DB_EngineTest.DbWriterTests
{
    [TestFixture]
    public class SqlServerDbWriterTest
    {
        private IDbWriter _dbWriter;
        private Mock<IDbProvider> _dbProviderMock;

        [SetUp]
        public void Initialize()
        {
            var dbClientFactoryMock = new Mock<IDbProviderFactory>();
            _dbProviderMock = new Mock<IDbProvider>();

            dbClientFactoryMock.Setup(x => x.GetSqlServerClient(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(_dbProviderMock.Object);

            _dbWriter = new SqlServerDbWriter(dbClientFactoryMock.Object);
        }

        [Test]
        public void DeleteDbTest()
        {
            //Arrange
            var dataBase = new DataBase();

            //Act
            _dbWriter.DeleteDb(dataBase);

            //Assert
            _dbProviderMock.Verify(m => m.DeleteDatabase(), Times.Once());
        }

        [Test]
        public void GetDbTest()
        {
            var dataBase = new DataBase
            {
                Name = DataBases.DataBase1.Name,
                Info = new DatabaseInfo
                {
                    RootPath = DataBases.DataBase1.RootPath
                },
                Entities = new List<Entity>
                {
                    new Entity
                    {
                        Name = Tables.TableData1.Name,
                        Schema = new EntitySchema
                        {
                            Columns = Tables.TableData1.EntityColumns
                        }
                    }
                }
            };

            var dbString = JsonSerializer.Serialize(dataBase);
            _dbProviderMock.Setup(x => x.GetDb()).Returns(dbString);

            //Act
            var actual = _dbWriter.GetDb(DataBases.DataBase1.RootPath);

            //Assert
            Assert.AreEqual(dataBase, actual);
        }

        [Test]
        public void GetDbsNamesTest()
        {
            //Arrange
            var expected = new List<string> { DataBases.DataBase1.Name, DataBases.DataBase2.Name };
            _dbProviderMock.Setup(x => x.GetDbsNames()).Returns(expected);

            //Act
            var actual = _dbWriter.GetDbsNames(CommonData.Server);

            //Assert
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.Multiple(() =>
            {
                for (int i = 0; i < actual.Count; i++)
                {
                    Assert.AreEqual(expected[i], actual[i]);
                }
            });
        }

        [Test]
        public void UpdateDbTest()
        {
            //Arrange
            var dataBase = new DataBase
            {
                Name = DataBases.DataBase1.Name,
                Info = new DatabaseInfo
                {
                    RootPath = DataBases.DataBase1.RootPath
                },
                Entities = new List<Entity>
                {
                    new Entity
                    {
                        Name = Tables.TableData1.Name,
                        Schema = new EntitySchema
                        {
                            Columns = Tables.TableData1.EntityColumns
                        }
                    }
                }
            };
            var stringDb = JsonSerializer.Serialize(dataBase);
            string actual = string.Empty;
            _dbProviderMock.Setup(x => x.UpdateOrCreateDb(stringDb)).Callback<string>(res => actual = res);

            //Act
            _dbWriter.UpdateDb(dataBase);

            //Assert
            Assert.AreEqual(stringDb, actual);
        }
    }
}
