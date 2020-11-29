using DB_Engine.Factories;
using DB_Engine.Implementations;
using DB_Engine.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB_EngineTest.SourceTests
{
    public class MongoDbSourceTest : DbSourceTest
    {
        protected override void Setup()
        {

            _dbProviderMock = new Mock<IDbProvider>();

            var dbProviderFactory = new Mock<IDbProviderFactory>();

            dbProviderFactory.Setup(x => x.GetMongoClient(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(_dbProviderMock.Object);

            _source = new MongoDbSource(dbProviderFactory.Object);
            _source.Url = "server|db|table";
        }
    }
}
