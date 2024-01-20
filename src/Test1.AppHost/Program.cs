var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.Test1_ApiService>("apiservice");

builder.AddProject<Projects.Test1_Web>("webfrontend")
    .WithReference(apiService);

builder.Build().Run();
