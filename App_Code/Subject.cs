using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Subject
/// </summary>
public class Subject
{
    public int subjectCode { get; set; }
    public string name { get; set; }
    DBconnection db = new DBconnection();

    public Subject()
    {

    }

    public Dictionary<int, string> getSubjects()
    {
        return db.GetSubjects();
    }

    public List<string> getSubjectsByPupilId(string Id)
    {
        return db.getSubjectsByPupilId(Id);
    }

    public bool IsExists(string newSubject)
    {
        return db.IsExists(newSubject);
    }

    public int AddNewSubject(string newSubject)
    {
        return db.AddNewSubject(newSubject);
    }

    public string GetSubjectNameBySubjectCode(string subjectCode)
    {
        return db.GetSubjectNameBySubjectCode(subjectCode);
    }

    public string GetSubjectCodeBySubjectName(string subjectName)
    {
        DBconnectionTeacher dbt = new DBconnectionTeacher();
        return dbt.GetSubjectCodeBySubjectName(subjectName);
    }

    public Dictionary<string, string> GetSubjectsByClassCode(string classCode)
    {
        return db.GetSubjectsByClassCode(classCode);
    }
}