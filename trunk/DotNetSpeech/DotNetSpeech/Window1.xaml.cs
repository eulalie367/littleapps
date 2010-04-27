using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Speech.Recognition;
using System.Speech.Recognition.SrgsGrammar;
using System.IO;
using System.Xml;

namespace DotNetSpeech
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private SpeechRecognitionEngine appRecognizer;

        public Window1()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
        }
        private void IdentifyVoice(Stream audioSource)
        { 
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {

            appRecognizer = new SpeechRecognitionEngine();
            
            appRecognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(appRecognizer_SpeechRecognized);
            appRecognizer.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(appRecognizer_SpeechRecognitionRejected);
            appRecognizer.SpeechHypothesized += new EventHandler<SpeechHypothesizedEventArgs>(appRecognizer_SpeechHypothesized);

            appRecognizer.SetInputToWaveFile(textBox1.Text);
            
            //appRecognizer.LoadGrammar(LoadDynamicGrammar(new string[] {"Hello","Hello World", "World", "Buger", "Call", "Below"}));

            //Default Dictation Grammar
            DictationGrammar defaultDictationGrammar = new DictationGrammar();
            defaultDictationGrammar.Name = "Default Dictation";
            defaultDictationGrammar.Enabled = true;
            appRecognizer.LoadGrammar(defaultDictationGrammar);

            appRecognizer.Recognize();
        }

        void appRecognizer_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            MessageBox.Show(ConcatStringArray(e.Result.Words) + e.Result.Confidence.ToString());
        }

        void appRecognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            MessageBox.Show(ConcatStringArray(e.Result.Words) + "Recognized with " + e.Result.Confidence.ToString() + "Confidence");
        }

        void appRecognizer_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
//            MessageBox.Show(ConcatStringArray(e.Result.Homophones));
        }


        private Grammar LoadDynamicGrammar(string[] Words)
        {
            Choices HelloWorld = new Choices(Words);
            Grammar g = new Grammar(HelloWorld.ToGrammarBuilder());
            return g;
        }

        private string ConcatStringArray(System.Collections.ObjectModel.ReadOnlyCollection<RecognizedWordUnit> stringArray)
        {
            string concatStringArray = "";
            foreach (RecognizedWordUnit w in stringArray)
            {
                concatStringArray += w.Text + " ";
            }
            concatStringArray = concatStringArray.Remove(concatStringArray.Length-1, 1) + ".";
            return concatStringArray;
        }
        private string ConcatStringArray(System.Collections.ObjectModel.ReadOnlyCollection<RecognizedPhrase> stringArray)
        {
            string concatStringArray = "";
            foreach (RecognizedPhrase w in stringArray)
            {
                concatStringArray += w.Text + " ";
            }
            concatStringArray = concatStringArray.Remove(concatStringArray.Length - 1, 1) + ".";
            return concatStringArray;
        }

    }
}
