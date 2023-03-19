# TP2 + TP3  - WidgetDev

- Créer un nouveau projet de type ```3D Core```

## Modifier la résolution

- Dans l'onglet ```Game``` de l'interface, cliquer sur ```Free Aspect``` et sélectionner une résolution, par exemple *Full HD (1920x1080*.

- Ajouter un ```Canvas``` dans la *Hierarchy*
    - Modifier le composant ```Canvas Scaler``` :
        - ```UI Scale Mode > Scale With Screen Size``` 
        - ```Reference Resolution``` : faire correspondre à la résolution précédemment choisie.

Remarque: par défaut, le ```Canvas``` a comme propriété ```Canvas > Render Mode``` la valeur ```Screen Space - Overlay``` qui signifie que le ```Canvas``` va couvrir tout l'écran d'affichage (par conséquent, les propriétés du ```Rect Transform``` sont désactivées).

## Widget complexe - FormattedInputField

### Création du Panel

- Dans le ```Canvas```, créer un ```Panel``` et le nommer *PanelInputFieldText*
    - Définir son ancre en ```Center / Middle``` pour qu'il soit placé automatiquement au centre du ```Canvas```.
    - Préciser les dimensions, par exemple ```Width = 1000``` et ```Height = 100```.


- Dans le ```Panel``` :
- ajouter un ```UI > Legacy > Input Field``` et le renommer ```InputFieldText1``` 
    - ```Rect Transform```
        - ```> Anchor Bottom Left```
        - ```Pos X = 0 ; Pos Y = 0; Pos Z = 0```
        - ```Width = 300```, ```Height = 100```
        - ```Pivot X = 0 ; Pivot Y```  
        - 
- dupliquer ce  ```GameObject``` et le renommer ```InputFieldText2```         
    - ```Rect Transform```
        - ```Pos X = 345 ; Pos Y = 0; Pos Z = 0```
         
- dupliquer ce  ```GameObject``` et le renommer ```InputFieldText3      ```         
    - ```Rect Transform```
        - ```Pos X = 690 ; Pos Y = 0; Pos Z = 0```

- Dans chaque ```InputFieldText```, sélectionner le composant ```Text (Legacy)``` et affecter la valeur 44 à la propriété ```Character > Font Size```.

### Script associé à chaque Inputfield

Associer le script *FormattedinputFieldScript.cs* :

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

// Script à associer aux GameObject de type "InputFieldText"

public class FormattedInputFieldScript : MonoBehaviour
{
    public string m_Regex = "^[0-9]+$"; // Regex validant uniquement une suite de chiffres. Modifiable dans l'Inspector
		private InputField m_InputField; // Composant InputField lié à ce GameObject
    private Color colorEmpty = Color.white; // Si Inputfield vide
    private Color colorError = Color.red; // Si texte de l'InputField ne valide pas le regex
    private Color colorValid = Color.green; // Si texte de l'InputField valide le regex

    // Start is called before the first frame update
    void Start()
    {
				m_InputField = this.GetComponent<InputField>();
				if (m_InputField == null) {
						Debug.Log("[FormattedInputFieldScript] input field = nul");
				}
				
				m_InputField.onValueChanged.AddListener(delegate {
								ValueChangeCheck();
						});
				
    }

		// Attention : cette fonction est définie indépendamment de la méthode Start()
		// => initialiser les attributs [m_Text] et [m_Image] dans Start() ne sert à rien
		// ==> ils sont considérés comme null dans cette fonction.
		// ===> on en fait des variables locales et le problème est réglé.
		private void ValueChangeCheck() {
				string m_Text = m_InputField.text;
				if (m_Text == null) {
						Debug.Log("[FormattedInputFieldScript] text = nul");
				}
				else {
						// Le type "string" ne supporte pas la concaténation à la Java
						// => Debug.Log("m_Text = " + m_Text); provoque des erreurs
						Debug.Log(string.Concat("m_Text = ", m_Text));

						Image m_Image = this.GetComponent<Image>();
						if (m_Image == null) {
								Debug.Log("[FormattedInputFieldScript] image = nul");
						}
						
						if (string.IsNullOrEmpty(m_Text)) {
								m_Image.color = this.colorEmpty;
						}
						else {
								Debug.Log(string.Concat("m_Regex = ", m_Regex));

								// Version fonctionnelle numéro 1
								/*
								if (Regex.IsMatch(m_Text, m_Regex)) {
										m_Image.color = colorValid;
								}
								else {
										m_Image.color = colorError;
								}
								*/

								// Version fonctionnelle numéro 2
								// https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity5.html
								// https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.match?view=net-8.0
								Regex myRegExp = new Regex(m_Regex);
								Match myMatch = myRegExp.Match(m_Text);
								if (myMatch.Success) {
										m_Image.color = colorValid;
								}
								else {
										m_Image.color = colorError;
								}
						}
				}
		}
}

```

## ComplexSlider

### Création du Panel

- Dans le ```Canvas``` principal, créer un nouveau ```Panel``` et le nommer *PanelComplexSlider1*.
        - Définir son ancre en ```Center / Middle``` pour qu'il soit placé automatiquement au centre du ```Canvas```.
        - Préciser les dimensions, par exemple ```Width = 1000``` et ```Height = 100```.
        - Déplacer ce ```Panel``` en ```Pos X = 0; Pos Y = -200; Pos Z = 0```

- Dans *PanelComplexSlider1*, créer
    - un ```Text``` avec ```UI > Legacy > Text``` renommé *Text*
        - ```Rect Transform```
            - ```Anchor = bottom left```
            - ```Pos X = Pos Y = Pos Z = 0```
            - ```Width = 40 : Height = 100```
            - ```Pivot : X = 0 ; Y = 0```
        - ```Text```
            - ```Font Size = 44```
            - ```Paragraph > Alignment = middle / center```    
         
    - un ```Slider``` avec ```UI > Slider```
        - ```Rect Transform```
            - ```Anchor = bottom left```
            - ```Pos X = 430 ; Pos Y = 50 ; Pos Z = 0```
            - ```Width = 780 : Height = 60```
            - ```Pivot : X = 0/5 ; Y = 0.5```
     
    - un ```Input Field``` avec ```UI > Legacy > Input Field```
        - ```Rect Transform```
            - ```Anchor = bottom left```
            - ```Pos X = 900 ; Pos Y = 50 ; Pos Z = 0```
            - ```Width = 170 : Height = 80```
            - ```Pivot : X.5 = 0 ; Y = 0.5```
        - Enfant ```PlaceHolder > Text``` : effacer le texte par défaut *Enter text...*
        - Enfant ```Text > Text```
            - ```Font Size = 44```
            - ```Paragraph > Alignment = middle / center```    


### Script associé au Panel

Associer le script *ComplexSliderScript.cs* suivant :

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ComplexSliderScript : MonoBehaviour 
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

### Complément au script précédent

Le code suivant permet d'afficher les coordonnées de la souris et lorsque celle-ci se trouve dans *PanelComplexslider1*,
de redimensionner ce dernier.

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

    //---------------------------------------------------------------------------------------------------------
		// Redimensionnement du Panel parent selon les déplacements de la souris
    private Vector2 previousPointerPosition;
    private Vector2 currentPointerPosition;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
		
    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("OnPointerDown " + data);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp " + eventData);
    }

    public void OnDrag(PointerEventData data)
    {
        Debug.Log("OnDrag " + data);
				rectTransform = GetComponent<RectTransform>();
				        
        if (rectTransform == null)
            return;

        Vector2 sizeDelta = rectTransform.sizeDelta;
        Debug.Log("Current Size: " + sizeDelta);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, data.position, data.pressEventCamera, out currentPointerPosition);
        Vector2 resizeValue = currentPointerPosition - previousPointerPosition;

        Debug.Log("resize: " + resizeValue);

        sizeDelta += new Vector2(resizeValue.x, -resizeValue.y);
        
        rectTransform.sizeDelta = sizeDelta;
        Debug.Log("resize: " + rectTransform);

        previousPointerPosition = currentPointerPosition;
    }
}

```

## Créer un Préfabriqué "Prefab"

Un *Prefab* est un container représentant la sauvegarde d'un *Composant*, que l'on peut réutiliser dans d'autres projets.

Il suffit de sélectionner le composant qui nous intéresse : ici, *PanelComplexslider1*, et de le déplacer dans le ```Project```.

Attention : veiller à ce que ce composant n'ait pas de dépendance extérieure, sinon ces dernières seront également sauvegardées dans le ```Prefab```.

Remarque : si l'on sélectionne ce ```Prefab```, l'```Inspector``` affiche un bouton ```Open``` qui modifie l'interface principale pour lister uniquement la hiérachie de ce ```Prefab```. Pour revenir à la hiérarchie complète, on clique sur le symbole ```<``` à gauche du nom du ```Prefab``` dans la hiérarchie. 

### Dupliquer un Prefab et le modifier

- La duplication est simple : on sélectionne le ```Prefab``` dans le ```Project``` et on le déplace dans la fenêtre d'affichage ou dans la hiérarchie.
- **Attention** : le ```Prefab``` original n'est pas dupliqué dans le ```Project``` : une seconde instance de ce ```Prefab``` a simplement été créée.
    - Cela implique que toute modification d'une instance se répercute automatiquement sur les autres (en d'autres termes, les instances partagent les mêmes attributs. 
    - Par exemple, modifier le nom *x* à côté d'un slider en *y* sur l'une des instances et noter la modification de l'autre.
    - En revanche, les scripts fonctionnent de manière indépendante entre les instances, comme s'il s'agissait de ```GameObject``` différents.

Si l'on veut modifier un attribut d'un ```Prefab```, on peut sélectionner ce dernier dans le ```Project```, puis faire un copier-coller. Un ```Prefab``` identique au précédent est créé (avec un nouveau nom) et on peut modifier ses attributs, indépendamment du ```Prefab``` original.


## Export

- Sélectionner un ```Prefab``` dans l'onglet ```Project```
    - clique droit : sélectionner ```Export...``` et ne cocher que les dépendances nécecessaires:
        - le ```Prefab``` lui-même
        - le script *ComplexSliderScript.cs*
    - puis valider l'export en précisant son nom, par exemple *MyPrefabComplexSlider* : un fichier du nom *MyPrefabComplexSlider.unitypackage* est créé.


## Importer 

- Créer un nouveau projet ```Unity``` 
- Dans le menu principal, cliquer sur ```Assets > Import Package > Custom Package...```
- Sélectionner *MyPrefabCmplexslider.unitypackage*
- On peut sélectionner tout ou partie du *package*
- Cliquer sur ```Import``` après la sélection

Un ```GameObject``` nommé *PanelComplexSlider1* apparaît dans la scène.
- Il n'est pas directement visible
    - Créer un ```Canvas``` est intégrer *PanelComplexSlider1* comme son enfant
    - *PanelComplesSlider* est un objet 3D ! Modifier sa position pour le "coller" au ```Canvas``` (```Rect Transform > Pos Z = 0```)

Passer dans le ```Game View``` et tester le ```ComplexSlider```

## Multi-résolution

Source [https://docs.unity3d.com/2020.1/Documentation/Manual/HOWTO-UIMultiResolution.html](https://docs.unity3d.com/2020.1/Documentation/Manual/HOWTO-UIMultiResolution.html)

- Sélectionner le ```Canvas``` contenant les widgets
- Composant ```Canvas Scaler```: tester les valeurs des propriétés ```UI Scale Mode```, ```Scale Factor```, ```Screen Match Mode```et ```Match```
- Dans le ```GameView```
    - modifier la résolution de l'image pour vérifier que les éléments du ```Canvas``` restent bien positionnés (si leur ancrage est bien défini)
    - créer  une résolution personnalisée en cliquant sur le menu à côté de ```Display 1``` puis sur le bouton ```+```
    - imiter un mode *Portrait* avec une largeur inférieure à la hauteur
    
Avec la propriété ```Canvas Scaler > UI Scale Mode = Scale With Screen Size```, tous les widgets sont redimensionnés correctement.

Lorsque l'on change de résolution, le facteur de redimensionnement appliqué aux widgets peut être affiné avec la propriété ```Screen Match Mode = Match Width or Height``` :
- Si ```Match = 0``` (resp. ```Match = 1```), le facteur tient uniquement compte du redimensionnement de la largeur (resp. la hauteur). Une valeur de *0,5* détermine un facteur calculé de manière égale sur les deux dimensions.


## Modifier l'angle de vue du Canvas

Source [https://docs.unity3d.com/2020.1/Documentation/Manual/UICanvas.html](https://docs.unity3d.com/2020.1/Documentation/Manual/UICanvas.html)

On veut faire pivoter le ```Canvas``` pour obtenir le même effet que celui montré dans l'hyperlien.

- Sélectionner le ```Canvas``` dans la *hiérarchie*
- Composant ```Canvas```
    - Propriété ```Render Mode = World Space``` => ```Unity``` se plaint si aucune caméra n'est associée à ce mode
    - Propriété ```Event Camera``` : faire glisser une des caméras de la scène (par exemple ```Main Camera``` dans cette propriété
- Composant ```Rect Transform```
    - ```Pos Z``` : modifier cette valeur pour éloigner le ```Canvas``` de la caméra (il est à la même profondeur sinon)
    - ```Rotation``` : modifier la valeur sur chacun des axes pour observer différents effets

Remarques : 
- On peut modifier la rotation de chaque enfant du ```Canvas``` indépendamment des autres
- Si on utilise la valeur ```Canvas > Render Mode = Screen Space - Camera``` (toujours avec la caméra associé au ```Canvas```), le positionnement du ```Canvas``` (*i.e.* composant ```Rect Transform```) est désactivé. Mais on peut toujours modifier le positionnement de ses enfants.






















 