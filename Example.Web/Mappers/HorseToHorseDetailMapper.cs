using Example.Web.Interfaces;

namespace Example.Web.Mappers
{
    public class HorseToHorseDetailMapper : IMapper<Dto.Horse, Models.HorseDetail>
    {
        public Models.HorseDetail Map(Dto.Horse horse)
        {
            return new Models.HorseDetail
            {
                Id = horse.Id,
                Name = horse.Name,
                Starts = horse.Starts,
                Win = horse.Win,
                Place = horse.Place,
                Show = horse.Show,
                Earnings = horse.Earnings
            };
        }
    }
}