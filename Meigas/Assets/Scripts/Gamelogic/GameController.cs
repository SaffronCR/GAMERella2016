using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellType
{
  Espirito,

  //  Ar,
  Agua,

  Terra,
  Fogo,
}

public enum Difficulty
{
  Easy,
  Normal,
  Hard
}

public class SpellCombo
{
  public List<KeyCode> keyCodes = new List<KeyCode>();
  public SpellType spellType;
  public float initialTime;
}

[Serializable]
public class GameRules
{
  public Difficulty difficulty;

  public float timeToCastSpell;
  public float timeToCastHordeSpell;

  public int hordeCount;

  public int maxSpellCount;
  public int maxHordeSpellCount;

  public float timePunishment;

  public bool showSpellHelp;
}

public class GameController : MonoBehaviour
{
  #region public variables

  public PlayerController m_playerCtrl = null;
  public RivalController m_rivalCtrl = null;

  public float m_roundTime = 0.0f;
  public GameRules[] m_gamerules = null;

  [NonSerialized]
  public SpellCombo m_currentSpell = null;

  #endregion public variables

  #region private variables

  private float m_currentRoundTime = 0.0f;
  private GameRules m_currentGR = null;
  private int m_currentScore = 0;

  private int m_currentDifficulty = 0;

  #endregion private variables

  #region public methods

  public void AddTimePenalty()
  {
    Debug.Log("AddTimePenalty");

    if (m_currentGR != null)
    {
      m_currentRoundTime -= m_currentGR.timePunishment;
    }

    m_currentSpell = null;
  }

  public void AddScore()
  {
    Debug.Log("AddScore");

    m_currentScore++;

    m_currentSpell = null;
  }

  public void SetLowerDifficulty()
  {
    if (m_currentDifficulty > 0)
      m_currentDifficulty--;
  }

  public void SetHigherDifficulty()
  {
    if (m_currentDifficulty < m_gamerules.Length - 1)
      m_currentDifficulty++;
  }

  #endregion public methods

  #region private methods

  // Use this for initialization
  private void Start()
  {
    InitRound();
  }

  // Update is called once per frame
  private void Update()
  {
    // Update time.
    m_currentRoundTime -= Time.deltaTime;

    if (m_currentRoundTime <= 0.0f)
    {
      EndRound();
    }
    else
    {
      RunRound();
    }
  }

  private void InitRound()
  {
    Debug.Log("InitRound");

    m_currentRoundTime = Time.time + m_roundTime;
    m_currentDifficulty = 0;
    m_currentScore = 0;

    if (m_gamerules.Length > m_currentDifficulty)
      m_currentGR = m_gamerules[m_currentDifficulty];
  }

  private void EndRound()
  {
    //TODO.
    Debug.Log("EndRound");
  }

  private void RunRound()
  {
    if (m_currentGR != null)
    {
      // Create spell the first time.
      if (m_currentSpell == null)
      {
        CreateNewSpell();
      }

      // Update spell.
      if (m_currentSpell != null)
      {
        if (m_currentSpell.initialTime + m_currentGR.timeToCastSpell <= Time.time)
        {
          AddTimePenalty();
        }
      }

      // Update many many things TODO.
    }
    else
    {
      Debug.LogError("m_currentGR is null!");

      EndRound();
    }
  }

  private void CreateNewSpell()
  {
    if (m_currentGR != null)
    {
      m_currentSpell = new SpellCombo();

      m_currentSpell.initialTime = Time.time;

      m_currentSpell.spellType = (SpellType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(SpellType)).Length);

      //if (m_currentSpell.spellType != SpellType.Ar)
      {
        for (int i = 0; i < m_currentGR.maxSpellCount; i++)
        {
          float result = UnityEngine.Random.Range(0.0f, 100.0f);

          if (result < 25.0f) m_currentSpell.keyCodes.Add(KeyCode.UpArrow);
          else if (result < 50.0f) m_currentSpell.keyCodes.Add(KeyCode.DownArrow);
          else if (result < 75.0f) m_currentSpell.keyCodes.Add(KeyCode.RightArrow);
          else m_currentSpell.keyCodes.Add(KeyCode.LeftArrow);
        }

        Debug.Log("Spell: " + m_currentSpell.spellType);
        foreach (KeyCode code in m_currentSpell.keyCodes)
        {
          Debug.Log("Key: " + code);
        }
      }
    }
  }

  #endregion private methods
}
