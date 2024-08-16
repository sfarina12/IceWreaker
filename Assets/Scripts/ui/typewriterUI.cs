using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class typewriterUI : MonoBehaviour
{

	[Header("General Settings")]
	[Tooltip("Start typing after enabling the gameobject")]
	public bool atStart = false;
	[Tooltip("Random time for printing letter")]
	public Vector2 timeBtwChars = new Vector2(0.1f,0.1f);
	[Tooltip("Random time for printing a new line. Need to manually tell where a new line ends with '\n'")]
	public Vector2 timeBtwReturns = new Vector2(0,0);
	
	[Space,Header("Start Settings")]
	[Tooltip("Time to wait before start typing")]
	public float delayBeforeStart = 0f;
	[Tooltip("[can be null] [can be with scriptOnEnd] I will play a script and execute the function typeScript(). The script must the in the same gameobject of <typewriterUI>")]
	public string scriptOnStart = "";
	[Space,Tooltip("Add text before starting typing")]
	public string leadingChar = "";
	public bool leadingCharBeforeDelay = false;


	[Space,Header("End Settings")]
	public float timeBeforeScriptEnd = 0f;
	[Tooltip("[can be null] [can be with scriptOnStart] I will play a script and execute the function typeScript(). The script must the in the same gameobject of <typewriterUI>")]
	public string scriptOnEnd = "";
	
	[Space,Header("Audio Settings")]
	[Tooltip("[can be null] Plays a sound every time a letter get printed")]
	public audioPlayer audio;

	[Space,Header("Additional Settings")]
	[Tooltip("[can be null] Shows on a slider the 'loading' time before completeing typing")]
	public Slider slider;
	
	
	
	[HideInInspector] public string textToWrite;
	[HideInInspector] bool restart=false;
	Coroutine _parser;
	Text _text;
	TMP_Text _tmpProText;
	string writer;
	bool canScriptStart = false;
	bool canScriptEnd = false;
	bool canAudio = true;
	bool useSlider = true;


	// Use this for initialization
	void Start() {
		if(scriptOnStart != "") { canScriptStart = true; } //non controlla se effettivamente c'e' lo script di quel nome. Se da errore e probabilmente perche non c'e lo script
		if(scriptOnEnd != "") { canScriptEnd = true; } //non controlla se effettivamente c'e' lo script di quel nome. Se da errore e probabilmente perche non c'e lo script
		if(slider == null) { useSlider = false; }
		if(audio == null) { canAudio = false; } 
		else { audio.overrideEnd = true; }

		if (atStart) {
			_text = GetComponent<Text>()!;
			_tmpProText = GetComponent<TMP_Text>()!;
			
			//Debug.Log(_tmpProText.text.Contains(@" "));
			
			if(canScriptStart) { CallScriptMethod(scriptOnStart,"typeScript"); }

			if (_text != null) {
				writer = _text.text;
				_text.text = "";

				if(timeBtwReturns != new Vector2(0,0)) { 
					if(!writer.Contains("\n")) { Debug.LogWarning("The <typeWriterUI> component in gameobject "+gameObject.name+" have just one line! Empty [timeBtwReturns] if using just one line"); }
				}

				StartCoroutine("TypeWriterText");
			}

			if (_tmpProText != null) {
				writer = _tmpProText.text;
				_tmpProText.text = "";

				if(timeBtwReturns != new Vector2(0,0)) { 
					if(!writer.Contains("\n")) { Debug.LogWarning("The <typeWriterUI> component in gameobject "+gameObject.name+" have just one line! Empty [timeBtwReturns] if using just one line"); }
				}

				StartCoroutine("TypeWriterTMP");
			}
		}
	}

    void Update()
    {
		if(restart) {
			try { StopCoroutine(_parser); }
			catch (Exception e) { }

			if(canScriptStart) { CallScriptMethod(scriptOnStart,"typeScript"); }

			_text = GetComponent<Text>()!;
			_tmpProText = GetComponent<TMP_Text>()!;

			if (_tmpProText != null) {
				writer = textToWrite;
				if (useSlider) { slider.maxValue = writer.Length; slider.value = 0; }

				if(timeBtwReturns != new Vector2(0,0)) { 
					if(!writer.Contains("\n")) { Debug.LogWarning("The <typeWriterUI> component in gameobject "+gameObject.name+" have just one line! Empty [timeBtwReturns] if using just one line"); }
				}

				_parser = StartCoroutine("TypeWriterTMP");
			}

			restart = false;
		}
	}

    IEnumerator TypeWriterText() {
		_text.text = leadingCharBeforeDelay ? leadingChar : "";

		yield return new WaitForSeconds(delayBeforeStart);

		foreach (char c in writer) {
			if (_text.text.Length > 0) { _text.text = _text.text.Substring(0, _text.text.Length - leadingChar.Length); }
			_text.text += c;
			_text.text += leadingChar;

			audio.playAudio();

			float randomTime = 0;
			if(c.Equals('\n')) { randomTime = Random.RandomRange(timeBtwReturns.x,timeBtwReturns.y); }
			else { randomTime = Random.RandomRange(timeBtwChars.x,timeBtwChars.y); }

			yield return new WaitForSeconds(randomTime);
		}

		if (leadingChar != "") { _text.text = _text.text.Substring(0, _text.text.Length - leadingChar.Length); }

		yield return new WaitForSeconds(timeBeforeScriptEnd);
		if(canScriptEnd) { CallScriptMethod(scriptOnEnd,"typeScript"); }
	}

	IEnumerator TypeWriterTMP() {
		_tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";

		yield return new WaitForSeconds(delayBeforeStart);

		foreach (char c in writer) {
			if (_tmpProText.text.Length > 0) { _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length); }
			_tmpProText.text += c;
			_tmpProText.text += leadingChar;

			if (useSlider) { slider.value += 1; }

			audio.playAudio();

			float randomTime = 0;
			if(c.Equals('\n')) { randomTime = Random.RandomRange(timeBtwReturns.x,timeBtwReturns.y); }
			else { randomTime = Random.RandomRange(timeBtwChars.x,timeBtwChars.y); }

			yield return new WaitForSeconds(randomTime);
		}

		if (leadingChar != "") { _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length); }

		yield return new WaitForSeconds(timeBeforeScriptEnd);
		if(canScriptEnd) { CallScriptMethod(scriptOnEnd,"typeScript"); }
	}

	public object CallScriptMethod(string componentName, string methodName) {
        var component = gameObject.GetComponent(componentName);
        var componentType = component.GetType();
        var methodInfo = componentType.GetMethod(methodName);
        var parameters = new object[0]; // Set up parameters here, if needed.
        var result = methodInfo.Invoke(component, parameters);
        return result;
    }
}
