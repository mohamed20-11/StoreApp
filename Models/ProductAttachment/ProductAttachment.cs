namespace Models
{
    public class ProductAttachment
    {
        public int ID { get; set; }
        public string Image { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
    }
}
