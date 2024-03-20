using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    public static PlayerController Instance;

    //float
    public float playerSpeed;
    public float maxSpeed;
    public float lineDistance;
    public float gravity;
    public float jumpForce;
    public float slideDuration;
    public float AttackModeDuration;

    public float transformEffectDuration;
    public float coinCollectEffectDuration;
    public float HitEffectDuration;
    public float DestroyObsticleEffectDuration;

    //Bool
    public bool isGrounded;
    public static bool isAttackModeActive;
    public static bool isPlayerTransform;
    private bool isSliding = false;
    bool toggle = false;
    bool isHit;

    //Vector 3
    private Vector3 move;
    public Vector3 velocity;

    //Int
    private int desiredLine = 1;

    //Refrence
    public CharacterController controller;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Animator animator;
    public GameObject Destroyer;
    public GameObject coinCollecter;

    // Transformation Var
    public GameObject boyMesh;
    public Animator boyAnimator;
    public GameObject wereWolfMesh;
    public Animator werewolfAnimator;


    //Effect
    public GameObject transformEffect;
    public GameObject coinCollectEffect;
    public GameObject HitEffect;
    public GameObject ObsticleDestroyEffect;

    #endregion


    #region Unity Functions

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isGameStarted || GameManager.Instance.gameOver)
            return;
        UpdateTransformBar();
        IncreasePlayerSpeed();
        CountScore();

    }

    void Update()
    {
        if (!GameManager.Instance.isGameStarted || GameManager.Instance.gameOver || GameManager.Instance.isGamePaused)
            return;
        if (GameManager.Instance.isTutorialPlay)
        {
            PlayerControllesForTutorial();
        }
        else
        {
            PlayerControlles();
        }

        if (velocity.y >= 10)
        {
            velocity.y = 10;
        }

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (isPlayerTransform || PlayerManager.Instance.isArmorActivate)
        {
            if (hit.transform.tag == "Obsticle")
            {
                if (!hit.transform.GetComponent<EnemyController>().IsHit)
                {
                    hit.transform.GetComponent<EnemyController>().IsHit = true;
                    AudioPlayer.Instance.PlayDestroySound();
                    PlayDestroyObsticleEffect();
                    Destroy(hit.gameObject, 0f);
                    if (PlayerManager.Instance.isArmorActivate && !isPlayerTransform)
                    {
                        PlayerManager.Instance.DisableArmor();
                    }
                }
            }

            if (hit.transform.tag == "Enemy")
            {
                if (isAttackModeActive)
                {
                    if (!hit.transform.GetComponent<EnemyController>().IsHit)
                    {
                        hit.transform.GetComponent<EnemyController>().IsHit = true;
                        AudioPlayer.Instance.PlayAttackSound();
                        PlayHitEffect();
                        hit.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 1) * 27, ForceMode.Impulse);
                        Destroy(hit.gameObject, 0.45f);
                    }
                }
                else
                {
                    if (!hit.transform.GetComponent<EnemyController>().IsHit)
                    {
                        hit.transform.GetComponent<EnemyController>().IsHit = true;
                        AudioPlayer.Instance.PlayAttackSound();
                        PlayHitEffect();
                        hit.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 1) * 27, ForceMode.Impulse);
                        Destroy(hit.gameObject, 0.45f);
                    }
                    if (PlayerManager.Instance.isArmorActivate && !isPlayerTransform)
                    {
                        PlayerManager.Instance.DisableArmor();
                    }
                }
            }
        }
        else
        {
            if (hit.transform.tag == "Obsticle")
            {
                if (!hit.transform.GetComponent<EnemyController>().IsHit)
                {
                    hit.transform.GetComponent<EnemyController>().IsHit = true;
                    AudioPlayer.Instance.PlayHitSound();
                    PlayHitEffect();
                    GameManager.Instance.gameOver = true;
                    animator.SetBool("Die", true);
                    GameManager.Instance.DisableGameplayUI();
                    Invoke("CallReviveOnDelay", 2f);
                }
            }

            if (hit.transform.tag == "Enemy")
            {
                if (isAttackModeActive)
                {
                    if (!hit.transform.GetComponent<EnemyController>().IsHit)
                    {
                        hit.transform.GetComponent<EnemyController>().IsHit = true;
                        hit.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 1) * 27, ForceMode.Impulse);
                        animator.SetBool("Attack", true);
                        PlayHitEffect();
                        AudioPlayer.Instance.PlayAttackSound();
                        Invoke("DisableAttackAnimation", 0.2f);
                        GameManager.Instance.transformPoint += 1;
                        GameManager.Instance.UpdateTransformBar();
                        Destroy(hit.gameObject, 0.45f);
                    }
                }
                else
                {
                    if (!hit.transform.GetComponent<EnemyController>().IsHit)
                    {
                        hit.transform.GetComponent<EnemyController>().IsHit = true;
                        hit.gameObject.GetComponent<Animator>().SetBool("Attack", true);
                        AudioPlayer.Instance.PlayHitSound();
                        PlayHitEffect();
                        GameManager.Instance.gameOver = true;
                        animator.SetBool("Die", true);
                        GameManager.Instance.DisableGameplayUI();
                        Invoke("CallReviveOnDelay", 2f);
                    }
                }
            }
        }
    }

    void DisableAttackAnimation()
    {
        animator.SetBool("Attack", false);
        werewolfAnimator.SetBool("Attack", false);
    }

    void CallReviveOnDelay()
    {
        GameManager.Instance.OpenRevive();
        GameManager.Instance.ReviveBarController.isReviveBarOn = true;
    }
    #endregion

    #region Custom Function
    private void Jump()
    {
        if (!isPlayerTransform)
        {
            AudioPlayer.Instance.PlayJumpSound();
            isAttackModeActive = true;
            CancelInvoke("DisableAttackMode");
            Invoke("DisableAttackMode", AttackModeDuration);
            StopCoroutine(Slide());
            animator.SetBool("isSliding", false);
            animator.SetTrigger("jump");
            controller.center = new Vector3(0, 1, 0);
            controller.height = 2;
            isSliding = false;

            velocity.y = Mathf.Sqrt(jumpForce * 2 * -gravity);
        }
    }
    private IEnumerator Slide()
    {
        if (!isPlayerTransform)
        {
            isSliding = true;
            animator.SetBool("isSliding", true);
            controller.center = new Vector3(0, 0.5f, 0);
            AudioPlayer.Instance.PlaySlideSound();
            isAttackModeActive = true;
            CancelInvoke("DisableAttackMode");
            Invoke("DisableAttackMode", AttackModeDuration);

            yield return new WaitForSeconds(0.25f / Time.timeScale);
            controller.height = 1;

            yield return new WaitForSeconds((slideDuration - 0.25f) / Time.timeScale);

            animator.SetBool("isSliding", false);

            controller.center = new Vector3(0, 1, 0);
            controller.height = 2;

            yield return new WaitForSeconds(0.25f);
            isSliding = false;
        }
    }

    void IncreasePlayerSpeed()
    {
            if (playerSpeed < maxSpeed)
                playerSpeed += 0.15f * Time.fixedDeltaTime;
    }

    void PlayerControllesForTutorial()
    {
        animator.SetBool("isGameStarted", true);
        move.z = playerSpeed;

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.17f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);
        if (isGrounded && velocity.y < 0)
            velocity.y = -1f;

        if (isGrounded)
        {
            if (TutorialStageController.tutorialStageIndex == 2 && GameManager.Instance.tutorialPages[1].activeInHierarchy || TutorialStageController.tutorialStageIndex == 5
                && GameManager.Instance.tutorialPages[4].activeInHierarchy)
            {
                if (SwipeControlls.swipeUp || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    TutorialStageController.Instance.ClearTutorialPage();
                    Jump();
                }
            }

            if (TutorialStageController.tutorialStageIndex == 3 && GameManager.Instance.tutorialPages[2].activeInHierarchy)
            {
                if (SwipeControlls.swipeDown && !isSliding || Input.GetKeyDown(KeyCode.DownArrow) && !isSliding)
                {
                    TutorialStageController.Instance.ClearTutorialPage();
                    StartCoroutine(Slide());
                }
            }
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);

        //Gather the inputs on which lane we should be

        if (TutorialStageController.tutorialStageIndex == 4 && GameManager.Instance.tutorialPages[3].activeInHierarchy)
        {
            if (SwipeControlls.swipeRight || Input.GetKeyDown(KeyCode.RightArrow))
            {
                TutorialStageController.Instance.ClearTutorialPage();
                AudioPlayer.Instance.PlayMoveSound();
                isAttackModeActive = true;
                CancelInvoke("DisableAttackMode");
                Invoke("DisableAttackMode", AttackModeDuration);
                desiredLine++;
                if (desiredLine == 3)
                    desiredLine = 2;
            }
        }


        if (TutorialStageController.tutorialStageIndex == 1 && GameManager.Instance.tutorialPages[0].activeInHierarchy)
        {
            if (SwipeControlls.swipeLeft || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                TutorialStageController.Instance.ClearTutorialPage();
                AudioPlayer.Instance.PlayMoveSound();
                isAttackModeActive = true;
                CancelInvoke("DisableAttackMode");
                Invoke("DisableAttackMode", AttackModeDuration);
                desiredLine--;
                if (desiredLine == -1)
                    desiredLine = 0;
            }
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLine == 0)
            targetPosition += Vector3.left * lineDistance;
        else if (desiredLine == 2)
            targetPosition += Vector3.right * lineDistance;

        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 30 * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.magnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }

        controller.Move(move * Time.deltaTime);
    }
    void PlayerControlles()
    {
        animator.SetBool("isGameStarted", true);
        move.z = playerSpeed;

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.17f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);
        if (isGrounded && velocity.y < 0)
            velocity.y = -1f;

        if (isGrounded)
        {
            if (SwipeControlls.swipeUp || Input.GetKeyDown(KeyCode.UpArrow))
                Jump();

            if (SwipeControlls.swipeDown && !isSliding || Input.GetKeyDown(KeyCode.DownArrow) && !isSliding)
                StartCoroutine(Slide());
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
            if (SwipeControlls.swipeDown && !isSliding || Input.GetKeyDown(KeyCode.DownArrow) && !isSliding)
            {
                StartCoroutine(Slide());
                velocity.y = -10;
            }

        }
        controller.Move(velocity * Time.deltaTime);

        //Gather the inputs on which lane we should be
        if (SwipeControlls.swipeRight || Input.GetKeyDown(KeyCode.RightArrow))
        {
            AudioPlayer.Instance.PlayMoveSound();
            isAttackModeActive = true;
            CancelInvoke("DisableAttackMode");
            Invoke("DisableAttackMode", AttackModeDuration);
            desiredLine++;
            if (desiredLine == 3)
                desiredLine = 2;
        }
        if (SwipeControlls.swipeLeft || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AudioPlayer.Instance.PlayMoveSound();
            isAttackModeActive = true;
            CancelInvoke("DisableAttackMode");
            Invoke("DisableAttackMode", AttackModeDuration);
            desiredLine--;
            if (desiredLine == -1)
                desiredLine = 0;
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLine == 0)
            targetPosition += Vector3.left * lineDistance;
        else if (desiredLine == 2)
            targetPosition += Vector3.right * lineDistance;

        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 30 * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.magnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }

        controller.Move(move * Time.deltaTime);
    }

    void DisableAttackMode()
    {
        isAttackModeActive = false;
    }

    void CountScore()
    {
        if (PlayerManager.Instance.isScoreBoosterActivate)
        {
            GameManager.Instance.score += Time.deltaTime * 10;
        }
        else
        {
            GameManager.Instance.score += Time.deltaTime * 5;
        }
        GameManager.Instance.scoreText.text = Mathf.RoundToInt(GameManager.Instance.score).ToString();
    }

    public void RevivePlayer()
    {
        StartShortArmor();
        animator.SetBool("Die", false);
        GameManager.Instance.gameOver = false;
    }

    public void StartShortArmor()
    {
        Destroyer.SetActive(true);
        Invoke("DisableShortArmor", 0.5f);
    }

    void DisableShortArmor()
    {
        Destroyer.SetActive(false);
    }


    public void PlayHitEffect()
    {
        HitEffect.SetActive(true);
        Invoke("DesableHitEffect", HitEffectDuration);
    }
    void DesableHitEffect()
    {
        HitEffect.SetActive(false);
    }
    public void PlayTransformEffect()
    {
        transformEffect.SetActive(true);
        Invoke("DesableTransformEffect", transformEffectDuration);
    }
    void DesableTransformEffect()
    {
        transformEffect.SetActive(false);
    }
    public void PlayCoinEffect()
    {
        coinCollectEffect.SetActive(true);
        Invoke("DesableCoinEffect", coinCollectEffectDuration);
    }
    void DesableCoinEffect()
    {
        coinCollectEffect.SetActive(false);
    }
    public void PlayDestroyObsticleEffect()
    {
        ObsticleDestroyEffect.SetActive(true);
        Invoke("DesableDestroyObsticleEffect", DestroyObsticleEffectDuration);
    }

    void DesableDestroyObsticleEffect()
    {
        ObsticleDestroyEffect.SetActive(false);
    }
    #endregion


    #region Transform Controls

    public void GetTransform()
    {
        if (!GameManager.Instance.isGameStarted || GameManager.Instance.gameOver)
            return;
        GameManager.Instance.transformBarFullEffect.SetActive(false);
        if (PlayerPrefs.GetInt("transformCount") == 0)
        {
            PlayerPrefs.SetInt("transformCount", 1);
            Time.timeScale = 1;
            GameManager.Instance.tutorialPages[6].SetActive(false);
        }
        AudioPlayer.Instance.PlayTransformSound();
        PlayTransformEffect();
        GameManager.Instance.transformPoint = 0;
        GameManager.Instance.TransformButton.interactable = false;
        isPlayerTransform = true;
        playerSpeed = 30;
        Invoke("TransformPlayerOnDelay", 0.25f);
        StartCoroutine(GetBackToNormalForm());
    }

    void TransformPlayerOnDelay()
    {
        boyMesh.SetActive(false);
        wereWolfMesh.SetActive(true);
    }

    void UpdateTransformBar()
    {
        if (isPlayerTransform)
        {
            //Reduce fill amount over 30 seconds
            GameManager.Instance.TransformFilBar.fillAmount -= 1.0f / GameManager.Instance.TransformDuration * Time.deltaTime;
        }
    }

     IEnumerator GetBackToNormalForm()
    {
        yield return new WaitForSeconds(GameManager.Instance.TransformDuration);

        StartShortArmor();
        AudioPlayer.Instance.PlayTransformSound();
        PlayTransformEffect();
        playerSpeed = 16;
        wereWolfMesh.SetActive(false);
        boyMesh.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        
        isPlayerTransform = false;
    }

    #endregion
}
