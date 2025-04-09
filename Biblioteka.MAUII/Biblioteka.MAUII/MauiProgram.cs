using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Biblioteka.MAUII
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddHttpClient<CategoryService>(client =>
            {
                client.BaseAddress = new Uri("https://127.0.0.1:7292");
            });

            builder.Services.AddDbContext<LibraryDbContext>(options =>
          options.UseMySql("Server=localhost;Database=biblioteka;User=root;Password=",
              new MySqlServerVersion(new Version(8, 0, 23))));

            builder.Services.AddSingleton<DatabaseService>();


            builder.Services.AddSingleton<CategoryViewModel>();
            builder.Services.AddSingleton<BookViewModel>();
            builder.Services.AddSingleton<BorrowViewModel>();
            builder.Services.AddSingleton<MemberViewModel>();

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<MainPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
