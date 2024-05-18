using ClassWebApi.Domain.Interfaces;
using ClassWebApi.Domain.Models.DTOs;
using ClassWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClassWebApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmaController : ControllerBase
    {
        private readonly ITurmaService _turmaService;

        public TurmaController(ITurmaService turmaService)
        {
            _turmaService = turmaService;
        }

        [HttpGet]
        public IEnumerable<Turma> GetTurmas()
        {
            return _turmaService.GetTurmas();
        }

        [HttpGet("{id}")]
        public Turma GetTurma(int id)
        {
            return _turmaService.GetTurma(id);
        }

        [HttpPost]
        [Route("CreateTurma")]
        public IActionResult CreateTurma([FromBody]TurmaDTO turma)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (turma != null)
            {
                var turmaBD = new Turma(turma);
                try
                {
                    var idAluno = _turmaService.CreateTurmaAsync(turmaBD);
                    return Ok("turma criado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return StatusCode(500, "Erro ao criar turma.");
                }
            }
            return StatusCode(500, "Erro ao criar turma.");
        }

        [Route("UpdateTurma")]
        [HttpPut]
        public IActionResult UpdateTurma([FromQuery] int turmaId, [FromBody] TurmaDTO turma)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (turma != null)
            {
                var turmaBD = new Turma(turma);
                turmaBD.TurmaId = turmaId;
                try
                {
                    var idAluno = _turmaService.UpdateTurma(turmaBD);
                    return Ok("turma atualizado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return StatusCode(500, "Erro ao atualizar turma.");
                }
            }
            return StatusCode(500, "Erro ao atualizar turma.");
        }

        [Route("DeleteTurma/{turmaId}")]
        [HttpDelete]
        public IActionResult DeleteTurma(int turmaId)
        {
            if (turmaId <= 0)
            {
                return BadRequest("Informe um ID de turma válido.");
            }

            try
            {
                _turmaService.DeleteTurma(turmaId);
                return Ok("turma deletado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Erro ao deletar turma.");
            }
        }
    }
}
