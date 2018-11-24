using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: Make text offset independant of real cariage position
//TODO: Script should be an API
//TODO: Shifted key_press sound
//TODO: realtime kerning setup, adjust lines
//TODO: Check all returns - sometimes should return error, sometimes not

public class typewriter : MonoBehaviour {
	//Carriage return animation parameters
	public float carriage_return_anim_speed = 1f;

	//Carriage horizontal move step
	public float cariage_step = 0.006f;
	public float cariage_limit = 0.16f;

	//Text 2D offset parameters
	public float text2D_coeff_X = 35f;
	public float text2D_coeff_Y = 60f;
	public float text2D_offset_Y = 54f;

	//Paper animation parameters
	int paperRollInit = 29;
	public float paperRollStep = 2.205f;

	//References variables
	GameObject mechanics;
	GameObject space;
	GameObject lenta_bone;
	List<GameObject> buttons = new List<GameObject>();
	Animation anim;
	Animator animator;

	//Public references
	public RectTransform ref_text_2D;
	public GameObject paper_out;

	//Audio
	AudioSource aud;
	[System.Serializable]
	public struct audio_set {
		public AudioClip[] ShiftPress;
		public AudioClip[] ButtonPress;
		public AudioClip[] ButtonPressShifted;
		public AudioClip[] SpaceBar;
		public AudioClip[] Carr_ret;
		public AudioClip[] End_of_Line;
		public AudioClip[] NewLine;
		public AudioClip[] Paper_In;
		public AudioClip[] Paper_Out;
	}
	public audio_set[] audio_sets;
	public int active_audio_set = 0;

	[HideInInspector]
	public string letters = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM-=`\\/.,[];'";

	int PaperPosition = -1;
	bool carriage_return_request = false;
	float animator_stop = 0f;
	string button_down_AZ = "";
	bool button_shift_down = false;
	List<Vector2> AnimationRanges = new List<Vector2>();
	int[] letters_to_button = new int[]{0,1,2,3,4,5,6,7,8,9,13,14,15,16,17,18,19,20,21,22,27,28,29,30,31,32,33,34,35,40,41,42,43,44,45,46,10,11,12,23,24,47,48,49,50,37,5};
	GameObject[] hammers = new GameObject[31];
	Vector3[,] hammer_positions = new Vector3[31,4];

	//Error handling variables
	public enum errors
	{
		ok,
		paper_not_inserted,
		can_not_press_second_button,
		can_not_release_button_that_is_not_pressed,
		paper_already_animating,
		last_line_reached,
		there_is_no_such_letter,
		not_set
	}

	// Use this for initialization
	void Start ()
	{
		mechanics = GameObject.Find ("TypewriterDetail02");
		lenta_bone = GameObject.Find ("TypewriterDetail04_Bones").transform.GetChild (0).GetChild (0).gameObject;
		space = GameObject.Find ("CButtonsSpacebar");
		if (!space) space = GameObject.Find ("SButtonsSpacebar");

		aud = gameObject.AddComponent<AudioSource> ();
		animator = GetComponent<Animator> ();
		animator.speed = 0f;

		//Get buttons reference
		GameObject Buttons_Root = GameObject.Find("CButtons");
		if (!Buttons_Root) Buttons_Root = GameObject.Find("SButtons");
		foreach (Transform t in Buttons_Root.GetComponentsInChildren<Transform>()) {
			if (t.gameObject.GetComponent<MeshRenderer> () != null)
				buttons.Add (t.gameObject);
		}

		//Add paper roll line by line animations
		for (float i = paperRollInit; i <= 100f - paperRollStep; i = i + paperRollStep) {
			AnimationRanges.Add (new Vector2 (i * 0.01f, (i + paperRollStep - 1f) * 0.01f));
		}

		cariage_limit = Mathf.Round(cariage_limit / cariage_step) * cariage_step;

		//Ugly code, to get hammers end positions and rotations, when pressing a button
		Vector3[] end_pos = new Vector3[]{new Vector3 (0.0329f, 0.1475f, -0.128f), new Vector3 (-0.0063f, 0.1427f, -0.1066f), new Vector3 (-0.0494f, 0.1454f, -0.1281f)};
		Vector3[] end_rot = new Vector3[]{new Vector3 (29.667f, -74.143f, 104.557f), new Vector3 (-0.262f, -90.026f, 114.086f), new Vector3 (-27.63f, -113.53f, 108.979f)};
		int count = 16;
		for (int n = 0; n <= 1; n++) {
			for (int i = 1; i <= count; i++) {
				int ind = (n*15) + i;
				GameObject h = GameObject.Find("Hammer0" + ind.ToString());
	
				if (!h)
					h = GameObject.Find("Hammer" + ind.ToString());

				hammers[ind-1] = h;
				float step = (1f/(float)(count - 1)) * (i-1);
				hammer_positions[ind-1,0] = h.transform.localPosition;
				hammer_positions[ind-1,1] = h.transform.localRotation.eulerAngles;
				hammer_positions[ind-1,2] = Vector3.Lerp(end_pos[n], end_pos[n+1], step);
				hammer_positions[ind-1,3] = Vector3.Lerp(end_rot[n], end_rot[n+1], step);
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
		//Handle animator stop
		if (animator_stop > 0f && animator.GetCurrentAnimatorStateInfo (0).IsName("main") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= animator_stop) {
			if (animator_stop == 1f) {
				paper_out.SetActive(true);
				paper_out.GetComponent<Animation>().Play();
				mechanics.transform.GetChild(0).gameObject.SetActive(false);
				StartCoroutine(waitAndHidePaperOut());
			}
			animator.Play ("main", -1, animator_stop);
			animator.Update (0f);
			animator.speed = 0f;
			animator_stop = 0f;
		}

		//Animate carriage return if requested
		carriage_return_anim();
	}

	void carriage_return_anim ()
	{
		if (!carriage_return_request)
			return;

		Vector3 new_pos = mechanics.transform.localPosition - new Vector3 (carriage_return_anim_speed * Time.deltaTime, 0f, 0f);
		if (new_pos.x <= -cariage_limit) {
			new_pos = new Vector3 (-cariage_limit, 0f, 0f);
			carriage_return_request = false;
		}
		mechanics.transform.localPosition = new_pos;
	}

	public void TypeString (string str, float minDelay, float maxDelay)
	{
		if (!animator) Start();
		StartCoroutine(TypeStringCoroutine(str, minDelay, maxDelay));
	}
	IEnumerator TypeStringCoroutine (string str, float minDelay, float maxDelay)
	{
		if (PaperPosition < 0) {
			PaperInit();
			while (animator_stop != 0f)
					yield return null;

			yield return new WaitForSeconds (1f);
		}

		foreach (char c in str) {
			if (animator_stop != 0f)
				yield return new WaitForSeconds (0.1f);

			if (c.ToString () == "\n") {
				PaperToNextPosition();
				while (animator_stop != 0f)
					yield return null;

				yield return new WaitForSeconds (0.35f);
				
				CarriageReturn();
				while (carriage_return_request)
					yield return null;

				yield return new WaitForSeconds (0.8f);
				continue;
			}

			if (c.ToString() == " ") {
				PressSpace ();
				yield return new WaitForSeconds (Random.Range (0.05f, 0.1f));
				ReleaseSpace ();
				yield return new WaitForSeconds (Random.Range (minDelay, maxDelay));
				continue;
			}

			bool isUpper = false;
			int ind = letters.ToLower ().IndexOf (c);
			if (ind < 0) {
				ind = letters.IndexOf (c);
				if (ind >= 0) {
					PressShift ();
					isUpper = true;
					yield return new WaitForSeconds (Random.Range (0.05f, 0.1f));
				}
			}
			if (ind >= 0) {
				PressButtonAndType (ind);
				yield return new WaitForSeconds (Random.Range (0.05f, 0.1f));
				ReleaseButton (ind);

				if (isUpper) {
					yield return new WaitForSeconds (Random.Range (0.075f, 0.15f));
					ReleaseShift();
				}

				yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
			}
		}
	}

	public errors PressButtonAndType (string s)
	{
		int i = letters.IndexOf(s.ToUpper().Substring(0,1));
		if (i < 0) return errors.there_is_no_such_letter;
		return PressButtonAndType(i);
	}
	public errors PressButtonAndType (int i)
	{
		errors last_error;

		last_error = PressButton(i);
		if (last_error != errors.ok) return last_error;

		return TypeLetter(i);
	}

	public errors PressButton (string s)
	{
		int i = letters.IndexOf(s.ToUpper().Substring(0,1));
		if (i < 0) return errors.there_is_no_such_letter;
		return PressButton(i);
	}
	public errors PressButton (int i)
	{
		if (button_down_AZ != "")
			return errors.can_not_press_second_button;

		button_down_AZ = letters.Substring(i,1).ToLower();

		if (PaperPosition < 0)
			return errors.paper_not_inserted;

		//Move button model down and lenta model up
		buttons[letters_to_button[i]].transform.localRotation = Quaternion.Euler(new Vector3 (4f, 0f, 0f));
		lenta_bone.transform.position += new Vector3 (0f, 0.01f, 0f);

		//Move hammer
		int hammer_ind = i % 31;
		hammers[hammer_ind].transform.localPosition = hammer_positions[hammer_ind,2];
		hammers[hammer_ind].transform.localRotation = Quaternion.Euler(hammer_positions[hammer_ind,3]);

		//Play 'ButtonPress' sound
		if (!button_shift_down)
			playSound (audio_sets[active_audio_set].ButtonPress);
		else
			playSound (audio_sets[active_audio_set].ButtonPressShifted);
		return errors.ok;
	}

	public errors TypeLetter (string s)
	{
		int i = letters.IndexOf(s.ToUpper().Substring(0,1));
		if (i < 0) return errors.there_is_no_such_letter;
		return TypeLetter (i);
	}
	public errors TypeLetter (int i)
	{
		if (PaperPosition < 0)
			return errors.paper_not_inserted;

		//Create a letter on texture
		string letter = letters.Substring(i,1);
		if (!button_shift_down) letter = letter.ToLower();
		RectTransform t = Instantiate (ref_text_2D, ref_text_2D.parent);
		t.GetComponent<Text>().text = letter;
		int current_carriage_step = Mathf.RoundToInt(mechanics.transform.localPosition.x / cariage_step);
		t.anchoredPosition = new Vector2 (current_carriage_step * text2D_coeff_X, (PaperPosition * -text2D_coeff_Y) - text2D_offset_Y);
		t.gameObject.SetActive (true);
		return errors.ok;
	}

	public errors ReleaseButton (string s)
	{
		int i = letters.IndexOf(s.ToUpper().Substring(0,1));
		if (i < 0) return errors.there_is_no_such_letter;
		return ReleaseButton (i);
	}
	public errors ReleaseButton (int i)
	{
		if (button_down_AZ != letters.Substring (i, 1).ToLower ())
			return errors.can_not_release_button_that_is_not_pressed;

		button_down_AZ = "";
		if (PaperPosition < 0)
			return errors.paper_not_inserted;

		//Restore button and lenta positions
		buttons[letters_to_button[i]].transform.localRotation = Quaternion.Euler(new Vector3 (0f, 0f, 0f));
		lenta_bone.transform.position += new Vector3 (0f, -0.01f, 0f);

		//Restore hammer position
		int hammer_ind = i % 31;
		hammers[hammer_ind].transform.localPosition = hammer_positions[hammer_ind,0];
		hammers[hammer_ind].transform.localRotation = Quaternion.Euler(hammer_positions[hammer_ind,1]);

		//carriage_move_request = true;
		MoveCarriage();

		return errors.ok;
	}

	public void PressSpace ()
	{
		space.transform.localRotation = Quaternion.Euler(new Vector3 (4f, 0f, 0f));
		playSound (audio_sets[active_audio_set].SpaceBar);
	}

	public void ReleaseSpace ()
	{
		space.transform.localRotation = Quaternion.Euler(new Vector3 (0f, 0f, 0f));
		MoveCarriage();
	}

	public void PressShift ()
	{
		button_shift_down = true;
		buttons[26].transform.localRotation = Quaternion.Euler(new Vector3 (4f, 0f, 0f)); //Shift left
		buttons[38].transform.localRotation = Quaternion.Euler(new Vector3 (4f, 0f, 0f)); //Shift Right
		mechanics.transform.localPosition += new Vector3 (0f, 0.02f, 0f);
		playSound (audio_sets[active_audio_set].ShiftPress);
	}

	public void ReleaseShift ()
	{
		button_shift_down = false;
		buttons[26].transform.localRotation = Quaternion.Euler(new Vector3 (0f, 0f, 0f)); //Shift left
		buttons[38].transform.localRotation = Quaternion.Euler(new Vector3 (0f, 0f, 0f)); //Shift Right
		mechanics.transform.localPosition += new Vector3 (0f, -0.02f, 0f);
	}

	public errors PaperInit ()
	{
		if (animator.speed != 0f)
			return errors.paper_already_animating;

		mechanics.transform.GetChild(0).gameObject.SetActive(true);

		animator.speed = 1f;
		animator.Play ("main", -1, 0f);
		animator.Update (0f);
		animator_stop = AnimationRanges[0].x;

		PaperPosition = 0;
		playSound (audio_sets[active_audio_set].Paper_In);

		//Clear texture
		Transform t = ref_text_2D.parent;
		for (int n = t.childCount - 1; n >= 0; n--) {
			//Debug.Log (t.GetChild(n).name);
			if (t.GetChild (n).name.ToUpper ().Contains ("CLONE"))
				Destroy (t.GetChild (n).gameObject);
		}

		return errors.ok;
	}

	public errors PaperToNextPosition ()
	{
		if (animator.speed != 0f)
			return errors.paper_already_animating;
		if (PaperPosition < 0)
			return errors.paper_not_inserted;
		if (PaperPosition > AnimationRanges.Count - 1)
			return errors.last_line_reached;

		animator.speed = 1f;
		animator.Play ("main", -1, AnimationRanges[PaperPosition].x);
		animator.Update (0f);
		animator_stop = AnimationRanges[PaperPosition].y;

		PaperPosition += 1;

		playSound (audio_sets[active_audio_set].NewLine);
		return errors.ok;
	}

	public errors PaperOut ()
	{
		if (animator.speed != 0f)
			return errors.paper_already_animating;

		if (PaperPosition < 0)
			return errors.paper_not_inserted;

		animator.speed = 1f;
		animator.Play ("main", -1, AnimationRanges [PaperPosition].x);
		animator.Update (0f);
		animator_stop = 1f;

		PaperPosition = -1;
		playSound (audio_sets[active_audio_set].Paper_Out);
		return errors.ok;
	}

	public void CarriageReturn ()
	{
		carriage_return_request = true;
		playSound (audio_sets[active_audio_set].Carr_ret);
	}

	public void MoveCarriage ()
	{
		if (mechanics.transform.localPosition.x < cariage_limit)
			mechanics.transform.localPosition += new Vector3 (cariage_step, 0f, 0f);
		else
			playSound (audio_sets[active_audio_set].End_of_Line);
	}

	void playSound(AudioClip[] clip_arr) {
		int n = Random.Range (0, clip_arr.Length);
		aud.PlayOneShot(clip_arr[n]);
	}

	IEnumerator waitAndHidePaperOut()
	{
		yield return new WaitForSeconds(2);
		paper_out.SetActive(false);
	}
}
