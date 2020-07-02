using BolaEstateApp.Web.Models;
using BolaEstateApp.Data.Entities;
using System.Threading.Tasks;

namespace BolaEstateApp.Web.Interfaces
{
    public interface IAccountsService
    {
        Task<ApplicationUser> CreateUserAsync(RegisterModel model);
    }
}


