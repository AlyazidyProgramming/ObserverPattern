internal class Example1
{
   private static void Main(string[] args)
   {
      Console.WriteLine();
      IUser Saleh = new User("Saleh");
      IUser Omar = new User("Omar");

      IFollower Saman = new Follower("Saman");
      Saleh.Follow(Saman);
      Omar.Follow(Saman);
      ((User)Saleh).Post("Hi");
      Console.ReadKey();
   }
}

//Subject interface
internal interface IUser
{
   string Name { get; }

   void Follow(IFollower follower);

   void UnFollow(IFollower follower);

   void NotifyFollowers(string message);
}

//concrete subject
internal class User : IUser
{
   private readonly string _name;
   private string? _message;
   private List<IFollower> _followers = new();
   public string Name => _name;

   public User(string name)
   {
      _name = name;
   }

   public void Post(string post)
   {
      Console.WriteLine($"Posted by:[{_name}],{_message}");
      NotifyFollowers(post);
   }

   public void Follow(IFollower follower)
   {
      _followers.Add(follower);
   }

   public void NotifyFollowers(string message)
   {
      _message = message;
      foreach (IFollower follower in _followers)
      {
         follower.Update(this, message);
      }
   }

   public void UnFollow(IFollower follower)
   {
      _followers.Remove(follower);
   }
}

//observer interface
internal interface IFollower
{
   void Update(IUser user, string message);
}

internal class Follower : IFollower
{
   private readonly string _name;

   public Follower(string name)
   {
      _name = name;
   }

   public void Update(IUser user, string post)
   {
      Console.WriteLine($"follower:[{_name}], received a new post from {user.Name}:{post}");
   }
}