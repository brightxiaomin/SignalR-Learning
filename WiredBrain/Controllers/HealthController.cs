using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;


namespace WiredBrain.Controllers
{
    public class HealthController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> Check()
        {
            // Add logic here to check dependencies

            HubConnection hubConnection = new HubConnection("http://localhost:61055/");
            //var hubproxy = hubConnection.CreateHubProxy("CoffeeHub");
            try
            {
                await hubConnection.Start();
                // return Ok();
                return Json("Healthy");

            }
            catch
            {
                return InternalServerError();
            }

            finally
            {
                 hubConnection?.Dispose();
            }
            //if (2==2)
            //{
            //    string test = "Healthy";
            //    return Ok(test);
            //}

            //return InternalServerError(); // Or whatever other HTTP status code is appropriate
        }
    }
}