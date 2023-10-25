using FinalHPMS.Models;
using FinalHPMS.ViewModels;

namespace FinalHPMS.Services;

public interface IClientService
{
    void Create(Client obj);
    ClientViewModel GetAll(string filter);
    void Update(Client obj, int id);
    void Delete(Client obj);
    Client? GetDetails(int id);
    Client? GetClient(int id);

}