using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parcial3.Models;

namespace Parcial3.Clases
{
        public class clsMatricula
    {
        private DBExamenEntities2 dbExamen = new DBExamenEntities2();

        private Matricula matricula { get; set; }


        public string Insertar(Matricula nuevaMatricula)
        {
            try
            {
                dbExamen.Matriculas.Add(nuevaMatricula);
                dbExamen.SaveChanges();
                return "Se ingresó la matricula la base de datos";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Matricula Consultar(string documento, string semestre)
        {
            Matricula mat= dbExamen.Matriculas.FirstOrDefault(m => m.Estudiante.Documento == documento && m.SemestreMatricula == semestre);
            return mat;
        }
        public List<Matricula> ConsultarTodos()
        {
            return dbExamen.Matriculas
                .OrderBy(m => m.idMatricula)
                .ToList();
        }

        public string Actualizar(string documento, string semestre, Matricula matricula)
        {
            try
            {
                Matricula mat = Consultar(documento, semestre);
                if (mat == null)
                {
                    return "No se encontró la matricula a eliminar";
                }
                mat.TotalMatricula = matricula.TotalMatricula;
                mat.FechaMatricula = matricula.FechaMatricula;
                mat.MateriasMatriculadas = matricula.MateriasMatriculadas;
                mat.SemestreMatricula = matricula.SemestreMatricula;
                mat.NumeroCreditos = matricula.NumeroCreditos;
                mat.ValorCredito = matricula.ValorCredito;
                dbExamen.Matriculas.AddOrUpdate(mat);
                dbExamen.SaveChanges();
                return "Se actualizó la matricula correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Eliminar(string documento, string semestre)
        {
            try
            {
                Matricula mat = Consultar(documento, semestre);
                dbExamen.Matriculas.Remove(mat);
                dbExamen.SaveChanges();
                return "Se eliminó la matricula correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }

}
