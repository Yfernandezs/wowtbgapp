using System;
using System.Collections;

namespace WoWTBGapp.Utils
{
    public interface ILogger
    {
        void TrackPage(string page, string id = null);

        void Track(string trackIdentifier);

        void Track(string trackIdentifier, string key, string value);

        void Report(Exception exception = null, Severity warningLevel = Severity.Warning);

        void Report(Exception exception, IDictionary extraData, Severity warningLevel = Severity.Warning);

        void Report(Exception exception, string key, string value, Severity warningLevel = Severity.Warning);
    }
}
