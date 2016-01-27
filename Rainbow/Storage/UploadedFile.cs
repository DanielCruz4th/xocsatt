using Rainbow.Web.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainbow.Web.Storage
{
    [Serializable]
    public class UploadedFile : BusinessBase<UploadedFile, Guid>, IUploadedFile
    {
        private Guid? _parentId;
        private string _filename;
        private string _description;
        private byte[] _data;
        private string _applicationName;
        private string _contentType;
        private int _contentLength;

        private Stream _inputstream = Stream.Null;

        public UploadedFile()
            : this(Guid.NewGuid())
        {
        }

        public UploadedFile(Guid id)
            : base(id)
        {
        }

        #region IUploadedFile Members

        public string ApplicationName
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _applicationName; }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                var changed = !object.Equals(_applicationName, value);
                if (changed)
                    this.OnPropertyChanging("ApplicationName");
                this._applicationName = value;
                if (changed)
                    MarkChanged("ApplicationName");
            }
        }

        public Guid? ParentID
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return this._parentId; }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                var changed = !object.Equals(_parentId, value);
                if (changed)
                    this.OnPropertyChanging("ParentID");
                this._parentId = value;
                if (changed)
                    MarkChanged("ParentID");
            }
        }

        public string FileName
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return this._filename; }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                var changed = !object.Equals(_filename, value);
                if (changed)
                    this.OnPropertyChanging("FileName");
                this._filename = value;
                if (changed)
                    MarkChanged("FileName");
            }
        }

        public string Description
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return this._description; }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                var changed = !object.Equals(_description, value);
                if (changed)
                    this.OnPropertyChanging("Description");
                this._description = value;
                if (changed)
                    MarkChanged("Description");
            }
        }

        public byte[] Data
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _data; }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                var changed = !object.Equals(_data, value);
                if (changed)
                    this.OnPropertyChanging("Data");
                this._data = value;
                if (changed)
                    MarkChanged("Data");
            }
        }

        public string ContentType
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _contentType; }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                var changed = !object.Equals(_contentType, value);
                if (changed)
                    this.OnPropertyChanging("ContentType");
                this._contentType = value;
                if (changed)
                    MarkChanged("ContentType");
            }
        }

        public int ContentLength
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return this._contentLength; }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                var changed = !object.Equals(_contentLength, value);
                if (changed)
                    this.OnPropertyChanging("ContentLength");
                this._contentLength = value;
                if (changed)
                    MarkChanged("ContentLength");
            }
        }

        #endregion

        #region UploadedFile Members

        /// <summary>
        // Gets a System.IO.Stream object that points to an uploaded file to prepare
        // for reading the contents of the file. Returns: A System.IO.Stream pointing to a file.
        /// </summary>
        [NotMapped]
        public virtual Stream InputStream
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return new MemoryStream(this.Data); }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                BinaryReader reader = new BinaryReader(value);

                try
                {
                    this.OnPropertyChanging("InputStream");
                    this._data = reader.ReadBytes((int)value.Length);
                    this.MarkChanged("InputStream");
                }
                finally
                {
                    reader.Close();
                }
            }
        }

        public enum MediaType
        {
            application,
            audio,
            example,
            image,
            message,
            model,
            multipart,
            text,
            video,
            other
        }

        public MediaType Media
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (UploadedFile.MediaType)Enum.Parse(typeof(UploadedFile.MediaType),
                            this.ContentType.Substring(0, this.ContentType.IndexOf("/")).ToLower());
            }
        }

        public string DownloadLink
        {
            get
            {
                return string.Format("{0}{1}?guid={2}",
                  Utils.RelativeWebRoot, UploadStorageService.Settings.FileHandlerUrl, this.ID);
            }
        }

        public string RelativeLink
        {
            get
            {
                if (this.Media == MediaType.image)
                    return string.Format("{0}{1}?guid={2}",
                        Utils.RelativeWebRoot, UploadStorageService.Settings.ImageHandlerUrl, this.ID);

                return string.Format("{0}{1}?guid={2}",
                    Utils.RelativeWebRoot, UploadStorageService.Settings.FileHandlerUrl, this.ID);
            }
        }

        public string HtmlLink
        {
            get
            {
                if (this.Media == MediaType.image)
                    return string.Format("<img src=\"{0}{1}?guid={2}\" alt=\"{3}\" />",
                        new object[] { Utils.RelativeWebRoot, UploadStorageService.Settings.ImageHandlerUrl, 
                            this.ID, this.FileName });

                return string.Format("<p><a href=\"{0}{1}?guid={2}\" rel=\"enclosure\">{3} ({4})</a></p>",
                    new object[] { Utils.RelativeWebRoot, UploadStorageService.Settings.FileHandlerUrl, this.ID, 
                        this.FileName, Utils.SizeFormat(this.ContentLength, "N") });
            }
        }

        public string ThumbnailLink
        {
            get
            {
                if (this.Media == MediaType.image)
                    return string.Format("{0}{1}?guid={2}&width={3}&height={4}&quality={5}",
                        new object[] {
                        Utils.RelativeWebRoot,
                        UploadStorageService.Settings.ImageHandlerUrl,
                        this.ID,
                        UploadStorageService.Settings.Thumbnail.Width,
                        UploadStorageService.Settings.Thumbnail.Height,
                        UploadStorageService.Settings.Thumbnail.Quality });

                return string.Format("{0}{1}", Utils.RelativeWebRoot,
                    UploadStorageService.Settings.Thumbnail.NotThumbnailAvailable);
            }
        }

        #endregion

        #region Validation

        protected override void ValidationRules()
        {
        }

        #endregion

        #region Data Access

        protected override UploadedFile DataSelect(Guid id)
        {
            return UploadStorageService.SelectFile(id);
        }

        protected override void DataUpdate()
        {
            UploadStorageService.UpdateFile(this);
        }

        protected override void DataInsert()
        {
            UploadStorageService.InsertFile(this);
        }

        protected override void DataDelete()
        {
            UploadStorageService.DeleteFile(this);
        }

        #endregion

        #region IDisposeable Members

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (this.InputStream != null && this.InputStream != Stream.Null && this.InputStream.CanRead)
                {
                    this.InputStream.Close();
                    this.InputStream.Dispose();
                    this._inputstream = Stream.Null;
                }

                Trace.TraceInformation(string.Format("{0} Disposing the file with id {1}",
                    DateTime.Now, this.ID));
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion

        public static UploadedFile FromStream(Stream input)
        {
            UploadedFile file = new UploadedFile();
            file.InputStream = input;
            file.ContentLength = file.Data.Length;
            return file;
        }

        public static IEnumerable<UploadedFile> GetFiles(Guid reference)
        {
            return UploadStorageService.GetFiles(reference);
        }
    }
}
