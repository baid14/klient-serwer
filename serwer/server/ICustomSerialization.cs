using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialisation
{
    public interface ICustomSerialization
    {
        byte[] toByteArray();
        bool fromByteArray(byte[] arr);

    }
}
