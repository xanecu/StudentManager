using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bd1_manha.Models;
using PagedList;

namespace bd1_manha.Controllers
{
    //FALTA PAGINAÇÃO NO PROGRAMA. feitos
    //TPC: resolver no edit a datanasc fazendo so com que apareça dia mes e ano(sem hora nem minutos nem segundos)
    //TPC:resolver o que fizemos na media para o campo CREATE tambem (resolvemos no database e no edit.cshtml). penso que feito

    public class HomeController : Controller
    {
        public ActionResult Delete(int? id)
        {
            using(bd_TB_manhaEntities bd= new bd_TB_manhaEntities())
            {
                aluno morto = bd.alunos.Find(id);
                if(morto != null)
                {
                    try {
                        bd.alunos.Remove(morto);
                        bd.SaveChanges();
                        return Json(new { msg = "Registo apagado com sucesso" }, JsonRequestBehavior.AllowGet);
                    } catch (Exception ex)
                    {
                        return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
                    }
                    
                }else  return Json(new { msg = "Não encontrou o registo" }, JsonRequestBehavior.AllowGet); //esta linha nem seria necessaria porque o codigo ou faz o try ou faz o catch
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id) //05.12 com turma D
        {
            using(bd_TB_manhaEntities bd = new bd_TB_manhaEntities())
            {
                List<curso> cursos = bd.cursos.ToList();
                ViewBag.cursos = new SelectList(cursos,"codcurso","curso1");
                aluno este = bd.alunos.Find(id);
                if (este != null) return View(este);
                return RedirectToAction("Alunos", new { msg = "Alunos nao encontrado" });
            }
        }

        [HttpPost]
        public ActionResult Edit(aluno a,String media) //05.12 com turma D e editado por mim para sintonia com aula do 11-12 a tarde
        {
            using (bd_TB_manhaEntities bd = new bd_TB_manhaEntities())
            {
                aluno este = bd.alunos.Where(x=> x.num == a.num).FirstOrDefault();
                    
                este.naluno = a.naluno;
                este.codcurso = a.codcurso;
                este.datanasc = a.datanasc;
                decimal m;
                if (Decimal.TryParse(media, out m))
                {
                    if (m >= 0 && m <= 20) este.media = m;
                }
                bd.SaveChanges();
                return RedirectToAction("Alunos", new { msg = "Aluno não existe" });
                
            }
        }


        public ActionResult Details(int ? id) { 
            using (bd_TB_manhaEntities bd = new bd_TB_manhaEntities())
            {
                aluno a = bd.alunos.Where(x => x.num == id).FirstOrDefault();
                if (a != null) return View(a);
                return RedirectToAction("Alunos", new { msg="Registo não encontrado" });
            }
        }

        [HttpGet]
        public ActionResult Create() //DropdownList
        {

            

            using (bd_TB_manhaEntities bd = new bd_TB_manhaEntities())
            {
                List<curso> cursos = bd.cursos.ToList<curso>();
                ViewBag.cursos = new SelectList(cursos, "codcurso", "curso1");
                aluno novo = new aluno();
                return View(novo);
            }//botão direito do rato e fazer "add View(Create)"
        }
        [HttpPost]
        public ActionResult Create(aluno a) //Create aluno and save na BD
        {

            using (bd_TB_manhaEntities bd = new bd_TB_manhaEntities())
            {
                bd.alunos.Add(a);
                bd.SaveChanges();
                return RedirectToAction("Alunos", new {msg="Alunos inserido com sucesso"});
            }
        }

        public ActionResult Alunos(String msg, int? page) //Esta msg faz referencia ao msg do campo Details msg (registo nao encontrado)
                                               //que por sua vez esta no h3 do Alunos.cshtml em ViewBag
        {
            using (bd_TB_manhaEntities bd = new bd_TB_manhaEntities())
            {//pluralize-alunos
                ViewBag.msg = msg;
                List<aluno> Alunos = bd.alunos.ToList<aluno>();
                int pagesize = 5;
                int pagenum = page ?? 1;
                return View(Alunos.ToPagedList(pagenum,pagesize));
            }//botão direito do rato e fazer "add View(List)"
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}