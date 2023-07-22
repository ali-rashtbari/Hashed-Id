namespace Hashed_Id.Services
{
    public interface IIntIdHahser
    {
        string Code(int rawId);
        int Decode(string hashdedId);
    }
}
