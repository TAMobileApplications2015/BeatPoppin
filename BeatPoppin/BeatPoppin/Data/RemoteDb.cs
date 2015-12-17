namespace BeatPoppin.Data
{
    using System;
    using BeatPoppin.Data.Contracts;
    using Parse;
    using Models;

    public class RemoteDb
    {
        public void Init()
        {
            ParseObject.RegisterSubclass<User>();
            ParseObject.RegisterSubclass<HighScore>();
            ParseClient.Initialize("c5uTlCk0PexNh2sM09UIGutoWyRBrJfvvZNeN9bn", "oXRQprbpaOYVFRd8PGDaOoM0lzeY528w8n6c4cLx");
        }
    }
}
