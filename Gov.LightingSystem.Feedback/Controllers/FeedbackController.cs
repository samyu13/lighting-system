using System.Text.RegularExpressions;
using Gov.LightingSystem.Feedback.Helper;
using Gov.LightingSystem.Feedback.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gov.LightingSystem.Feedback.Controllers
{
    [Route("[controller]")]
    public class FeedbackController : Controller
    {
        public static readonly string SESS_MODEL_KEY = "feedback";       
        Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        private readonly IUserFeedbackRepository _userFeedbackRepository;
        public FeedbackController(IUserFeedbackRepository userFeedbackRepository)
        {
            _userFeedbackRepository = userFeedbackRepository;
        }
       
        [HttpGet]
        [Route("~/")]
        [Route("")]
        public IActionResult Index(int? id)
        {
            ViewBag.QuestionId = id?? 1;
            return View(getFeedbackFromSession());
        }      
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(int id, UserFeedback model)
        {             
            var objComplex = getFeedbackFromSession();
            string errorMessage = null;
            switch (id)
            {
                case 1:
                    objComplex.UserName = model.UserName;
                    if (string.IsNullOrEmpty(model.UserName)) 
                       errorMessage = "Missing User Name";
                    if (model.UserName.Length > 50)
                        errorMessage = "Invalid Username";
                    break;
                case 2:
                       objComplex.EmailAddress = model.EmailAddress;
                    if (string.IsNullOrEmpty(model.EmailAddress) || !emailRegex.IsMatch(model.EmailAddress))                   
                        errorMessage = "Missing or Invalid email";                 
                    break;
                case 3:
                    objComplex.HomeAddress = model.HomeAddress;
                    if (string.IsNullOrEmpty(model.HomeAddress))
                        errorMessage = "Missing address";
                    break;
                case 4:
                     objComplex.IsHappyWithService = model.IsHappyWithService;
                    break;
                case 5:
                    objComplex.LevelOfBrightness = model.LevelOfBrightness;                       
                    break;
            }

            HttpContext.Session.SetObject(SESS_MODEL_KEY, objComplex);           
            if(id == 5)
            {
              return  RedirectToAction("Summary");
            }
            if (errorMessage != null)
            {
                ViewBag.QuestionId = id;
                ViewBag.errorMessage = errorMessage;
            } else
            {
                ViewBag.QuestionId = id + 1;
            }

            return View(objComplex);
        }  

        private UserFeedback getFeedbackFromSession()
        {

            var objComplex = HttpContext.Session.GetObject<UserFeedback>(SESS_MODEL_KEY);
            if (objComplex == null)
            {
                objComplex = new UserFeedback();
                objComplex.LevelOfBrightness = 1;
                HttpContext.Session.SetObject(SESS_MODEL_KEY, objComplex);
            }

            return objComplex;

        }

        [HttpGet]
        [Route("/Summary")]
        public IActionResult Summary() {

            var userFeedback = getFeedbackFromSession();
            if(string.IsNullOrEmpty(userFeedback.UserName)  || 
                string.IsNullOrEmpty(userFeedback.EmailAddress) ||
                    string.IsNullOrEmpty(userFeedback.HomeAddress))
            {
                return RedirectToAction("Index",1);
            }
                return View("Summary", getFeedbackFromSession());
        }

        [ActionName("Summary")]
        [HttpPost]
        [Route("/Summary")]
        public IActionResult PostSummary()
        {
            //store feedback details  
            var userFeedback = getFeedbackFromSession(); 
            var feedback =  _userFeedbackRepository.AddFeedback(userFeedback);
            if (feedback != null)
            {
                // clear session once the data is saved
                HttpContext.Session.Clear();
                return RedirectToAction("Confirm");
            }
            else
                return RedirectToAction("Error");
        }

        [Route("Confirm")]
        public IActionResult Confirm()
        {
            return View("Confirm");
        }

        [Route("Error")]
        public IActionResult Error()
        {
            return View("Error");
        }

    }
}