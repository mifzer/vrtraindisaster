using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasySurvivalScripts
{
    public enum PlayerStates
    {
        Idle,
        Walking,
        Running,
        Jumping
    }

    public class PlayerMovement : MonoBehaviour
    {
        [Header("Shake Properties")]
        [SerializeField] private float _ShakeThreshold = 2;
        [SerializeField] private Vector3[] _AllListStartPosition;
        [SerializeField] private ScenarioData _ScenarioData;
        [SerializeField] private bool _IsInitialPositionInStart = true;
        

        public PlayerStates playerStates;

        [Header("Inputs")]
        public string HorizontalInput = "Horizontal";
        public string VerticalInput = "Vertical";
        public string RunInput = "Run";
        public string JumpInput = "Jump";

        [Header("Player Motor")]
        [Range(1f,15f)]
        public float walkSpeed;
        [Range(1f,15f)]
         public float runSpeed;
        [Range(1f,15f)]
        public float JumpForce;
        public Transform FootLocation;

        [Header("Animator and Parameters")]
        public Animator CharacterAnimator;
        public float HorzAnimation;
        public float VertAnimation;
        public bool JumpAnimation;
        public bool LandAnimation;

        [Header("Sounds")]
        public List<AudioClip> FootstepSounds;
        public List<AudioClip> JumpSounds;
        public List<AudioClip> LandSounds;

        CharacterController characterController;

        float _footstepDelay;
        AudioSource _audioSource;
        float footstep_et = 0;

        
        void InitStartPos(){
            if(_ScenarioData.ChairPosition == 2){
                characterController.SimpleMove(_AllListStartPosition[0] );
            }else if(_ScenarioData.ChairPosition == 12){
                characterController.SimpleMove(_AllListStartPosition[2]);
            }
        }

        // Use this for initialization
        void Start()
        {
            characterController = GetComponent<CharacterController>();
            _audioSource = gameObject.AddComponent<AudioSource>();

            // InitStartPos();
        }

        // Update is called once per frame
        void Update()
        {
            //handle controller
            HandlePlayerControls();

            //sync animations with controller
            SetCharacterAnimations();

            //sync footsteps with controller
            PlayFootstepSounds();
        }

        void HandlePlayerControls()
        {
            float hInput = Input.GetAxisRaw(HorizontalInput);
            // float vInput = Input.GetAxisRaw(VerticalInput);
            // Debug.Log(vInput);



#region input accelerometer
            float vInput = 0;

            if(_IsInitialPositionInStart){
                
                // vInput += Time.deltaTime;

                if(_ScenarioData.ChairPosition == 2){
                    vInput += Time.deltaTime * 1000;
                    // vInput += 20000;
                    
                    if( transform.position.z > _AllListStartPosition[0].z){
                        Debug.Log("max");
                        _IsInitialPositionInStart = false;
                    }

                }

                if(_ScenarioData.ChairPosition == 12){
                    vInput -= Time.deltaTime * 1000;

                    if( transform.position.z < _AllListStartPosition[2].z){
                        Debug.Log("min");
                        _IsInitialPositionInStart = false;
                    }

                }

            }

            if(Input.acceleration.sqrMagnitude > _ShakeThreshold){
                vInput = 1;
            }

#endregion

            Vector3 fwdMovement = characterController.isGrounded == true ? transform.forward * vInput : Vector3.zero;
            Vector3 rightMovement = characterController.isGrounded == true ? transform.right * hInput : Vector3.zero;

            float _speed = Input.GetButton(RunInput) ? runSpeed : walkSpeed;
            characterController.SimpleMove(Vector3.ClampMagnitude(fwdMovement + rightMovement, 1f) * _speed);

            if (characterController.isGrounded)
                Jump();

            //Managing Player States
            if (characterController.isGrounded)
            {
                if (hInput == 0 && vInput == 0)
                    playerStates = PlayerStates.Idle;
                else
                {
                    if (_speed == walkSpeed)
                        playerStates = PlayerStates.Walking;
                    else
                        playerStates = PlayerStates.Running;

                    _footstepDelay = (2/_speed);
                }
            }
            else
                playerStates = PlayerStates.Jumping;
        }

        void Jump()
        {
            if (Input.GetButtonDown(JumpInput))
            {
                StartCoroutine(PerformJumpRoutine());
                JumpAnimation = true;
            }
        }

        IEnumerator PerformJumpRoutine()
        {
            //play jump sound
            if (_audioSource)
                _audioSource.PlayOneShot(JumpSounds[Random.Range(0, JumpSounds.Count)]);

            float _jump = JumpForce;

            do
            {
                characterController.Move(Vector3.up * _jump * Time.deltaTime);
                _jump -= Time.deltaTime;
                yield return null;
            }
            while (!characterController.isGrounded);

            //play land sound
            if (_audioSource)
                _audioSource.PlayOneShot(LandSounds[Random.Range(0, LandSounds.Count)]);

        }

        void SetCharacterAnimations()
        {
            if (!CharacterAnimator)
                return;

            switch (playerStates)
            {
                case PlayerStates.Idle:
                    HorzAnimation = Mathf.Lerp(HorzAnimation, 0, 5 * Time.deltaTime);
                    VertAnimation = Mathf.Lerp(VertAnimation, 0, 5 * Time.deltaTime);
                    break;

                case PlayerStates.Walking:
                    HorzAnimation = Mathf.Lerp(HorzAnimation, 1 * Input.GetAxis("Horizontal"), 5 * Time.deltaTime);
                    VertAnimation = Mathf.Lerp(VertAnimation, 1 * Input.GetAxis("Vertical"), 5 * Time.deltaTime);
                    break;

                case PlayerStates.Running:
                    HorzAnimation = Mathf.Lerp(HorzAnimation, 2 * Input.GetAxis("Horizontal"), 5 * Time.deltaTime);
                    VertAnimation = Mathf.Lerp(VertAnimation, 2 * Input.GetAxis("Vertical"), 5 * Time.deltaTime);
                    break;

                case PlayerStates.Jumping:
                    if (JumpAnimation)
                    {
                        CharacterAnimator.SetTrigger("Jump");
                        JumpAnimation = false;
                    }
                    break;
            }

            LandAnimation = characterController.isGrounded;
            CharacterAnimator.SetFloat("Horizontal", HorzAnimation);
            CharacterAnimator.SetFloat("Vertical", VertAnimation);
            CharacterAnimator.SetBool("isGrounded", LandAnimation);
        }

        bool onGround()
        {
            bool retVal = false;

            if (Physics.Raycast(FootLocation.position, Vector3.down, 0.1f))
                retVal = true;
            else
                retVal = false;

            return retVal;
        }

        void PlayFootstepSounds()
        {
            if (playerStates == PlayerStates.Idle || playerStates == PlayerStates.Jumping)
                return;

            if (footstep_et < _footstepDelay)
                footstep_et += Time.deltaTime;
            else
            {
                footstep_et = 0;
                _audioSource.PlayOneShot(FootstepSounds[Random.Range(0, FootstepSounds.Count)]);
            }
        }

    }
}