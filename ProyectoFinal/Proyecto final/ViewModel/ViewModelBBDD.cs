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

        public ViewModelBBDD() 
        { 
            bbdd = new AccesoBBDD();
        }

        public bool InsertarUsuario(string nombre, string ape, string email, string pass, int rol)
        {
            try
            {
                bbdd.AddUser(nombre, ape, email, pass, rol);
                Thread thread_back = new Thread(() => InsertarAjuste(ape, pass));
                thread_back.IsBackground = true;
                thread_back.Start();
                return true;
            } catch { return false; }
        }

        public void InsertarAjuste(string ape, string pass)
        {
            string id = ObtenerId(ape, pass);
            Thread.Sleep(100);
            bbdd.AddAdjust(new ObjectId(id), "claro", "14", "Espanol");
            Thread.Sleep(100);
        }

        public string ObtenerId(string email, string pass)
        {
            try
            {
                Usuario user = bbdd.GetUser(email, pass);
                return user.Id;
            } catch (Exception) { return "1"; }
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

        public string ObtenerNombre(ObjectId id)
        {
            try
            {
                Usuario user = bbdd.GetUser(id);
                return user.Nombre;
            }
            catch (Exception ex) { return ex.Message; }
        }

        public List<Usuario> ObtenerListaUsuarios()
        {
            List<Usuario> usuarios;
            usuarios = bbdd.ListUsuarios(1);
            return usuarios;
        }

        public List<Usuario> ObtenerListaVeterinarios()
        {
            List<Usuario> usuarios;
            usuarios = bbdd.ListVet(2);
            return usuarios;
        }

        public bool ActualizarPass(ObjectId id, string newpass)
        {
            try
            {
                bbdd.UpdateUser(id, newpass);
                return true;
            }catch (Exception) { return false; }
        }

        public bool BorrarUsuario(string id)
        {
            try
            {
                bbdd.RemoveUser(id);
                bbdd.RemoveAdjust(new ObjectId(id));
                return true;
            } catch (Exception) { return false; }
        }

        public bool GuardarMascota(string nombre, string tipo, string raza, string sexo, int peso, string vacunas, Uri imagen, ObjectId id_cl)
        {
            try
            {
                bbdd.AddPet(nombre, tipo, raza, sexo, peso, vacunas, imagen, id_cl);
                return true;
            }catch (Exception) { return false; }
        }

        public List<Mascota> ObtenerListaMascotas(ObjectId id)
        {
            List<Mascota> mascotas;
            mascotas = bbdd.GetPets(id);
            return mascotas;
        }

        public List<Mascota> ObtenerListaMascotasSinId()
        {
            List<Mascota> mascotas;
            mascotas = bbdd.GetPetsWithoutId();
            return mascotas;
        }

        public string ObtenerNombreMascota(ObjectId id)
        {
            try
            {
                Mascota mascota = bbdd.GetPet(id);
                return mascota.Nombre;
            }
            catch (Exception ex) { return ex.Message; }
        }

        public bool GuardarCita(ObjectId id_cl, ObjectId id_mas, DateTime fecha, ObjectId id_vet)
        {
            try
            {
                bbdd.AddDate(id_cl, id_mas, fecha, id_vet);
                return true;
            } catch { return false; }
        }

        public List<Cita> ObtenerListaCitas(ObjectId id)
        {
            List<Cita> citas;
            citas = bbdd.GetDates(id);
            return citas;
        }

        public List<Cita> ObtenerListaCitasVeterinario(ObjectId id)
        {
            List<Cita> citas;
            citas = bbdd.GetDatesVet(id);
            return citas;
        }

        public Dictionary<string, Object> ObtenerCitasP(string id)
        {
            return bbdd.GetDatesList(id);
        }

        public Ajuste ObtenerAjuste(ObjectId id)
        {
            return bbdd.GetAdjust(id);
        }

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
