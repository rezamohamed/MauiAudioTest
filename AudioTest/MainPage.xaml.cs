using Plugin.AudioRecorder;
using Plugin.Maui.Audio;

namespace AudioTest;

public partial class MainPage : ContentPage
{
    private readonly IAudioManager _audioManager;
    private IAudioRecorder _audioRecorder;

    private readonly AudioRecorderService _audioRecorderService = new AudioRecorderService();
    
    public MainPage(IAudioManager audioManager)
    {
        _audioManager = audioManager;
        InitializeComponent();
    }
    

    private async void RecordBtn_OnClicked(object? sender, EventArgs e)
    {
        try
        {
            //_audioRecorder = _audioManager.CreateRecorder();

            if (await Permissions.RequestAsync<Permissions.Microphone>() == PermissionStatus.Granted)
            {
                
                // if (!_audioRecorder.IsRecording)
                // {
                    // var options = new AudioRecordingOptions
                    // {
                    //     //SampleRate = AudioRecordingOptions.DefaultSampleRate,
                    //     SampleRate = 16000,
                    //     //Channels = AudioRecordingOptions.DefaultChannels,
                    //     Channels = ChannelType.Stereo,
                    //     //BitDepth = AudioRecordingOptions.DefaultBitDepth,
                    //     BitDepth = BitDepth.Pcm16bit,
                    //     //Encoding = AudioRecordingOptions.DefaultEncoding,
                    //     Encoding = Encoding.Flac,
                    //     ThrowIfNotSupported = true
                    // };
                    //
                    //
                    // await _audioRecorder.StartAsync(options);
                    // //await audioRecorder.StartAsync();
                // }


                await _audioRecorderService.StartRecording();


            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    private async void StopBtn_OnClicked(object? sender, EventArgs e)
    {
        // var recordedAudio = await _audioRecorder.StopAsync();
        //
        // // basic player - doesn't work in Android
        // //var player = AudioManager.Current.CreatePlayer(recordedAudio.GetAudioStream());
        //
        // byte[] recordedBytes;
        //
        // using (var stream = new MemoryStream())
        // {
        //     await recordedAudio.GetAudioStream().CopyToAsync(stream);
        //     recordedBytes = stream.ToArray();
        // }
        //
        //
        // // this is for testing
        // MemoryStream test = new MemoryStream(recordedBytes);
        // var player = AudioManager.Current.CreatePlayer(test);
        // player.Play();
        // //testing end


        if (_audioRecorderService.IsRecording)
        {
            await _audioRecorderService.StopRecording();

            byte[] recordedBytes;
            
            using (var stream  = new MemoryStream())
            {
                await _audioRecorderService.GetAudioFileStream().CopyToAsync(stream);
                recordedBytes = stream.ToArray();
            }
            
            
            // this is for testing
            MemoryStream test = new MemoryStream(recordedBytes);
            var player = AudioManager.Current.CreatePlayer(test);
            player.Play();
            //testing end
            
        }
        
        
    }
}