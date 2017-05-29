using Android.App;
using Android.Widget;
using Android.OS;
using Android.Media;
using System;
using Android.Views;

namespace TinnAwayAndroid
{
    [Activity(Label = "TinnAwayAndroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected MediaPlayer player;
       
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            setOnClickListeners();
        }

        private void setOnClickListeners()
        {
            Button bAuthoredMusic = (Button)FindViewById(Resource.Id.bAuthoredMusic);
            bAuthoredMusic.Click += delegate
            {
                StartActivity(typeof(AuthoredMusicActivity));
            };
            Button bSoundsOfNature = (Button)FindViewById(Resource.Id.bSoundsOfNature);
            bSoundsOfNature.Click += delegate
            {
                StartActivity(typeof(SoundsOfNatureActivity));
            };
            Button bSoundGenerator = (Button)FindViewById(Resource.Id.bSoundGenerator);
            bSoundGenerator.Click += delegate
            {
                StartActivity(typeof(SoundGeneratorActivity));
            };
            Button bTuner = (Button)FindViewById(Resource.Id.bTuner);
            bTuner.Click += delegate
            {
                StartActivity(typeof(TunerActivity));
            };
            Button bAlarmCLock = (Button)FindViewById(Resource.Id.bAlarmClock);
            bAlarmCLock.Click += delegate
            {
                StartActivity(typeof(AlarmClockMidActivity));
            };
            Button bSoundMixer = (Button)FindViewById(Resource.Id.bSoundMixer);
            bSoundMixer.Click += delegate
            {
                StartActivity(typeof(SoundMixerActivity));
            };
        }
    }
}

