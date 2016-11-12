using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public GameController m_gameCtrl = null;

  private int m_currentKeyIndex = 0;

  // Use this for initialization
  private void Start()
  {
  }

  // Update is called once per frame
  private void Update()
  {
    CheckCurrentSpell();
  }

  private void CheckCurrentSpell()
  {
    if (m_gameCtrl != null && m_gameCtrl.m_currentSpell != null)
    {
      if (IsCorrectKeyDown())
      {
        m_currentKeyIndex++;

        if (m_currentKeyIndex >= m_gameCtrl.m_currentSpell.keyCodes.Count)
        {
          m_currentKeyIndex = 0;
          m_gameCtrl.AddScore();
        }
      }
      else if (AnyKeyDown())
      {
        m_gameCtrl.AddTimePenalty();
      }
    }
    else
    {
      m_currentKeyIndex = 0;
    }
  }

  private bool IsCorrectKeyDown()
  {
    bool result = false;

    switch (m_gameCtrl.m_currentSpell.spellType)
    {
      // Repeat.
      case SpellType.Ar:
        result = Input.GetKeyDown(m_gameCtrl.m_currentSpell.keyCodes[m_currentKeyIndex]);
        break;

      // Mirror.
      case SpellType.Terra:
        result = Input.GetKeyDown(m_gameCtrl.m_currentSpell.keyCodes[m_gameCtrl.m_currentSpell.keyCodes.Count - m_currentKeyIndex - 1]);
        break;

      // Opposite.
      case SpellType.Agua:
        result = GetOppositeKeyDown(m_gameCtrl.m_currentSpell.keyCodes[m_currentKeyIndex]);
        break;

      // Mirror+Opposite
      case SpellType.Fogo:
        result = GetOppositeKeyDown(m_gameCtrl.m_currentSpell.keyCodes[m_gameCtrl.m_currentSpell.keyCodes.Count - m_currentKeyIndex - 1]);
        break;
    }

    return result;
  }

  private bool GetOppositeKeyDown(KeyCode code)
  {
    bool result = false;

    if (code == KeyCode.UpArrow) result = Input.GetKeyDown(KeyCode.DownArrow);
    else if (code == KeyCode.DownArrow) result = Input.GetKeyDown(KeyCode.UpArrow);
    else if (code == KeyCode.LeftArrow) result = Input.GetKeyDown(KeyCode.RightArrow);
    else result = Input.GetKeyDown(KeyCode.LeftArrow);

    return result;
  }

  private bool AnyKeyDown()
  {
    return (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) ||
      Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow));
  }
}
