using FinalHPMS.Models;
using FinalHPMS.ViewModels;

namespace FinalHPMS.Services;

public interface IProductService
{
    void Create(Product obj,List<int> CommunityIds);
    ProductViewModel GetAll(string filter);
    void Update(Product obj, List<int> CommunityIds);
    void Delete(Product obj);
    Product? GetDetails(int id);
    Product? GetProduct(int id);
    List<Product> GetProductsByCommunityId(int id);
}