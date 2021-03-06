﻿using DB_Engine.Implementations.Servises;
using DB_Engine.Interfaces;
using DB_Engine.Models;

namespace DB_Engine.Factories
{
    public class EntityServiceFactory
    {
        public static IEntityService GetEntityService(Entity entity, IStorage storage)
        {
            return new EntityService(entity, storage);
        }
    }
}
