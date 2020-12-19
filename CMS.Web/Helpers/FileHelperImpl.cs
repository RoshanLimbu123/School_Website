using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using CMS.Web.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
namespace CMS.Web.Helpers
{
    public class FileHelperImpl : FileHelper
    {
        private IHostingEnvironment hostingEnvironment;

        public FileHelperImpl(IHostingEnvironment _hostingEnvironment)
        {
            hostingEnvironment = _hostingEnvironment;
        }
        
        public bool isImageValid(string file_name)
        {
            var allowedExtensions = new[] { ".jpeg", ".png", ".jpg", ".gif " };
            var extension = Path.GetExtension(file_name).ToLower();
            if (!allowedExtensions.Contains(extension))
                return false;
            return true;
        }

        public bool isExcelFileValid(string file_name)
        {
            var allowedExtensions = new[] { ".xlsx", ".xls" };
            var extension = Path.GetExtension(file_name).ToLower();
            if (!allowedExtensions.Contains(extension))
                return false;
            return true;
        }

        public string saveImageAndGetFileName(IFormFile file, string file_prefix = "")
        {
            if (!isImageValid(file.FileName))
            {
                throw new CustomException("invalid Document format. Document must be an image.");
            }

            if (!isImageSizeLessThan1Mb(file))
                throw new CustomException("Image size must be less than 1 MB.");
            Random random = new Random();

            string file_name = "";
            if (string.IsNullOrWhiteSpace(file_prefix))
            {
                file_name = Path.GetFileNameWithoutExtension(file.FileName) + random.Next(1, 1232384943) + Path.GetExtension(file.FileName);
            }
            else
            {
                file_name = file_prefix + random.Next(1, 1232384943) + Path.GetExtension(file.FileName);
            }

            var filePath = Path.Combine(hostingEnvironment.WebRootPath, "images/custom");
            filePath = Path.Combine(filePath, file_name);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return file_name;
        }

        public void deleteImage(string imageName, string path)
        {
            if (File.Exists(Path.Combine(path, imageName)))
            {
                File.Delete(Path.Combine(path, imageName));
            }
        }
        public void imageResize(string input_image_path, string output_image_path, int new_width)
        {
            const long quality = 50L;
            Bitmap source_Bitmap = new Bitmap(input_image_path);
            double dblWidth_origial = source_Bitmap.Width;
            double dblHeigth_origial = source_Bitmap.Height;
            double relation_heigth_width = dblHeigth_origial / dblWidth_origial;

            int new_Height = (int)(new_width * relation_heigth_width);

            var new_DrawArea = new Bitmap(new_width, new_Height);
            using (var graphic_of_DrawArea = Graphics.FromImage(new_DrawArea))
            {
                graphic_of_DrawArea.CompositingQuality = CompositingQuality.HighSpeed;

                graphic_of_DrawArea.InterpolationMode = InterpolationMode.HighQualityBicubic;

                graphic_of_DrawArea.CompositingMode = CompositingMode.SourceCopy;
                //*imports the image into the drawarea

                graphic_of_DrawArea.DrawImage(source_Bitmap, 0, 0, new_width, new_Height);
                //--< Output as .Jpg >--

                using (var output = System.IO.File.Open(output_image_path, FileMode.Create))
                {
                    //< setup jpg >
                    var qualityParamId = Encoder.Quality;

                    var encoderParameters = new EncoderParameters(1);

                    encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);
                    //< save Bitmap as Jpg >

                    var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

                    new_DrawArea.Save(output, codec, encoderParameters);
                    output.Close();
                }
                graphic_of_DrawArea.Dispose();
            }
            source_Bitmap.Dispose();
        }

        public bool isImageSizeLessThan1Mb(IFormFile file)
        {
            if (file != null)
            {
                int maxFileSize = 1024 * 1024;
                if (file.Length <= maxFileSize)
                    return true;
            }
            return false;
        }


        public string getPathToImageFolder()
        {
            string path = Path.Combine(hostingEnvironment.WebRootPath, "assets");
            path = Path.Combine(path, "images");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        public string saveFileAndGetFileName(IFormFile file, string file_prefix = "")
        {
            if (!isFileValid(file.FileName))
            {
                throw new CustomException($"Invalid Document format.Allowed extensions are {string.Join(',', getAllowedFileNameExtensions())}.");
            }

            if (!isFileSizeLessThan3Mb(file))
            {
                throw new CustomException("File size must be less than 3 MB.");
            }

            Random random = new Random();

            string file_name = "";
            if (string.IsNullOrWhiteSpace(file_prefix))
            {
                file_name = Path.GetFileNameWithoutExtension(file.FileName) + random.Next(1, 1232384943) + Path.GetExtension(file.FileName);
            }
            else
            {
                file_name = file_prefix + random.Next(1, 1232384943) + Path.GetExtension(file.FileName);
            }

            var filePath = Path.Combine(hostingEnvironment.WebRootPath, "images/custom");
            filePath = Path.Combine(filePath, file_name);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return file_name;
        }

        private bool isFileSizeLessThan3Mb(IFormFile file)
        {
            if (file != null)
            {
                int maxFileSize = 3 * 1024 * 3 * 1024;
                return file.Length <= maxFileSize;
            }
            return true;
        }

        private string[] getAllowedFileNameExtensions()
        {
            return new[] { ".docx", ".doc", ".pdf", ".xls", ".xlsx", ".ppt", ".pptx", ".jpeg", ".png", ".jpg", ".gif " };
        }

        private bool isFileValid(string fileName)
        {
            var allowedExtensions = getAllowedFileNameExtensions();
            var extension = Path.GetExtension(fileName).ToLower();

            return allowedExtensions.Contains(extension);
        }

        public string getPathToDatabaseFolder() => Path.Combine(hostingEnvironment.WebRootPath, "database_backup");


    }
}
