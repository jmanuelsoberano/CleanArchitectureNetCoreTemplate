namespace CleanArchitectureNetCore.Application.Contracts.Identity.Claims;

public sealed class Claim
{
    private readonly string _value;

    public static readonly Claim NULL = new("");

    public static readonly Claim CAN_EDITH_COURSE = new("CAN_EDITH_COURSE");
    public static readonly Claim CAN_CREATE_COURSE = new("CAN_CREATE_COURSE");
    public static readonly Claim CAN_DELETE_COURSE = new("CAN_DELETE_COURSE");
    public static readonly Claim CAN_VIEW_COURSE = new("CAN_VIEW_COURSE");
    public static readonly Claim CAN_VIEW_COURSES = new("CAN_VIEW_COURSES");

    public static readonly Claim CAN_EDITH_USER = new("CAN_EDITH_USER");
    public static readonly Claim CAN_CREATE_USER = new("CAN_CREATE_USER");
    public static readonly Claim CAN_DELETE_USER = new("CAN_DELETE_USER");
    public static readonly Claim CAN_VIEW_USER = new("CAN_VIEW_USER");
    public static readonly Claim CAN_VIEW_USERS = new("CAN_VIEW_USERS");

    public static readonly Claim CAN_EDITH_ROLE = new("CAN_EDITH_ROLE");
    public static readonly Claim CAN_CREATE_ROLE = new("CAN_CREATE_ROLE");
    public static readonly Claim CAN_DELETE_ROLE = new("CAN_DELETE_ROLE");
    public static readonly Claim CAN_VIEW_ROLE = new("CAN_VIEW_ROLE");
    public static readonly Claim CAN_VIEW_ROLES = new("CAN_VIEW_ROLES");

    private Claim(string value)
    {
        _value = value;
    }

    public static IEnumerable<Claim> Values
    {
        get
        {
            yield return CAN_EDITH_COURSE;
            yield return CAN_CREATE_COURSE;
            yield return CAN_DELETE_COURSE;
            yield return CAN_VIEW_COURSE;
            yield return CAN_VIEW_COURSES;

            yield return CAN_EDITH_USER;
            yield return CAN_CREATE_USER;
            yield return CAN_DELETE_USER;
            yield return CAN_VIEW_USER;
            yield return CAN_VIEW_USERS;

            yield return CAN_EDITH_ROLE;
            yield return CAN_CREATE_ROLE;
            yield return CAN_DELETE_ROLE;
            yield return CAN_VIEW_ROLE;
            yield return CAN_VIEW_ROLES;
        }
    }

    public override string ToString()
    {
        return _value;
    }

    public string GetName()
    {
        return string.Join("", _value.Split('_'));
    }
}