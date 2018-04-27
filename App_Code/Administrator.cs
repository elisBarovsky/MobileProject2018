using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Administrator
/// </summary>
public class Administrator : Users
{
    DBconnection db = new DBconnection();

    public Administrator()
    {

    }
    public int AddPupil(string UserID, int classNumber)
    {
        return db.AddPupil(UserID, classNumber);
    }

    public int UpdatePupil(string userID, string CodeClass)
    {
        return db.UpdatePupil(userID, CodeClass);
    }

    public int AddTeacher(string UserID, string IsMain)
    {
        return db.AddTeacher(UserID, IsMain);
    }

    public int UpdateTeacher(string UserID, string IsMain)
    {
        return db.UpdateTeacher(UserID, IsMain);
    }

    public int AddMainTeacherToClass(string id, string OtClass)
    {
        return db.AddMainTeacherToClass(id, OtClass);
    }

    public int AddClassTeacher(string UserID, string ClassOt)
    {
        return db.AddMainTeacherToClass(UserID, ClassOt);
    }

    public int DeleteMainTeacherToClass(string TotalClassName)
    {
        return db.DeleteMainTeacherToClass(TotalClassName); // execute the command 
    }

    public List<string> IsAlreadyMainTeacher(string id)
    {
        return db.IsAlreadyMainTeacher(id);
    }

    public int AddParent(string ParentID, string PupilID, string ChildCodeClass)
    {
        return db.AddParent(ParentID, PupilID, ChildCodeClass);
    }

    public int UpdateParent(string PupilID, string ParentID, string ChildCodeClass)
    {
        return db.UpdateParent(PupilID, ParentID, ChildCodeClass);
    }

    public int AddUser(Users NewUser)
    {
        return db.AddUser(NewUser);
    }

    public int UpdateUser(string userID, string userFName, string userLName, string birthDate, string userImg, string userName, string userPassword, string phoneNumber)
    {
        return db.UpdateUser(userID, userFName, userLName, birthDate, userImg, userName, userPassword, phoneNumber);
    }
}