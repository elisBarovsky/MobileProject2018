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

    public int DeleteChild(string parentID, string childID)
    {
        String selectSTR = "DELETE FROM dbo.PupilsParent where [ParentID] = '" + parentID + "' and [PupilID] = '" + childID + "'";
        return ExecuteNonQuery(selectSTR);

    }


    public string GetClassCodeAccordingToClassFullName(string classTotalName)
    {
        String selectSTR = "SELECT ClassCode FROM Class where TotalName  = '" + classTotalName + "'";
        string codeClass = "";
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
                codeClass = dr["ClassCode"].ToString();
            }
            return codeClass;
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

    public List<Dictionary<string, string>> GetMessagesByUserId(string userId)
    {
        string type = GetUserTypeById(userId), selectSTR = "SELECT MessageCode, MessageDate, SenderID, " +
            " TheMessage, SubjectMessage FROM Messages where recipientID  = '" + userId +
            "' order by MessageCode desc";
        if (type == "3")
        {

        }

        List<Dictionary<string, string>> messages = new List<Dictionary<string, string>>();

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
                Dictionary<string, string> d = new Dictionary<string, string>();

                d.Add("MessageCode", dr["MessageCode"].ToString());
                d.Add("MessageDate", dr["MessageDate"].ToString());
                d.Add("SenderID", dr["SenderID"].ToString());
                //d.Add("SenderName", dr["SenderName"].ToString());
                d.Add("SubjectMessage", dr["SubjectMessage"].ToString());
                d.Add("TheMessage", dr["TheMessage"].ToString());

                messages.Add(d);
            }
            string SenderName;

            for (int i = 0; i < messages.Count; i++)
            {
                SenderName = GetSenderNameBySenderID(messages[i]["SenderID"]);
                messages[i].Add("SenderName", SenderName);
            }

            return messages;
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



    public string GetUserFullName(string Id)
    {
        string selectSTR = "SELECT UserFName + ' ' + UserLName as UserName " +
            " FROM Users where UserID  = '" + Id + "'",
            UserName = "";

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
                UserName = dr["UserName"].ToString();
            }

            return UserName;
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

    public string GetSenderNameBySenderID(string SenderID)
    {
        string selectSTR = "select UserFName + ' ' + UserLName as SenderName from Users where UserID = '" + SenderID + "'";

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
            string SenderName = "";
            while (dr.Read())
            {
                SenderName = dr["SenderName"].ToString();
            }
            return SenderName;
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

    public string GetSenderImgBySenderID(string SenderID)
    {
        string selectSTR = "select UserImg from Users where UserID = '" + SenderID + "'";

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
            string SenderIMG = "";
            while (dr.Read())
            {
                SenderIMG = dr["UserImg"].ToString();
            }
            return SenderIMG;
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

    public List<Messages> GetAllConversation(string SenderID, string RecipientID)
    {
        string selectSTR = "SELECT MessageCode, MessageDate, SenderID, " +
         " recipientID, (select UserFName + ' ' + UserLName from Users where UserID = '" + RecipientID + "') as RecipientName, " +
         " TheMessage, SubjectMessage FROM Messages where SenderID  = '" + SenderID + "' and recipientID = '" + RecipientID +
         "' or SenderID  = '" + RecipientID + "' and recipientID = '" + SenderID + "' order by MessageCode"; ;

        List<Messages> messages = new List<Messages>();

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
                Messages m = new Messages();

                m.MessageDate = dr["MessageDate"].ToString();
                m.SenderID = dr["SenderID"].ToString();
                m.RecipientID = dr["recipientID"].ToString();
                //m.SenderName = dr["SenderName"].ToString();
                m.RecipientName = dr["RecipientName"].ToString();
                m.Subject = dr["SubjectMessage"].ToString();
                m.Content = dr["TheMessage"].ToString();

                messages.Add(m);
            }

            string SenderName, SenderIMG;

            for (int i = 0; i < messages.Count; i++)
            {
                SenderName = GetSenderNameBySenderID(messages[i].SenderID);
                SenderIMG = GetSenderImgBySenderID(messages[i].SenderID);

                messages[i].SenderName = SenderName;
                messages[i].SenderIMG = SenderIMG;

            }
            return messages;
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

    public string GetUserTypeById(string UserID)
    {
        String selectSTR = "SELECT CodeUserType  FROM Users where UserID  = '" + UserID + "'";
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

    public List<string> GetClassesFullName()
    {
        String selectSTR = "select distinct [TotalName], [OtClass], [NumClass] from [dbo].[Class] order by OtClass, NumClass";
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

    public int UpdateStatusTT(string classCode, bool publish)
    {
        String cStr = "UPDATE [dbo].[Timetable]  SET [IsPublish] ='" + publish + "'WHERE [ClassCode]=" + classCode;
        return ExecuteNonQuery(cStr); // execute the command   
    }

    public int AddUser(Users NewUser)
    {
        string cStr = "INSERT INTO [dbo].[Users] ([UserID],[UserFName],[UserLName],[BirthDate],[UserImg],[LoginName],[LoginPassword],[PhoneNumber],[CodeUserType],[SecurityQ1Code],[SecurityQ1Answer],[alreadyLogin],[SecurityQ2Code],[SecurityQ2Answer])" +
                     " VALUES('" + NewUser.UserID1 + "','" + NewUser.UserFName1 + "','" + NewUser.UserLName1 + "','" + NewUser.BirthDate1 + "','" + NewUser.UserImg1 + "','" + NewUser.UserName1 + "','" + NewUser.UserPassword1 + "','" + NewUser.PhoneNumber1 + "','" + NewUser.CodeUserType1 + "' , null, null, 0, null, null)";
        return ExecuteNonQuery(cStr);
    }

    public int InsertTimeTable(string date, int classCode, bool publish)
    {
        int num = 0;
        try
        {
            using (var con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Betsefer"].ConnectionString))
            {
                using (var cmd = new SqlCommand("UpdateTimeTable", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@classCode", classCode);
                    cmd.Parameters.AddWithValue("@publish", publish);
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                        }
                        num = 1;
                    }
                }
            }
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
        return num;
    }

    public int InsertTempTimeTable(string date, int CodeWeekDay, int ClassTimeCode, int CodeLesson, string TeacherId, int ClassNum)
    {
        string cStr;
        int num = 0;

        cStr = "INSERT INTO [dbo].[TempTimetableLesson] ([dateString],[CodeWeekDay],[ClassTimeCode],[CodeLesson],[TeacherId],[CodeChoosenClass]) values ('" + date + "'," + CodeWeekDay + "," + ClassTimeCode + "," + CodeLesson + ",'" + TeacherId + "'," + ClassNum + ")";

        num = ExecuteNonQuery(cStr);

        return num;
    }

    public int InsertUpdateTimeTable(string TimeTableCode, int CodeWeekDay, int ClassTimeCode, int CodeLesson, string TeacherId)
    {
        string cStr;
        int num = 0;

        cStr = "INSERT INTO [dbo].[TimetableLesson] ([TimeTableCode],[CodeWeekDay],[ClassTimeCode],[CodeLesson],[TeacherId]) values ('" + TimeTableCode + "'," + CodeWeekDay + "," + ClassTimeCode + "," + CodeLesson + ",'" + TeacherId + "')";

        num = ExecuteNonQuery(cStr);

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

    public List<string> GetCellInfoUPDATE(string TableCode, int WeekDay, int LessonNum)
    {
        String cStr = "select ([UserFName]+' '+[UserLName]) as FullName from [dbo].[Users] where [UserID]=(select  [TeacherId] from [dbo].[TimetableLesson] where [TimeTableCode] ='" + TableCode + "' and [CodeWeekDay]=" + WeekDay + " and [ClassTimeCode]=" + LessonNum + ") union " +
                        "select [LessonName] from [dbo].[Lessons] where [CodeLesson]=(select CodeLesson from [dbo].[TimetableLesson] where [TimeTableCode] ='" + TableCode + "' and [CodeWeekDay]=" + WeekDay + " and [ClassTimeCode]=" + LessonNum + ") ";
        List<string> listInfo = new List<string>();
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
                string info = dr["FullName"].ToString();
                listInfo.Add(info);
            }
            return listInfo;
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

    public List<string> GetCellInfo(string date, int WeekDay, int LessonNum, string ClassNum)
    {
        String cStr = "select ([UserFName]+' '+[UserLName]) as FullName from [dbo].[Users] where [UserID]=(select  [TeacherId] from [dbo].[TempTimetableLesson] where [dateString] ='" + date + "' and [CodeWeekDay]=" + WeekDay + " and [ClassTimeCode]=" + LessonNum + "and CodeChoosenClass='" + ClassNum + "') union " +
                        "select [LessonName] from [dbo].[Lessons] where [CodeLesson]=(select CodeLesson from [dbo].[TempTimetableLesson] where [dateString] ='" + date + "' and [CodeWeekDay]=" + WeekDay + " and [ClassTimeCode]=" + LessonNum + " and CodeChoosenClass='" + ClassNum + "')";
        List<string> listInfo = new List<string>();
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
                string info = dr["FullName"].ToString();
                listInfo.Add(info);
            }
            return listInfo;
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

    public List<string> getPupilsIdByClassCode(string classCode)
    {

        String selectSTR = "SELECT   dbo.Users.UserID FROM dbo.Pupil INNER JOIN " +
            "dbo.Users ON dbo.Pupil.UserID = dbo.Users.UserID   where dbo.Pupil.CodeClass='" + classCode + "'";

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
                l.Add(dr["UserID"].ToString());
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

    public List<Dictionary<string, string>> GetTeachers2()
    {
        String selectSTR = "SELECT UserID, UserFName + ' ' + UserLName AS FullName FROM Users WHERE (CodeUserType = 2)";
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
                Dictionary<string, string> d = new Dictionary<string, string>();
                d.Add("UserId", dr["UserID"].ToString());
                d.Add("FullName", dr["FullName"].ToString());
                l.Add(d);
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

    public List<string> GetTeachersIds()
    {
        String selectSTR = "SELECT UserID FROM Users WHERE (CodeUserType = 2)";
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
                l.Add(dr["UserID"].ToString());
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

    public int DeleteTimeTable(string classCode) //
    {
        String selectSTR = "DELETE FROM dbo.Timetable where ClassCode = " + classCode;
        return ExecuteNonQuery(selectSTR);
    }

    public int DeleteTempTT(string date, string classcode)
    {
        String selectSTR = "DELETE FROM dbo.TempTimetableLesson where [dateString] = '" + date + "'[CodeChoosenClass] = " + classcode;
        return ExecuteNonQuery(selectSTR);
    }

    public int DeleteTT(string classCode)
    {
        int num = 0;
        try
        {
            using (var con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Betsefer"].ConnectionString))
            {
                using (var cmd = new SqlCommand("DeleteTempTT", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@classCode", classCode);
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                        }
                        num = 1;
                    }
                }
            }
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
        return num;
    }

    public string GetCellInfoUPDATECodeTable(string ClassCode)
    {
        String selectSTR = "select [TimeTableCode]  from [dbo].[Timetable] where [ClassCode]=" + ClassCode;
        string TableCode = "";
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
                TableCode = dr[0].ToString();
            }
            return TableCode;
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

    public List<string> GetParentsIdsByPupilId(string pupilID)
    {
        List<string> parents = new List<string>();

        String selectSTR = "SELECT dbo.PupilsParent.ParentID FROM dbo.UserType INNER JOIN " +
                            "dbo.Users ON dbo.UserType.CodeUserType = dbo.Users.CodeUserType INNER JOIN " +
                            "dbo.PupilsParent ON dbo.Users.UserID = dbo.PupilsParent.ParentID " +
                            "where dbo.PupilsParent.PupilID = '" + pupilID + "'";

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
                parents.Add(dr[0].ToString());
            }
            return parents;
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

    public List<Dictionary<string, string>> getParentsByClassCode(string classCode)
    {
        List<Dictionary<string, string>> parents = new List<Dictionary<string, string>>();

        String selectSTR = "SELECT UserID, (dbo.Users.UserFName+' '+ dbo.Users.UserLName) as 'FullName'" +
                               " FROM dbo.PupilsParent INNER JOIN dbo.Users ON dbo.PupilsParent.ParentID = dbo.Users.UserID" +
                               " where dbo.PupilsParent.codeClass = '" + classCode + "'";
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
                Dictionary<string, string> d = new Dictionary<string, string>();
                d.Add("UserId", dr["UserID"].ToString());
                d.Add("FullName", dr["FullName"].ToString());
                parents.Add(d);
            }
            return parents;
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

    public List<string> getParentsIdByClassCode(string classCode)
    {
        List<string> parents = new List<string>();

        String selectSTR = "SELECT UserID FROM dbo.PupilsParent INNER JOIN dbo.Users ON dbo.PupilsParent.ParentID =" +
            " dbo.Users.UserID where dbo.PupilsParent.codeClass = '" + classCode + "'";
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
                parents.Add(dr["UserID"].ToString());
            }
            return parents;
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

    public string IsStudentUserNotThisParentYet(string childID, string parentID)
    {
        String STRuserType = "SELECT CodeUserType FROM dbo.Users where UserID = '" + childID + "'",
            STRcheckIfConnectionExists = "SELECT count(ParentID) from PupilsParent where ParentID = '" + parentID +
            "' and PupilID = '" + childID + "'",
            codeUserType = "", answer = "";
        int numConnections = -1;

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
            SqlCommand cmd = new SqlCommand(STRuserType, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                codeUserType = dr[0].ToString();
            }

            if (codeUserType == "4")
            {
                try
                {
                    cmd = new SqlCommand(STRcheckIfConnectionExists, con);
                    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (dr.Read())
                    {
                        numConnections = int.Parse(dr[0].ToString());
                    }
                }
                catch (Exception ex)
                {

                    throw (ex);
                }
            }
            else answer = "userTypeNotStudent";

            if (numConnections == 0)
            {
                answer = "everythingGood";
            }
            else if (numConnections > 0)
            {
                answer = "connectionExists";
            }
            return answer;
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

    public int SaveChildAndParent(string parentID, string childID)
    {
        string codeClass = GetPupilOtClass(childID);
        String cStr = "INSERT INTO [dbo].[PupilsParent] VALUES ('" + parentID + "', '" + childID + "', '" + codeClass + "')";
        return ExecuteNonQuery(cStr);
    }

    public int SendPrivateMessage(string SenderID, string RecieientID, string Subject, string content)
    {
        String cStr = "INSERT INTO [dbo].[Messages] (MessageDate, SenderID, recipientID, TheMessage, SubjectMessage)" +
            " VALUES ('" + DateTime.Now + "', '" + SenderID + "', '" + RecieientID + "', '" + content + "', '" + Subject + "')";
        return ExecuteNonQuery(cStr);
    }

    public int SendKolektiveMessage(string SenderID, string userClass, string userType, string Subject, string content)
    {
        List<string> usersIds = new List<string>();
        String cStr = "";
        switch (userType)
        {
            case "pupils":
                usersIds = getPupilsIdByClassCode(userClass);
                break;
            case "parents":
                usersIds = getParentsIdByClassCode(userClass);
                break;
            case "teachers":
                usersIds = GetTeachersIds();
                break;
        }

        for (int i = 0; i < usersIds.Count; i++)
        {
            if (SenderID != usersIds[i])
            {
                cStr += "INSERT INTO [dbo].[Messages] (MessageDate, SenderID, recipientID, TheMessage, SubjectMessage) VALUES ('" + DateTime.Now + "','" + SenderID + "', '" + usersIds[i] +
            "', '" + content + "','" + Subject + "')";
            }
        }

        return ExecuteNonQuery(cStr);
    }

    public string GetUserImgByUserID(string UserID)
    {
        String selectSTR = "SELECT UserImg FROM dbo.Users where UserID = '" + UserID + "'";
        string UserImg = "";
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
                UserImg = dr["UserImg"].ToString();
            }
            return UserImg;
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
}