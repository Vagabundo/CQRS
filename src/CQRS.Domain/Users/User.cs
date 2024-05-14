using CQRS.Domain.Abstractions;

namespace CQRS.Domain.Users;

public sealed class User : Entity
{
    public Name? Name { get; private set; }
    public FamilyName? FamilyName { get; private set;}
    public Email Email { get; private set; }

    private User(Guid id, Name name, FamilyName familyName, Email email) : base(id)
    {
        Name = name;
        FamilyName = familyName;
        Email = email;
    }

    public static User Create(Name name, FamilyName familyName, Email email)
    {
        return new User(Guid.NewGuid(), name, familyName, email);
    }
}
