using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerScript : MonoBehaviour
{
    [SerializeField] private GameObject towerPiece;

    [SerializeField] private List<GameObject> towerList;

    [SerializeField] private int hpPerPart = 10;
    [SerializeField] private int hpNeedToUpgrade = 5;

    [SerializeField] private Material standard;
    [SerializeField] private Material damaged;
    public float sizeIncrease = 0.5f;

    private int currentHp;

    public int amountOfPieces = 1;

    private void Start()
    {
        currentHp = hpPerPart;
    }

    public void PlayerShotAt(int damage)
    {
        if (currentHp >= hpPerPart && amountOfPieces > 1)
        {
            towerList[amountOfPieces -1].GetComponent<MeshRenderer>().material = standard;
        }
        currentHp += damage;
        if (currentHp > hpPerPart + hpNeedToUpgrade)
        {
            foreach (GameObject piece in towerList)
            {
                piece.transform.localScale = new Vector3( piece.transform.localScale.x + sizeIncrease, piece.transform.localScale.y,
                    piece.transform.localScale.z + sizeIncrease);
            }

            GameObject newPiece = Instantiate(towerPiece,this.transform);
            newPiece.transform.position = (this.transform.position + new Vector3(0, amountOfPieces, 0));
            currentHp = hpPerPart;
            amountOfPieces++;
            towerList.Add(newPiece);
        }
    }

    public void EnemyAttack(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            if (amountOfPieces <= 0)
            {
                SceneManager.LoadScene("Lose");
                return;
            }
            currentHp = hpPerPart;
            GameObject objectToKill = towerList[amountOfPieces -1];
            towerList.RemoveAt(amountOfPieces -1);
            Destroy(objectToKill);
            amountOfPieces--;
            //transform.localScale = new Vector3(transform.localScale.x / 1.1f, transform.localScale.y,transform.localScale.z / 1.1f);
        }
        else if(amountOfPieces > 1)
        {
            towerList[amountOfPieces].GetComponent<MeshRenderer>().material = damaged;
        }
    }
}
