using VillaApp.Models.Data;
using VillaApp.Models.Repository.IRepository;

namespace VillaApp.Models.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _db;
		public IVillasRepository Villas { get; private set; }
		public IVillaNumberRepository VillaNumber { get; private set; }
		public IUserRepository User {  get; private set; }

		public UnitOfWork(AppDbContext db,
			IVillasRepository Villas,
			IVillaNumberRepository VillaNumber,
			IUserRepository User)
        {
            this._db = db;
			this.VillaNumber = VillaNumber;
			this.User = User;
			this.Villas = Villas;
        }
		public async Task SaveAsync()
		{
			await _db.SaveChangesAsync();
		}
	}
}
