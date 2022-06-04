using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Dependency injection of AppDbContext reference
        /// </summary>
        /// <param name="context">PlatformService.Data.AppDbContext</param>
        public PlatformRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreatePlatform(Platform plat)
        {
            if (plat == null)
            {
                throw new ArgumentNullException(nameof(plat));
            }

            _context.PlatForms.Add(plat);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.PlatForms.ToList();
        }

        public Platform GetPlatformById(int id)
        {
            return _context.PlatForms.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Any CRUD unsaved operation from the datasource will be flushed or saved to the database. 
        /// </summary>
        /// <returns>True if the changes were saved and false otherwise.</returns>
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}