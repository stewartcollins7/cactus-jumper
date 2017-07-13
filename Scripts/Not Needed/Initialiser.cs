using UnityEngine;
using System.Collections;

public class Initialiser : MonoBehaviour {

    InitialisationInfo info;

    public InitialisationInfo getInitialisationInfo()
    {
        return info;
    }

    public void updateInitialisationInfo(InitialisationInfo info)
    {
        this.info = info;
    }

	void Awake () {
        info = new InitialisationInfo();
	}
}
