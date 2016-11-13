using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SpellType
{
  Ar,
  Agua,
  Terra,
  Fogo,
  //Espirito,
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
  public float timeOff;

  public int hordeCount;

  public int maxSpellCount;
  public int maxHordeSpellCount;

  public float timePunishment;

  public bool showSpellHelp;
}

[Serializable]
public class SpellTypeWeight
{
  public SpellType spellType;
  public float weight;
}

public class GameController : MonoBehaviour
{
  #region public variables

  public GameObject m_spellArrowUp = null;
  public GameObject m_spellArrowDown = null;
  public GameObject m_spellArrowLeft = null;
  public GameObject m_spellArrowRight = null;

  public Text m_textSouls = null;
  public Text m_textTimer = null;

  public float m_roundTime = 0.0f;
  public GameRules[] m_gamerules = null;

  public SpellTypeWeight[] m_spellTypeWeight = new SpellTypeWeight[sizeof(SpellType)];

  public AudioSource m_spellRightSound = null;
  public AudioSource m_spellFailSound = null;

  [NonSerialized]
  public SpellCombo m_currentSpell = null;

  [NonSerialized]
  public GameRules m_currentGR = null;

  #endregion public variables

  #region private variables

  private float m_currentRoundTime = 0.0f;
  private float m_currentTimeOff = 0.0f;

  private int m_currentScore = 0;
  private int m_currentDifficulty = 0;

  private float m_spellTypeTotalWeight = 0.0f;

  private int m_rightCounter = 0;
  private int m_wrongCounter = 0;

  #endregion private variables

  #region public methods

  public void AddTimePenalty()
  {
    Debug.Log("AddTimePenalty");

    if (m_spellFailSound != null)
      m_spellFailSound.Play();

    m_rightCounter = 0;
    m_wrongCounter++;
    if (m_wrongCounter > 5)
    {
      SetLowerDifficulty();
    }

    if (m_currentGR != null)
    {
      m_currentRoundTime -= m_currentGR.timePunishment;
    }

    SetTimeOff();

    m_currentSpell = null;
  }

  public void AddScore()
  {
    Debug.Log("AddScore");

    if (m_spellRightSound != null)
      m_spellRightSound.Play();

    m_wrongCounter = 0;
    m_rightCounter++;
    if (m_rightCounter > 5)
    {
      SetHigherDifficulty();
    }

    m_currentScore++;

    SetTimeOff();

    m_currentSpell = null;

    m_textSouls.text = m_currentScore.ToString();
  }

  public void SetLowerDifficulty()
  {
    if (m_currentDifficulty > 0)
      m_currentDifficulty--;

    m_currentGR = m_gamerules[m_currentDifficulty];
  }

  public void SetHigherDifficulty()
  {
    if (m_currentDifficulty < m_gamerules.Length - 1)
      m_currentDifficulty++;

    m_currentGR = m_gamerules[m_currentDifficulty];
  }

  #endregion public methods

  #region private methods

  private void SetTimeOff()
  {
    if (m_currentGR != null)
    {
      m_currentTimeOff = Time.time + m_currentGR.timeOff;
    }
  }

  // Use this for initialization
  private void Start()
  {
    // Get the total weight for calculating random type of spell later.
    foreach (SpellTypeWeight spell in m_spellTypeWeight)
    {
      m_spellTypeTotalWeight += spell.weight;
    }

    InitRound();
  }

  // Update is called once per frame
  private void Update()
  {
    // Update time.
    m_currentRoundTime -= Time.deltaTime;
    m_currentRoundTime = Mathf.Clamp(m_currentRoundTime, 0.0f, m_roundTime);

    // Update round logic.
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

    m_textTimer.text = m_roundTime.ToString();

    SetTimeOff();
  }

  private void EndRound()
  {
    //TODO.
    m_currentSpell = null;

    Debug.Log("EndRound");
  }

  private void RunRound()
  {
    if (m_currentGR != null)
    {
      // Update UI time.
      m_textTimer.text = ((int)m_currentRoundTime).ToString();

      // Give player some rest between spells!
      if (Time.time > m_currentTimeOff)
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

      //m_currentSpell.spellType = (SpellType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(SpellType)).Length);
      m_currentSpell.spellType = RandomNewSpellType();

      //if (m_currentSpell.spellType != SpellType.Espirito)
      {
        for (int i = 0; i < m_currentGR.maxSpellCount; i++)
        {
          float result = UnityEngine.Random.Range(0.0f, 99.0f);

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

  private SpellType RandomNewSpellType()
  {
    SpellType result = SpellType.Ar;
    float currentWeight = 0.0f;

    float randomWeight = UnityEngine.Random.Range(0.0f, m_spellTypeTotalWeight);

    foreach (SpellTypeWeight spell in m_spellTypeWeight)
    {
      currentWeight += spell.weight;
      if (randomWeight <= currentWeight)
      {
        result = spell.spellType;
        break;
      }
    }

    return result;
  }

  #endregion private methods
}
