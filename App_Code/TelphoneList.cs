using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TelphoneList
/// </summary>
public class TelphoneList
{
    DBconnection db;
    DBconnectionTeacher dbT;

    public TelphoneList()
    {
        dbT = new DBconnectionTeacher();
        db = new DBconnection();
    }

    public DataTable FilterTelphoneList(string UserTypeFilterType, string ClassFilter)
    {
        return dbT.FilterTelphoneList(UserTypeFilterType, ClassFilter);
    }
    

    public DataTable FilterTelphoneListForMobile(string UserTypeFilterType, string ClassFilter)
    {
        return dbT.FilterTelphoneListForMobile(UserTypeFilterType, ClassFilter);
    }
}