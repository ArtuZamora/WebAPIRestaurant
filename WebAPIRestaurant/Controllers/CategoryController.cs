using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPIRestaurant.Models;

namespace WebAPIRestaurant.Controllers
{
    public class CategoryController : ApiController
    {
        public IHttpActionResult Get()
        {
            try
            {
                string sql = @"SELECT * FROM Category C";
                using (var db = new RestaurantContext())
                {
                    return Ok(db.Database.SqlQuery<Category>(sql).ToList()
                        .Select(r => new { r.ID, r.Name, r.ImageURL }).ToList());
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
                    Category c = JsonConvert.DeserializeObject<Category>(json.ToString());
                    db.Categories.Add(c);
                    db.SaveChanges();
                    return Ok(c);
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
                    Category c = JsonConvert.DeserializeObject<Category>(json.ToString());
                    Category c1 = db.Categories.Find(c.ID);
                    c1.Name = !string.IsNullOrEmpty(c.Name) ? c.Name : c1.Name;
                    c1.ImageURL = !string.IsNullOrEmpty(c.ImageURL) ? c.ImageURL : c1.ImageURL;
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
                    Category c = db.Categories.Find(id);
                    db.Categories.Remove(c);
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