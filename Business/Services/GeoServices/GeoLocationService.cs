using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using WeVsVirus.Business.Exceptions;
using WeVsVirus.Models.Entities;

namespace WeVsVirus.Business.Services.GeoServices
{
    public interface IGeoLocationService
    {
        Task<Point> GetGeoLocationFromApiAsync(Address address);
        Task<Address> FillOutAddressWithApiAsync(Address address);
        Task<IEnumerable<Address>> GetRelatedAddressesWithApiAsync(Address address);
    }

    public class GeoLocationService : IGeoLocationService
    {
        public GeoLocationService(TomTomApiService tomTomApiService, GoogleMapsApiService googleMapsApiService,
        ILogger<GeoLocationService> logger)
        {
            TomTomApiService = tomTomApiService;
            GoogleMapsApiService = googleMapsApiService;
            Logger = logger;
        }
        private TomTomApiService TomTomApiService { get; set; }
        private GoogleMapsApiService GoogleMapsApiService { get; set; }
        private ILogger<GeoLocationService> Logger { get; }


        private static GeometryFactory GetGeometryFactory()
        {
            return NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
        }

        public async Task<Point> GetGeoLocationFromApiAsync(Address address)
        {
            GeometryFactory geometryFactory = GetGeometryFactory();
            Point geoLocation = null;
            try
            {
                var googleResponse = await GoogleMapsApiService.GetLatLongAsync(address);
                HandleGoogleResult(googleResponse, googleResult =>
                {
                    geoLocation = geometryFactory.CreatePoint(new Coordinate(googleResult.Geometry.Location.Lng, googleResult.Geometry.Location.Lat));
                });
            }
            catch
            {
                try
                {
                    var tomTomResponse = await TomTomApiService.GetLatLongAsync(address);
                    HandleTomTomResult(tomTomResponse, tomTomResult =>
                    {
                        geoLocation = geometryFactory.CreatePoint(new Coordinate(tomTomResult.Position.Lon, tomTomResult.Position.Lat));
                    });
                }
                catch (Exception e)
                {
                    Logger.LogError("Sowohl Google Maps API als auch Tom Tom Api haben einen Fehler zurückgegeben. {exceptionMessage}", e.Message);
                    throw;
                }
            }
            return geoLocation;
        }

        public async Task<IEnumerable<Address>> GetRelatedAddressesWithApiAsync(Address address)
        {
            try
            {
                var googleResponse = await GoogleMapsApiService.GetLatLongAsync(address);
                return HandleGoogleResults(googleResponse);
            }
            catch
            {
                try
                {

                    var tomTomResponse = await TomTomApiService.GetLatLongAsync(address);
                    return HandleTomTomResults(tomTomResponse);
                }
                catch (Exception e)
                {
                    Logger.LogError("Sowohl Google Maps API als auch Tom Tom Api haben einen Fehler zurückgegeben. {exceptionMessage}", e.Message);
                    throw;
                }
            }
        }

        public async Task<Address> FillOutAddressWithApiAsync(Address address)
        {
            GeometryFactory geometryFactory = GetGeometryFactory();
            try
            {
                var googleResponse = await GoogleMapsApiService.GetLatLongAsync(address);
                HandleGoogleResult(googleResponse, FillOutAddressWithGoogleMapsAction(address, geometryFactory));
            }
            catch
            {
                var tomTomResponse = await TomTomApiService.GetLatLongAsync(address);
                HandleTomTomResult(tomTomResponse, FillOutAddressWithTomTomAction(address, geometryFactory));
            }
            return address;
        }

        private static Action<TomTomResponse.TomTomResult> FillOutAddressWithTomTomAction(Address address, GeometryFactory geometryFactory)
        {
            return tomTomResult =>
            {
                address.City = tomTomResult.Address.LocalName;
                address.StreetAndNumber = $"{tomTomResult.Address.StreetName} {tomTomResult.Address.StreetNumber}";
                address.ZipCode = tomTomResult.Address.postalCode;

                address.GeoLocation = geometryFactory.CreatePoint(new Coordinate(tomTomResult.Position.Lon, tomTomResult.Position.Lat));
            };
        }

        private static Action<GoogleMapsResponse.GoogleMapsResult> FillOutAddressWithGoogleMapsAction(Address address, GeometryFactory geometryFactory)
        {
            return googleResult =>
            {
                address.City = googleResult.Address_Components.FirstOrDefault(component => component.Types.Any(type => type == "locality"))?.Long_Name;
                address.ZipCode = googleResult.Address_Components.FirstOrDefault(component => component.Types.Any(type => type == "postal_code"))?.Long_Name;
                address.StreetAndNumber = $"{googleResult.Address_Components.FirstOrDefault(component => component.Types.Any(type => type == "route"))?.Long_Name} {googleResult.Address_Components.FirstOrDefault(component => component.Types.Any(type => type == "street_number"))?.Long_Name}";
                address.GeoLocation = geometryFactory.CreatePoint(new Coordinate(googleResult.Geometry.Location.Lng, googleResult.Geometry.Location.Lat));
            };
        }

        private static List<Address> HandleGoogleResults(GoogleMapsResponse googleResponse)
        {
            if (googleResponse?.Status == "OK")
            {
                GeometryFactory geometryFactory = GetGeometryFactory();
                var googleResults = googleResponse.Results.Where(googleResult => googleResult.Types.Any(type => type == "street_address")).ToList();
                if (googleResults.Any())
                {
                    List<Address> addresses = new List<Address>();
                    foreach (var googleResult in googleResults)
                    {
                        addresses.Add(ConvertGoogleResultToAddress(googleResult, geometryFactory));
                    }
                    return addresses;
                }
                else
                {
                    throw new BadRequestHttpException("Die angegebene Adresse ist ungültig.");
                }
            }
            else
            {
                throw new InternalServerErrorHttpException("Ups! Es ist ein Serverfehler aufgetreten. Tut uns leid! Bitte melde uns diesen Fehler und versuche es später erneut.");

            }
        }

        private static Address ConvertGoogleResultToAddress(GoogleMapsResponse.GoogleMapsResult googleResult, GeometryFactory geometryFactory)
        {
            return new Address
            {
                City = googleResult.Address_Components.FirstOrDefault(component => component.Types.Any(type => type == "locality"))?.Long_Name,
                ZipCode = googleResult.Address_Components.FirstOrDefault(component => component.Types.Any(type => type == "postal_code"))?.Long_Name,
                StreetAndNumber = $"{googleResult.Address_Components.FirstOrDefault(component => component.Types.Any(type => type == "route"))?.Long_Name} {googleResult.Address_Components.FirstOrDefault(component => component.Types.Any(type => type == "street_number"))?.Long_Name}",
                GeoLocation = geometryFactory.CreatePoint(new Coordinate(googleResult.Geometry.Location.Lng, googleResult.Geometry.Location.Lat))
            };
        }

        private static Address ConvertTomTomResultToAddress(TomTomResponse.TomTomResult tomTomResult, GeometryFactory geometryFactory)
        {
            return new Address
            {
                City = tomTomResult.Address.LocalName,
                ZipCode = tomTomResult.Address.postalCode,
                StreetAndNumber = $"{tomTomResult.Address.StreetName} {tomTomResult.Address.StreetNumber}",
                GeoLocation = geometryFactory.CreatePoint(new Coordinate(tomTomResult.Position.Lon, tomTomResult.Position.Lat))
            };
        }


        private static void HandleGoogleResult(GoogleMapsResponse googleResponse, Action<GoogleMapsResponse.GoogleMapsResult> callback)
        {
            if (googleResponse?.Status == "OK")
            {
                var googleResult = googleResponse.Results.FirstOrDefault();
                if (googleResult != null && googleResult.Types.Any(type => type == "street_address"))
                {
                    callback(googleResult);
                }
                else
                {
                    throw new BadRequestHttpException("Die angegebene Adresse ist ungültig.");
                }
            }
            else
            {
                throw new InternalServerErrorHttpException("Ups! Es ist ein Serverfehler aufgetreten. Tut uns leid! Bitte melde uns diesen Fehler und versuche es später erneut.");

            }
        }

        private static List<Address> HandleTomTomResults(TomTomResponse tomTomResponse)
        {
            if (tomTomResponse?.Results != null)
            {
                GeometryFactory geometryFactory = GetGeometryFactory();
                var tomTomResults = tomTomResponse.Results?.Where(result => result.Type == "Point Address").ToList();
                if (tomTomResults.Any())
                {
                    List<Address> addresses = new List<Address>();
                    foreach (var tomTomResult in tomTomResults)
                    {
                        addresses.Add(ConvertTomTomResultToAddress(tomTomResult, geometryFactory));
                    }
                    return addresses;
                }
                else
                {
                    throw new BadRequestHttpException("Die angegebene Adresse ist ungültig.");
                }
            }
            else
            {
                throw new InternalServerErrorHttpException("Ups! Es ist ein Serverfehler aufgetreten. Tut uns leid! Bitte melde uns diesen Fehler und versuche es später erneut.");

            }
        }

        private static void HandleTomTomResult(TomTomResponse tomTomResponse, Action<TomTomResponse.TomTomResult> callback)
        {
            if (tomTomResponse?.Results != null)
            {
                var tomTomResult = tomTomResponse.Results?.FirstOrDefault(result => result.Type == "Point Address");
                if (tomTomResult != null)
                {
                    callback(tomTomResult);
                }
                else
                {
                    throw new BadRequestHttpException("Die angegebene Adresse ist ungültig.");
                }
            }
            else
            {
                throw new InternalServerErrorHttpException("Ups! Es ist ein Serverfehler aufgetreten. Tut uns leid! Bitte melde uns diesen Fehler und versuche es später erneut.");

            }
        }
    }
}