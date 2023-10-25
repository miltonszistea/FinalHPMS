using FinalHPMS.Models;

namespace FinalHPMS.Services;

public interface IProductService
{
    void Create(Product obj);
    List<Product> GetAll();
    void Update(Product obj, int id);
    void Delete(Product obj);
    Product Get(int id);

}