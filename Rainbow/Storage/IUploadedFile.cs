using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainbow.Web.Storage
{
    public interface IUploadedFile
    {
        string ApplicationName { get; }
        Guid? ParentID { get; }
        string FileName { get; }
        string Description { get; }
        byte[] Data { get; }
        DateTime DateCreated { get; }
        DateTime? LastUpdated { get; }
        string ContentType { get; }
        int ContentLength { get; }
    }
}
