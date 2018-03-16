using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Summary description for DBconnectionTeacher
/// </summary>
public class DBconnectionTeacher
{
    public SqlDataAdapter da;
    public DataTable dt;
    SqlConnection con = new SqlConnection();

    public DBconnectionTeacher()
    {
    }

    public SqlConnection connect(String conString)  // read the connection string from the configuration file
    {
        string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object
        cmd.Connection = con;              // assign the connection to the command object
        cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 
        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds
        cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure
        return cmd;
    }

    public DataTable PupilList(string ClassOtID)
    {
        string selectSTR = " SELECT dbo.Pupil.UserID, (dbo.Users.UserFName +' '+ dbo.Users.UserLName) as PupilName, dbo.Grades.Grade" +
                        " FROM dbo.Pupil INNER JOIN dbo.Users ON dbo.Pupil.UserID = dbo.Users.UserID Full outer JOIN dbo.Grades ON dbo.Users.UserID = dbo.Grades.PupilID where dbo.Pupil.CodeClass = '" + ClassOtID + "'";
        DataTable dtt = new DataTable();
        DataSet ds ;
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
            SqlDataAdapter daa = new SqlDataAdapter(selectSTR, con);
            ds = new DataSet("PupilsDS");
            daa.Fill(ds);
            return  dtt = ds.Tables[0];
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

    public DataTable FilterNotes(string FilterType,string ValueFilter)
    {
        string selectSTR = " SELECT  dbo.GivenNotes.Comment AS 'הערת מורה', dbo.GivenNotes.NoteDate AS 'תאריך', dbo.Lessons.LessonName AS 'שיעור', dbo.NoteType.NoteName AS 'הערת משמעת', (dbo.Users.UserFName +' '+ dbo.Users.UserLName) as 'שם תלמיד' ,dbo.Pupil.UserID as 'תעודת זהות תלמיד'" +
                          " FROM  dbo.Users inner JOIN dbo.Pupil ON dbo.Users.UserID = dbo.Pupil.UserID inner JOIN dbo.GivenNotes " +
                          "ON dbo.Users.UserID = dbo.GivenNotes.PupilID  inner JOIN dbo.NoteType ON dbo.GivenNotes.CodeNoteType = dbo.NoteType.CodeNoteType  INNER JOIN  dbo.Lessons ON dbo.GivenNotes.LessonsCode = dbo.Lessons.CodeLesson " +
                          " where "+ FilterType + "='" + ValueFilter + "'";
        DataTable dtt = new DataTable();
        DataSet ds;
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
            SqlDataAdapter daa = new SqlDataAdapter(selectSTR, con);
            ds = new DataSet("NotesDS"); daa.Fill(ds);
            daa.Fill(ds);
            return dtt = ds.Tables[0];
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
    
    public DataTable FilterHomeWork(string TeacherID, string LessonsCode, string ClassCode)  
    {
        string selectSTR = "SELECT HWCode, IsLehagasha as 'האם השיעורים להגשה', HWDueDate AS 'תאריך סיום',HWInfo AS 'תוכן שיעורי הבית',HWGivenDate AS 'תאריך נתינת השיעורים'" +
                            " FROM dbo.HomeWork where  CodeClass = '"+ ClassCode  + "' and LessonsCode = '"+ LessonsCode + "' and TeacherID = '"+ TeacherID + "'";
        DataTable dtt = new DataTable();
        DataSet ds;
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
            SqlDataAdapter daa = new SqlDataAdapter(selectSTR, con); // create the data adapter
            ds = new DataSet("HWDS");
            daa.Fill(ds);
            return dtt = ds.Tables[0];
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

    public DataTable FillAllHomeWork(string ClassCode)//WebService
    {
        string selectSTR = "SELECT  HWCode,IsLehagasha,LessonsCode , HWDueDate ,HWInfo,HWGivenDate" +
                            " FROM dbo.HomeWork where  CodeClass = '" + ClassCode + "'";
        DataTable dtt = new DataTable();
        DataSet ds;
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
            SqlDataAdapter daa = new SqlDataAdapter(selectSTR, con); // create the data adapter
            ds = new DataSet("HWDS");
            daa.Fill(ds);
            return dtt = ds.Tables[0];
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

    public DataTable GivenAllNotes(string PupilID) //webService
    {
        string selectSTR = " SELECT dbo.GivenNotes.CodeGivenNote, dbo.GivenNotes.Comment , dbo.GivenNotes.NoteDate , dbo.Lessons.LessonName , dbo.NoteType.NoteName " +
                          " FROM  dbo.Users inner JOIN dbo.Pupil ON dbo.Users.UserID = dbo.Pupil.UserID inner JOIN dbo.GivenNotes " +
                          "ON dbo.Users.UserID = dbo.GivenNotes.PupilID  inner JOIN dbo.NoteType ON dbo.GivenNotes.CodeNoteType = dbo.NoteType.CodeNoteType  INNER JOIN  dbo.Lessons ON dbo.GivenNotes.LessonsCode = dbo.Lessons.CodeLesson " +
                          " where dbo.Pupil.UserID='" + PupilID + "'";
        DataTable dtt = new DataTable();
        DataSet ds;
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
            SqlDataAdapter daa = new SqlDataAdapter(selectSTR, con); // create the data adapter
            ds = new DataSet("ALLNotesDS");
            daa.Fill(ds);
            return dtt = ds.Tables[0];
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

    public DataTable FillBySubjectHomeWork(string PupilID, string ChooseSubjectCode) //webService
    {
        string selectSTR = " SELECT dbo.HomeWork.HWGivenDate,(dbo.Users.UserFName+' ' +dbo.Users.UserLName) as TeacherName, dbo.Lessons.LessonName ,dbo.HomeWork.HWInfo, dbo.HomeWork.HWDueDate, dbo.HomeWork.IsLehagasha" +
                          " FROM  dbo.Users INNER JOIN dbo.HomeWork ON dbo.Users.UserID = dbo.HomeWork.TeacherID INNER JOIN  dbo.Lessons  " +
                          " ON dbo.HomeWork.LessonsCode = dbo.Lessons.CodeLesson " +
                          " where dbo.HomeWork.CodeClass= (select [CodeClass] from [dbo].[Pupil] where [UserID]='"+ PupilID + "') and dbo.Lessons.CodeLesson='"+ ChooseSubjectCode + "'";
        DataTable dtt = new DataTable();
        DataSet ds;
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
            SqlDataAdapter daa = new SqlDataAdapter(selectSTR, con); // create the data adapter
            ds = new DataSet("ALLNotesDS");
            daa.Fill(ds);
            return dtt = ds.Tables[0];
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

    public DataTable GivenNotesBySubject(string PupilID, string ChooseSubjectCode) //webService
    {
        string selectSTR = " SELECT  dbo.GivenNotes.Comment , dbo.GivenNotes.NoteDate , dbo.Lessons.LessonName , dbo.NoteType.NoteName" +
                          " FROM  dbo.Users inner JOIN dbo.Pupil ON dbo.Users.UserID = dbo.Pupil.UserID inner JOIN dbo.GivenNotes " +
                          "ON dbo.Users.UserID = dbo.GivenNotes.PupilID  inner JOIN dbo.NoteType ON dbo.GivenNotes.CodeNoteType = dbo.NoteType.CodeNoteType  INNER JOIN  dbo.Lessons ON dbo.GivenNotes.LessonsCode = dbo.Lessons.CodeLesson " +
                          " where dbo.Pupil.UserID='" + PupilID + "' and dbo.GivenNotes.LessonsCode ='" + ChooseSubjectCode + "'";
        DataTable dtt = new DataTable();
        DataSet ds;
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
            SqlDataAdapter daa = new SqlDataAdapter(selectSTR, con); // create the data adapter
            ds = new DataSet("NotesDS");
            daa.Fill(ds);
            return dtt = ds.Tables[0];
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

    public DataTable FilterTelphoneList(string UserTypeFilterType, string ClassFilter)
    {
        string selectSTR = "";
        if (UserTypeFilterType=="4")
        {
             selectSTR = "  SELECT  dbo.Users.PhoneNumber as 'מספר סלולרי', (dbo.Users.UserFName +' '+ dbo.Users.UserLName) as 'שם מלא' " +
                     " FROM dbo.Users full JOIN dbo.PupilsParent ON dbo.Users.UserID = dbo.PupilsParent.PupilID  AND dbo.Users.UserID = dbo.PupilsParent.ParentID Full JOIN" +
                     " dbo.Pupil ON dbo.Users.UserID = dbo.Pupil.UserID   where dbo.Users.CodeUserType='" + UserTypeFilterType + "'and dbo.Pupil.CodeClass='" + ClassFilter + "'";
        }
        else
        {
             selectSTR = "SELECT dbo.Users.PhoneNumber,( dbo.Users.UserFName+' '+ dbo.Users.UserLName) as 'שם הורה'"+
                                " FROM dbo.PupilsParent INNER JOIN dbo.Users ON dbo.PupilsParent.ParentID = dbo.Users.UserID"+
                                " where dbo.PupilsParent.codeClass = '"+ ClassFilter + "'";
        }

        DataTable dtt = new DataTable();
        DataSet ds;
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
            SqlDataAdapter daa = new SqlDataAdapter(selectSTR, con); // create the data adapter
            ds = new DataSet("TelephoneNumDS");
            daa.Fill(ds);
            return dtt = ds.Tables[0];
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

    public List<string> GetUserInfo(string UserID)
    {
        string UserFName, UserLName, BirthDate, UserImg, UserName, UserPassword, PhoneNumber;
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
                UserFName = dr["UserFName"].ToString();
                UserInfo.Add(UserFName);
                UserLName = dr["UserLName"].ToString();
                UserInfo.Add(UserLName);
                BirthDate = dr["BirthDate"].ToString();
                UserInfo.Add(BirthDate);
                UserName = dr["LoginName"].ToString();
                UserInfo.Add(UserName);
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

    public int ChangePassword(string userID, string Password)
    {
        string cStr = "update[dbo].[Users] set[LoginPassword] = ('" + Password + "') WHERE UserID = '" + userID + "'";
        return ExecuteNonQuery(cStr);
    }

    public int InsertGrade(string PupilID, string TeacherID, string CodeLesson, string ExamDate, int Grade)
    {
        //SqlConnection conGrades = new SqlConnection();
        //conGrades = connect("Betsefer");
        string cStr = "INSERT INTO [dbo].[Grades]  ([PupilID] ,[TeacherID],[CodeLesson],[ExamDate],[Grade])   VALUES ('"+ PupilID + "','"+ TeacherID + "','"+ CodeLesson + "' ,'"+ ExamDate + "' ,"+ Grade + ")";
        return ExecuteNonQuery(cStr);
    }

    public Dictionary<string, string> FillNotes()
    {
        String selectSTR = "SELECT CodeNoteType,NoteName FROM NoteType ";
        string CodeNoteType, NoteName;
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
                    CodeNoteType = dr["CodeNoteType"].ToString();
                    NoteName = dr["NoteName"].ToString();
                    l.Add(CodeNoteType, NoteName);
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

    public int InsertNotes(string PupilID, string CodeNoteType, string NoteDate, string TeacherID,string LessonsCode, string Comment)
    {
        string cStr = "INSERT INTO [dbo].[GivenNotes]  ([PupilID] ,[CodeNoteType],[NoteDate],[TeacherID],[LessonsCode],[Comment])   VALUES ('" + PupilID + "','" + CodeNoteType + "','" + NoteDate + "' ,'" + TeacherID + "' ,'" + LessonsCode + "','"+ Comment + "')";
        return ExecuteNonQuery(cStr);
    }

    public int InserHomeWork(string LessonsCode, string HWInfo, string TeacherID, string CodeClass, string HWDate, bool IsLehagasha)
    {
        string cStr = "INSERT INTO [dbo].[HomeWork] ([LessonsCode] ,[HWInfo],[HWGivenDate],[TeacherID],[CodeClass],[HWDueDate],[IsLehagasha]) " +
                   " VALUES ('" + LessonsCode + "','" + HWInfo + "','" +DateTime.Today.ToShortDateString() +"','" + TeacherID + "' ,'" + CodeClass + "' ,'" + HWDate + "','" + IsLehagasha + "')";
        return ExecuteNonQuery(cStr);
    }

    public Dictionary<string, string> FillLessons()
    {
        String selectSTR = "SELECT CodeLesson, LessonName from Lessons";
        string CodeLesson, LessonName;
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
                l.Add("0", "בחר מקצוע");
                while (dr.Read())
                {
                    CodeLesson = dr["CodeLesson"].ToString();
                    LessonName = dr["LessonName"].ToString();
                    l.Add(CodeLesson, LessonName);
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

    public int GetMainTeacherClass(string id)
    {
        String selectSTR = "SELECT ClassCode FROM Class where MainTeacherID  = '" + id + "'";
        int classCode = -1;
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
                classCode = int.Parse(dr[0].ToString());
            }
            return classCode;
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
            // write to log
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
}