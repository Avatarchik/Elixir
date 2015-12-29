using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

enum PortraitPosition
{
    None,
    Left,
    Right
}

class Dialogue
{
    string portraitPath;
    PortraitPosition portraitPosition;
    string name;
    string dialogue;
    
    public Dialogue()
    {
        portraitPath = "";
        portraitPosition = PortraitPosition.None;
        name = "???";
        dialogue = "......";
    }
    
    public Dialogue(string portraitPath, PortraitPosition portraitPosition, string name, string dialogue)
    {
        this.portraitPath = portraitPath;
        this.portraitPosition = portraitPosition;
        this.name = name;
        this.dialogue = dialogue;
    }
    
    public string GetPortraitPath()
    {
        return "DialogueImages/" + portraitPath;
    }
    
    public PortraitPosition GetPortraitPosition()
    {
        return portraitPosition;
    }
    
    public string GetName()
    {
        return name;
    }
    
    public string GetDialogue()
    {
        return dialogue;
    }
}

public class DialogueManager : MonoBehaviour {

    public SpriteRenderer LeftPortraitRenderer;
    public SpriteRenderer RightPortraitRenderer;
	public int currentIndex;
	Text text;
    List<Dialogue> dialogues;
    bool isUpdated;

    // temp method.
    void SetDialogue()
    {
        dialogues = new List<Dialogue>();
        
        string str = "지금부터 내가 말하는 건 테스트.\n" +
			"자, 이제 뭘 해볼까?\n" +
			"아마 글자는 네 줄 까지 들어가는 것 같은데...\n" +
			"지금의 폰트 사이즈는 25."; 
        Dialogue newDialogue = new Dialogue("orchid", PortraitPosition.Left, "오키드", str);
        dialogues.Add(newDialogue);
        
        str = "아참, 회장님이 계셨군요?\n" + 
			"이쪽으로 와보세요."; 
        newDialogue = new Dialogue("orchid", PortraitPosition.Left, "오키드", str);
        dialogues.Add(newDialogue);
        
        str = "안녕! 오래간만이야.\n" + 
			"사실 회장을 넘긴 이후로 여긴 와본적이 없었는데 간만에 들러봤어.\n" + 
			"여전히 열심히 개발중이구나?\n";
        newDialogue = new Dialogue("jhj", PortraitPosition.Right, "전회장", str);
        dialogues.Add(newDialogue);
        
        str = "아, 대사가 잘 나오는지 테스트 하는 중이구나.\n" + 
			"이렇게 하면 되나? 아니면 이렇게? 이렇게?";
        newDialogue = new Dialogue("jhj", PortraitPosition.Right, "전회장", str);
        dialogues.Add(newDialogue);
        
        str = "이제 괜찮아요. 리스트에 들어가있는 대사들은 이게 끝이니까.";
        newDialogue = new Dialogue("orchid", PortraitPosition.Left, "오키드", str);
        dialogues.Add(newDialogue);
    }

    void ApplyDialogueImage(Dialogue currentDialogue)
    {
        if (currentDialogue.GetPortraitPosition() == PortraitPosition.Left)
        {
            LeftPortraitRenderer.sprite = Resources.Load(currentDialogue.GetPortraitPath(), typeof(Sprite)) as Sprite;
        }
        else if (currentDialogue.GetPortraitPosition() == PortraitPosition.Right)
        {
            RightPortraitRenderer.sprite = Resources.Load(currentDialogue.GetPortraitPath(), typeof(Sprite)) as Sprite;
        }
    }

	void ApplyDialogue(int currentIndex)
	{
        Dialogue currentDialogue;
        currentDialogue = dialogues[currentIndex];
        
        ApplyDialogueImage(currentDialogue);
        
		text.text = currentDialogue.GetName() + "\n\n" + currentDialogue.GetDialogue();
	}
    
    void InitializeAllPortraits()
    {
        LeftPortraitRenderer.sprite = null;
        RightPortraitRenderer.sprite = null;
    }

	// Use this for initialization
	void Start () {
		currentIndex = 0;
        text = GameObject.Find("DialogueText").GetComponent<Text>();
        SetDialogue();
        InitializeAllPortraits();
        isUpdated = false;
    }
    
	// Update is called once per frame
	void Update () {
        if (!isUpdated)
        {
            ApplyDialogue(currentIndex);
            isUpdated = true;
        }
	}
	
	void OnMouseDown()
	{
		if (currentIndex < dialogues.Count -1)
        {
			currentIndex ++;
            isUpdated = false;
        }
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
