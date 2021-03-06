/*Mathius: Defender of Earth 
 * attach this script to the Brain object in the Initialize Brain scene
 * to use this interface correctly, all scripts must make a variable of this script.
 * Then use the public functions to query information.
 * DO NOT HAVE COPIES OF THIS SCRIPT OR THERE WILL BE DELAYS IN QUERYING THE DEVICE
 * USE THREADING TO AVOID!
 * 
 * This code was written by Michial Green II (VMichial)
 * This code is self contained, meaning it can return
 * information relevant to finding the swipe gestures
 * and returning the current location of the FIRST
 * tracked hand or left hand if the camera can determine that.
 * This is an interface layer for the SDK i made, use the public
 * functions if you want to ask for information
  */


using UnityEngine;
using System.Collections;

public class PerCGesture : MonoBehaviour {
	public static PerCGesture PG;
	
	/*
	 * These variables hold the relevant data needed for the pipeline
	 * ALL INFORMATION IS PRIVATE, ANYTHING YOU NEED TO KNOW IS GAINED
	 * BY USING THE PUBLIC FUNCTIONS!
	 */
	private PXCUPipeline 					myPipe=null;		//handle for the pipeline
	private bool 							yup;
	private	PXCMGesture.GeoNode.Label 		trackedLimb; 	//this label tells the pipeline what to track
	private	PXCMGesture.GeoNode				nodeInfo;		//will hold geonode data, if the hand is present, this has info on left hand
	private PXCMGesture.Gesture				movement;		//will hold the gesture data the pipeline found.
	
	private int[]							resolution = new int[2];
	private bool							resFound;		//true if the resolution was gained successfully
	private PXCUPipeline.Mode 				myMode;
	private bool hand = false;
	private float[] xy;
	//these bools will be set to true in the event the corresponding gesture is found.
	//use the swipedLeft, swipedRight, swipedUp, and swipedDown functions for event tests
	private bool swipeLeft;
	private bool swipeUp;
	private bool swipeDown;
	private bool swipeRight;
	private bool circle;//in case you want a gesture for doing a barrel roll
	private bool thumbUp;
	private bool thumbDown;
	private bool initiated;
	//bools for turning on and off debugs, tracking shows hand loc, debugging
	//shows change in movement.
	private bool debugging = false;
	private bool tracking = false;
	
	private bool moveRight = false;
	private bool moveLeft = false;
	private bool moveUp = false;
	private bool moveDown = false;
	private bool centeredX = false;
	private bool centeredY = false;
	
	private float centerX = 157.0f;
	private float centerY = 121.0f;
	private float zoneX = 30.0f;
	private float zoneY = 20.0f;

	void Start () {
		xy = new float[2]{157.0f,121.0f};
		PG = gameObject.GetComponent<PerCGesture>();
		//in start i am just going to initialize the pipeline and get the resolution of the camera
		//to get the resolution, use the getRez function.
		
		myPipe = new PXCUPipeline();
		trackedLimb = PXCMGesture.GeoNode.Label.LABEL_BODY_HAND_PRIMARY;// Primary is first tracked hand
		myMode = PXCUPipeline.Mode.GESTURE|PXCUPipeline.Mode.COLOR_VGA  ; //the mode i want to use
		
		if(!myPipe.Init(myMode)){
			Debug.Log("The pipeline failed to initialize bras :'(\n");
			initiated=false;
			return;
		}
		else initiated = true;
		
		resFound = myPipe.QueryRGBSize(resolution);
		centeredX = true;
		centeredY = true;
	}
	

	void Update () {
		//don't do anything if the pipeline is null
		if(myPipe == null)return;
		if(!initiated)return;
		
		if(!myPipe.AcquireFrame(false))return;	//cannot query the device or check for data without
												//first acquiring the frame
		
		if(myPipe.QueryGeoNode(trackedLimb,out nodeInfo)){//out causes the function to change the data within nodeInfo
			if(tracking)Debug.Log ("hand found!"+" X="+nodeInfo.positionImage.x+" y="+nodeInfo.positionImage.y + ",res:"+resolution[0]+","+resolution[1]);
		}
		
		///the following code will perform the event sending for movement.
		//basically we only want to send an event if your hand position
		//changes mathius' movement. that is, if you decided to move Mathius
		//from the current movement to another. 
		//example: Your hand is centered, therefore mathius isn't to move.
		//you then move your hand to the right, once your hand leaves the
		//centered area and is in the right, we send a move right event
		//you can move your hand all you want in the right area and no event
		//will fire, but move your hand to the centered area and we fire a stop.
		if(centeredX){
			//shouldn't short circuit from centered, made some cases not get caugh
			if(nodeInfo.positionImage.x<(centerX-zoneX)){
				/*Perform event to move right*/
				centeredX = false;
				moveRight = true;
				if(debugging)Debug.Log("Move Right");
			}
			if(nodeInfo.positionImage.x>(centerX+zoneX)){
				centeredX = false;
				moveLeft = true;
				/*Perform event to move left*/
				if(debugging)Debug.Log("Move Left");
			}
		}
		if(centeredY){
			if(nodeInfo.positionImage.y<(centerY-zoneY)){
				/*Perform event to move up*/
				centeredY = false;
				moveUp = true;
				if(debugging)Debug.Log("Move Up");
			}
			if(nodeInfo.positionImage.y>(centerY+zoneY)){
				/*Perform event to move down*/				
				if(debugging)Debug.Log("Move Down");
				centeredY = false;
				moveDown = true;				
			}
		}
		if(moveUp){
			//have to set all moves to false when going centered.
			if(nodeInfo.positionImage.y>(centerY+zoneY)){
				if(debugging)Debug.Log("Move Down, Stop Up");
				moveUp = false;
				moveDown = true;
				/*Perform event to move down*/
				//send stop for moving up
				//moveup false, movedown true
			}
			else if(nodeInfo.positionImage.y<(centerY+zoneY) && nodeInfo.positionImage.y > (centerY-zoneY)){
				//perform event stop on move up, centeredY  = true	
				//moveup false
				if(debugging)Debug.Log("Y Centered, Stop Moving up/down");
				moveUp = false;
				centeredY = true;
			}
		}
		if(moveDown){
			if(nodeInfo.positionImage.y<(centerY-zoneY)){
				if(debugging)Debug.Log("Move Up, Stop down");
				moveDown = false;
				moveUp = true;
				/*Perform event to move up*/
				//send stop on move down
				//send go on move up
				//moveup true
				//movedown false
			}
			else if(nodeInfo.positionImage.y<(centerY+zoneY) && nodeInfo.positionImage.y > (centerY-zoneY)){
				//perform event to stop move down,centered Y true
				if(debugging)Debug.Log("Y Centered, Stop Moving up/down");
				moveDown = false;
				centeredY = true;
			}
		}
		if(moveRight){
			if(nodeInfo.positionImage.x>(centerX+zoneX)){
				if(debugging)Debug.Log("Move Left, Stop Right");
				moveRight = false;
				moveLeft = true;
				/*Perform event to move left*/
				//stop on move right
				//yes on move left
				//moveright false
				//moveleft true
			}
			else if(nodeInfo.positionImage.x<(centerX+zoneX) && nodeInfo.positionImage.x > (centerX-zoneX)){
				if(debugging)Debug.Log("X Centered, Stop Moving left/right");
				moveRight = false;
				centeredX = true;
				//stop on move right and move left
				//centeredX true
				
			}
		}
		if(moveLeft){
			if(nodeInfo.positionImage.x<(centerX-zoneX)){
				if(debugging)Debug.Log("Move right, Stop left");
				moveLeft = false;
				moveRight = true;
				/*Perform event to move right*/
				//stop on move left
				//yes on move right
				//moveright true
				//moveleft false
			}
			else if(nodeInfo.positionImage.x<(centerX+zoneX) && nodeInfo.positionImage.x > (centerX-zoneX)){
				if(debugging)Debug.Log("X Centered, Stop Moving left/right");
				moveLeft = false;
				centeredX = true;
				//stop on move right and move left
				//centeredX true
				
			}
		}
		
		if(myPipe.QueryGesture(trackedLimb,out movement)){//out causes the function to change the data within movement
			Debug.Log(movement);
			if(movement.label == PXCMGesture.Gesture.Label.LABEL_NAV_SWIPE_DOWN) swipeDown = true;
			else if(movement.label == PXCMGesture.Gesture.Label.LABEL_NAV_SWIPE_LEFT) swipeLeft = true;
			else if(movement.label == PXCMGesture.Gesture.Label.LABEL_NAV_SWIPE_RIGHT) swipeRight = true;
			else if(movement.label == PXCMGesture.Gesture.Label.LABEL_NAV_SWIPE_UP) swipeUp = true;
			else if(movement.label == PXCMGesture.Gesture.Label.LABEL_HAND_CIRCLE) circle = true;
			else if(movement.label == PXCMGesture.Gesture.Label.LABEL_POSE_THUMB_UP)thumbUp = true;
			else if(movement.label == PXCMGesture.Gesture.Label.LABEL_POSE_THUMB_DOWN)thumbDown = true;
		}
		
		myPipe.ReleaseFrame();
	}
	
	void OnDisable(){
		//shut this motha down.
		if(myPipe == null)return;
		myPipe.Dispose();
		myPipe = null;
	}
	
	// use the swiped functions if you want to check if a swipe gesture has occured
	//if the function returns true, it cannot return true again, until the swipe has
	//occured again
	public bool swipedUp(){
		if(swipeUp){swipeUp = false; return true;}
		else return false;
	}	
	public bool swipedDown(){
		if(swipeDown){swipeDown = false; return true;}
		else return false;
	}
	public bool swipedRight(){
		if(swipeRight){swipeRight = false; return true;}
		else return false;
	}
	public bool swipedLeft(){
		if(swipeLeft){swipeLeft = false; return true;}
		else return false;
	}
	//in case we want to make barrel rolling, the circle gesture is added
	public bool circled(){
		if(circle){circle=false; return true;}
		else return false;
	}
	
	//check for a thumb up
	public bool thumbedUp(){
		if(thumbUp){thumbUp=false; return true;}
		else return false;
	}
	
	public bool thumbedDown(){
		if(thumbDown){thumbDown=false; return true;}
		else return false;
	}
	
	//if you need to know the resolution for any mathematical reason, use getResolution.
	//if the value you received is -1,-1 then you have to use a less accurate method of hand tracking
	public int[] getResolution(){
		if(!resFound) return new int[2]{-1,-1};//-1,-1 is my error resolution
		else return resolution;		
	}
	// get resolution overload that allows you to pass in a bool for error checking if you so desire
	public int[] getResolution(out bool success){
		if(!resFound){success = false; return new int[2]{-1,-1};}//-1,-1 is my error resolution
		else {success = true; return resolution;}	
	}
	
	//this function returns the current primary hand location, meaning, the first hand found by the camera.
	//remember to hide your hands, then show the one you want first to have it tracked
	public float[] getHandLocation(){
		if(myPipe==null || !initiated){
		return new float[2]{157.0f,121.0f};	
		}
		return new float[2] {nodeInfo.positionImage.x,nodeInfo.positionImage.y};
	}
	
	public void restart(){
		//shut this motha down.
		if(myPipe != null){
			myPipe.Dispose();
			myPipe = null;
		}
		
		myPipe = new PXCUPipeline();
		trackedLimb = PXCMGesture.GeoNode.Label.LABEL_BODY_HAND_PRIMARY;// Primary is first tracked hand
		myMode = PXCUPipeline.Mode.GESTURE|PXCUPipeline.Mode.COLOR_VGA  ; //the mode i want to use
		
		if(!myPipe.Init(myMode)){
			Debug.Log("The pipeline failed to initialize bras :'(\n");
			initiated=false;
			return;
		}
		else initiated = true;
	}
	
	
}
