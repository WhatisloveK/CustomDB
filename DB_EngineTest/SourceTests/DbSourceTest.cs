using DB_Engine.Factories;
using DB_Engine.Implementations.DbWriters;
using DB_Engine.Interfaces;
using DB_Engine.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using static DB_EngineTest.TestData;


namespace DB_EngineTest.SourceTests
{
    [TestFixture]
    public abstract class DbSourceTest
    {
        protected ISource _source;
        protected Mock<IDbProvider> _dbProviderMock;

        [SetUp]
        public void Initialize()
        {
            Setup();
        }

        protected abstract void Setup();

        [Test]
        public void WriteDataTest_ValidData()
        {
            //Arrange
            var data = TestData.Tables.TableData1.Data;
            var excpectedData = data.Select(x => JsonSerializer.Serialize(x)).ToList();

            List<string> actualData = new List<string>();
            _dbProviderMock.Setup(x => x.InsertData(It.IsAny<List<string>>())).Callback<List<string>>(res => actualData = res);

            //Act
            _source.WriteData(data);

            //Assert
            _dbProviderMock.Verify(m => m.ClearTable(), Times.Once());
            _dbProviderMock.Verify(m => m.InsertData(It.IsAny<List<string>>()), Times.Once());

            Assert.AreEqual(excpectedData.Count, actualData.Count);
            Assert.Multiple(() =>
            {
                for (int i = 0; i < actualData.Count; i++)
                {
                    Assert.AreEqual(excpectedData[i], actualData[i]);
                }
            });
        }

        [Test]
        public void WriteDataTest_InvalidData()
        {
            //Arrange
            List<List<object>> data = null;

            List<string> actualData = new List<string>();
            _dbProviderMock.Setup(x => x.InsertData(It.IsAny<List<string>>())).Callback<List<string>>(res => actualData = res);

            //Act
            _source.WriteData(data);

            //Assert
            _dbProviderMock.Verify(m => m.ClearTable(), Times.Never());
            _dbProviderMock.Verify(m => m.InsertData(It.IsAny<List<string>>()), Times.Never());

            Assert.AreEqual(0, actualData.Count());
        }

        [Test]
        public void GetDataTest()
        {
            //Arrange
            var excpected = TestData.Tables.TableData1.Data;
            var stringListData = excpected.Select(x => JsonSerializer.Serialize(x)).ToList();
            excpected = stringListData.Select(x => JsonSerializer.Deserialize<List<object>>(x)).ToList();

            _dbProviderMock.Setup(x => x.GetData()).Returns(stringListData);

            //Act
            var actual = _source.GetData();

            //Assert
            _dbProviderMock.Verify(m => m.GetData(), Times.Once());

            Assert.AreEqual(excpected.Count, actual.Count);
            Assert.Multiple(() =>
            {
                for (int i = 0; i < actual.Count; i++)
                {
                    Assert.AreEqual(excpected[i].Count, actual[i].Count);
                    for (int j = 0; j < actual[i].Count; j++)
                    {
                        Assert.AreEqual(excpected[i][j].ToString(), actual[i][j].ToString());
                    }
                }
            });
        }

    }
}
