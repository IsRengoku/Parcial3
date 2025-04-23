using Parcial3.Models;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace Parcial3.Clases
{
    public class clsUsuario
    {
        private DBExamenEntities2 DBExamen = new DBExamenEntities2();
        public Usuario usuario { get; set; }

        public string CrearUsuario(int idPerfil)
        {
            try
            {
                // 1. Validaciones básicas
                if (usuario == null)
                    return "El objeto usuario no puede ser nulo";

                if (string.IsNullOrWhiteSpace(usuario.userName))
                    return "El nombre de usuario es requerido";

                if (string.IsNullOrWhiteSpace(usuario.Clave))
                    return "La contraseña es requerida";

                if (usuario.idEstudiante <= 0)
                    return "ID de estudiante inválido";

                // 2. Validar existencia de relaciones
                var estudianteExistente = DBExamen.Estudiantes.Find(usuario.idEstudiante);
                if (estudianteExistente == null)
                    return "No existe un estudiante con el ID proporcionado";

                var perfilExistente = DBExamen.Perfils.Find(idPerfil);
                if (perfilExistente == null)
                    return "No existe un perfil con el ID proporcionado";

                // 3. Cifrado de contraseña
                clsCypher cypher = new clsCypher();
                cypher.Password = usuario.Clave;

                if (!cypher.CifrarClave())
                    return "Error al cifrar la contraseña";

                // 4. Asignar valores cifrados
                usuario.Clave = cypher.PasswordCifrado;
                usuario.Salt = cypher.Salt;

                // 6. Guardar en base de datos
                DBExamen.Usuarios.Add(usuario);
                DBExamen.SaveChanges();

                // 7. Crear relación Usuario_Perfil
                var usuarioPerfil = new Usuario_Perfil
                {
                    idUsuario = usuario.idUsuario,
                    idPerfil = idPerfil,
                    Activo = true
                };

                DBExamen.Usuario_Perfil.Add(usuarioPerfil);
                DBExamen.SaveChanges();

                return "Usuario creado exitosamente";
            }
            catch (DbEntityValidationException ex)
            {
                // Capturar todos los errores de validación
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => $"- {e.PropertyName}: {e.ErrorMessage}");

                return $"Error de validación:\n{string.Join("\n", errorMessages)}";
            }
            catch (Exception ex)
            {
                return $"Error inesperado: {ex.Message}";
            }
        }
    }
}