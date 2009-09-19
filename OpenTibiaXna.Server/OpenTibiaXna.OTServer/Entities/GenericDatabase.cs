using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data;
using System.Collections;

namespace OpenTibiaXna.OTServer.Entities
{
    public class GenericDatabase
    {
        // CurrentContext is set from: 
        // OpenTibiaXna.OTServer.Engines.DatabaseEngine.SetDatabaseType() method.
        public static ObjectContext CurrentContext
        {
            get 
            {
                if (fCurrentContext == null)
                    throw new InvalidOperationException("Database context is null, you must call the OpenTibiaXna.OTServer.Engines.DatabaseEngine.SetDatabaseType() method first.");

                return fCurrentContext;
            }
            set { fCurrentContext = value; }
        }
        private static ObjectContext fCurrentContext;

        public static EntityObject GetById(EntityKey entityKey)
        {
            object resultObject = null;

            CurrentContext.TryGetObjectByKey(entityKey, out resultObject);

            return (EntityObject)resultObject;
        }

        public static void SaveOrUpdate(EntityObject entity)
        {
            if (entity.EntityState == EntityState.Modified)
                Update(entity);
            else
                Save(entity);
        }

        public static void Save(object entity)
        {
            CurrentContext.AddObject(entity.GetType().Name, entity);
            CurrentContext.SaveChanges();
        }

        public static void Update(EntityObject entity)
        {
            var original = GetById(entity.EntityKey);

            if (original != null)
            {
                CurrentContext.ApplyPropertyChanges(original.EntityKey.EntitySetName, entity);
                CurrentContext.SaveChanges();
            }
        }

        public static void Delete(EntityObject entity)
        {
            CurrentContext.DeleteObject(entity);
        }
    }
}
