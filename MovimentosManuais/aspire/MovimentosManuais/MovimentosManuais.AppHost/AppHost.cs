var builder = DistributedApplication.CreateBuilder(args);

var sql = builder
    .AddSqlServer("sqlserver", 
    password: builder.CreateResourceBuilder<ParameterResource>(
            new("sql-password", _ => "Candidato123@")))
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

sql.WithEndpoint("tcp", endpoint =>
{
    endpoint.Port = 1433;
    endpoint.TargetPort = 1433;
    endpoint.IsProxied = false;
});

var database = sql.AddDatabase("MovimentosManuaisDb");

var api = builder
    .AddProject<Projects.MovimentosManuais_Api>("movimentos-manuais-api")
    .WithReference(database)
    .WaitFor(database);

api.WithEndpoint("https", endpoint =>
{
    endpoint.Port = 7001;
    endpoint.TargetPort = 7001;
    endpoint.IsProxied = false;
});

builder
    .AddNpmApp(
        "movimentos-manuais-web",
        "../../../src/movimentos-manuais-web")
    .WithReference(api)
    .WaitFor(api)
    .WithHttpEndpoint(
        env: "PORT",
        port: 4200)
    .WithExternalHttpEndpoints();

builder.Build().Run();
