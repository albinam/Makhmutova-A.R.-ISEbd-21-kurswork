﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyBusinessLogic.BindingModel;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyWebClient.Models;

namespace TravelAgencyWebClient.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientLogic _client;

        public ClientController(IClientLogic client)
        {
            _client = client;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel client)
        {
            var clientView = _client.Read(new ClientBindingModel
            {
                Login = client.Login,
                Password = client.Password
            }).FirstOrDefault();
            if (clientView == null)
            {
                ModelState.AddModelError("", "Вы ввели неверный пароль, либо пользователь не найден");
                return View(client);
            }
            Program.Client = clientView;
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Logout()
        {
            Program.Client = null;
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Registration(RegistrationModel client)
        {
            if (ModelState.IsValid)
            {
                var existClient = _client.Read(new ClientBindingModel
                {
                    Login = client.Login
                }).FirstOrDefault();
                if (existClient != null)
                {
                    ModelState.AddModelError("", "Данный логин уже занят");
                    return View(client);
                }
                existClient = _client.Read(new ClientBindingModel
                {
                    Email = client.Email
                }).FirstOrDefault();
                if (existClient != null)
                {
                    ModelState.AddModelError("", "Данный E-Mail уже занят");
                    return View(client);
                }
                _client.CreateOrUpdate(new ClientBindingModel
                {
                    ClientFIO=client.ClientFIO,
                    Login = client.Login,
                    Password = client.Password,
                    Email = client.Email,
                    PhoneNumber = client.PhoneNumber
                });
                ModelState.AddModelError("", "Вы успешно зарегистрированы");
                return View("Registration", client);
            }
            return View(client);
        }
    }
}