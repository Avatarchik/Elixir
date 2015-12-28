using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour {

	public int currentIndex;
	Text text;

	void ApplyDialogue(int currentIndex)
	{
		text.text = GetText(currentIndex);
	}

	// Use this for initialization
	void Start () {
		currentIndex = 0;
		text = GameObject.Find("DialogueText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		ApplyDialogue(currentIndex);
	}
	
	void OnMouseDown()
	{
		if (currentIndex < 4)
			currentIndex ++;
	}
	
	string GetText(int currentIndex)
	{
		switch (currentIndex)
		{
			case 0 : return 
			"오키드\n\n" +
			"지금부터 내가 말하는 건 테스트.\n" +
			"자, 이제 뭘 해볼까?\n" +
			"아마 글자는 네 줄 까지 들어가는 것 같은데...\n" +
			"지금의 폰트 사이즈는 25."; 
			case 1 : return
			"오키드\n\n" +
			"아참, 회장님이 계셨군요?\n" + 
			"이쪽으로 와보세요."; 
			case 2 : return
			"전회장\n\n" +
			"안녕! 오래간만이야.\n" + 
			"사실 회장을 넘긴 이후로 여긴 와본적이 없었는데 간만에 들러봤어.\n" + 
			"여전히 열심히 개발중이구나?\n";
			case 3 : return
			"전회장\n\n" +
			"아, 대사가 잘 나오는지 테스트 하는 중이구나.\n" + 
			"이렇게 하면 되나? 아니면 이렇게? 이렇게?";
            case 4 : return
            "오키드\n\n" + 
            "이제 괜찮아요. 인덱스를 더 늘어나지 못하도록 해놨으니까.";
			default:
			return
			"오키드\n\n" + 
			"여기로 왔다는 건 적절하지 못한 인덱스를 넣었다는 뜻이지.";
		}
	}
}
