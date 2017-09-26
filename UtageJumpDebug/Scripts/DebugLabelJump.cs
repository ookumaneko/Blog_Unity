using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utage;

public class DebugLabelJump 
	: MonoBehaviour
{
    AdvEngine Engine { get { return m_engine ?? (m_engine = FindObjectOfType<AdvEngine>()); } }
    AdvEngine m_engine = null;

    [SerializeField]
    DebugLabelJumpButton m_buttonPrefab = null;

    [SerializeField]
    RectTransform m_buttonRoot = null;

    [SerializeField]
    int m_numberOfRows = 7;

    [SerializeField]
    int m_numberOfColumns = 5;

    [SerializeField]
    Vector2 m_buttonSpacing = new Vector2(-138, -86);

    [SerializeField]
    GameObject m_nextButton = null;

    [SerializeField]
    GameObject m_backButton = null;

    [SerializeField]
    bool m_isLogAllLables = true;

    List<string> m_labels = null;
    List<DebugLabelJumpButton> m_buttons = null;
    int m_currentPage;

    public int ButtonsPerPage { get { return (m_numberOfColumns * m_numberOfRows); } }

    void Start()
	{
        CreateLabelList();
        CreateButtons();
        OpenPage(0);
    }

    private void CreateLabelList()
    {
        m_labels = new List<string>();

        foreach (AdvScenarioData data in Engine.DataManager.ScenarioDataTbl.Values)
        {
            foreach (AdvScenarioLabelData labelData in data.ScenarioLabels.Values)
            {
                m_labels.Add(labelData.ScenarioLabel);
            }
        }

        if (!m_isLogAllLables)
        {
            return;
        }

        int count = m_labels.Count;
        Debug.Log("label Count = " + count);
        for (int i = 0; i < count; ++i)
        {
            Debug.Log("label " + i + " = " + m_labels[i]);
        }
    }

    private void CreateButtons()
    {
        m_buttons = new List<DebugLabelJumpButton>();
        for (int y = 0; y < m_numberOfRows; ++y)
        {
            for (int x = 0; x < m_numberOfColumns; ++x)
            {
                CreateButton(x, y);
            }
        }
    }

    private void CreateButton(int x, int y)
    {
        DebugLabelJumpButton button = Instantiate(m_buttonPrefab);
        Transform buttonTransform = button.transform;
        buttonTransform.SetParent(m_buttonRoot);
        buttonTransform.localPosition = new Vector3(x * m_buttonSpacing.x, y * m_buttonSpacing.y, 0.0f);
        buttonTransform.localScale = Vector3.one;
        m_buttons.Add(button);
    }

    public void OpenPage(int page)
    {
        m_currentPage = page;

        int count = m_buttons.Count;
        int startIndex = count * page;
        int labelCount = m_labels.Count;

        for (int i = 0; i < count; ++i)
        {
            int index = startIndex + i;
            if (index >= labelCount)
            {
                m_buttons[i].gameObject.SetActive(false);
                continue;
            }

            m_buttons[i].gameObject.SetActive(true);
            m_buttons[i].Setup( this, Engine, m_labels[index] );
        }

        SetupPageChangeButtons();
    }

    private void SetupPageChangeButtons()
    {
        m_backButton.SetActive((m_currentPage > 0));
        int maxPage = Mathf.FloorToInt( m_labels.Count / m_buttons.Count );
        m_nextButton.SetActive((m_currentPage < maxPage));
    }

    public void ChangePage(int changeAmount)
    {
        m_currentPage += changeAmount;
        OpenPage(m_currentPage);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
