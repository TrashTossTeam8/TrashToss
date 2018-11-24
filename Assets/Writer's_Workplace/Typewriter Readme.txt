  First of all, we are sorry, but we can not include typewriter fonts and sounds which were used in the demo.
For fonts, it's not a big deal: just go to http://www.1001fonts.com/typewriter-fonts.html and download whtever you want.
For sounds, it's a bit tricky: we didn't found any free sfx of this kind on the net, so you have to purchace a library or record your own sounds. For the demo,
we've just extracted sfx from a video.

The Skyboxes, used in the demo scene can be downloaded here:
https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-series-free-103633

1 	-	Using script API to make typewriter type.
1.1 - 	Error handling
2	-	Changing font and ajust spacing
3	-	Sounds
4	-	API Reference
--------------------------------------------------



1). Using script API to make typewriter type.
---------------------------------------------

  The Typewriter_TypingFromKeyboard prefab will work out of the box and allow to type directly from keyboard.
  F2 - Insert paper
  End - Eject paper
  Tab - Carriage return
  F5 - Autotype predefined text, that you can change in typewriter_simpleTyping.cs script.

  If you want to set up more advanced controls (touch screen, virtual keyboard, e.t.c.) you can look how it's done in the typewriter_simpleTyping.cs script,
  and write your own driving script.

  If you're an absolute beginner, who was never wrote a single script, just follow a tutorial below. Let's try, it's very easy.

  Create a new scene and drag a Typewriter prefab in it.
  Right click in project browser and create new c# script, and name it somehow - something like "typewriter_driver" or "typing_control" will do.
  Now, create an empty object in the scene and assign your new script to it, by pressing "Add Component" button in inspector and typing the name of your script,
or simply by drag'n'drop your script into inspector with this object selected. Or, you can just use "Typewriter" object as placeholder.
  
  Open your script, by double clicking on it in the project window.
  By default, unity create two function in this script - "Start" will be called once after script starts, and "Update" will be called every frame, 60+ times per second.
  
  To make things work, we only have to write 3 lines of code:
  1). Declare variable of type "typewriter". Just write BEFORE both functions but INSIDE a class, i.e. just after the line "public class name_of_your_script : MonoBehaviour {":
  	typewriter name_of_your_typewriter_variable;

  2). Assign typewriter script to your variable - inside "Start" function write (warning: all object and variable names are case sensitive, i.e. "TypeWriter" not equal "typewriter"):
  	name_of_your_typewriter_variable = GameObject.Find("Name_Of_The_Type_Writer_Object_In_The_Scene").transform.GetChild(0).GetComponent<typewriter>();

  3). Tell the script to type: just after the previous line, write:
  	name_of_your_typewriter_variable.TypeString("Chapter 1\nMy Cool Text\n\n", 0.15f, 0.25f);

  Now, move the camera so you can see typewriter in the game view, and press play.

  That was about making the typewriter type a text automatically.
  Now, we'll try to make it type from keyboard.

  Remove last line, or just comment it by putting // in the begining of that line.
  First of all, we need to insert the paper, as typewriter will refuse to type if the paper is not inserted.
  To handle this, we'll chack every frame, if the user press a button (we'll say F1), and if he does, we call the appropriate command on the typewriter script.
  So, you need to put this lines in the "Update" function.
	  if (Input.GetKeyDown (KeyCode.F1)) 
	  {
	  	name_of_your_typewriter_variable.PaperInit ();
	  }

  If you don't want to use "F1" key, just erase ".F1" part in the code above, and retype the point. The editor will let you choose from all buttons available.
  But the right way would be to assign a key "Paper Init" in unity Edit/Project Settings/Input and use Input.GetButtonDown("Paper Init") in the script. This way 
user can change this button later in the game.

  Anyway, now we can insert the paper by pressing F1. What about typing? Try to add this block in your "Update" function.
  	  if (Input.GetKeyDown (KeyCode.A)) {
  		  name_of_your_typewriter_variable.PressButtonAndType("A");
	  }
  
  Now, after inserting paper, you can press the "A" key on your keyboard and the typewriter will type it on the paper. But only once, because the key will never be released.
  To fix this add another block for handling key releasing.
	  if (Input.GetKeyUp (KeyCode.A)) {
		  name_of_your_typewriter_variable.ReleaseButton("A");
	  }

  Ok, now you can type as many "A" as you want. But the typewriter has 50+ buttons! Do we have to add 50 blocks for pressing and another 50 blocks for releasing???!!!111 Of course not.
  This loop will iterate through all available buttons in the typewriter:
	  foreach (char l in name_of_your_typewriter_variable.letters) {

	  }

  Most of the time, inside this loop, you'll want to put just two lines, to handle press and release of all the keys:
		if (Input.GetKeyDown (char.ToLower(l).ToString() )) name_of_your_typewriter_variable.PressButtonAndType (l.ToString());
		if (Input.GetKeyUp (char.ToLower(l).ToString() )) name_of_your_typewriter_variable.ReleaseButton (l.ToString());	

  That's it. You just need to add a few more blocks withing GetKeyDown/GetKeyUp conditions:
		.PressShift() / .ReleaseShift() - to handle pressing shift keys,
		.PressSpace() / .ReleaseSpace() - to handle spacebar,
		.PaperToNextPosition()			- to move paper to the next line,
		.PaperOut()						- to eject the paper,
		.CarriageReturn()				- self explanatory :)

1.1). Error handling
--------------------
Most of the typewriter functions will return you an error if something goes wrong. If you want to know what's happens, and why a letter was not printed you can use something like this:

typewriter.errors err = typewriter.errors.not_set;
err = type.PressButtonAndType ("A");
if (err != typewriter.errors.not_set)
	Debug.Log("Typewriter return error: " + err.ToString() );
  

2). Changing font and ajust spacing
-----------------------------------
  Typing on the texture and moving typewriter's carriage are two things that works independantly of each other. So, after changing a font size, you'll want to ajust text and carriage
 positions, so they move accordingly.

  To change font, open Typewriter/Camera_UI/Canvas/Panel/Text object in scene hierarchy, and select whatever font/style/size/color you want in the inspector.
  Now, if you'll try to type, the carriage and writings on the paper texture will probably get desynced.
  To fix this, find Typewriter/Typewriter_Obj object, and open typewriter script parameters in the inspector.

  	Text_2D_offset_Y 	- offset of the first line of the text on texture. Ajust this first, until you have first line printed where it should be.
  	Text_2D_coeff_X	- horizontal spacing between letters. Ajust this so the letters does not overlap.
  	Text_2D_coeff_Y	- vertical spacing between text lines. Ajust this so the lines does not overlap.

  When the text is printed correctly on the texture, you'll need to ajust carriage to move accordingly.

  	Carriage_Step		- carriage horizontal movement step. Ajust this, so the last printed letter on the line is printed right under the hammer.
  	PaperRollStep		- vertical paper movement step. Ajust this, so the last text line on the paper is printed under the hammers vertically.

  Other Parameters:
  	Carriage_limit				- horizontal limit of the carriage movement. The default value should work in most case. You don't need to change this until you'll want to make a paper wider.
	Carriage_return_anim_speed	- speed of the carriage movement, when returning to the begining of the line (when .CarriageReturn() is called).

3). Sounds
----------  
  The typewriter script allows to assign multiple sound sets and change the currently active sound set in inspector or by changing Active_audio_set public variable in the
typewriter class at runtime.
  To assign a sound set, select Typewriter/Typewriter_Obj object in the scene, open "Audio_sets" array in the inspector. Set the desired number of sound sets.
Then open "Element_0" sub array, and assign sounds to the typewriting actions. Do the same for other elements in the array if you have multiple sound sets.
!!!You can not leave an action with no sound at all (null) because it will throw an exception. Just use provided "dummy_empty_silence.mp3" if you don't want to use a sound for some actions.

4). API Reference
-----------------
TypeString (string str, float minDelay, float maxDelay)
- Automatically type a string. Use \n to move to the next line. The delay between pressing buttons is choosen randomly in range minDelay - maxDelay in seconds.

public errors PressButtonAndType (string s)
- Press a button on the typewriter's keyboard and print it on the paper texture. 
- s has to be one of the following symbols: 1234567890QWERTYUIOPASDFGHJKLZXCVBNM-=`\\/.,[];'

public errors PressButtonAndType (int i)
- Press a button on the typewriter's keyboard and print it on the paper texture.
- i ise the index of the letter in the string: "1234567890QWERTYUIOPASDFGHJKLZXCVBNM-=`\\/.,[];'"

public errors PressButton (string s)
- Press a button on the typewriter keyboard. The letter is not printed on the texture.
- s has to be one of the following symbols: 1234567890QWERTYUIOPASDFGHJKLZXCVBNM-=`\\/.,[];'

public errors PressButton (int i)
- Press a button on the typewriter keyboard. The letter is not printed on the texture.
- i ise the index of the letter in the string: "1234567890QWERTYUIOPASDFGHJKLZXCVBNM-=`\\/.,[];'"

public errors TypeLetter (string s)
- Type a letter on the paper texture.
- s has to be one of the following symbols: 1234567890QWERTYUIOPASDFGHJKLZXCVBNM-=`\\/.,[];'

public errors TypeLetter (int i)
- Type a letter on the paper texture.
- i ise the index of the letter in the string: "1234567890QWERTYUIOPASDFGHJKLZXCVBNM-=`\\/.,[];'"

public errors ReleaseButton (string s)
- Release a button
- s has to be one of the following symbols: 1234567890QWERTYUIOPASDFGHJKLZXCVBNM-=`\\/.,[];'

public errors ReleaseButton (int i)
- Release a button
- i ise the index of the letter in the string: "1234567890QWERTYUIOPASDFGHJKLZXCVBNM-=`\\/.,[];'"

PressSpace ()
- Press space bar key

ReleaseSpace ()
- Release space bar key and move carriage one letter forward

PressShift ()
- Press shift key. All the following letters will be printed in caps until ReleaseShift() method is called.

ReleaseShift ()
- Release shift key

public errors PaperInit ()
- Insert paper

public errors PaperToNextPosition ()
- Advance the paper to the next line

public errors PaperOut ()
- Eject paper

CarriageReturn ()
- Move the carriage to the initial (rightmost) position

MoveCarriage ()
- Advance carriage by one letter

Errors:
- "ok" - no error
- "there_is_no_such_letter" - if there is no such letter on the typewriter's keyboard (i.e. in the string: "1234567890QWERTYUIOPASDFGHJKLZXCVBNM-=`\\/.,[];'")
- "can_not_press_second_button" - if you're trying to press a button, but another button was already pressed and not released yet
- "paper_not_inserted" - you are trying to type, but the paper was not inserted yet
- "can_not_release_button_that_is_not_pressed" - you are calling ReleaseButton() method for a button which was not pressed or already released
- "paper_already_animating" - you are trying to insert/eject the paper or move it to the next line, but it's already animating (i.e. inserting/ejecting/moviing)
- "last_line_reached" - you are trying to move the paper to the next line, but it's already on the last line