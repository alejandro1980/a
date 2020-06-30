using Challenge.Models;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Challenge.Controllers
{
    public class ImportController : Controller
    {
        // GET: Import
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Post method for importing users 
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                try
                {
                    string fileExtension = Path.GetExtension(postedFile.FileName);

                    //Validate uploaded file and return error.
                    if (fileExtension != ".csv")
                    {
                        ViewBag.Message = "Please select the csv file with .csv extension";
                        return View();
                    }
                    var estadistica = new Estadistica();

                    var socios = new List<Socio>();                   

                    using (var sreader = new StreamReader(postedFile.InputStream))
                    {
                        //Loop through all the records
                        while (!sreader.EndOfStream)
                        {
                            string[] rows = sreader.ReadLine().Split(';');

                            socios.Add(new Socio
                            {
                                
                                SocioName = rows[0].ToString(),
                                SocioAge = int.Parse(rows[1].ToString()),
                                SocioTeam = rows[2].ToString(),
                                SocioCivilStatus = rows[3].ToString(),
                                SocioStudies = rows[4].ToString()
 
                        });
                        }
                    }

                    //Cantidad total de personas registradas.
                    var socios1 = socios.Count();

                    ViewBag.Socio1 = socios1;

                    //El promedio de edad de los socios de Racing.
                    //El promedio de edad de los socios de Racing.
                    var socios2 =  (from s in socios where s.SocioTeam=="Racing" select s.SocioAge).Average();

                    ViewBag.Socio2 = socios2;

                    //Un listado con las 100 primeras personas casadas, con estudios Universitarios,
                    //ordenadas de menor a mayor según su edad. Por cada persona, mostrar: nombre, edad y equipo.


                    var socios3 = (from s in socios
                                   where s.SocioCivilStatus == "Casado" && s.SocioStudies == "Universitario"
                                   orderby s.SocioAge
                                   select s).Take(100);

                    ViewBag.Socio3 = socios3;
                    //Un listado con los 5 nombres más comunes entre los hinchas de River.

                    var socios4 = ((from s in socios
                                   where s.SocioTeam=="River"
                                   group s by s.SocioName into playername
                                   
                                   select new
                                   {
                                       Name = playername.Key,
                                       Count = playername.Count()
                                   } 
                                   ).OrderByDescending(y => y.Count)).Take(5);

                    ViewBag.Socio4 = socios4;

                    //Un listado, ordenado de mayor a menor según la cantidad de socios, que enumere,
                    //junto con cada equipo, el promedio de edad de sus socios, la menor edad registrada y 
                    //la mayor edad registrada.

                    var socios5 = from s in socios
                                   group s by s.SocioTeam into playerGroup
                                  //orderby playerGroup
                                  select new
                                   {
                                       Equipo = playerGroup.Key,
                                       Cantidad = playerGroup.Count(),
                                        PromedioEdad = playerGroup.Average(x => x.SocioAge),
                                      MinAge = playerGroup.Min(x => x.SocioAge),
                                      MaxAge = playerGroup.Max(x => x.SocioAge)
                                  };

                    ViewBag.Socio5 = socios5;



                    estadistica.total = socios;
                    return View("View", socios3);
                   // return View("View", estadistica);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
            }
            else
            {
                ViewBag.Message = "Please select the file first to upload.";
            }
            return View();
        }
    }
}