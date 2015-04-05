﻿using System.Collections.Generic;

public class Inventory {

	public ClothingData[] Items {
		get { return inventory.ToArray(); }
	}

	private List<ClothingData> inventory;

	public Inventory() {
		inventory = new List<ClothingData>();
	}

	public void Add(ClothingData item) {
		this.inventory.Add(item);
	}

	public bool Contains(ClothingData item) {
		return inventory.Contains(item);
	}
}