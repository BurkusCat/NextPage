namespace NextPage.Models;

public class DropdownOption<T>
    where T : struct, Enum
{
    public string Description { get; set; }

    public T Value { get; set; }
}
