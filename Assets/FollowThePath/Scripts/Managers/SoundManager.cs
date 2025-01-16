using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace FollowThePath
{
	public class SoundManager : Singleton<SoundManager>
	{
		// BGM�� ����� �ҽ�
		private AudioSource _bgmSource;

		// SFX�� ����� �ҽ� Ǯ
		private List<AudioSource> _sfxSources;
		private int _poolSize = 10; // Ǯ�� ũ��

		// �ݺ� ����� ����� �ҽ�
		private Dictionary<AudioClip, AudioSource> _loopingSFXSources;

		[SerializeField] private AudioMixer _audioMixer;

		[SerializeField] private List<AudioClip> _bgmClipsStorage = new List<AudioClip>();
		[SerializeField] private List<AudioClip> _sfxClipsStorage = new List<AudioClip>();


		private void Awake()
		{
			var objs = FindObjectsOfType<SoundManager>();
			if (objs.Length != 1)
			{
				Destroy(gameObject);
				print("�̱��� ����");
				return;
			}

			DontDestroyOnLoad(gameObject);


			// BGM ����� �ҽ� �ʱ�ȭ
			_bgmSource = gameObject.AddComponent<AudioSource>();
			_bgmSource.loop = true; // BGM�� �⺻������ ���� ����
			_bgmSource.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("BGM")[0];

			// SFX ����� �ҽ� Ǯ �ʱ�ȭ
			_sfxSources = new List<AudioSource>();
			for (int i = 0; i < _poolSize; i++)
			{
				GameObject sfxObject = new GameObject("SFX_Source_" + i);
				sfxObject.transform.parent = transform; // SoundManager �Ʒ��� �α�
				AudioSource sfxSource = sfxObject.AddComponent<AudioSource>();
				sfxSource.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("SFX")[0];
				_sfxSources.Add(sfxSource);
			}

			// �ݺ� ����� ����� �ҽ� �ʱ�ȭ
			_loopingSFXSources = new Dictionary<AudioClip, AudioSource>();
		}

		public void SetMasterVolume(float volume = 1.0f)
		{
			_audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
		}

		public void SetBGMVolume(float volume = 1.0f)
		{
			_audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
		}

		public void SetSFXVolume(float volume = 1.0f)
		{
			_audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
		}

	
		private AudioClip FindBGMClip(string name)
		{
			foreach (AudioClip clip in _bgmClipsStorage)
            {
				if (clip.name == name)
                {
					return clip;
                }
            }
			
			return null;
		}


		private AudioClip FindSFXClip(string name)
		{
			foreach (AudioClip clip in _sfxClipsStorage)
			{
				if (clip.name == name)
				{
					return clip;
				}
			}

			return null;
		}

		// ��� ������ ����� �ҽ� ã��
		private AudioSource GetAvailableSFXSource()
		{
			foreach (AudioSource source in _sfxSources)
			{
				if (!source.isPlaying)
				{
					return source;
				}
			}
			// ��� �ҽ��� ��� ���̸� null ��ȯ
			return null;
		}

		// ȿ���� ��� �޼��� (������Ʈ Ǯ�� ���)
		public void PlaySFX(string clipName)
		{
			AudioSource sfxSource = GetAvailableSFXSource();
			if (sfxSource != null)
			{
				sfxSource.clip = FindSFXClip(clipName);
				sfxSource.Play();
			}
			else
			{
				Debug.LogWarning("No available SFX source to play clip: " + clipName);
			}
		}

		// �ݺ� ��� ȿ���� ��� �޼���
		public void PlayLoopingSFX(string clipName)
		{
			AudioClip clip = FindSFXClip(clipName);

			if (!_loopingSFXSources.ContainsKey(clip))
			{
				AudioSource sfxSource = gameObject.AddComponent<AudioSource>();
				sfxSource.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("SFX")[0];
				sfxSource.clip = clip;
				sfxSource.loop = true;
				sfxSource.Play();
				_loopingSFXSources.Add(clip, sfxSource);
			}
		}

		// �ݺ� ��� ȿ���� ���� �޼���
		public void StopLoopingSFX(string clipName)
		{
			AudioClip clip = FindSFXClip(clipName);

			if (_loopingSFXSources.ContainsKey(clip))
			{
				AudioSource sfxSource = _loopingSFXSources[clip];
				sfxSource.Stop();
				Destroy(sfxSource);
				_loopingSFXSources.Remove(clip);
			}
		}

		// ��� ���� ��� �޼���
		public void PlayBGM(string clipName, bool loop = true)
		{
			_bgmSource.clip = FindBGMClip(clipName);
			_bgmSource.loop = loop;
			_bgmSource.Play();
		}

		// ��� ���� ���� �޼���
		public void StopBGM()
		{
			_bgmSource.Stop();
		}

		// ��� ���� �Ͻ� ���� �޼���
		public void PauseBGM()
		{
			_bgmSource.Pause();
		}

		// ��� ���� �ٽ� ��� �޼���
		public void ResumeBGM()
		{
			_bgmSource.UnPause();
		}

	}
}