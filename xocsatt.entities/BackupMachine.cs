using Rainbow.Web;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOcsatt.Entities
{
    public class BackupMachine : BusinessBase<BackupMachine, Guid>, IEquatable<BackupMachine>
    {
        public BackupMachine()
            : base(Guid.NewGuid())
        {

        }

        private string _ipAddress;

        public string IPAddress
        {
            get { return _ipAddress; }
            set
            {
                var changed = !object.Equals(_ipAddress, value);
                if (changed)
                    this.OnPropertyChanging("IPAddress");
                this._ipAddress = value;
                if (changed)
                    MarkChanged("IPAddress");
            }
        }

        private string _sourcePath;

        public string SourcePath
        {
            get { return _sourcePath; }
            set
            {
                var changed = !object.Equals(_sourcePath, value);
                if (changed)
                    this.OnPropertyChanging("SourcePath");
                this._sourcePath = value;
                if (changed)
                    MarkChanged("SourcePath");
            }
        }

        private string _sourceUserName;

        public string SourceUserName
        {
            get { return _sourceUserName; }
            set
            {
                var changed = !object.Equals(_sourceUserName, value);
                if (changed)
                    this.OnPropertyChanging("SourceUserName");
                this._sourceUserName = value;
                if (changed)
                    MarkChanged("SourceUserName");
            }
        }

        private string _sourcePassword;

        public string SourcePassword
        {
            get { return _sourcePassword; }
            set
            {
                var changed = !object.Equals(_sourcePassword, value);
                if (changed)
                    this.OnPropertyChanging("SourcePassword");
                this._sourcePassword = value;
                if (changed)
                    MarkChanged("SourcePassword");
            }
        }

        private string _destinationPath;

        public string DestinationPath
        {
            get { return _destinationPath; }
            set
            {
                var changed = !object.Equals(_destinationPath, value);
                if (changed)
                    this.OnPropertyChanging("DestinationPath");
                this._destinationPath = value;
                if (changed)
                    MarkChanged("DestinationPath");
            }
        }

        private string _destinationUserName;

        public string DestinationUserName
        {
            get { return _destinationUserName; }
            set
            {
                var changed = !object.Equals(_destinationUserName, value);
                if (changed)
                    this.OnPropertyChanging("DestinationUserName");
                this._destinationUserName = value;
                if (changed)
                    MarkChanged("DestinationUserName");
            }
        }

        private string _destinationPassword;

        public string DestinationPassword
        {
            get { return _destinationPassword; }
            set
            {
                var changed = !object.Equals(_destinationPassword, value);
                if (changed)
                    this.OnPropertyChanging("DestinationPassword");
                this._destinationPassword = value;
                if (changed)
                    MarkChanged("DestinationPassword");
            }
        }

        private bool _enabled;

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                var changed = !object.Equals(_enabled, value);
                if (changed)
                    this.OnPropertyChanging("Enabled");
                this._enabled = value;
                if (changed)
                    MarkChanged("Enabled");
            }
        }

        protected override void ValidationRules()
        {
        }

        protected override BackupMachine DataSelect(Guid id)
        {
            return BackupMachine.GetBackupMachines(id, null, null).FirstOrDefault();
        }

        protected override void DataUpdate()
        {
            BackupMachine.UpdateBackupMachine(this);
        }

        protected override void DataInsert()
        {
            BackupMachine.InsertBackupMachine(this);
        }

        protected override void DataDelete()
        {
            throw new NotImplementedException();
        }

        public bool Equals(BackupMachine other)
        {
            throw new NotImplementedException();
        }

        static public IEnumerable<BackupMachine> GetAll()
        {
            return GetBackupMachines(null, null, null);
        }

        internal static void InsertBackupMachine(BackupMachine entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            using (var db = new BackupMachineContext("BackupMachineContextDb"))
            {
                db.BackupMachines.Add(entity);

                db.SaveChanges();
            }
        }

        internal static void DeleteBackupMachine(BackupMachine entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            using (var db = new BackupMachineContext("BackupMachineContextDb"))
            {
                var item = db.BackupMachines.Find(entity.ID);

                if (item == null)
                    return;

                db.BackupMachines.Remove(item);

                db.SaveChanges();
            }
        }

        internal static void UpdateBackupMachine(BackupMachine entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            using (var db = new BackupMachineContext("BackupMachineContextDb"))
            {
                db.BackupMachines.Attach(entity);

                var entry = db.Entry(entity);
                entry.State = EntityState.Modified;

                db.SaveChanges();
            }
        }

        static public IEnumerable<BackupMachine> GetBackupMachines(Guid? id, string ipAddress, bool? enabled)
        {
            List<BackupMachine> entities = new List<BackupMachine>();

            using (var db = new BackupMachineContext("BackupMachineContextDb"))
            {
                var query = from m in db.BackupMachines
                            where !id.HasValue || m.ID == id.Value
                            where string.IsNullOrEmpty(ipAddress) || m.IPAddress == ipAddress
                            where !enabled.HasValue || m.Enabled == enabled.Value
                            select m;
                
                entities.AddRange(query);
            }

            return entities;
        }
    }
}
