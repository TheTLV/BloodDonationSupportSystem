using System;
using System.Collections.Generic;

public class AdminController
{
    private List<User> users = new List<User>();
    private List<Staff> staffMembers = new List<Staff>();

    public AdminController()
    {
        // Initialize with some sample data
        users.Add(new User { Id = 1, Username = "admin", Password = "admin123", Role = "Admin" });
        staffMembers.Add(new Staff { Id = 1, Name = "Medical Staff 1", Role = "Doctor", Contact = "staff1@hospital.com" });
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\nAdmin Management System");
            Console.WriteLine("1. Manage Users");
            Console.WriteLine("2. Manage Staff");
            Console.WriteLine("3. View Dashboard");
            Console.WriteLine("4. Exit");
            Console.Write("Select an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ManageUsers();
                    break;
                case "2":
                    ManageStaff();
                    break;
                case "3":
                    ViewDashboard();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private void ManageUsers()
    {
        while (true)
        {
            Console.WriteLine("\nUser Management");
            Console.WriteLine("1. View All Users");
            Console.WriteLine("2. Create New User");
            Console.WriteLine("3. Update User");
            Console.WriteLine("4. Delete User");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Select an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ViewAllUsers();
                    break;
                case "2":
                    CreateUser();
                    break;
                case "3":
                    UpdateUser();
                    break;
                case "4":
                    DeleteUser();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private void ViewAllUsers()
    {
        Console.WriteLine("\nAll Users:");
        foreach (var user in users)
        {
            Console.WriteLine($"ID: {user.Id}, Username: {user.Username}, Role: {user.Role}");
        }
    }

    private void CreateUser()
    {
        Console.Write("Enter username: ");
        var username = Console.ReadLine();
        Console.Write("Enter password: ");
        var password = Console.ReadLine();
        Console.Write("Enter role (Admin/Staff/Member): ");
        var role = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
        {
            Console.WriteLine("All fields are required.");
            return;
        }

        var newUser = new User
        {
            Id = users.Count + 1,
            Username = username,
            Password = password,
            Role = role
        };

        users.Add(newUser);
        Console.WriteLine("User created successfully.");
    }

    private void UpdateUser()
    {
        Console.Write("Enter user ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        var user = users.Find(u => u.Id == userId);
        if (user == null)
        {
            Console.WriteLine("User not found.");
            return;
        }

        Console.WriteLine($"Current username: {user.Username}");
        Console.Write("Enter new username (leave blank to keep current): ");
        var newUsername = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newUsername))
        {
            user.Username = newUsername;
        }

        Console.WriteLine($"Current role: {user.Role}");
        Console.Write("Enter new role (leave blank to keep current): ");
        var newRole = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newRole))
        {
            user.Role = newRole;
        }

        Console.WriteLine("User updated successfully.");
    }

    private void DeleteUser()
    {
        Console.Write("Enter user ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        var user = users.Find(u => u.Id == userId);
        if (user == null)
        {
            Console.WriteLine("User not found.");
            return;
        }

        // Prevent deleting the last admin
        if (user.Role == "Admin" && users.FindAll(u => u.Role == "Admin").Count == 1)
        {
            Console.WriteLine("Cannot delete the last admin user.");
            return;
        }

        users.Remove(user);
        Console.WriteLine("User deleted successfully.");
    }

    private void ManageStaff()
    {
        while (true)
        {
            Console.WriteLine("\nStaff Management");
            Console.WriteLine("1. View All Staff");
            Console.WriteLine("2. Create New Staff");
            Console.WriteLine("3. Update Staff");
            Console.WriteLine("4. Delete Staff");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Select an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ViewAllStaff();
                    break;
                case "2":
                    CreateStaff();
                    break;
                case "3":
                    UpdateStaff();
                    break;
                case "4":
                    DeleteStaff();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private void ViewAllStaff()
    {
        Console.WriteLine("\nAll Staff Members:");
        foreach (var staff in staffMembers)
        {
            Console.WriteLine($"ID: {staff.Id}, Name: {staff.Name}, Role: {staff.Role}, Contact: {staff.Contact}");
        }
    }

    private void CreateStaff()
    {
        Console.Write("Enter staff name: ");
        var name = Console.ReadLine();
        Console.Write("Enter staff role: ");
        var role = Console.ReadLine();
        Console.Write("Enter contact information: ");
        var contact = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(role) || string.IsNullOrWhiteSpace(contact))
        {
            Console.WriteLine("All fields are required.");
            return;
        }

        var newStaff = new Staff
        {
            Id = staffMembers.Count + 1,
            Name = name,
            Role = role,
            Contact = contact
        };

        staffMembers.Add(newStaff);
        Console.WriteLine("Staff member created successfully.");
    }

    private void UpdateStaff()
    {
        Console.Write("Enter staff ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int staffId))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        var staff = staffMembers.Find(s => s.Id == staffId);
        if (staff == null)
        {
            Console.WriteLine("Staff member not found.");
            return;
        }

        Console.WriteLine($"Current name: {staff.Name}");
        Console.Write("Enter new name (leave blank to keep current): ");
        var newName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newName))
        {
            staff.Name = newName;
        }

        Console.WriteLine($"Current role: {staff.Role}");
        Console.Write("Enter new role (leave blank to keep current): ");
        var newRole = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newRole))
        {
            staff.Role = newRole;
        }

        Console.WriteLine("Staff member updated successfully.");
    }

    private void DeleteStaff()
    {
        Console.Write("Enter staff ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int staffId))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        var staff = staffMembers.Find(s => s.Id == staffId);
        if (staff == null)
        {
            Console.WriteLine("Staff member not found.");
            return;
        }

        staffMembers.Remove(staff);
        Console.WriteLine("Staff member deleted successfully.");
    }

    private void ViewDashboard()
    {
        Console.WriteLine("\nSystem Dashboard");
        Console.WriteLine($"Total Users: {users.Count}");
        Console.WriteLine($"Total Staff Members: {staffMembers.Count}");
        Console.WriteLine($"Admins: {users.FindAll(u => u.Role == "Admin").Count}");
        Console.WriteLine($"Staff Users: {users.FindAll(u => u.Role == "Staff").Count}");
        Console.WriteLine($"Members: {users.FindAll(u => u.Role == "Member").Count}");
    }
}

// Model classes
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } // Admin, Staff, Member
}

public class Staff
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; } // Doctor, Nurse, etc.
    public string Contact { get; set; }
}
