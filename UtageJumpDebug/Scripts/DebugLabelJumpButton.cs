using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utage;

public class DebugLabelJumpButton 
	: MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Text m_text = null;

    DebugLabelJump m_owner;
    AdvEngine m_engine = null;

    public void Setup(DebugLabelJump owner, AdvEngine engine, string label)
    {
        m_owner = owner;
        m_text.text = label;
        m_engine = engine;
    }

    public void OnSelected()
    {
        m_engine.JumpScenario(m_text.text);
        m_engine.Page.InputSendMessage();
        Destroy(m_owner.gameObject);
        System.GC.Collect();
    }
}
