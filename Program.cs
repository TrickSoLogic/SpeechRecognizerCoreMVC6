using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

namespace SpeechRecognizerCoreMVC6
{
    class Program
    {
        static void Main(string[] args)
        {
            RecognizeSpeechAsync().Wait();
            Console.WriteLine("Good Work Coder! Please press a key to exit the console and continue. Regards - Black-Coder");
            Console.ReadLine();
        }

        public static async Task RecognizeSpeechAsync()
        {
            //Get your-subscription-key-here : https://azure.microsoft.com/en-us/try/cognitive-services/my-apis/
            var config = SpeechConfig.FromSubscription("your-subscription-key-here", "westus");

            // Creates a speech recognizer.
            using (var recognizer = new SpeechRecognizer(config))
            {
                Console.WriteLine("Say something...");
                var result = await recognizer.RecognizeOnceAsync();

                switch (result.Reason)
                {
                    // Checks result.
                    case ResultReason.RecognizedSpeech:
                        Console.WriteLine($"We recognized: {result.Text}");
                        break;
                    case ResultReason.NoMatch:
                        Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                        break;
                    case ResultReason.Canceled:
                    {
                        var cancellation = CancellationDetails.FromResult(result);
                        Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                        if (cancellation.Reason == CancellationReason.Error)
                        {
                            Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                            Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                            Console.WriteLine($"CANCELED: Did you update the subscription info?");
                        }

                        break;
                    }
                }
            }
        }

    }
}
