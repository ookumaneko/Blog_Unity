using UnityEngine;
using System.Collections.Generic;

public class TransformTest
    : MonoBehaviour
{

	void Start()
    {
        string data = transform.CreateSaveString(true, true, true, true);
        Debug.Log("Start Data = " + data);

        transform.ResetLocal();
        transform.SetupFromSaveString(data, true, true, true, true);
        Debug.Log("Start Data = " + transform.CreateSaveString(false, true, true, true) );
	}
}
