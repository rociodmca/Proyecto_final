using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final.Modelo
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
            cliente = new MongoClient("mongodb://root:example@192.168.1.40:27017/");
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

        public void AddUser(string nombre, string ape, string email, string pass)
        {
            collectionUsuario = db.GetCollection<Usuario>("Usuarios");
            string passParser = PassParser(pass);
            Usuario user = new Usuario { Nombre = nombre, Apellidos = ape, Email = email, Pass = passParser, Rol = 1 };
            collectionUsuario.InsertOne(user);
        }

        public void AddPet(string nombre, string tipo, string raza, string sexo, int peso, string vacunas, string id_cl)
        {
            collectionMascota = db.GetCollection<Mascota>("Mascotas");
            Mascota pet = new Mascota { Nombre = nombre, Tipo = tipo, Raza = raza, Sexo = sexo, Peso = peso, Vacunas = vacunas, Id_Cliente = id_cl };
            collectionMascota.InsertOne(pet);
        }

        public void AddDate(string id_cl, string id_mas, string fecha, string id_vet)
        {
            collectionCita = db.GetCollection<Cita>("Citas");
            Cita date = new Cita { Id_cliente = id_cl, Id_mascota = id_mas, Fecha = fecha, Id_veterinario = id_vet };
            collectionCita.InsertOne(date);
        }

        public void AddAdjust(string id_user, string tema, string tam_letra, string idioma)
        {
            collectionAjuste = db.GetCollection<Ajuste>("Ajustes");
            Ajuste adjust = new Ajuste { Id = id_user, Tema = tema, Tam_letra = tam_letra, Idioma = idioma };
            collectionAjuste.InsertOne(adjust);
        }
    }
}
