using UnityEngine;
using System.Collections.Generic;

public class TransformTest
    : MonoBehaviour
{

	void Start()
    {
        string data = transform.CreateSaveString(false, true, true, true);
        Debug.Log("Start Data = " + data);

        transform.ResetLocal();
        transform.SetupFromSaveString(data, false, true, true, true);
        Debug.Log("Start Data = " + transform.CreateSaveString(false, true, true, true) );
	}
}
