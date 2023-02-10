using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;

namespace CompanySurvey.Controllers
{
    public class ManagerController : Controller
    {
    private readonly IUOW uOW;
        public ManagerController(IUOW uOW)
        {
                this.uOW = uOW;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("RoleId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (HttpContext.Session.GetInt32("RoleId") == 1)
            {
                return Unauthorized("You are not authorized to access this page.");
            }
            List<string> counts = new List<string>();
            counts.Add(uOW.CompanyRepository().GetAll().Count().ToString());
            counts.Add(uOW.ServiceRepository().GetAll().Count().ToString());
            counts.Add(uOW.SurveyRepository().GetAll().Count().ToString());
            counts.Add(uOW.CompanyRepository().GetAll().Count().ToString());
            ViewBag.Counts = counts;
            return View();
        }
        public IActionResult CreateCustomer(Guid Id)
        {
            if (HttpContext.Session.GetInt32("RoleId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (HttpContext.Session.GetInt32("RoleId") == 1)
            {
                return Unauthorized("You are not authorized to access this page.");
            }
            Customer customer = new Customer();
            if (Id != Guid.Empty)
            {
                customer = uOW.CustomerRepository().GetById(Id);
            }
            ViewData["companies"] = uOW.CompanyRepository().GetAll().Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
            return View(customer);
        }
        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
               
                if (customer.Id != Guid.Empty)
                {
                    uOW.CustomerRepository().Update(customer);
                }
                else
                {
                    Customer exist = uOW.CustomerRepository().GetAll().Where(x => x.Email == customer.Email).FirstOrDefault();
                    if (exist != null)
                    {
                        ViewBag.Error = "Email already exists, please try another one.";
                        return View(customer);
                    }
                    uOW.CustomerRepository().Insert(customer);
                }
                uOW.Save();
                return RedirectToAction("GetAllCustomers");
            }
            ViewData["companies"] = uOW.CompanyRepository().GetAll().Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString()}).ToList();
            return View(customer);
        }
        public IActionResult GetAllCustomers()
        {
            if (HttpContext.Session.GetInt32("RoleId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (HttpContext.Session.GetInt32("RoleId") == 1)
            {
                return Unauthorized("You are not authorized to access this page.");
            }
            List<Customer> customers = uOW.CustomerRepository().GetAll().ToList();
            return View(customers);
        }
        public IActionResult RemoveCustomer(Guid Id)
        {
            if (Id != Guid.Empty)
            {
                var service = uOW.CustomerRepository().GetById(Id);
                if (service != null)
                {
                    uOW.CustomerRepository().Delete(service);
                    uOW.Save();
                    return Json(1);
                }
            }
            return Json(-1);
        }
        [HttpPost]
        public IActionResult LoadCompanyServices(Guid CompanyId)
        {
            var SelectItemlist = new List<SelectListItem>();
            if (CompanyId != Guid.Empty)
            {
                SelectItemlist = uOW.ServiceRepository().GetAll().Select(x => new SelectListItem() { Text = x.Name, Value = Convert.ToString(x.Id) }).ToList();
            }
            return Json(SelectItemlist);
        }
        public IActionResult GetAllSurveys(Guid CustomerId)
        {
            HttpContext.Session.SetString("SelectedCustomer",CustomerId.ToString());
            var survey=uOW.SurveyRepository().GetAll().Include(x=>x.Service).Take(12).ToList();
            ViewData["companies"] = uOW.CompanyRepository().GetAll().Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
            return View(survey);
        }
        [HttpPost]
        public IActionResult FilterSurvey(Guid ServiceId)
        {
            List<Survey> surveys = new List<Survey>();
            if (ServiceId != Guid.Empty)
            {
                surveys = uOW.FilterSurvey(ServiceId);
            }
            ViewData["companies"] = uOW.CompanyRepository().GetAll().Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
            return View("GetAllSurveys", surveys);
        }
        public IActionResult GetSurveyDetails(Guid SurveyId)
        {
            var questions = uOW.QuestionRepository().GetAll().Include(x => x.Survey)
                .Where(x => x.SurveyId == SurveyId)
                .Select(x => new Question() {
                Id= x.Id,
                OPA= x.OPA,
                OPB= x.OPB,
                OPC= x.OPC,
                OPD= x.OPD,
                CreatedOn= x.CreatedOn,
                Limit= x.Limit,
                QuestionType= x.QuestionType,
                    Survey=x.Survey,
                    QuestionTypeId= x.QuestionTypeId,
                Text= x.Text,
                SurveyId= SurveyId,
                IsMandatory= x.IsMandatory,
                ServiceName= x.Survey.Service.Name,
                CompanyName=x.Survey.Service.Company.Title
                });
            return View(questions.ToList());
        }
        [HttpPost]
        public IActionResult SaveAnswers(Guid[] QuestionId, string[] Answer,Guid SurveyId)
        {
            Guid CustomerId = new Guid(HttpContext.Session.GetString("SelectedCustomer"));
            int ansLength = Answer.Where(x => x != null).Count();
            if (QuestionId.Length>1 && ansLength>1)
            {
                int count = 0;
                foreach (var item in QuestionId)
                {
                    
                    Dictionary<string,int> keyValuePairs= GetAnswers(item.ToString(), Answer, count);
                    count = count + keyValuePairs.Values.FirstOrDefault();
                    Answer answer = new Answer() { 
                    QuestionId=item,
                    Value= keyValuePairs.Keys.FirstOrDefault(),
                    CustomerId=CustomerId,
                    SurveyId=SurveyId
                    };
                    uOW.AnswerRepository().Insert(answer);
                    count++;
                }
            }
            Customer cutomer = uOW.CustomerRepository().GetById(CustomerId);
            if (cutomer != null)
            {
                cutomer.SurveyId = SurveyId;
                uOW.CustomerRepository().Update(cutomer);
            }
            uOW.Save();
            return RedirectToAction("GetAllSurveys", new {CustomerId=CustomerId});
        }
        public Dictionary<string,int> GetAnswers(string QId, string[] Answers,int count)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            string[] checkboxes=Answers.Where(x=>x.EndsWith("-"+QId)).Select(x=>x.Replace("-"+QId,"")).ToArray();
            if (checkboxes.Length>1)
            {
                result.Add(string.Join(", ", checkboxes),checkboxes.Length-1);
            }
            else
            {
                result.Add(Answers[count].ToString(),0);
            }
            return result;
        }
    }
}
