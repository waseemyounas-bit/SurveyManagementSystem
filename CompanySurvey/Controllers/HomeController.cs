using CompanySurvey.Models;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CompanySurvey.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUOW uOW;
        public HomeController(ILogger<HomeController> logger, IUOW _uow)
        {
            _logger = logger;
            uOW = _uow;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("RoleId")==null)
            {
                return RedirectToAction("Login","Account");
            }
            if (HttpContext.Session.GetInt32("RoleId")==2)
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
        public IActionResult CreateManager(Guid Id)
        {
            if (HttpContext.Session.GetInt32("RoleId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (HttpContext.Session.GetInt32("RoleId") == 2)
            {
                return Unauthorized("You are not authorized to access this page.");
            }
            var user = new User();
            if (Id != Guid.Empty)
            {
                user = uOW.UserRepository().GetById(Id);
            }
            return View(user);
        }
        [HttpPost]
        public IActionResult CreateManager(User user)
        {
            if (ModelState.IsValid)
            {
                User exist = uOW.UserRepository().GetAll().Where(x => x.Email == user.Email).FirstOrDefault();
                if (exist != null)
                {
                    ViewBag.Error = "Email already exists, try with another one.";
                    return View(user);
                }
                if (user.Id != Guid.Empty)
                {
                    uOW.UserRepository().Update(user);
                }
                else
                {
                    uOW.UserRepository().Insert(user);
                }
                uOW.Save();
                return RedirectToAction("GetAllManagers");
            }
            return View(user);
        }
        public IActionResult GetAllManagers()
        {
            if (HttpContext.Session.GetInt32("RoleId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (HttpContext.Session.GetInt32("RoleId") == 2)
            {
                return Unauthorized("You are not authorized to access this page.");
            }
            List<User> users = uOW.UserRepository().GetAll().Where(x => x.RoleId == 2).ToList();
            return View(users);
        }
        public IActionResult RemoveManager(Guid Id)
        {
            if (Id != Guid.Empty)
            {
                var user = uOW.UserRepository().GetById(Id);
                if (user != null)
                {
                    uOW.UserRepository().Delete(user);
                    uOW.Save();
                    return Json(1);
                }
            }
            return Json(-1);
        }
        public IActionResult CreateCompany(Guid Id)
        {
            if (HttpContext.Session.GetInt32("RoleId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (HttpContext.Session.GetInt32("RoleId") == 2)
            {
                return Unauthorized("You are not authorized to access this page.");
            }
            Company company = new Company();
            if (Id != Guid.Empty)
            {
                company = uOW.CompanyRepository().GetById(Id);
            }
            return View(company);
        }
        [HttpPost]
        public IActionResult CreateCompany(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id != Guid.Empty)
                {
                    uOW.CompanyRepository().Update(company);
                }
                else
                {
                    uOW.CompanyRepository().Insert(company);
                }
                uOW.Save();
                return RedirectToAction("GetAllCompanies");
            }
            return View(company);
        }
        public IActionResult GetAllCompanies()
        {
            if (HttpContext.Session.GetInt32("RoleId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (HttpContext.Session.GetInt32("RoleId") == 2)
            {
                return Unauthorized("You are not authorized to access this page.");
            }
            List<Company> companies = uOW.CompanyRepository().GetAll().ToList();
            return View(companies);
        }
        public IActionResult RemoveCompany(Guid Id)
        {
            if (Id != Guid.Empty)
            {
                var company = uOW.CompanyRepository().GetById(Id);
                if (company != null)
                {
                    uOW.CompanyRepository().Delete(company);
                    uOW.Save();
                    return Json(1);
                }
            }
            return Json(-1);
        }
        public IActionResult CreateService(Guid Id)
        {
            if (HttpContext.Session.GetInt32("RoleId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (HttpContext.Session.GetInt32("RoleId") == 2)
            {
                return Unauthorized("You are not authorized to access this page.");
            }
            Service service = new Service();
            if (Id != Guid.Empty)
            {
                service = uOW.ServiceRepository().GetById(Id);
            }
            ViewData["companies"] = uOW.CompanyRepository().GetAll().Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
            return View(service);
        }
        [HttpPost]
        public IActionResult CreateService(Service service)
        {
            if (ModelState.IsValid)
            {
                if (service.Id != Guid.Empty)
                {
                    uOW.ServiceRepository().Update(service);
                }
                else
                {
                    uOW.ServiceRepository().Insert(service);
                }
                uOW.Save();
                return RedirectToAction("GetAllServices");
            }
            ViewData["companies"] = uOW.CompanyRepository().GetAll().Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString(), Selected = x.Id == service.CompanyId }).ToList();
            return View(service);
        }
        public IActionResult GetAllServices()
        {
            if (HttpContext.Session.GetInt32("RoleId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (HttpContext.Session.GetInt32("RoleId") == 2)
            {
                return Unauthorized("You are not authorized to access this page.");
            }
            List<Service> services = uOW.ServiceRepository().GetAll().ToList();
            return View(services);
        }
        public IActionResult RemoveService(Guid Id)
        {
            if (Id != Guid.Empty)
            {
                var service = uOW.ServiceRepository().GetById(Id);
                if (service != null)
                {
                    uOW.ServiceRepository().Delete(service);
                    uOW.Save();
                    return Json(1);
                }
            }
            return Json(-1);
        }
        [HttpPost]
        public IActionResult LoadCompanyServices(Guid CompanyId,Guid Selected)
        {
            if (HttpContext.Session.GetInt32("RoleId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (HttpContext.Session.GetInt32("RoleId") == 2)
            {
                return Unauthorized("You are not authorized to access this page.");
            }
            var SelectItemlist = new List<SelectListItem>();
            if (CompanyId != Guid.Empty)
            {
                SelectItemlist = uOW.ServiceRepository().GetAll().Select(x => new SelectListItem() { Text = x.Name, Value = Convert.ToString(x.Id),Selected=x.Id== Selected}).ToList();
            }
            return Json(SelectItemlist);
        }
        public IActionResult CreateSurvey(Guid Id)
        {
            if (HttpContext.Session.GetInt32("RoleId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (HttpContext.Session.GetInt32("RoleId") == 2)
            {
                return Unauthorized("You are not authorized to access this page.");
            }
            Survey survey = new Survey();
            if (Id != Guid.Empty)
            {
                survey = uOW.SurveyRepository().GetById(Id);
            }
            ViewData["companies"] = uOW.CompanyRepository().GetAll().Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString(), Selected = x.Id == survey.CompanyId }).ToList();
            return View(survey);
        }
        [HttpPost]
        public IActionResult CreateSurvey(Survey survey)
        {
            if (ModelState.IsValid)
            {
                if (survey.Id != Guid.Empty)
                {
                    uOW.SurveyRepository().Update(survey);
                }
                else
                {
                    uOW.SurveyRepository().Insert(survey);
                }
                uOW.Save();
                return RedirectToAction("GetAllSurveys");
            }
            ViewData["companies"] = uOW.CompanyRepository().GetAll().Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString(), Selected = x.Id == survey.CompanyId }).ToList();
            return View(survey);
        }
        public IActionResult GetAllSurveys(Guid SurveyId)
        {
            if (HttpContext.Session.GetInt32("RoleId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (HttpContext.Session.GetInt32("RoleId") == 2)
            {
                return Unauthorized("You are not authorized to access this page.");
            }
            var surveys = uOW.SurveyRepository().GetAll();
            if (SurveyId!=Guid.Empty)
            {
                surveys.Where(x => x.Id == SurveyId);
            }
            ViewData["companies"] = uOW.CompanyRepository().GetAll().Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
            return View(surveys.ToList());
        }
        public IActionResult RemoveSurvey(Guid Id)
        {
            if (Id != Guid.Empty)
            {
                var survey = uOW.SurveyRepository().GetById(Id);
                if (survey != null)
                {
                    uOW.SurveyRepository().Delete(survey);
                    uOW.Save();
                    return Json(1);
                }
            }
            return Json(-1);
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
        public IActionResult CreateQuestionType(Guid Id)
        {
            if (HttpContext.Session.GetInt32("RoleId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (HttpContext.Session.GetInt32("RoleId") == 2)
            {
                return Unauthorized("You are not authorized to access this page.");
            }
            QuestionType questionType = new QuestionType();
            if (Id != Guid.Empty)
            {
                questionType = uOW.QuestionTypeRepository().GetById(Id);
            }
            //ViewData["questiontypes"] = uOW.QuestionTypeRepository().GetAll().Select(x => new SelectListItem { Text = x.Type, Value = x.Id.ToString(), Selected = x.Id == questionType.Id }).ToList();
            //ViewData["services"] = uOW.ServiceRepository().GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == questionType.serId }).ToList();
            return View(questionType);
        }
        [HttpPost]
        public IActionResult CreateQuestionType(QuestionType questionType)
        {
            if (ModelState.IsValid)
            {
                if (questionType.Id != Guid.Empty)
                {
                    uOW.QuestionTypeRepository().Update(questionType);
                }
                else
                {
                    uOW.QuestionTypeRepository().Insert(questionType);
                }
                uOW.Save();
                return RedirectToAction("GetAllQuestionTypes");
            }
            //ViewData["companies"] = uOW.CompanyRepository().GetAll().Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString(), Selected = x.Id == survey.CompanyId }).ToList();
            return View(questionType);
        }
        public IActionResult GetAllQuestionTypes()
        {
            if (HttpContext.Session.GetInt32("RoleId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (HttpContext.Session.GetInt32("RoleId") == 2)
            {
                return Unauthorized("You are not authorized to access this page.");
            }
            var types = uOW.QuestionTypeRepository().GetAll();
            return View(types.ToList());
        }
        public IActionResult RemoveQuestionType(Guid Id)
        {
            if (Id != Guid.Empty)
            {
                var type = uOW.QuestionTypeRepository().GetById(Id);
                if (type != null)
                {
                    uOW.QuestionTypeRepository().Delete(type);
                    uOW.Save();
                    return Json(1);
                }
            }
            return Json(-1);
        }
        public IActionResult CreateQuestion(Guid Id, Guid SurveyId)
        {
            if (HttpContext.Session.GetInt32("RoleId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (HttpContext.Session.GetInt32("RoleId") == 2)
            {
                return Unauthorized("You are not authorized to access this page.");
            }
            Question question = new Question();
            if (Id != Guid.Empty)
            {
                question = uOW.QuestionRepository().GetById(Id);
                SurveyId = question.SurveyId ?? Guid.Empty;
            }
            if (SurveyId != Guid.Empty)
            {
                question.SurveyId = SurveyId;
                question.Survey = uOW.SurveyRepository().GetById(SurveyId);
            }
            //ViewData["questiontypes"] = uOW.QuestionTypeRepository().GetAll().Select(x => new SelectListItem { Text = x.Type, Value = x.Id.ToString(), Selected = x.Id == questionType.Id }).ToList();
            //ViewData["surveys"] = uOW.ServiceRepository().GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == questionType.serId }).ToList();
            ViewData["types"] = uOW.QuestionTypeRepository().GetAll().Select(x => new SelectListItem { Text = x.Type, Value = x.Id.ToString(), Selected = x.Id == question.QuestionTypeId }).ToList();
            return View(question);
        }
        [HttpPost]
        public IActionResult CreateQuestion(Question question)
        {
            if (ModelState.IsValid)
            {
                if (question.Id != Guid.Empty)
                {
                    uOW.QuestionRepository().Update(question);
                }
                else
                {
                    uOW.QuestionRepository().Insert(question);
                }
                uOW.Save();
                return RedirectToAction("GetAllQuestions", new { SurveyId = question.SurveyId });
            }
            //ViewData["companies"] = uOW.CompanyRepository().GetAll().Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString(), Selected = x.Id == survey.CompanyId }).ToList();
            ViewData["types"] = uOW.QuestionTypeRepository().GetAll().Select(x => new SelectListItem { Text = x.Type, Value = x.Id.ToString(), Selected = x.Id == question.QuestionTypeId }).ToList();
            return View(question);
        }
        public IActionResult GetAllQuestions(Guid SurveyId)
        {
            if (HttpContext.Session.GetInt32("RoleId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (HttpContext.Session.GetInt32("RoleId") == 2)
            {
                return Unauthorized("You are not authorized to access this page.");
            }
            string SurveyName = uOW.SurveyRepository().GetById(SurveyId)?.Title;
            var questions = uOW.QuestionRepository().GetAll();
            if (SurveyId != Guid.Empty)
            {
                questions.Where(x => x.SurveyId == SurveyId);
            }
            List<QuestionVM> list = 
                questions.Select(x=> new QuestionVM() { 
                    CreatedOn= x.CreatedOn,
                    Id=x.Id,
                    IsMandatory=x.IsMandatory,
                    Limit=x.Limit,
                    OPA =x.OPA,
                    OPB=x.OPB,
                    OPC=x.OPC,
                    OPD=x.OPD,
                    QuestionTypeId=x.QuestionTypeId,
                    Text=x.Text,
                    SurveyId=SurveyId,
                    SurveyName=SurveyName
            }).ToList();
            return View(list);
        }
        public IActionResult RemoveQuestion(Guid Id)
        {
            if (Id != Guid.Empty)
            {
                var question = uOW.QuestionRepository().GetById(Id);
                
                if (question != null)
                {
                    var answers = uOW.AnswerRepository().GetAll().Where(x=>x.QuestionId==Id).Select(x=>new Answer() { Id=x.Id}).ToList();
                    foreach (var item in answers)
                    {
                        uOW.AnswerRepository().Delete(item);
                    }
                    uOW.QuestionRepository().Delete(question);
                    uOW.Save();
                    return Json(1);
                }
            }
            return Json(-1);
        }
        public IActionResult CreateCondition(Guid Id, Guid SurveyId)
        {
            if (HttpContext.Session.GetInt32("RoleId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (HttpContext.Session.GetInt32("RoleId") == 2)
            {
                return Unauthorized("You are not authorized to access this page.");
            }
            Condition condition = new Condition();
            if (Id != Guid.Empty)
            {
                condition = uOW.ConditionRepository().GetById(Id);
                SurveyId = condition.SurveyId ?? Guid.Empty;
            }
            if (SurveyId != Guid.Empty)
            {
                condition.SurveyId = SurveyId;
                condition.Survey = uOW.SurveyRepository().GetById(SurveyId);
                condition.Questions = uOW.QuestionRepository().GetAll().Where(x => x.SurveyId == SurveyId).Select(x => new SelectListItem { Text = x.Text, Value = x.Id.ToString() }).ToList();
            }
            //ViewData["questiontypes"] = uOW.QuestionTypeRepository().GetAll().Select(x => new SelectListItem { Text = x.Type, Value = x.Id.ToString(), Selected = x.Id == questionType.Id }).ToList();
            //ViewData["surveys"] = uOW.ServiceRepository().GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == questionType.serId }).ToList();
            //ViewData["types"] = uOW.QuestionTypeRepository().GetAll().Select(x => new SelectListItem { Text = x.Type, Value = x.Id.ToString(), Selected = x.Id == question.QuestionTypeId }).ToList();
            return View(condition);
        }
        [HttpPost]
        public IActionResult CreateCondition(Condition condition)
        {
            if (ModelState.IsValid)
            {
                if (condition.Id != Guid.Empty)
                {
                    uOW.ConditionRepository().Update(condition);
                }
                else
                {
                    uOW.ConditionRepository().Insert(condition);
                }
                uOW.Save();
                return RedirectToAction("GetAllConditions", new { SurveyId = condition.SurveyId });
            }
            //ViewData["companies"] = uOW.CompanyRepository().GetAll().Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString(), Selected = x.Id == survey.CompanyId }).ToList();
            //ViewData["types"] = uOW.QuestionTypeRepository().GetAll().Select(x => new SelectListItem { Text = x.Type, Value = x.Id.ToString(), Selected = x.Id == condition.QuestionTypeId }).ToList();
            return View(condition);
        }
        public IActionResult GetAllConditions(Guid SurveyId)
        {
            if (HttpContext.Session.GetInt32("RoleId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (HttpContext.Session.GetInt32("RoleId") == 2)
            {
                return Unauthorized("You are not authorized to access this page.");
            }
            var questions = uOW.QuestionRepository().GetAll().Where(x=>x.SurveyId==SurveyId).ToList();
            var conditions = uOW.ConditionRepository().GetAll().Where(x=>x.SurveyId==SurveyId).ToList();
           List<ConditionsVM> conditionsVMs= new List<ConditionsVM>();
            if (conditions.Count>0)
            {
                foreach (var item in conditions)
                {
                    List<string> selectedQuestions=questions.Where(x=>item.QuestionIds.ToLower().Contains(x.Id.ToString())).Select(x=>x.Text).ToList();
                    ConditionsVM conditionsVM = new ConditionsVM() {
                        Do = item.Do,
                        State=item.State.ToString(),
                        QuestionText = questions.Where(x => x.Id == item.QuestionId).FirstOrDefault().Text,
                        QuestionId = item.QuestionId,
                        QuestionIds = item.QuestionIds,
                        Value=item.Value,
                        ShowHideQuestionsText = string.Join(",", selectedQuestions),
                        
                    };
                conditionsVMs.Add(conditionsVM);
                }
            }
            return View(conditionsVMs);
        }
        public IActionResult RemoveCondition(Guid Id)
        {
            if (Id != Guid.Empty)
            {
                var condition = uOW.ConditionRepository().GetById(Id);
                if (condition != null)
                {
                    uOW.ConditionRepository().Delete(condition);
                    uOW.Save();
                    return Json(1);
                }
            }
            return Json(-1);
        }
        public IActionResult GetAnswers()
        {
            var customers = uOW.CustomerRepository().GetAll().Where(x => x.SurveyId != null)
                .Include(x => x.Survey);
            return View(customers.ToList());
        }
        public IActionResult ViewAnswers(Guid CustomerId, Guid SurveyId)
        {
            var questions = uOW.QuestionRepository().GetAll()
                .Include(x => x.Answers)
                .Where(x => x.SurveyId == SurveyId);
            return View(questions.ToList());
        }
        public JsonResult GetQuestionDetails(Guid QuestionId)
        {
            Question question = uOW.QuestionRepository().GetById(QuestionId);
            if (question != null)
            {
                QuestionType type = uOW.QuestionTypeRepository().GetById(question.QuestionTypeId ?? Guid.Empty);
                if (type != null)
                {
                    question.Type = type.Type;
                }
            }
            var json = new
            {
                Id = question.Id,
                QuestionTypeId = question.QuestionTypeId,
                Type = question.Type.ToLower(),
                OPA = question.OPA,
                OPB = question.OPB,
                OPC = question.OPC,
                OPD = question.OPD
            };
            return Json(JsonConvert.SerializeObject(json));
        }
        public JsonResult GetConditions(Guid QuestionId)
        {
            var conditions = uOW.ConditionRepository().GetAll().Where(x => x.QuestionId == QuestionId).ToList();
            List<ConditionsVM> json = new List<ConditionsVM>();
            if (conditions.Count > 0)
            {
                foreach (var item in conditions)
                {
                    ConditionsVM obj = new ConditionsVM() { 
                    QuestionId=item.QuestionId,
                    QuestionIds=item.QuestionIds.ToLower(),
                    State=item.State,
                    Value=item.Value,
                    Do=item.Do,
                    SurveyId=item.SurveyId
                    };
                    json.Add(obj);
                }
            }
            return Json(JsonConvert.SerializeObject(json));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}