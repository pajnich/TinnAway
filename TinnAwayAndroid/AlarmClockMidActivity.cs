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

namespace TinnAwayAndroid
{
    [Activity(Label = "AlarmClockMidActivity")]
    public class AlarmClockMidActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.alarm_clock_mid);

            setOnClickListeners();
        }

        private void setOnClickListeners()
        {
            Button bGoToSleep = (Button)FindViewById(Resource.Id.alarm_clock_mid_bGoToSleep);
            bGoToSleep.Click += delegate
            {
                StartActivity(typeof(GoToSleepActivity));
            };
            Button bWakeUp = (Button)FindViewById(Resource.Id.alarm_clock_mid_bWakeUp);
            bWakeUp.Click += delegate
            {
                StartActivity(typeof(AlarmClockActivity));
            };
        }
    }
}