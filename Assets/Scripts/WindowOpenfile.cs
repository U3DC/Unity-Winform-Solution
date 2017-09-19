/*===============================================================
 *Copyright(C) 2017 by    U3DC All rights reserved. 
 *FileName:               WindowOpenfile 
 *Author:                 MARK
 *Version:                1.0
 *UnityVersion：          Unity2017.1 
 *Date:                   2017.9.19
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
        ///HideTaskBar
        ToolControlTaskBar.HideTaskBar();

    }


    void OnApplicationQuit()
    {
        ToolControlTaskBar.ShowTaskBar();
     
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

		myofd.Filter = "AUDIOFILE(*.ogg,*.wav,*.mp3)|*.ogg;*.wav;*.mp3";
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
		mypicofd.Filter = "PICFILE(*.png,*.jpg,*.bmp)|*.png;*.jpg;*.bmp";
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

       ///convert texture to sprite
        Texture2D imgt = www.texture as Texture2D;
        Sprite pic = Sprite.Create(imgt, new Rect(0, 0, imgt.width, imgt.height), new Vector2(0.5f, 0.5f));//Vector2 is Pivot value, 0.5 is center
        img.sprite = pic;


    }

}
