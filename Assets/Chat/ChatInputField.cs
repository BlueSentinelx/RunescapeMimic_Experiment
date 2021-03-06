using UnityEngine;
using UnityEngine.UI;
public class ChatInputField : MonoBehaviour {

	public ChatterManager chatManager;
	private InputField inputField;

	private void Start()
	{
		inputField = GetComponent<InputField> ();
	}

	public void ValuedChanged()
	{
		if (inputField.text.Contains("\n"))
		{
			chatManager.WriteMessage (inputField);
		}
	}
}
