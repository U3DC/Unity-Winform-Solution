/*===============================================================
 *Copyright(C) 2017 by    #COMPANY# All rights reserved. 
 *FileName:               #SCRIPTFULLNAME# 
 *Author:                 #AUTHOR# 
 *Version:                #VERSION# 
 *UnityVersion：          #UNITYVERSION# 
 *Date:                   #DATE# 
 *Description:    		  
 *History: 				  
================================================================*/

using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WindowOpenfile : MonoBehaviour {


    private string audioPathName;
	private string audioFileName;


	FileInfo myfile;
    public Image img;


    void Start()
    {
        ///隐藏任务栏
        ToolControlTaskBar.HideTaskBar();
        Debug.Log(">>>>>>>>隐藏任务栏");
    }


    void OnApplicationQuit()
    {
        ToolControlTaskBar.ShowTaskBar();
        Debug.Log(">>>>>>>> 显示任务栏");
    }




    void Update () {

		if (Input.GetKey (KeyCode.Escape)) {
			UnityEngine.Application.Quit ();
            
		}


    }


	public	void OnClickOpenFileButton()
	{
		OpenFileDialog myofd = new OpenFileDialog ();
		myofd.InitialDirectory = "file://" + UnityEngine.Application.dataPath;
		myofd.Multiselect = false;

		myofd.Filter = "音频文件(*.ogg,*.wav,*.mp3)|*.ogg;*.wav;*.mp3";
		myofd.FilterIndex = 0;


		if (myofd.ShowDialog () == DialogResult.OK) {
			
			
			Debug.Log (myofd.FileName);
		
			StartCoroutine (ChangeAudio (myofd.FileName));
		}



    }


	public	void OnClickOpenPictureFileButtton()
	{
		OpenFileDialog mypicofd = new OpenFileDialog ();
        mypicofd.Title = "Test";
		mypicofd.InitialDirectory = "file://" + UnityEngine.Application.dataPath;
		mypicofd.Multiselect = false;
		mypicofd.Filter = "图片文件(*.png,*.jpg,*.bmp)|*.png;*.jpg;*.bmp";
		mypicofd.FilterIndex = 0;

		if (mypicofd.ShowDialog () == DialogResult.OK) {
			Debug.Log (mypicofd.FileName);
			StartCoroutine (ChangeTexture (mypicofd.FileName));
		}


    }


	IEnumerator ChangeAudio(string name)
	{
		WWW www = new WWW ("file://" + name);
		yield return www;
		UnityEngine.Resources.UnloadUnusedAssets ();
		this.GetComponent<AudioSource> ().clip = www.GetAudioClip ();
		this.GetComponent<AudioSource> ().Play ();
	}

	IEnumerator ChangeTexture(string name)
	{

		WWW www = new WWW ("file://" +name);
		yield return www;
		UnityEngine.Resources.UnloadUnusedAssets ();

       ///转换texture为sprite
        Texture2D imgt = www.texture as Texture2D;
        Sprite pic = Sprite.Create(imgt, new Rect(0, 0, imgt.width, imgt.height), new Vector2(0.5f, 0.5f));//后面Vector2就是你Anchors的Pivot的x/y属性值，0.5为居中
        img.sprite = pic;


    }

}
