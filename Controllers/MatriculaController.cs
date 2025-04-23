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
        [Route("Consultar")]
        public Matricula Consultar(string documento, string semestre)
        {
            return matricula.Consultar(documento, semestre);
        }

        [HttpGet]
        [Route("ConsultarTodos")]
        public List<Matricula> ConsultarTodos()
        {
            return matricula.ConsultarTodos();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar(string documento,string semestre, [FromBody] Matricula _matricula)
        {
            return matricula.Actualizar(documento, semestre, _matricula);
        }


        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar(string documento, string semestre)
        {
            return matricula.Eliminar(documento, semestre);
        }
    }
}