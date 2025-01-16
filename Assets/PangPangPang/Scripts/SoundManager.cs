using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace PangPangPang
{
	public class SoundManager : Singleton<SoundManager>
	{

		private AudioSource _bgmSource;
		private Transform _sfxSources;

		[SerializeField] private AudioMixer _audioMixer;

		[SerializeField] private List<AudioClip> _bgmClipsStorage = new List<AudioClip>();
		[SerializeField] private List<AudioClip> _sfxClipsStorage = new List<AudioClip>();

		private Dictionary<string, AudioClip> _bgmClips = new Dictionary<string, AudioClip>();
		private Dictionary<string, AudioClip> _sfxClips = new Dictionary<string, AudioClip>();

		public bool isMutedBGM { get; set; } = false;
		public bool isMutedSFX { get; set; } = false;

		private void Awake()
		{
			var objs = FindObjectsOfType<SoundManager>();
			if (objs.Length != 1)
			{
				Destroy(gameObject);
				print("싱글톤 삭제");
				return;
			}

			DontDestroyOnLoad(gameObject);


		}

		private void Start()
		{
			Init();
		}

		private void Init()
		{
			foreach (AudioClip s in _bgmClipsStorage)
			{
				_bgmClips.Add(s.name, s);
			}

			foreach (AudioClip s in _sfxClipsStorage)
			{
				//print(s.name);
				_sfxClips.Add(s.name, s);
			}

			_bgmSource = new GameObject("BgmSource").AddComponent<AudioSource>();
			_bgmSource.loop = true;
			_bgmSource.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("Master")[1];


			_sfxSources = new GameObject("SfxSources").transform;

			_bgmSource.transform.SetParent(transform);
			_sfxSources.SetParent(transform);



			for (int i = 0; i < 5; i++)//sfxSource 자식으로 5개만들기
			{
				GameObject temp = new GameObject("sfxSource" + (i + 1));
				temp.AddComponent<AudioSource>();
				temp.transform.SetParent(_sfxSources);
				temp.GetComponent<AudioSource>().outputAudioMixerGroup = _audioMixer.FindMatchingGroups("Master")[2];
			}

		}

		public void StopAll()
		{
			// 재생기 전부 재생 스탑, 음반 빼기

			_bgmSource.clip = null;
			_bgmSource.Stop();

			foreach (Transform es in _sfxSources)
			{
				es.GetComponent<AudioSource>().clip = null;
				es.GetComponent<AudioSource>().Stop();
			}
		}

		public void Clear()
		{
			// 재생기 전부 재생 스탑, 음반 빼기

			_bgmSource.clip = null;
			_bgmSource.Stop();

			foreach (Transform es in _sfxSources)
			{
				es.GetComponent<AudioSource>().clip = null;
				es.GetComponent<AudioSource>().Stop();
			}

			// 효과음 Dictionary 비우기
			_bgmClips.Clear();
			_sfxClips.Clear();
		}

		public void Play(Define.AudioType type, string name)
		{

			if (type == Define.AudioType.BGM)
			{
				if (_bgmSource.isPlaying)
					_bgmSource.Stop();

				AudioClip audioClip = FindClip(type, name);

				_bgmSource.clip = audioClip;
				_bgmSource.Play();
			}
			else
			{
				AudioClip audioClip = FindClip(type, name);
				//print(FindClip(type, name));
				foreach (Transform source in _sfxSources)
				{
					AudioSource temp = source.GetComponent<AudioSource>();
					if (!temp.isPlaying)
					{
						temp.clip = audioClip;
						temp.Play();
						return;
					}
				}

				//SFXSource가 부족할 시 생성 후 실행
				GameObject sfx = new GameObject("SfxSource" + (_sfxSources.childCount + 1));
				sfx.AddComponent<AudioSource>();
				sfx.transform.SetParent(_sfxSources);

				sfx.GetComponent<AudioSource>().clip = audioClip;
				sfx.GetComponent<AudioSource>().Play();
			}


		}

		public void SetVolume(Define.AudioType type, float volume = 1.0f)
		{
			switch (type)
			{
				case Define.AudioType.MASTER:
					_audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
					break;
				case Define.AudioType.BGM:
					_audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
					break;
				case Define.AudioType.SFX:
					_audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
					break;
			}

			/*	if (type == Define.AudioType.BGM)
				{
					if (_bgmSource == null)
						return;

					_bgmSource.volume = volume;

				}
				else
				{
					if (_sfxSources == null)
						return;
					foreach (Transform sfx in _sfxSources)
					{
						sfx.GetComponent<AudioSource>().volume = volume;
					}

				}*/

		}
		public bool GetMute(Define.AudioType type)
		{
			switch (type)
			{
				case Define.AudioType.BGM:
					return _bgmSource.mute;
				case Define.AudioType.SFX:
					return _sfxSources.GetChild(0).GetComponent<AudioSource>().mute;
				default:
					return false;
			}


		}

		public void SetMute(Define.AudioType type, bool value)
		{
			switch (type)
			{
				case Define.AudioType.BGM:
					if (_bgmSource == null)
						return;

					_bgmSource.mute = value;
					isMutedBGM = value;
					/*if (value)
					{
						_bgmSource.Pause();
					}
					else
					{

						_bgmSource.Play();
					}*/
					break;
				case Define.AudioType.SFX:
					if (_sfxSources == null)
						return;

					isMutedSFX = value;

					foreach (Transform sfx in _sfxSources)
					{
						sfx.GetComponent<AudioSource>().mute = value;

					}
					break;
			}

		}

		public void SetPause(Define.AudioType type, bool value)
		{
			switch (type)
			{
				case Define.AudioType.BGM:
					if (_bgmSource == null)
						return;

					if (value)
					{
						_bgmSource.Pause();
					}
					else
					{

						_bgmSource.Play();
					}
					break;
				case Define.AudioType.SFX:
					if (_sfxSources == null)
						return;
					foreach (Transform sfx in _sfxSources)
					{
						if (value)
						{
							sfx.GetComponent<AudioSource>().Pause();
						}
						else
						{

							sfx.GetComponent<AudioSource>().Play();
						}
					}
					break;
			}

		}

		public void SetPitch(Define.AudioType type, float pitch = 1.0f)
		{
			if (type == Define.AudioType.BGM)
			{
				if (_bgmSource == null)
					return;

				_bgmSource.pitch = pitch;

			}
			else
			{
				if (_sfxSources == null)
					return;
				foreach (Transform sfx in _sfxSources)
				{
					sfx.GetComponent<AudioSource>().pitch = pitch;
				}

			}

		}

		private AudioClip FindClip(Define.AudioType type, string name)
		{
			AudioClip audioClip = null;
			if (type == Define.AudioType.BGM)
			{
				if (_bgmClips.TryGetValue(name, out audioClip))
				{
					return audioClip;
				}
			}
			else
			{

				if (_sfxClips.TryGetValue(name, out audioClip))
				{
					return audioClip;
				}
			}
			return audioClip;
		}


	}
}