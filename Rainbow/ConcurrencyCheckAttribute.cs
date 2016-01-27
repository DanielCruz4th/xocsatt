using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainbow.Web
{
    /// <summary>
    /// Explicity include the BusinessBase.HasConcurrency method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class ConcurrencyCheckAttribute : Attribute
    {
    }
}
