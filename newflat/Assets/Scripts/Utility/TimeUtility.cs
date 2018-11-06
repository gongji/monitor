using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 时间工具类
/// </summary>
public class TimeUtility
{

    public struct Units
    {
        public int hours;
        public int minutes;
        public int seconds;
        public int deciSeconds;
        public int centiSeconds;
        public int milliSeconds;
    }

    public static Units TimeToUnits(float timeInSeconds)
    {
        Units iTime = new Units();
        iTime.hours = ((int)timeInSeconds) / 3600;
        iTime.minutes = (((int)timeInSeconds) - (iTime.hours * 3600)) / 60;
        iTime.seconds = ((int)timeInSeconds) % 60;

        iTime.deciSeconds = (int)((timeInSeconds - iTime.seconds) * 10) % 60;
        iTime.centiSeconds = (int)((timeInSeconds - iTime.seconds) * 100 % 600);
        iTime.milliSeconds = (int)((timeInSeconds - iTime.seconds) * 1000 % 6000);

        return iTime;
    }

    public static float UnitsToSeconds(Units units)
    {
        float seconds = 0.0f;
        seconds += units.hours * 3600;
        seconds += units.minutes * 60;
        seconds += units.seconds;

        seconds += (float)units.deciSeconds * 0.1f;
        seconds += (float)(units.centiSeconds / 100);
        seconds += (float)(units.milliSeconds / 1000);

        return seconds;
    }

    public static string TimeToString(float timeInSeconds, bool showHours, bool showMinutes, bool showSeconds, bool showTenths, bool showHundredths, bool showMilliSeconds, char delimiter = ':')
    {
        Units iTime = TimeToUnits(timeInSeconds);

        string hours = (iTime.hours < 10) ? "0" + iTime.hours.ToString() : iTime.hours.ToString();
        string minutes = (iTime.minutes < 10) ? "0" + iTime.minutes.ToString() : iTime.minutes.ToString();
        string seconds = (iTime.seconds < 10) ? "0" + iTime.seconds.ToString() : iTime.seconds.ToString();
        string deciSeconds = iTime.deciSeconds.ToString();
        string centiSeconds = (iTime.centiSeconds < 10) ? "0" + iTime.centiSeconds.ToString() : iTime.centiSeconds.ToString();
        string milliSeconds = (iTime.milliSeconds < 100) ? "0" + iTime.milliSeconds.ToString() : iTime.milliSeconds.ToString();
        milliSeconds = (iTime.milliSeconds < 10) ? "0" + milliSeconds : milliSeconds;

        return ((showHours ? hours : "") +
        (showMinutes ? delimiter + minutes : "") +
        (showSeconds ? delimiter + seconds : "") +
        (showTenths ? delimiter + deciSeconds : "") +
        (showHundredths ? delimiter + centiSeconds : "") +
        (showMilliSeconds ? delimiter + milliSeconds : "")).TrimStart(delimiter);
    }

    public static string SystemTimeToString(System.DateTime time, bool bHours, bool bMinutes, bool bSeconds, bool bTenths, bool bHundredths, bool bMilliSeconds, char delimiter = ':')
    {
        return TimeToString(GetTimeToSeconds(time), bHours, bMinutes, bSeconds, bTenths, bHundredths, bMilliSeconds, delimiter);
    }

    public static string SystemTimeToString(bool showHours, bool showMinutes, bool showSeconds, bool showTenths, bool showHundredths, bool showMilliSeconds, char delimiter = ':')
    {
        return SystemTimeToString(System.DateTime.Now, showHours, showMinutes, showSeconds, showTenths, showHundredths, showMilliSeconds, delimiter);
    }

    public static Units SystemTimeToUnits(System.DateTime systemTime)
    {
        Units iTime = new Units();

        iTime.hours = systemTime.Hour;
        iTime.minutes = systemTime.Minute;
        iTime.seconds = systemTime.Second;
        iTime.deciSeconds = (int)(systemTime.Millisecond / 100.0f);
        iTime.centiSeconds = (int)(systemTime.Millisecond / 10);
        iTime.milliSeconds = systemTime.Millisecond;

        return iTime;
    }

    public static Units SystemTimeToUnits()
    {
        return SystemTimeToUnits(System.DateTime.Now);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="systemTime">systemTime的datetime</param>
    /// <returns>返回时间戳</returns>
    public static float GetTimeToSeconds(System.DateTime systemTime)
    {
        return UnitsToSeconds(SystemTimeToUnits(systemTime));
    }

    public static float SystemTimeToSeconds()
    {
        return GetTimeToSeconds(System.DateTime.Now);
    }


    /// <summary>
    /// 时间戳转为C#格式时间
    /// </summary>
    /// <param name="timeStamp">Unix时间戳格式</param>
    /// <returns>C#格式时间</returns>
    public static string GetTime(string _time)
    {
        _time = _time.Substring(0, 10);
        //Debug.Log("_time" + _time);
        string timeStamp = _time;
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        DateTime dtResult = dtStart.Add(toNow);
        string date = dtResult.ToShortDateString().ToString();
        string time = dtResult.ToLongTimeString().ToString();
        string[] date_arr = date.Split('/');
        string[] time_arr = time.Split('/');

        string result = date_arr[0] + "月" + date_arr[1] + "日" + " " + time_arr[0] + "时" + time_arr[1] + "";

        //Debug.Log("_time" + result);
        return result;
    }

    // 时间戳转为C#格式时间
    public static string StampToDateTime(string timeStamp)
    {
        timeStamp = timeStamp.Substring(0, 10);
#if DEBUG
        //Debug.Log("_time" + timeStamp);
#endif
        DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        DateTime dTime = dateTimeStart.Add(toNow);
        string result = dTime.Year + "/" + dTime.Month.ToString("00") + "/" + dTime.Day.ToString("00") + " " 
            + dTime.Hour.ToString("00") + ":" + dTime.Minute.ToString("00") + ":" + dTime.Second.ToString("00");
        return result;
    }

    public static string StampToDateTime2(string timeStamp)
    {
        timeStamp = timeStamp.Substring(0, 10);
#if DEBUG
        //Debug.Log("_time" + timeStamp);
#endif
        DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        DateTime dTime = dateTimeStart.Add(toNow);
        string result = dTime.Year + "-" + dTime.Month.ToString("00") + "-" + dTime.Day.ToString("00") + " "
            + dTime.Hour.ToString("00") + ":" + dTime.Minute.ToString("00") + ":" + dTime.Second.ToString("00");
        return result;
    }

    /// <summary>
    /// 获取当前的时间
    /// </summary>
    /// <returns></returns>
    public static string GetCurrenDate()
    {
        string timeString = GetTimeStamp(false);
        System.DateTime time = StampToTime(timeString);
        timeString = time.Year + "-" + time.Month.ToString("00") + "-" + time.Day.ToString("00") + " " + time.Hour.ToString("00") + ":"
            + time.Minute.ToString("00") + ":" + time.Second.ToString("00");
        return timeString;
    }

    public static DateTime StampToTime(string timeStamp)
    {

        if (string.IsNullOrEmpty(timeStamp) || timeStamp == "0")
        {
            return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2016, 1, 1));
        }
        timeStamp = timeStamp.Substring(0, 10);
#if DEBUG
        //Debug.Log("_time" + timeStamp);
#endif
        DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        DateTime dTime = dateTimeStart.Add(toNow);
        return dTime;
    }

    /// <summary>
    /// 格式化为年-月-日
    /// </summary>
    /// <param name="stamp"></param>
    /// <returns></returns>
    public static string formatDate(string stamp)
    {
        DateTime time = StampToTime(stamp);

        return time.Year.ToString() + "-" +
             time.Month.ToString() + "-" +
             time.Day.ToString() + " " +
             time.Hour.ToString() + ":" +
             time.Minute.ToString() + ":" +
             time.Second.ToString();
    }

    /// <summary>
    ///获取年份
    /// </summary>
    /// <param name="stamp"></param>
    /// <returns></returns>
    public static int GetYear(string stamp)
    {
        DateTime time =StampToTime(stamp);

        return time.Year;
    }

    /// <summary>  
    /// 获取当前时间戳  
    /// </summary>  
    /// <param name="bflag">为真时获取10位时间戳,为假时获取13位时间戳.</param>  
    /// <returns></returns>  
    public static string GetTimeStamp(bool bflag = true)
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        string ret = string.Empty;
        if (bflag)
            ret = Convert.ToInt64(ts.TotalSeconds).ToString();
        else
            ret = Convert.ToInt64(ts.TotalMilliseconds).ToString();

        return ret;
    }

    /// <summary>
    /// 获取当前的时间戳
    /// </summary>
    /// <param name="bflag"></param>
    /// <returns></returns>
    public static DateTime GetDateTimeStamp(bool bflag = true)
    {
        //DateTime retTime;
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        string ret = string.Empty;
        if (bflag)
            ret = Convert.ToInt64(ts.TotalSeconds).ToString();
        else
            ret = Convert.ToInt64(ts.TotalMilliseconds).ToString();
        
        return StampToTime(ret);
    }

    /// <summary>
    /// 获取当前的时间戳
    /// </summary>
    /// <param name="isMilliseconds"></param>
    /// <returns></returns>
    public static long GetCurrentTimeStamp(bool isMilliseconds)
    {
       
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        if (!isMilliseconds)
            return Convert.ToInt64(ts.TotalSeconds);
        else
            return Convert.ToInt64(ts.TotalMilliseconds);
    }



    public static long GetTimeSpan(DateTime time)
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
        return (long)(time - startTime).TotalMilliseconds;
    }
}
