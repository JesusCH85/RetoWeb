using CepdiRetoWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CepdiRetoWeb.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly HttpClient? _httpClient;

        public UsuariosController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7069/api");
        }

        public async Task<IActionResult> Index()
        {

            var response = await _httpClient.GetAsync("api/Usuarios/listaUsuarios");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var usuarios = JsonConvert.DeserializeObject<IEnumerable<UsuariosViewModel>>(content);
                return View("Index", usuarios);
            }
            else
            {
                // Manejar el caso de error al obtener los detalles del usuario.
                return View(new List<UsuariosViewModel>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsuariosViewModel dUsuario)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(dUsuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Usuarios/crearUsuario", content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el caso de creación exitosa.
                    TempData["Success"] = "Usuario cargado exitosamente!";
                    return RedirectToAction("Index");
                }
                else
                {
                    // Manejar el caso de error en la solicitud POST, por ejemplo, mostrando un mensaje de error.
                    ModelState.AddModelError(string.Empty, "Error al crear el usuarios.");
                }
            }
            return View(dUsuario);
        }
        public async Task<IActionResult> Edit(int id)
        {

            var response = await _httpClient.GetAsync($"/api/Usuarios/verUsuario?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var usuario = JsonConvert.DeserializeObject<UsuariosViewModel>(content);

                // Devuelve la vista de edición con los detalles del usuario.
                return View(usuario);
            }
            else
            {
                // Manejar el caso de error al obtener los detalles del usuario
                
                return RedirectToAction("Details"); // Redirige a la página de lista de usuarios u otra acción apropiada.
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UsuariosViewModel dUsuario)
        {
            if (ModelState.IsValid)
            {


                var json = JsonConvert.SerializeObject(dUsuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Usuarios/editarUsuario?id={id}", content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el caso de actualización exitosa, por ejemplo, redirigiendo a la página de detalles del Usuario.
                    TempData["Success"] = "Usuario actualizado exitosamente!";
                    return RedirectToAction("Index");
                }
                else
                {
                    // Manejar el caso de error en la solicitud PUT o POST, por ejemplo, mostrando un mensaje de error.
                    ModelState.AddModelError(string.Empty, "Error al actualizar el Usuario.");
                }
            }

            // Si hay errores de validación, vuelve a mostrar el formulario de edición con los errores.
            return View(dUsuario);
        }

        public async Task<IActionResult> Details(int id)
        {

            var response = await _httpClient.GetAsync($"/api/Usuarios/verUsuario?id={id}");


            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var usuario = JsonConvert.DeserializeObject<UsuariosViewModel>(content);

                // Devuelve la vista de edición con los detalles del Usuario.
                return View(usuario);
            }
            else
            {
                // Manejar el caso de error al obtener los detalles del Usuario.
                return RedirectToAction("Details"); // Redirige a la página de lista de Usuarios u otra acción apropiada.
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Usuarios/eliminarUsuario?id={id}");

            if (response.IsSuccessStatusCode)
            {
                // Maneja el caso de eliminación exitosa, por ejemplo, redirigiendo a la página de lista de Usuarios.
                return RedirectToAction("Index");
            }
            else
            {
                // Maneja el caso de error en la solicitud DELETE, por ejemplo, mostrando un mensaje de error.
                TempData["Error"] = "Error al eliminar el usuario.";
                return RedirectToAction("Index");
            }
        }


    }
}
