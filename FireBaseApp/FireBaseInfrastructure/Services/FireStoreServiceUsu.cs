using CSharpFunctionalExtensions;
using FireBaseDomain.DTO;
using FireBaseDomain.Entities;
using FireBaseDomain.Repositories;
using FireBaseDomain.Services;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace FireBaseInfrastructure.Services;

public class FireStoreServiceUsu : IFireStoreServiceUsu
{
    private readonly IFireStoreRepositoryUsu _repository;
    public FireStoreServiceUsu(IConfiguration configuration, FirestoreDb firestore, IFireStoreRepositoryUsu repository)
    {
        _repository = repository;
    }

    public async Task ImportUsuarioFromCsvAsync(IFormFile file)
    {
        using var stream = new StreamReader(file.OpenReadStream(), Encoding.UTF8);
        bool isFirstLine = true;

        while (!stream.EndOfStream)
        {
            var line = await stream.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(line)) continue;

            if (isFirstLine)
            {
                isFirstLine = false;
                continue;
            }

            var values = line.Split(',');

            if (values.Length != 11)
                throw new InvalidOperationException("El archivo CSV tiene un formato incorrecto. Se esperaban 11 columnas.");

            var apellido = values[0].Trim();
            var correo = values[1].Trim();
            var direccion = values[2].Trim();
            var edad = int.TryParse(values[3], out int parseEdad) ? parseEdad : 0;
            var genero = values[4].Trim();
            var idUsuario = int.TryParse(values[5], out int parseIdUsuario) ? parseIdUsuario : 0;
            var jornada = values[6].Trim();
            var nombre = values[7].Trim();
            var semestre = int.TryParse(values[8], out int parseSemestre) ? parseSemestre : 0;
            var telefono = long.TryParse(values[9], out long parseTelefono) ? parseSemestre : 0;
            var universidad = values[10].Trim();

            var usuarioDato = usuario.Create(

                withapellido: apellido,
                withcorreo: correo,
                withdireccion: direccion,
                withedad: edad,
                withgenero: genero,
                withidUsuario: idUsuario,
                withjornada: jornada,
                withnombre: nombre,
                withsemestre: semestre,
                withtelefono: telefono,
                withuniversidad: universidad

            );

            await SaveUsuarioAsync(usuarioDato);
        }
    }


    public async Task SaveUsuarioAsync(usuario usuarioEntrada)
    {
        await _repository.SaveUsuarioAsync(usuarioEntrada);
    }

    public async Task<List<UsuarioDto>> GetAllUsuariosAsync()
    {
        return await _repository.GetAllUsuarioAsync();
    }

    public async Task<Maybe<UsuarioDto>> GetUsuarioByIdAsync(string id)
    {
        return await _repository.GetUsuarioByIdAsync(id);
    }

    public async Task<List<UsuarioDto>> GetUsuariosBySomeFieldFilterAsync(string fieldName, string fieldValue)
    {
        return await _repository.GetUsuariosBySomeFieldFilterAsync(fieldName, fieldValue);
    }
    

    public async Task UpdateUsuarioAsync(string id, usuario UsuarioEntrada)
    {
        await _repository.UpdateUsuarioAsync(id, UsuarioEntrada);
    }

    public async Task DeleteUsuarioAsync(string id)
    {
        await _repository.DeleteUsuarioAsync(id);
    }
}
