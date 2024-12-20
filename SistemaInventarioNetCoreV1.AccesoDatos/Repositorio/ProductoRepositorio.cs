﻿using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaInventarioNetCoreV1.AccesoDatos.Data;
using SistemaInventarioNetCoreV1.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventarioNetCoreV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioNetCoreV1.AccesoDatos.Repositorio
{
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        private readonly ApplicationDbContext _db;
        public ProductoRepositorio(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Actualizar(Producto producto)
        {
            var productoBD = _db.Producto.FirstOrDefault(b=>b.Id==producto.Id);
            if (productoBD != null)
            {
                if (producto.ImagenUrl !=null)
                {
                    productoBD.ImagenUrl = producto.ImagenUrl;
                }

                productoBD.NumeroSerie = producto.NumeroSerie;
                productoBD.Descripcion = producto.Descripcion;
                productoBD.Precio = producto.Precio;
                productoBD.Costo= producto.Costo;
                productoBD.CategoriaId= producto.CategoriaId;
                productoBD.MarcaId= producto.MarcaId;
                productoBD.PadreId= producto.PadreId;
                productoBD.Estado=producto.Estado;
                _db.SaveChanges();
            }
        }

        public IEnumerable<SelectListItem> ObtenerTodosDropdownLista(string obj)
        {
            if (obj == "Categoria")
            {
                return _db.Categoria.Where(c=>c.Estado==true).Select(c=> new SelectListItem
                {
                    Text=c.Nombre,
                    Value=c.Id.ToString()
                });
            }
            if (obj == "Marca")
            {
                return _db.Marca.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                });
            }
            return null;
        }
    }
}
