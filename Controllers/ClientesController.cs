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
    public class ClientesController : Controller
    {
        //Declara DbContext - injeta ele via construtor
        private readonly ApplicationDbContext _context;

        //Injeta via Construtor
        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clientes = Faz tudo de forma assíncrono - obtendo todos os processos para uma lista
        public async Task<IActionResult> Index()
        {
              return View(await _context.Clientes.ToListAsync());
        }

        // GET: Clientes/Details/5 = Retorna detalhes desse processo
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente) //Possibilidade receber dados adicionais.
        /*public async Task<IActionResult> Create([Bind  ("NomeCliente,Documento,TipoPessoa,Ativo,Cep,Logradouro,Numero,Complemento,Bairro,Cidade,Estado,Id")] Cliente cliente) // recebe todos os dados através deste Bind.*/
        {
            if (ModelState.IsValid)
            {
                //cliente.Id = Guid.NewGuid(); - Setar no Id o que não é necessário, pois já fazemos isso.
                _context.Add(cliente); // vai adicionar ao context
                try //Captura erro retornado do SGBD
                {
                    await _context.SaveChangesAsync();

                }catch 
                {
                    return Content("Banco de dados retornou erro de consistencia, confira os dados digitados, tipos, tamanhos ou registros duplicados.  Retorne a tela anterior.");

                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            // Vamos obter esse cliente via FindAsync: verificar se o Id e resultado do banco não foi passado
            // em nullo caso que ele retorna em NotFound 404.

            var cliente = await _context.Clientes.FindAsync(id);  
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Cliente cliente) // Ampliar possibilidade de mandar mais dados.
        {
            if (id != cliente.Id) // verifica se o Id que está mandando é o mesmo fornecido através do formulário.
            {                      //Verifica se esses dois Ids são iguais (Guid Id e do formulário)
                return NotFound();
            }

            if (ModelState.IsValid) // Vai tentar validar da ModelState, vai tentar salvar no banco. 
            {                       
                try
                {
                    _context.Update(cliente);
                    try //Captura erro retornado do SGBD
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch
                    {
                        return Content("Banco de dados retornou erro de consistencia, confira os dados digitados, tipos, tamanhos ou registros duplicados. Retorne a tela anterior. ");
                    }
                }
                catch (DbUpdateConcurrencyException)//Método está abaixo ClienteExists() onde procura esse Cliente.
                {
                    if (!ClienteExists(cliente.Id)) //Se problema de concorrência ele vai verificar se existe ou não.
                    {                               
                        return NotFound();
                    }
                    else
                    {
                        throw; // Se houver concorrência ele solta exception
                    }
                }
                return RedirectToAction(nameof(Index)); //Redireciona para Index em caso de sucesso, se não
                                                        //para View em caso de falha.
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(Guid? id) // Devolve essa informação para o Cliente
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) // Exclui esse Cliente via método Remove.
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(Guid id) //(DbUpdateConcurrencyException) Ele tenta encontrar esse Cliente.
        {
          return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
