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
    [Activity(Label = "SoundMixerActivity")]
    public class SoundMixerActivity : Activity
    {
        private Button bPlay;
        
        private SeekBar sbSong;
        private SeekBar sbNoise;

        MediaPlayer mediaPlayerSong, mediaPlayerNoise;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.sound_mixer);

     

            bPlay = (Button)FindViewById(Resource.Id.sound_mixer_bPlay);
            sbSong = (SeekBar)FindViewById(Resource.Id.sound_mixer_sbSong);
            sbNoise = (SeekBar)FindViewById(Resource.Id.sound_mixer_sbNoise);

            setInitialValues();
            setListeners();
        }

        protected override void OnPause()
        {
            base.OnPause();

            if (mediaPlayerSong.IsPlaying)
            {
                mediaPlayerSong.Stop();
            }
            if (mediaPlayerNoise.IsPlaying)
            {
                mediaPlayerNoise.Stop();
            }
        }

        private void setInitialValues()
        {
            sbSong.Progress = 1;
            sbSong.Max = 50;

            sbNoise.Progress = 1;
            sbNoise.Max = 50;

            var resourceId = (int)typeof(Resource.Raw).GetField("auth_FruityFallsSong").GetValue(null);
            mediaPlayerSong = MediaPlayer.Create(this, resourceId);

            resourceId = (int)typeof(Resource.Raw).GetField("nois_PinkNoise").GetValue(null);
            mediaPlayerNoise = MediaPlayer.Create(this, resourceId);

            //sbFrequency.SetProgress(currFrequency, true);
        }

        private void setListeners()
        {
            bPlay.Click += delegate
            {
                if (!mediaPlayerSong.IsPlaying)
                {
                    mediaPlayerSong.Start();
                }
                if (!mediaPlayerNoise.IsPlaying)
                {
                    mediaPlayerNoise.Start();
                }
            };

            sbSong.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) => {
                if (e.FromUser)
                {
                    mediaPlayerSong.SetVolume((float)(e.Progress / 100.0), (float)(e.Progress / 100.0));
                }
            };

            sbNoise.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) => {
                if (e.FromUser)
                {
                    mediaPlayerNoise.SetVolume((float)(e.Progress / 100.0), (float)(e.Progress / 100.0));
                }
            };
        }
    }
}
