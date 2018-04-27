using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Teacher
/// </summary>
public class Teacher : Users
{
    DBconnection db = new DBconnection();

    public string classMainTeacher { get; set; }
    public bool isMainTeacher { get; set; }

    public Teacher()
    {
        //i need to check what returns when this is not a main teacher if null or zero or something.

        classMainTeacher = db.IsAlreadyMainTeacher(this.UserID1).FirstOrDefault();
        if (classMainTeacher != null) isMainTeacher = true;
        else isMainTeacher = false;
    }
}