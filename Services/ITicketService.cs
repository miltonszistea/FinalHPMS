using FinalHPMS.Models;
using FinalHPMS.ViewModels;

namespace FinalHPMS.Services;

public interface ITicketService
{
    void Create(Ticket obj);
    TicketViewModel GetAll(string filter);
    void Update(Ticket obj, int id);
    void Delete(Ticket obj);
    Ticket? GetDetails(int id);
    Ticket? GetTicket(int id);

}