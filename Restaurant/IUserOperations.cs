public interface IUserOperations
{
    string Name { get; set; }
    string Email { get; set; }
    string Password { get; set; }
    bool IsAdmin { get; set; }
    bool IsSuperAdmin { get; set; }
    bool HasReserved { get; set; }
    Tuple<DateTime, DateTime> Time { get; set; }

    void UserMenu();
    bool UserInput(User currentUser, List<Table> tables);
}
