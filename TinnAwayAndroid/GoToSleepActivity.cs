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
    [Activity(Label = "GoToSleepActivity")]
    public class GoToSleepActivity : Activity
    {
        private Button bStartLullaby;

        private EditText etNumberOfMinutes;

        private int numberOfMinutes, timersElapsed, interval;
        private float currentVolume, nextVolume;

        System.Timers.Timer t;

        MediaPlayer mediaPlayer;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.go_to_sleep);

            bStartLullaby = (Button)FindViewById(Resource.Id.go_to_sleep_bStartLullaby);
            etNumberOfMinutes = (EditText)FindViewById(Resource.Id.go_to_sleep_etNumberOfMinutes);

            var resourceId = (int)typeof(Resource.Raw).GetField("auth_FruityFallsSong").GetValue(null);
            mediaPlayer = MediaPlayer.Create(this, resourceId);

            currentVolume = 1;
            nextVolume = currentVolume;

            timersElapsed = 0;
            
            setListeners();
        }

        protected override void OnPause()
        {
            base.OnPause();

            if (mediaPlayer.IsPlaying)
            {
                mediaPlayer.Stop();
            }
        }

        private void setListeners()
        {
            bStartLullaby.Click += delegate
            {
                if (!mediaPlayer.IsPlaying)
                {
                    numberOfMinutes = Int32.Parse(etNumberOfMinutes.Text);
                    interval = numberOfMinutes * 600;

                    mediaPlayer.Start();

                    startNewTimer(interval);
                }
            };
        }

        private void startNewTimer(int interval)
        {
            t = new System.Timers.Timer();
            t.Interval = interval;
            t.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);
            t.Start();
        }

        protected void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            t.Stop();

            nextVolume = (float)(currentVolume - 0.01);

            mediaPlayer.SetVolume(nextVolume, nextVolume);

            currentVolume = nextVolume;

            timersElapsed++;

            if (timersElapsed <= 100)
            {
                startNewTimer(interval);
            }
            else
            {
                Finish();
            }
        }
    }
}