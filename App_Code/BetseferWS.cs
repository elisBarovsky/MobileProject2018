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

        if (UserType == "")
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
        string jsonStringTelephoneList = js.Serialize(Qestions);
        return jsonStringTelephoneList;
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
        string jsonStringTelephoneList = js.Serialize(anssss);
        return jsonStringTelephoneList;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string TelephoneList(string UserTypeFilter,string PupilID)
    {
        Users PupilClass = new Users();
        string PupilClassCode = PupilClass.GetPupilOtClass(PupilID);

        TelphoneList TL = new TelphoneList();
        DataTable DT =  TL.FilterTelphoneList(UserTypeFilter, PupilClassCode);
        
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringTelephoneList = js.Serialize(DT);
        return jsonStringTelephoneList;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GivenAllNotes(string PupilID)
    {
        Notes AllNotesByID = new Notes();
        DataTable DT = AllNotesByID.GivenAllNotes(PupilID);

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringGivenAllNotes = js.Serialize(DT);
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
    public string GivenTimeTableByPupilID(string UserID, string UserType)
    {
        switch (UserType)
        {
            case "teacher":
                break;
            case "Child":
                break;
        }
        Users PupilClass = new Users();
        string PupilClassCode = PupilClass.GetPupilOtClass(UserID);
        TimeTable TimeTableByClassCode = new TimeTable();

        List<Dictionary<string, string>> ls = TimeTableByClassCode.GetTimeTableAcordingToClassCode(int.Parse(PupilClassCode));

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

