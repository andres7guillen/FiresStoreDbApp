using CSharpFunctionalExtensions;
using FireBaseDomain.DTO;
using FireBaseDomain.Entities;
using FireBaseDomain.Repositories;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBaseInfrastructure.Repositories;

public class FireStoreRepositoryUsu : IFireStoreRepositoryUsu
{
    private readonly FirestoreDb _firestore;

    public FireStoreRepositoryUsu(FirestoreDb firestore)
    {
        _firestore = firestore;
    }

    private const string _collectionName = "usuarios";

    public async Task<List<UsuarioDto>> GetAllUsuarioAsync()
    {
        QuerySnapshot snapshot = await _firestore.Collection(_collectionName).GetSnapshotAsync();
        var result = new List<UsuarioDto>();

        foreach (var doc in snapshot.Documents)
        {
            try
            {
                var usuarioEncontrado = doc.ConvertTo<usuario>();
                result.Add(new UsuarioDto
                {
                    Id = doc.Id,
                    apellido = usuarioEncontrado.apellido,
                    correo = usuarioEncontrado.correo,
                    direccion = usuarioEncontrado.direccion,
                    edad = usuarioEncontrado.edad,
                    genero = usuarioEncontrado.genero,
                    idUsuario = usuarioEncontrado.idUsuario,
                    jornada = usuarioEncontrado.jornada,
                    nombre = usuarioEncontrado.nombre,
                    semestre = usuarioEncontrado.semestre,
                    telefono = usuarioEncontrado.telefono,
                    universidad = usuarioEncontrado.universidad

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en documento {doc.Id}: {ex.Message}");
            }
        }

        return result;
    }

    public async Task<Maybe<UsuarioDto>> GetUsuarioByIdAsync(string id)
    {
        DocumentReference docRef = _firestore.Collection(_collectionName).Document(id);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            var usuario = snapshot.ConvertTo<usuario>();
            return new UsuarioDto
            {
                Id = snapshot.Id,
                apellido = usuario.apellido,
                correo = usuario.correo,
                direccion = usuario.direccion,
                edad = usuario.edad,
                genero = usuario.genero,
                idUsuario = usuario.idUsuario,
                jornada = usuario.jornada,
                nombre = usuario.nombre,
                semestre = usuario.semestre,
                telefono = usuario.telefono,
                universidad = usuario.universidad
            };
        }
        return Maybe.None;
    }



    public async Task<List<UsuarioDto>> GetUsuariosBySomeFieldFilterAsync(string fieldName, string fieldValue)
    {
        object valueToQuery=string.IsNullOrEmpty;
        switch (fieldName.ToLower()) 
        {
            
           
            case "edad" or "idUsuario" or "semestre":
                
                if (int.TryParse(fieldValue, out int intValue))
                {
                    valueToQuery = intValue;
                }
                
                break;
            case "telefono":
                if (long.TryParse(fieldValue, out long longValue))
                {
                    valueToQuery = longValue;
                }
               
                break;
            
            default:
                
                valueToQuery = fieldValue;
                break;
        }



        Query query = _firestore.Collection(_collectionName)
                                 .WhereEqualTo(fieldName, valueToQuery);

        QuerySnapshot snapshot = await query.GetSnapshotAsync();
        var result = new List<UsuarioDto>();
        foreach (var document in snapshot.Documents)
        {
            try
            {
                var usuarioEncontrado = document.ConvertTo<usuario>();
                result.Add(new UsuarioDto
                {
                    Id = document.Id,
                    apellido = usuarioEncontrado.apellido,
                    correo = usuarioEncontrado.correo,
                    direccion = usuarioEncontrado.direccion,
                    edad = usuarioEncontrado.edad,
                    genero = usuarioEncontrado.genero,
                    idUsuario = usuarioEncontrado.idUsuario,
                    jornada = usuarioEncontrado.jornada,
                    nombre = usuarioEncontrado.nombre,
                    semestre = usuarioEncontrado.semestre,
                    telefono = usuarioEncontrado.telefono,
                    universidad = usuarioEncontrado.universidad

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en documento {document.Id}: {ex.Message}");
            }
        }

        return result;


    }




    public async Task SaveUsuarioAsync(usuario usuario)
    {
        await _firestore
            .Collection(_collectionName).AddAsync(usuario);
    }

    public async Task UpdateUsuarioAsync(string id, usuario usuario)
    {
        DocumentReference docRef = _firestore.Collection(_collectionName).Document(id);
        await docRef.SetAsync(usuario, SetOptions.Overwrite);
    }

    public async Task DeleteUsuarioAsync(string id)
    {
        DocumentReference docRef = _firestore.Collection(_collectionName).Document(id);
        await docRef.DeleteAsync();
    }
}
