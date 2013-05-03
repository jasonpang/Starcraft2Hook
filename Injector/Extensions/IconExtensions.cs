using System.Drawing;
using System.IO;
using Injector.Properties;

namespace Injector.Extensions
{
    public static class IconExtensions
    {
        public static string GetIconAsSavedFilePath(this Icon icon)
        {
            string temp = Path.GetTempFileName();
            var iconAsStream = new MemoryStream();
            Resources.Starcraft2Icon.Save(iconAsStream);
            File.WriteAllBytes(temp, iconAsStream.ToArray());
            return temp;
        }
    }
}
