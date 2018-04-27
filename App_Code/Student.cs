using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Student
/// </summary>
public class Student : Users
{
    DBconnection db = new DBconnection();

    public string otClass { get; set; }
    public Parent[] parents { get; set; }

    public Student()
    {
        otClass = this.GetPupilOtClass(this.UserID1);

        // fill parents by crate a function in dbconnection.
    }
}