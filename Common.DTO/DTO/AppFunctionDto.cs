namespace Common.Dto
{
    public partial class AppFunctionDto : EntityBaseDto
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }
        public int DisplayOrder { get; set; }
        public string ParentId { get; set; }
        public string IconCss { get; set; }
    }
}