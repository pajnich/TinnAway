using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;

namespace TinnAwayAndroid.IntentService
{
    [Service]
    public class CheckEventsService : Service
    {
        public override StartCommandResult OnStartCommand(Android.Content.Intent intent, StartCommandFlags flags, int startId)
        {
            Log.Debug("DemoService", "DemoService started");

            LaunchNotification("askdj", new DateTime(), "?");

            return StartCommandResult.Sticky;
        }
        public void LaunchNotification(string tittel, DateTime start, string typeEvent)
        {
            var nMgr = (NotificationManager)GetSystemService(NotificationService);
            var launchIntent = new Intent(this, typeof(AlarmClockActivity));

            var pendingIntent = PendingIntent.GetActivity(this, 0, launchIntent, PendingIntentFlags.CancelCurrent);

            Notification.Builder builder = new Notification.Builder(this)
                .SetAutoCancel(true)
                .SetDefaults(NotificationDefaults.All)
                .SetContentIntent(pendingIntent)
                .SetTicker("Eventet " + tittel + " starter klokka " + start.ToShortTimeString())
                .SetContentTitle("Event starter snart!")
                .SetSmallIcon(Resource.Drawable.Icon)
                .SetContentText("Eventet " + tittel + " starter klokka " + start.ToShortTimeString());

            nMgr.Notify(0, builder.Build());
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
    }
}