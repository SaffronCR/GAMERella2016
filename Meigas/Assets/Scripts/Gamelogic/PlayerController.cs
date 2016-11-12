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
      if (Input.GetKeyDown(m_gameCtrl.m_currentSpell.keyCodes[m_currentKeyIndex]))
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
  }

  private bool AnyKeyDown()
  {
    return (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) ||
      Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow));
  }
}
