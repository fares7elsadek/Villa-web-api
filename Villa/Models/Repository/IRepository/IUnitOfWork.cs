namespace VillaApp.Models.Repository.IRepository
{
	public interface IUnitOfWork
	{
		IVillasRepository Villas { get; }
		void Save();
	}
}
