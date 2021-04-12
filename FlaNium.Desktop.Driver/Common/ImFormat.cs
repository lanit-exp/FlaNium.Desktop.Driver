namespace FlaNium.Desktop.Driver.Common
{
    using System.Drawing.Imaging;

    public class ImFormat
    {
        private const string BMP = "bmp";
        private const string EMF = "emf";
        private const string WMF = "wmf";
        private const string GIF = "gif";
        private const string JPEG = "jpeg";
        private const string PNG = "png";
        private const string TIFF = "tiff";
        private const string EXIF = "exif";
        private const string ICON = "icon";

        public static ImageFormat GetImageFormat(string imageFormat)
        {

            switch (imageFormat.ToLower())
            {
                case BMP: return ImageFormat.Bmp;
                case EMF: return ImageFormat.Emf;
                case WMF: return ImageFormat.Wmf;
                case GIF: return ImageFormat.Gif;
                case JPEG: return ImageFormat.Jpeg;
            }

            switch (imageFormat.ToLower())
            {                  
                case ICON: return ImageFormat.Icon;
                case PNG: return ImageFormat.Png;
                case TIFF: return ImageFormat.Tiff;
                case EXIF: return ImageFormat.Exif;

                default: throw new System.FormatException(string.Format("{0} - is not image format.", imageFormat));
            }
        }
    }
}

