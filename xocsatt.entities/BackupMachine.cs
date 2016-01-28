using Rainbow.Web;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        protected override BackupMachine DataSelect(Guid id)
        {
            throw new NotImplementedException();
        }

        protected override void DataUpdate()
        {
            throw new NotImplementedException();
        }

        protected override void DataInsert()
        {
            throw new NotImplementedException();
        }

        protected override void DataDelete()
        {
            throw new NotImplementedException();
        }

        public bool Equals(BackupMachine other)
        {
            throw new NotImplementedException();
        }
    }
}
