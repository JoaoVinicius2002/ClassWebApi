using ClassWebApi.Application;
using ClassWebApi.Domain.Interfaces;
using ClassWebApi.Domain.Models;
using ClassWebApi.Domain.Models.DTOs;
using ClassWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;


namespace ClassWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _alunoService; 

        public AlunoController(IAlunoService alunoService) 
        {
            _alunoService = alunoService;
        }

        [HttpGet]
        public IEnumerable<Aluno> GetAlunos()
        {
            return _alunoService.GetAlunos();
        }

        [HttpGet("{id}")]
        public Aluno GetAluno(int id)
        {
            return _alunoService.GetAluno(id);
        }

        [HttpPost]
        [Route("CreateAluno")]
        public IActionResult CreateAluno([FromBody] AlunoDTO aluno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(aluno != null)
            {
                var alunoBD = new Aluno(aluno);
                try
                {
                    var idAluno =  _alunoService.CreateAlunoAsync(alunoBD); 
                    return Ok("Aluno criado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return StatusCode(500, "Erro ao criar aluno.");
                }
            }
            return StatusCode(500, "Erro ao criar aluno.");
        }

        [Route("UpdateAluno")]
        [HttpPut]
        public IActionResult UpdateAluno([FromQuery] int alunoId, [FromBody] AlunoDTO aluno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (aluno != null)
            {
                var alunoBD = new Aluno(aluno);
                alunoBD.AlunoId = alunoId;
                try
                {
                    var idAluno = _alunoService.UpdateAluno(alunoBD);
                    return Ok("Aluno atualizado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return StatusCode(500, "Erro ao atualizar aluno.");
                }
            }
            return StatusCode(500, "Erro ao atualizar aluno.");
        }

        [Route("DeleteAluno/{alunoId}")]
        [HttpDelete]
        public IActionResult DeleteAluno(int alunoId)
        {
            if (alunoId <= 0)
            {
                return BadRequest("Informe um ID de aluno válido.");
            }

            try
            {
                _alunoService.DeleteAluno(alunoId);
                return Ok("Aluno deletado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Erro ao deletar aluno.");
            }
        }
    }
}
