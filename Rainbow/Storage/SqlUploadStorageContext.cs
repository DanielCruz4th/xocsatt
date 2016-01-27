using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainbow.Web.Storage
{
    public class SqlUploadStorageContext : DbContext
    {
        public SqlUploadStorageContext()
            : base()
        {

        }

        public SqlUploadStorageContext(string connectionStringName)
            : base(connectionStringName)
        {

        }

        public DbSet<UploadedFile> UploadedFiles { get; set; }
    }
}
