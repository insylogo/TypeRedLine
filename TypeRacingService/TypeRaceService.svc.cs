using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TypeRacingService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class TypeRaceService : ITypeRaceService
    {

        public void UpdateState(GameState gameState)
        {
            throw new NotImplementedException();
        }

        GameState ITypeRaceService.GetState(int raceId)
        {
            throw new NotImplementedException();
        }
    }
}
