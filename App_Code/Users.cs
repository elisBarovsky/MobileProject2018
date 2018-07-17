using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Users
/// </summary>
public class Users
{
    DBconnection db;
    string UserID, UserFName, UserLName, BirthDate, UserImg, UserName, UserPassword, PhoneNumber, CodeUserType;
    private string regId;

    public string UserID1
    {
        get
        {
            return UserID;
        }

        set
        {
            UserID = value;
        }
    }

    public string RegId
    {
        get { return regId; }
        set { regId = value; }
    }

    public string UserFName1
    {
        get
        {
            return UserFName;
        }

        set
        {
            UserFName = value;
        }
    }

    public string UserLName1
    {
        get
        {
            return UserLName;
        }

        set
        {
            UserLName = value;
        }
    }

    public string BirthDate1
    {
        get
        {
            return BirthDate;
        }

        set
        {
            BirthDate = value;
        }
    }

    public string UserImg1
    {
        get
        {
            return UserImg;
        }

        set
        {
            UserImg = value;
        }
    }

    public string UserName1
    {
        get
        {
            return UserName;
        }

        set
        {
            UserName = value;
        }
    }

    public string UserPassword1
    {
        get
        {
            return UserPassword;
        }

        set
        {
            UserPassword = value;
        }
    }

    public string PhoneNumber1
    {
        get
        {
            return PhoneNumber;
        }

        set
        {
            PhoneNumber = value;
        }
    }

    public string CodeUserType1
    {
        get
        {
            return CodeUserType;
        }

        set
        {
            CodeUserType = value;
        }
    }

    public Users()
    {
        db = new DBconnection();
    }

    public Users(string userID)
    {
        db = new DBconnection();
        UserID = userID;
        List<string> userDetails = GetUserInfo(userID);
        UserFName = userDetails[1];
        UserLName = userDetails[2];
        BirthDate = userDetails[3];
        UserImg = userDetails[4];
        UserName = userDetails[5];
        UserPassword = userDetails[6];
        PhoneNumber = userDetails[7];
        CodeUserType = userDetails[8];
    }

    public Users(string userID, string userFName, string userLName, string birthDate, string userImg, string userName, string userPassword, string phoneNumber, string codeUserType)
    {
        db = new DBconnection();

        UserID = userID;
        UserFName = userFName;
        UserLName = userLName;
        BirthDate = birthDate;
        UserImg = userImg;
        UserName = userName;
        UserPassword = userPassword;
        PhoneNumber = phoneNumber;
        CodeUserType = codeUserType;
    }

    public List<Users> getUserList()
    {
        db = new DBconnection();
        List<Users> listUser = new List<Users>();
        listUser = db.getUserList("Betsefer", "users");

        return listUser;
    }
    public string GetNumChild(string UserID)
    {
        return db.GetNumChild(UserID);
    }

    public Dictionary<string, string> getPupils(string classCode)
    {
        return db.getPupils(classCode);
    }

    public List<Dictionary<string, string>> getPupilsByClassCode(string TeacherID)
    {
        return db.getPupilsByClassCode(TeacherID);
    }

    public List<Dictionary<string, string>> getPupilsAndTeachers(string TeacherID)
    {
        return db.getPupilsAndTeachers(TeacherID);
    }

    public List<Dictionary<string, string>> getParentsAndTeachers(string TeacherID)
    {
        return db.getParentsAndTeachers(TeacherID);
    }

    public Dictionary<string, string> FillUsers(string CodeUserType)
    {
        return db.FillUsers(CodeUserType);
    }

    public Dictionary<string, string> FillClassOt()
    {
        return db.FillClassOt();
    }

    public List<string>  GetUserType(string UserID, string password)
    {
        return db.GetUserType(UserID, password);
    }

    public string GetUserTypeById(string UserID)
    {
        return db.GetUserTypeById(UserID);
    }

    public string GetPupilOtClass(string UserID)
    {
        return db.GetPupilOtClass(UserID);
    }

    public bool GetTeacherMain(string UserID)
    {
        return db.GetTeacherMain(UserID);
    }

    public string GetTeacherMainClass(string UserID)
    {
        return db.GetTeacherMainClass(UserID);
    }

    public List<string> GetUserInfo(string UserID)
    {
        return db.GetUserInfo(UserID);
    }

    public List<string> GetUserSecurityDetailsByuserIDandBday(string userID, string Bday)
    {
        List<string> l = new List<string>();
        l = db.GetUserSecurityDetailsByuserIDandBday(userID, Bday);

        return l;
    }
    
    public int ChangePassword(string userID, string Password)
    {
        return db.ChangePassword(userID, Password);
    }

    public int PushUpdateRegId(string userID, string RegID)
    {
        return db.PushUpdateRegId(userID, RegID);
    }

    public string IsAlreadyLogin(string UserID, string password)
    {
        return db.IsAlreadyLogin(UserID, password);
    }

    public int SaveQuestion(string id, int q1, string a1, int q2, string a2)
    {
        return db.SaveQuestion(id, q1, a1, q2, a2);
    }

    public int ChangeFirstLogin(string id)
    {
        return db.ChangeFirstLogin(id);
    }

    public List<Dictionary<string, string>> GetTeachers2()
    {
        return db.GetTeachers2();
    }

    public Dictionary<string, string> GetTeachers()
    {
        return db.GetTeachers();
    }

    public int GetMainTeacherClass(string id)
    {
        DBconnectionTeacher dbt = new DBconnectionTeacher();
        return dbt.GetMainTeacherClass(id);
    }

    public string GetUserFullNameByID(string teacherId)
    {
        return db.GetUserFullNameByID(teacherId);
    }

    public string GetUserIDByFullName(string FullName)
    {
        return db.GetUserIDByFullName(FullName);

    }
    public bool IsLegalBday(string day, string month)
    {
        if (int.Parse(month) <= 7)
        {
            if (int.Parse(month) % 2 == 0)
            {
                if ((int.Parse(month) != 2 && int.Parse(day) > 30) || (int.Parse(month) == 2 && int.Parse(day) > 28))
                {
                    return false;
                }
            }
        }
        else
        {
            if (int.Parse(month) % 2 != 0)
            {
                if (int.Parse(day) > 30)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public List<string> GetPupilIdByUserTypeAndId(string UserId)
    {
        DBconnection db = new DBconnection();
        return db.GetPupilIdByParentId(UserId);
    }

    public string IsStudentUserNotThisParentYet(string childID, string parentID)
    {
        return db.IsStudentUserNotThisParentYet(childID, parentID);
    }

    public List<string> GetUserImgAndFullNameByUserID(string UserID)
    {
        return db.GetUserImgAndFullNameByUserID(UserID);
    }

    public string GetUserFullName(string Id)
    {
        return db.GetUserFullName(Id);
    }

    public string GetUserImgByUserID(string UserID)
    {
        return db.GetUserImgByUserID(UserID);
    }

    public DataTable getPupillistsByClassCode(string ClassCode)
    {
        return db.getPupillistsByClassCode(ClassCode);
    }

    public List<Dictionary<string, string>> getParentsByClassCode(string classCode)
    {
        return db.getParentsByClassCode(classCode);
    }
}

