using VillaApp.Models.Data;
using VillaApp.Models.Repository.IRepository;

namespace VillaApp.Models.Repository
{
	public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
	{
		private readonly AppDbContext _db;
		public VillaNumberRepository(AppDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(VillaNumber villaNumber)
		{
			_db.Update(villaNumber);
		}
	}
}
