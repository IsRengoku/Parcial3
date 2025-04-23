using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Parcial3.Clases
{
    [RoutePrefix("api/Matriculas")]
    [Authorize]
    public class MatriculasController : ApiController
    {
        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Matricula matricula)
        {
            clsMatricula _matricula = new clsMatricula();
            return "Matrícula registrada exitosamente.";
        }

        [HttpGet]
        [Route("Consultar/{documento}/{semestre}")]
        public IQueryable<Matricula> Consultar(string documento, string semestre)
        {
            clsMatricula _matricula = new clsMatricula();
            return _matricula.ConsultarMatricula(documento, semestre);
        }

        [HttpPut]
        [Route("Actualizar/{id}")]
        public IHttpActionResult Actualizar(int id, [FromBody] Matricula matriculaActualizada)
        {
            clsMatricula _matricula = new clsMatricula();
            if (_matricula.ActualizarMatricula(id, matriculaActualizada))
            {
                return Ok("Matrícula actualizada exitosamente.");
            }
            return NotFound($"No se encontró la matrícula con ID {id}.");
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IHttpActionResult Eliminar(int id)
        {
            clsMatricula _matricula = new clsMatricula();
            if (_matricula.EliminarMatricula(id))
            {
                return Ok("Matrícula eliminada exitosamente.");
            }
            return NotFound();
        }
    }
}