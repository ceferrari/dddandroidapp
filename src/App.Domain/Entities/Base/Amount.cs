namespace App.Domain.Entities.Base
{
    public class Amount
    {
        public int Total { get; set; }

        public Amount(int total)
        {
            Total = total;
        }
    }
}