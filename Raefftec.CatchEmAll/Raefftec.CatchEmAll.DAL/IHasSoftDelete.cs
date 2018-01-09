namespace Raefftec.CatchEmAll.DAL
{
    public interface IHasSoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
