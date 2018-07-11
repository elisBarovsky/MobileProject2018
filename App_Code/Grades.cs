using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Grades
/// </summary>
public class Grades
{
    DBconnectionTeacher dbT;
    DBconnection db;

    public Grades()
    {
        dbT = new DBconnectionTeacher();
        db = new DBconnection();
    }

    public Dictionary<string, string> FillClassOt()
    {
        return db.FillClassOt();
    }

    public Dictionary<string, string> FillLessons()
    {
        return dbT.FillLessons();
    }

    public DataTable PupilList(string ClassOtID)
    {
        return dbT.PupilList(ClassOtID);
    }

    public int InsertGrade(string PupilID, string TeacherID, string CodeLesson, string ExamDate, int Grade)
    {
        return dbT.InsertGrade(PupilID, TeacherID, CodeLesson, ExamDate, Grade);
    }

    public DataTable PupilGrades(string PupilID) // NEW !!!!
    {
        return dbT.PupilGrades(PupilID);
    }

    public DataTable FilterGrade(string GradeDate)  // NEW !
    {
        return dbT.FilterGrade(GradeDate);
    }

    public DataTable PupilAvgGrades(string ClassCode) // NEW !!!!
    {
        return dbT.PupilAvgGrades(ClassCode);
    }
}