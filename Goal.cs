using UnityEngine;
using System.Collections;


public class Goal : MonoBehaviour {
	
	
	private	void OnTriggerEnter( Collider hitCollider) {

		GameObject	hitObject	= hitCollider.gameObject;
		if( null == hitObject.GetComponent<Player>()) { //부딪힌 애가 GetComponent를 했는데 Player가 없다면
			return; //실행을 안해
		}
        //만약 Player가 있다면 
		Game.SetStageClear(); //
	}
	
	
	
	
}
