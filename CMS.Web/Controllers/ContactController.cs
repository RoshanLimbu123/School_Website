using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers
{
    [Route("contact")]
    public class ContactController : Controller
    {
        private readonly SetupRepository _setupRepo;
        private readonly EmailSenderService _emailSenderService;

        public ContactController(SetupRepository setupRepo, EmailSenderService emailSenderService)
        {
            _setupRepo = setupRepo;
            _emailSenderService = emailSenderService;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var setupValues = _setupRepo.getQueryable().ToList();
            ViewBag.setup = setupValues;
            TempData["email-message"] = TempData["email-message"];
            return View();
        }

        [HttpPost]
        [Route("send-mailcontact")]
        public IActionResult sendEmail(ReceivedEmailDto dto)
        {
            var setupValue = _setupRepo.getQueryable().ToList();

            dto.receiver_email = setupValue.Where(a => a.key == Web.Models.SetupKeys.getEmailKey).SingleOrDefault()?.value;
            var email_password = setupValue.Where(a => a.key == Web.Models.SetupKeys.getEmailPasswordKey).SingleOrDefault()?.value;
            var email_host = setupValue.Where(a => a.key == Web.Models.SetupKeys.getEmailHostKey).SingleOrDefault()?.value;
            var email_port = setupValue.Where(a => a.key == Web.Models.SetupKeys.getEmailPortKey).SingleOrDefault()?.value;

            if (ModelState.IsValid)
            {
                try
                {
                    _emailSenderService.send(dto,email_password,email_host,email_port);
                    TempData["email-message"] = "Message sent";
                   
                }
                catch (EmailSendFailureException ex)
                {
                    TempData["email-message"] = "Failed to send email";
                }
                catch (Exception ex)
                {
                    TempData["email-message"] = ex.Message;
                   
                }
            }
            return RedirectToAction("index");
        }
    }
}