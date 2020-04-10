using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    [SerializeField] bool isUnlocked = false;
    [SerializeField] bool isBuyed = false;
    [SerializeField] GameObject[] subSkills;
    [SerializeField] int cost = 1;
    private Button button;
    private GameObject skillTree;
    private ScoreManager scoreManager;

    // REQUEST //
    [SerializeField] int Skill_ID;
    [SerializeField] int value;

    void Start()
    {
        button = this.GetComponent<Button>();
        skillTree = GameObject.FindGameObjectWithTag("SkillTree");
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        ManageButtonActivation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ManageButtonActivation()
    {
        button.interactable = isUnlocked;
    }

    void Unlock()
    {
        isUnlocked = true;
        ManageButtonActivation();
    }

    public void Buy()
    {
        if(scoreManager.getActualCash() >= cost)
        {
            scoreManager.removeCash(cost);
            isBuyed = true;
            skillTree.GetComponent<SkillsRoutines>().ManageRequest(Skill_ID,value);

            for (int i = 0; i< subSkills.Length; i++)
            {
                subSkills[i].GetComponent<Skill>().Unlock();
            }
            button.interactable = false;
        }
        else
        {
            Debug.Log("not enough cash");
        }
    }
}
