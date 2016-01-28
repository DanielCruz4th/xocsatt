using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOcsatt.Entities
{
    public class BackupMachineContext : DbContext
    {
        public BackupMachineContext()
            : base()
        {

        }

        public BackupMachineContext(string connectionStringName)
            : base(connectionStringName)
        {

        }

        public DbSet<BackupMachine> BackupMachines { get; set; }
    }
}
