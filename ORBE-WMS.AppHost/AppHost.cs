var builder = DistributedApplication.CreateBuilder(args);

var sqlPassword = builder.Configuration["Parameters:sqlPassword"];
var sqlPasswordParam = builder.AddParameter("sqlPassword", secret: true);

var sql = builder.AddSqlServer("sqlserver", password: sqlPasswordParam)
                 .WithDataVolume() // Persiste os dados em um volume
                 .WithHostPort(1433); // Mapeia a porta padrão do SQL Server

var orbeDb = sql.AddDatabase("orbeDb");

var ollama = builder
            .AddOllama("ollama", 11434)
            .WithDataVolume() // Persiste os dados em um volume
            .WithLifetime(ContainerLifetime.Persistent) // Mantém o container ativo mesmo sem conexões
            .WithOpenWebUI(); // Habilita a interface web para gerenciamento

var llama = ollama.AddModel("llama3.2"); // Adiciona o modelo Llama 3.2


//--------------------------------------------------------

//Projects

var wms = builder
                .AddProject<Projects.ORBE_WMS_API>("wms")
                .WithReference(llama)
                .WaitFor(llama);

builder.AddProject<Projects.ORBE_WMS_WebApp>("webapp")
        .WithReference(orbeDb)
        .WithReference(wms)
        .WithReference(llama)
        .WaitFor(orbeDb)
        .WaitFor(wms)
        .WaitFor(llama);


builder.Build().Run();
