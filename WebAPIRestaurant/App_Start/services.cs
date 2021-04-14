using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIRestaurant.Models;

namespace WebAPIRestaurant.App_Start
{
    public class services : ApiController
    {
        [Route("DishesPerCat/{id}")]
        [HttpGet]
        public IHttpActionResult DishesPerCat(int id)
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
        [Route("TotIngrtPerDish")]
        [HttpGet]
        public IHttpActionResult TotIngrtPerDish()
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
        [Route("TotDishesByIngrt/{id}")]
        [HttpGet]
        public IHttpActionResult TotDishesByIngrt(int id)
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
        [Route("DishesTags")]
        [HttpGet]
        public IHttpActionResult DishesTags()
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
        [Route("LastCreatedDishes")]
        [HttpGet]
        public IHttpActionResult LastCreatedDishes()
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