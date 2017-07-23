using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSample
{
    public interface IBackend
    {
        string getContents(string message);
        Guid InstanceID();
    }
    class Backend : IBackend
    {
        private Guid InstanceID;
        public Backend()
        {
            this.InstanceID = Guid.NewGuid();
        }

        public string getContents(string message)
        {
            return $"[Backend]: {message}";
        }

        Guid IBackend.InstanceID()
        {
            return this.InstanceID;
        }
    }
}
