using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Users
/// </summary>
public class Questions
{
    DBconnection db;
    int securityCode;
    string securityInfo;

    public int SecurityCode
    {
        get
        {
            return securityCode;
        }

        set
        {
            securityCode = value;
        }
    }

    public string SecurityInfo
    {
        get
        {
            return securityInfo;
        }

        set
        {
            securityInfo = value;
        }
    }

    public Questions()
    {
        SecurityInfo = "";
        db = new DBconnection();
    }

    public List<Questions> GetQuestions()
    {
        return db.GetQuestions();
    }

}

