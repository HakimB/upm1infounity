# TP Layouts

- Créer un nouveau projet de type ```3D Core```

## Étude de la documentation

Source : [Unity Layout doc](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/UIAutoLayout.html)

### Tester le composant *Content Size Fitter*

Source : [Content Size Fitter](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/script-ContentSizeFitter.html)

Ce composant permet de gérer les dimensions du composant auquel il est rattaché (pas celui de ses enfants).

- Dans un ```Panel```, créer un ```GameObject Text (Legacy)```
- Associer à ce ```Text``` un composant ```Content Size Fitter```
- Tester les propriétés de ce composant
    - ```Unconstrained``` : on peut redimensionner le ```Text``` comme on le souhaite
    - ```Min Size``` : fixe les dimensions aux valeurs minimum du ```Text``` => attention, ces valeurs valent 0
    - ```Preferred Size``` : adapte les dimensions du ```Text``` à son contenu ; tester avec différentes tailles de fonte.

 
### Tester le composant *Aspect Ratio Filter*

Source : [AspectRatioFitter](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/script-AspectRatioFitter.html)

- Retirer le composant ```Content Size Filter``` du ```Text``` précédent
- Ajouter le composant ```Aspect Ratio Filter``` au ```Text```
- Tester les propriétés de ce composant
    - ```Width Controls Height``` : la hauteur est calculée à partir de la valeur de la largeur, selon le coefficient multiplicateur ```Aspect Ratio``` (largeur divisée par hauteur)
    - ```Height Controls Width``` : c'est l'inverse
    - ```Fit In Parent``` : le ```Text``` est agrandi jusqu'à ce que sa largeur ou sa hauteur atteigne celle de son parent (ici, le ```Panel```) : ```Aspect Ratio``` est toujours modifiable.
    - ```Envelope Parent``` redimensionne le ```Text``` jusqu'à recouvrir son parent. Selon ```Aspect Ratio```, cela peut entraîner le dépassement du composant par rapport à son parent.

## Outils de mise en page

### Tester le composant *Horizontal Layout Group*

Source: [Horizontal Layout Group](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/script-HorizontalLayoutGroup.html)

- Supprimer le ```Text``` précédent et en ajouter un nouveau dans le ```Panel```
- Ajouter un ```Slider``` dans le ```Panel```
- Ajouter au ```Panel``` le composant ```Horizontal Layout Group```
- Modifier les propriétés de ce composant
    - ```Padding``` : espacement par rapport au bord du ```Panel``` 
    - ```Spacing``` : espacement entre les ```GameObject``` enfants
    - ```Child Alignment``` (```Upper Left``` par défaut) : alignement des enfants
    - ```Reverse Alignment``` : inverse l'ordre des enfants
    - ```Control Child Size``` : modifie les dimensions des enfants en fonction de celles du parent (les propriétés de dimension et de position du ```Rect Transform``` des enfants sont bloquées).
    - ```Use Child Scale``` : détermine si l'échelle de redimensionnement des enfants est prise en compte dans leur redimensionnement => tester les effets de cette propriété lorsque les propriétés ```Rect Transform > Scale``` des enfants sont différentes de 1.
    - ```Child Force Expand``` : force les enfants à se redimensionner pour occuper l'espace utilisable dans le parent.

### Tester le composant *Vertical Layout Group*

Source: [Vertical Layout Group](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/script-VerticalLayoutGroup.html)

Même procédure que pour le composant ```Horizontal Layout Group```

### Tester le composant *Grid Layout Group*

Source: [Grid Layout Group](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/script-GridLayoutGroup.html)

- Retirer le ```Layout Group``` précédent du ```Panel```
- Insérer d'autres widgets dans ce ```Panel``` et les disposer librement
- Associer le composant ```Grid Layout Group``` au ```Panel``` et modifier ses propriétés
    - ```Padding``` : cf. plus haut
    - ```Cell Size``` : définit les dimensions des enfants
    - ```Start Corner```: le coin où le premier enfant est placé
    - ```Start Axis``` : sélectionne l'axe de placement principal (horizontal ou vertical)
    - ```Child Alignment```: alignement des enfants s'ils ne remplissent pas tout l'espace disponible
    - ```Constraint```: précise le nombre de lignes et de colonnes à respecter. La valeur ```Flexible``` redispose les enfants selon le redimensionnement du parent.


### Tester le composant *Layout Element*

Ce composant est ajouté aux enfants eux-mêmes et leurs propriétés se combinent avec celles des ```Layout Group``` de leurs parents. Les propriétés d'un ```Layout Element``` sont :
- ```Ignore Layout``` : le ```Layout``` est ignoré (pratique si les réglages entrent en contradiction  avec ceux d'un parent
- ```Min Width, Min Height``` : dimensions minimales de cet élément
- ```Preferred Width, Preferred Height``` : dimensions préférées de cet élément. La valeur affectée est calculée en fonction du texte existant.
- ```Flexible Width, Flexible Height``` : valeur relative disponible pour que l'élément courant se redimensionne par rapport à ses frères.
- ```Layout Priority``` : si le ```GameObject``` est associé à plusieurs composants qui disposent chacun de propriétés de ```Layout```, il faut éviter que ces dernière interfèrent. Le composant disposant de la priorité maximale prend le pas sur les autres.

Remarque : certaines propriétés peuvent interférer avec celles du parent. Par exemple, si on utilise  un ```Grid Layout Group``` qui fixe les dimensions des cellules, les propriétés de dimension des éléments de ce groupe devraient être recalculées (voir l'effet avec ```Preferred Width/Height``` par exemple.

## Widget : Le ComplexSlider en joli

### Création du ComplexSlider

Le principe est de recréer un ```Complex Slider``` nommé *ComplexSliderJoli* tel que les dimensions du ```Text``` à gauche du ```Slider``` et du ```Input Field``` à droite soient conservées, tandis que le ```Slider``` au centre soit redimensionné en même temps que le ```Panel```.

- Dans un nouveau, ```Panel```, insérer 
    - un composant ```Horizontal Layout Group```
    - des enfants ```Text (Legacy)```, ```Slider``` et ```InputField (Legacy)```

Noter que ces enfants sont automatiquement disposés en haut à gauche du ```Panel``` (réglage par défaut du ```Horizontal Layout Group```)

- Dans l'enfant ```Text```
    - modifier les propriétés
        - ```Text > Text``` : valeur "Size"
        - ```Text > Character > Font Size``` : 42
        - ```Text > Paragraph > Alignment``` : *Center* et *Middle*
    - ajouter le composant ```Layout Element``` avec les propriétés
        - ```Min Width``` (cochée) : 100 
        - ```Preferred Width``` (cochée) : 100

- Dans l'enfant ```InputField```
    - propriété ```Interactable``` : désactiver pour empêcher l'utilisateur d'écrire dans ce champ
    - propriété ```Transition > Disabled Color``` : cette couleur est gris par défaut, passer cette couleur en blanc et alpha  = 255 pour suivre l'illustration du sujet
    - composant ```Text``` : modifier les propriétés
        - ```Text > Text``` : valeur "000" (ou bien directement dans le ```Input Field``` lui-même
        - ```Text > Character > Font Size``` : 36
        - ```Text > Paragraph > Alignment``` : *Center* et *Middle*
    - ajouter le composant ```Layout Element``` avec les propriétés
        - ```Min Width``` (cochée) : 100 
        - ```Preferred Width``` (cochée) : 100

- Dans l'enfant ```Slider```
    - modifier les propriétés du composant  ```Slider```
        - ```Min Value``` : 0
        - ```Max Value``` : 999
        - ```Whole Numbers``` : cocher 
    - ajouter le composant ```Layout Element``` avec les propriétés
        - ```Min Width``` (cochée) : 100 
        - ```Flexible Width``` (cochée) : 1
        
**Important** C'est cette propriété ```Flexible Width``` qui active le redimensionnement du ```Slider``` en même temps que celui du ```Panel``` *ComplexSliderJoli* parent.


- Dans le parent *ComplexSliderJoli*
    - Propriétés du ```Horizontal Layout Group```:
        - ```ChildAlignment```: conserver *Upper Left*
        - ```Control Child Size > Height / Width``` : valider les deux valeurs pour redimensionner les enfants en fonction de la taille de *ComplexSliderJoli* (plus précisément, la hauteur de tous les enfants sera affectée ; mais seule la largeur du ```Text``` et du ```InputField``` ne sera pas modifiée, car la propriété ```Flexible Width``` de leur composant ```Layout Element``` n'est pas cochée).
    - ```Child Force Expand > Height``` : cocher pour adapter la hauteur (pas la largeur) des enfants à celle de leur parent.

Remarque : les composants de type ```Text``` nécessitent une taille minimale pour que leur contenu soit visible. Attention en redimensionnant le parent...

### Ajout d'un script

Associer le script suivant au ```ComplexSlider```.

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ComplexSliderScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    public Slider m_Slider;
    public InputField m_InputField;

    // Start is called before the first frame update
    void Start()
    {
        m_Slider = gameObject.GetComponentInChildren<Slider>();
        m_InputField = gameObject.GetComponentInChildren<InputField>();

        Debug.Log("Slider found: " + m_Slider  + " name: " + m_Slider.name);
        Debug.Log("Field found: " + m_InputField + "name: " + m_InputField.name);

        m_Slider.onValueChanged.AddListener(UpdateValueFromFloat);
        m_InputField.onEndEdit.AddListener(UpdateValueFromString);

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateValueFromFloat(float value)
    {
        Debug.Log("float value changed: " + value);
        if (m_InputField) { m_InputField.text = value.ToString(); }
    }

    public void UpdateValueFromString(string value)
    {
        Debug.Log("string value changed: " + value);
        try
        {
            float ff = float.Parse(value);
            if (m_Slider && m_Slider.value != ff) { m_Slider.value = ff; }
        }
        catch(System.Exception e) {
            Debug.Log("error: " + e);
        }
    }

}
```

## Widget Spinner

Ce widget sera composé de deux parties principales : un ```Input Field``` et un groupe de deux ```Button```, disposés de manière à ne pas modifier la position des boutons mais à laisser le ```Input field``` être redimensionné en fonction des dimensions du parent.

### Création du widget

- Dans le ```Canvas``` principal, créer un ```Panel``` nommé *SpinnerPanel*
- Insérer dans *SpinnerPanel* les enfants suivants :
    - ```Input Field (Legacy)"  
    - un ```GameObject``` de type ```Empty``` nommé *EmptyButtonParent* et dans ce dernier :
        - deux ```Button (Legacy)``` nommés respectivement *ButtonUp* et *ButtonDown*
- Ajouter à *SpinnerPanel* un composant ```Horizontal Layout Group``` avec les propriétés:
    - ```Child Alignment``` : *Upper Left*
    - ```Control Child Size``` : cocher ```Width``` et ```Height```
    - ```Child Force Expand``` : cocher ```Height``` 

- Dans ```Input field (Legacy)``` : 
    - propriété ```Interactable``` : désactiver pour empêcher l'utilisateur d'écrire dans ce champ
    - propriété ```Transition > Disabled Color``` : cette couleur est gris par défaut, passer cette couleur en blanc et alpha = 255 pour suivre l'illustration du sujet
    - modifier les composants
        - ```Input Field > Text``` : "0"
        - ```PlaceHolder > Text > Text``` : effacer "*Enter text...*"
        - ```Text > Character > Font Size``` : 42
        - ```Text > Paragraph > Alignment``` : *Center* et *Middle*
    - ajouter un composant ```Layout Element``` avec les propriétés
        - ```Min Width``` : cocher, valeur = 100
        - ```Preferred Width``` : cocher, valeur = 100
        - ```Flexible Width``` : cocher, valeur = 1

- Dans *EmptyButtonParent*
    - ajouter les composants :
        - ```Vertical Layout Group``` avec les propriétés
            - ```Spacing``` : 10 
            - ```Child Alignment``` : *Upper Left*
            - ```Control Child Size``` : cocher ```Width``` et ```Height```
            - ```Child Force Expand``` : cocher ```Height```
        - ```Layout Element``` avec les propriétés
            - ```Min Width``` : cocher, valeur = 100
            - ```Preferred Width``` : cocher, valeur  = 100
            - ```Layout Priority``` : 1 
     - ajouter deux composant de type ```Button``` recpectivement nommés *ButtonUp* et *ButtonDown*

- Dans *ButtonUp*
    - Pour associer une image de flèche
        - récupérer une image de type ```PNG``` et dans l'onglet ```Project```, clic droit et *Import New Asset...* puis sélectionner le fichier
        - dans ```Project```, sélectionner l'image importée et modifier la propriété ```Texture Type``` : *Sprite (2D and UI)*
        - valider le message de création de l'*Asset*
    - dans le composant ```Image```, modifier la propriété
        - ```Source Image``` en sélectionnant le petit rond à droite du champ et en filtrant la liste avec le nom du fichier
        - si l'image est mal proportionnée par rapport au bouton, on peut modifier la propriété ```Scale``` de son composant ```Rect Transform```
        - dans l'enfant de type ```Text```, modifier la rpopriété
            - ```Text > Text``` : effacer le texte par défaut "*Button*"

- Dans *ButtonDown*
    - Répéter les opérations entreprises pour *ButtonUp* en choisissant une autre image 


### Script associé

Ajouter le script "*SpinnerScript.cs*" suivant à *SpinnerPanel* :

```
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpinnerScript : MonoBehaviour/*, IPointerDownHandler, IPointerUpHandler, IDragHandler*/
{
    private Button m_ButtonUp, m_ButtonDown;
    private InputField m_InputField;
		public int m_IncrementStep = 1; // Incremnt laisse au choix de l'utilisateur
		
    // Start is called before the first frame update
    void Start()
    {
        m_InputField = gameObject.GetComponentInChildren<InputField>();
				if (m_InputField != null) {
						Debug.Log("Input Field found: " + m_InputField + "name: " + m_InputField.name);
				}

				Button[] buttons = gameObject.GetComponentsInChildren<Button>();
				if (buttons != null) {
						if (buttons[0].name.Equals("ButtonUp") && buttons[1].name.Equals("ButtonDown")) {
								m_ButtonUp = buttons[0]; 
								m_ButtonDown = buttons[1];
						}
						else if (buttons[1].name.Equals("ButtonUp") && buttons[0].name.Equals("ButtonDown")) {
								m_ButtonUp = buttons[1];
								m_ButtonDown = buttons[0];
						}
						else {
								Debug.Log("Buttons Up and Down not found!");
						}
        }
				else {
						Debug.Log("Buttons not found!");
				}

				if (m_ButtonUp != null && m_ButtonDown != null) {
						m_ButtonUp.onClick.AddListener(delegate {
										IncrementField(true);
								});
						m_ButtonDown.onClick.AddListener(delegate {
										IncrementField(false);
								});
				}
		}

    // Update is called once per frame
    void Update()
    {
    }

		private void IncrementField(bool increment) {
				string m_Text = m_InputField.text;
				try {
						int value = Int32.Parse(m_Text);
						Debug.Log("value = " + value);
						if (increment) {
								value += m_IncrementStep;
						}
						else {
								value -= m_IncrementStep;
						}
						Debug.Log("new value = " + value);
						m_InputField.text = value.ToString();
				}
				catch (FormatException e) {
						Debug.Log(e.Message);
				}
		}
}
``` 

## Gestion de la souris

### Nouvelle scène 

#### Création d'une scène
Pour ajouter une ```Scene``` à la *SampleScene* actuelle :

- Menu ```File > New Scene > Basic (Built-in)``` et cocher ```Load additively``` pour que la seconde ```Scene``` apparaisse dans la hiérarchie
- **Important** : ajouter un ```EventSystem``` dans la ```Scene``` pour que ses futurs widgets soient maipulables.
- Dans la hiérarchie, le nom de la nouvelle ```Scene``` est *Untitled*.
    -  Cliquer sur le symbole "3 points verticaux" à droite de ce nom pour **sauvegarder** cette ```Scene```, par exemple sous le nom *MouseScene*
    - Les fichiers *MouseScene.unity* et *MouseScene.unity.meta* sont créés.
- Dans la hiérarchie, activer l'affichage de la *MouseScene* et désactiver celui de *SampleScene*.
- Ou bien, dans le ```Project```, sélectionner la ```Scene``` que l'on veut manipuler

**Important** Dans le menu ```File > Build Settings...```, vérifier que toutes les scènes sont bien cochées


#### Script de permutation de scène

- Ajouter un bouton dans ```SampleScene```
    - nommer ce bouton ```ButtonSwitchToMouseScene```
    - modifier son enfant ```Text``` 
        - Composant ```Text``` 
            - Propriété ```Text``` = *Set MouseScene*
            - Propriété ```Font Size``` : 32
        - Composant ```Rect Transform```
            - ancre   *bottom left*
            - ```Pos X = 50 ; Pos Y = 35 ; Pos Z = 0```
            - ```Width = 280 ; Height = 45```

- Dupliquer ce bouton sous le nom *ButtonSwitchToSampleScene*
    - Dans *MouseScene*, créer un ```Panel``` 
    - Dans ce ```Panel```
        -  Composant ```Image```, modifier la propriété ```Color``` pour mettre la valeur du canal *alpha* à 0 : le ```Panel``` devient complètement transparent.
    - insérer *ButtonSwitchToSampleScene* ```Panel```, à la même position que le bouton précédent
    - modifier son enfant ```Text``` 
        - Composant ```Text``` 
            - Propriété ```Text``` = *Set SampleScene*

Associer aux deux boutons le script *SelectSceneScript.cs* suivant. 

```
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// Charge une des scènes du projet. Ce script est associé à tous les boutons (un par scène)
// permettant de sélectionner une autre scène

// ATTENTION : un GameObject "EventSystem" doit être placé dans CHAQUE scène
// pour que les iteractions sient prises en compte.

// Source : https://www.youtube.com/watch?v=PpIkrff7bKU
public class SelectSceneScript : MonoBehaviour
{
		private Button m_Button;

    // Start is called before the first frame update
    void Start()
    {
				m_Button = gameObject.GetComponent<Button>();
				if (m_Button == null) {
						Debug.Log("m_Button = null");
				}
				else {
						Debug.Log("m_Button " + m_Button.name + " found.");
				}
				m_Button.onClick.AddListener(delegate {
								SetOtherScene();
						});
    }

    // Update is called once per frame
    void Update()
    {
    }

		private void SetOtherScene() {
				Debug.Log("Enter SetOtherScene");
				if (m_Button.name.ToString().Equals("ButtonSwitchToMouseScene")) {
						SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
						Debug.Log("Selected button : ButtonSwitchToMouseScene");
				}
				else if (m_Button.name.ToString().Equals("ButtonSwitchToSampleScene")) {
						SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
						Debug.Log("Selected button : ButtonSwitchToSmapleScene");
				}
				else {
						Debug.Log("Error SetOtherScene");
				}
		}
}
```





----------------------------------------------------


### Script de gestion de la souris

Associer le script *PanelMouseScript.cs* suivant au ```Panel```.

```
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Affiche les coordonnees de la souris à chaque clic
public class PanelMouseScript : MonoBehaviour, IPointerClickHandler
{
		public void OnPointerClick(PointerEventData data)
    {
        Debug.Log("OnPointerClick " + data);
    }

}

```

## Instanciation dynamique d'un Widget

Source : [doc Unity](https://docs.unity3d.com/ScriptReference/Object.Instantiate.html)

Objectif : définir un script qui clone un widget choisi par l'utilisateur lors d'un clic de souris et le place à la position du curseur de la souris

### Création d'un Panel

- Dans *MouseScene*, insérer dans le ```Canvas > Panel``` un  ```widget``` quelconque, par exemple un ```Button```
- Associer au ```Panel``` le script suivant.

### Script associé au Panel

```
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Script associe à un Panel
// Clone un widget choisi par l'utilisateur lors d'un clic de souris. Ce widget sera integre comme fils du Panel.
// La position du clone est determinee par la position du curseur de la souris 
public class CloneWidgetScript : MonoBehaviour, IPointerClickHandler
{
		private static string TAG = "CW - ";
		private Camera m_Camera;
		public GameObject m_Widget; // Widget selectionne par l'utilisateur

		void Start() {
				// https://docs.unity3d.com/ScriptReference/Camera.html
				// The first enabled Camera component that is tagged "MainCamera" (Read Only).
				m_Camera = Camera.main;
				if (m_Camera == null) {
						Debug.Log(TAG + "Camera not found !");
				}
				
				
		}
		
		public void OnPointerClick(PointerEventData data)
    {
        Debug.Log(TAG + "OnPointerClick " + data);
				if (m_Widget != null) {
						Vector2 localPoint = new Vector2(data.position.x, data.position.y);
						Debug.Log(TAG + "Local point: " + localPoint);

						GameObject obj = Instantiate(m_Widget, localPoint, Quaternion.identity, gameObject.transform);
				}
				else {
						Debug.Log(TAG + "Pas de widget selectionne !");
				}
    }
}

```

## Redimensionnement d'un Widget

### Création du widget

Dans le ```Panel précédent```, insérer un ```widget``` quelconque (par exemple une image).

### Association du script

Associer le script suivant au ```widget``` créé.

```
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Redimensionne le Wisget associé à ce script, selon le "drag" de souris
// Seul le déplacement dans les directions "Droite" et "Bas" est correctement pris en compte
public class ResizeWidgetScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
		private Vector2 previousPointerPosition;
    private Vector2 currentPointerPosition;
    private RectTransform rectTransform;
		private static string TAG = "RW - ";
		
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
		
    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log(TAG + "OnPointerDown " + data);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(TAG + "OnPointerUp " + eventData);
    }

    public void OnDrag(PointerEventData data)
    {
        Debug.Log(TAG + "OnDrag " + data);
				rectTransform = GetComponent<RectTransform>();
				        
        if (rectTransform == null) {
						Debug.Log(TAG + "RectTransform not found !");
            return;
				}

				// https://docs.unity3d.com/ScriptReference/RectTransform-sizeDelta.html
        Vector2 sizeDelta = rectTransform.sizeDelta;
        Debug.Log(TAG + "Current Size: " + sizeDelta);

				// https://docs.unity3d.com/ScriptReference/RectTransformUtility.ScreenPointToLocalPointInRectangle.html
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, data.position, data.pressEventCamera, out currentPointerPosition);
        Vector2 resizeValue = currentPointerPosition - previousPointerPosition;

        Debug.Log(TAG + "Resize: " + resizeValue);

        sizeDelta += new Vector2(resizeValue.x, -resizeValue.y);
        
        rectTransform.sizeDelta = sizeDelta;
        Debug.Log(TAG + "New size: " + rectTransform);

        previousPointerPosition = currentPointerPosition;
    }
}
```
