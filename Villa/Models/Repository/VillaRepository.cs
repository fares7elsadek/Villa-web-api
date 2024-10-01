using VillaApp.Models.Data;
using VillaApp.Models.Repository.IRepository;

namespace VillaApp.Models.Repository
{
	public class VillaRepository : Repository<Villas>, IVillasRepository
	{
		private readonly AppDbContext _db;
		public VillaRepository(AppDbContext db) : base(db)
		{
			this._db = db;
		}

		public void Update(Villas villa)
		{
			this._db.villas.Update(villa);
		}
	}
}
