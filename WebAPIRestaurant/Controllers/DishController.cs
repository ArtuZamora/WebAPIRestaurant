using Microsoft.Ajax.Utilities;
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
    public class DishController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Content(HttpStatusCode.NoContent, "No function found");
        }
        public IHttpActionResult Get(int id)
        {
            try
            {
                using (var db = new RestaurantContext())
                {
                    DishCE d = new DishCE();
                    Dish d1 = new Dish();
                    d1 = db.DishDetail(id).FirstOrDefault();
                    d.ID = d1.ID;
                    d.Name = d1.Name;
                    d.ImageURL = d1.ImageURL;
                    d.Area = d1.Area;
                    d.Cooking = d1.Cooking;
                    CategoryCE c = new CategoryCE();
                    Category c1 = db.DishCategory(d1.ID_Category).FirstOrDefault();
                    c.ID = c1.ID;
                    c.Name = c1.Name;
                    c.ImageURL = c1.ImageURL;
                    d.Category = c;
                    d.Ingredients = db.DishIngredients(id).ToList();
                    d.Tags = db.DishTags(id).ToList();
                    return Ok(d);
                }
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, e.Message);
            }
        }
        public IHttpActionResult Get(int id, int opt)
        {
            try
            {
                using (var db = new RestaurantContext())
                {
                    IHttpActionResult result = Content(HttpStatusCode.BadRequest, "Bad Second Parameter. available 1 (Ingredient) and 0 (Category)"); ;
                    if (opt == 0) result = Ok(db.sp_cat_dishes(id).ToList()); //By Category
                    else if (opt == 1) result = Ok(db.sp_dish_by_ingredient(id).ToList()); //By Ingredient
                    return result;
                }
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, e.Message);
            }
        }

        private IHttpActionResult Post([FromBody] JObject json)
        {
            try
            {
                using (var db = new RestaurantContext())
                {
                    IHttpActionResult result;
                    DishCE d = JsonConvert.DeserializeObject<DishCE>(json.ToString());
                    if (db.Categories.Find(d.Category.ID) != null)
                    {
                        Dish d1 = new Dish();
                        d1.ID = d.ID;
                        d1.Name = d.Name;
                        d1.ImageURL = d.ImageURL;
                        d1.Area = d.Area;
                        d1.Cooking = d.Cooking;
                        d1.ID_Category = d.Category.ID;
                        db.Dishes.Add(d1);
                        foreach (var tag in d.Tags)
                        {
                            if (db.Tags.FirstOrDefault(t => t.TagName == tag) == default)
                            {
                                Tag tag1 = new Tag();
                                tag1.TagName = tag;
                                db.Tags.Add(tag1);
                            }
                        }
                        db.SaveChanges();
                        foreach (var tag in d.Tags)
                        {
                            Tag tag1 = db.Tags.First(t => t.TagName == tag);
                            Dish_Tag dt = new Dish_Tag();
                            dt.ID_Dish = d1.ID;
                            dt.ID_Tag = tag1.ID;
                            db.Dish_Tag.Add(dt);
                        }
                        foreach (var ingt in d.Ingredients)
                        {
                            string ingtQty = ingt.Substring(0, ingt.IndexOf('*'));
                            string ingtID = ingt.Substring(ingt.IndexOf('*') + 1);
                            Dish_Ingredient DI = new Dish_Ingredient();
                            DI.ID_Dish = d1.ID;
                            DI.ID_Ingredient = Convert.ToInt32(ingtID);
                            DI.Quantity = ingtQty;
                            db.Dish_Ingredient.Add(DI);
                        }
                        db.SaveChanges();
                        d.ID = d1.ID;
                        result = Ok(d);
                    }
                    else result = Content(HttpStatusCode.Conflict, "Error");
                    return result;
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
                    IHttpActionResult result;
                    DishCE d = JsonConvert.DeserializeObject<DishCE>(json.ToString());
                    Dish d1 = db.Dishes.Find(d.ID);
                    if (d1 != null)
                    {
                        d1.Name = !string.IsNullOrEmpty(d.Name) ? d.Name : d1.Name;
                        d1.ImageURL = !string.IsNullOrEmpty(d.ImageURL) ? d.ImageURL : d1.ImageURL;
                        d1.Area = !string.IsNullOrEmpty(d.Area) ? d.Area : d1.Area;
                        d1.Cooking = !string.IsNullOrEmpty(d.Cooking) ? d.Cooking : d1.Cooking;
                        d1.ID_Category = !string.IsNullOrEmpty(d.Category.ID.ToString()) ? d.Category.ID : d1.Category.ID;
                        if (d.Tags.Count() != 0)
                        {
                            var dish_Tags = db.Dish_Tag.Where(dt => dt.ID_Dish == d.ID).ToList<Dish_Tag>();
                            foreach (Dish_Tag item in dish_Tags)
                            {
                                db.Dish_Tag.Remove(item);
                            }
                            db.SaveChanges();
                            foreach (var tag in d.Tags)
                            {
                                if (db.Tags.FirstOrDefault(t => t.TagName == tag) == default)
                                {
                                    Tag tag1 = new Tag();
                                    tag1.TagName = tag;
                                    db.Tags.Add(tag1);
                                }
                            }
                            db.SaveChanges();
                            foreach (var tag in d.Tags)
                            {
                                Tag tag1 = db.Tags.First(t => t.TagName == tag);
                                Dish_Tag dt1 = new Dish_Tag();
                                dt1.ID_Dish = d1.ID;
                                dt1.ID_Tag = tag1.ID;
                                db.Dish_Tag.Add(dt1);
                            }
                        }
                        if (d.Ingredients.Count() != 0)
                        {
                            var dish_ingt = db.Dish_Ingredient.Where(dt => dt.ID_Dish == d.ID).ToList<Dish_Ingredient>();
                            foreach (Dish_Ingredient item in dish_ingt)
                            {
                                db.Dish_Ingredient.Remove(item);
                            }
                            db.SaveChanges();
                            foreach (var ingt in d.Ingredients)
                            {
                                string ingtQty = ingt.Substring(0, ingt.IndexOf('*'));
                                string ingtID = ingt.Substring(ingt.IndexOf('*') + 1);
                                Dish_Ingredient DI = new Dish_Ingredient();
                                DI.ID_Dish = d1.ID;
                                DI.ID_Ingredient = Convert.ToInt32(ingtID);
                                DI.Quantity = ingtQty;
                                db.Dish_Ingredient.Add(DI);
                            }
                        }
                        db.SaveChanges();
                        result = Content(HttpStatusCode.OK, "Row updated");
                    }
                    else result = Content(HttpStatusCode.NotFound, "Dish not found");
                    return result;
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
                    Dish d = db.Dishes.Find(id);
                    db.Dishes.Remove(d);
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