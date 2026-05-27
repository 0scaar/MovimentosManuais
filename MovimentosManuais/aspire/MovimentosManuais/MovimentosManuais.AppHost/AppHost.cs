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

builder
    .AddProject<Projects.MovimentosManuais_Api>("movimentos-manuais-api")
    .WithReference(database)
    .WaitFor(database);

builder.Build().Run();
