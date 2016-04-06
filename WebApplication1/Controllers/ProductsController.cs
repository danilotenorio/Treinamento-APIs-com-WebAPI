using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApplication1.Controllers
{
    using DevStore.Domain;
    using DevStore.Infra.DataContexts;

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/v1/public")]
    public class ProductsController : ApiController
    {
        private DevStoreDataContext db = new DevStoreDataContext();

        [Route("products")]
        public HttpResponseMessage getProducts()
        {
            var result = db.Products.Include("Category").ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [Route("categories/{categoryId}/products")]
        public HttpResponseMessage getProductsByCategories(int categoryId)
        {
            var result = db.Products.Include("Category").Where(x => x.CategoryId == categoryId).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("products")]
        public HttpResponseMessage PostProduct(Product product)
        {
            if (product == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            try
            {
                db.Products.Add(product);
                db.SaveChanges();
                var result = product;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir o produto");
            }
        }

        [HttpPatch]
        [Route("products")]
        public HttpResponseMessage PatchProduct(Product product)
        {
            if (product == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            try
            {
                db.Entry<Product>(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                var result = product;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao alterar o produto");
            }
        }

        [HttpPut]
        [Route("products")]
        public HttpResponseMessage PutProduct(Product product)
        {
            if (product == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            try
            {
                db.Entry<Product>(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                var result = product;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao alterar o produto");
            }
        }

        [HttpDelete]
        [Route("products")]
        public HttpResponseMessage DeleteProduct(int productId)
        {
            if (productId <= 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            try
            {
                db.Products.Remove(db.Products.Find(productId));
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Produto Excluido");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao excluir");
            }
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}