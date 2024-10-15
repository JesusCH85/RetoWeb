using CepdiRetoWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;


namespace CepdiRetoWeb.Controllers
{
    public class MedicamentosController : Controller
    {
        private readonly HttpClient? _httpClient;

        public MedicamentosController ( IHttpClientFactory httpClientFactory) 
        {
            _httpClient=httpClientFactory.CreateClient ();
            _httpClient.BaseAddress = new Uri("https://localhost:7069/api");
        }

        public async Task<IActionResult> Index()
        {

            var response = await _httpClient.GetAsync("api/Medicamentos/listaMedicamentos");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var medicamentos= JsonConvert.DeserializeObject<IEnumerable<MedicamentosViewModel>>(content);
                return View("Index", medicamentos);
            }
            else
            {
                // Manejar el caso de error al obtener los detalles del medicamento.
                return View(new List<MedicamentosViewModel>());
            }
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MedicamentosViewModel medicamento)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(medicamento);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Medicamentos/crearMedicamento", content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el caso de creación exitosa.
                    TempData["Success"] = "Medicamento agregado exitosamente!";
                    return RedirectToAction("Index");
                }
                else
                {
                    // Manejar el caso de error en la solicitud POST, por ejemplo, mostrando un mensaje de error.
                    ModelState.AddModelError(string.Empty, "Error al crear el medicamento.");
                }
            }
            return View(medicamento);
        }
        public async Task<IActionResult> Edit(int id)
        {

            var response = await _httpClient.GetAsync($"/api/Medicamentos/verMedicamento?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var medicamento = JsonConvert.DeserializeObject<MedicamentosViewModel>(content);

                // Devuelve la vista de edición con los detalles del Medicamento.
                return View(medicamento);
            }
            else
            {
                // Manejar el caso de error al obtener los detalles del Medicamento.
                return RedirectToAction("Details"); // Redirige a la página de lista de Medicamentos u otra acción apropiada.
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MedicamentosViewModel medicamento)
        {
            if (ModelState.IsValid)
            {


                var json = JsonConvert.SerializeObject(medicamento);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Medicamentos/editarMedicamento?id={id}", content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el caso de actualización exitosa, por ejemplo, redirigiendo a la página de detalles del Medicamento.
                    TempData["Success"] = "Medicamento actualizado exitosamente!";
                    return RedirectToAction("Index", new { id });
                }
                else
                {
                    // Manejar el caso de error en la solicitud PUT o POST, por ejemplo, mostrando un mensaje de error.
                    ModelState.AddModelError(string.Empty, "Error al actualizar el medicamento.");
                }
            }

            // Si hay errores de validación, vuelve a mostrar el formulario de edición con los errores.
            return View(medicamento);
        }

        public async Task<IActionResult> Details(int id)
        {

            var response = await _httpClient.GetAsync($"/api/Medicamentos/verMedicamento?id={id}");


            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var medicamento = JsonConvert.DeserializeObject<MedicamentosViewModel>(content);

                // Devuelve la vista de edición con los detalles del Medicamento.
                return View(medicamento);
            }
            else
            {
                // Manejar el caso de error al obtener los detalles del Medicamento.
                return RedirectToAction("Details"); // Redirige a la página de lista de Medicamentos u otra acción apropiada.
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Medicamentos/eliminarMedicamento?id={id}");

            if (response.IsSuccessStatusCode)
            {
                // Maneja el caso de eliminación exitosa, por ejemplo, redirigiendo a la página de lista de medicamentos.
                return RedirectToAction("Index");
            }
            else
            {
                // Maneja el caso de error en la solicitud DELETE, por ejemplo, mostrando un mensaje de error.
                TempData["Error"] = "Error al eliminar el medicamento.";
                return RedirectToAction("Index");
            }
        }
    }

}
