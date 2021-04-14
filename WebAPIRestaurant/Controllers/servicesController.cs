using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIRestaurant.Models;

namespace WebAPIRestaurant.Controllers
{
    public class servicesController : ApiController
    {
        [Route("api/services/DishesPerCat/{id}")]
        [HttpGet]
        private IHttpActionResult DishesPerCat(int id)
        {
            try
            {
                using (var db = new RestaurantContext())
                {
                    return Ok(db.sp_tot_dishes_per_cat(id).ToList());
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.Conflict, ex.Message);
            }
        }
        [Route("api/services/TotIngrtPerDish")]
        [HttpGet]
        private IHttpActionResult TotIngrtPerDish()
        {

            try
            {
                using (var db = new RestaurantContext())
                {
                    return Ok(db.vw_tot_ingrt_per_dish.ToList());
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.Conflict, ex.Message);
            }
        }
        [Route("api/services/TotDishesByIngrt/{id}")]
        [HttpGet]
        private IHttpActionResult TotDishesByIngrt(int id)
        {

            try
            {
                using (var db = new RestaurantContext())
                {
                    return Ok(db.sp_tot_dishes_by_ingt(id).ToList());
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.Conflict, ex.Message);
            }
        }
        [Route("api/services/DishesTags")]
        [HttpGet]
        private IHttpActionResult DishesTags()
        {

            try
            {
                using (var db = new RestaurantContext())
                {
                    return Ok(db.vw_dishes_tags.ToList());
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.Conflict, ex.Message);
            }
        }
        [Route("api/services/LastCreatedDishes")]
        [HttpGet]
        private IHttpActionResult LastCreatedDishes()
        {

            try
            {
                using (var db = new RestaurantContext())
                {
                    return Ok(db.vw_last_created_dishes.ToList());
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.Conflict, ex.Message);
            }
        }
    }
}