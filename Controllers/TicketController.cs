using FinalHPMS.Models;
using FinalHPMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FinalHPMS.Controllers
{
    [Authorize(Roles = "Administrator,Employee,Supervisor")]
    public class TicketController : Controller
    {
        private ITicketService _ticketService;
        private IClientService _clientService;
        private ICommunityService _communityService;
        private IProductService _productService;
        public TicketController(ITicketService ticketService, IClientService clientService, ICommunityService communityService, IProductService productService)
        {
            _ticketService = ticketService;
            _clientService = clientService;
            _communityService = communityService;
            _productService = productService;

        }

        // GET: Ticket
        public IActionResult Index(string filter)
        {
            var ticketContext = _ticketService.GetAll(filter);
            return View(ticketContext);
        }

        // GET: Ticket/Details/5
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var ticketDetailViewModel = _ticketService.GetTicket(id);

            if (ticketDetailViewModel == null)
            {
                return NotFound();
            }

            return View(ticketDetailViewModel);
        }

        // GET: Ticket/Create
        [Authorize(Roles = "Administrator,Employee")]
        public IActionResult Create()
        {
            var model = new TicketCreateViewModel()
            {
                DateAndHour = DateTime.Now,
                Communities = _communityService.GetAll().Communities,
                Clients = _clientService.GetAll(string.Empty).Clients,
            };
            return View(model);
        }

        // POST: Ticket/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Employee")]
        public IActionResult Create(TicketCreateViewModel model)
        {

            if (ModelState.IsValid)
            {       
                if (model.Total == 0)
                {
                    model.Clients = _clientService.GetAll(string.Empty).Clients;
                    model.Communities = _communityService.GetAll(string.Empty).Communities;
                    ModelState.AddModelError("Total", "El total no puede ser cero. Agregue productos válidos.");
                    return View(model);
                } 
  
                var selectedProducts = JsonConvert.DeserializeObject<List<Product>>(model.SelectedProducts ?? string.Empty);
                var existingProducts = _productService.GetAll(string.Empty);
                foreach (var product in selectedProducts)
                {
                    foreach(var existingProduct in existingProducts.Products)
                    {
                        if(existingProduct.Id == product.Id && existingProduct.Stock < product.Quantity && product.Quantity >= 1)
                        {
                            model.Clients = _clientService.GetAll(string.Empty).Clients;
                            model.Communities = _communityService.GetAll(string.Empty).Communities;
                            ModelState.AddModelError("Total", $"Por favor, ingresa otra cantidad deseada. {existingProduct.Name} posee {existingProduct.Stock} existencias. Estás ingresando {product.Quantity}.");
                            return View(model);
                        }
                    }
                }


                var ticket = new Ticket()
                {
                    Id = model.Id,
                    PaymentMethod = model.PaymentMethod,
                    DateAndHour = model.DateAndHour,
                    Total = model.Total,
                    ProductTickets = model.ProductTickets,
                    CommunityId = model.CommunityId,
                    ClientId = model.ClientId,
                };

                


                _ticketService.Create(ticket, selectedProducts ?? new List<Product>());

                return RedirectToAction(nameof(Index));
            }
            return View(model);

        }


        // GET: Ticket/Edit/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var ticket = _ticketService.GetTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }

            var model = new TicketCreateViewModel()
            {
                Id = ticket.Id,
                DateAndHour = ticket.DateAndHour,
                Total = ticket.Total,
            };
            return View(model);
        }

        // POST: Ticket/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(TicketCreateViewModel model)
        {
            if (model.Id == 0)
            {
                return NotFound();
            }
            ModelState.Remove("Communities");
            if (ModelState.IsValid)
            {
                var ticket = new Ticket()
                {
                    Id = model.Id,
                    DateAndHour = DateTime.Now,
                    Total = model.Total,
                    ProductTickets = new List<ProductTicket> { }

                };
                var list = new List<int> { 0 };

                _ticketService.Update(ticket, 1);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Ticket/Delete/5
        [Authorize(Roles = "Administrator,Supervisor")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var ticket = _ticketService.GetDetails(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Supervisor")]
        public IActionResult DeleteConfirmed(int id)
        {
            var ticket = _ticketService.GetTicket(id);
            if (ticket != null)
            {
                _ticketService.Delete(ticket);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Products(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var ticket = _ticketService.GetTicket(id);
            var products = _productService.GetProductsByTicketId(id);

            if (products == null || ticket == null)
            {
                return NotFound();
            }

            var model = new TicketCreateViewModel()
            {
                Id = ticket.Id,
                DateAndHour = ticket.DateAndHour,
                Total = ticket.Total,
                Products = products
            };


            return View(model);
        }



        //[Authorize(Roles = "Administrator,Employee")]
        public IActionResult GetProductsByCommunityId(int id)
        {
            var products = _productService.GetProductsByCommunityId(id);

            return Json(products);

        }

    }

}
