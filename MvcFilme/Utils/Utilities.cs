using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MvcFilme.Utils 
{
    /// <summary>
    /// Extensão para <c>Enum</c> que retorna a string do atributo <c>Display</c> 
    /// </summary>
    public static class EnumExtension
    {
        public static string GetEnumDisplayName(this Enum u)
        {
            return ((DisplayAttribute)u.GetType().GetMember(u.ToString())
            .First().GetCustomAttributes(typeof(DisplayAttribute), false).First()).Name;
        }
    }
    public static class Utilities
    {
        /// <summary>
        ///  Checa se a rota passado é a ativa
        /// </summary>
        /// <param name="viewContext"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns>Uma string com active para a rota átiva e vazia para as que não</returns>
        public static string IsActive(ViewContext viewContext, string controller, string action)
        {

            var routeData = viewContext.RouteData;
            var routeController = routeData.Values["controller"];
            var routeAction = routeData.Values["action"];

            var active = routeController.Equals(controller) && routeAction.Equals(action);
            return active ? "active" : "";
        }
    }
}
