using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Grades
/// </summary>
public class Grades
{
    DBconnectionTeacher dbT;
    DBconnection db;
    public string subject { get; set; }
    public string subjectCode { get; set; }
    public string date { get; set; }
    public string teacherID { get; set; }
    public string pupilID { get; set; }
    public string pupilName { get; set; }
    public int grade { get; set; }
    public int classID { get; set; }
    public string className { get; set; }
    public int examCode { get; set; }
    

    public Grades()
    {
        dbT = new DBconnectionTeacher();
        db = new DBconnection();
    }

    public Grades(string _subject, string _date, string _teacherID, string _pupilID, int _grade, int _classID, string _className, string _subjectCode)
    {
        subject = _subject;
        date = _date;
        teacherID = _teacherID;
        pupilID = _pupilID;
        grade = _grade;
        classID = _classID;
        subjectCode = _subjectCode;
    }

    public Dictionary<string, string> FillClassOt()
    {
        return db.FillClassOt();
    }

    public Dictionary<string, string> FillLessons()
    {
        return dbT.FillLessons();
    }

    public DataTable PupilList(string ClassOtID)
    {
        return dbT.PupilList(ClassOtID);
    }

    public int InsertGrade(string PupilID,int ExamCode, int Grade)
    {
        return dbT.InsertGrade(PupilID, ExamCode, Grade);
    }

    public DataTable PupilGrades(string PupilID) // NEW !!!!
    {
        return dbT.PupilGrades(PupilID);
    }

    public DataTable FilterGrade(string GradeDate)  // NEW !
    {
        return dbT.FilterGrade(GradeDate);
    }

    public DataTable PupilAvgGrades(string ClassCode) // NEW !!!!
    {
        return dbT.PupilAvgGrades(ClassCode);
    }

    public int InsertGradesAfterAdjustTheDetails(List<Object> pupilGrade)
    {
        List<Grades> grades = new List<Grades>();

        for (int i = 0; i < pupilGrade.Count; i++)
        {// lo oved offfff

            Dictionary<System.String, System.Object> y = (Dictionary<System.String, System.Object>)pupilGrade[i];

            Grades g = new Grades();
            g.subject = y["subject"].ToString();
            g.teacherID = y["teacherID"].ToString();
            g.pupilID = y["pupilID"].ToString();
            g.grade = int.Parse(y["grade"].ToString());
            g.date = y["date"].ToString();
            g.classID = int.Parse(db.GetClassCodeByPupilId(g.pupilID));
            grades.Add(g);
        }

        string lessonCode = db.GetLessonCodeByLessonName(grades[0].subject);
        int counter = 0;

        counter += InserExam(grades[0].date, grades[0].teacherID, grades[0].classID, lessonCode);

        if (counter == 1)
        {
            int examCode = GetLastExamCode();
            counter = 0;
            for (int i = 0; i < pupilGrade.Count; i++)
            {
                counter += InsertGrade(grades[i].pupilID, examCode, grades[i].grade);
            }
            if (counter == grades.Count)
            {
                return 1;
            }
        }
        return 0;
    }

    public int InserExam(string examDate, string teacherID, int classCode, string lessonCode)
    {
        return dbT.InserExam(examDate, teacherID, classCode, lessonCode);
    }

    public int GetLastExamCode()
    {
        return dbT.GetLastExamCode();
    }

    public List<Grades> LoadTestsByTeacherID(string teacherId)
    {
        List<Grades> tests = dbT.LoadTestsByTeacherID(teacherId);
        for (int i = 0; i < tests.Count; i++)
        {
            tests[i].subject = db.GetLessonNameByLessonCode(tests[i].subjectCode);
            tests[i].className = db.GetClassNameByCodeClass(tests[i].classID);
        }
        return tests;
    }

    public List<Grades> GetAllGradesByExamCode(string examCode)
    {
        return dbT.GetAllGradesByExamCode(examCode);
    }
}