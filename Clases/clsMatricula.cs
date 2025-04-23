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
                nuevaMatricula.TotalMatricula = nuevaMatricula.NumeroCreditos * nuevaMatricula.ValorCredito;
                dbExamen.Matriculas.Add(nuevaMatricula);
                dbExamen.SaveChanges();
                return "Se ingresó la matricula la base de datos";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public List<Matricula> ConsultarXSemestre(string semestre)
        {
            return dbExamen.Matriculas
                .Where(m => m.SemestreMatricula == semestre)
                .OrderBy(m => m.Estudiante.NombreCompleto)
                .ToList();
        }
        public Matricula Consultar(int idEstudiante)
        {
            Matricula mat = dbExamen.Matriculas.FirstOrDefault(p => p.idEstudiante == idEstudiante);
            return mat;
        }
        public List<Matricula> ConsultarTodos()
        {
            return dbExamen.Matriculas
                .OrderBy(m => m.idMatricula)
                .ToList();
        }

        public string Actualizar(int idEstudiante, Matricula matricula)
        {
            try
            {
                Matricula mat = Consultar(idEstudiante);
                if (mat == null)
                {
                    return "No se encontró la matricula a eliminar";
                }
                mat.TotalMatricula = matricula.NumeroCreditos * matricula.ValorCredito;
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

        public string Eliminar(int idEstudiante)
        {
            try
            {
                Matricula prod = Consultar(idEstudiante);
                if (prod == null)
                {
                    return "El producto no existe";
                }
                dbExamen.Matriculas.Remove(prod);
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
