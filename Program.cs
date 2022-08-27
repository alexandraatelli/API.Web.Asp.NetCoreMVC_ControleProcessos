using AppControleJuridico.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Adiciona serviços ao contêiner
/// Guarda a connection string do arquivo appSettings.json
/// </summary>
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

///<summary>
/// Adiciona suporte ao acesso ao DB do Identity via EF
/// </summary>
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

///<summary>
/// Adiciona a tela de erro de dados (para desenvolvimento)
/// </summary>
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

///<summary>
/// Adiciona o Identity
/// </summary>
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

///<summary>
/// Adicionando o MVC - passado parâmetros para validação de valores monetários
/// </summary>
builder.Services.AddControllersWithViews(o =>
{
    o.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "O valor preenchido é inválido para este campo.");
    o.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => "Este campo precisa ser preenchido.");
    o.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Este campo precisa ser preenchido.");
    o.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "É necessário que o body na requisição não esteja vazio.");
    o.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor((x) => "O valor preenchido é inválido para este campo.");
    o.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "O valor preenchido é inválido para este campo.");
    o.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "O campo deve ser numérico.");
    o.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) => "O valor preenchido é inválido para este campo.");
    o.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) => "O valor preenchido é inválido para este campo.");
    o.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => "O campo deve ser numérico.");
    o.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => "Este campo deve ser preenchido.");
});

///<summary>
/// Gera o App
/// </summary>
var app = builder.Build();

///<summary>
/// Configuração conforme os ambientes
/// </summary>
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // O valor HSTS padrão é 30 dias. Pode-se optar por mudar para cenários de produção, veja: https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

var defaultCulture = new CultureInfo("pt-BR");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(defaultCulture),
    SupportedCultures = new List<CultureInfo> { defaultCulture},
    SupportedUICultures = new List<CultureInfo> { defaultCulture}
};
app.UseRequestLocalization(localizationOptions);

///<summary>
/// Rota padrão
/// </summary>
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

///<summary>
/// Mapeando componentes Razor Pages (ex: Identity)
/// </summary>
app.MapRazorPages();

app.Run();
