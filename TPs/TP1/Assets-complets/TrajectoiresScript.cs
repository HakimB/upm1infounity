using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Trajectoire circulaire ou elliptique en utilisant le soleil comme foyer d'une ellipse

// https://vanoise49.pagesperso-orange.fr/annexe_1_Trajectoire%20elliptique.pdf

// Pour ajouter un "Trail Renderer" : https://docs.unity3d.com/Manual/class-TrailRenderer.html
// Attention : la position par défaut de ce Trail n'est pas forcément calée sur le GameObject associé
// => mettre à jour les propriétés de "Transform" de ce Trail pour le positionner "sur" le GameObject
// => mettre à jour la propriété "Time" de "Trail Renderer" pour jouer sur la durée de la trainée.

public class TrajectoiresScript : MonoBehaviour
{
		private GameObject soleil;
		public float semiMajor = 20f ; // demi-grand axe de l'ellipse > 0

		// angle de rotation entre ce GameObject et le soleil ; à calculer à chaque pas de temps (
		public float angle = 0f; // à convertir en radians

		// inclinaison de l'ellipse (en degrés) en restant dans le même plan de l'écliptique
		public float eclipticAngle = 0f; // à convertir en radians

		// L'excentricité est définie selon la distance entre le centre de l'ellipse, l'un de ses foyers et le demi-grand axe
		private float eccentricity;
		
    // Start is called before the first frame update 
    void Start()
    {
				soleil = GameObject.Find("Soleil"); 
				if (soleil == null) {
						Debug.Log("TrajectoiresScript: soleil = null");
				}
    }
		
    // Update is called once per frame
    void Update()
    {
    }
		
    // FixedUpdate is called at fixed frames. To use with rigid Bodies
    // https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
    void FixedUpdate()
    {
				// En toute généralité, l'excentricité est définie sur un axe, X par convention.
				// Formule générale : excentricité = Abs(centre de l'ellipse.X - foyer de l'ellipse.X) / demi-grand axe
				// => et cette valeur doit être comprise dans [0 ; 1[
				// On suppose ici que le centre de l'ellipse est le centre du repère global (0,0,0)
				eccentricity = Mathf.Abs(soleil.transform.position.x) / semiMajor;
				Debug.Log("eccentricity = " + eccentricity);

				//	semiMinor = demi-petit axe de l'ellipse
				//   => semiMajor et semiMinor sont liés à l'excentricité de l'ellipse E par semiMinor = semiMajor * sqrt(1 - E*E)
				//  => si semiMajor et semiMinor sont égales, l'orbite est un cercle (facile à voir avec tiltEclipticAngle = 0)
				float semiMinor = semiMajor *Mathf.Sqrt(1 - eccentricity * eccentricity);
				
				float angleRad = Mathf.Deg2Rad * angle;
				float angleRadCos = Mathf.Cos(angleRad);
				float angleRadSin = Mathf.Sin(angleRad);

				float eclipticAngleRad = Mathf.Deg2Rad * eclipticAngle;
				float eclipticAngleRadCos = Mathf.Cos(eclipticAngleRad);
				float eclipticAngleRadSin = Mathf.Sin(eclipticAngleRad);

        // Remarque: Multiplier par Time.fixedDeltaTime (=0.02) donne des résultats proches de 0 
        // les coordonnées X et Z résultantes sont donc proches de 0 => ce GameObjet ne se déplace donc quasiment pas
        // => on "équilibre les coordonnées en multipliant par 50
				this.gameObject.transform.position = new Vector3(((semiMajor * angleRadCos * eclipticAngleRadCos) - (semiMajor * angleRadSin * eclipticAngleRadSin))
																												 * Time.fixedDeltaTime * 50f,
																												 0,
																												 ((semiMinor * angleRadCos * eclipticAngleRadSin) - (semiMinor * angleRadSin * eclipticAngleRadCos))
																												 * Time.fixedDeltaTime * 50f);
				
				angle += 1f;//can be used as speed
				if (angle > 360f) {
						angle = 0f;
				}
				
				Debug.Log("new X = " + this.gameObject.transform.position.x + ", new Z = " + this.gameObject.transform.position.z);
				Debug.Log("soleil.X = " + soleil.transform.position.x); // On peut faire varier la position du Soleil en X, sans dépasser la valeur de semiMajor
				Debug.Log("Time.fixedDeltaTime = " + Time.fixedDeltaTime);
    }
}
