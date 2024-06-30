namespace SSB.Shared.Attributes;

/// <summary>
/// This attribute is used to represent a string value
/// for a value in an enum.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class StringValueAttribute : Attribute
{
    // Attribution to Stefan Sedich's Blog Post:
    // https://weblogs.asp.net/stefansedich/enum-with-string-values-in-c

    #region Properties
    public string StringValue { get; protected set; }
    #endregion

    #region Constructor
    /// <summary>
    /// Constructor used to init a StringValue Attribute
    /// </summary>
    /// <param name="value"></param>
    public StringValueAttribute(string value) => StringValue = value;
    #endregion

}
