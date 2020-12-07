using DB_Engine.Interfaces;
using DB_Engine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Engine.Factories
{
    public interface IEntityServiceFactory
    {
        IEntityService GetEntityService(Entity entity, IStorage storage);
    }
}
