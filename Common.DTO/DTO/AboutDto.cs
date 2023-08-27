using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Dto;

namespace Common.DTO.DTO
{
  
    public class AboutDto : EntityBaseDto
    {
        public int ID { get; set; }
        public string Introduce { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string OpenTime { get; set; }
        public string Address { get; set; }
        public string Calendar { get; set; }
        public string Contact { get; set; }
    }
}
