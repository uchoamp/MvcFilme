using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcFilme {

    public static class Utilities
    {
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
