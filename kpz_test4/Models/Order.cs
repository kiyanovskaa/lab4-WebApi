namespace kpz_test4.Models
{
    public class Order
    {
        public int id { set; get; }
        public string status { set; get; }
        public int customer_id { get; set; }
        public int jewelry_id { get; set; }
        public int saler_id { get; set; }
    }
}
