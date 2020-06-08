

namespace Model
{
    using Helper;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Data.Entity.Validation;
    using System.IO;
    using System.Linq;
    using System.Web;

    [Table("CatalogoPrecios")]
    public class CatalogoPrecios
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCatalogoPrecio { get; set; }

        [DisplayName("Nombre")]
        [StringLength(30)]
        public string nombre { get; set; }

        [DisplayName("caracteristicas")]
        [StringLength(30)]
        public string caracteristicas { get; set; }

        [DisplayName("precio")]
        [StringLength(30)]
        public string precio { get; set; }

        [DisplayName("otros")]
        [StringLength(30)]
        public string otros { get; set; }

        [DisplayName("empresa")]
        [StringLength(30)]
        public string empresa { get; set; }

        [DisplayName("estado")]
        [StringLength(30)]
        public string estado { get; set; }

        //public JsonResult listarAlumnos()
        //{
        //    var lista = (bd.Alumno.Where(p => p.BHABILITADO.Equals(1))
        //        .Select(p => new
        //        {
        //            p.IIDALUMNO,
        //            p.NOMBRE,
        //            p.APPATERNO,
        //            p.APMATERNO,
        //            p.TELEFONOPADRE
        //        })).ToList();
        //    return Json(lista, JsonRequestBehavior.AllowGet);
        //}


        public List<CatalogoPrecios> ListarCatalogoPorEmpresa(string nombreempresa)
        {
            var catalogo = new List<CatalogoPrecios>();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    catalogo = ctx.CatalogoPrecios.Where(p => p.empresa == nombreempresa).Distinct().ToList();
                }
            }
            catch (Exception E)
            {

                throw;
            }

            return catalogo;
        }



        //para lado CLIENTE




        public List<CatalogoPrecios> ListarCatalogoPorEmpresaTipo(string nombreempresa, string tipo)
        {
            var catalogo = new List<CatalogoPrecios>();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    catalogo = ctx.CatalogoPrecios.Where(p => p.empresa == nombreempresa && p.nombre==tipo).ToList();
                }
            }
            catch (Exception E)
            {

                throw;
            }

            return catalogo;
        }


        public List<CatalogoPrecios> ListarCatalogoPorEmpresayTipo(string nombreempresa, string tipo, string caracteristicas)
        {
            var catalogo = new List<CatalogoPrecios>();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    catalogo = ctx.CatalogoPrecios.Where(p => p.empresa == nombreempresa && p.nombre ==tipo && p.caracteristicas==caracteristicas).ToList();
                }
            }
            catch (Exception E)
            {

                throw;
            }

            return catalogo;
        }



    }
}
