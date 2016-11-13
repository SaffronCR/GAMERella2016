using System.Collections;
using UnityEngine;

public class RivalController : MonoBehaviour
{
  public GameController m_gameCtrl = null;

  public Transform[] m_spellPositions = null;

  public GameObject[] m_signs = null;
  public GameObject[] m_signsHelp = null;

  public AudioSource m_spellSound = null;

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
      if (m_gameCtrl.m_currentSpell == null)
      {
        // Hide all signs.
        foreach (GameObject sign in m_signs)
        {
          sign.SetActive(false);
        }

        foreach (GameObject sign in m_signsHelp)
        {
          sign.SetActive(false);
        }

        // Delete previous spell arrows.
        foreach (Transform position in m_spellPositions)
        {
          foreach (Transform child in position.transform)
          {
            GameObject.Destroy(child.gameObject);
          }
        }

        // Prevent any spell arrow from showing.
        StopAllCoroutines();

        // Stop spell sound.
        if (m_spellSound != null)
          m_spellSound.Stop();
      }

      if (m_gameCtrl.m_currentSpell != null && m_currentSpell != m_gameCtrl.m_currentSpell)
      {
        // Initialize spell arrows variables.
        m_currentSpellIndex = 0;
        m_currentSpell = m_gameCtrl.m_currentSpell;

        // Set active current sign.
        m_signs[(int)m_currentSpell.spellType].SetActive(true);

        // Set active current sign help, if the current game rules allows it.
        if (m_gameCtrl.m_currentGR.showSpellHelp)
          m_signsHelp[(int)m_currentSpell.spellType].SetActive(true);

        // Play spell sound.
        if (m_spellSound != null)
          m_spellSound.Play();
      }

      // Draw spell.
      while (m_currentSpell != null && m_currentSpellIndex < m_currentSpell.keyCodes.Count)
      {
        IEnumerator coroutine = null;

        switch (m_currentSpell.keyCodes[m_currentSpellIndex])
        {
          case KeyCode.UpArrow:
            coroutine = CreateArrow(m_gameCtrl.m_spellArrowUp, m_currentSpellIndex);
            break;

          case KeyCode.DownArrow:
            coroutine = CreateArrow(m_gameCtrl.m_spellArrowDown, m_currentSpellIndex);
            break;

          case KeyCode.LeftArrow:
            coroutine = CreateArrow(m_gameCtrl.m_spellArrowLeft, m_currentSpellIndex);
            break;

          case KeyCode.RightArrow:
            coroutine = CreateArrow(m_gameCtrl.m_spellArrowRight, m_currentSpellIndex);
            break;
        }

        if (coroutine != null)
          StartCoroutine(coroutine);

        m_currentSpellIndex++;
      }
    }
  }

  private IEnumerator CreateArrow(GameObject go, int index)
  {
    yield return new WaitForSeconds(index * 0.4f);

    Instantiate(go, m_spellPositions[index], false);
  }
}
