using MongoDB.Bson;
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

        /// <summary>
        /// Constructor
        /// </summary>
        public ViewModelBBDD() 
        { 
            bbdd = new AccesoBBDD();
        }

        /// <summary>
        /// Método para insertar usuario
        /// </summary>
        /// <param name="nombre">nombre del usuario</param>
        /// <param name="ape">apellidos del usuario</param>
        /// <param name="email">correo electrónico del usuario</param>
        /// <param name="pass">contraseña del usuario</param>
        /// <param name="rol">rol del usuario</param>
        /// <returns>true si se insertado correctamente y false si lo contrario</returns>
        public bool InsertarUsuario(string nombre, string ape, string email, string pass, int rol)
        {
            try
            {
                bbdd.AddUser(nombre, ape, email, pass, rol);
                Thread thread_back = new Thread(() => InsertarAjuste(ape, pass));
                //thread_back.IsBackground = true;
                thread_back.Start();
                return true;
            } catch { return false; }
        }

        /// <summary>
        /// Método para insertar un ajuste 
        /// </summary>
        /// <param name="email">correo electrónico del usuario</param>
        /// <param name="pass">contraseña del usuario</param>
        public void InsertarAjuste(string email, string pass)
        {
            string id = ObtenerId(email, pass);
            Thread.Sleep(100);
            bbdd.AddAdjust(new ObjectId(id), "claro", "14", "Espanol");
            Thread.Sleep(100);
        }

        /// <summary>
        /// Método para obtener el identificador del usuario
        /// </summary>
        /// <param name="email">correo electrónico del usuario</param>
        /// <param name="pass">contraseña del usuario</param>
        /// <returns>identificador del usuario</returns>
        public string ObtenerId(string email, string pass)
        {
            try
            {
                Usuario user = bbdd.GetUser(email, pass);
                return user.Id;
            } catch (Exception) { return "1"; }
        }

        /// <summary>
        /// Método para obtener el rol de un usuario
        /// </summary>
        /// <param name="email">correo electrónico del usuario</param>
        /// <param name="pass">contraseña del usuario</param>
        /// <returns>rol del usuario</returns>
        public int ObtenerRol(string email, string pass)
        {
            try
            {
                Usuario user = bbdd.GetUser(email, pass);
                return user.Rol;
            }
            catch (Exception ex) { return ex.GetHashCode(); }
        }

        /// <summary>
        /// Método para obtener el nombre del usuario dado el identificador
        /// </summary>
        /// <param name="id">identificador del usuario</param>
        /// <returns>nombre del usuario</returns>
        public string ObtenerNombre(ObjectId id)
        {
            try
            {
                Usuario user = bbdd.GetUser(id);
                return user.Nombre;
            }
            catch (Exception ex) { return ex.Message; }
        }

        /// <summary>
        /// Método para obtener la lista de usuarios
        /// </summary>
        /// <returns>lista de objetos de tipo Usuario</returns>
        public List<Usuario> ObtenerListaUsuarios()
        {
            List<Usuario> usuarios;
            usuarios = bbdd.ListUsuarios(1);
            return usuarios;
        }

        /// <summary>
        /// Método para obtener la lista de veterinarios
        /// </summary>
        /// <returns>lista de objetos de tipo Usuario</returns>
        public List<Usuario> ObtenerListaVeterinarios()
        {
            List<Usuario> usuarios;
            usuarios = bbdd.ListVet(2);
            return usuarios;
        }

        /// <summary>
        /// Método para actualizar la contraseña
        /// </summary>
        /// <param name="id">identificador del usuario</param>
        /// <param name="newpass">nueva contraseña</param>
        /// <returns>true si se actualiza y false si no</returns>
        public bool ActualizarPass(ObjectId id, string newpass)
        {
            try
            {
                bbdd.UpdateUser(id, newpass);
                return true;
            }catch (Exception) { return false; }
        }

        /// <summary>
        /// Método para borrar un usuario
        /// </summary>
        /// <param name="id">identificador del usuario a borrar</param>
        /// <returns>true si se ha borrado y false si no</returns>
        public bool BorrarUsuario(string id)
        {
            try
            {
                bbdd.RemoveUser(id);
                bbdd.RemoveAdjust(new ObjectId(id));
                return true;
            } catch (Exception) { return false; }
        }

        /// <summary>
        /// Método para guardar una mascota
        /// </summary>
        /// <param name="nombre">nombre de la mascota</param>
        /// <param name="tipo">tipo de la mascota</param>
        /// <param name="raza">raza de la mascota</param>
        /// <param name="sexo">sexo de la mascota</param>
        /// <param name="peso">peso de la mascota</param>
        /// <param name="vacunas">vacunas de la mascota</param>
        /// <param name="imagen">uri de la mascota</param>
        /// <param name="id_cl">identificador del dueño de la mascota</param>
        /// <returns>true si se ha guardado y false si no</returns>
        public bool GuardarMascota(string nombre, string tipo, string raza, string sexo, int peso, string vacunas, Uri imagen, ObjectId id_cl)
        {
            try
            {
                bbdd.AddPet(nombre, tipo, raza, sexo, peso, vacunas, imagen, id_cl);
                return true;
            }catch (Exception) { return false; }
        }

        /// <summary>
        /// Método para borrar una mascota
        /// </summary>
        /// <param name="id">identificador de la mascota</param>
        /// <returns>true si se ha eliminado correctamente y false si no</returns>
        public bool BorrarMascota(string id)
        {
            try
            {
                bbdd.RemovePet(id);
                return true;
            } catch (Exception) { return false; }
        }

        /// <summary>
        /// Método para obtener la lista de mascotas
        /// </summary>
        /// <param name="id">identificador del dueño</param>
        /// <returns>lista de objetos de tipo Mascota</returns>
        public List<Mascota> ObtenerListaMascotas(ObjectId id)
        {
            List<Mascota> mascotas;
            mascotas = bbdd.GetPets(id);
            return mascotas;
        }

        /// <summary>
        /// Método para obtener todas las mascotas
        /// </summary>
        /// <returns>lista de objetos de tipo Mascota</returns>
        public List<Mascota> ObtenerListaMascotasSinId()
        {
            List<Mascota> mascotas;
            mascotas = bbdd.GetPetsWithoutId();
            return mascotas;
        }

        /// <summary>
        /// Método para obtener el nombre de la mascota que tenga el identificador
        /// pasado por parámetro
        /// </summary>
        /// <param name="id">identificador de la mascota</param>
        /// <returns>nombre de la mascota</returns>
        public string ObtenerNombreMascota(ObjectId id)
        {
            try
            {
                Mascota mascota = bbdd.GetPet(id);
                return mascota.Nombre;
            }
            catch (Exception ex) { return ex.Message; }
        }

        /// <summary>
        /// Método para guardar una cita
        /// </summary>
        /// <param name="id_cl">identificador del cliente</param>
        /// <param name="id_mas">identificador de la mascota</param>
        /// <param name="fecha">fecha</param>
        /// <param name="id_vet">identificador del veterinario</param>
        /// <returns>true si se ha guardado correctamente y false si no</returns>
        public bool GuardarCita(ObjectId id_cl, ObjectId id_mas, DateTime fecha, ObjectId id_vet)
        {
            try
            {
                bbdd.AddDate(id_cl, id_mas, fecha, id_vet);
                return true;
            } catch { return false; }
        }

        /// <summary>
        /// Método para obtener la lista de citas
        /// </summary>
        /// <param name="id">identificador del usuario</param>
        /// <returns>lista de objetos de tipo Cita</returns>
        public List<Cita> ObtenerListaCitas(ObjectId id)
        {
            List<Cita> citas;
            citas = bbdd.GetDates(id);
            return citas;
        }

        /// <summary>
        /// Método para obtener la lista de citas de citas en función del veterinario
        /// </summary>
        /// <param name="id">identificador del veterinario</param>
        /// <returns>lista de objetos Cita</returns>
        public List<Cita> ObtenerListaCitasVeterinario(ObjectId id)
        {
            List<Cita> citas;
            citas = bbdd.GetDatesVet(id);
            return citas;
        }

        /// <summary>
        /// Método para borrar una cita
        /// </summary>
        /// <param name="id">identificador de la cita</param>
        /// <returns>true si se borra correctamente y false si no</returns>
        public bool BorrarCita(string id)
        {
            try
            {
                bbdd.RemoveDate(id);
                return true;
            }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// Método para obtener el ajuste del usuario
        /// </summary>
        /// <param name="id">identificador del usuario</param>
        /// <returns>objeto de tipo Ajuste</returns>
        public Ajuste ObtenerAjuste(ObjectId id)
        {
            return bbdd.GetAdjust(id);
        }

        /// <summary>
        /// Método para actualizar el ajuste
        /// </summary>
        /// <param name="id">identificador del usuario</param>
        /// <param name="tema">tema</param>
        /// <param name="tam_letra">tamaño de la letra</param>
        /// <param name="idioma">idioma</param>
        /// <returns>true si se ha actualizado y false si no</returns>
        public bool ActualizarAjuste(ObjectId id, string tema, string tam_letra, string idioma)
        {
            try
            {
                bbdd.UpdateAdjust(id, tema, tam_letra, idioma);
                return true;
            } catch (Exception) { return false; }
        }
    }
}
