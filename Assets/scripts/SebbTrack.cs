using System;
using UnityEngine;
using System.Collections;

public class SebbTrack : MonoBehaviour
{
	private static SebbTrack instance;

	void Start() {
		instance = this;
		Track ("level_load", Application.loadedLevelName);
	}

	public static SebbTrack getInstance() {
		if (instance != null) {
			return instance;
		}

		instance = new SebbTrack ();
		return instance;
	}

	public static void Track (String slug, int value) {
		getInstance()._Track (slug, value.ToString());
	}

	public static void Track (String slug, String value) {
		getInstance()._Track (slug, value.ToString());
	}

	private string GetPrefID() {
		if (PlayerPrefs.GetString ("deviceUniqueIdentifier") == "") {
			PlayerPrefs.SetString ("deviceUniqueIdentifier", System.Guid.NewGuid ().ToString ());
		}

		return PlayerPrefs.GetString ("deviceUniqueIdentifier");
	}

	public void _Track (String slug, String value) {
		string id = "unkown";
		#if UNITY_WEBGL
			id = GetPrefID();
		#else
			id = SystemInfo.deviceUniqueIdentifier;
		#endif
		string url = "http://track.sebb.dk/points/add.json";

		WWWForm form = new WWWForm();
		form.AddField("data[Point][slug]", slug);
		form.AddField("data[Point][value]", value);
		form.AddField("data[Point][client_identifier]", id);
		form.AddField("data[Point][property_slug]", "ReverseDungeon");
		WWW www = new WWW(url, form);

		StartCoroutine("WaitForRequest", www);
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;

		// check for errors
		if (www.error == null) {
			//Debug.Log("WWW Ok!: " + www.data);
		} else {
			//Debug.Log("WWW Error: "+ www.error);
		}
	}   

}