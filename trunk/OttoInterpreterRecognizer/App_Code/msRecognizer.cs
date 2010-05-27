using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Speech.Recognition;

/// <summary>
/// Summary description for msRecognizer
/// </summary>
public class msRecognizer
{
    private HttpApplicationState Application = HttpContext.Current.Application;
    private HttpServerUtility Server = HttpContext.Current.Server;
    private SpeechRecognitionEngine appRecognizer;
    private bool finished = false;
    public string retVal{get;set;}
    public Grammar DefaultGrammar { get { return (Grammar)Application["tvGrammar"]; } }

    public msRecognizer()
	{
        retVal = "";
	}
    public string InitializeRecord()
    {
        //start a blank audio file and send back the id
        return "1.mp3";
    }
    public void AddToAudioFile(string CachedAudioFileId)
    {
    }
    public void Recognize(object CachedAudioFileId)
    {
        //Application.Remove("tvGrammar");
        appRecognizer = new SpeechRecognitionEngine();

        appRecognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(appRecognizer_SpeechRecognized);
        appRecognizer.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(appRecognizer_SpeechRecognitionRejected);
        //appRecognizer.RecognizeCompleted += new EventHandler<RecognizeCompletedEventArgs>(appRecognizer_RecognizeCompleted);
        //appRecognizer.SpeechHypothesized += new EventHandler<SpeechHypothesizedEventArgs>(appRecognizer_SpeechHypothesized);
        if (!DefaultGrammar.Loaded)
            appRecognizer.LoadGrammar(DefaultGrammar);
        
        //Default Dictation Grammar
        //Removed, unneeded since the Default Grammar is set to a dictation
        //DictationGrammar defaultDictationGrammar = new DictationGrammar();
        //defaultDictationGrammar.Name = "Default Dictation";
        //defaultDictationGrammar.Enabled = true;
        //appRecognizer.LoadGrammar(defaultDictationGrammar);


        appRecognizer.SetInputToWaveFile(Server.MapPath("/Voice/ExcessiveExposure.wav"));
        appRecognizer.AudioSignalProblemOccurred += new EventHandler<AudioSignalProblemOccurredEventArgs>(appRecognizer_AudioSignalProblemOccurred);
        //appRecognizer.RequestRecognizerUpdate 

        appRecognizer.Recognize();
        //wait for events to hit
        while (!finished)
        {
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(250));
        }
    }

    void appRecognizer_AudioSignalProblemOccurred(object sender, AudioSignalProblemOccurredEventArgs e)
    {
        retVal += e.AudioSignalProblem.ToString() + ":;";
    }
    private void appRecognizer_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
    {
        retVal += "Unrecognized:;";
        finished = true;
    }
    private void appRecognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
    {

        retVal += e.Result.Text + "\n\n Confidence=" + (e.Result.Confidence * 100).ToString() + ":;";
        finished = true;
    }

    //private void appRecognizer_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
    //{
    //}
}
