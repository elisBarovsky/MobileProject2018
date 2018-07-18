using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using PushSharp.Core;
using PushSharp.Google;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using Newtonsoft.Json.Linq;

/// <summary>
/// Summary description for myPushNot
/// </summary>
public class myPushNot
{
    public myPushNot()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private string Message;

    public string message
    {
        get { return Message; }
        set { Message = value; }
    }

    private string Title;

    public string title
    {
        get { return Title; }
        set { Title = value; }
    }

    private string Msgcnt;

    public string msgcnt
    {
        get { return Msgcnt; }
        set { Msgcnt = value; }
    }

    private int Badge;

    public int badge
    {
        get { return Badge; }
        set { Badge = value; }
    }

    private string Sound;

    public string sound
    {
        get { return Sound; }
        set { Sound = "default"; }
    }

    private Payload payload1;

    public Payload data
    {
        get { return payload1; }
        set { payload1 = value; }
    }

    public myPushNot(string _message, string _title, string _msgcnt, int _badge, string _sound)
    {
        message = _message;
        title = _title;
        msgcnt = _msgcnt;
        badge = _badge;
        sound = _sound;
    }

    public void RunPushNotification(List<Users> userList, myPushNot pushNot)
    {
        List<string> registrationIDs = new List<string>();

        foreach (var item in userList)
        {
            //ignore nulls
            if (item.RegId != "")
            {
                registrationIDs.Add(item.RegId);
            }

        }

        // Configuration
        var config = new GcmConfiguration("AIzaSyCXwH-HNkRlKLCCD47q2ybPM3AWaUq7uL0");
        config.GcmUrl = "https://fcm.googleapis.com/fcm/send";

        // Create a new broker
        var gcmBroker = new GcmServiceBroker(config);

        // Wire up events
        gcmBroker.OnNotificationFailed += (notification, aggregateEx) =>
        {
            Console.WriteLine("GCM Notification Failed!");
        };

        gcmBroker.OnNotificationSucceeded += (notification) =>
        {
            Console.WriteLine("GCM Notification Sent!");
        };

        // Start the broker
        gcmBroker.Start();

        foreach (var regId in registrationIDs)
        {

            // Queue a notification to send
            gcmBroker.QueueNotification(new GcmNotification
            {

                RegistrationIds = new List<string> {
            regId
        },
                Data = JObject.Parse(
                        "{" +
                               "\"title\" : \"" + pushNot.Title + "\"," +
                               "\"message\" : \"" + pushNot.Message + "\"," +
                                "\"info\" : \" Optional \"," +
                            "\"content-available\" : \"" + "1" + "\"" +
                        "}")
            });
        }


        // Stop the broker, wait for it to finish   
        // This isn't done after every message, but after you're
        // done with the broker
        gcmBroker.Stop();

    }
    public void RunPushNotificationOne(Users user, JObject data)
    {

        // Configuration
        var config = new GcmConfiguration("AIzaSyCXwH-HNkRlKLCCD47q2ybPM3AWaUq7uL0");
        config.GcmUrl = "https://fcm.googleapis.com/fcm/send";

        // Create a new broker
        var gcmBroker = new GcmServiceBroker(config);

        // Wire up events
        gcmBroker.OnNotificationFailed += (notification, aggregateEx) =>
        {
            //Console.WriteLine("GCM Notification Failed!");
        };

        gcmBroker.OnNotificationSucceeded += (notification) =>
        {
            //Console.WriteLine("GCM Notification Sent!");
        };

        // Start the broker
        gcmBroker.Start();

        // Queue a notification to send
        gcmBroker.QueueNotification(new GcmNotification
        {

            RegistrationIds = new List<string> {
            user.RegId
        },
            Data = data
        });



        // Stop the broker, wait for it to finish   
        // This isn't done after every message, but after you're
        // done with the broker
        gcmBroker.Stop();
    }

    public void cancelRide(int rideID, Users user)
    {
        var x = new JObject();

        x.Add("title", "נסיעה בוטלה");

        //modify by import ride from db by rideID
        x.Add("message", "ביום 26.8 מהדסה לירושלים");

        x.Add("rideID", rideID);
        x.Add("info", "Canceled");
        x.Add("content-available", 1);

        RunPushNotificationOne(user, x);
    }

    void NotificationSent(object sender, INotification notification)
    {
        //ScriptManager.RegisterClientScriptBlock(this., this.GetType(), "success", "Erroralert(Sent'" + sender + " -> " + notification + ".');", true);
        Console.WriteLine("Sent: " + sender + " -> " + notification);
    }

}