using CSharpFunctionalExtensions;
using FireBaseDomain.DTO;
using FireBaseDomain.Entities;
using Microsoft.AspNetCore.Http;

namespace FireBaseDomain.Services;

public interface IFireStoreServiceUsu
{
    Task ImportUsuarioFromCsvAsync(IFormFile file);
    Task SaveUsuarioAsync(usuario Usuario);
    Task<List<UsuarioDto>> GetAllUsuariosAsync();
    Task<Maybe<UsuarioDto>> GetUsuarioByIdAsync(string id);
    Task<List<UsuarioDto>> GetUsuariosBySomeFieldFilterAsync(string fieldName, string fieldValue);
    Task UpdateUsuarioAsync(string id, usuario Usuario); 
    Task DeleteUsuarioAsync(string id);
    
}
