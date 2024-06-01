using Microsoft.Maui.ApplicationModel.Communication;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Proyecto_final.Model
{
    public class AccesoBBDD
    {
        MongoClient cliente;
        IMongoDatabase db;
        IMongoCollection<Usuario> collectionUsuario;
        IMongoCollection<Mascota> collectionMascota;
        IMongoCollection<Cita> collectionCita;
        IMongoCollection<Ajuste> collectionAjuste;

        public AccesoBBDD()
        {
            //cliente = new MongoClient("mongodb://root:example@192.168.1.36:27017/");
            
            //cliente = new MongoClient("mongodb://root:example@localhost:27017/");
            db = cliente.GetDatabase("prueba_app");
        }

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

        public void AddUser(string nombre, string ape, string email, string pass, int rol)
        {
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");
            string passParser = PassParser(pass);
            Usuario user = new Usuario { Nombre = nombre, Apellidos = ape, Email = email, Pass = passParser, Rol = rol };
            collectionUsuario.InsertOne(user);
        }

        public Usuario GetUser(string email, string pass)
        {
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");
            string passParser = PassParser(pass);
            FilterDefinition<Usuario> filtro = Builders<Usuario>.Filter.And(Builders<Usuario>.Filter.Eq(x => x.Email, email), Builders<Usuario>.Filter.Eq(x => x.Pass, passParser));
            return collectionUsuario.Find(filtro).First<Usuario>();
        }

        public Usuario GetUser(ObjectId id)
        {
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");
            FilterDefinition<Usuario> filtro = Builders<Usuario>.Filter.And(Builders<Usuario>.Filter.Eq(x => x.Id, id.ToString()));
            return collectionUsuario.Find(filtro).First<Usuario>();
        }

        public List<Usuario> ListUsuarios(int rol)
        {
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");
            FilterDefinition<Usuario> filtro = Builders<Usuario>.Filter.Ne(x => x.Rol, rol);
            return collectionUsuario.Find(filtro).ToList();
        }

        public List<Usuario> ListVet(int rol)
        {
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");
            FilterDefinition<Usuario> filtro = Builders<Usuario>.Filter.Eq(x => x.Rol, rol);
            return collectionUsuario.Find(filtro).ToList();
        }

        public void UpdateUser(ObjectId id, string newpass)
        {
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");
            var filter = Builders<Usuario>.Filter.Eq(x => x.Id, id.ToString());
            string pass = PassParser(newpass);
            var update = Builders<Usuario>.Update.Set(x => x.Pass, pass);
            collectionUsuario.UpdateOne(filter, update);
        }

        public void RemoveUser(string id)
        {
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");
            FilterDefinition<Usuario> filtro = Builders<Usuario>.Filter.Eq(x => x.Id, id);
            collectionUsuario.DeleteOne(filtro);
        }

        public void AddPet(string nombre, string tipo, string raza, string sexo, int peso, string vacunas, Uri imagen, ObjectId id_cl)
        {
            collectionMascota = db.GetCollection<Mascota>("Mascotas");
            Mascota pet = new Mascota { Nombre = nombre, Tipo = tipo, Raza = raza, Sexo = sexo, Peso = peso, Vacunas = vacunas, Imagen = imagen, Id_Cliente = id_cl };
            collectionMascota.InsertOne(pet);
        }

        public Mascota GetPet(ObjectId id)
        {
            collectionMascota = db.GetCollection<Mascota>("Mascotas");
            FilterDefinition<Mascota> filtro = Builders<Mascota>.Filter.Eq(x => x.Id, id.ToString());
            return collectionMascota.Find(filtro).First<Mascota>();
        }

        public void RemovePet(string id)
        {
            collectionMascota = db.GetCollection<Mascota>("Mascotas");
            FilterDefinition<Mascota> filtro = Builders<Mascota>.Filter.Eq(x => x.Id, id);
            collectionMascota.DeleteOne(filtro);
        }

        public List<Mascota> GetPets(ObjectId id)
        {
            collectionMascota = db.GetCollection<Mascota>("Mascotas");
            FilterDefinition<Mascota> filtro = Builders<Mascota>.Filter.Eq(x => x.Id_Cliente, id);
            return collectionMascota.Find(filtro).ToList();
        }

        public List<Mascota> GetPetsWithoutId()
        {
            collectionMascota = db.GetCollection<Mascota>("Mascotas");
            return collectionMascota.AsQueryable<Mascota>().ToList();
        }

        public void AddDate(ObjectId id_cl, ObjectId id_mas, DateTime fecha, ObjectId id_vet)
        {
            collectionCita = db.GetCollection<Cita>("Citas");
            Cita date = new Cita { Id_cliente = id_cl, Id_mascota = id_mas, Fecha = fecha, Id_veterinario = id_vet };
            collectionCita.InsertOne(date);
        }

        public List<Cita> GetDates(ObjectId id)
        {
            collectionCita = db.GetCollection<Cita>("Citas");
            FilterDefinition<Cita> filtro = Builders<Cita>.Filter.Eq(x => x.Id_cliente, id);
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

        public Dictionary<string, Object> GetDatesList(string id)
        {
            /*collectionCita = db.GetCollection<Cita>("Citas");
            collectionMascota = db.GetCollection<Mascota>("Mascotas");
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");

            var result = db.Aggregate().Lookup<Mascota, Cita, dynamic>(collectionCita,mascota => mascota.Id,cita => cita.Id_mascota).Unwind<dynamic>("Citas").Lookup<dynamic, Usuario, dynamic>(collectionUsuario,cita => cita.collectionCita.Id_cliente,usuario => usuario.Id).Unwind<dynamic>("Usuarios");
            return result.ToList<Object>();*/

            var obj_id = new ObjectId(id);

            var coleccion1 = db.GetCollection<BsonDocument>("Usuarios");

            var match = new BsonDocument
            {
                {
                    "$match",
                    new BsonDocument
                    {
                        {"_id", obj_id}
                    }
                }
            };

            var lookup1 = new BsonDocument
            {
                {
                    "$lookup",
                    new BsonDocument
                    {
                        {"from", "Citas"},
                        {"localField", "_id"},
                        {"foreignField", "Id_cliente"},
                        {"as", "Cita"}
                    }
                }
            };

            var lookup2 = new BsonDocument
            {
                {
                    "$lookup",
                    new BsonDocument
                    {
                        {"from", "Mascotas"},
                        {"localField", "_id"},
                        {"foreignField", "Id_Cliente"},
                        {"as", "Mascota"}
                    }
                }
            };

            var pipeline = new[] { match, lookup1, lookup2 };
            var result = coleccion1.Aggregate<BsonDocument>(pipeline);
            Dictionary<string, Object> dicc = new Dictionary<string, Object>();
            List<Object> listaObjetos = new List<Object>();
            try
            {
                while (result.MoveNext())
                {
                    foreach (var item in result.Current)
                    {
                        dicc[item.Names.ToString()] = item.Values;
                    }
                }
            } catch (Exception e) { }

            //return listaObjetos;
            return dicc;
        }
    }
}
