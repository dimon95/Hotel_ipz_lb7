using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Hotel.Services;
using Hotel.Dto;

using Hotel.Web.Filters;

namespace Hotel.Web.Controllers
{
    [Filters.Exception]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;

        private void ChangeCriterias ( Guid id, bool onRestavration, IDictionary<string, bool> sCriterias )
        {
            RoomDto r = _roomService.View(id);

            int criterias = Model.Utils.ToInt(sCriterias);

            if ( r.OnRestavration == true && onRestavration == false )
                _roomService.ResetPlaceFromRestavration( id );
            else if ( r.OnRestavration == false && onRestavration == true )
                _roomService.SetPlaceOnRestavration( id );

            foreach ( Model.SearchCriteria sc in Enum.GetValues( typeof( Model.SearchCriteria ) ) )
            {
                if ( Model.Utils.HasCriteria( criterias, sc ) &&
                    !Model.Utils.HasCriteria( r.SearchCriterias, sc ) )
                {
                    _roomService.SetCriteria( id, sc );
                }

                else if ( !Model.Utils.HasCriteria( criterias, sc ) &&
                           Model.Utils.HasCriteria( r.SearchCriterias, sc ) )
                {
                    _roomService.ResetCriteria( id, sc );
                }
            }
        }

        /*private List<Dto.RoomDto> RoomsByPrice ( decimal bPrice, decimal ePrice )
        {
            return ( _roomService.GetRooms( bPrice, ePrice ).OrderBy( r => r.Number ).ToList() );
        }

        private List<Dto.RoomDto> RoomsByCriteria ( int sCriteria )
        {
            return ( _roomService.GetRooms( sCriteria ).OrderBy( r => r.Number ).ToList() );
        }*/

        public RoomController ( IRoomService roomServ )
        {
            _roomService = roomServ;
        }

        // GET: Place
        public ActionResult Rooms()
        {
            List<RoomDto> rs = _roomService.GetRooms().OrderBy(r => r.Number).ToList();

            return View( _roomService.GetRooms().OrderBy(r => r.Number).ToList() );
        }

        /*public PartialViewResult FindRooms ( decimal begin, decimal end, bool onRestavration )
        {
            //int sCriteria = Model.Utils.ToInt(searchCriterias);

            List<Dto.RoomDto> res = RoomsByPrice(begin, end).ToList();

            if ( onRestavration )
                return PartialView( "AdminRoomsPartial", res.FindAll( r => r.OnRestavration == true ) );

            return PartialView( "AdminRoomsPartial", res );
        }*/

        

        [HttpGet]
        [Authentication]
        [Filters.Authorize( Dto.Role.Admin) ]
        public ActionResult AddRoom ()
        {
            return View( "AddRoom", new Models.AddRoomViewModel() );
        }

        [HttpPost]
        [Authentication]
        [Filters.Authorize( Dto.Role.Admin )]
        public ActionResult AddRoom ( Models.AddRoomViewModel m )
        {
            if ( ModelState.IsValid )
            {
                _roomService.CreateRoom(m.Number, m.Description, m.PersonsCount, m.BedCount, m.Price);

                return Redirect( Url.Action( "Rooms", "Room" ) );
            }

            return View( m );
        }

        [Authentication]
        [Filters.Authorize( Dto.Role.Admin )]
        public ActionResult DeleteRoom ( Guid id )
        {
            _roomService.DeletePlace( id );

            return Redirect( Url.Action( "Rooms", "Room" ) );
        }

        [HttpGet]
        [Authentication]
        [Filters.Authorize( Dto.Role.Admin )]
        public ActionResult UpdateRoom (Guid id)
        {
            Models.UpdateRoomViewModel m = new Models.UpdateRoomViewModel();

            RoomDto r = _roomService.View( id );

            m.Id = r.Id;
            m.Description = r.Description;
            m.Number = r.Number;
            m.Price = r.Price;
            m.PersonsCount = r.PersonsCount;
            m.BedCount = r.BedCount;
            m.OnRestavration = r.OnRestavration;
            m.Criterias = Model.Utils.SearchCriteriaToDictionary( r.SearchCriterias );

            return View( "UpdateRoom", m );
        }

        [HttpPost]
        [Authentication]
        [Filters.Authorize( Dto.Role.Admin )]
        public ActionResult UpdateRoom ( Models.UpdateRoomViewModel m )
        {
            if ( ModelState.IsValid )
            {
                _roomService.ChangeDescription( m.Id, m.Description );
                _roomService.ChangePrice( m.Id, m.Price );
                ChangeCriterias(m.Id, m.OnRestavration, m.Criterias);

                return Redirect( Url.Action( "Rooms", "Room" ) );
            }

            return View( m );
        }

        public JsonResult CheckRoomNumber ( int number )
        {
            if ( _roomService.HasRoomWithNumber( number ) )
                return Json( "Room with such number exists", JsonRequestBehavior.AllowGet );
            else
                return Json( true, JsonRequestBehavior.AllowGet );
        }
    }
}