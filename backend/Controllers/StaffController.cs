using System;
using System.Collections.Generic;

public class StaffController
{
    private List<Member> members = new List<Member>();
    private List<BloodRequest> bloodRequests = new List<BloodRequest>();
    private List<BlogPost> blogPosts = new List<BlogPost>();
    private List<Event> events = new List<Event>();

    public StaffController()
    {
        // Initialize with some sample data
        members.Add(new Member { Id = 1, Name = "John Doe", BloodType = "A+", ContactNumber = "1234567890" });
        bloodRequests.Add(new BloodRequest { Id = 1, PatientName = "Jane Smith", BloodType = "B+", Quantity = 2, Urgency = "High", Status = "Pending" });
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\nStaff Management System");
            Console.WriteLine("1. Manage Member Profiles");
            Console.WriteLine("2. Manage Blood Requests");
            Console.WriteLine("3. Manage Blog Posts");
            Console.WriteLine("4. Manage Events");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ManageMemberProfiles();
                    break;
                case "2":
                    ManageBloodRequests();
                    break;
                case "3":
                    ManageBlogPosts();
                    break;
                case "4":
                    ManageEvents();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private void ManageMemberProfiles()
    {
        while (true)
        {
            Console.WriteLine("\nMember Profile Management");
            Console.WriteLine("1. View All Members");
            Console.WriteLine("2. Search Member");
            Console.WriteLine("3. Update Member Status");
            Console.WriteLine("4. Back to Main Menu");
            Console.Write("Select an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ViewAllMembers();
                    break;
                case "2":
                    SearchMember();
                    break;
                case "3":
                    UpdateMemberStatus();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private void ViewAllMembers()
    {
        Console.WriteLine("\nAll Members:");
        foreach (var member in members)
        {
            Console.WriteLine($"ID: {member.Id}, Name: {member.Name}, Blood Type: {member.BloodType}, Contact: {member.ContactNumber}");
        }
    }

    private void SearchMember()
    {
        Console.Write("Enter member name or ID to search: ");
        var searchTerm = Console.ReadLine();

        var results = members.FindAll(m => 
            m.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || 
            m.Id.ToString() == searchTerm);

        if (results.Count == 0)
        {
            Console.WriteLine("No members found.");
            return;
        }

        Console.WriteLine("\nSearch Results:");
        foreach (var member in results)
        {
            Console.WriteLine($"ID: {member.Id}, Name: {member.Name}, Blood Type: {member.BloodType}");
        }
    }

    private void UpdateMemberStatus()
    {
        Console.Write("Enter member ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int memberId))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        var member = members.Find(m => m.Id == memberId);
        if (member == null)
        {
            Console.WriteLine("Member not found.");
            return;
        }

        Console.WriteLine($"Current status for {member.Name}: {member.Status}");
        Console.Write("Enter new status: ");
        member.Status = Console.ReadLine();
        Console.WriteLine("Member status updated successfully.");
    }

    private void ManageBloodRequests()
    {
        while (true)
        {
            Console.WriteLine("\nBlood Request Management");
            Console.WriteLine("1. View All Blood Requests");
            Console.WriteLine("2. Search Blood Requests");
            Console.WriteLine("3. Update Blood Request Status");
            Console.WriteLine("4. Search Compatible Donors");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Select an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ViewAllBloodRequests();
                    break;
                case "2":
                    SearchBloodRequests();
                    break;
                case "3":
                    UpdateBloodRequestStatus();
                    break;
                case "4":
                    SearchCompatibleDonors();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private void ViewAllBloodRequests()
    {
        Console.WriteLine("\nAll Blood Requests:");
        foreach (var request in bloodRequests)
        {
            Console.WriteLine($"ID: {request.Id}, Patient: {request.PatientName}, Blood Type: {request.BloodType}, Quantity: {request.Quantity}, Status: {request.Status}");
        }
    }

    private void SearchBloodRequests()
    {
        Console.Write("Enter patient name or blood type to search: ");
        var searchTerm = Console.ReadLine();

        var results = bloodRequests.FindAll(r => 
            r.PatientName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || 
            r.BloodType.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

        if (results.Count == 0)
        {
            Console.WriteLine("No blood requests found.");
            return;
        }

        Console.WriteLine("\nSearch Results:");
        foreach (var request in results)
        {
            Console.WriteLine($"ID: {request.Id}, Patient: {request.PatientName}, Blood Type: {request.BloodType}, Status: {request.Status}");
        }
    }

    private void UpdateBloodRequestStatus()
    {
        Console.Write("Enter blood request ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int requestId))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        var request = bloodRequests.Find(r => r.Id == requestId);
        if (request == null)
        {
            Console.WriteLine("Blood request not found.");
            return;
        }

        Console.WriteLine($"Current status for request {request.Id}: {request.Status}");
        Console.Write("Enter new status: ");
        request.Status = Console.ReadLine();
        Console.WriteLine("Blood request status updated successfully.");
    }

    private void SearchCompatibleDonors()
    {
        Console.Write("Enter blood type needed: ");
        var bloodType = Console.ReadLine();

        // Simple compatibility logic - in real system would be more complex
        var compatibleTypes = GetCompatibleBloodTypes(bloodType);
        var compatibleDonors = members.FindAll(m => compatibleTypes.Contains(m.BloodType));

        Console.WriteLine($"\nCompatible blood types for {bloodType}: {string.Join(", ", compatibleTypes)}");
        
        if (compatibleDonors.Count == 0)
        {
            Console.WriteLine("No compatible donors found.");
            return;
        }

        Console.WriteLine("\nCompatible Donors:");
        foreach (var donor in compatibleDonors)
        {
            Console.WriteLine($"ID: {donor.Id}, Name: {donor.Name}, Blood Type: {donor.BloodType}, Contact: {donor.ContactNumber}");
        }
    }

    private List<string> GetCompatibleBloodTypes(string bloodType)
    {
        // Simplified compatibility - real system would have more complex rules
        var compatibility = new Dictionary<string, List<string>>
        {
            {"A+", new List<string> {"A+", "A-", "O+", "O-"}},
            {"A-", new List<string> {"A-", "O-"}},
            {"B+", new List<string> {"B+", "B-", "O+", "O-"}},
            {"B-", new List<string> {"B-", "O-"}},
            {"AB+", new List<string> {"A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-"}},
            {"AB-", new List<string> {"A-", "B-", "AB-", "O-"}},
            {"O+", new List<string> {"O+", "O-"}},
            {"O-", new List<string> {"O-"}}
        };

        return compatibility.ContainsKey(bloodType) ? compatibility[bloodType] : new List<string>();
    }

    private void ManageBlogPosts()
    {
        while (true)
        {
            Console.WriteLine("\nBlog Post Management");
            Console.WriteLine("1. View All Blog Posts");
            Console.WriteLine("2. Create New Blog Post");
            Console.WriteLine("3. Update Blog Post");
            Console.WriteLine("4. Delete Blog Post");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Select an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ViewAllBlogPosts();
                    break;
                case "2":
                    CreateBlogPost();
                    break;
                case "3":
                    UpdateBlogPost();
                    break;
                case "4":
                    DeleteBlogPost();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private void ViewAllBlogPosts()
    {
        if (blogPosts.Count == 0)
        {
            Console.WriteLine("No blog posts available.");
            return;
        }

        Console.WriteLine("\nAll Blog Posts:");
        foreach (var post in blogPosts)
        {
            Console.WriteLine($"ID: {post.Id}, Title: {post.Title}, Created: {post.CreatedDate}, Author: {post.Author}");
        }
    }

    private void CreateBlogPost()
    {
        Console.Write("Enter blog post title: ");
        var title = Console.ReadLine();
        Console.Write("Enter blog post content: ");
        var content = Console.ReadLine();
        Console.Write("Enter your name as author: ");
        var author = Console.ReadLine();

        var newPost = new BlogPost
        {
            Id = blogPosts.Count + 1,
            Title = title,
            Content = content,
            Author = author,
            CreatedDate = DateTime.Now
        };

        blogPosts.Add(newPost);
        Console.WriteLine("Blog post created successfully.");
    }

    private void UpdateBlogPost()
    {
        Console.Write("Enter blog post ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int postId))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        var post = blogPosts.Find(p => p.Id == postId);
        if (post == null)
        {
            Console.WriteLine("Blog post not found.");
            return;
        }

        Console.WriteLine($"Current title: {post.Title}");
        Console.Write("Enter new title (leave blank to keep current): ");
        var newTitle = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newTitle))
        {
            post.Title = newTitle;
        }

        Console.WriteLine($"Current content: {post.Content}");
        Console.Write("Enter new content (leave blank to keep current): ");
        var newContent = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newContent))
        {
            post.Content = newContent;
        }

        Console.WriteLine("Blog post updated successfully.");
    }

    private void DeleteBlogPost()
    {
        Console.Write("Enter blog post ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int postId))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        var post = blogPosts.Find(p => p.Id == postId);
        if (post == null)
        {
            Console.WriteLine("Blog post not found.");
            return;
        }

        blogPosts.Remove(post);
        Console.WriteLine("Blog post deleted successfully.");
    }

    private void ManageEvents()
    {
        while (true)
        {
            Console.WriteLine("\nEvent Management");
            Console.WriteLine("1. View All Events");
            Console.WriteLine("2. Create New Event");
            Console.WriteLine("3. Update Event");
            Console.WriteLine("4. Delete Event");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Select an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ViewAllEvents();
                    break;
                case "2":
                    CreateEvent();
                    break;
                case "3":
                    UpdateEvent();
                    break;
                case "4":
                    DeleteEvent();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private void ViewAllEvents()
    {
        if (events.Count == 0)
        {
            Console.WriteLine("No events scheduled.");
            return;
        }

        Console.WriteLine("\nAll Events:");
        foreach (var ev in events)
        {
            Console.WriteLine($"ID: {ev.Id}, Title: {ev.Title}, Date: {ev.EventDate}, Location: {ev.Location}");
        }
    }

    private void CreateEvent()
    {
        Console.Write("Enter event title: ");
        var title = Console.ReadLine();
        Console.Write("Enter event date (MM/dd/yyyy): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime eventDate))
        {
            Console.WriteLine("Invalid date format.");
            return;
        }
        Console.Write("Enter event location: ");
        var location = Console.ReadLine();
        Console.Write("Enter event description: ");
        var description = Console.ReadLine();

        var newEvent = new Event
        {
            Id = events.Count + 1,
            Title = title,
            EventDate = eventDate,
            Location = location,
            Description = description
        };

        events.Add(newEvent);
        Console.WriteLine("Event created successfully.");
    }

    private void UpdateEvent()
    {
        Console.Write("Enter event ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int eventId))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        var ev = events.Find(e => e.Id == eventId);
        if (ev == null)
        {
            Console.WriteLine("Event not found.");
            return;
        }

        Console.WriteLine($"Current title: {ev.Title}");
        Console.Write("Enter new title (leave blank to keep current): ");
        var newTitle = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newTitle))
        {
            ev.Title = newTitle;
        }

        Console.WriteLine($"Current date: {ev.EventDate}");
        Console.Write("Enter new date (MM/dd/yyyy, leave blank to keep current): ");
        var dateInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(dateInput) && DateTime.TryParse(dateInput, out DateTime newDate))
        {
            ev.EventDate = newDate;
        }

        Console.WriteLine($"Current location: {ev.Location}");
        Console.Write("Enter new location (leave blank to keep current): ");
        var newLocation = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newLocation))
        {
            ev.Location = newLocation;
        }

        Console.WriteLine("Event updated successfully.");
    }

    private void DeleteEvent()
    {
        Console.Write("Enter event ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int eventId))
        {
            Console.WriteLine("Invalid ID format.");
            return;
        }

        var ev = events.Find(e => e.Id == eventId);
        if (ev == null)
        {
            Console.WriteLine("Event not found.");
            return;
        }

        events.Remove(ev);
        Console.WriteLine("Event deleted successfully.");
    }
}

// Model classes
public class Member
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string BloodType { get; set; }
    public string ContactNumber { get; set; }
    public string Status { get; set; } = "Active";
}

public class BloodRequest
{
    public int Id { get; set; }
    public string PatientName { get; set; }
    public string BloodType { get; set; }
    public int Quantity { get; set; }
    public string Urgency { get; set; }
    public string Status { get; set; }
}

public class BlogPost
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class Event
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime EventDate { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
}
