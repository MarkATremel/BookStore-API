using System.Threading.Tasks;

namespace BookStore_API.Controllers
{
    internal interface xIBookRepository
    {
        Task FindByID();
        Task FindByID(int id);
        Task FindAll();
    }
}