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
    public string GetUserFullName(string Id)
    {
        Users u = new Users();
        string res = u.GetUserFullName(Id);

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(res);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetPupilsByClassTotalName(string TeacherID)
    {
        // Classes c = new Classes();
        //string classCode = c.GetClassCodeAccordingToClassFullName(TeacherID);
        Users u = new Users();
        List<Dictionary<string, string>> s = new List<Dictionary<string, string>>();
        s = u.getPupilsByClassCode(TeacherID);
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(s);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetPupilsByAndTeachers(string TeacherID)
    {
        // Classes c = new Classes();
        //string classCode = c.GetClassCodeAccordingToClassFullName(TeacherID);
        Users u = new Users();
        List<Dictionary<string, string>> s = new List<Dictionary<string, string>>();
        s = u.getPupilsAndTeachers(TeacherID);
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(s);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetParentsByAndTeachers(string TeacherID)
    {
        // Classes c = new Classes();
        //string classCode = c.GetClassCodeAccordingToClassFullName(TeacherID);
        Users u = new Users();
        List<Dictionary<string, string>> s = new List<Dictionary<string, string>>();
        s = u.getParentsAndTeachers(TeacherID);
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(s);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetUserImgWeb(string UserID)
    {
        Users u = new Users();
        string UserImg = u.GetUserImgByUserID(UserID);

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(UserImg);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetUserImg(string UserID)
    {
        Users u = new Users();
        List<string> res = u.GetUserImgAndFullNameByUserID(UserID);

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(res);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetParentsByClassTotalName(string classTotalName)
    {
        Classes c = new Classes();
        string classCode = c.GetClassCodeAccordingToClassFullName(classTotalName);
        Users u = new Users();
        List<Dictionary<string, string>> s = new List<Dictionary<string, string>>();
        s = u.getParentsByClassCode(classCode);
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(s);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetClassesByTeacherId(string TeacherID)
    {
        Teacher t = new Teacher();
        List<string> classes = t.FillClassOtAccordingTeacherId_List(TeacherID);
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(classes);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetMessagesByUserId(string userId)
    {
        Messages u = new Messages();
        List<Dictionary<string, string>> m = u.GetMessagesByUserId(userId);
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(m);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetsubjectsByClassandTeacherID(string TeacherID,string ClassCode)
    {
        Subject s = new Subject();

        DataTable subjs =s.GetsubjectsByClassandTeacherID(TeacherID, ClassCode);

        var list = new List<Dictionary<string, object>>();

        foreach (DataRow row in subjs.Rows)
        {
            var dict = new Dictionary<string, object>();

            foreach (DataColumn col in subjs.Columns)
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
    public string GetPupilsListByClassTotalName(string Class)
    {
        Users u = new Users();
        DataTable pupils = u.getPupillistsByClassCode(Class);//className - not code.

        var list = new List<Dictionary<string, object>>();

        foreach (DataRow row in pupils.Rows)
        {
            var dict = new Dictionary<string, object>();

            foreach (DataColumn col in pupils.Columns)
            {
                dict[col.ColumnName] = row[col];
            }
            list.Add(dict);
        }

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringFillHW = js.Serialize(list);
        return jsonStringFillHW; ;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetMessagesByUserIdUnread(string userId)
    {
        Messages u = new Messages();
        List<Dictionary<string, string>> m = u.GetMessagesByUserIdUnread(userId);
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(m);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetAllConversation(string SenderID, string RecipientID)
    {
        Messages u = new Messages();
        List<Messages> m = u.GetAllConversation(SenderID, RecipientID);
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(m);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetTeachers2()
    {
        Users u = new Users();
        List<Dictionary<string, string>> t = new List<Dictionary<string, string>>();
        t = u.GetTeachers2();
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(t);
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
        List<string> UserInfo = UserLogin.GetUserType(UserID, password);
        string UserType = "";
        string isvalid = "";
        string UserRegID = "";
        if (UserInfo.Count >0)
        {
            UserType = UserInfo[0].ToString();

            UserRegID = UserInfo[1].ToString();
    

            if (UserType == "" || UserType == "1")
            {
                isvalid = "Forbidden";
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
        }
        else
        {
            isvalid = "wrongDetails";
        }
      
        string[] arr = new string[] { isvalid, UserType, UserRegID };
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringCategory = js.Serialize(arr);
        return jsonStringCategory;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string ParentChooseChild(string ParentID)
    {
        Parent parent = new Parent(ParentID);
        Student[] children = parent.children.ToArray();

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringCategory = js.Serialize(children);
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
        //לדאוג למלא לתלמיד את כל הפרטים
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
    public string SaveQuestion(string ID, string Q1, string Q2, string A1, string A2)
    {
        Users UserSaveQA = new Users();
        int ans = UserSaveQA.SaveQuestion(ID, int.Parse(Q1), A1, int.Parse(Q2), A2);
        Users UserUpdateLogin = new Users();
        int ans2 = UserUpdateLogin.ChangeFirstLogin(ID);
        int anssss = ans + ans2;

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringQ = js.Serialize(anssss);
        return jsonStringQ;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string TelephoneList(string type, string PupilID,string Teacher)
    {
        string ClassCode = "";
        if (Teacher == "True")
        {
            ClassCode = PupilID;
        }
        else
        {
            Users PupilClass = new Users();
            ClassCode = PupilClass.GetPupilOtClass(PupilID);

        }

        TelphoneList TL = new TelphoneList();

        DataTable DT = TL.FilterTelphoneListForMobile(type, ClassCode);

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
        else if (type == "parent")
        {
            PupilId = u.GetPupilIdByUserTypeAndId(UserId).FirstOrDefault(); /////לתקןןןןןן כמה ילדים להורה

        }
        {
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
        string PupilClassCode = PupilClass.GetPupilOtClass(PupilID);
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
    public string getHwInfoForProgBar(string PupilID)
    {
        HomeWork HW = new HomeWork();
        DataTable HWs = HW.getHwInfoForProgBar(PupilID);

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
    public string getClassStudentsAvgGrades(string PupilID)
    {
        Classes GetClass = new Classes();
        string ClassCode= GetClass.GetClassCodeByUserID(PupilID);

        Grades ClassAvg = new Grades();
        DataTable HWs = ClassAvg.PupilAvgGrades(ClassCode);

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
    public string FillHWHistory(string UserID)
    {
        HomeWork HW = new HomeWork();
        DataTable HWs = HW.FillAllHomeWork_history(UserID);

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
    public string CheckedHW(string PupilID, bool IsDone, string HWCode)
    {
        HomeWork HW = new HomeWork();
        int HWs = HW.HWDone(PupilID, IsDone, HWCode);
        string result = "";
        if (IsDone == true & HWs > 0)
        {
            result = "well done!";
        }
        else if (IsDone == true & HWs < 0 || IsDone == false & HWs < 0)
        {
            result = "something went wrong";
        }
        else if (IsDone == false & HWs > 0)
        {
            result = "updated";
        }


        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringFillHW = js.Serialize(result);
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
    public string FillGradeInfoByCode(string GradeCode)
    {
        Grades Grade = new Grades();
        DataTable Grades = Grade.FilterGrade(GradeCode);

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
    public string GetClassesFullName()
    {
        Classes classesOt = new Classes();
        List<string> classes = classesOt.GetClassesFullName();

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringFillSubjects = js.Serialize(classes);
        return jsonStringFillSubjects;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SubmitHWInfo( string HWContent, string DueDate ,string ChoosenClass, string ChoosenSubject, string IsLehagasha, string TeacherID)
    {
        int answer = 0;
        string stringAnswer = "bad";
        HomeWork InsertHomeW = new HomeWork();
        bool IsLehag = false;

        Classes ClassC = new Classes();
        string ClassCode= ClassC.GetClassCodeAccordingToClassFullName(ChoosenClass);

        Subject Subj = new Subject();
        string Lesson = Subj.GetSubjectCodeBySubjectName(ChoosenSubject);
        if (IsLehagasha=="True")
        {
            IsLehag = true;
        }

        answer= InsertHomeW.InserHomeWork(Lesson, HWContent, TeacherID, ClassCode, DueDate, IsLehag);

        if (answer > 0)
        {
            stringAnswer = "good";
        }

      //  return stringAnswer;

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringFillSubjects = js.Serialize(stringAnswer);
        return jsonStringFillSubjects;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SubmitNoteInfo(string Pupil, string CodeNoteType, string TeacherID, string LessonsCode, string Comment)
    {
        int answer = 0;
        string stringAnswer = "bad";

        Notes InsertNote = new Notes();
        string NoteCode = InsertNote.GiveNoteCodeByNoteName(CodeNoteType);

        Subject Subj = new Subject();
        string Lesson = Subj.GetSubjectCodeBySubjectName(LessonsCode);

        Student student = new Student();
        string StudID = student.GetUserIDByFullName(Pupil);

        string todayDate = DateTime.Today.ToShortDateString();
        answer = InsertNote.InsertNotes(StudID, NoteCode, todayDate, TeacherID, Lesson, Comment);

        if (answer > 0)
        {
            stringAnswer = "good";
        }

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringFillSubjects = js.Serialize(stringAnswer);
        return jsonStringFillSubjects;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SubmitMessage(Messages m)
    {
        Messages message = new Messages();
        int answer; string stringAnswer = "bad";
        Classes c = new Classes();
        if (m.UserClass == "null" & m.UserType == "pupils")
        {
            m.UserClass = c.GetClassCodeByUserID(m.SenderID);
            //m.UserClass = classCode;
        }
        else if (m.UserClass == "null" & m.UserType == "parents")
        {
            Dictionary<string, string> ClassAndParent = c.GetClassCodeAndParentIDByPupilID(m.SenderID);

            m.SenderID = ClassAndParent["ParentID"];
            m.UserClass = ClassAndParent["codeClass"];
        }

        if (m.MessageType == "private")
        {
            answer = message.SendPrivateMessage(m);
        }
        else
        {
            answer = message.SendKolektiveMessage(m);
        }

        if (answer > 0)
        {
            stringAnswer = "good";


            if (m.MessageType == "private")
            {
                Users user = new Users();

                List<Users> userList = user.getUserList("Privet", "3", m.RecipientID);

                string Pushmessage = " התקבלה הודעה חדשה בנושא " + m.Subject;
                string title = "הודעה";

                myPushNot pushNot = new myPushNot(Pushmessage, title, "1", 7, "default");
                pushNot.RunPushNotification(userList, pushNot);
            }
            else
            {
                Users user = new Users();

                List<Users> userList = new List<Users>();
                Classes clas = new Classes();
                string classCode = clas.GetClassCodeAccordingToClassFullName(m.UserClass);
                switch (m.UserType)
                {
                    case "pupils":
                        userList = user.getUserList("Colective", "4", classCode);
                        break;
                    case "parents":
                        userList = user.getUserList("Colective", "3", classCode);
                        break;
                    case "teachers":
                        userList = user.getUserList("Colective", "2", classCode);
                        break;
                    case "parentsAndPupils":
                        userList = user.getUserList("Colective", "5", classCode);
                        break;
                }

                string Pushmessage = " התקבלה הודעה חדשה בנושא " + m.Subject;
                string title = "הודעה";

                myPushNot pushNot = new myPushNot(Pushmessage, title, "1", 7, "default");
                pushNot.RunPushNotification(userList, pushNot);
            }

        }
        return stringAnswer;

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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SetMessageAsRead(string MessageCode)
    {
        Messages m = new Messages();


        //   string bla = m.UpdateMessageAsRead(MessageCode);
        return m.UpdateMessageAsRead(MessageCode);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string LoadScheduleForToday(string Id, string userType)
    {
        TimeTable t = new TimeTable();

        var schedule = t.LoadScheduleForToday(Id, userType);

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringSchedule = js.Serialize(schedule);
        return jsonStringSchedule;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetUserTypeById(string Id)
    {
        Users u = new Users();
        string res = u.GetUserTypeById(Id);
        return res;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetClassesFullName_JustClassesWithPupils()
    {
        Classes c = new Classes();
        var res = c.GetClassesFullName_JustClassesWithPupils();
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringSchedule = js.Serialize(res);
        return jsonStringSchedule;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetPupilsByClassTotalName_TheGoodOne(string ClassTotalName)
    {
        Classes c = new Classes(); Users u = new Users();
        string classCode = c.GetClassCodeAccordingToClassFullName(ClassTotalName);

        List<Dictionary<string, string>> s = new List<Dictionary<string, string>>();
        s = u.getPupilsByClassCode(classCode);
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(s);
        return jsonString;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string LoadTimeTableByIdAndDay(string UserId, string UserType, int Day)
    {
        TimeTable t = new TimeTable();
        List<Dictionary<string, string>> timeTable = t.LoadScheduleForToday(UserId, UserType, Day);
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringTimeTable = js.Serialize(timeTable);
        return jsonStringTimeTable;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string IfMehanech_LoadParentDay(string UserId)
    {
        ParentsDay p = new ParentsDay();
        p = p.IfMehanech_LoadParentDay(UserId);

        if (p == null)
        {
            return "no";
        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonStringParentsDay = js.Serialize(p);
        return jsonStringParentsDay;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string PushInsertNewGuard(string UserId, string RegId)
    {
        int cID = Convert.ToInt32(UserId);

        Users newUser = new Users();
        newUser.UserID1 = cID.ToString();
        newUser.RegId = RegId;

        int numEffected = newUser.PushUpdateRegId(newUser.UserID1, newUser.RegId);
        if (numEffected == 1)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonString = js.Serialize(newUser);
            return jsonString;
        }
        else
        {
            throw (new Exception("error in create user"));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SaveParentDay(string date, string from, string to, string longMeeting, string teacher)
    {
        ParentsDay p = new ParentsDay();
        p.ParentsDayDate = date;
        p.from = from;
        p.to = to;
        p.longMeeting = int.Parse(longMeeting);
        p.TeacherID = teacher;

        int numEffected = p.SaveParentsDay(p);
        if (numEffected >= 1)
        {
            Users user = new Users();
            string CodeClass = user.GetTeacherMainClass(teacher);

            List<Users> userList = user.getUserList("Colective", "3", CodeClass);

            string message = "נפתח יום הורים, מהרו להשתבץ";
            string title = "יום הורים";

            myPushNot pushNot = new myPushNot(message, title, "1", 7, "default");
            pushNot.RunPushNotification(userList, pushNot);

            JavaScriptSerializer js = new JavaScriptSerializer();
            // serialize to string
            string jsonString = js.Serialize(numEffected);
            return jsonString;
        }
        else
        {
            throw (new Exception("error in create ParentDay"));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SaveMeMeeting(string ParentsDayMeeting, string PupilID)
    {
        ParentsDay p = new ParentsDay();
        string res = p.SaveMeMeeting(ParentsDayMeeting, PupilID);

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(res);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string PushUpdateRegId(string Id, string RegID)
    {
        Users u = new Users();
        int res = u.PushUpdateRegId(Id, RegID);

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(res);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GiveMeBreak(string ParentsDayMeeting)
    {
        ParentsDay p = new ParentsDay();
        string res = p.GiveMeBreak(ParentsDayMeeting);

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(res);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string DeleteBreak(string ParentsDayMeeting)
    {
        ParentsDay p = new ParentsDay();
        int res = p.DeleteBreak(ParentsDayMeeting);

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(res);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Parent_LoadParentDay(string PupilID)
    {
        ParentsDay p = new ParentsDay();
        PupilID = PupilID.Replace(@"\", string.Empty);
        p = p.Parent_LoadParentDay(PupilID);

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(p);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string DeleteMyMeeting(string ParentsDayMeeting)
    {
        ParentsDay p = new ParentsDay();
        int res = p.DeleteMyMeeting(ParentsDayMeeting);

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(res);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetNoteTypes()
    {
        Notes AllNotes = new Notes();
        DataTable DT = AllNotes.GetNotestype();

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
    public string LoadSubjectsByTeacherID(string teacherId)
    {
        Dictionary<string, string> subjects;
        List<string> subjectsList = new List<string>();

        Teacher t = new Teacher();
        subjects = t.FillLessonsAccordingTeacherId(teacherId);

        foreach (var item in subjects)
        {
            subjectsList.Add(item.Value);
        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(subjectsList);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string LoadClassesAccordingToTeacherIdAndSubject(string teacherId, string subject)
    {
        Dictionary<string, string> classes;
        List<string> classesList = new List<string>();

        Teacher t = new Teacher();
        classes = t.FillClassOtAccordingTeacherIdAndSubject(teacherId, subject);

        foreach (var item in classes)
        {
            classesList.Add(item.Value);
        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(classesList);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SaveClassGrades(List<Object> pupilGrades)
    {
        Grades g = new Grades();
        int res = g.InsertGradesAfterAdjustTheDetails(pupilGrades);
        if (res >=1)
        {
            Dictionary<System.String, System.Object> y = (Dictionary<System.String, System.Object>)pupilGrades[0];

            Subject sub = new Subject();
            Users user = new Users();            
            List<Users> userList = user.getUserList("Colective", "5", user.GetPupilOtClass(y["pupilID"].ToString()));

            string Pushmessage = "הוזנו ציונים ב"+ y["subject"].ToString();
            string title = "ציונים";

            myPushNot pushNot = new myPushNot(Pushmessage, title, "1", 7, "default");
            pushNot.RunPushNotification(userList, pushNot);
        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(res);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string LoadTestsByTeacherID(string teacherId)
    {
        List<Grades> tests = new List<Grades>();
        Grades g = new Grades();
        tests = g.LoadTestsByTeacherID(teacherId);

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(tests);
        return jsonString;
    }

    
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetAllGradesByExamCode(string examCode)
    {
        List<Grades> grades = new List<Grades>();
        Grades g = new Grades();
        grades = g.GetAllGradesByExamCode(examCode);

        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = js.Serialize(grades);
        return jsonString;
    }
}

