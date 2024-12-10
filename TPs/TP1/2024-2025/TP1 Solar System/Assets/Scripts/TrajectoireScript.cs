using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Trajectoire circulaire ou elliptique en utilisant le soleil comme foyer d'une ellipse dans le plan (X,Z)
// On supose que le centre est en (0,0,0)
// Source : https://www.youtube.com/watch?v=CLXOEqvJ5Co

public class TrajectoireScript : MonoBehaviour
{
	private GameObject soleil;

	// Affichage en lecture seule. Voir fichiers
	// ShowOnlyAttribute.cs et ShowOnlyDriver.cs
	[ShowOnly] public float distanceCenterSun;
	[ShowOnly] public float semiMajor ; // demi-grand axe de l'ellipse > distanceCenterSun
	[ShowOnly] public float param; // paramèetre de l'équation polaire
	[ShowOnly] public float dist; // distance entre soleil et objet
	[ShowOnly] public float eccentricity;
	
	private Vector3 center = new Vector3(0.0f, 0.0f, 0.0f);
	private float semiMajorScale = 1.3f; // utilisé pour s'assurer que semiMajor est toujours > ditanceCenterSun 

	public float semiMinor ; // demi-petit axe de l'ellipse <= semiMajor, modifiable par l'utilisateur

	// Angle de rotation entre ce GameObject et le soleil ; 
	// a calculer a chaque pas de temps 
	[ShowOnly] public float angle = 0f; // a convertir en radians
	
	
	
	// Start is called before the first frame update
	void Start()
	{
		soleil = GameObject.Find("Soleil");
		if (soleil == null) 
			Debug.Log("TrajectoiresScript: soleil = null");

		distanceCenterSun = Vector3.Distance(center, soleil.transform.position);
		semiMajor = distanceCenterSun * semiMajorScale; // toujours
		semiMinor = semiMajor;

	}
	
	// Update is called once per frame
	void Update() { }

		// FixedUpdate is called at fixed frames. To use with rigid Bodies
	// https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
	void FixedUpdate()	{
		 // L'utilisateur peut modifier la position du soleil (en restant dans le même plan), ce qui va jouer sur semiMajor
		 distanceCenterSun = Vector3.Distance(center, soleil.transform.position);
		 semiMajor = distanceCenterSun * semiMajorScale;

		 // semiMinor doit rester <= semiMajor
		 if (semiMinor > semiMajor || semiMinor == 0f)
		 	semiMinor = semiMajor;

	     // L'excentricité doit être incluse dans [0.0; 1.0[
	  	eccentricity = (Mathf.Sqrt(semiMajor * semiMajor - semiMinor * semiMinor)) / semiMajor;

		// Parametre pour équation polaire
		param = semiMinor * semiMinor / semiMajor;
		Debug.Log("param = " + param);

		// Distance entre l'objet en mouvement et l'un des foyers (le soleil)
		float angleRad = Mathf.Deg2Rad * angle;
		dist = param / (1 + eccentricity * Mathf.Cos(angleRad));

		// Position en X,Z de l'objet : dépend de sa distance par rapport au soleil
		// et de la position de ce dernier dans le repère global
		float newX = dist * Mathf.Cos(angleRad) + soleil.transform.position.x;
		float newZ = dist * Mathf.Sin(angleRad) + soleil.transform.position.z;

		this.gameObject.transform.position = new Vector3(newX,
										   soleil.transform.position.y,
										   newZ);

		angle += 1f;//can be used as speed
				
		if (angle > 360f) { angle = 0f;	}
			
		Debug.Log("new X = " + this.gameObject.transform.position.x + 
		", new Z = " + this.gameObject.transform.position.z);
	
	}
}		
