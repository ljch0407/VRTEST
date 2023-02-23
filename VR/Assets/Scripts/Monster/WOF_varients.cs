using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class WOF_varients : MonoBehaviour
{
    [SerializeField] Transform eyeBone;
    [SerializeField] Transform target;
    [SerializeField] float headMaxTurnAngle;
    [SerializeField] float headTrackingSpeed;
    public SoundManager _soundManager;
    public AudioSource _AudioSource;
    public int _id;
    public ManagerAIScript _Ai;
    private void Start()
    {
        _Ai = GameObject.FindGameObjectWithTag("AiManager").GetComponent<ManagerAIScript>();
        _id = _Ai.monster_id_Update();
        
        target = GameObject.FindGameObjectWithTag("Player").transform;
        _soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        _soundManager.Add_Monster_audio(_AudioSource, _id);
        StartCoroutine(IDLESoundPlay());
    }
    
    void Update()
    {
        HeadTrackingUpdate();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explosive")
        {
            //TimeDirector.Play();
            StartCoroutine(DeadSoundPlay());
        }
    }
    void HeadTrackingUpdate()
    {
        //Head to Target
        Quaternion currentLocalRotation = eyeBone.localRotation;
        eyeBone.localRotation = Quaternion.identity;
        
        Vector3 targetWorldLookDir = target.position - eyeBone.position;
        Vector3 targetLocalLookDir = eyeBone.parent.InverseTransformDirection(targetWorldLookDir);
        
        
        targetLocalLookDir = Vector3.RotateTowards(
            Vector3.forward,
            targetLocalLookDir,
            Mathf.Deg2Rad * headMaxTurnAngle,
            0);
        Quaternion targetLocalRotation = Quaternion.LookRotation(targetLocalLookDir, -Vector3.up);
        
        eyeBone.localRotation = Quaternion.Slerp(currentLocalRotation,
            targetLocalRotation,
            1-Mathf.Exp(-headTrackingSpeed * Time.deltaTime)
        );        
    }
    IEnumerator DeadSoundPlay()
    {
        _soundManager.Monster_PlaySFX("SFX_WOF_Howling", _id);
        yield return new WaitForSeconds(0.4f);
    }
    IEnumerator IDLESoundPlay()
    {
        _soundManager.Monster_PlaySFX("SFX_WOF_IDLE", _id);
        yield return new WaitForSeconds(0.4f);
    }
}
