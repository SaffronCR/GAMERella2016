using System.Collections;
using UnityEngine;

public class RivalController : MonoBehaviour
{
  public GameController m_gameCtrl = null;

  public Transform[] m_spellPositions = null;

  public GameObject[] m_signs = null;

  private SpellCombo m_currentSpell = null;
  private int m_currentSpellIndex = 0;

  // Use this for initialization
  private void Start()
  {
  }

  // Update is called once per frame
  private void Update()
  {
    if (m_gameCtrl != null)
    {
      // Update current spell.
      if (m_gameCtrl.m_currentSpell != null && m_currentSpell != m_gameCtrl.m_currentSpell)
      {
        // Initialize spell arrows variables.
        m_currentSpellIndex = 0;
        m_currentSpell = m_gameCtrl.m_currentSpell;

        // Hide all signs.
        foreach (GameObject sign in m_signs)
        {
          sign.SetActive(false);
        }

        // Set active current sign.
        m_signs[(int)m_currentSpell.spellType].SetActive(true);

        // Delete previous spell arrows.
        foreach (Transform position in m_spellPositions)
        {
          foreach (Transform child in position.transform)
          {
            GameObject.Destroy(child.gameObject);
          }
        }
      }

      // Draw spell.
      if (m_currentSpell != null && m_currentSpellIndex < m_currentSpell.keyCodes.Count)
      {
        switch (m_currentSpell.keyCodes[m_currentSpellIndex])
        {
          case KeyCode.UpArrow:
            Instantiate(m_gameCtrl.m_spellArrowUp, m_spellPositions[m_currentSpellIndex], false);
            break;

          case KeyCode.DownArrow:
            Instantiate(m_gameCtrl.m_spellArrowDown, m_spellPositions[m_currentSpellIndex], false);
            break;

          case KeyCode.LeftArrow:
            Instantiate(m_gameCtrl.m_spellArrowLeft, m_spellPositions[m_currentSpellIndex], false);
            break;

          case KeyCode.RightArrow:
            Instantiate(m_gameCtrl.m_spellArrowRight, m_spellPositions[m_currentSpellIndex], false);
            break;
        }

        m_currentSpellIndex++;
      }
    }
  }
}
