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

    public int InsertTimeTable(string date, int classCode, bool publish)
    {
        return db.InsertTimeTable(date, classCode, publish);
    }

    public int UpdateStatusTT(string ClassCode, bool publish)
    {
        return db.UpdateStatusTT(ClassCode, publish);
    }

    public int InsertTempTimeTable(string date, int CodeWeekDay, int ClassTimeCode, int CodeLesson, string TeacherId, int ClassNum)
    {
        return db.InsertTempTimeTable(date, CodeWeekDay, ClassTimeCode, CodeLesson, TeacherId, ClassNum);
    }

    public int InsertUpdateTimeTable(string TableCode, int CodeWeekDay, int ClassTimeCode, int CodeLesson, string TeacherId)
    {
        return db.InsertUpdateTimeTable(TableCode, CodeWeekDay, ClassTimeCode, CodeLesson, TeacherId);
    }

    public List<string> GetCellInfo(string date, int WeekDay, int LessonNum, string ClassNmum)
    {
        return db.GetCellInfo(date, WeekDay, LessonNum, ClassNmum);
    }

    public List<string> GetCellInfoUPDATE(string codeTable, int WeekDay, int LessonNum)
    {
        return db.GetCellInfoUPDATE(codeTable, WeekDay, LessonNum);
    }

    public List<Dictionary<string, string>> GetTimeTableAcordingToClassCode(int classCode)
    {
        return db.GetTimeTableAcordingToClassCode(classCode);
    }

    public List<Dictionary<string, string>> GetTimeTableAcordingToClassCodeForMobile(int classCode)
    {
        return db.GetTimeTableAcordingToClassCodeForMobile(classCode);
    }

    public bool IsClassHasTimeTable(string classCodee)
    {
        return db.IsClassHasTimeTable(classCodee);
    }

    public string GetCellInfoUPDATECodeTable(string classCode)
    {
        return db.GetCellInfoUPDATECodeTable(classCode);
    }

    public int DeleteTimeTableLessons(string classCode)
    {
        return db.DeleteTimeTableLessons(classCode);
    }

    public int DeleteTimeTable(string classCode)
    {
        return db.DeleteTimeTable(classCode);
    }

    public int DeleteTempTT(string date, string classcode)
    {
        return db.DeleteTempTT(date, classcode);
    }

    public int DeleteTT(string classcode)
    {
        return db.DeleteTT(classcode);
    }

    public string GetLessonNameByLessonCode(string lessonCode)
    {
        return db.GetLessonNameByLessonCode(lessonCode);
    }
}