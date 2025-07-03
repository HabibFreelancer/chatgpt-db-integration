public class Setting
{
    /// <summary>
    /// Unique identifier for the setting (Primary Key).
    /// </summary>
    public int SettingId { get; set; }

    /// <summary>
    /// The name or key of the setting.
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// The value assigned to the setting.
    /// </summary>
    public string Value { get; set; } = string.Empty;
}
