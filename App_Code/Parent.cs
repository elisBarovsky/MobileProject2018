using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Parent
/// </summary>
public class Parent : Users
{
    DBconnection db = new DBconnection();

    public int numChildren { get; set; }
    public List<Student> children { get; set; }

    public Parent()
    {
        children = new List<Student>();
        FindMyChildren();
    }

    public Parent(string id)
    {
        this.UserID1 = id;
        children = new List<Student>();
        FindMyChildren();
    }

    public void FindMyChildren()
    {
        numChildren = int.Parse(this.GetNumChild(this.UserID1));
        List<string> childrenIds = db.GetPupilIdByParentId(this.UserID1);
        for (int i = 0; i < numChildren; i++)
        {
            List<string> userInfo = this.GetUserInfo(childrenIds[i]);
            Student s = new Student();
            s.UserID1 = userInfo[0];
            s.UserFName1 = userInfo[1];
            s.UserLName1 = userInfo[2];
            s.PhoneNumber1 = userInfo[5];
            s.UserPassword1 = userInfo[4];
            s.UserImg1 = userInfo[6];
            s.BirthDate1 = userInfo[3];

            children.Add(s);
        }
    }

    public Student[] ChildrenToArray()
    {
        Student[] pupils = new Student[children.Count * 2];
        for (int i = 0; i < children.Count; i++)
        {
            pupils[i] = children[i];
        }
        return pupils;
    }
}