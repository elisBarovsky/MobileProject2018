﻿using System;
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

    public DataTable FillAllHomeWork_history(string PupilID)
    {
        return dbT.FillAllHomeWork_history(PupilID);

    }
    public DataTable getHwInfoForProgBar(string PupilID)
    {
        return dbT.getHwInfoForProgBar(PupilID);
    }

    public int HWDone(string PupilID, bool IsDone, string HWCode)
    {
        return dbT.HWDone( PupilID,IsDone, HWCode);
    }

    public DataTable FillBySubjectHomeWork(string PupilID, string ChooseSubjectCode)
    {
        return dbT.FillBySubjectHomeWork(PupilID, ChooseSubjectCode);
    }
}