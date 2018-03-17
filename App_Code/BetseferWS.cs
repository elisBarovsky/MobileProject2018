using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Summary description for BetseferWS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class BetseferWS : System.Web.Services.WebService
{
    public BetseferWS()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SaveNewPassword(string Id, string password)
    {
        Users u = new Users();
        int res = u.ChangePassword(Id, password);

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(res);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetUserQuestionsByIdAndBday(string userID, string BDay)
    {
        Users UserLogin = new Users();
        List<string> questionsDetails = UserLogin.GetUserSecurityDetailsByuserIDandBday(userID, BDay);

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(questionsDetails);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Login(string UserID, string password)
    {
        Users UserLogin = new Users();
        string UserType = UserLogin.GetUserType(UserID, password);
        string isvalid = "";

        if (UserType == "" || UserType == "1" || UserType == "2")
        {
            isvalid = "wrongDetails";
        }
        else
        {
            bool isAlreadyLogin = bool.Parse(UserLogin.IsAlreadyLogin(UserID, password));

            if (!isAlreadyLogin)
            {
                isvalid = "openSeqQestion";/*FillSecurityQ();*/
            }
                switch (int.Parse(UserType))
                {
                    case 1:
                        UserType = "Admin";
                        break;
                    case 2:
                        UserType = "Teacher";
                        break;
                    case 3:
                        UserType = "Parent";
                        break;
                    case 4:
                        UserType = "Child";
                        break;
                }
        }
        string[] arr = new string[] { isvalid, UserType };
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringCategory = js.Serialize(arr);
        return jsonStringCategory;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetUserInfo(string Id)
    {
        Users userInfo = new Users();
        List<string> res = userInfo.GetUserInfo(Id);

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(res);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string FillSecurityQ()
    {
        Questions q = new Questions();
        List<Questions> qqqq = q.GetQuestions();
        string[] Qestions = new string[qqqq.Count];
        for (int i = 0; i < qqqq.Count; i++)
        {
            Qestions[i] = qqqq[i].SecurityInfo;
        }
        
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringQ = js.Serialize(Qestions);
        return jsonStringQ;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SaveQuestion(string ID,string Q1, string Q2, string A1, string A2)
    {
        Users UserSaveQA = new Users();
        int ans = UserSaveQA.SaveQuestion(ID,int.Parse(Q1),A1, int.Parse(Q2), A2);
        Users UserUpdateLogin = new Users();
        int ans2 = UserUpdateLogin.ChangeFirstLogin(ID);
        int anssss = ans + ans2;

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringQ = js.Serialize(anssss);
        return jsonStringQ;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string TelephoneList(string type, string PupilID)
    {
        Users PupilClass = new Users();
        string PupilClassCode = PupilClass.GetPupilOtClass(PupilID);

        TelphoneList TL = new TelphoneList();
        DataTable DT =  TL.FilterTelphoneListForMobile(type, PupilClassCode);

        var list = new List<Dictionary<string, object>>();

        foreach (DataRow row in DT.Rows)
        {
            var dict = new Dictionary<string, object>();

            foreach (DataColumn col in DT.Columns)
            {
                dict[col.ColumnName] = row[col];
            }
            list.Add(dict);
        }

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringTelephoneList = js.Serialize(list);
        return jsonStringTelephoneList;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetPupilIdByUserTypeAndId(string UserId, string type)
    {
        Users u = new Users();
        string PupilId = "";
        if (type == "Child")
        {
            PupilId = UserId;
        }
        else
        {
            PupilId = u.GetPupilIdByUserTypeAndId(UserId);
        }

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringFillHW = js.Serialize(PupilId);
        return jsonStringFillHW;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GivenAllNotes(string PupilID)
    {
        Notes AllNotesByID = new Notes();
        DataTable DT = AllNotesByID.GivenAllNotes(PupilID);

        var list = new List<Dictionary<string, object>>();

        foreach (DataRow row in DT.Rows)
        {
            var dict = new Dictionary<string, object>();

            foreach (DataColumn col in DT.Columns)
            {
                dict[col.ColumnName] = row[col];
            }
            list.Add(dict);
        }

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringGivenAllNotes = js.Serialize(list);
        return jsonStringGivenAllNotes;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GivenNoteByCode(string NoteCode)
    {
        Notes AllNotesByID = new Notes();
        DataTable DT = AllNotesByID.GivenNoteByCode(NoteCode);

        var list = new List<Dictionary<string, object>>();

        foreach (DataRow row in DT.Rows)
        {
            var dict = new Dictionary<string, object>();

            foreach (DataColumn col in DT.Columns)
            {
                dict[col.ColumnName] = row[col];
            }
            list.Add(dict);
        }

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringGivenAllNotes = js.Serialize(list);
        return jsonStringGivenAllNotes;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GivenNotesBySubject(string PupilID, string ChooseSubjectCode)
    {
        Dictionary<string, string> LessonsList = new Dictionary<string, string>();
        LessonsList = (Dictionary<string, string>)(Session["LessonsList"]);
        string LessonCode = KeyByValue(LessonsList, ChooseSubjectCode);

        Notes FilterNoteBySubject = new Notes();

        DataTable DT = FilterNoteBySubject.GivenNotesBySubject(PupilID, ChooseSubjectCode);

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringGivenNotesBySubject = js.Serialize(DT);
        return jsonStringGivenNotesBySubject;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GivenTimeTableByPupilID(string PupilID)
    {
        Users PupilClass = new Users();
        string PupilClassCode = PupilClass.GetPupilOtClass(PupilID);
        TimeTable TimeTableByClassCode = new TimeTable();

        List<Dictionary<string, string>> ls = TimeTableByClassCode.GetTimeTableAcordingToClassCodeForMobile(int.Parse(PupilClassCode));

        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonStringGivenTimeTableByClassCode = js.Serialize(ls);
        return jsonStringGivenTimeTableByClassCode;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string FillAllHomeWork(string PupilID)
    {
        Users PupilClass = new Users();
        string PupilClassCode =  PupilClass.GetPupilOtClass(PupilID);
        HomeWork HomeWork = new HomeWork();

        DataTable DT = HomeWork.FillAllHomeWork(PupilClassCode);

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringFillAllHomeWork = js.Serialize(DT);
        return jsonStringFillAllHomeWork;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GivenHWByCode(string HWCode)
    {
        Notes AllHWByID = new Notes();
        DataTable DT = AllHWByID.GivenHTByCode(HWCode);

        var list = new List<Dictionary<string, object>>();

        foreach (DataRow row in DT.Rows)
        {
            var dict = new Dictionary<string, object>();

            foreach (DataColumn col in DT.Columns)
            {
                dict[col.ColumnName] = row[col];
            }
            list.Add(dict);
        }

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringGivenAllNotes = js.Serialize(list);
        return jsonStringGivenAllNotes;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string FillBySubjectHomeWork(string PupilID, string ChooseSubjectCode)
    {
        Users PupilClass = new Users();
        string PupilClassCode = PupilClass.GetPupilOtClass(PupilID);

        Dictionary<string, string> LessonsList = new Dictionary<string, string>();
        LessonsList = (Dictionary<string, string>)(Session["LessonsList"]);
        string LessonCode = KeyByValue(LessonsList, ChooseSubjectCode);
        HomeWork HomeWork = new HomeWork();

        DataTable DT = HomeWork.FillBySubjectHomeWork(PupilID, LessonCode);

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringFillBySubjectHomeWork = js.Serialize(DT);
        return jsonStringFillBySubjectHomeWork;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string getSubjectsByPupilId(string PupilID)
    {
        Users PupilClass = new Users();
        Subject s = new Subject();
        List<string> subjects = s.getSubjectsByPupilId(PupilID);

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringFillSubjects = js.Serialize(subjects);
        return jsonStringFillSubjects;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string FillHW(string UserID)
    {
        HomeWork HW = new HomeWork();
        DataTable HWs = HW.FillAllHomeWork(UserID);

        var list = new List<Dictionary<string, object>>();

        foreach (DataRow row in HWs.Rows)
        {
            var dict = new Dictionary<string, object>();

            foreach (DataColumn col in HWs.Columns)
            {
                dict[col.ColumnName] = row[col];
            }
            list.Add(dict);
        }

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringFillHW = js.Serialize(list);
        return jsonStringFillHW;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string FillGrades(string UserID)
    {
        Grades UserGrades = new Grades();
        DataTable Grades = UserGrades.PupilGrades(UserID);

        var list = new List<Dictionary<string, object>>();

        foreach (DataRow row in Grades.Rows)
        {
            var dict = new Dictionary<string, object>();

            foreach (DataColumn col in Grades.Columns)
            {
                dict[col.ColumnName] = row[col];
            }
            list.Add(dict);
        }

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringFillHW = js.Serialize(list);
        return jsonStringFillHW;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string FillGradeInfoByCode(string GradeDate)
    {
        Grades Grade = new Grades();
        DataTable Grades = Grade.FilterGrade(GradeDate);

        var list = new List<Dictionary<string, object>>();

        foreach (DataRow row in Grades.Rows)
        {
            var dict = new Dictionary<string, object>();

            foreach (DataColumn col in Grades.Columns)
            {
                dict[col.ColumnName] = row[col];
            }
            list.Add(dict);
        }

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringFillHW = js.Serialize(list);
        return jsonStringFillHW;
    }
    public static string KeyByValue(Dictionary<string, string> dict, string val)
    {
        string key = null;
        foreach (KeyValuePair<string, string> pair in dict)
        {
            if (pair.Value == val)
            {
                key = pair.Key;
                break;
            }
        }
        return key;
    }
}

