using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;

namespace AudioTest;

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
            })
            .AddAudio(
                playbackOptions =>
                {
#if IOS || MACCATALYST
                    playbackOptions.Category = AVFoundation.AVAudioSessionCategory.Playback;
#endif
                },
                recordingOptions =>
                {
#if IOS || MACCATALYST
                    recordingOptions.Category = AVFoundation.AVAudioSessionCategory.Record;
                    recordingOptions.Mode = AVFoundation.AVAudioSessionMode.Default;
                    recordingOptions.CategoryOptions = AVFoundation.AVAudioSessionCategoryOptions.MixWithOthers;
#endif
                });


        builder.Services.AddTransient<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}