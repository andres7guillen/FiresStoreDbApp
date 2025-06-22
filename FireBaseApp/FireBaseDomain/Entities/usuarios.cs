using FireBaseDomain.Services;
using Google.Cloud.Firestore;

namespace FireBaseDomain.Entities;

[FirestoreData]
public class usuario
{
    [FirestoreProperty] 
    public string apellido { get; private set; }
    [FirestoreProperty] 
    public string correo { get; private set; }
    [FirestoreProperty] 
    public string direccion { get; private set; }
    [FirestoreProperty] 
    public int edad { get; private set; }
    [FirestoreProperty] 
    public string genero { get; private set; }
    [FirestoreProperty] 
    public int idUsuario { get; private set; }
    [FirestoreProperty] 
    public string jornada { get; private set; }
    [FirestoreProperty] 
    public string nombre { get; private set; }
    [FirestoreProperty] 
    public int semestre { get; private set; }
    [FirestoreProperty]
    public long telefono { get; private set; }
    [FirestoreProperty]
    public string universidad { get; private set; }

    private usuario() { }

    public static usuario Create(
        string withapellido, string withcorreo, string withdireccion, int withedad, string withgenero,
        int withidUsuario, string withjornada, string withnombre, int withsemestre, long withtelefono, string withuniversidad)
    {
        return new usuario
        {
            apellido = withapellido,
            correo = withcorreo,
            direccion = withdireccion,
            edad = withedad,
            genero = withgenero,
            idUsuario = withidUsuario,
            jornada = withjornada,
            nombre = withnombre,
            semestre = withsemestre,
            telefono = withtelefono,
            universidad = withuniversidad
        };
    }

    public async Task AddTo(IFireStoreServiceUsu firestoreService)
    {
        await firestoreService.SaveUsuarioAsync(this);
    }
}
