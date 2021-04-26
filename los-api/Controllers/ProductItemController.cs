using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace los_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductItemController : ControllerBase
    {
        //private readonly LosApiContext _losApiContext;

        //public ProductItemController(LosApiContext losApiContext)
        //{
        //    _losApiContext = losApiContext;
        //}
        // GET: api/<ProductItemController>

        [HttpGet]
        public IEnumerable<Models.Product> Get()
        {
            List<Models.Product> DataProduct = new List<Models.Product>();

            DataProduct = SessionHelper.GetObjectFromJson<List<Models.Product>>(HttpContext.Session, "SessionProduct");

            return DataProduct;
        }

        // GET api/<ProductItemController>/5
        [HttpGet("{id}")]
        public Models.Product Get(int id)
        {
            List<Models.Product> getProduct = new List<Models.Product>();

            getProduct = SessionHelper.GetObjectFromJson<List<Models.Product>>(HttpContext.Session, "SessionProduct");

            Models.Product oneProduct = getProduct.Where(x => x.id == id).FirstOrDefault();

            return oneProduct;
        }

        // POST api/<ProductItemController>
        [HttpPost]
        public void Post([FromBody] Models.Product Products)
        {
            List<Models.Product> ObjectProduct = new List<Models.Product>();

            List<Models.Product> DataProduct = new List<Models.Product>();

            if (Products.id > 1)
            {
                DataProduct = SessionHelper.GetObjectFromJson<List<Models.Product>>(HttpContext.Session, "SessionProduct");
            }

            if (ObjectProduct.Count == 0 && DataProduct.Count == 0)
            {
                ObjectProduct.Add(Products);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "SessionProduct", ObjectProduct);
            }
            else
            {         

                bool CheckProduct;

                if (Products != null)
                {
                    CheckProduct = DataProduct.Any(x => x.id == Products.id);

                    if (CheckProduct == false)
                    {
                        DataProduct = DataProduct.Append(Products).ToList();
                    }

                    if (DataProduct.Count != 0) 
                    {
                        HttpContext.Session.Remove("SessionProduct");
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "SessionProduct", DataProduct);
                    }                    
                }
            }

        }

        // PUT api/<ProductItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] List<Models.Product> Products)
        {
            Products[0].id = id;

            List<Models.Product> listProduct = SessionHelper.GetObjectFromJson<List<Models.Product>>(HttpContext.Session, "SessionProduct");


            var editProduct = listProduct.Where(x => x.id == id);

            editProduct = Products;

            SessionHelper.SetObjectAsJson(HttpContext.Session, "SessionProduct", editProduct);

        }

        // DELETE api/<ProductItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
