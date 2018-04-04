using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaLoja.Models;

namespace SistemaLoja.Controllers
{
    public class PessoaController : Controller
    {
        private SistemaLojaContext db = new SistemaLojaContext();

        // GET: Pessoa
        public ActionResult Index()
        {
            var customizars = db.Customizars.Include(c => c.TipoDocumento);
            return View(customizars.ToList());
        }

        // GET: Pessoa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customizar customizar = db.Customizars.Find(id);
            if (customizar == null)
            {
                return HttpNotFound();
            }
            return View(customizar);
        }

        // GET: Pessoa/Create
        public ActionResult Create()
        {
            var list = db.TipoDocumentoes.ToList();
            list.Add(new TipoDocumento { TipoDocumentoId = 0, Descricao = "[Selecione um tipo de documento!]" });
            list = list.OrderBy(c => c.Descricao).ToList();
            ViewBag.TipoDocumentoId = new SelectList(list, "TipoDocumentoId", "Descricao");
            return View();
        }

        // POST: Pessoa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomizarId,Nome,Sobrenome,Telefone,Endereco,Email,Documento,TipoDocumentoId")] Customizar customizar)
        {
            if (ModelState.IsValid)
            {
                db.Customizars.Add(customizar);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                    
                }
                
                return RedirectToAction("Index");
            }

            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumentoes, "TipoDocumentoId", "Descricao", customizar.TipoDocumentoId);
            return View(customizar);
        }

        // GET: Pessoa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customizar customizar = db.Customizars.Find(id);
            if (customizar == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumentoes, "TipoDocumentoId", "Descricao", customizar.TipoDocumentoId);
            return View(customizar);
        }

        // POST: Pessoa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomizarId,Nome,Sobrenome,Telefone,Endereco,Email,Documento,TipoDocumentoId")] Customizar customizar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customizar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumentoes, "TipoDocumentoId", "Descricao", customizar.TipoDocumentoId);
            return View(customizar);
        }

        // GET: Pessoa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customizar customizar = db.Customizars.Find(id);
            if (customizar == null)
            {
                return HttpNotFound();
            }
            return View(customizar);
        }

        // POST: Pessoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customizar customizar = db.Customizars.Find(id);
            db.Customizars.Remove(customizar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
