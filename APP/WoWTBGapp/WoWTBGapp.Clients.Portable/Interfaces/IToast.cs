using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoWTBGapp.Clients.Portable
{
    public interface IToast
    {
        void SendToast(string message, int length);
    }
}
