using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TimeTable
/// </summary>
public class TimeTable
{
    DBconnection db = new DBconnection();

    public TimeTable()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int InsertTimeTable(List<Dictionary<string, string>> matrix, string classCode)
    {
        return db.InsertTimeTable(matrix, classCode);
    }

    public List<Dictionary<string, string>> GetTimeTableAcordingToClassCode(int classCode)
    {
        return db.GetTimeTableAcordingToClassCode(classCode);
    }

    public bool IsClassHasTimeTable(string classCodee)
    {
        return db.IsClassHasTimeTable(classCodee);
    }

    public int DeleteTimeTable(string classCode)
    {
        return db.DeleteTimeTable(classCode);
    }

    public string GetLessonNameByLessonCode(string lessonCode)
    {
        return db.GetLessonNameByLessonCode(lessonCode);
    }

    public string GetTeacherNameByID(string TeacherId)
    {
        return db.GetTeacherNameByID(TeacherId);
    }
}