using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data;
using System.Collections;
using System.Data.EntityClient;

namespace OpenTibiaXna.OTServer.Entities
{
    public class GenericDatabase
    {
        public static ObjectContext CurrentContext
        {
            get 
            {
                if (fCurrentContext == null)
                    fCurrentContext = new OTXEntities();

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
            switch (entity.EntityState)
            {
                case EntityState.Added:
                    Save(entity);
                    break;
                case EntityState.Deleted:
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Modified:
                    Update(entity);
                    break;
                case EntityState.Unchanged:
                    break;
                default:
                    break;
            }
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
