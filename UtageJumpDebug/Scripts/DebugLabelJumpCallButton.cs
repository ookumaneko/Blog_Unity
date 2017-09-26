using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLabelJumpCallButton 
	: MonoBehaviour
{
    [SerializeField]
    GameObject m_debugJump = null;

    public void OnSelected()
    {
        //m_debugJump.SetActive(true);
        Instantiate(m_debugJump); //, m_attachTarget, false);
    }
}
