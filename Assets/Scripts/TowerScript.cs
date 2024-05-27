using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [SerializeField] private GameObject towerPiece;

    [SerializeField] private List<GameObject> towerList;

    [SerializeField] private int hpPerPart = 10;
    [SerializeField] private int hpNeedToUpgrade = 5;
    public float sizeIncrease = 0.5f;

    private int currentHp;

    public int amountOfPieces = 1;

    public void PlayerShotAt(int damage)
    {
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
            currentHp = hpPerPart;
            towerList.RemoveAt(amountOfPieces);
            amountOfPieces--;
            //transform.localScale = new Vector3(transform.localScale.x / 1.1f, transform.localScale.y,transform.localScale.z / 1.1f);
        }
    }
}
