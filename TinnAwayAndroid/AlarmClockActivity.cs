
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

using TinnAwayAndroid.IntentService;

namespace TinnAwayAndroid
{
    [Activity(Label = "AlarmClockActivity")]
    public class AlarmClockActivity : Activity
    {
        private Button bOneTimeAlarm;
        private Button bRepeatingAlarm;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.alarm_clock);

            bOneTimeAlarm = (Button)FindViewById(Resource.Id.alarm_clock_bOneTimeAlarm);
            bRepeatingAlarm = (Button)FindViewById(Resource.Id.alarm_clock_bRepeatingAlarm);

            setListeners();
        }

        private void setListeners()
        {
            bOneTimeAlarm.Click += delegate
            {
                AlarmManager alarm = (AlarmManager)GetSystemService(Context.AlarmService);
                Intent checkEventsIntent = (new Intent(this, typeof(CheckEventsService)));

                PendingIntent pendingServiceIntent = PendingIntent.GetService(this, 0, checkEventsIntent, PendingIntentFlags.UpdateCurrent);
                alarm.Set(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + 10000, pendingServiceIntent);
            };
        }
    }
}