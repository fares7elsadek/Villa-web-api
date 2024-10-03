namespace VillaApp.Models.Repository.IRepository
{
	public interface IVillaNumberRepository: IRepository<VillaNumber>
	{
		void Update(VillaNumber villaNumber);
	}
}
