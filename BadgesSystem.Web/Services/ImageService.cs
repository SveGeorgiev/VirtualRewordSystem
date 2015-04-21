using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Omu.Drawing;

namespace BadgesSystem.Web.Services
{
    public class ImageService
    {
        private const int ImgMaxWidth = 250;
        private const int ImgMaxHeight = 125;

        public BadgesSystem.Models.File RenderFile(HttpPostedFileBase file)
        {
            var fileImage = new BadgesSystem.Models.File();
            fileImage.FileName = file.FileName;
            fileImage.FileContent = ConverToBytes(file);

            return fileImage;
        }

        public BadgesSystem.Models.File RenderFile(KeyValuePair<string, Stream> file)
        {
            var fileImage = new BadgesSystem.Models.File();
            fileImage.FileName = file.Key;
            fileImage.FileContent = ConverToBytes(file.Value);

            return fileImage;
        }

        public BadgesSystem.Models.File ResizeFile(BadgesSystem.Models.File file)
        {
            var ms = new MemoryStream(file.FileContent);
            var source = Imager.Resize(Image.FromStream(ms), ImgMaxWidth, ImgMaxHeight, false);
            var fileResize = new MemoryStream();

            source.Save(fileResize, ImageFormat.Png);
            file.FileContent = fileResize.ToArray();

            return file;
        }

        private byte[] ConverToBytes(Stream strm)
        {
            byte[] imageBytes = null;
            using (System.IO.BinaryReader reader = new System.IO.BinaryReader(strm))
            {
                imageBytes = reader.ReadBytes((int)strm.Length);
                return imageBytes;
            }
        }

        private byte[] ConverToBytes(HttpPostedFileBase file)
        {
            byte[] imageBytes = null;
            using (System.IO.BinaryReader reader = new System.IO.BinaryReader(file.InputStream))
            {
                imageBytes = reader.ReadBytes((int)file.ContentLength);
                return imageBytes;
            }
        }
    }
}