using SistemaLoja.Models;
using SistemaLoja.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SistemaLoja.Controllers
{
    public class OrdensController : Controller
    {

        private SistemaLojaContext db = new SistemaLojaContext();

        // GET: Ordens
        public ActionResult NovaOrdem()
        {
            var ordemView = new OrdemView();
            ordemView.Customizar = new Customizar();
            ordemView.Produtos = new List<ProdutoOrdem>();
            Session["ordemView"] = ordemView;


            var list = db.Customizars.ToList();
            list.Add(new Customizar { CustomizarId = 0, Nome = "[Selecione um cliente!]" });
            list = list.OrderBy(c => c.NomeCompleto).ToList();
            ViewBag.CustomizarId = new SelectList(list, "CustomizarId", "NomeCompleto");
            return View(ordemView);
        }

        [HttpPost]
        public ActionResult NovaOrdem(OrdemView ordemView)
        {
            ordemView = Session["ordemView"] as OrdemView;
            var customizarId = int.Parse(Request["CustomizarId"]);
            var list = db.Customizars.ToList();

            if (customizarId == 0)
            {
                list = db.Customizars.ToList();
                list.Add(new Customizar { CustomizarId = 0, Nome = "[Selecione um cliente!]" });
                list = list.OrderBy(c => c.NomeCompleto).ToList();
                ViewBag.CustomizarId = new SelectList(list, "CustomizarId", "NomeCompleto");
                ViewBag.Error = "Selecione um Cliente";

                return View(ordemView);
            }

            var cliente = db.Customizars.Find(customizarId);
            if (cliente == null)
            {
                list = db.Customizars.ToList();
                list.Add(new Customizar { CustomizarId = 0, Nome = "[Selecione um cliente!]" });
                list = list.OrderBy(c => c.NomeCompleto).ToList();
                ViewBag.CustomizarId = new SelectList(list, "CustomizarId", "NomeCompleto");
                ViewBag.Error = "O cliente não existe";

                return View(ordemView);
            }

            if(ordemView.Produtos.Count == 0)
            {
                list = db.Customizars.ToList();
                list.Add(new Customizar { CustomizarId = 0, Nome = "[Selecione um cliente!]" });
                list = list.OrderBy(c => c.NomeCompleto).ToList();
                ViewBag.CustomizarId = new SelectList(list, "CustomizarId", "NomeCompleto");
                ViewBag.Error = "Selecione um Produto";

                return View(ordemView);
            }

            int ordemId = 0;
            using(var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var ordem = new Ordem
                    {
                        CustomizarId = customizarId,
                        OrdemData = DateTime.Now,
                        OrdemStatus = OrdemStatus.Criada

                    };


                    db.Ordem.Add(ordem);
                    db.SaveChanges();

                    ordemId = db.Ordem.ToList().Select(o => o.OrdemId).Max();

                    foreach (var item in ordemView.Produtos)
                    {
                        var ordemDetalhes = new OrdemDetalhe
                        {
                            ProdutoId = item.ProdutoId,
                            Descricao = item.Descricao,
                            Preco = item.Preco,
                            Quantidade = item.Quantidade,
                            OrdemId = ordemId

                        };

                        db.OrdemDetalhe.Add(ordemDetalhes);
                        db.SaveChanges();
                    }

                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ViewBag.Error = "Error " + ex.Message;
                    return View(ordemView);
                }
            }

            
            ViewBag.Mensagem = string.Format("Ordem: {0}, foi salva com sucesso", ordemId);

            list = db.Customizars.ToList();
            list.Add(new Customizar { CustomizarId = 0, Nome = "[Selecione um cliente!]" });
            list = list.OrderBy(c => c.NomeCompleto).ToList();
            ViewBag.CustomizarId = new SelectList(list, "CustomizarId", "NomeCompleto");

            ordemView = new OrdemView();
            ordemView.Customizar = new Customizar();
            ordemView.Produtos = new List<ProdutoOrdem>();
            Session["ordemView"] = ordemView;

            return View(ordemView);
        }

        public ActionResult AddProduto()
        {
           
            var list = db.Produtoes.ToList();
            list.Add(new ProdutoOrdem { ProdutoId = 0, Descricao = "[Selecione um Produto!]" });
            list = list.OrderBy(c => c.Descricao).ToList();
            ViewBag.ProdutoId = new SelectList(list, "ProdutoId", "Descricao");
           

            return View();
        }

        [HttpPost]
        public ActionResult AddProduto(ProdutoOrdem produtoOrdem)
        {
            var ordemView = Session["ordemView"] as OrdemView;

            var list = db.Produtoes.ToList();
            var produtoId = int.Parse(Request["ProdutoId"]);

            if(produtoId == 0)
            {
                list.Add(new ProdutoOrdem { ProdutoId = 0, Descricao = "[Selecione um Produto!]" });
                list = list.OrderBy(c => c.Descricao).ToList();
                ViewBag.ProdutoId = new SelectList(list, "ProdutoId", "Descricao");
                ViewBag.Error = "Selecione um Produto";

                return View(produtoOrdem);
            }

            var produto = db.Produtoes.Find(produtoId);
            if (produto == null)
            {
                list.Add(new ProdutoOrdem { ProdutoId = 0, Descricao = "[Selecione um Produto!]" });
                list = list.OrderBy(c => c.Descricao).ToList();
                ViewBag.ProdutoId = new SelectList(list, "ProdutoId", "Descricao");
                ViewBag.Error = "Não existe o produto selecionado";

                return View(produtoOrdem);
            }

            produtoOrdem = ordemView.Produtos.Find(p => p.ProdutoId == produtoId);
            if(produtoOrdem == null) { 

            produtoOrdem = new ProdutoOrdem
            {
            Descricao = produto.Descricao,
            Preco = produto.Preco,
            ProdutoId = produto.ProdutoId,
            Quantidade = float.Parse(Request["Quantidade"])
        };
            
            ordemView.Produtos.Add(produtoOrdem);
            }else
            {
                produtoOrdem.Quantidade += float.Parse(Request["Quantidade"]);
            }


            var listC = db.Customizars.ToList();
           // listC.Add(new Customizar { CustomizarId = 0, Nome = "[Selecione um cliente!]" });
            listC = listC.OrderBy(c => c.NomeCompleto).ToList();
            ViewBag.CustomizarId = new SelectList(listC, "CustomizarId", "NomeCompleto");

            return View("NovaOrdem", ordemView);
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