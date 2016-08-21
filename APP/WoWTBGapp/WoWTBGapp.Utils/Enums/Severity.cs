using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoWTBGapp.Utils
{
    public enum Severity
    {
        /// <summary>
        /// Warning Severity
        /// </summary>
        Warning,

        /// <summary>
        /// Error Severity, you are not expected to call this from client side code unless you have disabled unhandled exception handling.
        /// </summary>
        Error,

        /// <summary>
        /// Critical Severity
        /// </summary>
        Critical
    }
}
