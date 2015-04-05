﻿using UnityEngine;
using System;
using System.Collections.Generic;

public class BattleController {
	private static BattleController INSTANCE;

	private Battle battle;
	private IDictionary<string, Sprite> itemSprites;

	public int TargetScore {
		get { return 70; }
	}

	public BattleController(ClothingData.ClothingStyle style) {
		ClothingManager manager = ClothingManager.GetInstance();
		// TODO: Make target score dynamic for battle
		battle = new Battle(manager, style, 70);
		itemSprites = new Dictionary<string, Sprite>();

		foreach (ClothingData datum in manager.GetClothingData()) {
			itemSprites.Add(datum.Path, Resources.Load<Sprite>(datum.Path));
		}
	}

	public ClothingData GenerateRandomItem() {
		return battle.GenerateRandomItem();
	}

	public Sprite GetCurrentItemSprite() {
		return itemSprites[battle.CurrentItem.Path];
	}

	public string RemainingTime(float delta) {
		int remainingTime = (int)Math.Ceiling(battle.RemainingTime(delta));
		int minutes = remainingTime / 60;
		int seconds = remainingTime - (60 * minutes);
		return string.Format(generateNumberFormat(minutes, 0) + ":" + generateNumberFormat(seconds, 1),
			minutes, seconds);
	}

	// Should be called AFTER RemainingTime() as it doesn't update the timer
	public bool TimeOut() {
		return battle.TimeOut();
	}

	public int UpdateOutfitScore(ClothingData data) {
		battle.UpdateOutfit(data);
		return battle.OutfitScore;
	}

	public int RemoveItem(ClothingData data) {
		battle.RemoveItem(data);
		return battle.OutfitScore;
	}

	// TODO: Handle end of battle sequence
	public void AcceptOutfit() {
	}

	private string generateNumberFormat(int number, int index) {
		return string.Format(number < 10 ? "0{{{0}}}" : "{{{0}}}", index);
	}
}
