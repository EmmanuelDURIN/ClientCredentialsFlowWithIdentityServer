using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// ED
// 1) l'appel des APi sécurisées par [Authorize] utilise la vérification du token fournie par le client'appelant
// 2) Le Token est vérifié auprès de l'autorité = Identity Provider
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
      options.Authority = "https://localhost:5001";

      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateAudience = true
      };
    });
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
// ED dds the authentication middleware to the pipeline
// so authentication will be performed automatically on every call into the host.
app.UseAuthentication();
// ED UseAuthorization adds the authorization middleware to make sure,
// our API endpoint cannot be accessed by anonymous clients. 
app.UseAuthorization();

app.MapControllers();

app.Run();
