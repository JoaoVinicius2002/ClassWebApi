using ClassFrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ClassFrontEnd.Controllers
{
    public class TurmaController : Controller
    {
        private readonly HttpClient _httpClient;
        public TurmaController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:7080/api/Turma");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var turmas = JsonConvert.DeserializeObject<List<TurmaViewModel>>(jsonData);

                return View(turmas);
            }
            else
            {
                return View("Error");
            }
        }

        public async Task<ActionResult> Details(int turmaId)
        {
            var urlTurma = $"https://localhost:7080/api/Turma/{turmaId}";
            var responseTurma = await _httpClient.GetAsync(urlTurma);
            var turma = new TurmaViewModel();
            if (responseTurma.IsSuccessStatusCode)
            {
                var responseContent = await responseTurma.Content.ReadAsStringAsync();
                turma = JsonConvert.DeserializeObject<TurmaViewModel>(responseContent);
            }
            var url = $"https://localhost:7080/api/AlunoTurma/GetAlunosByTurma/{turmaId}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var alunos = JsonConvert.DeserializeObject<IEnumerable<AlunoViewModel>>(json);

                    var viewModel = new TurmaDetailsViewModel
                    {
                        AlunoId = turmaId,
                        Nome = turma.TurmaNome,
                        Alunos = alunos
                    };

                    return View(viewModel);
                }
                else
                {
                    ModelState.AddModelError("", "Erro ao obter os alunos dessa turma. Tente novamente");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao obter os alunos dessa turma. Tente novamente");
                return View();
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            try
            {
                var ano = collection["ano"][0];
                var turmaNome = collection["turmaNome"][0];
                var materia = collection["materia"][0];

                var turma = new { ano = ano, turmaNome = turmaNome, materia = materia };

                var json = JsonConvert.SerializeObject(turma);

                var url = "https://localhost:7080/api/Turma/CreateTurma";

                using var httpClient = new HttpClient();

                var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(url, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Erro ao criar turma. Tente novamente.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao criar turma. Tente novamente.");
                return View();
            }
        }

        public async Task<ActionResult> Edit(int turmaId)
        {
            var url = $"https://localhost:7080/api/Turma/{turmaId}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync(); 
                var turma = JsonConvert.DeserializeObject<TurmaViewModel>(responseContent);

                return View(turma);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TurmaViewModel turma)
        {
            if (Request.Form["_method"] == "PUT")
            {
                var turmaSemId = new
                {
                    turma.TurmaNome,
                    turma.Ano,
                    turma.Materia
                };
                var json = JsonConvert.SerializeObject(turmaSemId);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var url = $"https://localhost:7080/api/Turma/UpdateTurma?turmaId={turma.TurmaId}";
                var response = await _httpClient.PutAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error");
                }
            }
            return View("Error");
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var url = $"https://localhost:7080/api/Turma/DeleteTurma/{id}";
                var response = await _httpClient.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View("Error");
                }
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
