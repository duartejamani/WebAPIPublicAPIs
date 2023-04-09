using XcaretTestWebApi.Models;
namespace XcaretTestWebApi.Servicios
{
    public interface IServicio_API
    {
        Task<List<Entry>> Listahttps(bool binary);
        Task<List<string>> ListaCategory();

    }
}
