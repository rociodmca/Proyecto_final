using Proyecto_final.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final.ViewModel
{
    public class ViewModelBBDD
    {
        AccesoBBDD bbdd;

        public ViewModelBBDD() 
        { 
            bbdd = new AccesoBBDD();
        }

        public bool InsertarUsuario(string nombre, string ape, string email, string pass, int rol)
        {
            try
            {
                bbdd.AddUser(nombre, ape, email, pass, rol);
                return true;
            } catch { return false; }
        }

        public string ObtenerId(string email, string pass)
        {
            try
            {
                Usuario user = bbdd.GetUser(email, pass);
                return user.Id;
            } catch (Exception ex) { return "1"; }
        }

        public int ObtenerRol(string email, string pass)
        {
            try
            {
                Usuario user = bbdd.GetUser(email, pass);
                return user.Rol;
            }
            catch (Exception ex) { return ex.GetHashCode(); }
        }

        public List<Usuario> ObtenerListaUsuarios()
        {
            List<Usuario> usuarios;
            usuarios = bbdd.ListUsuarios(1);
            return usuarios;
        }

        public bool BorrarUsuario(string id)
        {
            try
            {
                bbdd.RemoveUser(id);
                return true;
            } catch (Exception ex) { return false; }
        }

        public bool GuardarMascota(string nombre, string tipo, string raza, string sexo, int peso, string vacunas, string id_cl)
        {
            try
            {
                bbdd.AddPet(nombre, tipo, raza, sexo, peso, vacunas, id_cl);
                return true;
            }catch (Exception ex) { return false; }
        }

        public List<Mascota> ObtenerListaMascotas(string id)
        {
            List<Mascota> mascotas;
            mascotas = bbdd.GetPets(id);
            return mascotas;
        }
    }
}
