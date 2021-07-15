using Facturacion.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facturacion.Filters
{
    public class VerificarSession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var user = context.HttpContext.Session.GetString("Usuario");

                if (user == null)
                {
                    if (context.Controller is AccesoController == false)
                    {
                        context.HttpContext.Response.Redirect("/Acceso/Login");
                    }
                }
            }
            catch (Exception)
            {
                context.HttpContext.Response.Redirect("/Acceso/Login");
            }
        }
    }
}
