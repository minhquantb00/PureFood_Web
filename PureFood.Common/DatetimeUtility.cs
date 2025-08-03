using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.Common;

public class DatetimeUtility
{
    public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dtDateTime;
    }

    public static DateTime UnixTimeStampMillisecondToDateTime(long unixTimeStampMillisecond)
    {
        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddMilliseconds(unixTimeStampMillisecond).ToLocalTime();
        return dtDateTime;
    }

    public static long DateTimeToUnixTimeStamp(DateTime? dateTime)
    {
        if (dateTime == null) return 0;
        return ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
    }
}
