using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BackendService.Model;
using Application.DTO;

namespace MobileWebAPI.Controllers
{
    public class TeamsController : ApiController
    {

        private UnitOfWork unitofwork = new UnitOfWork();


        // GET: api/Teams
        public IEnumerable<Team> Get()
        {
            return unitofwork.TeamsRepository.GetAll(T => T.State == "IL", Q=> Q.OrderBy( O => O.Name));
        }

        // GET: api/Teams/5
        public Team Get(int id)
        {
            return unitofwork.TeamsRepository.GetByID(id);
        }

        // POST: api/Teams
        [Authorize(Roles = "Domain Users")]
        public void Post([FromBody] Team value)
        {
            unitofwork.TeamsRepository.Insert(value);
            unitofwork.TeamsRepository.Save();
        }

        // PUT: api/Teams/5
        [Authorize(Roles = "Domain Admins")]
        public void Put([FromBody] Team value)
        {
            unitofwork.TeamsRepository.Update(value);
            unitofwork.TeamsRepository.Save();
        }

        // DELETE: api/Teams/5
        [Authorize(Roles = "Domain Admins")]
        public void Delete(int id)
        {
            unitofwork.TeamsRepository.Delete(id);
        }
    }
}
