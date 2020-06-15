using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetDummyBehaviour : MonoBehaviour
{
	public Canvas        canvas;
	public Slider        hp;
    public GameObject    TEMP_shilde;
    public CharacterInfo dummyStats;
	private Slider       characterHp;
	public Camera        cam;
	public Vector3       camOffset;
	private float        temp;

    private GameObject TEMP_character_shilde;

    public bool blocking = false;

    private void OnEnable()
	{
    
        characterHp = Instantiate(hp, transform.position, Quaternion.identity);
        TEMP_character_shilde = Instantiate(TEMP_shilde, transform.position, Quaternion.identity);


        characterHp.transform.SetParent(canvas.transform);

        characterHp.maxValue = dummyStats.HP;
		characterHp.value    = characterHp.maxValue;
		dummyStats.currentHP = (int)characterHp.value;
		temp                 = characterHp.value;
	}

	private void Update()
	{
        TEMP_character_shilde.transform.position = transform.position;
        TEMP_character_shilde.SetActive(blocking);

        characterHp.transform.position = cam.WorldToScreenPoint(transform.position + camOffset);
		if(dummyStats.currentHP != temp)
		{
			TakeDmg(10);
		}
	}

	public void TakeDmg(int dmg)
	{
        if(!blocking)
		    characterHp.value -= dmg;
	}
}
