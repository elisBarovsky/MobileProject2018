using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Messages
/// </summary>
public class Messages
{
    public string SenderID { get; set; }
    public string SenderName { get; set; }
    public string RecipientID { get; set; }
    public string RecipientName { get; set; }
    public string MessageDate { get; set; }
    public string UserClass { get; set; }
    public string UserType { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public bool IsContinues { get; set; }
    public bool IsReadByMe { get; set; }
    public string MessageType { get; set; }
    public string SenderIMG { get; set; }

    DBconnection db = new DBconnection();

    public Messages()
    {

    }

    public Messages(string _SenderID, string _SenderName, string _RecipientID, string _Subject, string _Content, string _MessageType, string _UserClass, string _UserType) //private message.
    {
        SenderID = _SenderID;
        SenderName = _SenderName;
        RecipientID = _RecipientID;
        Subject = _Subject;
        Content = _Content;
        MessageType = _MessageType;
        UserClass = _UserClass;
        UserType = _UserType;
    }


    public Messages(string _SenderID, string _SenderName, string _UserClass, string _UserType, string _Subject, string _Content, string _MessageType)  //koleltive message.
    {
        SenderID = _SenderID;
        SenderName = _SenderName;
        UserClass = _UserClass;
        UserType = _UserType;
        Subject = _Subject;
        Content = _Content;
        MessageType = _MessageType;
    }

    public int SendPrivateMessage(Messages m)
    {
        return db.SendPrivateMessage(m.SenderID, m.RecipientID, m.Subject, m.Content);
    }

    public int SendKolektiveMessage(Messages m)
    {
        return db.SendKolektiveMessage(m.SenderID, m.UserClass, m.UserType, m.Subject, m.Content);
    }

    public List<Dictionary<string, string>> GetMessagesByUserId(string userId)
    {
        return db.GetMessagesByUserId(userId);
    }

    public List<Messages> GetAllConversation(string SenderID, string RecipientID)
    {
        return db.GetAllConversation(SenderID, RecipientID);
    }

    public string UpdateMessageAsRead(string MessageCode)
    {
        return db.UpdateMessageAsRead(MessageCode);
    }
}