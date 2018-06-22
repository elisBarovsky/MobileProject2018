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

    public DataTable PupilGrades(string PupilID)  //New !! 
    {
        string selectSTR = " SELECT dbo.Grades.GradeCode, dbo.Lessons.LessonName, dbo.Grades.ExamDate, dbo.Grades.Grade,dbo.Grades.PupilID , (select [UserFName]+' '+[UserLName]  from [dbo].[Users] where [UserID]= dbo.Grades.TeacherID) as Teacher_FullName" +
                        " FROM  dbo.Grades INNER JOIN dbo.Users ON dbo.Grades.PupilID = dbo.Users.UserID  INNER JOIN " +
                        "  dbo.Pupil ON dbo.Users.UserID = dbo.Pupil.UserID INNER JOIN  dbo.Lessons ON dbo.Grades.CodeLesson = dbo.Lessons.CodeLesson " +
                        " where dbo.Grades.PupilID = '"+ PupilID + "' order by dbo.Grades.ExamDate desc";
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
            ds = new DataSet("GradesDS");
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

    public DataTable FilterGrade(string GradeDate) //NEW
    {
        string selectSTR = " SELECT  dbo.Lessons.LessonName, dbo.Grades.ExamDate, dbo.Grades.Grade ,( dbo.Users.UserFName+' '+ dbo.Users.UserLName) as TeacherName " +
                            " FROM   dbo.Grades INNER JOIN dbo.Lessons ON dbo.Grades.CodeLesson = dbo.Lessons.CodeLesson INNER JOIN " +
                            "                          dbo.Users ON  dbo.Grades.TeacherID = dbo.Users.UserID where dbo.Grades.ExamDate='" + GradeDate + "'" +
                            " group by dbo.Lessons.LessonName,dbo.Grades.ExamDate,dbo.Users.UserFName,dbo.Users.UserLName ,dbo.Grades.Grade";
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
            ds = new DataSet("GradeAvgDS");
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

    public DataTable FilterNotes(string FilterType, string ValueFilter, string teacherID)
    {
        string selectSTR = " SELECT  dbo.Pupil.UserID as 'תעודת זהות תלמיד' ,(dbo.Users.UserFName +' '+ dbo.Users.UserLName) as 'שם תלמיד' , dbo.NoteType.NoteName AS 'הערת משמעת', dbo.Lessons.LessonName AS 'שיעור', dbo.GivenNotes.NoteDate AS 'תאריך' ,dbo.GivenNotes.Comment AS 'הערת מורה'" +
                          " FROM  dbo.Users inner JOIN dbo.Pupil ON dbo.Users.UserID = dbo.Pupil.UserID inner JOIN dbo.GivenNotes " +
                          "ON dbo.Users.UserID = dbo.GivenNotes.PupilID  inner JOIN dbo.NoteType ON dbo.GivenNotes.CodeNoteType = dbo.NoteType.CodeNoteType  INNER JOIN  dbo.Lessons ON dbo.GivenNotes.LessonsCode = dbo.Lessons.CodeLesson " +
                          " where " + FilterType + "='" + ValueFilter + "' and dbo.GivenNotes.TeacherID = '" + teacherID + "'";
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

    public DataTable FilterHomeWork(string TeacherID, string LessonsCode, string ClassCode)
    {
        string selectSTR = "SELECT HWCode as 'קוד שיעורים', IsLehagasha as 'האם השיעורים להגשה', HWDueDate AS 'תאריך סיום',HWInfo AS 'תוכן שיעורי הבית',HWGivenDate AS 'תאריך נתינת השיעורים'" +
                            " FROM dbo.HomeWork where  CodeClass = '" + ClassCode + "' and LessonsCode = '" + LessonsCode + "' and TeacherID = '" + TeacherID + "'";
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
        string selectSTR = " SELECT dbo.GivenNotes.CodeGivenNote, dbo.GivenNotes.Comment , dbo.GivenNotes.NoteDate , dbo.Lessons.LessonName , dbo.NoteType.NoteName ,(select ( UserFName+ ' '+ UserLName)  from dbo.Users where [UserID]= dbo.GivenNotes.TeacherID) as Teacher_FullName" +
                          " FROM  dbo.Users inner JOIN dbo.Pupil ON dbo.Users.UserID = dbo.Pupil.UserID inner JOIN dbo.GivenNotes " +
                          "ON dbo.Users.UserID = dbo.GivenNotes.PupilID  inner JOIN dbo.NoteType ON dbo.GivenNotes.CodeNoteType = dbo.NoteType.CodeNoteType  INNER JOIN  dbo.Lessons ON dbo.GivenNotes.LessonsCode = dbo.Lessons.CodeLesson " +
                          " where dbo.Pupil.UserID='" + PupilID + "'order by dbo.GivenNotes.NoteDate desc";
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

    public DataTable GivenNoteByCode(string NoteID) //webService
    {
        string selectSTR = " SELECT dbo.GivenNotes.Comment, dbo.GivenNotes.NoteDate, dbo.Lessons.LessonName, dbo.NoteType.NoteName, (dbo.Users.UserFName+' '+dbo.Users.UserLName)as TeacherName " +
                          " FROM  dbo.Users INNER JOIN dbo.GivenNotes ON dbo.Users.UserID = dbo.GivenNotes.TeacherID INNER JOIN dbo.NoteType " +
                          " ON dbo.GivenNotes.CodeNoteType = dbo.NoteType.CodeNoteType INNER JOIN  dbo.Lessons ON dbo.GivenNotes.LessonsCode = dbo.Lessons.CodeLesson " +
                          " where dbo.GivenNotes.CodeGivenNote='" + NoteID + "'";
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
            ds = new DataSet("OneNoteDS");
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

    public DataTable GivenHTByCode(string HWID) //webService
    {
        string selectSTR = " SELECT dbo.HomeWork.HWCode, dbo.HomeWork.HWInfo, dbo.HomeWork.HWGivenDate, dbo.Lessons.LessonName, dbo.HomeWork.HWDueDate, dbo.HomeWork.IsLehagasha, (dbo.Users.UserFName+' '+ dbo.Users.UserLName) as TeacherName " +
                          " FROM  dbo.HomeWork INNER JOIN dbo.Lessons ON dbo.HomeWork.LessonsCode = dbo.Lessons.CodeLesson INNER JOIN " +
                          " dbo.Users ON dbo.HomeWork.TeacherID = dbo.Users.UserID where dbo.HomeWork.HWCode='" + HWID + "'";
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
            ds = new DataSet("OneNoteDS");
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

    public DataTable FillAllHomeWork(string Id)//WebService
    {
        string selectSTR = "SELECT dbo.HomeWork.HWCode,  dbo.HomeWork.HWInfo, dbo.HomeWork.HWGivenDate, dbo.Lessons.LessonName, dbo.HomeWork.HWDueDate, dbo.HomeWork.IsLehagasha ,(select ( UserFName+ ' '+ UserLName)  from dbo.Users where [UserID]= dbo.HomeWork.TeacherID) as Teacher_FullName , dbo.HWPupil.IsDone" +
            " FROM  dbo.HomeWork INNER JOIN  dbo.HWPupil ON dbo.HomeWork.HWCode = dbo.HWPupil.HWCode INNER JOIN  dbo.Lessons ON dbo.HomeWork.LessonsCode = dbo.Lessons.CodeLesson " +
            "  where dbo.HWPupil.PupilID = '" + Id + "' order by dbo.HomeWork.HWGivenDate desc";
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

    public Dictionary<string, string> FillClassOtAccordingTeacherIdAndSubjectCode(string teacherID, string LessonCode)
    {
        string selectSTR = "SELECT  distinct  dbo.Class.ClassCode, dbo.Class.TotalName FROM dbo.TimetableLesson INNER " +
                           "JOIN dbo.Timetable ON dbo.TimetableLesson.TimeTableCode = dbo.Timetable.TimeTableCode INNER JOIN "+
                           "dbo.Class ON dbo.Timetable.ClassCode = dbo.Class.ClassCode AND dbo.Timetable.ClassCode = " +
                           "dbo.Class.ClassCode where dbo.TimetableLesson.TeacherId = '" + teacherID +
                           "' and dbo.TimetableLesson.CodeLesson = " + LessonCode;

        Dictionary<string, string> classesAccordingTeacherIdAndSubjectCode = new Dictionary<string, string>();

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
                classesAccordingTeacherIdAndSubjectCode.Add(dr["ClassCode"].ToString(), dr["TotalName"].ToString());
            }
            return classesAccordingTeacherIdAndSubjectCode;
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

    public Dictionary<string, string> FillLessonsAccordingTeacherIdAndClassCode(string teacherID, string ClassCode)
    {
        string selectSTR = "SELECT distinct dbo.Lessons.CodeLesson, dbo.Lessons.LessonName FROM dbo.Lessons INNER " +
                           "JOIN dbo.TimetableLesson ON dbo.Lessons.CodeLesson = dbo.TimetableLesson.CodeLesson "+
                           "INNER JOIN dbo.Timetable ON dbo.TimetableLesson.TimeTableCode = dbo.Timetable.TimeTableCode " +
                           "where dbo.TimetableLesson.TeacherId = '" + teacherID + "' and dbo.Timetable.ClassCode = " + ClassCode;

        Dictionary<string, string> lessonsAccordingTeacherIdAndClassCode = new Dictionary<string, string>();

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
                string CodeLesson = dr["CodeLesson"].ToString();
                string LessonName = dr["LessonName"].ToString();
                lessonsAccordingTeacherIdAndClassCode.Add(dr["CodeLesson"].ToString(), dr["LessonName"].ToString());
            }
            return lessonsAccordingTeacherIdAndClassCode;
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

    public Dictionary<string, string> FillLessonsAccordingTeacherId(string teacherID)
    {
        string selectSTR = "SELECT  dbo.Lessons.CodeLesson, dbo.Lessons.LessonName FROM  dbo.Lessons " +
                           "INNER JOIN dbo.TeachersTeachesSubjects ON dbo.Lessons.CodeLesson = " +
                           "dbo.TeachersTeachesSubjects.CodeLessons where dbo.TeachersTeachesSubjects.TeacherID = '" + teacherID + "'";

        Dictionary<string, string> lessonsAccordingTeacherId = new Dictionary<string, string>();

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
                string CodeLesson = dr["CodeLesson"].ToString();
                string LessonName = dr["LessonName"].ToString();
                lessonsAccordingTeacherId.Add(dr["CodeLesson"].ToString(), dr["LessonName"].ToString());
            }
            return lessonsAccordingTeacherId;
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

    public Dictionary<string, string> FillClassOtAccordingTeacherId(string teacherID)
    {
        string selectSTR = "SELECT  distinct  dbo.Class.ClassCode, dbo.Class.TotalName FROM dbo.TimetableLesson " +
                           "INNER JOIN dbo.Timetable ON dbo.TimetableLesson.TimeTableCode = " +
                           "dbo.Timetable.TimeTableCode INNER JOIN dbo.Class ON dbo.Timetable.ClassCode = " +
                           "dbo.Class.ClassCode AND dbo.Timetable.ClassCode = dbo.Class.ClassCode where " +
                           "dbo.TimetableLesson.TeacherId = '" + teacherID + "'";

        Dictionary<string, string> classesAccordingTeacherId = new Dictionary<string, string>();

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
                string CodeClass = dr["ClassCode"].ToString();
                string ClassName = dr["TotalName"].ToString();
                classesAccordingTeacherId.Add(dr["ClassCode"].ToString(), dr["TotalName"].ToString());
            }
            return classesAccordingTeacherId;
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

    public List<string> FillClassOtAccordingTeacherId_List(string teacherID)
    {
        string selectSTR = "SELECT  distinct  dbo.Class.TotalName FROM dbo.TimetableLesson " +
                           "INNER JOIN dbo.Timetable ON dbo.TimetableLesson.TimeTableCode = " +
                           "dbo.Timetable.TimeTableCode INNER JOIN dbo.Class ON dbo.Timetable.ClassCode = " +
                           "dbo.Class.ClassCode AND dbo.Timetable.ClassCode = dbo.Class.ClassCode where " +
                           "dbo.TimetableLesson.TeacherId = '" + teacherID + "'";

        List<string> classesAccordingTeacherId = new List<string>();

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
                classesAccordingTeacherId.Add(dr["TotalName"].ToString());
            }
            return classesAccordingTeacherId;
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
                          " where dbo.HomeWork.CodeClass= (select [CodeClass] from [dbo].[Pupil] where [UserID]='" + PupilID + "') and dbo.Lessons.CodeLesson='" + ChooseSubjectCode + "'";
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
        if (UserTypeFilterType=="4") //pupil
        {
             selectSTR = "  SELECT  dbo.Users.PhoneNumber as 'מספר סלולרי', (dbo.Users.UserFName +' '+ dbo.Users.UserLName) as 'שם מלא' " +
                     " FROM dbo.Users full JOIN dbo.PupilsParent ON dbo.Users.UserID = dbo.PupilsParent.PupilID  AND dbo.Users.UserID = dbo.PupilsParent.ParentID Full JOIN" +
                     " dbo.Pupil ON dbo.Users.UserID = dbo.Pupil.UserID   where dbo.Users.CodeUserType='" + UserTypeFilterType + "'and dbo.Pupil.CodeClass='" + ClassFilter + "'";
        }
        else //parent -> 3
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

    public DataTable FilterTelphoneListForMobile(string UserTypeFilterType, string ClassFilter) //newwwwwwwwww
    {
        string selectSTR = "";
        if (UserTypeFilterType == "4") //pupil
        {
            selectSTR = "  SELECT  dbo.Users.PhoneNumber, (dbo.Users.UserFName +' '+ dbo.Users.UserLName) as 'FullName' " +
                    " FROM dbo.Users full JOIN dbo.PupilsParent ON dbo.Users.UserID = dbo.PupilsParent.PupilID  AND dbo.Users.UserID = dbo.PupilsParent.ParentID Full JOIN" +
                    " dbo.Pupil ON dbo.Users.UserID = dbo.Pupil.UserID   where dbo.Users.CodeUserType='" + UserTypeFilterType + "'and dbo.Pupil.CodeClass='" + ClassFilter + "'";
        }
        else //parent -> 3
        {
            selectSTR = "SELECT dbo.Users.PhoneNumber,( dbo.Users.UserFName+' '+ dbo.Users.UserLName) as 'FullName'" +
                               " FROM dbo.PupilsParent INNER JOIN dbo.Users ON dbo.PupilsParent.ParentID = dbo.Users.UserID" +
                               " where dbo.PupilsParent.codeClass = '" + ClassFilter + "'";
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

    public int InsertNotes(string PupilID, string CodeNoteType, string NoteDate, string TeacherID, string LessonsCode, string Comment)
    {
        string cStr = "INSERT INTO [dbo].[GivenNotes]  ([PupilID] ,[CodeNoteType],[NoteDate],[TeacherID],[LessonsCode],[Comment])   VALUES ('" + PupilID + "','" + CodeNoteType + "','" + NoteDate + "' ,'" + TeacherID + "' ,'" + LessonsCode + "','" + Comment + "')";
        return ExecuteNonQuery(cStr);
    }

    public int HWDone(string PupilID, bool IsDone, string HWCode)
    {
        int Done = 0;
        if (IsDone)
        {
            Done = 1;
        }
        string cStr = "UPDATE [dbo].[HWPupil] SET [IsDone] =" + Done + " where HWCode= " + HWCode + " and PupilID ='" + PupilID + "'";
        return ExecuteNonQuery(cStr);
    }

    public int InserHomeWork(string LessonsCode, string HWInfo, string TeacherID, string CodeClass, string HWDate, bool IsLehagasha)
    {
        int num = 0;
        try
        {
            using (var con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Betsefer"].ConnectionString))
        {
            using (var cmd = new SqlCommand("InsertHomeWork", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LessonsCode", LessonsCode);
                cmd.Parameters.AddWithValue("@HWInfo", HWInfo);
                cmd.Parameters.AddWithValue("@GivenDate", DateTime.Today.ToShortDateString());
                cmd.Parameters.AddWithValue("@TeacherID", TeacherID);
                cmd.Parameters.AddWithValue("@CodeClass", CodeClass);
                cmd.Parameters.AddWithValue("@WDate", HWDate);
                cmd.Parameters.AddWithValue("@IsLehagasha", IsLehagasha);

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

    public string GetSubjectCodeBySubjectName(string subjectName)
    {
        String selectSTR = "SELECT CodeLesson FROM Lessons where LessonName  = '" + subjectName + "'";
        string subjectCode = "";
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
                subjectCode = dr[0].ToString();
            }
            return subjectCode;
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