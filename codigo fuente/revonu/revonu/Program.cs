using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace revonu
{
    class Program
    {
        static SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        static SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        static void Main(string[] args)
        {

            try
            {
                Choices commands = new Choices();
                String[] numbers = new String[100];

                for (int i = 1; i <= 100; i++)
                {
                    numbers[i - 1] = i.ToString();
                }

                commands.Add(numbers);
                GrammarBuilder gBuilder = new GrammarBuilder();
                gBuilder.Append(commands);
                Grammar grammar = new Grammar(gBuilder);

                recEngine.LoadGrammarAsync(grammar);
                recEngine.SetInputToDefaultAudioDevice();
                recEngine.RecognizeAsync(RecognizeMode.Multiple);
                recEngine.SpeechRecognized += recEngine_SpeechRecognized;

                synthesizer.SpeakAsync("Bienvenido al programa revonu");
                synthesizer.SpeakAsync("Por favor digame un numero del 1 al 100");

                Console.ReadLine();
            }catch(Exception ex){

                Console.WriteLine("No se encontro un dispositivo de voz. Por favor verifique ! \n" + ex.Message);
                Console.ReadLine();

            }
        }



        private static void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("\nTu numero es " + e.Result.Text);

            int n = 0;

            try
            {
                n = Convert.ToInt16(e.Result.Text);

                for (int i = 1; i <= n; i++)
                {
                    Console.WriteLine("\n" + i.ToString());
                }

            }
            catch (Exception ex)
            {
                synthesizer.SpeakAsync("Error inesperado! Intentalo nuevamente.");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\n-------------------------------------------------------------");

            synthesizer.SpeakAsync("Muchas gracias");
            synthesizer.SpeakAsync("Por favor digame otro numero del 1 al 100.");

        }

    }
}
