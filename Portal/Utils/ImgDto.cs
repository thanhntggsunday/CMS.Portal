using System.Web;

namespace e_Learning.Utils
{
    public class ImgDto
    {
        public string FileId { get; set; }
        public string FileName { get; set; }
        public string FileContent { get; set; }
        public string FileContentType { get; set; }
        public HttpPostedFileBase FileAttach { get; set; }
    }
}
