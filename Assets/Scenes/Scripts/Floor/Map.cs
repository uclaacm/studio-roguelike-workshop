using System.Collections.Generic;

// make it serializable for
// easy debugging (allow us
// to create a map in the editor)
[System.Serializable]
public class Map {
	public int Width = 10, Height = 10;
	public List<MapEntry> Entries;

	public Map(int w, int h){
		Width = w;
		Height = h;
		Entries = new(Width * Height);
	}

	public MapEntry Get(int x, int y){
		int idx = x + y * Width;
		return idx < Entries.Count ? Entries[idx] : MapEntry.Empty;
	}
}

[System.Serializable]
public enum MapEntryType {
	Empty,
	NormalRoom,
	StartRoom,
	ItemRoom,
	BossRoom,
}

[System.Serializable]
public class MapEntry {
	public static MapEntry Empty = new() { Type = MapEntryType.Empty };

	public MapEntryType Type = MapEntryType.Empty;

	public bool IsEmpty => Type == MapEntryType.Empty;
}