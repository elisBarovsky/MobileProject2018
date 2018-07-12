using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ParentsDay
/// </summary>
public class ParentsDay
{
    public int ParentsDayCode { get; set; }
    public int CodeWeekDay { get; set; }
    public string WeekDayName { get; set; }
    public string ParentsDayDate { get; set; }
    public string TeacherID { get; set; }
    public string TeacherName { get; set; }
    public int ClassCode { get; set; }
    public string ClassName { get; set; }
    public string from { get; set; }
    public string to { get; set; }
    public int longMeeting { get; set; }

    public List<Meeting> ParentsDayMeetings { get; set; }

    DBconnectionTeacher dbt = new DBconnectionTeacher();

    public ParentsDay()
    {

    }

    public ParentsDay IfMehanech_LoadParentDay(string UserId)
    {
        return dbt.LoadParentDay(UserId);
    }

    public int SaveParentsDay(ParentsDay p)
    {
        return dbt.SaveParentsDay(p);
    }

    public int GiveMeBreak(string ParentsDayMeeting)
    {
        return dbt.GiveMeBreak(ParentsDayMeeting);

    }

    public int DeleteBreak(string ParentsDayMeeting)
    {
        return dbt.DeleteBreak(ParentsDayMeeting);
    }

    public ParentsDay Parent_LoadParentDay(string PupilID)
    {
        return dbt.Parent_LoadParentDay(PupilID);
    }
}