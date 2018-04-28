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
    public List<Parent> parents { get; set; }

    public Student()
    {
        otClass = this.GetPupilOtClass(this.UserID1);
        parents = new List<Parent>();
        GetMyParents();
        // fill parents by crate a function in dbconnection.
    }

    public Student(string id)
    {
        this.UserID1 = id;
        otClass = this.GetPupilOtClass(this.UserID1);
        parents = new List<Parent>();
        GetMyParents();
    }


    public void GetMyParents()
    {
        List<string> parentsIds = db.GetParentsIdsByPupilId(this.UserID1);

        for (int i = 0; i < parentsIds.Count; i++)
        {
            List<string> userInfo = this.GetUserInfo(parentsIds[i]);
            Parent s = new Parent();
            s.UserID1 = userInfo[0];
            s.UserFName1 = userInfo[1];
            s.UserLName1 = userInfo[2];
            s.PhoneNumber1 = userInfo[5];
            s.UserPassword1 = userInfo[4];
            s.UserImg1 = userInfo[6];
            s.BirthDate1 = userInfo[3];

            parents.Add(s);
        }
    }
}