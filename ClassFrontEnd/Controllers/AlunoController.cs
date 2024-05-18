using ClassFrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ClassFrontEnd.Controllers
{
    public class AlunoController : Controller
    {
        private readonly HttpClient _httpClient;
        public AlunoController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:7080/api/Aluno");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var alunos = JsonConvert.DeserializeObject<List<AlunoViewModel>>(jsonData);

                return View(alunos);
            }
            else
            {
                return View("Error");
            }
        }

        public async Task<ActionResult> Details(int alunoId)
        {
            var urlAluno = $"https://localhost:7080/api/Aluno/{alunoId}";
            var responseAluno = await _httpClient.GetAsync(urlAluno);
            var aluno = new AlunoViewModel();
            if (responseAluno.IsSuccessStatusCode)
            {
                var responseContent = await responseAluno.Content.ReadAsStringAsync();
                aluno = JsonConvert.DeserializeObject<AlunoViewModel>(responseContent);
            }
            var url = $"https://localhost:7080/api/AlunoTurma/GetTurmasByAluno/{alunoId}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var turmas = JsonConvert.DeserializeObject<IEnumerable<TurmaViewModel>>(json);

                    var viewModel = new AlunoDetailsViewModel
                    {
                        AlunoId = alunoId,
                        Nome = aluno.Nome, 
                        Turmas = turmas
                    };

                    return View(viewModel);
                }
                else
                {
                    ModelState.AddModelError("", "Erro ao consultar as matrículas de um aluno, tente novamente.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao consultar as matrículas de um aluno, tente novamente.");
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
                var nome = collection["nome"][0];
                var usuario = collection["usuario"][0];
                var senha = collection["senha"][0];

                var aluno = new { Nome = nome, Usuario = usuario, Senha = senha };

                var json = JsonConvert.SerializeObject(aluno);

                var url = "https://localhost:7080/api/Aluno/CreateAluno";

                using var httpClient = new HttpClient();

                var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(url, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Erro ao criar aluno, tente novamente.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao criar aluno, tente novamente.");
                return View();
            }
        }

        public async Task<ActionResult> Edit(int alunoId)
        {
            var url = $"https://localhost:7080/api/Aluno/{alunoId}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync(); 
                var aluno = JsonConvert.DeserializeObject<AlunoViewModel>(responseContent); 

                return View(aluno);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AlunoViewModel aluno)
        {
            if (Request.Form["_method"] == "PUT")
            {
                var alunoSemId = new
                {
                    aluno.Nome,
                    aluno.Usuario,
                    aluno.Senha
                };
                var json = JsonConvert.SerializeObject(alunoSemId);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var url = $"https://localhost:7080/api/Aluno/UpdateAluno?alunoId={aluno.AlunoId}";
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
                var url = $"https://localhost:7080/api/Aluno/DeleteAluno/{id}";
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
