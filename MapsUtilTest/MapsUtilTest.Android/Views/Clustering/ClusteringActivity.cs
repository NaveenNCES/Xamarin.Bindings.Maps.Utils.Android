using System;
using System.IO;

using Android.App;
using Android.Widget;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

using Sample.Droid.Utils;
using Sample.Droid.Views.Base;
using Com.Google.Maps.Android.Clustering;
using MapsUtilTest.Droid;

namespace Sample.Droid.Views.Clustering
{
    [Activity(Label = "ClusteringActivity")]
    public class ClusteringActivity : BaseActivity
    {
        private ClusterManager clusterManager;

        protected override void StartMap()
        {
            googleMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(51.503186, -0.126446),10));
            clusterManager = new ClusterManager(this,googleMap);
            googleMap.SetOnCameraIdleListener(clusterManager);

            try
            {
                ReadJson();
            }
            catch(Exception)
            {
                Toast.MakeText(this, "Problem reading list of markers.", ToastLength.Long).Show();
            }
        }

        private void ReadJson()
        {
            Stream stream = Resources.OpenRawResource(Resource.Raw.radar_search);
            var items = ItemReader.StreamToClusterMarker(stream);
            clusterManager.AddItems(items);
        }
    }
}