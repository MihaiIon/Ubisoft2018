using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialEmission : MonoBehaviour {

	[SerializeField] float emissionMax = 20f;
	[SerializeField] float speed = 3f;
	[SerializeField] Color color1 = Color.red;
	[SerializeField] Color color2 = Color.blue;
	[SerializeField] float boostMultiplier = 3f;
	[SerializeField] bool switchColor;
	[SerializeField] bool emit;

	private Renderer _renderer;
	private MaterialPropertyBlock _propBlock;
	private float emission;

	// Use this for initialization
	void Awake () {
		
		emission = 0f;

		_propBlock = new MaterialPropertyBlock ();
		_renderer = GetComponent<Renderer> ();

		// Checks it the material on the object uses the Custom/shadow_SurfaceShader
		// Logs Error if not
		if(!_renderer.material.shader.Equals (UnityEngine.Shader.Find ("Custom/shadow_SurfaceShader")))
		{
			Debug.LogError ("Error : " + this.name + " is not using the Custom/shadow_SurfaceShader." +
				"Script/MaterialEmission should be used with Custom/shadow_SurfaceShader");
		}

		// Sets the default color to color1 and emission to 0.
		_renderer.GetPropertyBlock (_propBlock);
		_propBlock.SetFloat ("_Emission", emission);
		_propBlock.SetColor ("_EmissionColor", color1);
		_renderer.SetPropertyBlock (_propBlock);
	}

	// Update is called once per frame
	void Update () {

		// Update the emission intensity
		// Either towards emissionMax or zero emission
		if(emit)
			emission = Mathf.Lerp (emission, emissionMax, Time.deltaTime * speed);
		else
			emission = Mathf.Lerp (emission, 0f, Time.deltaTime * speed);

		// Get the current property block
		_renderer.GetPropertyBlock (_propBlock);

		// Changes the color when the switch changes
		if(switchColor){
			_propBlock.SetColor ("_EmissionColor", color2);
		}
		else{
			_propBlock.SetColor ("_EmissionColor", color1);
		}

		// Update the property block with the new parameters
		_propBlock.SetFloat ("_Emission", emission);
		_renderer.SetPropertyBlock (_propBlock);
	}

	// Setter
	public void SetEmit() {
		emit = true;
	}

	public void UnsetEmit() {
		emit = false;
	}

	// To switch between color1 and color2
	public void SwitchColor(){
		switchColor = !switchColor;
	}

	// To Boost the Emission for a short period of time
	public void BoostEmission(){

		emission = emissionMax * boostMultiplier;
	}
}
