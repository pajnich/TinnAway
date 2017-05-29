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
using static Android.Widget.AdapterView;
using Android.Media;

namespace TinnAwayAndroid
{
    [Activity(Label = "SoundEffectsActivity")]
    public class SoundsOfNatureActivity : Activity
    {
        List<string> rawFileNames;

        Dictionary<string, MediaPlayer> mediaPlayers;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.sounds_of_nature);

            initializeValues();

            setupSongListView();
        }

        protected override void OnPause()
        {
            base.OnPause();

            stopPlaying();
        }

        private void stopPlaying()
        {
            foreach (KeyValuePair<string, MediaPlayer> entry in mediaPlayers)
            {
                entry.Value.Stop();
            }
        }

        private void initializeValues()
        {

            mediaPlayers = new Dictionary<string, MediaPlayer>();
            rawFileNames = new List<string>();
        }

        private void setupSongListView()
        {
            rawFileNames = getRawFileNames();

            fillMediaPlayers();

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, rawFileNames);

            ListView lvSongs = (ListView)FindViewById(Resource.Id.sounds_of_nature_lvSongs);
            lvSongs.Adapter = adapter;

            setListViewItemClickListener(lvSongs);

        }

        private void fillMediaPlayers()
        {
            foreach (string rawFileName in rawFileNames)
            {
                var resourceId = (int)typeof(Resource.Raw).GetField("natu_" + rawFileName).GetValue(null);
                MediaPlayer mediaPlayer;
                mediaPlayer = MediaPlayer.Create(this, resourceId);
                mediaPlayers.Add(rawFileName, mediaPlayer);
            }
        }

        // set onclick listeners for the song list
        private void setListViewItemClickListener(ListView lvSongs)
        {
            // when an item is clicked
            lvSongs.ItemClick += (object sender, ItemClickEventArgs e) =>
            {
                // get the clicked song name
                String selectedFromList = (String)lvSongs.GetItemAtPosition(e.Position);
                // stop playing the current song
                foreach (KeyValuePair<string, MediaPlayer> entry in mediaPlayers)
                {
                    if (entry.Value.IsPlaying)
                    {
                        entry.Value.Stop();
                    }
                }
                // start playing the clicked song
                mediaPlayers[selectedFromList].Start();
            };
        }

        private List<string> getRawFileNames()
        {
            List<string> items = new List<string>();
            var fields = typeof(Resource.Raw).GetFields();
            foreach (var fieldInfo in fields)
            {
                if (fieldInfo.Name.Substring(0, 5).Equals("natu_"))
                {
                    items.Add(fieldInfo.Name.Substring(5));
                }

            }
            return items;
        }
    }
}