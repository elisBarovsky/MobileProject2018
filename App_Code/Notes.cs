using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Notes
/// </summary>
public class Notes
{
    DBconnection db;
    DBconnectionTeacher dbT;

    public Notes()
    {
        dbT = new DBconnectionTeacher();
        db = new DBconnection();
    }

    public Dictionary<string, string> FillNotes()
    {
        return dbT.FillNotes();
    }

    public int InsertNotes(string PupilID, string CodeNoteType, string NoteDate, string TeacherID, string LessonsCode, string Comment)
    {
        return dbT.InsertNotes(PupilID, CodeNoteType, NoteDate, TeacherID, LessonsCode, Comment);
    }

    public DataTable FilterNotes(string FilterType,string ValueFilter)
    {
        return dbT.FilterNotes(FilterType, ValueFilter);
    }

    public DataTable GivenAllNotes(string PupilID)
    {
        return dbT.GivenAllNotes(PupilID);
    }

    public DataTable GivenNotesBySubject(string PupilID, string ChooseSubjectCode)
    {
        return dbT.GivenNotesBySubject(PupilID, ChooseSubjectCode);
    }
}