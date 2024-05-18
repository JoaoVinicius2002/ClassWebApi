using ClassWebApi.Application;
using ClassWebApi.Domain.Interfaces;
using ClassWebApi.Domain.Models;
using ClassWebApi.Domain.Models.DTOs;
using ClassWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClassWebApi.Presentation.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AlunoTurmaController : ControllerBase
    {
        private readonly IAlunoTurmaService _alunoTurmaService;
        private readonly IAlunoService _alunoService;
        private readonly ITurmaService _turmaService;

        public AlunoTurmaController(IAlunoTurmaService alunoTurmaService, IAlunoService alunoService, ITurmaService turmaService)
        {
            _alunoTurmaService = alunoTurmaService;
            _alunoService = alunoService;
            _turmaService = turmaService;
        }

        [HttpGet]
        public IEnumerable<AlunoTurma> GetAlunoTurmas()
        {
            return _alunoTurmaService.GetAlunoTurmas();
        }

        [HttpGet("{alunoId}/{turmaId}")]
        public AlunoTurma GetAlunoTurma(int alunoId, int turmaId)
        {
            return _alunoTurmaService.GetAlunoTurma(alunoId, turmaId);
        }
        [HttpGet("GetTurmasByAluno/{alunoId}")]
        public IEnumerable<Turma> GetTurmasByAluno(int alunoId)
        {
            return _turmaService.GetTurmas(alunoId);

        }
        [HttpGet("GetAlunosByTurma/{turmaId}")]
        public IEnumerable<Aluno> GetAlunosByTurma(int turmaId)
        {
            return _alunoService.GetAlunos(turmaId);

        }

        [HttpPost]
        [Route("CreateAlunoTurma")]
        public IActionResult CreateAlunoTurma([FromBody] AlunoTurma model = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model != null)
            {
                var alunoTurmaBD = new AlunoTurma(model.AlunoId, model.TurmaId);
                try
                {
                     _alunoTurmaService.CreateAlunoTurmaAsync(alunoTurmaBD);
                    return Ok("alunoTurma criado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return StatusCode(500, "Erro ao criar alunoTurma.");
                }
            }
            return StatusCode(500, "Erro ao criar alunoTurma.");
        }

        [Route("UpdateAlunoTurma")]
        [HttpPut]
        public IActionResult UpdateAlunoTurma(int alunoTurmaId, [FromBody] AlunoTurmaDTO alunoTurma)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (alunoTurma != null)
            {
                var alunoTurmaBD = new AlunoTurma(alunoTurma);
                alunoTurmaBD.TurmaId = alunoTurmaId;
                try
                {
                    var idAluno = _alunoTurmaService.UpdateAlunoTurma(alunoTurmaBD);
                    return Ok("alunoTurma atualizado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return StatusCode(500, "Erro ao atualizar alunoTurma.");
                }
            }
            return StatusCode(500, "Erro ao atualizar alunoTurma.");
        }

        [Route("DeleteAlunoTurma/{alunoId}/{turmaId}")]
        [HttpDelete]
        public IActionResult DeleteAlunoTurma(int alunoId, int turmaId)
        {
            if (alunoId <= 0)
            {
                return BadRequest("Informe um ID de alunoTurma válido.");
            }

            try
            {
                _alunoTurmaService.DeleteAlunoTurma(alunoId, turmaId);
                return Ok("alunoTurma deletado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Erro ao deletar alunoTurma.");
            }
        }
    }
}
