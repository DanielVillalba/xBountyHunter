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

using xBountyHunter.Droid;
using Xamarin.Forms;
using Java.IO;
using Android.Provider;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(CameraAndroid))]
namespace xBountyHunter.Droid
{
    public class CameraAndroid : DependencyServices.ICamera
    {
        private static File file;
        private static File pictureDirectory;
        private static TaskCompletionSource<string> tcs;
        public const int OK_CODE = 155;
        static public int CAMERA_OK_CODE = OK_CODE;

        public Task<string> takePhoto()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            pictureDirectory = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "CameraAppDemo");
            if(!pictureDirectory.Exists())
            {
                pictureDirectory.Mkdirs();
            }

            file = new File(pictureDirectory, String.Format("fugitivo_{0}.jpg", Guid.NewGuid()));

            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(file));
            var activity = (Activity)Forms.Context;
            activity.StartActivityForResult(intent, OK_CODE);
            //activity.StartActivityForResult(intent, 0);
            tcs = new TaskCompletionSource<string>();
            return tcs.Task;
        }


        public static void onResult(Result resultCode)
        {
            if (resultCode == Result.Canceled)
            {
                tcs.TrySetResult(null);
                return;
            }
            if(resultCode != Result.Ok)
            {
                tcs.TrySetException(new Exception("Unexpected error"));
                return;
            }
            string res = "";
            res = file.Path;
            tcs.TrySetResult(res);
        }
    }
}