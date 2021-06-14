using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatteshipLiteLib.Models
{
    public class PlayerInfoModel
    {
        public string UserName { get; set; }
        public List<GridSpotModel> ShipLocations { get; set; } = new List<GridSpotModel>(); // 5 places a ship can be
        public List<GridSpotModel> ShotGrid { get; set; } = new List<GridSpotModel>();
    }
}
