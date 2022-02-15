using DtnAssesment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtnAssesment.Infrastructure.Services
{
    public class QuadkeyConverterService : IQuadkeyConverterService
    {
        private const double MinLatitude = -85.05112878;
        private const double MaxLatitude = 85.05112878;
        private const double MinLongitude = -180;
        private const double MaxLongitude = 180;
        private const int LevelOfDetail = 12;

        private readonly ILogService _logService;

        public QuadkeyConverterService(ILogService logService)
        {
            _logService = logService;
        }
        public string ConvertToQuadkey(double latitude, double longitude)
        {
            int pixelX;
            int pixelY;
            int tileX;
            int tileY;

            LatLongToPixelXY(latitude, longitude, LevelOfDetail, out pixelX, out pixelY);
            PixelXYToTileXY(pixelX, pixelY, out tileX, out tileY);
            return TileXYToQuadKey(tileX, tileY, LevelOfDetail);
            
        }


        /// <summary>  
        /// Converts a point from latitude/longitude WGS-84 coordinates (in degrees)  
        /// into pixel XY coordinates at a specified level of detail.  
        /// </summary>  
        /// <param name="latitude">Latitude of the point, in degrees.</param>  
        /// <param name="longitude">Longitude of the point, in degrees.</param>  
        /// <param name="levelOfDetail">Level of detail, from 1 (lowest detail)  
        /// to 23 (highest detail).</param>  
        /// <param name="pixelX">Output parameter receiving the X coordinate in pixels.</param>  
        /// <param name="pixelY">Output parameter receiving the Y coordinate in pixels.</param>  
        private void LatLongToPixelXY(double latitude, double longitude, int levelOfDetail, out int pixelX, out int pixelY)
        {
            var latitudeNew = Clip(latitude, MinLatitude, MaxLatitude);
            var longitudeNew = Clip(longitude, MinLongitude, MaxLongitude);

            if (latitudeNew != latitude)
            {
                _logService.Log($"Latitude has been clipped : {latitude} => {latitudeNew}");

            }
            if (longitudeNew != longitude)
            {
                _logService.Log($"Longitude has been clipped : {longitude} => {longitudeNew}");
            }

            double x = (longitude + 180) / 360;
            double sinLatitude = Math.Sin(latitude * Math.PI / 180);
            double y = 0.5 - Math.Log((1 + sinLatitude) / (1 - sinLatitude)) / (4 * Math.PI);

            uint mapSize = MapSize(levelOfDetail);
            pixelX = (int)Clip(x * mapSize + 0.5, 0, mapSize - 1);
            pixelY = (int)Clip(y * mapSize + 0.5, 0, mapSize - 1);
        }

        /// <summary>  
        /// Converts pixel XY coordinates into tile XY coordinates of the tile containing  
        /// the specified pixel.  
        /// </summary>  
        /// <param name="pixelX">Pixel X coordinate.</param>  
        /// <param name="pixelY">Pixel Y coordinate.</param>  
        /// <param name="tileX">Output parameter receiving the tile X coordinate.</param>  
        /// <param name="tileY">Output parameter receiving the tile Y coordinate.</param>  
        private void PixelXYToTileXY(int pixelX, int pixelY, out int tileX, out int tileY)
        {
            tileX = pixelX / 256;
            tileY = pixelY / 256;
        }

        /// <summary>  
        /// Converts tile XY coordinates into a QuadKey at a specified level of detail.  
        /// </summary>  
        /// <param name="tileX">Tile X coordinate.</param>  
        /// <param name="tileY">Tile Y coordinate.</param>  
        /// <param name="levelOfDetail">Level of detail, from 1 (lowest detail)  
        /// to 23 (highest detail).</param>  
        /// <returns>A string containing the QuadKey.</returns>  
        private string TileXYToQuadKey(int tileX, int tileY, int levelOfDetail)
        {
            StringBuilder quadKey = new StringBuilder();
            for (int i = levelOfDetail; i > 0; i--)
            {
                char digit = '0';
                int mask = 1 << (i - 1);
                if ((tileX & mask) != 0)
                {
                    digit++;
                }
                if ((tileY & mask) != 0)
                {
                    digit++;
                    digit++;
                }
                quadKey.Append(digit);
            }
            return quadKey.ToString();
        }

        /// <summary>  
        /// Clips a number to the specified minimum and maximum values.  
        /// </summary>  
        /// <param name="n">The number to clip.</param>  
        /// <param name="minValue">Minimum allowable value.</param>  
        /// <param name="maxValue">Maximum allowable value.</param>  
        /// <returns>The clipped value.</returns>  
        private double Clip(double n, double minValue, double maxValue)
        {
            return Math.Min(Math.Max(n, minValue), maxValue);
        }

        /// <summary>  
        /// Determines the map width and height (in pixels) at a specified level  
        /// of detail.  
        /// </summary>  
        /// <param name="levelOfDetail">Level of detail, from 1 (lowest detail)  
        /// to 23 (highest detail).</param>  
        /// <returns>The map width and height in pixels.</returns>  
        private uint MapSize(int levelOfDetail)
        {
            return (uint)256 << levelOfDetail;
        }
    }
}
