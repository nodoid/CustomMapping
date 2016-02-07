using Xamarin.Forms;

namespace CustomMapping.Droid
{
    public static class UIFileUtils
    {
        public static int GetResourceIdFromFilename(string filename)
        {
            var fn2 = filename.Replace('-', '_').Split('.')[0];
            var res = Forms.Context.Resources.GetIdentifier(fn2, "drawable", Forms.Context.PackageName);
            return res;
        }
    }
}

