using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Meeting
/// </summary>
public class Meeting
{
    public string MeetingCode { get; set; }
    public string PupilID { get; set; }
    public string PupilName { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }

    public Meeting(string _PupilID, string _PupilName, string _StartTime, string _EndTime)
    {
        PupilID = _PupilID;
        PupilName = _PupilName;
        StartTime = _StartTime;
        EndTime = _EndTime;
    }
    public Meeting()
    {

    }
}