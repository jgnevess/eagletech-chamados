namespace eagletechapi.dto;

public class ResponseList<T>
{
    public Dictionary<string, int> Quantities { get; set; } = [];
    public List<T> Data {get; set;} = [];
    
}