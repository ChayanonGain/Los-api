using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using los_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace los_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        // GET: api/<StockController>
        [HttpGet]
        public IEnumerable<Models.Stock> Get()
        {
            List<Models.Stock> Stockmodels = new List<Models.Stock>();

            Stockmodels = SessionHelper.GetObjectFromJson<List<Models.Stock>>(HttpContext.Session, "SessionStock");

            return Stockmodels;
        }

        // GET api/<StockController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StockController>
        [HttpPost]
        public void Post([FromBody] Models.Stock Stocks)
        {
            List<Models.Stock> ObjectStock = new List<Models.Stock>();

            List<Models.Stock> DataStock = new List<Models.Stock>();

            List<Models.Product> DataProduct = new List<Models.Product>();


            if (Stocks != null)
            {
                DataProduct = SessionHelper.GetObjectFromJson<List<Models.Product>>(HttpContext.Session, "SessionProduct");
            }

            if (Stocks.id > 1)
            {
                DataStock = SessionHelper.GetObjectFromJson<List<Models.Stock>>(HttpContext.Session, "SessionProduct");
            }

            if (DataProduct.Count > 0)
            {
                bool CheckProduct = DataProduct.Any(x => x.id == Stocks.productId);
                if (CheckProduct == true)
                {
                    ObjectStock.Add(Stocks);
                    HttpContext.Session.SetString("setDataStock", Stocks.ToString());
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "SessionStock", ObjectStock);
                }
            }

            if (DataStock.Count > 0)
            {
                bool CheckStock;

                if (Stocks != null)
                {
                    CheckStock = ObjectStock.Any(x => x.id == Stocks.id);

                    if (CheckStock == false)
                    {
                        DataStock = DataStock.Append(Stocks).ToList();
                    }

                    if (DataStock.Count != 0)
                    {
                        HttpContext.Session.Remove("SessionStock");
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "SessionStock", DataStock);
                    }
                }
                else
                {
                    DataStock = SessionHelper.GetObjectFromJson<List<Models.Stock>>(HttpContext.Session, "SessionStock");
                }
            }

        }

        // PUT api/<StockController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StockController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
