using Firebase.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetoBackendApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetoBackendApp.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private FirebaseClient firebaseClient;
        private string collectionName;

        public ClientesController()
        {
            string auth = "SD8IJdrI4zahdfuKVo0O8MqqDZHiNfoBu3HBQKGg";
            string databaseURL = "https://reto-fs-default-rtdb.firebaseio.com/";
            collectionName = "clients";
            this.firebaseClient = new FirebaseClient(
                databaseURL,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(auth)
                }
            ); ;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var operation = await firebaseClient.Child(collectionName).OnceAsync<Client>();
            var list = new List<Client>();
            operation.ToList().ForEach(item => list.Add(item.Object));
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Client model)
        {
            var operation = await firebaseClient.Child(collectionName).PostAsync(model.ToString());
            return Ok(operation.Key);
        }
    }
}
