using FireBaseDomain.Services;
using FireBaseInfrastructure.Services;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Net.Client;
using Grpc.Auth;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var projectId = config["Firestore:ProjectId"];
var credentialsPath = config["Firestore:CredentialsPath"];

GoogleCredential googleCredential = GoogleCredential
    .FromFile(credentialsPath)
    .CreateScoped("https://www.googleapis.com/auth/cloud-platform");

var firestoreClientBuilder = new FirestoreClientBuilder
{
    Credential = googleCredential
};

FirestoreClient firestoreClient = firestoreClientBuilder.Build();
FirestoreDb firestoreDb = FirestoreDb.Create(projectId, firestoreClient);

// Add services to the container.

builder.Services.AddSingleton(firestoreDb);
builder.Services.AddScoped<IFireStoreService, FirestoreService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
