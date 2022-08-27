using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppControleJuridico.Data;
using AppControleJuridico.Models;
using Microsoft.AspNetCore.Authorization;

namespace AppControleJuridico.Controllers
{
    [Authorize]
    public class ProcessosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProcessosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Processos
        public async Task<IActionResult> Index()
        {   //Pegou o context, o produto e incluiu o Cliente na relação, pois o processo pertence ao Cliente que podemos mostrar os dados do cliente também
            var applicationDbContext = _context.Processos.Include(p => p.Cliente);
            return View(await applicationDbContext.ToListAsync()); //Setou uma instância desse context que seria o
                                                                   //retorno dessa query
        }

        // GET: Processos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Processos == null)
            {
                return NotFound();
            }

            var processo = await _context.Processos         //Aqui nos detalhes, sempre inclui o cliente para trazer
                .Include(p => p.Cliente)                    // informações completas.
                .FirstOrDefaultAsync(m => m.Id == id);
            if (processo == null)
            {
                return NotFound();
            }

            return View(processo);
        }

        // GET: Processos/Create
        public IActionResult Create()
        {
            //TempData ela dura mais um request - usada para redirecionamento, quando move de um controller para o
            //outro - ela é uma sessão de curta duração.
            // ViewBag é dinâmica - está dentro da ViewData - elas só duram até chegar na View a informação
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "NomeCliente");
            return View();
            //Componente de transporte da Controller para a View - seria informação da View que seta na controller.
            //Diz que vai existir uma ViewData com nome de ClienteId, que ela é um objeto, produto de uma instância
            //SelectList, onde ele pega a lista de fornecedores, através do contexto, e fala que o valor do dado é
            //o Id e que o texto dessa informação é o documento, para fazer um DropDow na tela. Escolhe-se o
            //fornecedor a partir dessa lista.
        }

        // POST: Processos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Processo processo)
        {
            if (ModelState.IsValid)
            {                
                _context.Add(processo);
                try //Captura erro retornado do SGBD
                {
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    return Content("Banco de dados retornou erro de consistencia, confira os dados digitados, tipos, tamanhos ou registros duplicados.  Retorne a tela anterior.");
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "NomeCliente", processo.ClienteId);
            return View(processo);
        }

        // GET: Processos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Processos == null)
            {
                return NotFound();
            }

            var processo = await _context.Processos.FindAsync(id);
            if (processo == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "NomeCliente", processo.ClienteId);
            return View(processo);
        }

        // POST: Processos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Processo processo)
        {
            if (id != processo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(processo);
                    try //Captura erro retornado do SGBD
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch
                    {
                        return Content("Banco de dados retornou erro de consistencia, confira os dados digitados, tipos, tamanhos ou registros duplicados. Retorne a tela anterior. ");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcessoExists(processo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "NomeCliente", processo.ClienteId);
            return View(processo);
        }

        // GET: Processos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Processos == null)
            {
                return NotFound();
            }

            var processo = await _context.Processos
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (processo == null)
            {
                return NotFound();
            }

            return View(processo);
        }

        // POST: Processos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Processos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Processos'  is null.");
            }
            var processo = await _context.Processos.FindAsync(id);
            if (processo != null)
            {
                _context.Processos.Remove(processo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcessoExists(Guid id)
        {
          return _context.Processos.Any(e => e.Id == id);
        }
    }
}
