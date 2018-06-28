using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

using PushSharp;
using PushSharp.Android;
//using PushSharp.Apple;
using PushSharp.Core;

using System.Web.Script.Serialization;
using System.Web.Script.Services;


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

    //private Payload payload1;

    //public Payload data
    //{
    //    get { return payload1; }
    //    set { payload1 = value; }
    //}

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
        //Create our push services broker
        var push = new PushBroker();

        //Wire up the events for all the services that the broker registers
        push.OnNotificationSent += NotificationSent;
        //push.OnChannelException += ChannelException;
        //push.OnServiceException += ServiceException;
        //push.OnNotificationFailed += NotificationFailed;
        //push.OnDeviceSubscriptionExpired += DeviceSubscriptionExpired;
        //push.OnDeviceSubscriptionChanged += DeviceSubscriptionChanged;
        //push.OnChannelCreated += ChannelCreated;
        //push.OnChannelDestroyed += ChannelDestroyed;


        //------------------------------------------------
        //IMPORTANT NOTE about Push Service Registrations
        //------------------------------------------------
        //Some of the methods in this sample such as 'RegisterAppleServices' depend on you referencing the correct
        //assemblies, and having the correct 'using PushSharp;' in your file since they are extension methods!!!

        // If you don't want to use the extension method helpers you can register a service like this:
        //push.RegisterService<WindowsPhoneToastNotification>(new WindowsPhonePushService());

        //If you register your services like this, you must register the service for each type of notification
        //you want it to handle.  In the case of WindowsPhone, there are several notification types!



        //---------------------------
        // ANDROID GCM NOTIFICATIONS 
        //---------------------------
        //Configure and start Android GCM
        //IMPORTANT: The API KEY comes from your Google APIs Console App, under the API Access section, 
        //  by choosing 'Create new Server key...'
        //  You must ensure the 'Google Cloud Messaging for Android' service is enabled in your APIs Console
        //push.RegisterGcmService(new GcmPushChannelSettings("YOUR Google API's Console API Access  API KEY for Server Apps HERE"));

        //string from web.config
        string serverKey = ConfigurationManager.AppSettings["AIzaSyCXwH-HNkRlKLCCD47q2ybPM3AWaUq7uL0"];
        push.RegisterGcmService(new GcmPushChannelSettings(serverKey));
        //Fluent construction of an Android GCM Notification
        //IMPORTANT: For Android you MUST use your own RegistrationId here that gets generated within your Android app itself!

        //string message = "test";
        //push.QueueNotification(new GcmNotification().ForDeviceRegistrationId("efFGfsxygyU:APA91bFOuNDsdSlTeJX191QLL64HMoKuqQYiVx_AqBL4bbrj_8K8l7CVyBe7KS0BpvNYIn2s883xF3T9uRZdFnTdt8ybWOn9lIs3Y7WTfObMSdVzhc5v7O-EHX0PFUpieGP9Rq9sNKxF")
        //                      .WithJson("{\"message\": \" " + message + " \", \"title\": \" my title\", \"msgcnt\": \"1\", \"alert\":\"Hello World!\",\"badge\":7,\"sound\":\"default\"}"));
        //{\"message\": \" my message\", \"title\": \" my title\", \"msgcnt\": \"1\" - FOR the BACKGROUND!!!
        // \"alert\":\"Hello World!\",\"badge\":7,\"sound\":\"sound.caf\"}" - FOR the FOREGROUND!!!

        //create the json to push message
        JavaScriptSerializer serializer = new JavaScriptSerializer();


        string jsonString = serializer.Serialize(pushNot);

        //get the user regId
        foreach (Users u in userList)
        {
            string readRegFromDb = u.RegId;

            //string readRegFromDb = "dVDPWsEDyQ8:APA91bG-PkJ-yv2vN_ayfb3ipxPowl2ittiIe5gW_Yw-M_mxSdLLViTpaCvDSMFRVQYQSsgwNoLjAiV_EtwZlMvyQ15GpN1jWMS5qvYMdqY19fBYAGhHrBf30QHKnDhbFiY7ojqtuTiQ";
            push.QueueNotification(new GcmNotification().ForDeviceRegistrationId(readRegFromDb)
                                  .WithJson(jsonString));
        }

        //Console.WriteLine("Waiting for Queue to Finish...");

        //Stop and wait for the queues to drains
        push.StopAllServices();

        //Console.WriteLine("Queue Finished, press return to exit...");
        //Console.ReadLine();	
    }
    void NotificationSent(object sender, INotification notification)
    {
        //Console.WriteLine("Sent: " + sender + " -> " + notification);
    }

}