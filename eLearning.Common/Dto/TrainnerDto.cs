using System;
using Common.Dto;

namespace eLearning.Common.Dto
{
    public class TrainnerDto : EntityBaseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Avatar { get; set; }

        public string Bio { get; set; }
    
        public string CreatorId { get; set; }
        public string EditorId { get; set; }
      
    }
}