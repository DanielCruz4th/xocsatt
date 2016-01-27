using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainbow.Web.Storage
{
    public class SqlUploadStorageProvider : UploadStorageProvider
    {
        private string _connectionStringName;

        public SqlUploadStorageProvider() { }

        public string ConnectionStringName
        { get { return _connectionStringName; } }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);

            if (string.IsNullOrEmpty(config["connectionStringName"]))
                throw new ProviderException("connectionStringName config attribute is required.");

            this._connectionStringName = config["connectionStringName"];

            config.Remove("connectionStringName");

            if (config.Count > 0)
                throw new ProviderException(string.Format("Unknown config attribute '{0}'", config.GetKey(0)));
        }

        public override UploadedFile SelectFile(Guid id)
        {
            int totalCount = 0;
            return this.GetFiles(id, null, null, null, null, null, null, null, false, int.MaxValue, 0, out totalCount).FirstOrDefault();
        }

        public override UploadedFile SelectFile(Guid id, bool includeData)
        {
            int totalCount = 0;
            return this.GetFiles(id, null, null, null, null, null, null, null, includeData, int.MaxValue, 0, out totalCount).FirstOrDefault();
        }

        public override void UpdateFile(UploadedFile entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            using (var db = new SqlUploadStorageContext(this.ConnectionStringName))
            {
                db.UploadedFiles.Attach(entity);

                var entry = db.Entry(entity);
                entry.State = EntityState.Modified;

                db.SaveChanges();
            }
        }

        public override void InsertFile(UploadedFile entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            entity.ApplicationName = this.ApplicationName;

            using (var db = new SqlUploadStorageContext(this.ConnectionStringName))
            {
                db.UploadedFiles.Add(entity);

                db.SaveChanges();
            }
        }

        public override void DeleteFile(UploadedFile entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            using (var db = new SqlUploadStorageContext(this.ConnectionStringName))
            {
                var item = db.UploadedFiles.Find(entity.ID);

                if (item == null)
                    return;

                db.UploadedFiles.Remove(item);

                db.SaveChanges();
            }
        }

        public override List<UploadedFile> GetFiles(Guid parentId)
        {
            int totalCount = 0;

            return this.GetFiles(null, parentId, null, null, null, null, null, null, false, int.MaxValue, 0, out totalCount);
        }

        public override List<UploadedFile> GetFiles(Guid? id, Guid? parentId, string fileName, 
            DateTime? initialDateCreated, DateTime? finalDateCreated,
            DateTime? initialLastUpdated, DateTime? finalLastUpdated,
            string contentType, bool includeData, int pageSize, int pageIndex, out int totalCount)
        {
            List<UploadedFile> entities = new List<UploadedFile>();

            using (var db = new SqlUploadStorageContext(this.ConnectionStringName))
            {
                var query = from file in db.UploadedFiles
                            where !id.HasValue || file.ID == id.Value
                            where !parentId.HasValue || file.ParentID == parentId.Value
                            where string.IsNullOrEmpty(fileName) || (!string.IsNullOrEmpty(file.FileName) && file.FileName.ToLower().Contains(fileName.ToLower()))
                            where !initialDateCreated.HasValue || file.DateCreated >= initialDateCreated.Value
                            where !finalDateCreated.HasValue || file.DateCreated <= initialDateCreated.Value
                            where !initialLastUpdated.HasValue || (file.LastUpdated.HasValue && file.LastUpdated.Value >= initialLastUpdated.Value)
                            where !finalLastUpdated.HasValue || (file.LastUpdated.HasValue && file.LastUpdated.Value >= finalLastUpdated.Value)
                            where string.IsNullOrEmpty(contentType) || (!string.IsNullOrEmpty(file.ContentType) && file.ContentType.ToLower().Contains(contentType.ToLower()))
                            select file;

                entities.AddRange(query);
            }

            totalCount = entities.Count;

            return entities.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }
    }
}
