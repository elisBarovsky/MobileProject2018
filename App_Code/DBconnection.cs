using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Summary description for DBconnection
/// </summary>
public class DBconnection
{
    public SqlDataAdapter da;
    public DataTable dt;

    SqlConnection con = new SqlConnection();

    public DBconnection()
    {

    }

    public SqlConnection connect(String conString)
    {
        string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = CommandSTR;
        cmd.CommandTimeout = 10;
        cmd.CommandType = System.Data.CommandType.Text;
        return cmd;
    }

    public string GetUserType(string UserID, string password)
    {
        String selectSTR = "SELECT CodeUserType  FROM Users where UserID  = '" + UserID + "' and LoginPassword  = '" + password + "'";
        string type = "";
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                type = dr["CodeUserType"].ToString();
            }
            return type;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public bool IsExists(string newSubject)
    {
        String selectSTR = "SELECT count(LessonName) FROM Lessons where LessonName  = '" + newSubject + "'";
        int countRow = 0;
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                countRow = int.Parse(dr[0].ToString());
            }

            if (countRow > 0)
            {
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public int AddNewSubject(string newSubject)
    {
        String cStr = "INSERT INTO [dbo].[Lessons]  (LessonName) VALUES ('" + newSubject + "')";
        return ExecuteNonQuery(cStr);
    }

    public string GetPupilGroup(string UserID)
    {
        String selectSTR = "SELECT CodePgroup  FROM Pupil where UserID  = '" + UserID + "'";
        string CodePgroup = "";

        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                CodePgroup = dr["CodePgroup"].ToString();
            }
            return CodePgroup;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }



    public string GetPupilOtClass(string UserID)
    {
        String selectSTR = "SELECT  dbo.Class.ClassCode FROM dbo.Class INNER JOIN  dbo.Pupil ON dbo.Class.ClassCode = dbo.Pupil.CodeClass where  dbo.Pupil.UserID='" + UserID + "'";
        string ClassCode = "";
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                ClassCode = dr["ClassCode"].ToString();
            }
            return ClassCode;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public bool GetTeacherMain(string UserID)
    {
        String selectSTR = "SELECT IsMainTeacher  FROM Teachers where TeacherID  = '" + UserID + "'";
        bool Checked = false;
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                Checked = bool.Parse(dr["IsMainTeacher"].ToString());
            }
            return Checked;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string GetTeacherMainClass(string UserID)
    {
        String selectSTR = "SELECT ClassCode  FROM Class where MainTeacherID  = '" + UserID + "'";
        string ClassCode = "";
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                ClassCode = dr["ClassCode"].ToString();
            }
            return ClassCode;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public List<string> GetUserInfo(string UserID)
    {
        string ID, UserFName, UserLName, BirthDate, UserImg, UserPassword, PhoneNumber;
        String selectSTR = "select * from [dbo].[Users] where UserID  = '" + UserID + "'";
        List<string> UserInfo = new List<string>();
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                ID = dr["UserID"].ToString();
                UserInfo.Add(ID);
                UserFName = dr["UserFName"].ToString();
                UserInfo.Add(UserFName);
                UserLName = dr["UserLName"].ToString();
                UserInfo.Add(UserLName);
                BirthDate = dr["BirthDate"].ToString();
                UserInfo.Add(BirthDate);
                UserPassword = dr["LoginPassword"].ToString();
                UserInfo.Add(UserPassword);
                PhoneNumber = dr["PhoneNumber"].ToString();
                UserInfo.Add(PhoneNumber);
                UserImg = dr["UserImg"].ToString();
                UserInfo.Add(UserImg);
            }
            return UserInfo;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public List<string> GetUserSecurityDetailsByuserIDandBday(string userID, string Bday)
    {
        List<string> l = new List<string>();
        l = GetSecurityInfo(1, userID, Bday).Concat(GetSecurityInfo(2, userID, Bday)).ToList();
        return l;
    }

    public List<string> GetSecurityInfo(int numQ, string id, string bDay)
    {
        string str = "";
        SqlCommand cmd;
        List<string> l = new List<string>();
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            switch (numQ)
            {
                case 1:
                    str = "SELECT dbo.SecurityQ.SecurityQInfo, dbo.Users.SecurityQ1Answer FROM dbo.SecurityQ " +
                "INNER JOIN dbo.Users ON dbo.SecurityQ.CodeSecurityQ = dbo.Users.SecurityQ1Code " +
                "where dbo.Users.UserID = '" + id + "' and dbo.Users.BirthDate ='" + bDay + "'";

                    cmd = new SqlCommand(str, con);
                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (dr.Read())
                    {
                        string Q1 = dr["SecurityQInfo"].ToString();
                        l.Add(Q1);
                        string answer1 = dr["SecurityQ1Answer"].ToString();
                        l.Add(answer1);
                    }
                    break;
                case 2:
                    str = "SELECT dbo.SecurityQ.SecurityQInfo, dbo.Users.SecurityQ2Answer FROM dbo.SecurityQ " +
                "INNER JOIN dbo.Users ON dbo.SecurityQ.CodeSecurityQ = dbo.Users.SecurityQ2Code " +
                "where dbo.Users.UserID = '" + id + "' and dbo.Users.BirthDate ='" + bDay + "'";

                    cmd = new SqlCommand(str, con);
                    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (dr.Read())
                    {
                        string Q2 = dr["SecurityQInfo"].ToString();
                        l.Add(Q2);
                        string answer2 = dr["SecurityQ2Answer"].ToString();
                        l.Add(answer2);
                    }
                    break;
            }
            return l;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public int ChangePassword(string userID, string Password)
    {
        string cStr = "update[dbo].[Users] set[LoginPassword] = ('" + Password + "') WHERE UserID = '" + userID + "'";
        return ExecuteNonQuery(cStr);
    }

    public List<string> GetClassesOt()
    {
        String selectSTR = "select distinct [OtClass] from [dbo].[Class]";
        string Ot;
        List<string> l = new List<string>();
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                Ot = dr["OtClass"].ToString();
                l.Add(Ot);
            }
            return l;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public List<string> ClassesExites(string ClassOt, string ClassNum)
    {
        String selectSTR = "select [TotalName] from [dbo].[Class] where [TotalName] = '" + ClassOt + ClassNum + "'";
        string Ot;
        List<string> l = new List<string>();
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                Ot = dr["TotalName"].ToString();
                l.Add(Ot);
            }
            return l;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string GetClassNameByCodeClass(int codeClass)
    {
        String selectSTR = "select [TotalName] from [dbo].[Class] where [ClassCode] = " + codeClass;
        string className = "";
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                className = dr[0].ToString();
            }
            return className;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public int InsertClass(string ClassOt, string ClassNum)
    {
        string cStr = "INSERT INTO [dbo].[Class]  ([OtClass], [NumClass], [MainTeacherID], [TotalName]) VALUES ('" + ClassOt + "', '" + ClassNum + "',null,'" + ClassOt + ClassNum + "')";
        return ExecuteNonQuery(cStr);
    }

    public string IsAlreadyLogin(string UserID, string password)
    {
        String selectSTR = "select alreadyLogin from Users where UserID = '" + UserID + "' and LoginPassword = '" + password + "'";
        string isAlreadyLogin = "";
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                isAlreadyLogin = dr["alreadyLogin"].ToString();
            }
            return isAlreadyLogin;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public List<Questions> GetQuestions()
    {
        List<Questions> questions = new List<Questions>();

        String selectSTR = "select * from SecurityQ";
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                Questions q = new Questions();
                q.SecurityCode = int.Parse(dr["CodeSecurityQ"].ToString());
                q.SecurityInfo = dr["SecurityQInfo"].ToString();
                questions.Add(q);
            }
            return questions;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public int SaveQuestion(string id, int q1, string a1, int q2, string a2)
    {
        String cStr = "update Users set SecurityQ1Code = " + q1 + ", SecurityQ1Answer = '" + a1 + "', SecurityQ2Code=" + q2 + ",SecurityQ2Answer='" + a2 + "'  where UserID = '" + id + "'";
        return ExecuteNonQuery(cStr); // execute the command   
    }

    public int AddUser(Users NewUser)
    {
        string cStr = "INSERT INTO [dbo].[Users] ([UserID],[UserFName],[UserLName],[BirthDate],[UserImg],[LoginName],[LoginPassword],[PhoneNumber],[CodeUserType],[SecurityQ1Code],[SecurityQ1Answer],[alreadyLogin],[SecurityQ2Code],[SecurityQ2Answer])" +
                     " VALUES('" + NewUser.UserID1 + "','" + NewUser.UserFName1 + "','" + NewUser.UserLName1 + "','" + NewUser.BirthDate1 + "','" + NewUser.UserImg1 + "','" + NewUser.UserName1 + "','" + NewUser.UserPassword1 + "','" + NewUser.PhoneNumber1 + "','" + NewUser.CodeUserType1 + "' , null, null, 0, null, null)";
        return ExecuteNonQuery(cStr);
    }

    public int InsertTimeTable(List<Dictionary<string, string>> matrix, string classCode)
    {
        string cStr;
        int num = 0;
        //check empty cells.

        cStr = "INSERT INTO [dbo].[Timetable] (ClassCode) values (" + classCode + ")";
        ExecuteNonQuery(cStr);

        for (int i = 0; i < matrix.Count; i++)
        {
            SqlConnection conn = connect("Betsefer");

            if (matrix[i]["classCode"] != "empty")
            {
                int TimeTableCode = GetLastTimeTableCode();
                int CodeWeekDay = int.Parse(matrix[i]["CodeWeekDay"]);
                int ClassTimeCode = int.Parse(matrix[i]["ClassTimeCode"]);
                int CodeLesson = int.Parse(matrix[i]["CodeLesson"]);
                string TeacherId = matrix[i]["TeacherID"];

                cStr = "INSERT INTO [dbo].[TimetableLesson] (TimeTableCode, CodeWeekDay, ClassTimeCode, CodeLesson, TeacherId) values (" + TimeTableCode + ", " + CodeWeekDay + ", " + ClassTimeCode + ", " + CodeLesson + ", '" + TeacherId + "')";
                num = ExecuteNonQuery(cStr);
            }
        }
        return num;
    }

    public int GetLastTimeTableCode()
    {
        int TTC = 0;
        String cStr = "select max(TimeTableCode) from dbo.TimeTable";
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(cStr, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                TTC = int.Parse(dr[0].ToString());
            }
            return TTC;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public int UpdateUser(string userID, string userFName, string userLName, string birthDate, string userImg, string userName, string userPassword, string phoneNumber)
    {
        string cStr;
        if (userImg == "")
        {
            cStr = "Update Users set [UserID]='" + userID + "',[UserFName]='" + userFName + "',[UserLName]='" + userLName + "',[BirthDate]='" + birthDate + "',[LoginName]='" + userName + "',[LoginPassword]='" + userPassword + "',[PhoneNumber]='" + phoneNumber + "' where [UserID]='" + userID + "'";
        }
        else
        {
            cStr = "Update Users set [UserID]='" + userID + "',[UserFName]='" + userFName + "',[UserLName]='" + userLName + "',[BirthDate]='" + birthDate + "',[UserImg]='" + userImg + "',[LoginName]='" + userName + "',[LoginPassword]='" + userPassword + "',[PhoneNumber]='" + phoneNumber + "' where [UserID]='" + userID + "'";
        }
        return ExecuteNonQuery(cStr); // execute the command   
    }

    public int AddPupil(string UserID, int classNumber)
    {
        String cStr = "INSERT INTO [dbo].[Pupil]([UserID],[CodeClass])  VALUES ('" + UserID + "'," + classNumber + ")";
        return ExecuteNonQuery(cStr);
    }

    public string GetNumChild(string UserID)
    {
        String cStr = "select count([ParentID]) as num from [dbo].[PupilsParent] where [ParentID]='" + UserID + "'";
        string NumChilds = "";
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(cStr, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                NumChilds = dr["num"].ToString();
            }
            return NumChilds;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public int UpdatePupil(string userID, string ClassOt)
    {
        string cStr = "UPDATE[dbo].[Pupil] set [CodeClass]='" + ClassOt + "' where [UserID]='" + userID + "'";
        return ExecuteNonQuery(cStr);
    }

    public int AddTeacher(string UserID, string IsMain)
    {
        string cStr = "INSERT INTO [dbo].[Teachers] ([TeacherID] ,[IsMainTeacher]) VALUES ('" + UserID + "' ,'" + IsMain + "')";
        return ExecuteNonQuery(cStr);
    }

    public int UpdateTeacher(string UserID, string IsMain)
    {
        string cStr = "UPDATE [dbo].[Teachers]  SET [IsMainTeacher] = '" + IsMain + "' where [TeacherID]='" + UserID + "'";
        return ExecuteNonQuery(cStr);
    }

    public int UpdateClassTeacher(string UserID, string ClassOt)
    {
        string cStr = "UPDATE [dbo].[Class] SET [MainTeacherID] = '" + UserID + "' where [TotalName]='" + ClassOt + "'";
        return ExecuteNonQuery(cStr);
    }

    public int AddParent(string ParentID, string PupilID, string ChildCodeClass)
    {
        string cStr = "INSERT INTO [dbo].[PupilsParent] ([ParentID] ,[PupilID],[codeClass]) VALUES ('" + ParentID + "' ,'" + PupilID + "'," + ChildCodeClass + ")";
        return ExecuteNonQuery(cStr);
    }

    public int UpdateParent(string ParentID, string PupilID, string ChildCodeClass)
    {
        string cStr = "INSERT INTO [dbo].[PupilsParent] ([ParentID] ,[PupilID],[codeClass]) VALUES ('" + ParentID + "' ,'" + PupilID + "'," + ChildCodeClass + ")";
        return ExecuteNonQuery(cStr);
    }

    public int ChangeFirstLogin(string id)
    {
        string cStr = "update Users set alreadyLogin = 1  where UserID = '" + id + "'";
        return ExecuteNonQuery(cStr);
    }

    public int AddMainTeacherToClass(string id, string OtClass)
    {
        string cStr = "update Class set MainTeacherID = '" + id + "'  where TotalName = '" + OtClass + "'";
        return ExecuteNonQuery(cStr);
    }

    public int DeleteMainTeacherToClass(string TotalClassName)
    {
        string DeletePrevieusClassTeacher = "update Class set MainTeacherID = null where TotalName = '" + TotalClassName + "'";
        return ExecuteNonQuery(DeletePrevieusClassTeacher);
    }

    public List<string> IsAlreadyMainTeacher(string id)
    {
        String cStr = "select [TotalName] from Class where MainTeacherID= '" + id + "'";
        string ClassOt;
        List<string> l = new List<string>();
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(cStr, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                ClassOt = dr["TotalName"].ToString();
                l.Add(ClassOt);
            }
            return l;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public Dictionary<string, string> getPupils(string classCode)
    {
        String selectSTR = "SELECT   dbo.Users.UserID,(dbo.Users.UserLName + ' ' + dbo.Users.UserFName)AS PupilName" +
           "  FROM dbo.Pupil INNER JOIN   dbo.Users ON dbo.Pupil.UserID = dbo.Users.UserID   where dbo.Pupil.CodeClass='" + classCode + "'";
        string UserID, UserName;
        Dictionary<string, string> l = new Dictionary<string, string>();
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            l.Add("1", "בחר תלמיד");
            while (dr.Read())
            {
                UserID = dr["UserID"].ToString();
                UserName = dr["PupilName"].ToString();
                l.Add(UserID, UserName);
            }
            return l;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public List<Dictionary<string, string>> getPupilsByClassCode(string classCode)
    {
        String selectSTR = "SELECT   dbo.Users.UserID,(dbo.Users.UserLName + ' ' + dbo.Users.UserFName)AS PupilName" +
           "  FROM dbo.Pupil INNER JOIN   dbo.Users ON dbo.Pupil.UserID = dbo.Users.UserID   where dbo.Pupil.CodeClass='" + classCode + "'";
        List<Dictionary<string, string>> l = new List<Dictionary<string, string>>();
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p["UserId"] = dr["UserID"].ToString();
                p["UserName"] = dr["PupilName"].ToString();

                l.Add(p);
            }
            return l;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public Dictionary<string, string> GetTeachers()
    {
        String selectSTR = "SELECT UserID, UserFName + ' ' + UserLName AS FullName FROM Users WHERE (CodeUserType = 2)";
        string UserID, TeacherFullName;
        Dictionary<string, string> l = new Dictionary<string, string>();
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            l.Add("0", "-");
            while (dr.Read())
            {
                UserID = dr["UserID"].ToString();
                TeacherFullName = dr["FullName"].ToString();
                l.Add(UserID, TeacherFullName);
            }
            return l;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public Dictionary<string, string> FillUsers(string CodeUserType)
    {
        String selectSTR = "SELECT(UserLName+' '+ UserFName) as UserName, UserID" +
           " FROM dbo.Users where CodeUserType='" + CodeUserType + "'";
        string UserID, UserName;
        Dictionary<string, string> l = new Dictionary<string, string>();
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            l.Add("1", "בחר משתמש");
            while (dr.Read())
            {
                UserID = dr["UserID"].ToString();
                UserName = dr["UserName"].ToString();
                l.Add(UserID, UserName);
            }
            return l;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);

        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public Dictionary<int, string> GetSubjects()
    {
        String selectSTR = "SELECT DISTINCT * FROM [Lessons] ORDER BY [LessonName]";
        int SubjectID;
        string SubjectName;
        Dictionary<int, string> l = new Dictionary<int, string>();
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            l.Add(0, "-");
            while (dr.Read())
            {
                SubjectID = int.Parse(dr["CodeLesson"].ToString());
                SubjectName = dr["LessonName"].ToString();
                l.Add(SubjectID, SubjectName);
            }
            return l;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public List<string> getSubjectsByPupilId(string Id)
    {
        String selectSTR = "SELECT dbo.TimetableLesson.CodeLesson, dbo.Lessons.LessonName " +
            "FROM dbo.Timetable INNER JOIN dbo.Class ON dbo.Timetable.ClassCode = dbo.Class.ClassCode INNER JOIN " +
            "dbo.Pupil ON dbo.Class.ClassCode = dbo.Pupil.CodeClass INNER JOIN dbo.TimetableLesson ON " +
            "dbo.Timetable.TimeTableCode = dbo.TimetableLesson.TimeTableCode INNER JOIN dbo.Lessons ON " +
            "dbo.TimetableLesson.CodeLesson = dbo.Lessons.CodeLesson where dbo.Pupil.UserID = '" + Id + "'";

        List<string> l = new List<string>();
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                string SubjectName = dr["LessonName"].ToString();
                l.Add(SubjectName);
            }
            return l;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string GetTeacherNameByID(string TeacherId)
    {
        String selectSTR = "SELECT UserFName + ' ' + UserLName FROM Users where UserId = '" + TeacherId + "'";
        string teacherName = "";

        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                teacherName = dr[0].ToString();
            }

            return teacherName;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public Dictionary<string, string> FillClassOt()
    {
        String selectSTR = "SELECT ClassCode,TotalName FROM Class ";
        string ClassCode, TotalName;
        Dictionary<string, string> l = new Dictionary<string, string>();
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            l.Add("0", "בחר");
            while (dr.Read())
            {
                ClassCode = dr["ClassCode"].ToString();
                TotalName = dr["TotalName"].ToString();
                l.Add(ClassCode, TotalName);
            }
            return l;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    //--------------------------------------------------------------------------------------------------
    // This method returns number of rows affected
    //--------------------------------------------------------------------------------------------------
    public int ExecuteNonQuery(string str)
    {
        SqlCommand cmd;
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        try
        {
            cmd = CreateCommand(str, con);
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public List<Dictionary<string, string>> GetTimeTableAcordingToClassCode(int classCode) //webService
    {
        //keep just one time table for a class. no history.
        List<Dictionary<string, string>> TT = new List<Dictionary<string, string>>();
        SqlCommand cmd; string cStr;
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            cStr = "select [dbo].[TimetableLesson].TimeTableCode, [dbo].[TimetableLesson].CodeWeekDay, [dbo].[TimetableLesson].ClassTimeCode, [dbo].[TimetableLesson].CodeLesson, [dbo].[TimetableLesson].TeacherId from [dbo].[TimetableLesson] inner join[dbo].[Timetable] on[dbo].[TimetableLesson].TimeTableCode = [dbo].[Timetable].TimeTableCode where[dbo].[Timetable].ClassCode = " + classCode;
            cmd = CreateCommand(cStr, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                Dictionary<string, string> lesson = new Dictionary<string, string>();
                lesson.Add("TimeTableCode", dr["TimeTableCode"].ToString());
                lesson.Add("CodeWeekDay", dr["CodeWeekDay"].ToString());
                lesson.Add("ClassTimeCode", dr["ClassTimeCode"].ToString());
                lesson.Add("CodeLesson", dr["CodeLesson"].ToString());
                lesson.Add("TeacherId", dr["TeacherId"].ToString());

                TT.Add(lesson);
            }
            return TT;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public List<Dictionary<string, string>> GetTimeTableAcordingToClassCodeForMobile(int classCode) //webService
    {
        //keep just one time table for a class. no history.
        List<Dictionary<string, string>> TT = new List<Dictionary<string, string>>();
        SqlCommand cmd; string cStr;
        cStr = "select [dbo].[TimetableLesson].TimeTableCode, [dbo].[TimetableLesson].CodeWeekDay, [dbo].[TimetableLesson].ClassTimeCode, [dbo].[TimetableLesson].CodeLesson, [dbo].[TimetableLesson].TeacherId from [dbo].[TimetableLesson] inner join[dbo].[Timetable] on[dbo].[TimetableLesson].TimeTableCode = [dbo].[Timetable].TimeTableCode where[dbo].[Timetable].ClassCode = " + classCode;
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            cmd = CreateCommand(cStr, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                Dictionary<string, string> lesson = new Dictionary<string, string>();
                lesson.Add("TimeTableCode", dr["TimeTableCode"].ToString());
                lesson.Add("CodeWeekDay", dr["CodeWeekDay"].ToString());
                lesson.Add("ClassTimeCode", dr["ClassTimeCode"].ToString());
                string subjectCode = dr["CodeLesson"].ToString();
                Subject s = new Subject();
                //המרה של הקוד מקצוע לשם מקצוע
                string subjectName = s.GetSubjectNameBySubjectCode(subjectCode);
                lesson.Add("CodeLesson", subjectName);
                string teacherId = dr["TeacherId"].ToString();
                Users t = new Users();
                //המרה של הת.ז. מורה לשם מורה
                string teacherName = t.GetUserFullNameByID(teacherId);
                lesson.Add("TeacherId", teacherName);
                TT.Add(lesson);
            }
            return TT;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string GetSubjectNameBySubjectCode(string subjectCode)
    {
        SqlCommand cmd; string cStr, lessonName = "";
        cStr = "select LessonName from Lessons where CodeLesson = " + int.Parse(subjectCode);
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            cmd = CreateCommand(cStr, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                lessonName = dr[0].ToString();
            }
            return lessonName;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public List<Dictionary<string, string>> GetValuesTimeTableAcordingToClassCode(int classCode)
    {
        List<Dictionary<string, string>> TT = new List<Dictionary<string, string>>();
        SqlCommand cmd; string cStr;
        cStr = "select [dbo].[TimetableLesson].TimeTableCode, [dbo].[TimetableLesson].CodeWeekDay, [dbo].[TimetableLesson].ClassTimeCode, [dbo].[TimetableLesson].CodeLesson, [dbo].[TimetableLesson].TeacherId from [dbo].[TimetableLesson] inner join[dbo].[Timetable] on[dbo].[TimetableLesson].TimeTableCode = [dbo].[Timetable].TimeTableCode where[dbo].[Timetable].ClassCode = " + classCode;
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            cmd = CreateCommand(cStr, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                Dictionary<string, string> lesson = new Dictionary<string, string>();
                lesson.Add("TimeTableCode", dr["TimeTableCode"].ToString());
                lesson.Add("CodeWeekDay", dr["CodeWeekDay"].ToString());
                lesson.Add("ClassTimeCode", dr["ClassTimeCode"].ToString());
                lesson.Add("CodeLesson", dr["CodeLesson"].ToString());
                lesson.Add("TeacherId", dr["TeacherId"].ToString());

                TT.Add(lesson);
            }
            return TT;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public bool IsClassHasTimeTable(string classCode)
    {
        int num = 0;
        String selectSTR = "SELECT count(TimeTableCode) FROM Timetable where ClassCode = " + classCode;
        bool ans = false;
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                num = int.Parse(dr[0].ToString());
            }

            if (num > 0)
            {
                ans = true;
            }
            return ans;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public int DeleteTimeTableLessons(string classCode)
    {
        String selectSTR = "DELETE T2 FROM dbo.TimetableLesson as T2 INNER JOIN dbo.Timetable as T1 ON T2.TimeTableCode = T1.TimeTableCode where T1.ClassCode = " + classCode;
        return ExecuteNonQuery(selectSTR);
    }

    public int DeleteTimeTable(string classCode)
    {
        String selectSTR = "DELETE FROM dbo.Timetable where ClassCode = " + classCode;
        return ExecuteNonQuery(selectSTR);
    }

    public string GetLessonNameByLessonCode(string lessonCode)
    {
        String selectSTR = "SELECT LessonName FROM Lessons where CodeLesson = " + lessonCode;
        string lessonName = "";
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                lessonName = dr[0].ToString();
            }
            return lessonName;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }
    public Dictionary<string, string> GetSubjectsByClassCode(string classCode)
    {
        String selectSTR = "SELECT distinct dbo.Lessons.CodeLesson, dbo.Lessons.LessonName FROM dbo.Timetable " +
            "INNER JOIN dbo.Class ON dbo.Timetable.ClassCode = dbo.Class.ClassCode AND " +
            "dbo.Timetable.ClassCode = dbo.Class.ClassCode INNER JOIN dbo.TimetableLesson ON " +
            "dbo.Timetable.TimeTableCode = dbo.TimetableLesson.TimeTableCode INNER JOIN dbo.Lessons " +
            "ON dbo.TimetableLesson.CodeLesson = dbo.Lessons.CodeLesson where dbo.Class.ClassCode = " + classCode;
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            Dictionary<string, string> d = new Dictionary<string, string>();
            while (dr.Read())
            {
                string lessonCode = dr["CodeLesson"].ToString();
                string lessonName = dr["LessonName"].ToString();
                d.Add(lessonCode, lessonName);
            }
            return d;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string GetUserFullNameByID(string TeacherId)
    {
        String selectSTR = "SELECT UserFName + ' ' + UserLName FROM Users where UserId = '" + TeacherId + "'";
        string Name = "";
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                Name = dr[0].ToString();
            }
            return Name;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public List<string> GetPupilIdByParentId(string UserId)
    {
        String selectSTR = "SELECT dbo.PupilsParent.PupilID FROM dbo.UserType INNER JOIN " +
                         "dbo.Users ON dbo.UserType.CodeUserType = dbo.Users.CodeUserType INNER JOIN " +
                         "dbo.PupilsParent ON dbo.Users.UserID = dbo.PupilsParent.ParentID " +
                         "where dbo.PupilsParent.ParentID ='" + UserId + "'";
        List<string> PupilId = new List<string>();
        try
        {
            con = connect("Betsefer"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        try
        {
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                PupilId.Add(dr[0].ToString());
            }
            return PupilId;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    //public List<Dictionary<string, string>> GetPupilsByParentId(string parentID)
    //{
    //    List<Dictionary<string, string>> l = new List<Dictionary<string, string>>();

    //    String selectSTR = "SELECT dbo.PupilsParent.PupilID, dbo.Users.UserFName + ' ' + dbo.Users.UserLName as FullName FROM dbo.UserType INNER JOIN " +
    //                    "dbo.Users ON dbo.UserType.CodeUserType = dbo.Users.CodeUserType INNER JOIN " +
    //                    "dbo.PupilsParent ON dbo.Users.UserID = dbo.PupilsParent.ParentID " +
    //                    "where dbo.PupilsParent.ParentID ='" + parentID + "'";
    //    try
    //    {
    //        con = connect("Betsefer"); // create the connection
    //    }
    //    catch (Exception ex)
    //    {
    //        // write to log
    //        throw (ex);
    //    }
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand(selectSTR, con);
    //        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

    //        while (dr.Read())
    //        {
    //            Dictionary<string, string> d = new Dictionary<string, string>();
    //            d.Add(dr["PupilID"].ToString(), dr["FullName"].ToString());
    //            l.Add(d);
    //        }
    //        return l;
    //    }
    //    catch (Exception ex)
    //    {
    //        // write to log
    //        throw (ex);
    //    }
    //    finally
    //    {
    //        if (con != null)
    //        {
    //            con.Close();
    //        }
    //    }
    //} 
}