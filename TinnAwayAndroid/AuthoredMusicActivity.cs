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
    [Activity(Label = "AuthoredMusicActivity")]
    public class AuthoredMusicActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.authored_music);

            setupSongListView();
        }

        private void setupSongListView()
        {
            List<string> items = getRawFilesAsStrings();

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, items);

            ListView lvSongs = (ListView)FindViewById(Resource.Id.authored_music_lvSongs);
            lvSongs.Adapter = adapter;

            setListViewItemClickListener(lvSongs);
        }

        private void setListViewItemClickListener(ListView lvSongs)
        {
            lvSongs.ItemClick += (object sender, ItemClickEventArgs e) =>
            {
                String selectedFromList = (String)lvSongs.GetItemAtPosition(e.Position);
                var resourceId = (int)typeof(Resource.Raw).GetField("auth_" + selectedFromList).GetValue(null);
                MediaPlayer mediaPlayer;
                mediaPlayer = MediaPlayer.Create(this, resourceId);
                mediaPlayer.Start();
            };
        }

        private List<string> getRawFilesAsStrings()
        {
            List<string> items = new List<string>();
            var fields = typeof(Resource.Raw).GetFields();
            foreach (var fieldInfo in fields)
            {
                if (fieldInfo.Name.Substring(0, 5).Equals("auth_"))
                {
                    items.Add(fieldInfo.Name.Substring(5));
                }
                
            }
            return items;
        }
    }
}