using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Classes
/// </summary>
public class Classes
{
    DBconnection db;

    public Classes()
    {
        db = new DBconnection();
    }

    public List<string> GetClassesOt()
    {
        return db.GetClassesOt();
    }

    public List<string> GetClassesFullName()
    {
        return db.GetClassesFullName();
    }

    public int InsertClass(string ClassOt, string ClassNum)
    {
        return db.InsertClass(ClassOt, ClassNum);
    }

    public string GetClassNameByCodeClass(int classCode)
    {
        return db.GetClassNameByCodeClass(classCode);
    }

    public List<string> ClassesExites(string ClassOt, string ClassNum)
    {
        return db.ClassesExites(ClassOt, ClassNum);
    }

    public string GetClassCodeAccordingToClassFullName(string classTotalName)
    {
        return db.GetClassCodeAccordingToClassFullName(classTotalName);
    }
}