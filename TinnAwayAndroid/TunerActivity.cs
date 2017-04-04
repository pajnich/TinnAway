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
using Java.IO;

namespace TinnAwayAndroid
{
    [Activity(Label = "TunerActivity")]
    public class TunerActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.tuner);

            /*  KEEP THIS CODE TO USE WITH playSound() IN SOUND MIXER
            System.IO.Stream inpStr = Resources.OpenRawResource(Resource.Raw.auth_FruityFallsSong);
            ByteArrayOutputStream baos = new ByteArrayOutputStream();
            byte[] buff = new byte[10240];
            int i = int.MaxValue;
            while ((i = inpStr.Read(buff, 0, buff.Length)) > 0) {
                baos.Write(buff, 0, i);
            }
            PlayAudioTrack(baos.ToByteArray());
            */
            playSound();
        }
        public void playSound()
        {
            var duration = 1;
            var sampleRate = 8000;
            var numSamples = duration * sampleRate;
            var sample = new double[numSamples];
            var freqOfTone = 1900;
            byte[] generatedSnd = new byte[2 * numSamples];

            for (int i = 0; i < numSamples; ++i)
            {
                sample[i] = Math.Sin(2 * Math.PI * i / (sampleRate / freqOfTone));
            }

            int idx = 0;
            foreach (double dVal in sample)
            {
                short val = (short)(dVal * 32767);
                generatedSnd[idx++] = (byte)(val & 0x00ff);
                generatedSnd[idx++] = (byte)((val & 0xff00) >> 8);
            }

            var track = new AudioTrack(global::Android.Media.Stream.Music, sampleRate, ChannelOut.Mono, Android.Media.Encoding.Pcm16bit, numSamples, AudioTrackMode.Static);
            track.Write(generatedSnd, 0, numSamples);
            track.Play();
        }
    }
}