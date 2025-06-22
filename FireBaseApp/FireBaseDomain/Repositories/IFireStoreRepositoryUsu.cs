using CSharpFunctionalExtensions;
using FireBaseDomain.DTO;
using FireBaseDomain.Entities;
using Microsoft.AspNetCore.Http;

namespace FireBaseDomain.Repositories
{
    public interface IFireStoreRepositoryUsu 
    {
        Task SaveUsuarioAsync(usuario student);
        Task<List<UsuarioDto>> GetAllUsuarioAsync();
        Task<Maybe<UsuarioDto>> GetUsuarioByIdAsync(string id);
        Task<List<UsuarioDto>> GetUsuariosBySomeFieldFilterAsync(string fieldName, string fieldValue);
        Task UpdateUsuarioAsync(string id, usuario Usuario);
        Task DeleteUsuarioAsync(string id);
    }
}
