using VillaApp.Models.Data;
using VillaApp.Models.Repository.IRepository;

namespace VillaApp.Models.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _db;
		public IVillasRepository Villas { get; private set; }

		public UnitOfWork(AppDbContext db)
        {
            this._db = db;
			Villas = new VillaRepository(this._db);
        }
		public void Save()
		{
			_db.SaveChanges();
		}
	}
}
