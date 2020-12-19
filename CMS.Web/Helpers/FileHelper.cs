using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Helpers
{
    public interface FileHelper
    {
        bool isImageValid(string file_name);
        void deleteImage(string imageName, string path);
        string saveFileAndGetFileName(IFormFile file, string file_prefix = "");
        string saveImageAndGetFileName(IFormFile file,string file_prefix="");
        bool isExcelFileValid(string file_name);
        void imageResize(string input_image_path,string output_image_path,int new_width);
        bool isImageSizeLessThan1Mb(IFormFile file);

        string getPathToImageFolder();
        string getPathToDatabaseFolder();
    }
}
