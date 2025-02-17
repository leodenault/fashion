﻿using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class OutfitItemTest {
	private OutfitItem item;

	[SetUp]
	public void SetUp() {
		item = new OutfitItem(null);
	}

	[Test]
	public void PointsShouldReturnZeroIfSlotNotFilled() {
		Assert.AreEqual(0, item.Points);
	}

	[Test]
	public void PointsShouldReturnTopMultiplierIfNoSynergies() {
		ClothingData itemStub = Substitute.For<ClothingData>();
		itemStub.Slot = ClothingData.ClothingSlot.TOP;
		item.Item = itemStub;
		Assert.AreEqual(30, item.Points);
	}

	[Test]
	public void PointsShouldReturnSynergizedTotalIfSynergies() {
		int synergyPoints = 4;
		ClothingData itemStub = Substitute.For<ClothingData>();
		itemStub.Slot = ClothingData.ClothingSlot.TOP;
		item.Item = itemStub;
		ISynergy synergyStub = Substitute.For<ISynergy>();
		synergyStub.GetPoints(Arg.Any<ClothingData>(), Arg.Any<ClothingData>()).Returns<int>(synergyPoints);
		item.ApplySynergyPoints(synergyPoints);
		Assert.AreEqual(150, item.Points);
	}

	[Test]
	public void PointsShouldReturnBaseIfSynergiesCleared() {
		int synergyPoints = 4;
		ClothingData itemStub = Substitute.For<ClothingData>();
		itemStub.Slot = ClothingData.ClothingSlot.TOP;
		item.Item = itemStub;
		ISynergy synergyStub = Substitute.For<ISynergy>();
		synergyStub.GetPoints(Arg.Any<ClothingData>(), Arg.Any<ClothingData>()).Returns<int>(synergyPoints);
		item.ApplySynergyPoints(synergyPoints);
		Assert.AreEqual(150, item.Points);
		item.ClearSynergies();
		Assert.AreEqual(30, item.Points);
	}
}
