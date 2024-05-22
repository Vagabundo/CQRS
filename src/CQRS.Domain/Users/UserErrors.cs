using CQRS.Domain.Abstractions;

namespace CQRS.Domain.Users;

public static class UserErrors
{
    public static Error NotFound = new("User.NotFound", "No user found by given Id");
    public static Error InvalidCredentials = new("User.InvalidCredentials", "Invalid credentials");
}
