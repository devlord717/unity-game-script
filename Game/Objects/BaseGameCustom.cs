using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Engine.Events;

public class GameCustomActorTypes {
    public static string heroType = "hero"; // used for customizer, defualt to profile, then allow changes.
    public static string enemyType = "enemy"; // use profile
    public static string sidekickType = "sidekick"; // call out a preset set
}

public class GameCustomTypes {
    public static string customType = "custom"; // used for customizer, defualt to profile, then allow changes.
    public static string defaultType = "default"; // use profile
    public static string explicitType = "explicit"; // call out a preset set
    public static string teamType = "team"; // call out a preset set
}

[Serializable]
public class GameCustomCharacterData {

    public string type = GameCustomTypes.defaultType;
    public string actorType = GameCustomActorTypes.heroType;
    public string teamCode = "default";
    public string presetType = "character";
    public string presetColorCodeDefault = "game-default";
    public string presetColorCode = GameCustomTypes.defaultType;
    public string presetTextureCodeDefault = GameCustomTypes.defaultType;
    public string presetTextureCode = GameCustomTypes.defaultType;
    public string characterCode = ProfileConfigs.defaultGameCharacterCode;
    public string characterDisplayName = ProfileConfigs.defaultGameCharacterDisplayName;
    public string characterDisplayCode = ProfileConfigs.defaultGameCharacterDisplayCode;

    public bool isCustomType {
        get {
            return type == GameCustomTypes.customType;
        }
    }
    
    public bool isDefaultType {
        get {
            return type == GameCustomTypes.defaultType;
        }
    }
    
    public bool isExplicitType {
        get {
            return type == GameCustomTypes.explicitType;
        }
    }
    
    public bool isTeamType {
        get {
            return type == GameCustomTypes.teamType;
        }
    }
    
    public bool isActorTypeHero {
        get {
            return actorType == GameCustomActorTypes.heroType;
        }
    }

    public bool isActorTypeEnemy {
        get {
            return actorType == GameCustomActorTypes.enemyType;
        }
    }

    public bool isActorTypeSidekick {
        get {
            return actorType == GameCustomActorTypes.sidekickType;
        }
    }

    public void SetActorEnemy() {
        SetActorType(GameCustomActorTypes.enemyType);
    }
    
    public void SetActorHero() {
        SetActorType(GameCustomActorTypes.heroType);
    }
    
    public void SetActorSidekick() {
        SetActorType(GameCustomActorTypes.sidekickType);
    }

    public void SetActorType(string actorTypeTo) {
        actorType = actorTypeTo;
    }
}

public class GameCustomCharacterDataCurrent {    
    
    public string lastCustomColorCode = "--";
    public string lastCustomTextureCode = "--";
    public string lastCustomDisplayName = "";
    public string lastCustomDisplayCode = "";
}

/*
[Serializable]
public class GameCharacterDataSet { 
    //public GameCustomCharacterObjectData characterObjectData;

    [HideInInspector]
    public GameCustomCharacterDataCurrent characterDataCurrent;

    public GameCustomCharacterData characterData;

    public GameCharacterDataSet() {
        Reset();
    }

    public void Reset() {

        //characterObjectData = 
        //    new GameCustomCharacterObjectData();

        characterData = 
            new GameCustomCharacterData();    

        characterDataCurrent = new GameCustomCharacterDataCurrent();
    }
}
*/

[Serializable]
public class GameCustomCharacterObjectData {    

    public GameCustomPlayer gameCustomPlayer;

    public GameCustomCharacterObjectData() {
    
    }

    public void Fill(GameObject go) {
        gameCustomPlayer = go.Get<GameCustomPlayer>();
    }
        
    public bool hasPlayer {
        get {
            return gameCustomPlayer != null 
                && gameCustomPlayer.isActorTypeHero;
        }
    }
}

public class BaseGameCustom : GameObjectBehavior {
    
    public GameCustomCharacterDataCurrent customCharacterDataCurrent;
    public GameCustomCharacterData customCharacterData;
    [HideInInspector]

    public GameCustomPlayerContainer
        gameCustomPlayerContainer;
    public bool freezeRotation = false;
    public bool resetPositionRotationModel = true;
    float lastCustomUpdate = 0;

    public virtual void Start() {

        Init();
    }

    public virtual void Init() {

        if(customCharacterDataCurrent == null) {
            customCharacterDataCurrent = new GameCustomCharacterDataCurrent();
        }
        
        if (customCharacterData == null) {
            customCharacterData = new GameCustomCharacterData();
                        
            if (customCharacterData.presetColorCode == GameCustomTypes.customType) {
                customCharacterData.type = GameCustomTypes.customType;
            }
            else if (customCharacterData.presetColorCode == GameCustomTypes.defaultType) {
                customCharacterData.type = GameCustomTypes.defaultType;
            }
            else {
                customCharacterData.type = GameCustomTypes.explicitType;
                customCharacterData.presetColorCode = customCharacterData.presetColorCodeDefault;
                customCharacterData.presetTextureCode = customCharacterData.presetTextureCodeDefault;
            }
            
            Load(customCharacterData);
        }
    }
    
    public virtual void OnEnable() {
        Messenger.AddListener(
            GameCustomMessages.customColorsChanged, 
            BaseOnCustomizationColorsChangedHandler);
    }
    
    public virtual void OnDisable() {
        Messenger.RemoveListener(
            GameCustomMessages.customColorsChanged, 
            BaseOnCustomizationColorsChangedHandler);
    }
    
    public void SetActorEnemy() {
        SetActorType(GameCustomActorTypes.enemyType);
    }
    
    public void SetActorHero() {
        SetActorType(GameCustomActorTypes.heroType);
    }
    
    public void SetActorSidekick() {
        SetActorType(GameCustomActorTypes.sidekickType);
    }

    public bool isActorTypeHero {
        get {

            if (customCharacterData == null) {
                return false;
            }
            
            return customCharacterData.isActorTypeHero;
        }
    }

    public bool isActorTypeSidekick {
        get {

            if (customCharacterData == null) {
                return false;
            }
            
            return customCharacterData.isActorTypeSidekick;
        }
    }

    public bool isActorTypeEnemy {
        get {

            if (customCharacterData == null) {
                return false;
            }
            
            return customCharacterData.isActorTypeEnemy;
        }
    }
    
    public void SetActorType(string actorTypeTo) {

        if (customCharacterData == null) {
            return;
        }

        customCharacterData.SetActorType(actorTypeTo);
    }
    
    /*
    public virtual void Load(string typeTo) {
        Load(typeTo, customActorType, typeTo, typeTo);
    }

    public virtual void Load(string typeTo, string actorType, string presetColorCodeTo, string presetTextureCodeTo) {

        GameCustomCharacterData customCharacterDataTo = new GameCustomCharacterData();
        customCharacterDataTo.type = typeTo;
        characterData.actorType = actorType;
        customCharacterDataTo.presetColorCode = presetColorCodeTo;
        customCharacterDataTo.presetTextureCode = presetTextureCodeTo;

        Load(customCharacterDataTo);
    }
    */

    public virtual void Load(GameCustomCharacterData customCharacterDataTo) {
        Change(customCharacterDataTo);
    }

    public virtual void Change(GameCustomCharacterData customCharacterDataTo) {

        customCharacterData = customCharacterDataTo;  

        if (gameCustomPlayerContainer != null) {
            gameCustomPlayerContainer.customCharacterData = customCharacterData;
        }

        //LogUtil.Log("GameCustomBase:Change:characterData:" + characterData.teamCode);

        if (customCharacterData != null) {
            //customCharacterData.presetColorCode = customCharacterData.presetColorCode;
            //customCharacterData.presetTextureCode = customCharacterData.presetTextureCode;
            
            //LogUtil.Log("GameCustomBase:Change:customColorCode:" + customColorCode);
            //LogUtil.Log("GameCustomBase:Change:customTextureCode:" + customTextureCode);

            if (!string.IsNullOrEmpty(customCharacterData.teamCode)
                && customCharacterData.teamCode != "default") {
                
                //LogUtil.Log("Loading TEAM Custom Type:characterData.teamCode:" + characterData.teamCode);

                GameTeam team = GameTeams.Instance.GetById(customCharacterData.teamCode);

                if (team != null) {

                    if (team.data != null) {
                        
                        customCharacterData.teamCode = team.code;
                        customCharacterData.type = GameCustomTypes.teamType;
                        
                        //LogUtil.Log("Loading TEAM EXISTS Type:teamCode:" + teamCode);                        
                        
                        GameDataTexturePreset itemTexture = team.data.GetTexturePreset();
                        
                        if (itemTexture != null) {  
                            customCharacterData.presetTextureCode = itemTexture.code;   
                            customCharacterDataCurrent.lastCustomColorCode = "--";
                        }

                        GameDataColorPreset itemColor = team.data.GetColorPreset();

                        if (itemColor != null) { 
                            customCharacterData.presetColorCode = itemColor.code;    
                            customCharacterDataCurrent.lastCustomTextureCode = "--";
                        }
                    } 
                }
            }
        }

        UpdatePlayer();
    }
    
    public virtual void UpdatePlayer() {

        if (customCharacterData == null) {
            Init();
        }
        /*
        LogUtil.Log("UpdatePlayer"  
                  + " type:" + characterData.type
                  + " presetType:" + characterData.presetType
                  + " presetColorCode:" + characterData.presetColorCode
                  + " presetTextureCode:" + characterData.presetTextureCode
                  + " isCustomType:" + characterData.isCustomType
                  + " isDefaultType:" + characterData.isDefaultType
                  + " isExplicitType:" + characterData.isExplicitType);
                  */

        if (customCharacterData.isCustomType 
            || customCharacterData.isTeamType) {
            return;
        }
        else if (customCharacterData.isDefaultType) {
            SetCustom();
        }
    }
    
    void BaseOnCustomizationColorsChangedHandler() {
        UpdatePlayer();

        //LogUtil.Log("BaseOnCustomizationColorsChangedHandler");
    }

    public void SetCustom() {

        if (customCharacterData == null) {
            Init();
        }

        SetCustomTextures();

        SetCustomColors();
    }
        
    public void SetCustomColors() {
        
        if (customCharacterData == null) {
            return;
        }

        //LogUtil.Log("SetCustomColors"  
        //          + " type:" + characterData.type
        //          + " presetType:" + characterData.presetType
        //          + " presetColorCode:" + characterData.presetColorCode
        //          + " presetTextureCode:" + characterData.presetTextureCode);
                  

        if (customCharacterData.isCustomType 
            || customCharacterData.isTeamType 
            || customCharacterData.isExplicitType) {
            return;
        }
        else if (customCharacterData.isDefaultType) {

            if (customCharacterData.actorType == GameCustomActorTypes.heroType) {

                GameProfileCustomItem customItem = GameProfileCharacters.currentCustom;
                
                //LogUtil.Log("SetCustomColors"  
                //         + " customItem:" + customItem.ToJson());

                if (customItem != null) {

                    if (!customItem.HasData()) {
                        
                        GameCustomController.UpdateColorPresetObject(
                            gameObject, 
                            AppColorPresets.Instance.GetByCode(
                                customCharacterData.presetColorCodeDefault));
                    }
                    else {

                        //customItem = GameCustomController.FillDefaultCustomColors(customItem, type);

                        GameCustomController.UpdateColorPresetObject(
                            customItem, 
                            gameObject, 
                            customCharacterData.presetType);
                    }
                }
                else {                
                    
                    GameCustomController.UpdateColorPresetObject(
                        gameObject, 
                        AppColorPresets.Instance.GetByCode(
                            customCharacterData.presetColorCodeDefault));
                }//GameCustomController.BroadcastCustomColorsChanged
            }
            else {    
                
                GameCustomController.UpdateColorPresetObject(
                    gameObject, 
                    AppColorPresets.Instance.GetByCode(
                        customCharacterData.presetColorCodeDefault));

            }//GameCustomController.BroadcastCustomColorsChanged
        }
    }
    
    public void SetCustomTextures() {
        
        if (customCharacterData == null) {
            return;
        }
        
        /*
        LogUtil.Log("SetCustomTextures"  
                  + " presetType:" + characterData.presetType
                  + " presetColorCode:" + characterData.presetColorCode
                  + " presetTextureCode:" + characterData.presetTextureCode);
                  */
        
        if (customCharacterData.isCustomType 
            || customCharacterData.isTeamType 
            || customCharacterData.isExplicitType) {
            return;
        }
        else if (customCharacterData.isDefaultType) {
            
            if (customCharacterData.actorType == GameCustomActorTypes.heroType) {
                
                GameProfileCustomItem customItem = GameProfileCharacters.currentCustom;
                
                if (customItem != null) {
                 
                    GameCustomController.UpdateTexturePresetObject(
                        customItem, 
                        gameObject, 
                        customCharacterData.presetType);
                }
                else {                
                    
                    GameCustomController.UpdateTexturePresetObject(
                        gameObject, 
                        AppContentAssetTexturePresets.Instance.GetByCode(
                        customCharacterData.presetTextureCodeDefault));
                }//GameCustomController.BroadcastCustomColorsChanged
            }
            else {                
                GameCustomController.UpdateTexturePresetObject(
                    gameObject, 
                    AppContentAssetTexturePresets.Instance.GetByCode(
                    customCharacterData.presetTextureCodeDefault));
            }
        }
    }
    
    public void HandleCustomPlayer() {
        
        if (customCharacterData == null) {
            Init();
        }

        HandleCustomPlayerTexture();
        HandleCustomPlayerColor();
        HandleCustomPlayerDisplayValues();
    }
    
    public void HandleCustomPlayerTexture() {

        if (customCharacterData == null) {
            Init();
        }

        if (customCharacterData.isCustomType 
            || customCharacterData.isDefaultType) {
            return;
        }
        else if (customCharacterDataCurrent.lastCustomTextureCode 
            != customCharacterData.presetTextureCode) {
            
            //if(AppColorPresets.Instance.CheckByCode(customTextureCode)) {
            
            //LogUtil.Log("HandleCustomPlayerColor:changing:" + 
            //          " lastCustomColorCode:" + lastCustomTextureCode + 
            //          " characterData.presetColorCode:" + characterData.presetTextureCode);
                
            AppContentAssetTexturePreset preset = 
                AppContentAssetTexturePresets.Instance.GetByCode(
                    customCharacterData.presetTextureCode);

            if (preset != null) {
                // load from current code
                GameCustomController.UpdateTexturePresetObject(
                    gameObject, preset);
            }
                
            customCharacterDataCurrent.lastCustomTextureCode = 
                customCharacterData.presetTextureCode;
            //}
        }
    }

    public void HandleCustomPlayerColor() {
        
        if (customCharacterData == null) {
            Init();
        }
        
        if (customCharacterData.isCustomType 
            || customCharacterData.isDefaultType) {
            return;
        }
        else if (customCharacterDataCurrent.lastCustomColorCode 
            != customCharacterData.presetColorCode) {
            
            if (AppColorPresets.Instance.CheckByCode(customCharacterData.presetColorCode)) {

                //LogUtil.Log("HandleCustomPlayerColor:changing:" + 
                //          " lastCustomColorCode:" + lastCustomColorCode + 
                //         " characterData.presetColorCode:" + characterData.presetColorCode);

                // load from current code
                AppColorPreset preset = AppColorPresets.Instance.GetByCode(
                    customCharacterData.presetColorCode);

                GameCustomController.UpdateColorPresetObject(
                    gameObject, preset);

                customCharacterDataCurrent.lastCustomColorCode = 
                    customCharacterData.presetColorCode;

            }
        }
    }

    public void HandleCustomPlayerDisplayValues() {
        if (customCharacterDataCurrent.lastCustomDisplayCode 
            != customCharacterData.characterDisplayCode 
            || customCharacterDataCurrent.lastCustomDisplayName 
            != customCharacterData.characterDisplayName) {

            GameCustomController.UpdateCharacterDisplay(
                gameObject, 
                customCharacterData.characterDisplayName, 
                customCharacterData.characterDisplayCode);
           
            customCharacterDataCurrent.lastCustomDisplayCode = customCharacterData.characterDisplayCode;
            customCharacterDataCurrent.lastCustomDisplayName = customCharacterData.characterDisplayName;
        }
    }
        
    public virtual void Update() {        
        
        if (resetPositionRotationModel) {
            
            gameObject.transform.localPosition = 
                Vector3.Lerp(gameObject.transform.localPosition, 
                             Vector3.zero, Time.deltaTime);
            
            gameObject.transform.localRotation = 
                Quaternion.Lerp(gameObject.transform.localRotation, 
                                Quaternion.identity, Time.deltaTime);
        }

        if (lastCustomUpdate + 1 < Time.time) {
            lastCustomUpdate = Time.time;

            HandleCustomPlayer();

            
            if (freezeRotation) {
                gameObject.transform.rotation = Quaternion.identity;
                gameObject.transform.localRotation = Quaternion.identity;
            }
        }

    }
}