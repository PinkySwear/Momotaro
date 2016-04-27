using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor.Audio;

	public class RealLevelManager : MonoBehaviour {

		bool fadeOut;

		public int targetLevel;

		public AudioSource audioPlayer;

		void Start(){
	
		}

		public void LoadPrevious(){
			Debug.Log("Level load requested for " + name); 
			SceneManager.LoadScene ((SceneManager.GetActiveScene().buildIndex - 1));
		}

		public void Restart(){
			Debug.Log("Restart!"); 
			SceneManager.LoadScene ((SceneManager.GetActiveScene().buildIndex));
		}

		public void Begin(){
			Debug.Log("begin");
			fadeOut = true;
			StartCoroutine(StartGame(1.2f));
		}

		public void creditScreen(){
			SceneManager.LoadScene ("Credits");
		}

		void Update() {
			if (!fadeOut) return;
			audioPlayer.volume = Mathf.Lerp(
			audioPlayer.volume, 0f, Time.deltaTime*2f);
		}
			

		public IEnumerator StartGame(float delay) {
			yield return new WaitForSeconds(delay);
			SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1));
		}
		
		public void QuitLevel()	{
			Debug.Log("Quit requested");
			Application.Quit();
		}

		public void LoseScreen(){
			Debug.Log("Level load requested for " + name); 
			SceneManager.LoadScene ("Lose_Scene");
		}
		
		public void LoadMenu(){
			SceneManager.LoadScene(0);
		}
		
	}