using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SaviourButtigiegEP.Presentation.Filtering
{
    public class OneVoteOnly : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = context.HttpContext.Session.GetString("UserId");
            if (userId == null || !context.ActionArguments.TryGetValue("id", out var pollIdObj))
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
                return;
            }

            var pollId = pollIdObj?.ToString();
            var sessionKey = $"Voted_{pollId}_{userId}";

            if (context.HttpContext.Session.GetString(sessionKey) != null)
            {
                context.Result = new ContentResult
                {
                    Content = "You cannot vote on this poll again",
                    StatusCode = 403
                };
            }

            base.OnActionExecuting(context);
        }
    }
}
