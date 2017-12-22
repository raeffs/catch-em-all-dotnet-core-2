namespace Raefftec.CatchEmAll.Services
{
    public class SecurityOptions
    {
        public string JwtSecret { get; set; }

        public int SaltBytes { get; set; }

        public int HashBytes { get; set; }

        public int HashIterations { get; set; }

        public string HashAlgorithm { get; set; }
    }
}