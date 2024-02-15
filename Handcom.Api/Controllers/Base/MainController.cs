using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Handcom.Services.Interfaces;
using Handcom.Services.Responses;
using Handcom.Services.Services.Notifications;

namespace Handcom.Api.Controllers.Base
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifierService _notifierService;

        protected MainController(INotifierService notifierService) =>
            _notifierService = notifierService;

        protected bool ValidOperation() =>
            !_notifierService.HasNotifications();

        protected ActionResult CustomResponse(object? result = null)
        {
            if (ValidOperation())
                return Ok(new ResponseSuccess(result));

            return BadRequest(new ResponseFailure(_notifierService.GetNotifications().Select(n => n.Message)));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                NotifyInvalidModelError(modelState);

            return CustomResponse();
        }

        protected void NotifyInvalidModelError(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(errorMsg);
            }
        }

        protected void NotifyError(string message) =>
            _notifierService.Handle(new Notification(message));

        protected ActionResult CustomResponse(ResponseExternalResult response)
        {
            ResponseHasErrors(response);

            return CustomResponse();
        }

        protected bool ResponseHasErrors(ResponseExternalResult response)
        {
            if (response == null || !response.Errors.Messages.Any()) return false;

            foreach (var mensagem in response.Errors.Messages)
            {
                NotifyError(mensagem);
            }

            return true;
        }

        protected void ClearProcessingErrors() =>
            _notifierService.ClearErrors();
    }
}
