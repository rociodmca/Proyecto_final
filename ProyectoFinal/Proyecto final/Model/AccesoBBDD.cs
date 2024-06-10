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
            ///Distinción entre una conexión desde una plataforma Android
            
            ///Conexión a la base de datos
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
                ///Se necesita crear un array de bytes del tamaño de la longitud de la cadena
                byte[] encData_byte = new byte[pass.Length];
                ///Se guarda en el array anterior los bytes en utf-8 de la contraseña pasada por parámetro
                encData_byte = System.Text.Encoding.UTF8.GetBytes(pass);
                ///Se guarda en la variable que devolverá la salida del método ToBase64String que es el que devolverá el string de entrada en base64
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en base64Encode" + ex.Message);
            }
        }

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
            ///Llamada a guardar los datos de la colección Usuarios en una variable global
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");
            ///Llamada al método PassParser para obtener la contraseña en base64
            string passParser = HashPasword(pass);
            ///Creación del objeto de tipo Usuario que se guardará
            Usuario user = new Usuario { Nombre = nombre, Apellidos = ape, Email = email, Pass = passParser, Rol = rol };
            ///Inserción del usuario en la colección
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
            ///Llamada a guardar los datos de la colección Usuarios en una variable global
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");
            ///Llamada al método PassParser para obtener la contraseña en base64
            string passParser = HashPasword(pass);
            ///Filtro para buscar usuarios con los requisitos pasados por parámetro
            FilterDefinition<Usuario> filtro = Builders<Usuario>.Filter.And(Builders<Usuario>.Filter.Eq(x => x.Email, email), Builders<Usuario>.Filter.Eq(x => x.Pass, passParser));
            ///Devuelve el primer objeto de tipo Usuario que cumple los requisitos
            return collectionUsuario.Find(filtro).First<Usuario>();
        }

        /// <summary>
        /// Método para obtener un usuario un usuario 
        /// </summary>
        /// <param name="id">identificador del usuario</param>
        /// <returns>primer usuario que encuentra con ese identificador</returns>
        public Usuario GetUser(ObjectId id)
        {
            ///Llamada a guardar los datos de la colección Usuarios en una variable global
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");
            ///Filtro para buscar el usuario con el identificador pasado por parámetro
            FilterDefinition<Usuario> filtro = Builders<Usuario>.Filter.And(Builders<Usuario>.Filter.Eq(x => x.Id, id.ToString()));
            ///Devuelve el primer objeto de tipo Usuario que cumple el requisito
            return collectionUsuario.Find(filtro).First<Usuario>();
        }

        /// <summary>
        /// Método para obtener la lista de usuarios que no tienen un determinado rol 
        /// </summary>
        /// <param name="rol">rol del usuario</param>
        /// <returns>lista de usuarios que no tienen el rol pasado por parámetro</returns>
        public List<Usuario> ListUsuarios(int rol)
        {
            ///Llamada a guardar los datos de la colección Usuarios en una variable global
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");
            ///Filtro para buscar los usuarios que no tengan el rol pasado por parámetro
            FilterDefinition<Usuario> filtro = Builders<Usuario>.Filter.Ne(x => x.Rol, rol);
            ///Devuelve la lista de objetos de tipo Usuario que cumplen con el filtro
            return collectionUsuario.Find(filtro).ToList();
        }

        /// <summary>
        /// Método que devuelve la lista de veterinarios
        /// </summary>
        /// <param name="rol">rol de veterinarios</param>
        /// <returns>lista de usuarios que tienen el rol de veterinarios</returns>
        public List<Usuario> ListVet(int rol)
        {
            ///Llamada a guardar los datos de la colección Usuarios en una variable global
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");
            ///Filtro para buscar a los usuarios que tengan un determinado rol
            FilterDefinition<Usuario> filtro = Builders<Usuario>.Filter.Eq(x => x.Rol, rol);
            ///Devuelve la lista de objetos de tipo Usuario que cumplen con el filtro
            return collectionUsuario.Find(filtro).ToList();
        }

        /// <summary>
        /// Método para actualizar la contraseña del usuario
        /// </summary>
        /// <param name="id">identificador del usuario a actualizar</param>
        /// <param name="newpass">contraseña nueva</param>
        public void UpdateUser(ObjectId id, string newpass)
        {
            ///Llamada a guardar los datos de la colección Usuarios en una variable global
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");
            ///Filtro para buscar al usuario
            var filter = Builders<Usuario>.Filter.Eq(x => x.Id, id.ToString());
            ///Llamada al método PassParser para codificar la contraseña
            string pass = HashPasword(newpass);
            ///Definición de la actualización del campo
            var update = Builders<Usuario>.Update.Set(x => x.Pass, pass);
            ///Actualización del usuario pasando por parámetro al método el filtro y la definición de la actualización
            collectionUsuario.UpdateOne(filter, update);
        }

        /// <summary>
        /// Método para borrar un usuario
        /// </summary>
        /// <param name="id">identificador del usuario a borrar</param>
        public void RemoveUser(string id)
        {
            ///Llamada a guardar los datos de la colección Usuarios en una variable global
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");
            ///Filtro para buscar al usuario que tiene el identificador pasado por parámetro
            FilterDefinition<Usuario> filtro = Builders<Usuario>.Filter.Eq(x => x.Id, id);
            ///Eliminación del usuario que cumpla el filtro
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
            ///Llamada a guardar los datos de la colección Mascotas en una variable global
            collectionMascota = db.GetCollection<Mascota>("Mascotas");
            ///Creación de un objeto tipo Mascota que se guardará
            Mascota pet = new Mascota { Nombre = nombre, Tipo = tipo, Raza = raza, Sexo = sexo, Peso = peso, Vacunas = vacunas, Imagen = imagen, Id_Cliente = id_cl };
            ///Inserción del objeto de tipo Mascota en la colección
            collectionMascota.InsertOne(pet);
        }

        /// <summary>
        /// Método para obtener una mascota
        /// </summary>
        /// <param name="id">identificador de la mascota</param>
        /// <returns>primera mascota que encuentre con ese identificador</returns>
        public Mascota GetPet(ObjectId id)
        {
            ///Llamada a guardar los datos de la colección Mascotas en una variable global
            collectionMascota = db.GetCollection<Mascota>("Mascotas");
            ///Filtro para buscar por identificador
            FilterDefinition<Mascota> filtro = Builders<Mascota>.Filter.Eq(x => x.Id, id.ToString());
            ///Devuelve el primer objeto de tipo Mascota que cumple el requisito
            return collectionMascota.Find(filtro).First<Mascota>();
        }

        /// <summary>
        /// Método para borrar una mascota
        /// </summary>
        /// <param name="id">identificador de la mascota a borrar</param>
        public void RemovePet(string id)
        {
            ///Llamada a guardar los datos de la colección Mascotas en una variable global
            collectionMascota = db.GetCollection<Mascota>("Mascotas");
            ///Filtro para buscar el objeto de tipo Mascota con identificador el pasado por parámetro
            FilterDefinition<Mascota> filtro = Builders<Mascota>.Filter.Eq(x => x.Id, id);
            ///Eliminación de la mascota que cumpla con el filtro
            collectionMascota.DeleteOne(filtro);
        }

        /// <summary>
        /// Método para listar las mascotas del mismo dueño
        /// </summary>
        /// <param name="id">identificador del dueño de la mascota</param>
        /// <returns>lista de mascotas pertenecientes al dueño que tiene como identificador el id</returns>
        public List<Mascota> GetPets(ObjectId id)
        {
            ///Llamada a guardar los datos de la colección Mascotas en una variable global
            collectionMascota = db.GetCollection<Mascota>("Mascotas");
            ///Filtro para buscar las mascotas que tienen como dueño el usuario con identificador pasado por parámetro
            FilterDefinition<Mascota> filtro = Builders<Mascota>.Filter.Eq(x => x.Id_Cliente, id);
            ///Devuelve la lista de objetod Mascota filtrada
            return collectionMascota.Find(filtro).ToList();
        }

        /// <summary>
        /// Método para mostrar todas las mascotas
        /// </summary>
        /// <returns>lista de todas las mascotas de la colección Mascotas</returns>
        public List<Mascota> GetPetsWithoutId()
        {
            ///Llamada a guardar los datos de la colección Mascotas en una variable global
            collectionMascota = db.GetCollection<Mascota>("Mascotas");
            ///Devuelve la lista de todas las mascotas
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
            ///Llamada a guardar los datos de la colección Citas en una variable global
            collectionCita = db.GetCollection<Cita>("Citas");
            ///Creación de un objeto de tipo Cita para luego guardarlo
            Cita date = new Cita { Id_cliente = id_cl, Id_mascota = id_mas, Fecha = fecha, Id_veterinario = id_vet };
            ///Inserción del objeto cita en la colección
            collectionCita.InsertOne(date);
        }

        /// <summary>
        /// Método para obtener las citas agendadas por un determinado usuario
        /// </summary>
        /// <param name="id">identificador del usuario</param>
        /// <returns>lista de las citas del usuario identificado por el parámetro</returns>
        public List<Cita> GetDates(ObjectId id)
        {
            ///Llamada a guardar los datos de la colección Citas en una variable global
            collectionCita = db.GetCollection<Cita>("Citas");
            ///Filtro para buscar las citas del usuario pasado por parámetro
            FilterDefinition<Cita> filtro = Builders<Cita>.Filter.Eq(x => x.Id_cliente, id);
            ///
            return collectionCita.Find(filtro).ToList();
        }

        public List<Cita> GetDatesVet(ObjectId id)
        {
            collectionCita = db.GetCollection<Cita>("Citas");
            FilterDefinition<Cita> filtro = Builders<Cita>.Filter.Eq(x => x.Id_veterinario, id);
            return collectionCita.Find(filtro).ToList();
        }

        public void AddAdjust(ObjectId id_user, string tema, string tam_letra, string idioma)
        {
            collectionAjuste = db.GetCollection<Ajuste>("Ajustes");
            Ajuste adjust = new Ajuste { Id = id_user, Tema = tema, Tam_letra = tam_letra, Idioma = idioma };
            collectionAjuste.InsertOne(adjust);
        }

        public Ajuste GetAdjust(ObjectId id)
        {
            collectionAjuste = db.GetCollection<Ajuste>("Ajustes");
            FilterDefinition<Ajuste> filtro = Builders<Ajuste>.Filter.Eq(x => x.Id, id);
            return collectionAjuste.Find(filtro).First<Ajuste>();
        }

        public void RemoveAdjust(ObjectId id)
        {
            collectionAjuste = db.GetCollection<Ajuste>("Ajustes");
            FilterDefinition<Ajuste> filtro = Builders<Ajuste>.Filter.Eq(x => x.Id, id);
            collectionAjuste.DeleteOne(filtro);
        }

        public void UpdateAdjust(ObjectId id, string tema, string tam_letra, string idioma)
        {
            collectionAjuste = db.GetCollection<Ajuste>("Ajustes");
            var filter = Builders<Ajuste>.Filter.Eq(x => x.Id, id);
            var update = Builders<Ajuste>.Update.Set(x => x.Tema, tema).Set(x => x.Tam_letra, tam_letra).Set(x => x.Idioma, idioma);
            collectionAjuste.UpdateOne(filter, update);
        }
    }
}
