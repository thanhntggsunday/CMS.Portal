using System;
using Common.Dto;

namespace eLearning.Common.Dto
{
    public class OrderDto : EntityBaseDto
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerMessage { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public decimal? Total { get; set; }
        public int? CourseId { get; set; }
        public string AppUserId { get; set; }
        public string CourseName { get; set; }
        public string CategoryName { get; set; }
        public string CreatorId { get; set; }
        public string EditorId { get; set; }
    }
}