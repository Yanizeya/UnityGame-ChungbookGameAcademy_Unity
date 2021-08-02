using UnityEngine;
using System.Collections;


public class Target : MonoBehaviour {
	public	GameObject	hitEffectPrefab	= null;			
	private	static	int	m_allTargetNum	= 0;				
	
	private	void Awake() { // '시작' 중 '시'할 때 하는게 Awake '작'끝나는 순간 Start 그 이후가 Update
		m_allTargetNum++;
	}
	
	private	void OnTriggerEnter( Collider hitCollider) {
		
		GameObject	hitObject = hitCollider.gameObject;

		if( null==hitObject.GetComponent<Bullet>()) {
			return;
		}
		if( null!=hitEffectPrefab) {
			Instantiate( hitEffectPrefab, transform.position, transform.rotation);
		}
		{
			m_allTargetNum--;
			if( m_allTargetNum <= 0) {
				Game.SetStageClear();
			}
		}
		
		Destroy( gameObject);
	}
	
	
	
	
}
