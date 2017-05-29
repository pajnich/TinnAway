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
using Android.Media;

namespace TinnAwayAndroid
{
    [Activity(Label = "AlarmTriggeredActivity")]
    public class AlarmTriggeredActivity : Activity
    {
        MediaPlayer mediaPlayer;

        Button bStopPlaying;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.alarm_triggered);

            bStopPlaying = (Button)FindViewById(Resource.Id.alarm_triggered_bStopPlaying);

            setListeners();

            startPlaying();
        }

        protected void onPause()
        {
            stopPlaying();
        }

        private void setListeners()
        {
            bStopPlaying.Click += delegate
            {
                stopPlaying();
            };
        }

        private void startPlaying()
        {
            var resourceId = (int)typeof(Resource.Raw).GetField("auth_FruityFallsSong").GetValue(null);
            
            mediaPlayer = MediaPlayer.Create(this, resourceId);
            mediaPlayer.Start();
        }

        private void stopPlaying()
        {
            mediaPlayer.Stop();
        }
    }
}