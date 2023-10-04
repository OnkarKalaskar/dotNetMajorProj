namespace PopcornBackend.Models
{
    public class Subscription
    {

        public int SubscriptionId { get; set; }

        public string PlanName { get; set; }

        public int Duration { get; set; }
        
        public int Price { get; set; }

        //employee reference
        public ICollection<User>? Users { get; set; }
       
    }
}
