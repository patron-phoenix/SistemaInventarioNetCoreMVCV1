using Microsoft.AspNetCore.Mvc;
using SistemaInventarioNetCoreV1.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventarioNetCoreV1.Modelos;
using SistemaInventarioNetCoreV1.Utilidades;

namespace SistemaInventarioNetCoreV1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public CategoriaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Categoria  categoria = new Categoria();
            if (id==null)
            {
                //crear nueva bodega
                categoria.Estado = true;
                return View(categoria);
            }
            //actualizamos bodega
            categoria = await _unidadTrabajo.Categoria.Obtener(id.GetValueOrDefault());
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> Upsert(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                if (categoria.Id == 0)
                {
                    await _unidadTrabajo.Categoria.Agregar(categoria);
                    TempData[DS.Exitosa] = "Categoría Creada Exitosamente";
                }
                else
                {
                    _unidadTrabajo.Categoria.Actualizar(categoria);
                    TempData[DS.Exitosa] = "Categoría Actualizada Exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction("Index");
            }
            TempData[DS.Error] = "Error al crear Categoría";
            return View(categoria);
        }


        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Categoria.ObtenerTodos();
            return Json(new {data=todos});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var categoriaDb = await _unidadTrabajo.Categoria.Obtener(id);
            if (categoriaDb == null)
            {
                return Json(new {success=false,message="Error al Borrar Categoría"});
            }
            _unidadTrabajo.Categoria.Remover(categoriaDb);
            await _unidadTrabajo.Guardar();
            return Json(new { success=true,message="Categoría Borrada Exitosamente"});
        }


        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id=0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Categoria.ObtenerTodos();
            if (id==0)
            {
                valor = lista.Any(b=>b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && b.Id !=id);
            }
            if (valor)
            {
                return Json(new {data=true});
            }
            return Json(new { data = false });
        }

        #endregion

    }
}
