using Microsoft.Maui.ApplicationModel.Communication;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final.Model
{
    /// <summary>
    /// Modelo que conecta con la base de datos
    /// </summary>
    public class AccesoBBDD
    {
        /// <summary>
        /// Variables globales
        /// </summary>
        MongoClient cliente;
        IMongoDatabase db;
        IMongoCollection<Usuario> collectionUsuario;
        IMongoCollection<Mascota> collectionMascota;
        IMongoCollection<Cita> collectionCita;
        IMongoCollection<Ajuste> collectionAjuste;
        const int keySize = 64;
        const int iterations = 350;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        byte[] salt = new byte[8];

        /// <summary>
        /// Constructor del modelo acceso a datos
        /// </summary>
        public AccesoBBDD()
        {
            
            //Conexión a la base de datos
            db = cliente.GetDatabase("prueba_app");
        }

        /// <summary>
        /// Método para transformar la contraseña a base64
        /// </summary>
        /// <param name="pass">contraseña en texto plano</param>
        /// <returns>string de la contraseña en base64</returns>
        /// <exception cref="Exception">devolverá </exception>
        public string PassParser(string pass)
        {
            try
            {
                byte[] encData_byte = new byte[pass.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(pass);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en base64Encode" + ex.Message);
            }
        }

        /// <summary>
        /// Método para hashear la contraseña
        /// </summary>
        /// <param name="password">contraseña en texto plano</param>
        /// <returns>string de la contraseña hasheada</returns>
        public string HashPasword(string password)
        {
            password = PassParser(password);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }

        /// <summary>
        /// Método para añadir un usuario a la base de datos
        /// </summary>
        /// <param name="nombre">nombre del usuario</param>
        /// <param name="ape">apellidos del usuario</param>
        /// <param name="email">correo electrónico del usuario</param>
        /// <param name="pass">contraseña del usuario</param>
        /// <param name="rol">rol del usuario</param>
        public void AddUser(string nombre, string ape, string email, string pass, int rol)
        {
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");
            string passParser = HashPasword(pass);
            Usuario user = new Usuario { Nombre = nombre, Apellidos = ape, Email = email, Pass = passParser, Rol = rol };
            collectionUsuario.InsertOne(user);
        }

        /// <summary>
        /// Método para obtener el primer usuario dados los parámetros
        /// </summary>
        /// <param name="email">correo electrónico</param>
        /// <param name="pass">contraseña</param>
        /// <returns>Primer usuario que cumpla con esos requisitos</returns>
        public Usuario GetUser(string email, string pass)
        {
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");
            string passParser = HashPasword(pass);
            FilterDefinition<Usuario> filtro = Builders<Usuario>.Filter.And(Builders<Usuario>.Filter.Eq(x => x.Email, email), Builders<Usuario>.Filter.Eq(x => x.Pass, passParser));
            return collectionUsuario.Find(filtro).First<Usuario>();
        }

        /// <summary>
        /// Método para obtener un usuario un usuario 
        /// </summary>
        /// <param name="id">identificador del usuario</param>
        /// <returns>primer usuario que encuentra con ese identificador</returns>
        public Usuario GetUser(ObjectId id)
        {
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");
            FilterDefinition<Usuario> filtro = Builders<Usuario>.Filter.And(Builders<Usuario>.Filter.Eq(x => x.Id, id.ToString()));
            return collectionUsuario.Find(filtro).First<Usuario>();
        }

        /// <summary>
        /// Método para obtener la lista de usuarios que no tienen un determinado rol 
        /// </summary>
        /// <param name="rol">rol del usuario</param>
        /// <returns>lista de usuarios que no tienen el rol pasado por parámetro</returns>
        public List<Usuario> ListUsuarios(int rol)
        {
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");
            FilterDefinition<Usuario> filtro = Builders<Usuario>.Filter.Ne(x => x.Rol, rol);
            return collectionUsuario.Find(filtro).ToList();
        }

        /// <summary>
        /// Método que devuelve la lista de veterinarios
        /// </summary>
        /// <param name="rol">rol de veterinarios</param>
        /// <returns>lista de usuarios que tienen el rol de veterinarios</returns>
        public List<Usuario> ListVet(int rol)
        {
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");
            FilterDefinition<Usuario> filtro = Builders<Usuario>.Filter.Eq(x => x.Rol, rol);
            return collectionUsuario.Find(filtro).ToList();
        }

        /// <summary>
        /// Método para actualizar la contraseña del usuario
        /// </summary>
        /// <param name="id">identificador del usuario a actualizar</param>
        /// <param name="newpass">contraseña nueva</param>
        public void UpdateUser(ObjectId id, string newpass)
        {
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");
            var filter = Builders<Usuario>.Filter.Eq(x => x.Id, id.ToString());
            string pass = HashPasword(newpass);
            var update = Builders<Usuario>.Update.Set(x => x.Pass, pass);
            collectionUsuario.UpdateOne(filter, update);
        }

        /// <summary>
        /// Método para borrar un usuario
        /// </summary>
        /// <param name="id">identificador del usuario a borrar</param>
        public void RemoveUser(string id)
        {
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");
            FilterDefinition<Usuario> filtro = Builders<Usuario>.Filter.Eq(x => x.Id, id);
            collectionUsuario.DeleteOne(filtro);
        }

        /// <summary>
        /// Método para añadir mascotas
        /// </summary>
        /// <param name="nombre">nombre de la mascota</param>
        /// <param name="tipo">tipo de mascota</param>
        /// <param name="raza">raza de la mascota</param>
        /// <param name="sexo">sexo de la mascota</param>
        /// <param name="peso">peso de la mascota</param>
        /// <param name="vacunas">vacunas que tiene la mascota</param>
        /// <param name="imagen">imagen de la mascota</param>
        /// <param name="id_cl">identificador del dueño de la mascota</param>
        public void AddPet(string nombre, string tipo, string raza, string sexo, int peso, string vacunas, Uri imagen, ObjectId id_cl)
        {
            collectionMascota = db.GetCollection<Mascota>("Mascotas");
            Mascota pet = new Mascota { Nombre = nombre, Tipo = tipo, Raza = raza, Sexo = sexo, Peso = peso, Vacunas = vacunas, Imagen = imagen, Id_Cliente = id_cl };
            collectionMascota.InsertOne(pet);
        }

        /// <summary>
        /// Método para obtener una mascota
        /// </summary>
        /// <param name="id">identificador de la mascota</param>
        /// <returns>primera mascota que encuentre con ese identificador</returns>
        public Mascota GetPet(ObjectId id)
        {
            collectionMascota = db.GetCollection<Mascota>("Mascotas");
            FilterDefinition<Mascota> filtro = Builders<Mascota>.Filter.Eq(x => x.Id, id.ToString());
            return collectionMascota.Find(filtro).First<Mascota>();
        }

        /// <summary>
        /// Método para borrar una mascota
        /// </summary>
        /// <param name="id">identificador de la mascota a borrar</param>
        public void RemovePet(string id)
        {
            collectionMascota = db.GetCollection<Mascota>("Mascotas");
            FilterDefinition<Mascota> filtro = Builders<Mascota>.Filter.Eq(x => x.Id, id);
            collectionMascota.DeleteOne(filtro);
        }

        /// <summary>
        /// Método para listar las mascotas del mismo dueño
        /// </summary>
        /// <param name="id">identificador del dueño de la mascota</param>
        /// <returns>lista de mascotas pertenecientes al dueño que tiene como identificador el id</returns>
        public List<Mascota> GetPets(ObjectId id)
        {
            collectionMascota = db.GetCollection<Mascota>("Mascotas");
            FilterDefinition<Mascota> filtro = Builders<Mascota>.Filter.Eq(x => x.Id_Cliente, id);
            return collectionMascota.Find(filtro).ToList();
        }

        /// <summary>
        /// Método para mostrar todas las mascotas
        /// </summary>
        /// <returns>lista de todas las mascotas de la colección Mascotas</returns>
        public List<Mascota> GetPetsWithoutId()
        {
            collectionMascota = db.GetCollection<Mascota>("Mascotas");
            return collectionMascota.AsQueryable<Mascota>().ToList();
        }

        /// <summary>
        /// Método para guardar citas
        /// </summary>
        /// <param name="id_cl">identificador del cliente</param>
        /// <param name="id_mas">identificador de la mascota</param>
        /// <param name="fecha">fecha de la cita</param>
        /// <param name="id_vet">identificador del veterinario</param>
        public void AddDate(ObjectId id_cl, ObjectId id_mas, DateTime fecha, ObjectId id_vet)
        {
            collectionCita = db.GetCollection<Cita>("Citas");
            Cita date = new Cita { Id_cliente = id_cl, Id_mascota = id_mas, Fecha = fecha, Id_veterinario = id_vet };
            collectionCita.InsertOne(date);
        }

        /// <summary>
        /// Método para obtener las citas agendadas por un determinado usuario
        /// </summary>
        /// <param name="id">identificador del usuario</param>
        /// <returns>lista de las citas del usuario identificado por el parámetro</returns>
        public List<Cita> GetDates(ObjectId id)
        {
            collectionCita = db.GetCollection<Cita>("Citas");
            FilterDefinition<Cita> filtro = Builders<Cita>.Filter.Eq(x => x.Id_cliente, id);
            return collectionCita.Find(filtro).ToList<Cita>();
        }

        /// <summary>
        /// Método para obtener las citas asignadas a un determinado veterinario
        /// </summary>
        /// <param name="id">identificador del veterinario</param>
        /// <returns>lista de citas</returns>
        public List<Cita> GetDatesVet(ObjectId id)
        {
            collectionCita = db.GetCollection<Cita>("Citas");
            FilterDefinition<Cita> filtro = Builders<Cita>.Filter.Eq(x => x.Id_veterinario, id);
            return collectionCita.Find(filtro).ToList();
        }

        /// <summary>
        /// Método para borrar una cita por su identificador
        /// </summary>
        /// <param name="id">identificador de la cita</param>
        public void RemoveDate(string id)
        {
            collectionCita = db.GetCollection<Cita>("Citas");
            FilterDefinition<Cita> filtro = Builders<Cita>.Filter.Eq(x => x.Id, id);
            collectionCita.DeleteOne(filtro);
        }

        /// <summary>
        /// Método para guardar un ajuste
        /// </summary>
        /// <param name="id_user">identificador del usuario</param>
        /// <param name="tema">tema elegido</param>
        /// <param name="tam_letra">tamaño de letra</param>
        /// <param name="idioma">idioma predilecto</param>
        public void AddAdjust(ObjectId id_user, string tema, string tam_letra, string idioma)
        {
            collectionAjuste = db.GetCollection<Ajuste>("Ajustes");
            Ajuste adjust = new Ajuste { Id = id_user, Tema = tema, Tam_letra = tam_letra, Idioma = idioma };
            collectionAjuste.InsertOne(adjust);
        }

        /// <summary>
        /// Método para obtener los ajustes de un usuario
        /// </summary>
        /// <param name="id">identificador del usuario</param>
        /// <returns>ajuste del usuario</returns>
        public Ajuste GetAdjust(ObjectId id)
        {
            collectionAjuste = db.GetCollection<Ajuste>("Ajustes");
            FilterDefinition<Ajuste> filtro = Builders<Ajuste>.Filter.Eq(x => x.Id, id);
            return collectionAjuste.Find(filtro).First<Ajuste>();
        }

        /// <summary>
        /// Método para borrar el ajuste asignado de un usuario
        /// </summary>
        /// <param name="id">identificador del usuario</param>
        public void RemoveAdjust(ObjectId id)
        {
            collectionAjuste = db.GetCollection<Ajuste>("Ajustes");
            FilterDefinition<Ajuste> filtro = Builders<Ajuste>.Filter.Eq(x => x.Id, id);
            collectionAjuste.DeleteOne(filtro);
        }

        /// <summary>
        /// Método para actualizar un ajuste
        /// </summary>
        /// <param name="id">identificador del usuario</param>
        /// <param name="tema">tema elegido</param>
        /// <param name="tam_letra">tamaño de letra elegido</param>
        /// <param name="idioma">idioma elegido</param>
        public void UpdateAdjust(ObjectId id, string tema, string tam_letra, string idioma)
        {
            collectionAjuste = db.GetCollection<Ajuste>("Ajustes");
            var filter = Builders<Ajuste>.Filter.Eq(x => x.Id, id);
            var update = Builders<Ajuste>.Update.Set(x => x.Tema, tema).Set(x => x.Tam_letra, tam_letra).Set(x => x.Idioma, idioma);
            collectionAjuste.UpdateOne(filter, update);
        }
    }
}
