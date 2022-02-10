using System;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;
using UnityEngine.UI;

public class ChatterManager : ChatterManagerBehavior
{
	public Transform chatContent;
	public GameObject chatMessage;

	private string username;

	protected override void NetworkStart()
	{
		base.NetworkStart ();

		if(networkObject.IsServer)
			username = "Server";
		else
			username = "Client";
	}
	public void WriteMessage(InputField sender)
	{
		if (!string.IsNullOrEmpty (sender.text) && sender.text.Trim ().Length > 0) {
			sender.text = sender.text.Replace ("\r", string.Empty).Replace ("\n", string.Empty);
			networkObject.SendRpc (RPC_TRANSMIT_MESSAGE, Receivers.All, username, sender.text.Trim ());
			sender.text = string.Empty;
			sender.ActivateInputField ();
		}
	}

	public override void TransmitMessage(RpcArgs args)
	{
		string username = args.GetNext<string> ();
		string message = args.GetNext<string> ();

		if (string.IsNullOrEmpty (username) || string.IsNullOrEmpty (message)) {
			return;
		}
		GameObject newMessage = Instantiate (chatMessage, chatContent);
		Text content = newMessage.GetComponent<Text> ();

		if(username == "Server")
			content.text = string.Format (content.text,"<color=white>" + username + "</color>", "<color=#6ABAFC>" +  message + "</color>");
		else if (username == "Client")
			content.text = string.Format (content.text,"<color=white>" + username + "</color>", "<color=red>" +  message + "</color>");
	}
}