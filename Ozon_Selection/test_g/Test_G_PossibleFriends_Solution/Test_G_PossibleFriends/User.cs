namespace Test_G_PossibleFriends
{
    public class User
    {
        public int ID;
        public int CountFriends = 0;
        public List<int> Friends = new(5);
        public List<int> FutureFriends = new();

        public User(int id)
        {
            this.ID = id;
        }

        public void AddFriend(int friend)
        {
            if (CountFriends < 5)
            {
                this.Friends.Add(friend);
                CountFriends++;
                //Console.WriteLine($"friend {friend} add in {Id}, " + "count=" + CountFriens);
            }
            else
            {
                //Console.WriteLine("error: max 5 friends!");
            }
        }
    }

}
