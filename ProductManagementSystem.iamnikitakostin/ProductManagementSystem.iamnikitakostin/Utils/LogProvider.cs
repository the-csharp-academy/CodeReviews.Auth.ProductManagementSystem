using ProductManagementSystem.iamnikitakostin.Data;

namespace ProductManagementSystem.iamnikitakostin.Utils;

public class LogProvider : ILoggerProvider
{
        private readonly ApplicationDbContext _context;

        public LogProvider(ApplicationDbContext context)
        {
            _context = context;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DatabaseLogger(_context);
        }

        public void Dispose()
        {

        }

}
