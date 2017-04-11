namespace AndroidApp.Domain.Core.Models
{
    public class Count
    {
        public int Total { get; set; }

        public Count(int total)
        {
            Total = total;
        }
    }
}