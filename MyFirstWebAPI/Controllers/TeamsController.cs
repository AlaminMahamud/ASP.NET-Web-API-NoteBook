using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using MyFirstWebAPI.Models;
using System.Collections;
using System.Net.Http;

namespace MyFirstWebAPI.Controllers
{
    public class TeamsController : ApiController
    {

        #region dummydata

        private static IList<Team> _teams = new List<Team>
        {
            new Team
            {
            Id = 100,
            Title = "Team 100",
            Owner = "Owner 100"
            },
            new Team
            {
            Id = 101,
            Title = "Team 101",
            Owner = "Owner 101"
            }

            // Add more teams
        };

        #endregion

        //public IEnumerable Get()
        //{
        //    return _teams;
        //}

        public Team Get(int id)
        {
            var team = _teams.First(t => t.Id == id);

            if (team == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return _teams.First(t => t.Id == id);
        }

        public IEnumerable Get([FromUri] TeamFilter teamFilter)
        {
            var teams = _teams.Where(t => t.Title.ToLower().Contains(teamFilter.Title.ToLower()) &&
            t.Owner.ToLower().Contains(teamFilter.Owner.ToLower()));

            if (teams == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return teams;
        }


        // Create Resource With HTTP Post
        public HttpResponseMessage Post(int id, Team team)
        {
            var maxId = _teams.Max(t => t.Id);
            team.Id = maxId + 1;
            _teams.Add(team);

            // Prepare the Response to Return
            var response = Request.CreateResponse(HttpStatusCode.Created, team);
            var uri = Url.Link("DefaultApi", new { id = team.Id });
            response.Headers.Location = new System.Uri(uri);
            return response;
        }


        public HttpResponseMessage Put(int id, Team team)
        {
            var index = _teams.ToList().FindIndex(t=>t.Id == id);
            if (index >= 0)
            {
                // overwrite the existing resource
                _teams[index] = team;
                // Prepare the Response to return
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                _teams.Add(team);

                // Prepare the Response to Return
                var response = Request.CreateResponse(HttpStatusCode.Created, team);
                var uri = Url.Link("DefaultApi", new { id = team.Id});
                response.Headers.Location = new System.Uri (uri);
                return response;
            }
        }

        // Deleting A Resource with HTTP DELETE
        public void Delete(int id)
        {
            var team = Get(id);
            _teams.Remove(team);
        }


        //// Create Resource with HTTP PUT
        //public HttpResponseMessage Put(int id, Team team)
        //{
        //    if (_teams.Any(t => t.Id == id))
        //    {
        //        _teams.Add(team);
        //    }

        //    // Prepare the response to Return
        //    var response = Request.CreateResponse(HttpStatusCode.Created, team);
        //    var uri = Url.Link("DefaultApi", new { id = team.Id});
        //    response.Headers.Location = new System.Uri(uri);
        //    return response;
        //}

        #region comment
        /*
        //public IEnumerable GetTeamsByTitle(string title)
        //{
        //    var teams = _teams.Where(t => t.Title.Contains(title));

        //    if (teams == null)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.NotFound);
        //    }

        //    return teams;
        //}

        //// GET api/teams
        //public Team Get(int compId, int id)
        //{
        //    return _teams.First(t => t.Id == id);
        //}

        ////// GET api/teams/100
        ////public Team Get(int id)
        ////{
        ////    return _teams.First(t => t.Id == id);
        ////}

        ////[HttpGet]
        ////public Team RetrieveTeamById(int id)
        ////{
        ////    return _teams.First(t => t.Id == id);
        ////}

        //[HttpGet]
        //public Team RetrieveTeamByIdRpcMode(int id)
        //{
        //    return _teams.First(t => t.Id == id);
        //}


        //// POST api/teams
        //public void Post(Team team)
        //{
        //    var maxId = _teams.Max(t => t.Id);
        //    team.Id = maxId + 1;
        //    _teams.Add(team);
        //}

        //// PUT api/teams/100
        //public void Put(int id, Team team)
        //{
        //    var index = _teams.ToList().FindIndex(t => t.Id == id);
        //    if (index >= 0)
        //    {
        //        _teams[index] = team;
        //    }
        //}

        ////[HttpPut]
        ////public void UpdateTeam(int id, Team team)
        ////{
        ////    var index = _teams.ToList().FindIndex(t => t.Id == id);
        ////    if (index >= 0)
        ////    {
        ////        _teams[index] = team;
        ////    }
        ////}

        //// DELETE api/teams/100
        //public void Delete(int id)
        //{
        //    var index = _teams.ToList().FindIndex(t => t.Id == id);
        //    if (index >= 0)
        //    {
        //        _teams.RemoveAt(index);
        //    }
        //}
        */
        #endregion
    }
}
