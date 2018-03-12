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
    public string Login(string UserID, string password)
    {
        Users UserLogin = new Users();
        string isAlreadyLogin = UserLogin.IsAlreadyLogin(UserID, password);
        string UserType = "";
        if (isAlreadyLogin != "")
        {
            if (!bool.Parse(isAlreadyLogin))
            {
                UserType = "openSeqQestion";/*FillSecurityQ();*/
            }
            else
            {
                switch (int.Parse(UserLogin.GetUserType(UserID, password)))
                {
                    case 1:
                        UserType="Admin";
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
        }
        else
        {
            UserType = "אחד מהפרטים שהקשת שגוים";
        }
     

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringCategory = js.Serialize(UserType);
        return jsonStringCategory;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string FillSecurityQ()
    {
        string[] Qestions = new string[8];
        Qestions[0]= "מה שם חיית המחמד שלך";
        Qestions[1] = "מה שם סבתך מצד אמא";
        Qestions[2] = "מה שם בית הספר היסודי שבו אביך למד";
        Qestions[3] = "מה שם בית הספר היסודי בו למדה אמך";
        Qestions[4] = "מה שם סבך מצד אמך";
        Qestions[5] = "מה שם סבך מצד אביך";
        Qestions[6] = "מה שם סבתך מצד אביך";
        Qestions[7] = "מה הצבע המועדף עליך";
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringTelephoneList = js.Serialize(Qestions);
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
    public string GivenTimeTableByClassCode(string PupilID)
    {
        Users PupilClass = new Users();
        string PupilClassCode = PupilClass.GetPupilOtClass(PupilID);
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

