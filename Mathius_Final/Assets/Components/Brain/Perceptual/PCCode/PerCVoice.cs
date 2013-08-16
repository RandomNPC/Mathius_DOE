/*
 * Mathius: Defender of Earth
 * Attach this script to the Brain object in the Initialize Brain Scene
 * To use this interface correctly, all scripts must make a variable of
 * this script; then use the public functions to query information.
 * YOUCAN HAVE MULTIPLE COPIES OF THIS SCRIPT BECAUSE OF IT'S MULTITHREADED
 * NATURE! HOWEVER DON'T GO CRAZY WITH IT BECAUSE LAUNCHING THREADS HAS OVERHEAD!
 * 
 * This code was written by Michial Green II (VMichial)
 * This code is self contained, meaning it can return
 * information relevant to finding voice commands said by
 * the player. This is an interface layer for the SDK I made,
 * use the public functions if you want to ask for information
 */

using UnityEngine;
using System.Collections;
using System.Threading;

/*
 * The Voice code will be relatively simple. Start will initialize
 * the pipeline, along with launching the second thread. the second
 * thread will constantly be looping, looking for any recognized voice
 * commands. if it gets a hit, i set the correct bools to true.
 * I will submit debug info as errors not logs if the problem is system 
 * breaking.
 */

//

public class PerCVoice : MonoBehaviour {

	/*
	 * These variables hold the relevant data needed for the pipeline
	 * ALL INFORMATION IS PRIVATE, ANYTHING YOU NEED TO KNOW IS GAINED
	 * BY USINGTHING THE PUBLIC FUNCTIONS!
	 * Volume being the only exception for testing purposes in the inspector
	 * or the ability to change it at will via some option in game
	 */
	private PXCUPipeline						myPipe = null;		//handle for the pipeline
	private PXCUPipeline.Mode					myMode = PXCUPipeline.Mode.VOICE_RECOGNITION;
	private PXCMVoiceRecognition.Recognition	voice;				//struct for holding recognition data
	private bool								voiceHeard=false;	//true if a voice command was heard.
	
	//volume values range from zero to one as a float. Zero is muted. 1 is the loudest.
	//if you have the volume close to zero, only quiet voices are recognized in a query.
	//if you have the volume near 1, only loud voices are recognized in a query
	//use the setVolume function to change it. just give it a float.
	//0.1 is not practical, you are whispering.
	//0.5 appears to be normal speech
	//1.0 is for someone who needs to be loud
	private float[]								volume = new float[1]{0.5f};
	private System.Threading.Thread				myThread=null;		//handle for thread
												//This array is used to set voice commands into the pipeline
	private string[]							commands = new string[18]{
												"zero","one","two","three","four","five","six","seven",
												"ate","nine","left","right","up","down","select","cancel","pause","do a barrel roll"	
												};
	private bool[] 								numbers = new bool[10]{false,false,false,false,false,false,false,false,false,false};
	private bool[]								options = new bool[8]{false,false,false,false,false,false,false,false};
	private string								dictated;//if there is dictation strings,I'll hold them here
	private PXCMCapture.Device.Property			audio_mix_prop = PXCMCapture.Device.Property.PROPERTY_AUDIO_MIX_LEVEL;
	private bool								commandsSet = false;//bool for determining if the voice commands were set
	private bool								keepLooping = false;//bool to let the thread know to keep looping for query
	
	//this system object only exists so I can use it for the lock command later.
	private System.Object						lockObj = new System.Object();
	
	void Start () {
		myPipe = new PXCUPipeline();
		
		commandsSet = myPipe.SetVoiceCommands(commands);
		if(!commandsSet)Debug.Log("Failed to set Commands! :'(");
		if(!myPipe.Init(myMode) || !commandsSet){
			Debug.LogError("Failed To initialize PipeLine OH NOES!");	
			return;
		}
		else myPipe.SetDeviceProperty(audio_mix_prop,volume);//must choose a volume that handles the environment, sensitive mic
		
		keepLooping = true;//tell the thread not to stop
		dictated	= null;
		
		myThread = new Thread(ThreadFunc);//make thread handle
		myThread.Start();				  //let 'er rip!
		
	}
	
	void OnDisable(){
		//shut down the thread
		if(myThread!=null){
			keepLooping = false;
			Thread.Sleep(5);
			myThread.Join();
			myThread = null;
		}
		
		//shut down the pipeline
		if(myPipe != null){
			myPipe.Dispose();
			myPipe = null;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	//the thread function allows us to SPAM querying the pipeline for voice recognition
	//the outer loop continually loops until until told to stop. or you fail to acquire 
	//a frame (meaning the pipeline is not initialized or it is null)
	private void ThreadFunc(){
		while(myPipe.AcquireFrame(true) && keepLooping){
			if(myPipe.QueryVoiceRecognized(out voice)){//the out keyword causes the the function to change the original voice var
				lock(lockObj){						   //lockObj esists only for this purpose, making this critical section
					voiceHeard = true;
					dictated = "Voice Heard! Label: "+voice.label+", text: "+voice.dictation+", confidence: "+voice.confidence+"\n";
					Debug.Log(dictated);//the label is the index in the commands array of the heard command.
										//the dictation holds the string value at that position in the array.
										//confidence is for reference, 
					if(voice.label>-1 && voice.label<10)numbers[voice.label] = true;
					if(voice.label >9 && voice.label < 18)options[voice.label-10] = true;
				}
			}
			myPipe.ReleaseFrame();//must release the frame or you will never get any responses.
			
		}
		
	}
	
	//use the setVolume function to set how loud the player should speak to play, can be used anytime
	//the volume is private, can only be changed here
	public void setVolume(float desiredVolume){
		if(desiredVolume>1.0 || desiredVolume<=0){
			Debug.LogWarning("Volume choice must be [0.0f,1.0f]");
			return;
		}
		else{
			if(myPipe!=null){
				float[] newVolume = new float[1]{desiredVolume};
				myPipe.SetDeviceProperty(audio_mix_prop,newVolume);
			}
		}
	}
	
	//use this function to get an integer representing the number the player said.
	//if the valie is -1, the player said no number yet.
	public int getNumberVoiced(){
		for(int i = 0; i<10; i++){
			if(numbers[i] == true){numbers[i]=false; voiceHeard=false; return i;}
		}
		return -1;
	}
	
	//if you need to know which option was voiced, and need the actual string, here ya go.
	//returns an empty string if nothing was voiced. note: C# can switch on strings
	public string getOptionVoicedAsString(){
		for(int i = 0; i<7; i++){
			if(options[i]==true){	
				options[i]=false; voiceHeard = false; return commands[10+i];
			}
		}
		return "";
	}
	
	
	
}
