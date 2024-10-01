namespace VillaApp.Models.Repository.IRepository
{
	public interface IVillasRepository: IRepository<Villas>
	{
		void Update(Villas villa);
	}
}
