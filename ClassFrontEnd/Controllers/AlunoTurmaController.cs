using ClassFrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ClassFrontEnd.Controllers
{
    public class AlunoTurmaController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AlunoTurmaController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var model = new AlunoTurmaViewModel();

            var httpClient = _httpClientFactory.CreateClient();

            var alunosResponse = await httpClient.GetAsync("https://localhost:7080/api/Aluno");
            if (alunosResponse.IsSuccessStatusCode)
            {
                var alunosContent = await alunosResponse.Content.ReadAsStringAsync();
                model.Alunos = JsonConvert.DeserializeObject<List<AlunoViewModel>>(alunosContent);
            }
            var turmasResponse = await httpClient.GetAsync("https://localhost:7080/api/Turma");
            if (turmasResponse.IsSuccessStatusCode)
            {
                var turmasContent = await turmasResponse.Content.ReadAsStringAsync();
                model.Turmas = JsonConvert.DeserializeObject<List<TurmaViewModel>>(turmasContent);
            }

            return View("Create", model);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new AlunoTurmaViewModel();

            var httpClient = _httpClientFactory.CreateClient();

            var alunosResponse = await httpClient.GetAsync("https://localhost:7080/api/Aluno");
            if (alunosResponse.IsSuccessStatusCode)
            {
                var alunosContent = await alunosResponse.Content.ReadAsStringAsync();
                model.Alunos = JsonConvert.DeserializeObject<List<AlunoViewModel>>(alunosContent);
            }

            var turmasResponse = await httpClient.GetAsync("https://localhost:7080/api/Turma");
            if (turmasResponse.IsSuccessStatusCode)
            {
                var turmasContent = await turmasResponse.Content.ReadAsStringAsync();
                model.Turmas = JsonConvert.DeserializeObject<List<TurmaViewModel>>(turmasContent);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AlunoTurmaViewModel model)
        {
            if (model.TurmaId != null && model.AlunoId != null)
            {
                var httpClient = _httpClientFactory.CreateClient();
                var alunoTurmaResponse = await httpClient.GetAsync("https://localhost:7080/api/AlunoTurma");
                if (alunoTurmaResponse.IsSuccessStatusCode)
                {
                    var alunosContent = await alunoTurmaResponse.Content.ReadAsStringAsync();
                    var alunoTurmaCadastrados = JsonConvert.DeserializeObject<List<AlunoTurmaViewModel>>(alunosContent);

                    bool associationExists = alunoTurmaCadastrados.Any(at => at.AlunoId == model.AlunoId && at.TurmaId == model.TurmaId);
                    if (associationExists)
                    {
                        ViewBag.CreateSuccess = false;
                        ViewBag.ErrorMessage = "Este aluno já está associado a esta turma!";
                        return await PrepareCreateViewModel();
                    }
                }
                else
                {
                    var url = $"https://localhost:7080/api/AlunoTurma/CreateAlunoTurma";

                    var objeto = new { alunoId = model.AlunoId, turmaId = model.TurmaId };
                    var json = JsonConvert.SerializeObject(objeto);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        ViewBag.CreateSuccess = true;
                        return View(model);
                    }
                    else
                    {
                        return BadRequest("Um erro aconteceu ao tentar matricular o aluno na turma selecionada");
                    }
                }
            }

            return View(model);
        }
        private async Task<IActionResult> PrepareCreateViewModel()
        {
            var model = new AlunoTurmaViewModel();
            var httpClient = _httpClientFactory.CreateClient();

            var alunosResponse = await httpClient.GetAsync("https://localhost:7080/api/Aluno");
            if (alunosResponse.IsSuccessStatusCode)
            {
                var alunosContent = await alunosResponse.Content.ReadAsStringAsync();
                model.Alunos = JsonConvert.DeserializeObject<List<AlunoViewModel>>(alunosContent);
            }

            var turmasResponse = await httpClient.GetAsync("https://localhost:7080/api/Turma");
            if (turmasResponse.IsSuccessStatusCode)
            {
                var turmasContent = await turmasResponse.Content.ReadAsStringAsync();
                model.Turmas = JsonConvert.DeserializeObject<List<TurmaViewModel>>(turmasContent);
            }

            return View("Create", model);
        }
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
