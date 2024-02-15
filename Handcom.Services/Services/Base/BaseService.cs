using Handcom.Services.Interfaces;
using Handcom.Services.Responses;
using Handcom.Services.Services.Notifications;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Handcom.Services.Services.Base
{
    public abstract class BaseService
    {
        private readonly INotifierService _notifierService;

        protected BaseService(INotifierService notifierService) =>
            _notifierService = notifierService;

        protected void Notify(string message) =>
            _notifierService.Handle(new Notification(message));

        protected T Notify<T>(string message, T item) where T : class
        {
            _notifierService.Handle(new Notification(message));
            return item;
        }

        protected StringContent SerializeObject(object data) =>
            new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");

        protected string SerializeObjectToJson(object data) =>
             JsonSerializer.Serialize(data);

        protected async Task<T?> DeserializeObject<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }

        protected T? DeserializeObject<T>(RestResponse restResponse)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(restResponse.Content ?? "", options);
        }

        protected bool HandleResponseErrors(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest) return false;

            response.EnsureSuccessStatusCode();
            return true;
        }

        protected ResponseExternalResult ReturnOk() =>
            new ResponseExternalResult();
    }
}