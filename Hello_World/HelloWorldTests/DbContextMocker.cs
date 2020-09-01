using Microsoft.EntityFrameworkCore;
using Hello_World.Models;

namespace HelloWorldTests
{
    public static class DbContextMocker
    {
        public static MessageContext GetMessageContext(string dbName)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<MessageContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create instance of DbContext
            var dbContext = new MessageContext(options);

            return dbContext;
        }
    }
}
