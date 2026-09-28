using MSCLoader;
using System.Collections.Generic;
using UnityEngine;

namespace BetterNeeds
{
    public class BetterNeeds : Mod
    {
        public override string ID => "BetterNeeds";
        public override string Name => "BetterNeeds";
        public override string Author => "destruxxion";
        public override string Version => "1.3";
        public override string Description => "Shows the percentage of the player's needs, so you know when it's time to fill them";
        public override void ModSetup()
        {
            SetupFunction(Setup.ModSettings, Mod_Settings);
            SetupFunction(Setup.FixedUpdate, Mod_FixedUpdate);
            SetupFunction(Setup.OnNewGame, Mod_NewGame);
            SetupFunction(Setup.Update, Mod_Update);
            SetupFunction(Setup.OnLoad, Mod_OnLoad);
            SetupFunction(Setup.OnSave, Mod_OnSave);
        }
        private void Mod_Settings()
        {
            NeedsCore.Keybinds.Thirst = Keybind.Add(this, "Toggle Thirst", "Toggle Thirst", KeyCode.Alpha1, KeyCode.LeftAlt);
            NeedsCore.Keybinds.Hunger = Keybind.Add(this, "Toggle Hunger", "Toggle Hunger", KeyCode.Alpha2, KeyCode.LeftAlt);
            NeedsCore.Keybinds.Stress = Keybind.Add(this, "Toggle Stress", "Toggle Stress", KeyCode.Alpha3, KeyCode.LeftAlt);
            NeedsCore.Keybinds.Urine = Keybind.Add(this, "Toggle Urine", "Toggle Urine", KeyCode.Alpha4, KeyCode.LeftAlt);
            NeedsCore.Keybinds.Fatigue = Keybind.Add(this, "Toggle Fatigue", "Toggle Fatigue", KeyCode.Alpha5, KeyCode.LeftAlt);
            NeedsCore.Keybinds.Dirtiness = Keybind.Add(this, "Toggle Dirtiness", "Toggle Dirtiness", KeyCode.Alpha6, KeyCode.LeftAlt);
            NeedsCore.Keybinds.Background = Keybind.Add(this, "Toggle Background", "Toggle Background", KeyCode.Alpha7, KeyCode.LeftAlt);
        }
        private void Mod_NewGame()
        {
            SaveLoad.DeleteValue(this, "Thirst");
            SaveLoad.DeleteValue(this, "Hunger");
            SaveLoad.DeleteValue(this, "Stress");
            SaveLoad.DeleteValue(this, "Urine");
            SaveLoad.DeleteValue(this, "Fatigue");
            SaveLoad.DeleteValue(this, "Dirtiness");
            SaveLoad.DeleteValue(this, "Background");
        }
        private void Mod_OnLoad()
        {
            NeedsCore.Text.ThirstBG = GameObject.Find("GUI/HUD/Thrist/Pivot/HUDBar");
            NeedsCore.Text.HungerBG = GameObject.Find("GUI/HUD/Hunger/Pivot/HUDBar");
            NeedsCore.Text.StressBG = GameObject.Find("GUI/HUD/Stress/Pivot/HUDBar");
            NeedsCore.Text.UrineBG = GameObject.Find("GUI/HUD/Urine/Pivot/HUDBar");
            NeedsCore.Text.FatigueBG = GameObject.Find("GUI/HUD/Fatigue/Pivot/HUDBar");
            NeedsCore.Text.DirtinessBG = GameObject.Find("GUI/HUD/Dirty/Pivot/HUDBar");
            NeedsCore.Text.Needs = GameObject.Find("GUI/HUD/Hunger/HUDLabel");

            NeedsCore.Text.Thirst = Object.Instantiate(NeedsCore.Text.Needs);
            NeedsCore.Text.Hunger = Object.Instantiate(NeedsCore.Text.Needs);
            NeedsCore.Text.Stress = Object.Instantiate(NeedsCore.Text.Needs);
            NeedsCore.Text.Urine = Object.Instantiate(NeedsCore.Text.Needs);
            NeedsCore.Text.Fatigue = Object.Instantiate(NeedsCore.Text.Needs);
            NeedsCore.Text.Dirtiness = Object.Instantiate(NeedsCore.Text.Needs);

            NeedsCore.PlayMaker.ThirstValue = GameObject.Find("GUI/HUD/Thrist/Pivot").GetComponent<PlayMakerFSM>();
            NeedsCore.PlayMaker.HungerValue = GameObject.Find("GUI/HUD/Hunger/Pivot").GetComponent<PlayMakerFSM>();
            NeedsCore.PlayMaker.StressValue = GameObject.Find("GUI/HUD/Stress/Pivot").GetComponent<PlayMakerFSM>();
            NeedsCore.PlayMaker.UrineValue = GameObject.Find("GUI/HUD/Urine/Pivot").GetComponent<PlayMakerFSM>();
            NeedsCore.PlayMaker.FatigueValue = GameObject.Find("GUI/HUD/Fatigue/Pivot").GetComponent<PlayMakerFSM>();
            NeedsCore.PlayMaker.DirtinessValue = GameObject.Find("GUI/HUD/Dirty/Pivot").GetComponent<PlayMakerFSM>();

            List<GameObject> playerNeeds = new List<GameObject> { NeedsCore.Text.Thirst, NeedsCore.Text.Hunger, NeedsCore.Text.Stress, NeedsCore.Text.Urine, NeedsCore.Text.Fatigue, NeedsCore.Text.Dirtiness, NeedsCore.Text.Thirst.transform.GetChild(0).gameObject, NeedsCore.Text.Hunger.transform.GetChild(0).gameObject, NeedsCore.Text.Stress.transform.GetChild(0).gameObject, NeedsCore.Text.Urine.transform.GetChild(0).gameObject, NeedsCore.Text.Fatigue.transform.GetChild(0).gameObject, NeedsCore.Text.Dirtiness.transform.GetChild(0).gameObject };
            foreach (GameObject obj in playerNeeds)
            {
                TextMesh textMesh = obj.GetComponent<TextMesh>();
                textMesh.characterSize = 0.045f;            
            }

            if (SaveLoad.ValueExists(this, "Thirst"))
            {
                NeedsCore.Text.Thirst.SetActive(SaveLoad.ReadValue<bool>(this, "Thirst"));
                NeedsCore.Text.Hunger.SetActive(SaveLoad.ReadValue<bool>(this, "Hunger"));
                NeedsCore.Text.Stress.SetActive(SaveLoad.ReadValue<bool>(this, "Stress"));
                NeedsCore.Text.Urine.SetActive(SaveLoad.ReadValue<bool>(this, "Urine"));
                NeedsCore.Text.Fatigue.SetActive(SaveLoad.ReadValue<bool>(this, "Fatigue"));
                NeedsCore.Text.Dirtiness.SetActive(SaveLoad.ReadValue<bool>(this, "Dirtiness"));

                List<GameObject> playerBG = new List<GameObject> { NeedsCore.Text.ThirstBG, NeedsCore.Text.HungerBG, NeedsCore.Text.StressBG, NeedsCore.Text.UrineBG, NeedsCore.Text.FatigueBG, NeedsCore.Text.DirtinessBG };
                foreach (GameObject obj in playerBG)
                {
                    obj.SetActive(SaveLoad.ReadValue<bool>(this, "Background"));
                }
            }
        }
        private void Mod_FixedUpdate()
        {
            int intPercentThirst = Mathf.RoundToInt(Mathf.Clamp01((NeedsCore.PlayMaker.ThirstValue.FsmVariables.GetFsmFloat("PlayerThirst").Value - 0f) / (195f - 0f)) * 100f);
            int intPercentHunger = Mathf.RoundToInt(Mathf.Clamp01((NeedsCore.PlayMaker.HungerValue.FsmVariables.GetFsmFloat("PlayerHunger").Value - 0f) / (195f - 0f)) * 100f);
            int intPercentStress = Mathf.RoundToInt(Mathf.Clamp01((NeedsCore.PlayMaker.ThirstValue.FsmVariables.GetFsmFloat("PlayerStress").Value - 0f) / (195f - 0f)) * 100f);
            int intPercentUrine = Mathf.RoundToInt(Mathf.Clamp01((NeedsCore.PlayMaker.ThirstValue.FsmVariables.GetFsmFloat("PlayerUrine").Value - 0f) / (195f - 0f)) * 100f);
            int intPercentFatigue = Mathf.RoundToInt(Mathf.Clamp01((NeedsCore.PlayMaker.ThirstValue.FsmVariables.GetFsmFloat("PlayerFatigue").Value - 0f) / (195f - 0f)) * 100f);
            int intPercentDirtiness = Mathf.RoundToInt(Mathf.Clamp01((NeedsCore.PlayMaker.ThirstValue.FsmVariables.GetFsmFloat("PlayerDirtiness").Value - 0f) / (195f - 0f)) * 100f);

            NeedsCore.Text.Thirst.GetComponent<TextMesh>().text = $"{intPercentThirst}%";
            NeedsCore.Text.Hunger.GetComponent<TextMesh>().text = $"{intPercentHunger}%";
            NeedsCore.Text.Stress.GetComponent<TextMesh>().text = $"{intPercentStress}%";
            NeedsCore.Text.Urine.GetComponent<TextMesh>().text = $"{intPercentUrine}%";
            NeedsCore.Text.Fatigue.GetComponent<TextMesh>().text = $"{intPercentFatigue}%";
            NeedsCore.Text.Dirtiness.GetComponent<TextMesh>().text = $"{intPercentDirtiness}%";

            NeedsCore.Text.Thirst.transform.GetChild(0).GetComponent<TextMesh>().text = $"{intPercentThirst}%";
            NeedsCore.Text.Hunger.transform.GetChild(0).GetComponent<TextMesh>().text = $"{intPercentHunger}%";
            NeedsCore.Text.Stress.transform.GetChild(0).GetComponent<TextMesh>().text = $"{intPercentStress}%";
            NeedsCore.Text.Urine.transform.GetChild(0).GetComponent<TextMesh>().text = $"{intPercentUrine}%";
            NeedsCore.Text.Fatigue.transform.GetChild(0).GetComponent<TextMesh>().text = $"{intPercentFatigue}%";
            NeedsCore.Text.Dirtiness.transform.GetChild(0).GetComponent<TextMesh>().text = $"{intPercentDirtiness}%";

            if (intPercentThirst < 10)
            {
                NeedsCore.Text.Thirst.transform.localPosition = new Vector3(-9.2f, 9.411f, 0f);
            }
            else
            {
                NeedsCore.Text.Thirst.transform.localPosition = new Vector3(-9.14f, 9.411f, 0f);
            }
            if (intPercentHunger < 10)
            {
                NeedsCore.Text.Hunger.transform.localPosition = new Vector3(-9.2f, 9.011f, 0f);
            }
            else
            {
                NeedsCore.Text.Hunger.transform.localPosition = new Vector3(-9.14f, 9.011f, 0f);
            }
            if (intPercentStress < 10)
            {
                NeedsCore.Text.Stress.transform.localPosition = new Vector3(-9.2f, 8.611f, 0f);
            }
            else
            {
                NeedsCore.Text.Stress.transform.localPosition = new Vector3(-9.14f, 8.611f, 0f);
            }
            if (intPercentUrine < 10)
            {
                NeedsCore.Text.Urine.transform.localPosition = new Vector3(-9.2f, 8.211f, 0f);
            }
            else
            {
                NeedsCore.Text.Urine.transform.localPosition = new Vector3(-9.14f, 8.211f, 0f);
            }
            if (intPercentFatigue < 10)
            {
                NeedsCore.Text.Fatigue.transform.localPosition = new Vector3(-9.2f, 7.811f, 0f);
            }
            else
            {
                NeedsCore.Text.Fatigue.transform.localPosition = new Vector3(-9.14f, 7.811f, 0f);
            }
            if (intPercentDirtiness < 10)
            {
                NeedsCore.Text.Dirtiness.transform.localPosition = new Vector3(-9.2f, 7.411f, 0f);
            }
            else
            {
                NeedsCore.Text.Dirtiness.transform.localPosition = new Vector3(-9.14f, 7.411f, 0f);
            }
        }
        private void Mod_Update()
        {
            if (NeedsCore.Keybinds.Thirst.GetKeybindDown())
            {
                NeedsCore.Booleans.ThirstOn = !NeedsCore.Booleans.ThirstOn;
                NeedsCore.Text.Thirst.SetActive(NeedsCore.Booleans.ThirstOn);
            }
            if (NeedsCore.Keybinds.Hunger.GetKeybindDown())
            {
                NeedsCore.Booleans.HungerOn = !NeedsCore.Booleans.HungerOn;
                NeedsCore.Text.Hunger.SetActive(NeedsCore.Booleans.HungerOn);
            }
            if (NeedsCore.Keybinds.Stress.GetKeybindDown())
            {
                NeedsCore.Booleans.StressOn = !NeedsCore.Booleans.StressOn;
                NeedsCore.Text.Stress.SetActive(NeedsCore.Booleans.StressOn);
            }
            if (NeedsCore.Keybinds.Urine.GetKeybindDown())
            {
                NeedsCore.Booleans.UrineOn = !NeedsCore.Booleans.UrineOn;
                NeedsCore.Text.Urine.SetActive(NeedsCore.Booleans.UrineOn);
            }
            if (NeedsCore.Keybinds.Fatigue.GetKeybindDown())
            {
                NeedsCore.Booleans.FatigueOn = !NeedsCore.Booleans.FatigueOn;
                NeedsCore.Text.Fatigue.SetActive(NeedsCore.Booleans.FatigueOn);
            }
            if (NeedsCore.Keybinds.Dirtiness.GetKeybindDown())
            {
                NeedsCore.Booleans.DirtinessOn = !NeedsCore.Booleans.DirtinessOn;
                NeedsCore.Text.Dirtiness.SetActive(NeedsCore.Booleans.DirtinessOn);
            }
            if (NeedsCore.Keybinds.Background.GetKeybindDown())
            {    
                NeedsCore.Booleans.BackgroundOn = !NeedsCore.Booleans.BackgroundOn;
                NeedsCore.Text.ThirstBG.SetActive(NeedsCore.Booleans.BackgroundOn);
                NeedsCore.Text.HungerBG.SetActive(NeedsCore.Booleans.BackgroundOn);
                NeedsCore.Text.StressBG.SetActive(NeedsCore.Booleans.BackgroundOn);
                NeedsCore.Text.UrineBG.SetActive(NeedsCore.Booleans.BackgroundOn);
                NeedsCore.Text.FatigueBG.SetActive(NeedsCore.Booleans.BackgroundOn);
                NeedsCore.Text.DirtinessBG.SetActive(NeedsCore.Booleans.BackgroundOn);

            }
        }
        private void Mod_OnSave()
        {
            SaveLoad.WriteValue(this, "Thirst", NeedsCore.Booleans.ThirstOn);
            SaveLoad.WriteValue(this, "Hunger", NeedsCore.Booleans.HungerOn);
            SaveLoad.WriteValue(this, "Stress", NeedsCore.Booleans.StressOn);
            SaveLoad.WriteValue(this, "Urine", NeedsCore.Booleans.UrineOn);
            SaveLoad.WriteValue(this, "Fatigue", NeedsCore.Booleans.FatigueOn);
            SaveLoad.WriteValue(this, "Dirtiness", NeedsCore.Booleans.DirtinessOn);
            SaveLoad.WriteValue(this, "Background", NeedsCore.Booleans.BackgroundOn);
        }
    }
}