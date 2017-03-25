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

            /*
            MediaPlayer _player2;
            _player2 = MediaPlayer.Create(this, Resource.Raw.FruityFallsSong);
            _player2.Start();
            */
        }

        private void setOnClickListeners()
        {
            Button bAuthoredMusic = (Button)FindViewById(Resource.Id.bAuthoredMusic);
            bAuthoredMusic.Click += delegate
            {
                StartActivity(typeof(AuthoredMusicActivity));
            };
        }
    }
}

