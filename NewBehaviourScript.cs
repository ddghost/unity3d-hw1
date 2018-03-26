using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
		nowSize = 1;
		ifFinish = false;
		ifPeace = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setChess(int row , int col){
		chessBoard [row, col] = nowSize;
		checkFinish ();
		if(ifFinish){
			return ;
		}
		else if (nowSize == 1) {
			nowSize = 2;
		} 
		else {
			nowSize = 1;
		}
	}

	void resetChessBoard(){
		for (int loop = 0; loop < 3; loop++) {
			for (int loop1 = 0; loop1 < 3; loop1++) {
				chessBoard [loop, loop1] = 0;
			}
		}
		ifFinish = false;
		ifPeace = false;
	}

	void checkWin(){
		for (int loop = 0; loop < 3; loop++) {
			if (chessBoard [loop, 0] != 0 && chessBoard [loop, 0] == chessBoard [loop, 1]
				&& chessBoard [loop, 0] == chessBoard [loop, 2]) {
				ifFinish = true;
			}
			if (chessBoard [0, loop] != 0 && chessBoard [0, loop] == chessBoard [1, loop]
				&& chessBoard [0, loop] == chessBoard [2, loop]) {
				ifFinish = true;
			}
		}
		if (chessBoard [0, 0] != 0 && chessBoard [0, 0] == chessBoard [1, 1]
			&& chessBoard [0, 0] == chessBoard [2, 2]) {
			ifFinish = true;
		}
		if (chessBoard [0, 2] != 0 && chessBoard [0, 2] == chessBoard [1, 1]
			&& chessBoard [0, 2] == chessBoard [2, 0]) {
			ifFinish = true;
		}
	}

	void checkPeace(){
		ifPeace = true;
		for (int loop = 0; loop < 3; loop++) {
			for (int loop1 = 0; loop1 < 3; loop1++) {
				if(chessBoard[loop , loop1] == 0){
					ifPeace = false;
				}
			}
		}
	}


	void checkFinish(){
		checkWin ();
		if (!ifFinish) {
			checkPeace ();
		}
		if (ifPeace) {
			ifFinish = true;
		}
		else if (ifFinish) {
			winCount [nowSize - 1]++;
		}
	}

	void OnGUI () {
		// Make a background box
		if(ifFinish){
			string str;
			GUIStyle winBoxStyle = new GUIStyle(GUI.skin.box);
			winBoxStyle.fontSize = 30;
			if (ifPeace) {
				str = "Peace !!!";
			}
			else if (nowSize == 1) {
				str = "√ win !!!";
			}
			else{
				str = "x win !!!"; 
			}
			GUI.Box( new Rect(250 , 150 , 200, 100) ,  str , winBoxStyle);
			if( GUI.Button( new Rect(335 , 210 , 30, 30) , "ok" ) ){
				resetChessBoard ();
			}
			return ;
		}

		GUI.Box(new Rect(10,10,100,60), "Menu");
		GUI.Box(new Rect(10,80,100,90), "scorecard");
		GUI.Label (new Rect (30, 100, 60, 20), "√ ：" + winCount[0].ToString() );
		GUI.Label (new Rect (30, 120, 60, 20), "× ：" + winCount[1].ToString() );
		GUI.Box(new Rect(200,10,350,350) , "");

		GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
		buttonStyle.fontSize = 30;
		for (int loop = 0; loop < 3; loop++) {
			for (int loop1 = 0; loop1 < 3; loop1++) {
				switch(chessBoard[loop , loop1]){
				case 1:
					GUI.Button (new Rect (220 + loop1 * 110, 20 + loop * 110, 100, 100), "√" , buttonStyle);
					break;
				case 2: 
					GUI.Button (new Rect (220 + loop1 * 110, 20 + loop * 110, 100, 100), "×" , buttonStyle);
					break;
				default:
					if(GUI.Button (new Rect (220 + loop1 * 110, 20 + loop * 110, 100, 100), "",  buttonStyle)){
						setChess (loop, loop1);
					}
					break;
				}

			}
		}

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,80,20), "reset")) {
			resetChessBoard ();
		}
			
	}
	private int[,] chessBoard = new int[3 , 3];
	private int nowSize;
	private bool ifFinish , ifPeace;
	private int[] winCount = new int [2];
}
