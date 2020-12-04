namespace DemoApp.Model
{
    public class InterestRegistration
    {
        /// <summary>
        /// The title of the interest
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The description of the Interest
        /// </summary>
        public string Description { get; set; }
        public bool IsSubscribedTo { get; set; }
    }
}