using Common.Dto;

namespace Common.DTO.DTO
{
    public class FooterDto : EntityBaseDto
    {
        public string ID { get; set; }

        public string Content { get; set; }

        public bool? Status { get; set; }
    }
}