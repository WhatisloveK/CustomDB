using GrpcClient;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DB_EngineTest.ServisesTests
{
    [TestFixture]
    public class TableServiceTest
    {

        private TableService _tableService;
        private Mock<EntityService.EntityServiceClient> _client;


        [SetUp]
        public void Setup()
        {
            _client = new Mock<EntityService.EntityServiceClient>();
            _tableService = new TableService(_client.Object);
        }

        [Test]
        public  void AddColumnTest()
        {
            var columnRequest = new AddColumnRequest();
            string columnName = "TestColumnName1_Int", 
                   dataValueTypeID = DataValueType.IntegerDataValueTypeId.ToString(),
                   dbName = "TestDb1",
                   tableName = "TestTableName1";

            string actualTableName = string.Empty, actualDbName = string.Empty,
                actualColumnName = string.Empty, actualDataValueTypeId = string.Empty;

            _client.Setup(x => x.AddColumnAsync(It.IsAny<AddColumnRequest>(), It.IsAny<Grpc.Core.CallOptions>()))
                .Callback<AddColumnRequest, Grpc.Core.CallOptions>
                    ((addColumnRequest, options) =>
                    {
                        actualTableName = addColumnRequest.TableName;
                        actualColumnName = addColumnRequest.ColumnName;
                        actualDbName = addColumnRequest.DbName;
                        actualDataValueTypeId = addColumnRequest.DataValueTypeId;
                    });

            //Act
           _tableService.AddColumn(dbName, tableName, columnName, dataValueTypeID, null).Wait();

            //Assert
            Assert.AreEqual(columnName, actualColumnName);
            Assert.AreEqual(dbName, actualDbName);
            Assert.AreEqual(dataValueTypeID, actualDataValueTypeId);
            Assert.AreEqual(tableName, actualTableName);
        }


    }
}
