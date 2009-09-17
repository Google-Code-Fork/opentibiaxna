using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data;
using System.Collections;
using System.Data.Objects;

namespace OpenTibiaXna.Server.Entities
{
    public class GenericEntities
    {
        // TODO: private static readonly SQLiteEntities fSQLiteContext = new SQLiteEntities();
        // TODO: private static readonly MySqlEntities fMySqlContext = new MySqlEntities();
        private static readonly MSSQLEntities fMSSQLContext = new MSSQLEntities();

        public static ObjectContext CurrentContext
        { 
            get 
            {
                // TODO:
                //switch (DataBaseType)
                //{
                //    case SQLite:
                //      return fSQLiteContext;
                //      break;
                //    case MySql:
                //      return fMySqlContext;
                //      break;
                //    case MSSQL:
                //      return fMySqlContext;
                //      break;
                //}

                return fMSSQLContext;
            }
        }

        public static EntityObject GetById(EntityKey entityKey)
        {
            object resultObject = null;

            CurrentContext.TryGetObjectByKey(entityKey, out resultObject);

            return (EntityObject)resultObject;
        }

        public static void SaveOrUpdate(EntityObject entity)
        {
            if (entity.EntityKey.IsTemporary)
                Save(entity.GetType().Name, entity);
            else
                Update(entity);
        }

        public static void Save(string entitySetName, object entity)
        {
            CurrentContext.AddObject(entitySetName, entity);
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
