using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NogaWebApplication.Data;
using NogaWebApplication.Models;
using NogaWebApplication.Models.Domain;

namespace NogaWebApplication.Controllers
{
    public class CustomersController : Controller
    {

        private readonly NogaDbContext nogaDbContext;
        
        public CustomersController(NogaDbContext nogaDbContext)
        {
            this.nogaDbContext = nogaDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var customers = await nogaDbContext.Customers.ToListAsync();
            return View(customers);
        }
		
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        
		[HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AjaxMethod(string customername)
        {
            bool isValid = !this.nogaDbContext.Customers.ToList().Exists(p => p.name.Equals(customername, StringComparison.CurrentCultureIgnoreCase));
            return Json(isValid);

        }
            [HttpPost]
        public async Task<IActionResult> Add(AddCustomerViewModel addCustomerRequest) 
        {
            var customer = new Customer()
            {
                name = addCustomerRequest.Name,
                customerNumber = addCustomerRequest.customerNumber

            };

            if (ModelState.IsValid)
            {
                await nogaDbContext.Customers.AddAsync(customer);
                await nogaDbContext.SaveChangesAsync();
				return RedirectToAction("Add");
			}
            return RedirectToAction("Add");
        
        
        }
        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var customer = await nogaDbContext.Customers.FirstOrDefaultAsync(x => x.id == id);

            if (customer != null)
            {

                var viewModel = new UpdateCustomerViewModel()
                {
                    id = customer.id,
                    name = customer.name,
                    customerNumber = customer.customerNumber

                };

                return await Task.Run(() => View("View", viewModel));
            }


            return RedirectToAction("Index");
        
        }

		[HttpGet]
		public async Task<IActionResult> ShowDetails(int id)
		{
            var presentCustomer =  await nogaDbContext.Customers.FirstOrDefaultAsync(x => x.id == id);
            var nameOfCustomer = "";

            if (presentCustomer != null)
            {
                nameOfCustomer = presentCustomer.name;
                var customerContactsDetails = nogaDbContext.Contacts;
                var customerAddressesDetails = nogaDbContext.Addresses;
                List<Contact> contacts = new List<Contact>();
                List<Address> addresses = new List<Address>();
                foreach (var contact in customerContactsDetails)
                {
                    if (contact.CustomerId == id)
                    {
                        contacts.Add(contact);
                    }

                }

                foreach (var address in customerAddressesDetails)
                {
                    if (address.CustomerId == id)
                    {
                        addresses.Add(address);

                    }


                }

                if (contacts != null && addresses != null)
                {
                    var viewModel = new ShowDetailsViewModel()
                    {
                        nameOfCustomer = nameOfCustomer,
                        contacts = contacts,
                        addresses = addresses
                    };
                    return await Task.Run(() => View("ShowDetails", viewModel));

                }

                else if (contacts == null && addresses != null)
                {
                    var viewModel = new ShowDetailsViewModel()
                    {
                        nameOfCustomer = nameOfCustomer,
                        addresses = addresses
                    };
                    return await Task.Run(() => View("ShowDetails", viewModel));
                }
                else if (contacts != null && addresses == null)
                {
                    var viewModel = new ShowDetailsViewModel()
                    {
                        nameOfCustomer = nameOfCustomer,
                        contacts = contacts
                    };
                    return await Task.Run(() => View("ShowDetails", viewModel));
                }

            }  
                return RedirectToAction("Index");

		}





		[HttpPost]
        public async Task<IActionResult> View(UpdateCustomerViewModel model)
        {
            var customer = await nogaDbContext.Customers.FindAsync(model.id);

            if (customer != null) 
            { 
                customer.name = model.name;

                customer.customerNumber = model.customerNumber;

                if (ModelState.IsValid)
                {
                    await nogaDbContext.SaveChangesAsync();
                }

                 return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateCustomerViewModel model)
        {
            var customer = await nogaDbContext.Customers.FindAsync(model.id);

            if(customer!=null)
            {

                customer.isDeleted = true;
                await nogaDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


		[HttpGet]
		public IActionResult AddContact(int id)
		{
			return View();
		}


        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactViewModel model)
        {


            var contact = new Contact()
            {
                FullName = model.FullName,
                Email = model.Email,
                OfficeNumber = model.OfficeNumber,
                CustomerId = model.CustomerId

            };
            
            if (ModelState.IsValid)
            {
                await nogaDbContext.Contacts.AddAsync(contact);
                await nogaDbContext.SaveChangesAsync();
                return RedirectToAction("AddContact");
            }

			return RedirectToAction("AddContact");
		}

	}
}
