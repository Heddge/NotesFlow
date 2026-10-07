using Microsoft.Extensions.Logging;
using NotesFlow.Objects;
using NotesFlow.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace NotesFlow
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            string _dbpath = Path.Combine(FileSystem.AppDataDirectory, "notesflowdb.db");

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<NotesContainer>();
            builder.Services.AddSingleton<JsonParserManager>();
            builder.Services.AddSingleton<AppStorageManager>();
            builder.Services.AddSingleton<NoteDbManager>();
            builder.Services.AddDbContext<NoteDbContext>(options => options.UseSqlite("Data Source="+_dbpath));

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            var app =  builder.Build();

            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<NoteDbContext>();
            context.Database.EnsureCreated();


            return app;
        }
    }
}
