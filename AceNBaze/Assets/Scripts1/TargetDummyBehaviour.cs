using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetDummyBehaviour : MonoBehaviour
{
	public Canvas canvas;
	public Slider hp;
	public CharacterInfo dummyStats;
	private Slider characterHp;
	public Camera cam;
	public Vector3 camOffset;
	private void OnEnable()
	{
		characterHp = Instantiate(hp, transform.position, Quaternion.identity);
		characterHp.transform.SetParent(canvas.transform);
		characterHp.maxValue = dummyStats.HP;
		characterHp.value = characterHp.maxValue;
	}

	private void Update()
	{
		characterHp.transform.position = cam.WorldToScreenPoint(transform.position + camOffset);
	}

	public void TakeDmg(int dmg)
	{
		characterHp.value -= dmg;
	}
}
