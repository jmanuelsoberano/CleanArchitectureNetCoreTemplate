namespace CleanArchitectureNetCore.Application.Common;

public class Sort<TDto>
{
    private readonly Dictionary<string, string> _fielsToSort = new Dictionary<string, string>();
    private readonly string _orderBy;
    private readonly Dictionary<string, string> _properties = new Dictionary<string, string>();
    private bool _ascendingSort = true;
    private string _preTable = "resultado";

    public Sort(string orderBy)
    {
        _orderBy = orderBy;
        MapProperties();
        MapOrderBy();
    }

    public Sort<TDto> Set(string preTable)
    {
        _preTable = preTable;

        return this;
    }

    public bool ContainOrderBy()
    {
        return !string.IsNullOrWhiteSpace(_orderBy);
    }

    public bool IsValid()
    {
        return _fielsToSort.Select(s => _properties.ContainsKey(s.Key)).All(w => w);
    }

    public override string ToString()
    {
        if (ContainOrderBy())
            return string.Join("'", _fielsToSort.Select(s => _preTable + "." + _properties[s.Key] + s.Value));

        return "(SELECT 1)";
    }

    private void MapProperties()
    {
        foreach (var propertyInfo in typeof(TDto).GetProperties())
            _properties.Add(propertyInfo.Name.ToLower(), propertyInfo.Name);
    }

    private void MapOrderBy()
    {
        var orderByAfterSplit = _orderBy.Split(',');
        foreach (var orderByClause in orderByAfterSplit)
        {
            var trimmedOrderByClause = orderByClause.Trim();
            var orderDescending = trimmedOrderByClause.EndsWith(" desc");
            var indexOfFirstSpace = trimmedOrderByClause.IndexOf(" ");
            var propertyName = indexOfFirstSpace == -1
                ? trimmedOrderByClause
                : trimmedOrderByClause.Remove(indexOfFirstSpace);
            var order = orderDescending ? " desc" : " asc";
            _fielsToSort.Add(propertyName.ToLower(), order);
        }
    }
}