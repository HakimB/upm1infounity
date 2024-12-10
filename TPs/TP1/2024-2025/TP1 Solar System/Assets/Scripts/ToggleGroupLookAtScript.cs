using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class ToggleGroupLookAtScript : MonoBehaviour {
  ToggleGroup m_ToggleGroup;
  Toggle m_PreviousToggle;
  Toggle m_SelectedToggle;
	
  public Toggle GetCurrentSelection() {
  	return m_ToggleGroup.ActiveToggles().FirstOrDefault();
  }
	
  public Toggle GetPreviousSelection() {
	return m_PreviousToggle;
  }
	
  void Start() {
	m_ToggleGroup = GetComponent<ToggleGroup>();
	m_PreviousToggle = GetCurrentSelection();
	m_SelectedToggle = GetCurrentSelection();
	Debug.Log("[ToggleGroupLookAtScript][Start] m_PreviousToggle = " +
	m_PreviousToggle.name + ", m_SelectedToggle = " +
	m_SelectedToggle.name);
  }
	
  public GameObject GetGameObjectFromToggle(Toggle toggle) {
	switch(toggle.name) {
		case "ToggleSoleil":
			return GameObject.Find("Soleil");
		case "ToggleTerre":
			return GameObject.Find("Terre");
		case "ToggleMars":
			return GameObject.Find("Mars");
		case "ToggleOriginal":
			return GameObject.Find("Main Camera");
		default:
			return null;
	}
  }

  void Update() {
	if (GetCurrentSelection() != m_SelectedToggle) {
		m_PreviousToggle = m_SelectedToggle;
		m_SelectedToggle = GetCurrentSelection();

		Debug.Log("[ToggleGroupLookAtScript][Update] m_PreviousToggle = " +

		m_PreviousToggle.name + ", m_SelectedToggle = " +
		m_SelectedToggle.name);
	}
  }
}
