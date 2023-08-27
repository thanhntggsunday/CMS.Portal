

using Common.Model;

namespace eLearning.Model.Entities
{
    using System.ComponentModel.DataAnnotations;

    public partial class Order : EntityBase
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(256)]
        public string CustomerAddress { get; set; }

        [Required]
        [StringLength(256)]
        public string CustomerEmail { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerMobile { get; set; }

        [Required]
        [StringLength(256)]
        public string CustomerMessage { get; set; }

        [StringLength(256)]
        public string PaymentMethod { get; set; }

        public string PaymentStatus { get; set; }

        public decimal? Total { get; set; }
    }
}