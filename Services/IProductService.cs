using FinalHPMS.Models;
using FinalHPMS.ViewModels;

namespace FinalHPMS.Services;

public interface IProductService
{
    void Create(Product obj);
    ProductViewModel GetAll(string filter);
    void Update(Product obj, int id);
    void Delete(Product obj);
    Product? GetDetails(int id);
    Product? GetProduct(int id);

}