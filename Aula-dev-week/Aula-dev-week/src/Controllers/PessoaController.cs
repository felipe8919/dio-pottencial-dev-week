using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

using Aula_dev_week.src.models;
using Aula_dev_week.src.Persistence;


namespace Aula_dev_week.src.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private DatabaseContext _context { get; set; }

        public PessoaController(DatabaseContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public ActionResult<List<Pessoa>> Get()
        {
            var result = _context.Pessoas.Include(p => p.contratos).ToList();

            if (!result.Any()) return NoContent();

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<Pessoa> Post([FromBody] Pessoa pessoa)
        {
            try
            {
                _context.Pessoas.Add(pessoa);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

            return Created("criado", pessoa);
        }

        [HttpPut("{id}")]
        public ActionResult<Object> Update([FromRoute] int id, [FromBody] Pessoa pessoa)
        {
            var result = _context.Pessoas.SingleOrDefault(e => e.Id == id);

            if (result is null)
                return NotFound(new
                {
                    mgs = "Registro não encontrado",
                    status = HttpStatusCode.NotFound
                });

            try
            {
                _context.Pessoas.Update(pessoa);
                _context.SaveChanges();

            }
            catch (System.Exception)
            {
                return BadRequest(new
                {
                    msg = $"Houve erro ao enviar solicitação de atualização do {id} atualizados",
                    status = HttpStatusCode.OK
                });
            }

            return Ok(new
            {
                msg = $"Dados do id {id} atualizados",
                status = HttpStatusCode.OK
            });
        }

        [HttpDelete("{id}")]
        public ActionResult<Object> Delete([FromRoute] int id)
        {
            var result = _context.Pessoas.SingleOrDefault(e => e.Id == id);

            if (result is null)
            {
                return BadRequest(new
                {
                    msg = "Conteúde inexistente, solicitação inválida",
                    status = HttpStatusCode.BadRequest
                });
            }

            _context.Pessoas.Remove(result);
            _context.SaveChanges();

            return Ok(new
            {
                msg = $"deletado pessoa de Id {id}",
                status = HttpStatusCode.OK
            });
        }

    }
}
