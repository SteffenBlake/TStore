using System;
using System.Collections.Generic;
using System.Linq;
using TStore.Interfaces;

namespace TStore.Implementations
{
    public class TStore : ITStore
    {
        private readonly Dictionary<Type, Type> Entities = new Dictionary<Type, Type>();

        public virtual void Register(Type key, Type value)
        {
            Entities[key] = value;
        }

        public virtual void UnRegister(Type type)
        {
            Entities.Remove(type);
        }

        public virtual bool IsRegistered(Type type)
        {
            return Entities.ContainsKey(type);
        }

        //Our Cache of pre-compiled Entities
        private readonly Dictionary<Type, object> CompiledEntities = new Dictionary<Type, object>();
        public virtual object Fetch(Type type)
        {
            // Short circuit out if we've already cached this type
            if (CompiledEntities.ContainsKey(type))
                return CompiledEntities[type];

            if (!Entities.ContainsKey(type))
                throw new KeyNotFoundException($"No Registered key for {type.Name} found.");

            // Actual type we mapped to that we will now construct
            var targetType = Entities[type];

            // Type[][] Matrix
            var constructors = targetType
                .GetConstructors()
                .Select(c => 
                    c.GetParameters()
                    .Select(p => 
                        p.ParameterType
                    ).ToArray()
                ).ToArray();

            // Our best match Type[] Array
            var bestConstructor = constructors
                // Grab constructors we have every param known in our Entities
                .Where(c => c.All(p => Entities.ContainsKey(p)))
                // Grab the first one with the biggest constructor
                .OrderByDescending(c => c.Length).FirstOrDefault();

            if (bestConstructor == null)
                throw new KeyNotFoundException($"No valid constructor found for {type.Name}");

            // Fetch all the entities for those params Recursively
            // Compiling Type[] into object[]
            var constructorParams = bestConstructor.Select(Fetch).ToArray();

            // Use System to build an instance of our type(s) from scratch
            var entity = Activator.CreateInstance(targetType, constructorParams);

            // Cache this for later ease of access
            CompiledEntities[type] = entity;

            return entity;
        }
    }
}