namespace Raefftec.CatchEmAll.DAL
{
    public class Subscription : IHasIdentifier
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }

        public int LowPriotityQuota { get; set; }

        public int NormalPriotiryQuota { get; set; }

        public int HighPriorityQuota { get; set; }
    }
}
