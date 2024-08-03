namespace DotnetCouchbaseExample.Models
{
    public class UserInfo
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string ClerkId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
        public Presentation[]? Presentations { get; set; } 
    }

    public class Presentation
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = false;
        public Slide[]? Slides { get; set; }
    }

    public class Slide
    {
        public string Id { get; set; }
        public int TemplateId { get; set; } = 1;
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public Question[]? Questions { get; set; }
    }

    public class Question
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Score { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
        public Option[]? Options { get; set; }
    }

    public class Option
    {
        public string Id { get; set; }
        public string OptionText { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = false;
    }

}
