using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDebug : MonoBehaviour {

    public Player PlayerScript;

    public InputField VelocityLabel;

    private void Update()
    {
        VelocityLabel.text = PlayerScript.GetVelocity.ToString();
    }

}
