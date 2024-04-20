using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Models
{
    public class DelegateWrapper
    {
        public Delegate Delegate {  get; private set; }
        
        public GCHandle? GCH { get; private set; }

        public DelegateWrapper(Delegate unmanagerMethod)
        {
            Delegate = unmanagerMethod;
            GCH = GCHandle.Alloc(Delegate);
        }

        public void Free()
        {
            if (GCH.HasValue)
            {
                GCH.Value.Free();
            }
        }
    }
}
