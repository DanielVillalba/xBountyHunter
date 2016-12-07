using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;


namespace xBountyHunter.Droid
{
    [Activity(Label = "xBountyHunter.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity: global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new xBountyApp());
        }


        // check if we can catch when the camera is returning
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            // validate if the requestCode is the one assigned to the Camera child activity
            if (CameraAndroid.CAMERA_OK_CODE == requestCode)
                CameraAndroid.onResult(resultCode);
        }
    }
}

