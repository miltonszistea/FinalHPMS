using FinalHPMS.Models;
using FinalHPMS.ViewModels;

namespace FinalHPMS.Services;

public interface ICommunityService
{
    void Create(Community obj);
    CommunityViewModel GetAll(string filter);
    CommunityViewModel GetAll();
    void Update(Community obj, int id);
    void Delete(Community obj);
    Community? GetDetails(int id);
    Community? GetCommunity(int id);

}