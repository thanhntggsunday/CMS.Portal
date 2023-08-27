
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_Learning.Utils
{
    public class FileLib
    {
        public static void GetFileFromFileAttach(ImgDto fileDto)
        {
            try
            {
                // Converting to bytes.
                byte[] uploadedFile = new byte[fileDto.FileAttach.InputStream.Length];
                fileDto.FileAttach.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

                // Initialization.
                fileDto.FileContent = Convert.ToBase64String(uploadedFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
          
           
        }
    }
}