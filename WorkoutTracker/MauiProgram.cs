using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using WorkoutTracker.Data;
using WorkoutTracker.Data.Abstractions;
using WorkoutTracker.Repositories.Abstractions;
using WorkoutTracker.Repositories;
using WorkoutTracker.ViewModels;
using WorkoutTracker.Views;

namespace WorkoutTracker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IDbService, DbService>();

            builder.Services.AddSingleton<IExerciseRepository, ExerciseRepository>();
            builder.Services.AddSingleton<IMuscleRepository, MuscleRepository>();

            builder.Services.AddTransient<ExerciseViewModel>();
            builder.Services.AddTransient<ExercisesListPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
