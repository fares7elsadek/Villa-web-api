namespace VillaApp.Models.Repository.IRepository
{
	public interface IUnitOfWork
	{
		IVillasRepository Villas { get; }
		IVillaNumberRepository VillaNumber { get; }

		IUserRepository User { get; }
		Task SaveAsync();
	}
}
