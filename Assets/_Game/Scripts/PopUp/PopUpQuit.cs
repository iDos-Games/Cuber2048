public class PopUpQuit : PopUp
{
	public void Quit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		UnityEngine.Application.Quit();
#endif
	}
}
