using ReactiveUI;

namespace MimaSim.Models;

public class TableCellModel(string key, string value) : ReactiveObject
{
    public string Key { get; } = key;
    public string Value { get; set; } = value;
}