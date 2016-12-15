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
    public class HallController : Controller
    {
        private readonly IHallService _hallService;

        public HallController ( IHallService hallServ )
        {
            _hallService = hallServ;
        } 

        // GET: Hall
        public ActionResult Halls()
        {
            return View(_hallService.GetHalls().OrderBy(h => h.Number).ToList());
        }

        [HttpGet]
        [Authentication]
        [Filters.Authorize(Role.Admin)]
        public ActionResult AddHall ()
        {
            return View( new Models.AddHallViewModel() );
        }

        [HttpPost]
        [Authentication]
        [Filters.Authorize( Role.Admin )]
        public ActionResult AddHall ( Models.AddHallViewModel m )
        {
            if ( ModelState.IsValid )
            {
                _hallService.CreateHall( m.Number, m.Description, m.PersonsCount, m.Price );

                return Redirect( Url.Action( "Halls", "Hall" ) );
            }

            return View(m);
        }

        [Authentication]
        [Filters.Authorize( Role.Admin )]
        public ActionResult DeleteHall ( Guid id )
        {
            _hallService.DeletePlace( id );

            return Redirect( Url.Action( "Halls", "Hall" ) );
        }

        [HttpGet]
        [Authentication]
        [Filters.Authorize( Role.Admin )]
        public ActionResult UpdateHall (Guid id)
        {
            HallDto h = _hallService.View(id);

            Models.UpdateHallViewModel m = new Models.UpdateHallViewModel();

            m.Id = h.Id;
            m.Number = h.Number;
            m.Description = h.Description;
            m.Price = h.Price;
            m.PersonsCount = h.PersonsCount;
            m.OnRestavration = h.OnRestavration;

            return View( m );
        }

        [HttpPost]
        [Authentication]
        [Filters.Authorize( Role.Admin )]
        public ActionResult UpdateHall (Models.UpdateHallViewModel m)
        {
            if ( ModelState.IsValid )
            {
                _hallService.Update( m.Id, m.Description, m.Price );

                HallDto h = _hallService.View(m.Id);

                if ( h.OnRestavration && !m.OnRestavration )
                    _hallService.ResetPlaceFromRestavration( m.Id );
                else if ( !h.OnRestavration && m.OnRestavration )
                    _hallService.SetPlaceOnRestavration( m.Id );

                return Redirect( Url.Action( "Halls", "Hall" ) );
            }

            return View( m );
        }

        public JsonResult CheckHallNumber ( int number )
        {
            if ( _hallService.HasHallWithNumber( number ) )
                return Json( "Room with such number exists", JsonRequestBehavior.AllowGet );
            else
                return Json( true, JsonRequestBehavior.AllowGet );
        }
    }
}