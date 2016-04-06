using DevStore.Domain;
using DevStore.Infra.DataContexts;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApplication1.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/v1/public")]
    public class CategoriesController : ApiController
    {
        private DevStoreDataContext db = new DevStoreDataContext();

        [Route("categories")]
        public HttpResponseMessage getCategories()
        {
            var result = db.Categories.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("categories")]
        public HttpResponseMessage PostCategories(Category category)
        {
            if (category == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            try
            {
                db.Categories.Add(category);
                db.SaveChanges();
                var result = category;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir a Categoria");
            }
        }

        [HttpPatch]
        [Route("categories")]
        public HttpResponseMessage PatchCategories(Category category)
        {
            if (category == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            try
            {
                db.Entry<Category>(category).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                var result = category;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao alterar a Categoria");
            }
        }

        [HttpPut]
        [Route("categories")]
        public HttpResponseMessage PutCategories(Category category)
        {
            if (category == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            try
            {
                db.Entry<Category>(category).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                var result = category;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao alterar a Categoria");
            }
        }

        [HttpDelete]
        [Route("categories")]
        public HttpResponseMessage DeleteCategories(int categoryId)
        {
            if (categoryId <= 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            try
            {
                db.Categories.Remove(db.Categories.Find(categoryId));
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Categoria Excluida");
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