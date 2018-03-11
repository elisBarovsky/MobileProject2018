using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HomeWork
/// </summary>
public class HomeWork
{
    DBconnection db;
    DBconnectionTeacher dbT;

    public HomeWork()
    {
        dbT = new DBconnectionTeacher();
        db = new DBconnection();
    }

    public int InserHomeWork(string LessonsCode, string HWInfo, string TeacherID, string CodeClass, string HWDate, bool IsLehagasha)
    {
        return dbT.InserHomeWork(LessonsCode, HWInfo, TeacherID, CodeClass, HWDate, IsLehagasha);
    }

    public DataTable FilterHomeWork(string TeacherID, string LessonsCode, string ClassCode)
    {
        return dbT.FilterHomeWork(TeacherID, LessonsCode, ClassCode);
    }

    public DataTable FillAllHomeWork(string PupilID)
    {
        return dbT.FillAllHomeWork(PupilID);
    }

    public DataTable FillBySubjectHomeWork(string PupilID, string ChooseSubjectCode)
    {
        return dbT.FillBySubjectHomeWork(PupilID, ChooseSubjectCode);
    }
}