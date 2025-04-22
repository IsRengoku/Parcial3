using Parcial3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parcial3.Clases
{
    public class clsUsuario
    {
        private DBExamenEntities1 DBExamen = new DBExamenEntities1();
        public Usuario usuario { get; set; }
        public string CrearUsuario(int idPerfil)
        {
            //Se van a crear el usuario y el usuario perfil
            clsCypher cypher = new clsCypher();
            string ClaveCifrada;
            cypher.Password = usuario.Clave;
            if (cypher.CifrarClave())
            {
                ClaveCifrada = cypher.PasswordCifrado;
            }
            else
            {
                return "Error al cifrar la clave";
            }
            //Graba el usuario
            usuario.Clave = ClaveCifrada;
            usuario.Salt = cypher.Salt;
            DBExamen.Usuarios.Add(usuario);
            DBExamen.SaveChanges();
            //Graba el usuario perfil
            Usuario_Perfil usuarioPerfil = new Usuario_Perfil();
            usuarioPerfil.idUsuario = usuario.idUsuario;
            usuarioPerfil.idPerfil = idPerfil;
            usuarioPerfil.Activo = true; //Cuando se crea normalmente, debe ser activo
            DBExamen.Usuario_Perfil.Add(usuarioPerfil);
            DBExamen.SaveChanges();
            return "Se creó el usuario exitosamente";
        }
    }
}