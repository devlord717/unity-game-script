using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Engine.Events;

public class GameCustomPlayerContainer : MonoBehaviour {
            
    public GameCustomCharacterData customCharacterData = new GameCustomCharacterData();
    public GameObject containerRotator;
    public GameObject containerPlayerDisplay;
    public bool allowRotator = false;
    public bool isProfileCharacterCode = false;
    Rigidbody rotatorRigidbody;
    RotateObject rotateObject;
    CapsuleCollider rotatorCollider;
    GameCustomPlayer customPlayerObject;
    
    //float lastUpdateScale = 0;
    //float factorUpdateScale = 0.0;
    
    public double currentContainerScale = 1.0;
    public double currentContainerRotation = 1.0;
    public double scaleMax = 1.8;
    public double scaleMin = 1.0;
    public string uuid = System.Guid.NewGuid().ToString();
    public double rotationMin = 0.0;
    public double rotationMax = 360.0;
    public bool zoomAdjust = false;
    public double zoomAdjustAmount = 10f;

    public void Awake() {
    }
    
    public void Start() {
        //Init();
    }
    
    public void Init() {

    }

    public void LoadPlayer() {        
        LoadPlayer(customCharacterData.characterCode);
    }

    public void LoadPlayer(GameCustomCharacterData customCharacterDataTo) {
        customCharacterData = customCharacterDataTo;

        LoadPlayer(customCharacterData.characterCode);
    }
    
    public void OnEnable() {
        Messenger<string>.AddListener(
            GameCustomMessages.customCharacterPlayerChanged, 
            OnCustomCharacterPlayerChangedHandler);
    }
    
    public void OnDisable() {

        Messenger<string>.RemoveListener(
            GameCustomMessages.customCharacterPlayerChanged, 
            OnCustomCharacterPlayerChangedHandler);
    }
    
    public void OnCustomCharacterPlayerChangedHandler(string characterCode) {
        LoadPlayer(characterCode);
    }
    
    public void LoadPlayer(string characterCodeTo) {
        
        if (string.IsNullOrEmpty(characterCodeTo)) {
            return;
        }

        customCharacterData.characterCode = characterCodeTo;
        
        if (containerPlayerDisplay == null) {
            return;
        }

        string gameCharacterCode = customCharacterData.characterCode;

        if (isProfileCharacterCode) {
        
            GameProfileCharacterItem gameProfileCharacterItem = 
                GameProfileCharacters.Current.GetCharacter(
                    customCharacterData.characterCode);
            
            if (gameProfileCharacterItem == null) {
                return;
            }

            gameCharacterCode = gameProfileCharacterItem.characterCode;
        }
        
        GameCharacter gameCharacter = 
            GameCharacters.Instance.GetById(gameCharacterCode);
        
        if (gameCharacter == null) {
            return;
        }
        
        containerPlayerDisplay.DestroyChildren();
        
        GameObject go = gameCharacter.Load();
        
        if (go == null) {
            return;
        }
        
        go.transform.parent = containerPlayerDisplay.transform;
        go.transform.position = Vector3.zero;
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        go.transform.localRotation = Quaternion.identity;//.Euler(Vector3.zero.WithY(133));
        
        GameController.CurrentGamePlayerController.LoadCharacter(gameCharacter.data.GetModel().code);
        
        GameCustomController.BroadcastCustomSync();
                
        go.SetLayerRecursively(gameObject.layer);

        // LOAD UP PASSED IN VALUES

        customPlayerObject = go.GetOrSet<GameCustomPlayer>();

        if (customPlayerObject != null) {
            customPlayerObject.Change(customCharacterData);
        }
                
        if (containerRotator != null) {
            containerRotator.ResetObject();
        }
    }

    public void UpdateScale() {
        
        if (containerPlayerDisplay == null) {
            return;
        }
        
        if (containerPlayerDisplay.transform.childCount == 0) {
            return;
        }
        
        if (containerRotator == null) {
            return;
        }

        string keyScale = "scale-" + uuid;

        if (AnimationEasing.EaseExists(keyScale)) {
        
            AnimationEasing.AnimationItem aniItem = AnimationEasing.EaseGet(keyScale);

            currentContainerScale = aniItem.val;//AnimationEasing.EaseGetValue(keyScale, 1.0f);

            if (currentContainerScale != scaleMax || currentContainerScale != scaleMin) {

                currentContainerScale = (double)Mathf.Clamp((float)currentContainerScale, (float)scaleMin, (float)scaleMax);
        
                //Debug.Log("UpdateScale:" + " currentContainerScale:" + currentContainerScale);

                float scaleTo = (float)currentContainerScale;
                float zoomAdjustY = 0f;

                containerRotator.transform.localScale = 
                    Vector3.zero
                        .WithX(scaleTo)
                        .WithY(scaleTo)
                        .WithZ(scaleTo);

                zoomAdjustY = -(Mathf.Abs((float)aniItem.valEnd - scaleTo) / 5);

                if (zoomAdjust && aniItem.valEnd > aniItem.valStart) {
                    zoomAdjustY = -(scaleTo / (float)zoomAdjustAmount);
                }

                containerRotator.transform.localPosition = Vector3.zero.WithY(zoomAdjustY);
                ;
            }
        }
    }
    
    public void HandleContainerScale(double valEnd) {

        HandleContainerScale(currentContainerScale, valEnd);
    }

    public void HandleContainerScale(double valStart, double valEnd) {
    
        
        if (containerPlayerDisplay == null) {
            return;
        }
        
        if (containerPlayerDisplay.transform.childCount == 0) {
            return;
        }
        
        if (containerRotator == null) {
            return;
        }
        
        string keyScale = "scale-" + uuid;

        //Debug.Log("HandleContainerScale:" + " valStart:" + valStart + " valEnd:" + valEnd);

        AnimationEasing.EaseAdd(
            keyScale, 
            AnimationEasing.Equations.QuadEaseInOut, 
            currentContainerScale, 
            valStart, 
            valEnd, 
            .5, 
            .1
        );
    }
    
    public void HandleContainerRotation(double valEnd) {        

        HandleContainerRotation(currentContainerRotation, valEnd);
    }
        
    public void HandleContainerRotation(double valStart, double valEnd) {
        
        
        if (containerPlayerDisplay == null) {
            return;
        }
        
        if (containerPlayerDisplay.transform.childCount == 0) {
            return;
        }
        
        if (containerRotator == null) {
            return;
        }

        containerRotator.ResetRigidBodiesVelocity();
        
        string keyRotation = "rotation-" + uuid;
        
        //Debug.Log("HandleContainerRotation:" + " valStart:" + valStart + " valEnd:" + valEnd);
        
        AnimationEasing.EaseAdd(keyRotation, AnimationEasing.Equations.QuadEaseInOut, currentContainerRotation, valStart, valEnd, .5, .1);
    }
    
    public void UpdateRotator() {

        if (containerPlayerDisplay == null) {
            return;
        }

        if (containerPlayerDisplay.transform.childCount == 0) {
            return;
        }

        if (containerRotator == null) {
            return;
        }

        if (rotatorRigidbody == null) {
            rotatorRigidbody = containerRotator.GetOrSet<Rigidbody>();
        }
        
        if (rotateObject == null) {
            rotateObject = containerRotator.GetOrSet<RotateObject>();
        }
        
        if (rotatorCollider == null) {
            rotatorCollider = containerRotator.GetOrSet<CapsuleCollider>();
        }

        rotateObject.enabled = false;

        if (allowRotator) {   
            
            if (rotatorRigidbody != null) {
                rotatorRigidbody.constraints = RigidbodyConstraints.FreezePositionX
                    | RigidbodyConstraints.FreezePositionY
                    | RigidbodyConstraints.FreezePositionZ
                    | RigidbodyConstraints.FreezeRotationX
                    | RigidbodyConstraints.FreezeRotationZ;
            }
            
            //if (rotateObject != null) {
            //    rotateObject.RotateSpeedAlongY = 2;
            //}
            
            string keyRotation = "rotation-" + uuid;
            
            if (AnimationEasing.EaseExists(keyRotation)) {
                
                currentContainerRotation = AnimationEasing.EaseGetValue(keyRotation, 0.0f);
                
                currentContainerRotation = (double)Mathf.Clamp((float)currentContainerRotation, (float)rotationMin, (float)rotationMax);
                
                //Debug.Log("UpdateScale:" + " currentContainerScale:" + currentContainerScale);
                
                float rotateTo = (float)currentContainerRotation;

                containerRotator.transform.localRotation = 
                    Quaternion.Euler(Vector3.zero
                                     .WithX(0)
                                     .WithY(rotateTo * 360)
                                     .WithZ(0));
            }
        }
        else {
            
            containerRotator.ResetObject();
            
            if (rotatorRigidbody != null) {
                rotatorRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
            
            if (rotateObject != null) {
                rotateObject.RotateSpeedAlongY = 0;
            }
        }
    }
    
    void Update() {
        
        UpdateRotator();
        UpdateScale();
    }
}