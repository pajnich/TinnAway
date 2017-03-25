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

            ListView lvSongs = (ListView)FindViewById(Resource.Id.lvSongs);
            lvSongs.Adapter = adapter;

            setListViewItemClickListener(lvSongs);
        }

        private void setListViewItemClickListener(ListView lvSongs)
        {
            lvSongs.ItemClick += (object sender, ItemClickEventArgs e) =>
            {
                String selectedFromList = (String)lvSongs.GetItemAtPosition(e.Position);
                var resourceId = (int)typeof(Resource.Raw).GetField(selectedFromList).GetValue(null);
                MediaPlayer _player2;
                _player2 = MediaPlayer.Create(this, resourceId);
                _player2.Start();

            };
        }

        private List<string> getRawFilesAsStrings()
        {
            List<string> items = new List<string>();
            var fields = typeof(Resource.Raw).GetFields();
            foreach (var fieldInfo in fields)
            {
                items.Add(fieldInfo.Name);
            }
            return items;
        }
    }
}