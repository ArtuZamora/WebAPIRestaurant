using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIRestaurant.Models;

namespace WebAPIRestaurant.Controllers
{
    public class IngredientController : ApiController
    {
        public IHttpActionResult Get()
        {
            try
            {
                string sql = @"SELECT * FROM Ingredient I";
                using (var db = new RestaurantContext())
                {
                    return Ok(db.Database.SqlQuery<Ingredient>(sql).ToList()
                        .Select(r => new { r.ID, r.Name, r.ImageURL}).ToList());
                }
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, e.Message);
            }
        }
        public IHttpActionResult Get(int id)
        {
            return Content(HttpStatusCode.NoContent, "No function found");
        }

        private IHttpActionResult Post([FromBody] JObject json)
        {
            try
            {
                using (var db = new RestaurantContext())
                {
                    Ingredient i = JsonConvert.DeserializeObject<Ingredient>(json.ToString());
                    db.Ingredients.Add(i);
                    db.SaveChanges();
                    return Ok(i);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.Conflict, ex.Message);
            }
        }

        private IHttpActionResult Put([FromBody] JObject json)
        {
            try
            {
                using (var db = new RestaurantContext())
                {
                    Ingredient i = JsonConvert.DeserializeObject<Ingredient>(json.ToString());
                    Ingredient i1 = db.Ingredients.Find(i.ID);
                    i1.Name = !string.IsNullOrEmpty(i.Name) ? i.Name : i1.Name;
                    i1.ImageURL = !string.IsNullOrEmpty(i.ImageURL) ? i.ImageURL : i1.ImageURL;
                    db.SaveChanges();
                    return Content(HttpStatusCode.OK, "Row Updated");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.Conflict, ex.Message);
            }
        }

        private IHttpActionResult Delete(int id)
        {
            try
            {
                using (var db = new RestaurantContext())
                {
                    Ingredient i = db.Ingredients.Find(id);
                    db.Ingredients.Remove(i);
                    db.SaveChanges();
                    return Content(HttpStatusCode.OK, "Row Deleted");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.Conflict, ex.Message);
            }
        }
    }
}