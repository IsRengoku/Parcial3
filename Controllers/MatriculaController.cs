using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Parcial3.Clases;
using Parcial3.Models;

namespace Parcial3.Controllers
{
    [RoutePrefix("api/Matricula")]
    [Authorize]
    public class MatriculaController : ApiController
    {
        private clsMatricula matricula = new clsMatricula();

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Matricula nuevaMatricula)
        {
            return matricula.Insertar(nuevaMatricula);
        }

        [HttpGet]
        [Route("ConsultarXId")]
        public Matricula ConsultarXId(int idEstudiante)
        {
            return matricula.Consultar(idEstudiante);
        }
        [HttpGet]
        [Route("ConsultarXSemestre")]
        public List<Matricula> ConsultarXSemestre(string semestre)
        {
            return matricula.ConsultarXSemestre(semestre);
        }

        [HttpGet]
        [Route("ConsultarTodos")]
        public List<Matricula> ConsultarTodos()
        {
            return matricula.ConsultarTodos();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar(int idEstudiante, [FromBody] Matricula _matricula)
        {
            return matricula.Actualizar(idEstudiante, _matricula);
        }


        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar(int idEstudiante)
        {
            return matricula.Eliminar(idEstudiante);
        }
    }
}